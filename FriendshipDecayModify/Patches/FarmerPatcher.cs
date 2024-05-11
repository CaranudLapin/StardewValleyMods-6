using System.Reflection.Emit;
using Common.Patch;
using FriendshipDecayModify.Framework;
using HarmonyLib;
using StardewValley;

namespace FriendshipDecayModify.Patches;

internal class FarmerPatcher : BasePatcher
{
    private static ModConfig config = null!;

    public FarmerPatcher(ModConfig config)
    {
        FarmerPatcher.config = config;
    }

    public override void Apply(Harmony harmony)
    {
        harmony.Patch(
            RequireMethod<Farmer>(nameof(Farmer.resetFriendshipsForNewDay)),
            transpiler: GetHarmonyMethod(nameof(ResetFriendshipsForNewDayTranspiler))
        );
    }

    // 每日对话修改
    private static IEnumerable<CodeInstruction> ResetFriendshipsForNewDayTranspiler(IEnumerable<CodeInstruction> instructions)
    {
        var codes = instructions.ToList();

        var index = codes.FindIndex(code => code.opcode == OpCodes.Ldc_I4_S && code.operand.Equals((sbyte)-20));
        codes[index] = new CodeInstruction(OpCodes.Call, GetMethod<FarmerPatcher>(nameof(GetDailyGreetingModifyForSpouse)));
        index = codes.FindIndex(index, code => code.opcode == OpCodes.Ldc_I4_S && code.operand.Equals((sbyte)-8));
        codes[index] = new CodeInstruction(OpCodes.Call, GetMethod<FarmerPatcher>(nameof(GetDailyGreetingModifyForDatingVillager)));
        index = codes.FindIndex(index, code => code.opcode == OpCodes.Ldc_I4_S && code.operand.Equals((sbyte)-2));
        codes[index] = new CodeInstruction(OpCodes.Call, GetMethod<FarmerPatcher>(nameof(GetDailyGreetingModifyForVillager)));

        return codes.AsEnumerable();
    }

    private static int GetDailyGreetingModifyForVillager()
    {
        return -config.DailyGreetingModifyForVillager;
    }

    private static int GetDailyGreetingModifyForDatingVillager()
    {
        return -config.DailyGreetingModifyForDatingVillager;
    }

    private static int GetDailyGreetingModifyForSpouse()
    {
        return -config.DailyGreetingModifyForSpouse;
    }
}