using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;
using UnityEngine;

namespace aRandomKiwi.DFM
{
    [StaticConstructorOnStartup]
    static class Utils
    {
        public static string TranslateTicksToTextIRLSeconds(int ticks)
        {
            //Si moins d'une heure ingame alors affichage secondes
            if (ticks < 2500)
                return ticks.ToStringSecondsFromTicks();
            else
                return ticks.ToStringTicksToPeriodVerbose(true);
        }

        public static readonly Texture2D texDown = ContentFinder<Texture2D>.Get("UI/Icons/Down", true);
        public static readonly Texture2D texUndown = ContentFinder<Texture2D>.Get("UI/Icons/Undown", true);
    }
}
