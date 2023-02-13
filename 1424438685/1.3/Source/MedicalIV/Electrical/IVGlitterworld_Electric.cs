using System.Collections.Generic;
using Verse;

namespace IV.Electrical
{
    public class IVGlitterworld_Electric : IVBase
    {
        public IVGlitterworld_Electric() : base("Glitterworld", FuelType.ELECTRICITY, 
               new List<HediffDef> { HediffDef.Named("IV_Blood"), HediffDef.Named("IV_Painkiller"), HediffDef.Named("IV_Antibiotics") }, 0f) {
        }

        public override bool OnAddPatient(Pawn pawn) {
            return true;
        }

        public override bool OnRemovePatient(Pawn pawn) {
            return true;
        }
    }
}

