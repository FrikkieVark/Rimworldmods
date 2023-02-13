using System.Collections.Generic;
using Verse;

namespace IV.Traditional
{
    public class IVBlood : IVBase
    {
        public IVBlood() : base("Blood", FuelType.RESOURCE, new List<HediffDef> { HediffDef.Named("IV_Blood") }, 0.0075f) {
        }

        public override bool OnAddPatient(Pawn pawn) {
            return true;
        }

        public override bool OnRemovePatient(Pawn pawn) {
            return true;
        }
    }
}

