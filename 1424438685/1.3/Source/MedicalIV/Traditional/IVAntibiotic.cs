using System.Collections.Generic;
using Verse;

namespace IV.Traditional
{
    public class IVAntibiotic : IVBase
    {
        public IVAntibiotic() : base("Antibiotic", FuelType.RESOURCE, new List<HediffDef> { HediffDef.Named("IV_Antibiotics") }, 0.0075f) {
        }

        public override bool OnAddPatient(Pawn pawn) {
            return true;
        }

        public override bool OnRemovePatient(Pawn pawn) {
            return true;
        }
    }
}

