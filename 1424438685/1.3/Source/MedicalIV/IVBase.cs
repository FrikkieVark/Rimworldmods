using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;

namespace IV
{
    public abstract class IVBase : Building
    {
        protected readonly string name;
        protected readonly FuelType fuelType;
        protected readonly float fuelConsumption;
        protected readonly List<HediffDef> IVHediffs;

        protected CompRefuelable refuelComp;
        protected CompPowerTrader powerComp;
        protected CompFlickable flickableComp;
        protected List<Pawn> ActivePatients = new List<Pawn>();

        protected float basePower;
        protected float idlePower;

        protected IVBase(string name, FuelType fuelType, List<HediffDef> hediff, float fuelConsumption) : base() {
            this.name = name;
            this.fuelType = fuelType;
            this.IVHediffs = hediff;
            this.fuelConsumption = fuelConsumption;
            Log.Message("[Medical IVs] Initialised " + this.name + " IV");
        }

        /// <summary>
        /// Not intended to be called; an override of Building.SpawnSetup()
        /// </summary>
        public override void SpawnSetup(Map map, bool respawningAfterLoad) {
            base.SpawnSetup(map, respawningAfterLoad);
            if (this.fuelType == FuelType.RESOURCE) {
                this.refuelComp = base.GetComp<CompRefuelable>();
                this.flickableComp = base.GetComp<CompFlickable>();
            }
            else {
                this.powerComp = base.GetComp<CompPowerTrader>();
                this.basePower = this.powerComp.Props.basePowerConsumption;
                this.idlePower = basePower / 2;
            }
        }

        /// <summary>
        /// Not intended to be called; an override of Building.Tick()
        /// * Runs IV code every 80-ticks 
        /// </summary>
        public override void Tick() {
            if ((Find.TickManager.TicksGame + thingIDNumber.HashOffset()) % 80 == 0) {
                base.Tick();
                if (this.IsReady) {
                    if (this.ActivePatients.ToList().Count > 0) {                                                                   // .ToList() used to prevent "Collection Modified" error
                        ApplyIV();
                    }
                    UpdateActivePawns();
                    UpdatePowerMode();
                }
                else {
                    this.ActivePatients.Clear();
                }
            }
        }

        /// <summary>
        /// Update list of adjacent pawns that will be affected by the IV
        /// </summary>
        private void UpdateActivePawns() {
            var adjacent = GenAdj.CardinalDirectionsAround;
            var position = this.Position;
            for (int i = 0; i < adjacent.Length; i++) {
                var things = this.Map.thingGrid.ThingsListAt(adjacent[i] + position);
                foreach (Thing thing in things) {
                    if (thing is Pawn) {
                        Pawn pawn = thing as Pawn;
                        if (this.ActivePatients.Contains(pawn)) {
                            continue;
                        }
                        if ((pawn.RaceProps.Humanlike || pawn.RaceProps.Animal) && pawn.InBed()) {
                            if (this.OnAddPatient(pawn)) {
                                this.ActivePatients.Add(pawn);
                                ApplyIV();
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Handle application of IV hediff and removal of pawn from list once out of bed
        /// </summary>
        private void ApplyIV() {
            foreach (Pawn pawn in this.ActivePatients.ToList()) {                                                                   // .ToList() used to prevent "Collection Modified" error
                if (pawn.InBed()) {
                    this.IVHediffs.ForEach(hediff => pawn.health.AddHediff(hediff));
                    if (this.fuelType == FuelType.RESOURCE) {
                        refuelComp.ConsumeFuel(this.fuelConsumption);
                    }
                    continue;
                }
                else {
                    if (this.OnRemovePatient(pawn)) {
                        this.ActivePatients.Remove(pawn);
                    }
                    continue;
                }
            }
        }

        /// <summary>
        /// Determine readiness of IV by power/fuel/switch status
        /// </summary>
        private bool IsReady {
            get {
                if (this.fuelType == FuelType.RESOURCE) {
                    return (this.refuelComp != null && this.refuelComp.HasFuel) && (this.flickableComp != null && this.flickableComp.SwitchIsOn);
                }
                else {
                    return this.powerComp != null && this.powerComp.PowerOn;
                }
            }
        }

        /// <summary>
        /// Determines electrical power consumption
        /// </summary>
        private void UpdatePowerMode() {
            if (this.fuelType != FuelType.ELECTRICITY) {
                return;
            }
            if (this.ActivePatients.ToList().Count == 0) {
                this.powerComp.PowerOutput = -this.idlePower;
            }
            else {
                this.powerComp.PowerOutput = -this.basePower;
            }
        }

        /// <summary>
        /// Instructions to run after pawn is SET as an active patient
        /// but before the initial IV application
        /// </summary>
        /// <param name="pawn">Patient</param>
        /// <returns>boolean: whether to continue SETTING patient</returns>
        public abstract bool OnAddPatient(Pawn pawn);

        /// <summary>
        /// Instructions to run after pawn is UNSET as an active patient
        /// </summary>
        /// <param name="pawn">Patient</param>
        /// <returns>boolean: whether to continue UNSETTING patient</returns>
        public abstract bool OnRemovePatient(Pawn pawn);
    }

    public enum FuelType
    {
        RESOURCE,
        ELECTRICITY
    }
}

