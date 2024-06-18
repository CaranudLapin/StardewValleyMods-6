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
                config => config.EnableKickDelayedPlayer,
                (config, value) => config.EnableKickDelayedPlayer = value,
                I18n.Config_EnableKickDelayedPlayer_Name,
                I18n.Config_EnableKickDelayedPlayer_Tooltip
            )
            // 模组限制
            .AddBoolOption(
                config => config.EnableModLimit,
                (config, value) => config.EnableModLimit = value,
                I18n.Config_EnableModLimit_Name
            )
            // 禁止金钱作弊
            .AddBoolOption(
                config => config.BanMoneyCheat,
                (config, value) => config.BanMoneyCheat = value,
                I18n.Config_EnableBanMoneyCheat_Name
            )
            // 显示玩家数量
            .AddBoolOption(
                config => config.ShowPlayerCount,
                (config, value) => config.ShowPlayerCount = value,
                I18n.Config_ShowPlayerCount_Name
            )
            // 踢出未准备玩家
            .AddBoolOption(
                config => config.EnableKickUnreadyPlayer,
                (config, value) => config.EnableKickUnreadyPlayer = value,
                I18n.Config_EnableKickUnreadyPlayer_Name
            )
            .AddKeybindList(
                config => config.KickUnreadyPlayerKey,
                (config, value) => config.KickUnreadyPlayerKey = value,
                I18n.Config_KickUnreadyPlayerKey_Name
            )
            ;
    }
}