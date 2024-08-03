﻿using StardewModdingAPI;
using StardewModdingAPI.Utilities;

namespace weizinai.StardewValleyMod.ActiveMenuAnywhere.Framework;

internal class ModConfig
{
    public static ModConfig Instance { get; set; } = null!;

    public static void Init(IModHelper helper)
    {
        Instance = helper.ReadConfig<ModConfig>();
    }

    public KeybindList MenuKey { get; set; } = new(SButton.L);
    public bool OpenMenuByTelephone { get; set; }
    public MenuTabId DefaultMeanTabId { get; set; } = MenuTabId.Town;
    public bool ProgressMode { get; set; } = true;
}