﻿using Microsoft.Xna.Framework;
using StardewValley;
using weizinai.StardewValleyMod.LazyMod.Framework;
using weizinai.StardewValleyMod.LazyMod.Framework.Config;
using xTile.Dimensions;
using Rectangle = Microsoft.Xna.Framework.Rectangle;
using SObject = StardewValley.Object;

namespace weizinai.StardewValleyMod.LazyMod.Automation;

internal abstract class Automate
{
    protected ModConfig Config;
    protected readonly Func<int, List<Vector2>> GetTileGrid = TileHelper.GetTileGrid;

    protected Automate(ModConfig config)
    {
        this.Config = config;
    }

    public abstract void Apply(GameLocation location, Farmer player, Tool? tool, Item? item);

    protected void UseToolOnTile(GameLocation location, Farmer player, Tool tool, Vector2 tile)
    {
        var tilePixelPosition = this.GetTilePixelPosition(tile);
        tool.swingTicker++;
        tool.DoFunction(location, (int)tilePixelPosition.X, (int)tilePixelPosition.Y, 1, player);
    }

    protected void ConsumeItem(Farmer player, Item item)
    {
        item.Stack--;
        if (item.Stack <= 0) player.removeItemFromInventory(item);
    }


    protected Vector2 GetTilePixelPosition(Vector2 tile, bool center = true)
    {
        return tile * Game1.tileSize + (center ? new Vector2(Game1.tileSize / 2f) : Vector2.Zero);
    }

    protected Rectangle GetTileBoundingBox(Vector2 tile)
    {
        var tilePixelPosition = this.GetTilePixelPosition(tile, false);
        return new Rectangle((int)tilePixelPosition.X, (int)tilePixelPosition.Y, Game1.tileSize, Game1.tileSize);
    }

    protected void CheckTileAction(GameLocation location, Farmer player, Vector2 tile)
    {
        location.checkAction(new Location((int)tile.X, (int)tile.Y), Game1.viewport, player);
    }

    protected void HarvestMachine(Farmer player, SObject? machine)
    {
        if (machine is null) return;

        var heldObject = machine.heldObject.Value;
        if (machine.readyForHarvest.Value && heldObject is not null)
        {
            if (player.freeSpotsInInventory() == 0 && !player.Items.ContainsId(heldObject.ItemId)) return;
            machine.checkForAction(player);
        }
    }
}