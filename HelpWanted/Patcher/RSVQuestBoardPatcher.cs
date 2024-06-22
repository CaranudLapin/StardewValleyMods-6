using HarmonyLib;
using Microsoft.Xna.Framework.Graphics;
using StardewValley;
using weizinai.StardewValleyMod.Common.Patcher;
using weizinai.StardewValleyMod.HelpWanted.Framework;
using weizinai.StardewValleyMod.HelpWanted.Framework.Menu;

namespace weizinai.StardewValleyMod.HelpWanted.Patcher;

internal class RSVQuestBoardPatcher : BasePatcher
{
    private static ModConfig config = null!;

    public RSVQuestBoardPatcher(ModConfig config)
    {
        RSVQuestBoardPatcher.config = config;
    }

    public override void Apply(Harmony harmony)
    {
        harmony.Patch(
            AccessTools.Method(Type.GetType("RidgesideVillage.Questing.RSVQuestBoard,RidgesideVillage"), "draw", new[] { typeof(SpriteBatch) }),
            GetHarmonyMethod(nameof(DrawPrefix))
        );
    }

    private static bool DrawPrefix(string ___boardType)
    {
        if (___boardType != "VillageQuestBoard" || !config.EnableRSVQuestBoard) return true;
        Game1.activeClickableMenu = new RSVQuestBoard(config);
        return false;
    }
}