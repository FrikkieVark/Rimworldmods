using System.Collections.Generic;
using Verse;

namespace IV.Traditional
{
    public class IV_Anesthetic : IV_Base
    {
        public IV_Anesthetic() : base("Anesthetic", FuelType.Resource, new List<HediffDef> { HediffDef.Named("IV_Anesthetic_Hediff") }, 0.0075f) {
        }

        public override bool OnAddPatient(Pawn pawn) {
            return true;
        }

        public override bool OnRemovePatient(Pawn pawn) {
            return true;
        }
    }
}

