using System.Collections.Generic;
using System.Linq;
using System;
using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;
using Verse.AI;

namespace aRandomKiwi.DFM
{
    internal static class MapPawns_Patch
    {
        [HarmonyPatch(typeof(MapPawns), "get_AnyPawnBlockingMapRemoval")]
        public class MapPawns_get_AnyPawnBlockingMapRemoval
        {
            [HarmonyPostfix]
            public static void Listener(MapPawns __instance, ref bool __result, List<Pawn> ___pawnsSpawned)
            {
                //Si retour pas true alors check s'il y a de la correction a faire
                if (!__result) {
                    Faction ofPlayer = Faction.OfPlayer;
                    for (int i = 0; i < ___pawnsSpawned.Count; i++)
                    {
                        //Si pawn non décédé mais posséde Hediff de downage alors on rectifis le retour de la fonction
                        if (!___pawnsSpawned[i].Dead && ___pawnsSpawned[i].health != null && ___pawnsSpawned[i].health.hediffSet != null && ___pawnsSpawned[i].health.hediffSet.GetFirstHediffOfDef(DefDatabase<HediffDef>.GetNamed("aRandomKiwi_DFM_WTBD")) != null)
                        {
                            __result = true;
                            return;
                        }
                    }
                }
            }
        }
    }
}