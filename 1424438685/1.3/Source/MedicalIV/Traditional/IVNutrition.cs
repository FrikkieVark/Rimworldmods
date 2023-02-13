using System.Collections.Generic;
using Verse;

namespace IV.Traditional
{
    public class IVNutrition : IVBase
    {
        public IVNutrition() : base("Nutrition", FuelType.RESOURCE, new List<HediffDef> { HediffDef.Named("IV_Nutrition") }, 0.1f) {
        }

        public override bool OnAddPatient(Pawn pawn) {
            return true;
        }

        public override bool OnRemovePatient(Pawn pawn) {
            return true;
        }
    }
}

