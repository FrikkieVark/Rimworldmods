using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;

namespace IV
{

    public class Electrical_BloodMachine : Building
    {

        public static HediffDef IV_BloodTransfusion = HediffDef.Named("IV_BloodTransfusion");
        private CompPowerTrader powerComp = null;
        private List<Pawn> ActivePawns = new List<Pawn>();

        public Electrical_BloodMachine()
            : base()
        {
            Log.Message("[Medical IV's] Initialised Blood Machine");
        }

        // Make sure it has power before doing stuff
        public override void SpawnSetup(Map map, bool respawningAfterLoad)
        {
            base.SpawnSetup(map, respawningAfterLoad);
            powerComp = base.GetComp<CompPowerTrader>();
        }

        // Repeat code every Tick (might change it to TickRare at some point)
        public override void Tick() 
        {

            if ((Find.TickManager.TicksGame + thingIDNumber.HashOffset()) % 80 == 0)
            {
                base.Tick();

                if (powerComp.PowerOn)
                {
                    // .ToList() used to prevent "Collection Modified" error
                    if (ActivePawns.ToList().Count > 0)
                    {
                        ManageActivePawns();
                    }
                    ApplyIV();
                }
                else
                {
                    ActivePawns.Clear();
                }
            }
        }

        // Apply Hediff to every Pawn in adjacent cells once Tick() has been called
        public void ApplyIV()
        {
            var adjacent = GenAdj.CardinalDirectionsAround;
            var position = this.Position;
            for (int i = 0; i < adjacent.Length; i++)
            {
                var things = this.Map.thingGrid.ThingsListAt(adjacent[i] + position);
                foreach (Thing thing in things)
                {
                    if (thing is Pawn)
                    {
                        Pawn pawn = thing as Pawn;
                        if (ActivePawns.Contains(pawn))
                        {
                            continue;
                        }

                        if (pawn.RaceProps.Humanlike && pawn.InBed() || pawn.RaceProps.Animal && pawn.InBed())
                        {
                            // Add them to the list of active pawns
                            ActivePawns.Add(pawn);
                            ManageActivePawns();
                        }
                        
                    }

                }
            }
        }

        public void ManageActivePawns()
        {
            // .ToList() used to prevent "Collection Modified" error
            foreach (Pawn pawn in ActivePawns.ToList())
            {
                // While the pawn is bed-ridden, keep applying IV / hunger buff
                if (pawn.InBed())
                {
                    pawn.health.AddHediff(IV_BloodTransfusion);
                    continue;
                }
                else
                {
                    // When pawn exits bed, remove them from list of active pawns
                    ActivePawns.Remove(pawn);
                    continue;
                }
            }
        }

    }
}

