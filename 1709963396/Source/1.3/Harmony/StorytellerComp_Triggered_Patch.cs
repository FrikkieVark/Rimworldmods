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
    internal static class StorytellerComp_Triggered_Patch
    {
        [HarmonyPatch(typeof(StorytellerComp_Triggered), "Notify_PawnEvent")]
        public class Notify_PawnEvent
        {
            [HarmonyPrefix]
            public static bool Listener(Pawn p, AdaptationEvent ev, DamageInfo? dinfo = null)
            {
                if (!p.RaceProps.Humanlike || !p.IsColonist)
                {
                    return false;
                }
                //Si pawn downed ET possede hediff WANT TO BE DOWN alors on ignore afin d'éviter l'evenement stanger in black
                if ((ev == AdaptationEvent.Downed) &&  p.health.hediffSet.GetFirstHediffOfDef(DefDatabase<HediffDef>.GetNamed("aRandomKiwi_DFM_WTBD")) != null)
                {
                    return false;
                }
                return true;
            }
        }
    }
}