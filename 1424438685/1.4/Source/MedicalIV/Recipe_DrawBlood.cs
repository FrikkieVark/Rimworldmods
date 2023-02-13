using System.Collections.Generic;
using RimWorld;
using Verse;

namespace IV
{
    internal class Recipe_DrawBlood : RecipeWorker
    {
        private readonly HediffDef _bloodLoss = HediffDef.Named("BloodLoss");
        private readonly ThingDef _bloodBag = ThingDef.Named("IV_BloodBag");

        public override void ApplyOnPawn(Pawn pawn, BodyPartRecord part, Pawn billDoer, List<Thing> ingredients, Bill bill) {
            // Get the pawn's location so we can spawn blood bags on them
            var loc = billDoer.Position;
            var map = billDoer.Map;

            // Add or set the severity of the BloodLoss hediff
            var appliedBloodLoss = pawn.health.hediffSet.GetFirstHediffOfDef(_bloodLoss) ?? pawn.health.AddHediff(_bloodLoss);
            appliedBloodLoss.Severity = 0.15f;
            GenSpawn.Spawn(_bloodBag, loc, map);
        }
    }
}
