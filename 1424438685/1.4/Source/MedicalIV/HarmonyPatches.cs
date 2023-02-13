﻿using RimWorld;
using Verse;
using HarmonyLib;
using System.Reflection;

[StaticConstructorOnStartup]
class Main
{
    static Main()
    {
        var harmony = new Harmony("com.medical.iv");
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
