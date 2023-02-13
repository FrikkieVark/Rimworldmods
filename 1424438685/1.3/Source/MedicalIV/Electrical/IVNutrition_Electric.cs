using System.Collections.Generic;
using Verse;

namespace IV.Electrical
{
    public class IVNutrition_Electric : IVBase
    {
        public IVNutrition_Electric() : base("Electrical Nutrition", FuelType.ELECTRICITY, new List<HediffDef> { HediffDef.Named("IV_Nutrition") }, 0f) {
        }

        public override bool OnAddPatient(Pawn pawn) {
            return true;
        }

        public override bool OnRemovePatient(Pawn pawn) {
            return true;
        }
    }
}

