using Common;
using SomeMultiplayerFeature.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;

namespace SomeMultiplayerFeature.Handlers;

internal class ModLimitHandler
{
    private const string ModRequirementPatch = "assets/ModRequirement.json";

    private readonly ModConfig config;
    private readonly Dictionary<string, string[]>? modRequirement;

    public ModLimitHandler(IModHelper helper, ModConfig config)
    {
        this.config = config;
        // 读取要求模组列表
        modRequirement = helper.Data.ReadJsonFile<Dictionary<string, string[]>>(ModRequirementPatch);
        if (modRequirement is null)
            Log.Error($"无法找到Json文件: {ModRequirementPatch}");
    }

    public void OnPeerContextReceived(PeerContextReceivedEventArgs e)
    {
        if (!Context.IsMainPlayer || !e.Peer.HasSmapi || modRequirement is null || !config.EnableModLimit) return;

        var targetMods = e.Peer.Mods.Select(mod => mod.Name).ToList();
        var unAllowedMods = new List<string>();

        foreach (var id in modRequirement["RequiredModList"])
        {
            if (!targetMods.Contains(id))
                unAllowedMods.Add(id);
        }

        foreach (var id in targetMods)
        {
            if (!modRequirement["RequiredModList"].Contains(id) && !modRequirement["AllowedModList"].Contains(id))
                unAllowedMods.Add(id);
        }

        if (unAllowedMods.Any())
            Game1.server.kick(e.Peer.PlayerID);
    }
}