using System.Reflection;
using System.Reflection.Emit;
using weizinai.StardewValleyMod.Common.Patcher;
using HarmonyLib;
using StardewValley;
using StardewValley.Locations;
using weizinai.StardewValleyMod.TestMod.Framework;

namespace weizinai.StardewValleyMod.TestMod.Patches;

internal class MineShaftPatcher : BasePatcher
{
    private static ModConfig config = null!;

    public MineShaftPatcher(ModConfig config)
    {
        MineShaftPatcher.config = config;
    }

    public override void Apply(Harmony harmony)
    {
        harmony.Patch(this.RequireMethod<MineShaft>(nameof(MineShaft.loadLevel)), transpiler: this.GetHarmonyMethod(nameof(LoadLevelTranspiler)));
    }

    private static IEnumerable<CodeInstruction> LoadLevelTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        var codes = new List<CodeInstruction>(instructions);

        var findCreateDaySaveRandomMethod = false;
        var findChanceField = false;
        for (var i = 0; i < codes.Count; i++)
        {
            if (!findChanceField && codes[i].opcode == OpCodes.Call)
            {
                if (codes[i].opcode == OpCodes.Call && codes[i].operand.Equals(AccessTools.Method(typeof(Utility), nameof(Utility.CreateDaySaveRandom))))
                    findCreateDaySaveRandomMethod = true;
            }

            if (findCreateDaySaveRandomMethod && !findChanceField)
            {
                if (codes[i].opcode == OpCodes.Ldc_R8 && Math.Abs((double)codes[i].operand - 0.06) < 0.1)
                {
                    codes[i].operand = 1.0;
                    findChanceField = true;
                }
            }

            if (findChanceField)
            {
                if (codes[i].opcode == OpCodes.Stloc_1)
                {
                    codes.Insert(i - 3, new CodeInstruction(OpCodes.Ldc_I4, config.MineShaftMap));
                    codes.Insert(i - 2, new CodeInstruction(OpCodes.Stloc_0));
                    break;
                }
            }
        }

        return codes.AsEnumerable();
    }
}