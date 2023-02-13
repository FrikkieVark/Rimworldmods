using System.Collections.Generic;
using Verse;

namespace IV.Traditional
{
    public class IVAnesthetic : IVBase
    {
        public IVAnesthetic() : base("Anesthetic", FuelType.RESOURCE, new List<HediffDef> { HediffDef.Named("IV_Anesthetic") }, 0.0075f) {
        }

        public override bool OnAddPatient(Pawn pawn) {
            return true;
        }

        public override bool OnRemovePatient(Pawn pawn) {
            return true;
        }
    }
}

