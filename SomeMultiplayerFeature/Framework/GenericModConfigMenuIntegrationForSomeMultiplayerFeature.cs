using Common.Integrations;
using StardewModdingAPI;

namespace SomeMultiplayerFeature.Framework;

public class GenericModConfigMenuIntegrationForSomeMultiplayerFeature
{
    private readonly GenericModConfigMenuIntegration<ModConfig> configMenu;

    public GenericModConfigMenuIntegrationForSomeMultiplayerFeature(IModHelper helper, IManifest manifest, Func<ModConfig> getConfig, Action reset, Action save)
    {
        configMenu = new GenericModConfigMenuIntegration<ModConfig>(helper.ModRegistry, manifest, getConfig, reset, save);
    }

    public void Register()
    {
        if (!configMenu.IsLoaded) return;
        configMenu
            .Register()
            // 显示商店信息
            .AddBoolOption(
                config => config.ShowAccessShopInfo,
                (config, value) => config.ShowAccessShopInfo = value,
                I18n.Config_ShowAccessShopInfo_Name,
                I18n.Config_ShowAccessShopInfo_Tooltip
            )
            // 踢出延迟玩家
            .AddBoolOption(
                config => config.ShowDelayedPlayer,
                (config, value) => config.ShowDelayedPlayer = value,
                I18n.Config_ShowDelayedPlayer_Name,
                I18n.Config_ShowDelayedPlayer_Tooltip
            )
            // 模组限制
            .AddBoolOption(
                config => config.ModLimit,
                (config, value) => config.ModLimit = value,
                I18n.Config_ModLimit_Name,
                I18n.Config_ModLimit_Tooltip
            )
            // 显示玩家数量
            .AddBoolOption(
                config => config.ShowPlayerCount,
                (config, value) => config.ShowPlayerCount = value,
                I18n.Config_ShowPlayerCount_Name,
                I18n.Config_ShowPlayerCount_Tooltip
            )
            // 显示提示
            .AddBoolOption(
                config => config.ShowTip,
                (config, value) => config.ShowTip = value,
                I18n.Config_ShowTip_Name,
                I18n.Config_ShowTip_Tooltip
            )
            .AddTextOption(
                config => config.TipText,
                (config, value) => config.TipText = value,
                I18n.Config_TipText_Name
            )
            // 踢出未准备玩家
            .AddBoolOption(
                config => config.KickUnreadyPlayer,
                (config, value) => config.KickUnreadyPlayer = value,
                I18n.Config_KickUnreadyPlayer_Name,
                I18n.Config_KickUnreadyPlayer_Tooltip
            )
            .AddKeybindList(
                config => config.KickUnreadyPlayerKey,
                (config, value) => config.KickUnreadyPlayerKey = value,
                I18n.Config_KickUnreadyPlayerKey_Name
            )
            // 版本限制
            .AddBoolOption(
                config => config.VersionLimit,
                (config, value) => config.VersionLimit = value,
                I18n.Config_VersionLimit_Name,
                I18n.Config_VersionLimit_Tooltip
            )
            ;
    }
}