using System.Linq;
using Verse;
using UnityEngine;

namespace IV
{
    public class PlaceWorker_AdjacentFacility : PlaceWorker
    {
        public override void DrawGhost(ThingDef def, IntVec3 center, Rot4 rot, Color ghostCol, Thing thing)
        {
            GenDraw.DrawFieldEdges(GenAdj.CellsAdjacentCardinal(center, rot, def.size).ToList<IntVec3>(), Color.white);
        }
    }
}
