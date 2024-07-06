using weizinai.StardewValleyMod.Common.Integration;
using StardewModdingAPI;

namespace weizinai.StardewValleyMod.SpectatorMode.Framework;

internal class GenericModConfigMenuForSpectatorMode
{
    private readonly GenericModConfigMenuIntegration<ModConfig> configMenu;

    public GenericModConfigMenuForSpectatorMode(IModHelper helper, IManifest manifest, Func<ModConfig> getConfig, Action reset, Action save)
    {
        this.configMenu = new GenericModConfigMenuIntegration<ModConfig>(helper.ModRegistry, manifest, getConfig, reset, save);
    }

    public void Register()
    {
        if (!this.configMenu.IsLoaded) return;

        this.configMenu
            .Register()
            // 旁观者模式
            .AddSectionTitle(I18n.Config_SpectatorModeTitle_Name)
            .AddKeybindList(
                config => config.SpectateLocationKey,
                (config, value) => config.SpectateLocationKey = value,
                I18n.Config_SpectateLocationKey_Name
            )
            .AddBoolOption(
                config => config.OnlyShowOutdoors,
                (config, value) => config.OnlyShowOutdoors = value,
                I18n.Config_OnlyShowOutdoors_Name
            )
            .AddKeybindList(
                config => config.SpectatePlayerKey,
                (config, value) => config.SpectatePlayerKey = value,
                I18n.Config_SpectatePlayerKey_Name
            )
            .AddKeybindList(
                config => config.ToggleStateKey,
                (config, value) => config.ToggleStateKey = value,
                I18n.Config_ToggleStateKey_Name
            )
            .AddNumberOption(
                config => config.MoveSpeed,
                (config, value) => config.MoveSpeed = value,
                I18n.Config_MoveSpeed_Name
            )
            .AddNumberOption(
                config => config.MoveThreshold,
                (config, value) => config.MoveThreshold = value,
                I18n.Config_MoveThreshold_Name
            )
            .AddBoolOption(
                config => config.ShowTimeAndMoney,
                (config, value) => config.ShowTimeAndMoney = value,
                I18n.Config_ShowTimeAndMoney_Name,
                I18n.Config_ShowTimeAndMoney_Tooltip
            )
            // 轮播玩家
            .AddSectionTitle(I18n.Config_RotatePlayerTitle_Name)
            .AddKeybindList(
                config => config.RotatePlayerKey,
                (config, value) => config.RotatePlayerKey = value,
                I18n.Config_RotatePlayerKey_Name
            )
            .AddNumberOption(
                config => config.RotationInterval,
                (config, value) => config.RotationInterval = value,
                I18n.Config_RotationInterval_Name
            )
            ;
    }
}