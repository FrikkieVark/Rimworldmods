using System.Collections.Generic;
using Verse;

namespace IV.Electrical
{
    public class IV_E_Nutrition : IV_E_Base
    {
        public IV_E_Nutrition() : base("Electrical Nutrition", new List<HediffDef> { HediffDef.Named("IV_Nutrition_Hediff") }) {
        }

        public override bool OnAddPatient(Pawn pawn) {
            return true;
        }

        public override bool OnRemovePatient(Pawn pawn) {
            return true;
        }
    }
}

