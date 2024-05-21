using Common.Patch;
using FastControlInput.Framework;
using HarmonyLib;
using StardewValley;
using StardewValley.Mods;

namespace FastControlInput.Patches;

internal class ModHooksPatcher : BasePatcher
{
    private static Func<ModConfig> getConfig = null!;
    private static ModConfig Config => getConfig();

    private static float actionButtonRemainder;
    private static float useToolButtonRemainder;

    public ModHooksPatcher(Func<ModConfig> getConfig)
    {
        ModHooksPatcher.getConfig = getConfig;
    }
    
    public override void Apply(Harmony harmony)
    {
        harmony.Patch(
            RequireMethod<ModHooks>(nameof(ModHooks.OnGame1_UpdateControlInput)),
            postfix: GetHarmonyMethod(nameof(UpdateControlInputPostfix))
        );
    }

    private static void UpdateControlInputPostfix()
    {
        var gameTime = Game1.currentGameTime;
         
        for (var i = 0; i < GetSkipsThisTick(Config.ActionButton,ref actionButtonRemainder); i++)
            Game1.rightClickPolling -= gameTime.ElapsedGameTime.Milliseconds;
        
        for (var i = 0; i < GetSkipsThisTick(Config.UseToolButton,ref useToolButtonRemainder); i++) 
            Game1.mouseClickPolling += gameTime.ElapsedGameTime.Milliseconds;
    }

    private static int GetSkipsThisTick(float multiplier, ref float remainder)
    {
        if (multiplier <= 1) return 0;

        var skips = multiplier + remainder - 1;
        remainder = skips % 1;
        return (int)skips;
    }
}