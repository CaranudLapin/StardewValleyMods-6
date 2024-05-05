﻿using Microsoft.Xna.Framework;
using StardewValley;
using StardewValley.Locations;
using StardewValley.TerrainFeatures;
using StardewValley.Tools;
using SObject = StardewValley.Object;

namespace LazyMod.Framework.Automation;

public class AutoFarming : Automate
{
    private readonly ModConfig config;

    public AutoFarming(ModConfig config)
    {
        this.config = config;
    }

    public override void AutoDoFunction(GameLocation location, Farmer player, Tool? tool, Item? item)
    {
        TileCache.Clear();
        // 自动耕地
        if (config.AutoTillDirt && tool is Hoe) AutoTillDirt(location, player, tool);
        // 自动清理耕地
        if (config.AutoClearTilledDirt && tool is Pickaxe) AutoClearTilledDirt(location, player, tool);
        // 自动浇水
        if (config.AutoWaterDirt && tool is WateringCan wateringCan) AutoWaterDirt(location, player, wateringCan);
        // 自动填充水壶
        if (config.AutoRefillWateringCan && (tool is WateringCan || config.FindWateringCanFromInventory)) AutoRefillWateringCan(location, player);
        // 自动播种
        if (config.AutoSeed && item?.Category == SObject.SeedsCategory) AutoSeed(location, player, item);
        // 自动施肥
        if (config.AutoFertilize && item?.Category == SObject.fertilizerCategory) AutoFertilize(location, player, item);
        // 自动收获作物
        if (config.AutoHarvestCrop) AutoHarvestCrop(location, player);
        // 自动摇晃果树
        if (config.AutoShakeFruitTree) AutoShakeFruitTree(location, player);
        // 自动清理枯萎作物
        if (config.AutoClearDeadCrop) AutoClearDeadCrop(location, player);
        TileCache.Clear();
    }

    // 自动耕地
    private void AutoTillDirt(GameLocation location, Farmer player, Tool tool)
    {
        var grid = GetTileGrid(player, config.AutoTillDirtRange);
        foreach (var tile in grid)
        {   
            if (!CanTillDirt(location, tile)) continue;
            if (player.Stamina <= config.StopTillDirtStamina) break;
            UseToolOnTile(location, player, tool, tile);
        }
    }

    // 自动清理耕地
    private void AutoClearTilledDirt(GameLocation location, Farmer player, Tool tool)
    {
        var hasAddMessage = true;
        var grid = GetTileGrid(player, config.AutoClearTilledDirtRange);
        foreach (var tile in grid)
        {
            location.terrainFeatures.TryGetValue(tile, out var tileFeature);
            if (tileFeature is HoeDirt { crop: null } hoeDirt && hoeDirt.state.Value == HoeDirt.dry)
            {
                if (player.Stamina <= config.StopClearTilledDirtStamina) break;
                UseToolOnTile(location, player, tool, tile);
            }
        }
    }

    // 自动浇水
    private void AutoWaterDirt(GameLocation location, Farmer player, WateringCan wateringCan)
    {
        var hasAddWaterMessage = true;
        var grid = GetTileGrid(player, config.AutoWaterDirtRange);
        foreach (var tile in grid)
        {
            location.terrainFeatures.TryGetValue(tile, out var tileFeature);
            if (tileFeature is HoeDirt hoeDirt && hoeDirt.state.Value == HoeDirt.dry)
            {
                if (wateringCan.WaterLeft <= 0)
                {
                    if (!hasAddWaterMessage) Game1.showRedMessageUsingLoadString("Strings\\StringsFromCSFiles:WateringCan.cs.14335");
                    break;
                }

                hasAddWaterMessage = false;

                if (player.Stamina <= config.StopWaterDirtStamina) break;
                UseToolOnTile(location, player, wateringCan, tile);
                if (wateringCan.WaterLeft > 0 && player.ShouldHandleAnimationSound())
                {
                    player.playNearbySoundLocal("wateringCan");
                }
            }
        }
    }

    // 自动填充水壶
    private void AutoRefillWateringCan(GameLocation location, Farmer player)
    {
        var wateringCan = FindToolFromInventory<WateringCan>();
        if (wateringCan is null || wateringCan.WaterLeft == wateringCan.waterCanMax)
            return;
        
        var grid = GetTileGrid(player, config.AutoRefillWateringCanRange);
        foreach (var tile in grid.Where(tile => location.CanRefillWateringCanOnTile((int)tile.X, (int)tile.Y)))
        {
            UseToolOnTile(location, player, wateringCan, tile);
            break;
        }
    }

    // 自动播种
    private void AutoSeed(GameLocation location, Farmer player, Item item)
    {
        var grid = GetTileGrid(player, config.AutoSeedRange);
        foreach (var tile in grid)
        {
            location.terrainFeatures.TryGetValue(tile, out var terrainFeature);
            if (terrainFeature is HoeDirt { crop: null } hoeDirt)
            {
                location.objects.TryGetValue(tile, out var obj);
                if (obj is not null) continue;
                
                if (item.Stack <= 0)
                    break;

                if (hoeDirt.plant(item.ItemId, player, false)) player.reduceActiveItemByOne();
            }
        }
    }

    // 自动施肥
    private void AutoFertilize(GameLocation location, Farmer player, Item item)
    {
        var grid = GetTileGrid(player, config.AutoFertilizeRange);
        foreach (var tile in grid)
        {
            location.terrainFeatures.TryGetValue(tile, out var terrainFeature);
            if (item.Stack <= 0)
                break;
            switch (item.QualifiedItemId)
            {
                // 树肥逻辑
                case "(O)805":
                    if (terrainFeature is Tree tree && !tree.fertilized.Value && tree.growthStage.Value < Tree.treeStage && tree.fertilize())
                        player.reduceActiveItemByOne();
                    break;
                // 其他肥料逻辑
                default:
                    if (terrainFeature is HoeDirt hoeDirt && hoeDirt.plant(item.ItemId, player, true))
                        player.reduceActiveItemByOne();
                    break;
            }
        }
    }

    // 自动收获作物
    private void AutoHarvestCrop(GameLocation location, Farmer player)
    {
        var grid = GetTileGrid(player, config.AutoHarvestCropRange);
        foreach (var tile in grid)
        {
            location.terrainFeatures.TryGetValue(tile, out var terrainFeature);
            if (terrainFeature is HoeDirt { crop: not null } hoeDirt)
            {
                var crop = hoeDirt.crop;
                // 自动收获花逻辑
                if (!config.AutoHarvestFlower && ItemRegistry.GetData(crop.indexOfHarvest.Value)?.Category == SObject.flowersCategory)
                    continue;
                if (crop.harvest((int)tile.X, (int)tile.Y, hoeDirt))
                {
                    hoeDirt.destroyCrop(true);
                    // 姜岛金核桃逻辑
                    if (location is IslandLocation && Game1.random.NextDouble() < 0.05)
                        player.team.RequestLimitedNutDrops("IslandFarming", location, (int)tile.X * 64, (int)tile.Y * 64, 5);
                }
            }
        }
    }

    // 自动摇晃果树
    private void AutoShakeFruitTree(GameLocation location, Farmer player)
    {
        var grid = GetTileGrid(player, config.AutoShakeFruitTreeRange);
        foreach (var tile in grid)
        {
            location.terrainFeatures.TryGetValue(tile, out var terrainFeature);
            if (terrainFeature is FruitTree fruitTree && fruitTree.fruit.Count > 0)
                fruitTree.performUseAction(tile);
        }
    }

    // 自动清理枯萎作物
    private void AutoClearDeadCrop(GameLocation location, Farmer player)
    {
        var grid = GetTileGrid(player, config.AutoHarvestCropRange);
        foreach (var tile in grid)
        {
            location.terrainFeatures.TryGetValue(tile, out var terrainFeature);
            if (terrainFeature is HoeDirt { crop: not null } hoeDirt)
            {
                var crop = hoeDirt.crop;
                if (crop.dead.Value)
                    hoeDirt.performToolAction(FakeScythe.Value, 0, tile);
            }
        }
    }

    private bool CanTillDirt(GameLocation location, Vector2 tile)
    {
        location.terrainFeatures.TryGetValue(tile, out var tileFeature);
        location.objects.TryGetValue(tile, out var obj);
        return tileFeature is null && obj is null &&
               !location.IsTileOccupiedBy(tile, CollisionMask.All, CollisionMask.Farmers) &&
               location.isTilePassable(tile) &&
               location.doesTileHaveProperty((int)tile.X, (int)tile.Y, "Diggable", "Back") is not null;
    }
}