using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using RimWorld;
using Verse;

namespace anglewyrm {

	[StaticConstructorOnStartup]
	internal static class Mining_Initializer 
	{
		static Mining_Initializer() {
			LongEventHandler.QueueLongEvent(Setup, "LibraryStartup", false, null);
		}
		
		/* Look for mineable resources and add them to the Mine */
		public static void Setup() {
			
			// localized title-case capitalization class
			CultureInfo cultureInfo   = Thread.CurrentThread.CurrentCulture;
			TextInfo textInfo = cultureInfo.TextInfo;  // usage: textInfo.ToTitleCase(string)

			List<RecipeDef> RecipeDefs = DefDatabase<RecipeDef>.AllDefsListForReading;
			List<ThingDef>  ThingDefs  = DefDatabase<ThingDef>.AllDefsListForReading;
			
			// scan all the things
			for (int someThing = 0; someThing < ThingDefs.Count; someThing++) {
				
				// select things with deep commonality
				if (ThingDefs[someThing].deepCommonality > 0){
					
					// Create recipe
					RecipeDef recipe = new RecipeDef();
					
					recipe.defName = "Excavate".Translate(ThingDefs[someThing].defName);
					
					Log.Message("LogMessageFoundResource".Translate(ThingDefs[someThing].label)
					            + "LogMessageAddedRecipe".Translate(recipe.defName));
					
					recipe.label       = "Mine".Translate(ThingDefs[someThing].label, " ");
					recipe.description = "Mine".Translate(ThingDefs[someThing].label, ".");
					recipe.jobString   = "Mining".Translate(ThingDefs[someThing].label);
					
					recipe.effectWorking = EffecterDef.Named("Smith");
					recipe.efficiencyStat = StatDefOf.MiningSpeed;
					recipe.workAmount = 332 * ThingDefs[someThing].GetStatValueAbstract(StatDefOf.MarketValue, null);
					recipe.workSkill = SkillDefOf.Mining;
					recipe.workSkillLearnFactor = 0.25f;
					recipe.products.Add(new ThingCountClass(ThingDefs[someThing], 1));
					recipe.fixedIngredientFilter = new ThingFilter();				
					recipe.defaultIngredientFilter = new ThingFilter();

					// add bill to mineshaft
					recipe.recipeUsers = new List<ThingDef>();
					recipe.recipeUsers.Add(ThingDef.Named("Mineshaft"));

					RecipeDefs.Add(recipe);
				}// has deepCommonality
			}// loop through things
		}// Setup
	}// Mining_Initializer

}// anglewyrm
