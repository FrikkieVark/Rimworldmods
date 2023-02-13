using System.Collections.Generic;
using System.Linq;
using System;
using Harmony;
using RimWorld;
using UnityEngine;
using Verse;
using Verse.AI;

namespace aRandomKiwi.DFM
{
    internal static class Pawn_Patch
    {
        [HarmonyPatch(typeof(Pawn), "DropAndForbidEverything")]
        public class DropAndForbidEverything
        {
            [HarmonyPrefix]
            public static bool Listener(Pawn __instance, bool keepInventoryAndEquipmentIfInBed = false)
            {
                if (__instance == null || __instance.health.hediffSet.GetFirstHediffOfDef(DefDatabase<HediffDef>.GetNamed("aRandomKiwi_DFM_WTBD")) != null)
                    return false;
                return true;
            }
        }
    }
}