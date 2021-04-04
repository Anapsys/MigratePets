//using ExampleMod.NPCs;
//using ExampleMod.Tiles;
//using ExampleMod.UI;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.GameContent.Dyes;
using Terraria.GameContent.UI;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace MigratePets
{
	public class MigratePets : Mod
	{
		public MigratePets()
        {
			//constructor
        }
		public override void PostSetupContent()
		{
			// Showcases mod support with Boss Checklist without referencing the mod
			/*
			Mod bossChecklist = ModLoader.GetMod("BossChecklist");
			if (bossChecklist != null)
			{
				bossChecklist.Call(
					"AddBoss",
					10.5f,
					new List<int> { ModContent.NPCType<NPCs.Abomination.Abomination>(), ModContent.NPCType<NPCs.Abomination.CaptiveElement2>() },
					this, // Mod
					"$Mods.ExampleMod.NPCName.Abomination",
					(Func<bool>)(() => ExampleWorld.downedAbomination),
					ModContent.ItemType<Items.Abomination.FoulOrb>(),
					new List<int> { ModContent.ItemType<Items.Armor.AbominationMask>(), ModContent.ItemType<Items.Placeable.AbominationTrophy>() },
					new List<int> { ModContent.ItemType<Items.Abomination.SixColorShield>(), ModContent.ItemType<Items.Abomination.MoltenDrill>() },
					"$Mods.ExampleMod.BossSpawnInfo.Abomination"
				);
				bossChecklist.Call(
					"AddBoss",
					15.5f,
					ModContent.NPCType<PuritySpirit>(),
					this,
					"Purity Spirit",
					(Func<bool>)(() => ExampleWorld.downedPuritySpirit),
					ItemID.Bunny,
					new List<int> { ModContent.ItemType<Items.Armor.PuritySpiritMask>(), ModContent.ItemType<Items.Armor.BunnyMask>(), ModContent.ItemType<Items.Placeable.PuritySpiritTrophy>(), ModContent.ItemType<Items.Placeable.BunnyTrophy>(), ModContent.ItemType<Items.Placeable.TreeTrophy>() },
					new List<int> { ModContent.ItemType<Items.PurityShield>(), ItemID.Bunny },
					$"Kill a [i:{ItemID.Bunny}] in front of [i:{ModContent.ItemType<Items.Placeable.ElementalPurge>()}]"
				);
			}
			*/
		}
		public override void AddRecipeGroups()
		{
			// Creates a new recipe group
			RecipeGroup group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " " + Lang.GetItemNameValue(ItemType("ExampleItem")), new[]
			{
				ItemType("ExampleItem"),
				ItemType("EquipMaterial"),
				ItemType("BossItem")
			});
			// Registers the new recipe group with the specified name
			// RecipeGroup.RegisterGroup("ExampleMod:ExampleItem", group);

			// Modifying a vanilla recipe group. Now we can use Lava Snail to craft Snail Statue
			// RecipeGroup snailGroup = RecipeGroup.recipeGroups[RecipeGroup.recipeGroupIDs["Snails"]];
			// snailGroup.ValidItems.Add(ModContent.ItemType<NPCs.ExampleCritterItem>());

			// We also add ExampleSand to the Sand group, which is used in the Magic Sand Dropper recipe
			// RecipeGroup.recipeGroups[RecipeGroup.recipeGroupIDs["Sand"]].ValidItems.Add(ModContent.ItemType<Items.Placeable.ExampleSand>());
		}

		// Learn how to do Recipes: https://github.com/tModLoader/tModLoader/wiki/Basic-Recipes 
		public override void AddRecipes()
		{
			// Here is an example of a recipe.
			//ModRecipe recipe = new ModRecipe(this);
			//recipe.AddIngredient(ItemType("ExampleItem"));
			//recipe.SetResult(ItemID.Wood, 999);
			//recipe.AddRecipe();

			// To make ExampleMod more organized, the rest of the recipes are added elsewhere, see the method calls below.
			// See RecipeHelper.cs
			RecipeHelper.AddExampleRecipes(this);
			RecipeHelper.ExampleRecipeEditing(this);
		}
	}
}