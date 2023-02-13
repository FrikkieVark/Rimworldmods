using System.Collections.Generic;
using Verse;

namespace IV.Electrical
{
    public class IV_E_Anesthetic : IV_E_Base
    {
        public IV_E_Anesthetic() : base("Electrical Anesthetic", new List<HediffDef> { HediffDef.Named("IV_Anesthetic_Hediff") }) {
        }

        public override bool OnAddPatient(Pawn pawn) {
            return true;
        }

        public override bool OnRemovePatient(Pawn pawn) {
            return true;
        }
    }
}

