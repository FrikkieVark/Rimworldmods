using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;

namespace IV
{
    internal class Recipe_DrawBlood : RecipeWorker
    {
        public HediffDef BloodLoss = HediffDef.Named("BloodLoss");
        public ThingDef BloodBag = ThingDef.Named("IV_BloodBag");

        public override void ApplyOnPawn(Pawn pawn, BodyPartRecord part, Pawn billDoer, List<Thing> ingredients, Bill bill)
        {
            // Get the pawn's location so we can spawn blood bags on them
            IntVec3 loc = billDoer.Position;
            Map map = billDoer.Map;

            // Return the BloodLoss hediffdef as a hediff so we can then set the severity
            List<Hediff> Hediffs = pawn.health.hediffSet.GetHediffs<Hediff>().ToList();

            foreach (Hediff hediff in Hediffs)
            {
                var StrHediff = hediff.ToString();
                if (StrHediff.Contains("BloodLoss"))
                {
                    hediff.Severity += 0.15f;
                    GenSpawn.Spawn(BloodBag, loc, map);
                    return;
                }
            }
            Hediff appliedBloodLoss = pawn.health.AddHediff(BloodLoss);
            appliedBloodLoss.Severity = 0.15f;
            GenSpawn.Spawn(BloodBag, loc, map);
        }
    }
}
