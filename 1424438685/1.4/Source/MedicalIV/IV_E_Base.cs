using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Verse;

namespace IV
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public abstract class IV_E_Base : IV_Base
    {
        protected IV_E_Base(string name, List<HediffDef> hediff) : 
            base(name, FuelType.Electricity, hediff, 0f) { }
    }
}