using System.Reflection.Emit;
using Common.Patch;
using HarmonyLib;
using StardewValley;

namespace TestMod.Patches;

public class GameLocationPatcher : BasePatcher
{
    public override void Patch(Harmony harmony)
    {
        harmony.Patch(
            RequireMethod<GameLocation>(nameof(GameLocation.performTenMinuteUpdate)),
            transpiler: GetHarmonyMethod(nameof(PerformTenMinuteUpdateTranspiler))
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