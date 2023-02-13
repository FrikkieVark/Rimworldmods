using System;
using System.Reflection;
using Harmony;
using Verse;

namespace aRandomKiwi.DFM
{
    [StaticConstructorOnStartup]
    public static class HarmonyPatches
    {
        static HarmonyPatches()
        {
            HarmonyInstance.Create("rimworld.randomKiwi.CMP").PatchAll(Assembly.GetExecutingAssembly());
        }
    }
}
