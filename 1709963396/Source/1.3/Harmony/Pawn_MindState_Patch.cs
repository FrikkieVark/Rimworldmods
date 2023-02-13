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
    internal static class Pawn_MindState_Patch
    {
        [HarmonyPatch(typeof(Pawn_MindState), "GetGizmos")]
        public class GetGizmos
        {
            [HarmonyPostfix]
            public static void Listener(Pawn_MindState __instance, ref IEnumerable<Gizmo> __result)
            {
                List<Gizmo> ret = new List<Gizmo>();
                Gizmo cur;

                //Si pas pawn appartenant au player on affiche rien
                if (__instance == null || __instance.pawn.Faction != Faction.OfPlayer || __instance.pawn.Dead 
                    || __instance.pawn.InMentalState 
                    || (__instance.pawn.RaceProps.Animal && !__instance.pawn.training.HasLearned(TrainableDefOf.Obedience)))
                {
                    return;
                }


                //Si possede hediff de down
                if (__instance.pawn.health.hediffSet.GetFirstHediffOfDef(DefDatabase<HediffDef>.GetNamed("aRandomKiwi_DFM_WTBD")) != null)
                {
                    //Ajout bouton de dedownage du pawn
                    cur = new Command_Action
                    {
                        icon = Utils.texUndown,
                        defaultLabel = "aRandomKiwi_DFM_ForceUndownLabel".Translate(),
                        defaultDesc = "aRandomKiwi_DFM_ForceUndownDesc".Translate(),
                        action = delegate ()
                        {
                            //Remove headiff
                            __instance.pawn.health.RemoveHediff(__instance.pawn.health.hediffSet.GetFirstHediffOfDef(DefDatabase<HediffDef>.GetNamed("aRandomKiwi_DFM_WTBD")));
                        }
                    };
                    ret.Add(cur);
                }
                else if(!__instance.pawn.Downed)
                {
                    //Ajout bouton de downage du pawn
                    cur = new Command_Action
                    {
                        icon = Utils.texDown,
                        defaultLabel = "aRandomKiwi_DFM_ForceDownLabel".Translate(),
                        defaultDesc = "aRandomKiwi_DFM_ForceDownDesc".Translate(),
                        action = delegate ()
                        {
                            //Ajout headiff pour downer le pawn
                            __instance.pawn.health.AddHediff(DefDatabase<HediffDef>.GetNamed("aRandomKiwi_DFM_WTBD"));
                        }
                    };
                    ret.Add(cur);
                }

                __result = __result.Concat(ret);
            }
        }
    }
}
