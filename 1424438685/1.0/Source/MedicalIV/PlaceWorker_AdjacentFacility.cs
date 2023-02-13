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
using UnityEngine;

namespace IV
{
    public class PlaceWorker_AdjacentFacility : PlaceWorker
    {
        public override void DrawGhost(ThingDef def, IntVec3 center, Rot4 rot, Color ghostCol)
        {
            GenDraw.DrawFieldEdges(GenAdj.CellsAdjacentCardinal(center, rot, def.size).ToList<IntVec3>(), Color.white);
        }
    }
}
