using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;

namespace IV
{
    public abstract class IV_Base : Building
    {
        private readonly FuelType _fuelType;
        private readonly float _fuelConsumption;
        private readonly List<HediffDef> _ivHediffs;
        private readonly List<Pawn> _activePatients = new List<Pawn>();

        private CompRefuelable _refuelComp;
        private CompPowerTrader _powerComp;
        private CompFlickable _flickableComp;

        private float _basePower;
        private float _idlePower;
        private TickManager _tickManager;

        protected IV_Base(string name, FuelType fuelType, List<HediffDef> hediff, float fuelConsumption) {
            _fuelType = fuelType;
            _ivHediffs = hediff;
            _fuelConsumption = fuelConsumption;
            Log.Message("[Medical IVs] Initialised " + name + " IV");
        }

        /// <summary>
        /// Not intended to be called; an override of Building.SpawnSetup()
        /// </summary>
        public override void SpawnSetup(Map map, bool respawningAfterLoad) {
            base.SpawnSetup(map, respawningAfterLoad);
            _tickManager = Find.TickManager;
            if (_fuelType == FuelType.Resource) {
                _refuelComp = GetComp<CompRefuelable>();
                _flickableComp = GetComp<CompFlickable>();
            }
            else {
                _powerComp = GetComp<CompPowerTrader>();
                _basePower = _powerComp.Props.PowerConsumption;
                _idlePower = _basePower / 2;
            }
        }

        /// <summary>
        /// Not intended to be called; an override of Building.Tick()
        /// * Runs IV code every 80-ticks 
        /// </summary>
        public override void Tick() {
            if ((_tickManager.TicksGame + thingIDNumber.HashOffset()) % 80 != 0) return;
            base.Tick();
            if (IsReady) {
                if (_activePatients.ToList().Count > 0) {                                                               // .ToList() used to prevent "Collection Modified" error
                    ApplyIv();
                }
                UpdateActivePawns();
                UpdatePowerMode();
            }
            else {
                _activePatients.Clear();
            }
        }

        /// <summary>
        /// Update list of adjacent pawns that will be affected by the IV
        /// </summary>
        private void UpdateActivePawns() {
            var adjacent = GenAdj.CardinalDirectionsAround;
            var position = Position;
            foreach (var tile in adjacent) {
                var things = Map.thingGrid.ThingsListAt(tile + position);
                foreach (var pawn in things.OfType<Pawn>()) {
                    if (_activePatients.Contains(pawn)) continue;
                    if (!pawn.RaceProps.Humanlike && !pawn.RaceProps.Animal || !pawn.InBed()) continue;
                    if (!OnAddPatient(pawn)) continue;
                    
                    _activePatients.Add(pawn);
                    ApplyIv();
                }
            }
        }

        /// <summary>
        /// Handle application of IV hediff and removal of pawn from list once out of bed
        /// </summary>
        private void ApplyIv() {
            foreach (var pawn in _activePatients.ToList()) {                                                            // .ToList() used to prevent "Collection Modified" error
                if (pawn.InBed()) {
                    _ivHediffs.ForEach(hediff => pawn.health.AddHediff(hediff));
                    if (_fuelType == FuelType.Resource) {
                        _refuelComp.ConsumeFuel(_fuelConsumption);
                    }
                }
                else {
                    if (OnRemovePatient(pawn)) {
                        _activePatients.Remove(pawn);
                    }
                }
            }
        }

        /// <summary>
        /// Determine readiness of IV by power/fuel/switch status
        /// </summary>
        private bool IsReady =>
            _fuelType == FuelType.Resource 
                ? _refuelComp != null && _refuelComp.HasFuel && _flickableComp != null && _flickableComp.SwitchIsOn
                : _powerComp != null && _powerComp.PowerOn;

        /// <summary>
        /// Determines electrical power consumption
        /// </summary>
        private void UpdatePowerMode() {
            if (_fuelType != FuelType.Electricity) {
                return;
            }
            if (_activePatients.ToList().Count == 0) {
                _powerComp.PowerOutput = -_idlePower;
            }
            else {
                _powerComp.PowerOutput = -_basePower;
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

    public enum FuelType {
        Resource,
        Electricity
    }
}

