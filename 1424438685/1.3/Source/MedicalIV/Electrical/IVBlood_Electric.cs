using System.Collections.Generic;
using Verse;

namespace IV.Electrical
{
    public class IVBlood_Electric : IVBase
    {
        public IVBlood_Electric() : base("Electrical Blood", FuelType.ELECTRICITY, new List<HediffDef> { HediffDef.Named("IV_Blood") }, 0f) {
        }

        public override bool OnAddPatient(Pawn pawn) {
            return true;
        }

        public override bool OnRemovePatient(Pawn pawn) {
            return true;
        }
    }
}

