using System.Collections.Generic;
using Verse;

namespace IV.Electrical
{
    public class IVAntibiotic_Electric : IVBase
    {
        public IVAntibiotic_Electric() : base("Electrical Antibiotic", FuelType.ELECTRICITY, new List<HediffDef> { HediffDef.Named("IV_Antibiotics") }, 0f) {
        }

        public override bool OnAddPatient(Pawn pawn) {
            return true;
        }

        public override bool OnRemovePatient(Pawn pawn) {
            return true;
        }
    }
}

