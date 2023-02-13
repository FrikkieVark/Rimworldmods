using System.Collections.Generic;
using Verse;

namespace IV.Electrical
{
    public class IV_E_Antibiotic : IV_E_Base
    {
        public IV_E_Antibiotic() : base("Electrical Antibiotic", new List<HediffDef> { HediffDef.Named("IV_Antibiotics_Hediff") }) {
        }

        public override bool OnAddPatient(Pawn pawn) {
            return true;
        }

        public override bool OnRemovePatient(Pawn pawn) {
            return true;
        }
    }
}

