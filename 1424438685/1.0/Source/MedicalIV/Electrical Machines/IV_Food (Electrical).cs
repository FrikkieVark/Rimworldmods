using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;
using Verse.AI;
using Verse.Sound;
using Harmony;
using Harmony.ILCopying;
using IV;
using System.Collections;
using System.Reflection;

namespace IV
{
    public class Electrical_FoodMachine : Building
    {

        public static HediffDef IV_Food = HediffDef.Named("IV_Food");
        private CompPowerTrader powerComp = null;
        private Dictionary<Pawn, float> HungerDict = new Dictionary<Pawn, float>();
        private List<Pawn> ActivePawns = new List<Pawn>();

        public Electrical_FoodMachine()
            : base()
        {
            Log.Message("[Medical IV's] Initialised Nutrition Machine");
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
                    foreach (Pawn pawn in ActivePawns.ToList())
                    {
                        GetHungerValue(pawn);
                    }
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
                            // Save initial hunger value and add them to the list of active pawns
                            SaveHungerValue(pawn);
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
                    pawn.health.AddHediff(IV_Food);
                    pawn.needs.food.CurLevelPercentage = 1f;
                    continue;
                }
                else
                {
                    // When pawn exits bed, remove them from dictionary and re-apply their original hunger value
                    float lastValue = GetHungerValue(pawn);
                    pawn.needs.food.CurLevelPercentage = lastValue;
                    continue;
                }
            }
        }

        public void SaveHungerValue(Pawn pawn)
        {
            float curFood = pawn.needs.food.CurLevelPercentage;
            HungerDict.Add(pawn, curFood);
            ActivePawns.Add(pawn);
        }

        public float GetHungerValue(Pawn pawn)
        {
            float lastHunger = HungerDict[pawn];
            HungerDict.Remove(pawn);
            ActivePawns.Remove(pawn);
            return lastHunger;
        }
    }
}

