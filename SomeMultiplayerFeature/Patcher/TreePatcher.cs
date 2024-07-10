using HarmonyLib;
using StardewValley;
using StardewValley.TerrainFeatures;
using StardewValley.Tools;
using weizinai.StardewValleyMod.Common.Patcher;

namespace weizinai.StardewValleyMod.SomeMultiplayerFeature.Patcher;

internal class TreePatcher : BasePatcher
{
    public override void Apply(Harmony harmony)
    {
        harmony.Patch(
            original: this.RequireMethod<Tree>("performTreeFall"),
            postfix: this.GetHarmonyMethod(nameof(PerformTreeFallPostfix))
        );
    }

    // 修改砍树获得的经验为20点
    // 修改砍树桩获得的经验为10点
    private static void PerformTreeFallPostfix(Tool t, Tree __instance)
    {
        if (t is Axe)
        {
            t.getLastFarmerToUse().gainExperience(Farmer.foragingSkill, __instance.stump.Value ? 9 : 8);
        }
    }
}