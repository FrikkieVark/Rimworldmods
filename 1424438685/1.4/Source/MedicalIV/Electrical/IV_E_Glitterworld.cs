using System.Collections.Generic;
using RimWorld;
using Verse;
using Verse.AI;

namespace IV.Electrical
{
    public class IV_E_Glitterworld : IV_E_Base
    {
        public IV_E_Glitterworld() : base("Glitterworld",
               new List<HediffDef> { HediffDef.Named("IV_Blood_Hediff"), HediffDef.Named("IV_Painkiller_Hediff"), HediffDef.Named("IV_Antibiotics_Hediff") }) {
        }

        public override bool OnAddPatient(Pawn pawn) {
            return true;
        }

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

