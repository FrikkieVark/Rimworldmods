using System;
using System.Reflection;
using HarmonyLib;
using Verse;

namespace aRandomKiwi.DFM
{
    [StaticConstructorOnStartup]
    public static class HarmonyPatches
    {
        static HarmonyPatches()
        {
            var inst = new Harmony("rimworld.randomKiwi.CMP");
            inst.PatchAll(Assembly.GetExecutingAssembly());
        }
    }
}
