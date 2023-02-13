using System.Collections.Generic;
using Verse;

namespace IV.Electrical
{
    public class IV_E_Blood : IV_E_Base
    {
        public IV_E_Blood() : base("Electrical Blood", new List<HediffDef> { HediffDef.Named("IV_Blood_Hediff") }) {
        }

        public override bool OnAddPatient(Pawn pawn) {
            return true;
        }

        public override bool OnRemovePatient(Pawn pawn) {
            return true;
        }
    }
}

