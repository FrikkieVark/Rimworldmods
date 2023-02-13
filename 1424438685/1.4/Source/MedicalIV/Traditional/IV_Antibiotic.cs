using System.Collections.Generic;
using Verse;

namespace IV.Traditional
{
    public class IV_Antibiotic : IV_Base
    {
        public IV_Antibiotic() : base("Antibiotic", FuelType.Resource, new List<HediffDef> { HediffDef.Named("IV_Antibiotics_Hediff") }, 0.0075f) {
        }

        public override bool OnAddPatient(Pawn pawn) {
            return true;
        }

        public override bool OnRemovePatient(Pawn pawn) {
            return true;
        }
    }
}

