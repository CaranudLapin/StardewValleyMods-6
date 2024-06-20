﻿using Common;
using FreeLock.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;

namespace FreeLock;

internal class ModEntry : Mod
{
    private ModConfig config = null!;
    public override void Entry(IModHelper helper)
    {
        // 初始化
        config = helper.ReadConfig<ModConfig>();
        Log.Init(Monitor);
        I18n.Init(helper.Translation);
        // 注册事件
        helper.Events.GameLoop.GameLaunched += OnGameLaunched;
        helper.Events.GameLoop.UpdateTicked += OnUpdateTicked;
        helper.Events.Input.ButtonsChanged += OnButtonChanged;
    }

    private void OnUpdateTicked(object? sender, UpdateTickedEventArgs e)
    {
        if (!Context.IsWorldReady || !Game1.viewportFreeze) return;
        
        var mouseX = Game1.getOldMouseX(false);
        var mouseY = Game1.getOldMouseY(false);
        var moveSpeed = config.MoveSpeed;
        var moveThreshold = config.MoveThreshold;
        
        // 水平移动
        if (mouseX < moveThreshold)
            Game1.panScreen(-moveSpeed, 0);
        else if (mouseX - Game1.viewport.Width >= -moveThreshold) 
            Game1.panScreen(moveSpeed, 0);
        
        // 垂直移动
        if (mouseY < moveThreshold)
            Game1.panScreen(0, -moveSpeed);
        else if (mouseY - Game1.viewport.Height >= -moveThreshold) 
            Game1.panScreen(0, moveSpeed);
    }

    private void OnButtonChanged(object? sender, ButtonsChangedEventArgs e)
    {
        if (!Context.IsWorldReady) return;

        if (config.FreeLockKeybind.JustPressed())
        {
            if (Game1.viewportFreeze)
            {
                Game1.viewportFreeze = false;
                Game1.addHUDMessage(new HUDMessage(I18n.UI_ViewportLocked()));
            }
            else
            {
                Game1.viewportFreeze = true;
                Game1.addHUDMessage(new HUDMessage(I18n.UI_ViewportUnlocked()));
            }
        }
    }

    private void OnGameLaunched(object? sender, GameLaunchedEventArgs e)
    {
        new GenericModConfigMenuForFreeLock(
            Helper,
            ModManifest,
            () => config,
            () => config = new ModConfig(),
            () => Helper.WriteConfig(config)
        ).Register();
    }
}