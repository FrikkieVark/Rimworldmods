using System.Collections.Generic;
using Verse;

namespace IV.Traditional
{
    public class IV_Blood : IV_Base
    {
        public IV_Blood() : base("Blood", FuelType.Resource, new List<HediffDef> { HediffDef.Named("IV_Blood_Hediff") }, 0.0075f) {
        }

        public override bool OnAddPatient(Pawn pawn) {
            return true;
        }

        public override bool OnRemovePatient(Pawn pawn) {
            return true;
        }
    }
}

