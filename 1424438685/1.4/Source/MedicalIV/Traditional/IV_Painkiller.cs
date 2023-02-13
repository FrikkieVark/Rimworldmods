using Verse;
using RimWorld;
using Verse.AI;
using System.Collections.Generic;

namespace IV.Traditional
{
    public class IV_Painkiller : IV_Base
    {
        public IV_Painkiller() : base("Painkiller", FuelType.Resource, new List<HediffDef> { HediffDef.Named("IV_Painkiller_Hediff") }, 0.0075f) {
        }

        public override bool OnAddPatient(Pawn pawn) {
            return true;
        }

        /** Waits until pawn would be able to walk before removing painkiller hediff
        * Tries to issue PatientGoToBed job to prevent them from going down and to maintain treatment
        * This does mean pawns could have the IV effects when not in bed (until they heal enough) */
        public override bool OnRemovePatient(Pawn pawn) {
            if (pawn.health.InPainShock) {
                var jgp = new JobGiver_PatientGoToBed { respectTimetable = false };
                var result = jgp.TryIssueJobPackage(pawn, new JobIssueParams());
                if (result == ThinkResult.NoJob) {
                    Log.Warning("[Medical IVs] Pawn [" + pawn.Name + "] gave up painkiller IV treatment despite pain shock and cannot be reissued a 'bed rest' job. " +
                                "Painkiller hediff will persist until they are able to tolerate the pain...");
                }
                return false;                                                                                
            }
            return true;
        }
    }
}

