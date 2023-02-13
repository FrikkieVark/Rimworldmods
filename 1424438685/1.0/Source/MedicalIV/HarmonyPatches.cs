using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;
using Verse.AI;
using Verse.Sound;
using Harmony;
using Harmony.ILCopying;
using IV;
using System.Collections;
using System.Reflection;

[StaticConstructorOnStartup]
class Main
{
    static Main()
    {
        var harmony = HarmonyInstance.Create("com.medical.iv");
        harmony.PatchAll(Assembly.GetExecutingAssembly());
    }
}

[HarmonyPatch(typeof(HediffSet), "CalculateBleedRate")]
static class HarmonyPatches
{
    static void Postfix(ref float __result, HediffSet __instance)
    {
        __result *= __instance.pawn.GetStatValue(StatDef.Named("BleedRate"));
    }
}
