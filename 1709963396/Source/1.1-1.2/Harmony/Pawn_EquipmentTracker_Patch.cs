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
    internal static class Pawn_EquipmentTracker_Patch
    {
        [HarmonyPatch(typeof(Pawn_EquipmentTracker), "TryTransferEquipmentToContainer")]
        public class TryTransferEquipmentToContainer
        {
            [HarmonyPrefix]
            public static bool Listener(Pawn_EquipmentTracker __instance, ThingWithComps eq, ThingOwner container)
            {
                if (__instance.pawn.health.hediffSet.GetFirstHediffOfDef(DefDatabase<HediffDef>.GetNamed("aRandomKiwi_DFM_WTBD")) != null)
                    return false;
                return true;
            }
        }

        [HarmonyPatch(typeof(Pawn_EquipmentTracker), "TryDropEquipment")]
        public class TryDropEquipment
        {
            [HarmonyPrefix]
            public static bool Listener(Pawn_EquipmentTracker __instance, ThingWithComps eq, out ThingWithComps resultingEq, IntVec3 pos, bool forbid = true)
            {
                resultingEq = null;
                if (__instance.pawn.health.hediffSet.GetFirstHediffOfDef(DefDatabase<HediffDef>.GetNamed("aRandomKiwi_DFM_WTBD")) != null)
                    return false;
                return true;
            }
        }


        [HarmonyPatch(typeof(Pawn_EquipmentTracker), "DestroyEquipment")]
        public class DestroyEquipment
        {
            [HarmonyPrefix]
            public static bool Listener(Pawn_EquipmentTracker __instance, ThingWithComps eq)
            {
                if (__instance.pawn.health.hediffSet.GetFirstHediffOfDef(DefDatabase<HediffDef>.GetNamed("aRandomKiwi_DFM_WTBD")) != null)
                    return false;
                return true;
            }
        }
    }
}
