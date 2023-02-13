using System.Collections.Generic;
using Verse;

namespace IV.Traditional
{
    public class IV_Nutrition : IV_Base
    {
        public IV_Nutrition() : base("Nutrition", FuelType.Resource, new List<HediffDef> { HediffDef.Named("IV_Nutrition_Hediff") }, 0.1f) {
        }

        public override bool OnAddPatient(Pawn pawn) {
            return true;
        }

        public override bool OnRemovePatient(Pawn pawn) {
            return true;
        }
    }
}

