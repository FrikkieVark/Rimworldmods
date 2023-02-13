using System.Collections.Generic;
using Verse;

namespace IV.Electrical
{
    public class IVAnesthetic_Electric : IVBase
    {
        public IVAnesthetic_Electric() : base("Electrical Anesthetic", FuelType.ELECTRICITY, new List<HediffDef> { HediffDef.Named("IV_Anesthetic") }, 0f) {
        }

        public override bool OnAddPatient(Pawn pawn) {
            return true;
        }

        public override bool OnRemovePatient(Pawn pawn) {
            return true;
        }
    }
}

