using System.Reflection.Emit;
using weizinai.StardewValleyMod.Common.Patcher;
using HarmonyLib;
using StardewValley;

namespace weizinai.StardewValleyMod.TestMod.Patches;

internal class GameLocationPatcher : BasePatcher
{
    public override void Apply(Harmony harmony)
    {
        harmony.Patch(this.RequireMethod<GameLocation>(nameof(GameLocation.performTenMinuteUpdate)),
            transpiler: this.GetHarmonyMethod(nameof(PerformTenMinuteUpdateTranspiler))
        );
    }

    public static IEnumerable<CodeInstruction> PerformTenMinuteUpdateTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        var codes = instructions.ToList();
        var index = codes.FindIndex(code => code.opcode == OpCodes.Ldc_R8 && Math.Abs((double)code.operand - 0.01) < 0.0001);
        codes[index].operand = 1d;
        index = codes.FindIndex(code => code.opcode == OpCodes.Ldc_R8 && Math.Abs((double)code.operand - 0.008) < 0.0001);
        codes[index].operand = 1d;
        index = codes.FindIndex(code => code.opcode == OpCodes.Ldc_I4_2);
        index = codes.FindIndex(index, code => code.opcode == OpCodes.Ldc_I4_2);
        codes[index].operand = 999;
        return codes.AsEnumerable();
    }
}