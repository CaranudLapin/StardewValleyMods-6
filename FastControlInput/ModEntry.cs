﻿using Common.Patch;
using FastControlInput.Framework;
using FastControlInput.Patches;
using StardewModdingAPI;
using StardewModdingAPI.Events;

namespace FastControlInput;

internal class ModEntry : Mod
{
    private ModConfig config = null!;

    public override void Entry(IModHelper helper)
    {
        // 初始化
        I18n.Init(helper.Translation);
        config = helper.ReadConfig<ModConfig>();
        // 注册事件
        helper.Events.GameLoop.GameLaunched += OnGameLaunched;
        // 注册Harmony补丁
        HarmonyPatcher.Apply(this, new ModHooksPatcher(() => config));
    }

    private void OnGameLaunched(object? sender, GameLaunchedEventArgs e)
    {
        new GenericModConfigMenuIntegrationForFastControlInput(
            Helper,
            ModManifest,
            () => config,
            () => config = new ModConfig(),
            () => Helper.WriteConfig(config)
        ).Register();
    }
}