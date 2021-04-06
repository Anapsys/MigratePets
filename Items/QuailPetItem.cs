//using MigratePets.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace MigratePets.Items
{
	public class QuailPetItem : AbstractPetItem
	{
		public override void SetDefaults()
		{
			item.CloneDefaults(ItemID.AmberMosquito);
			item.damage = 0;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.shoot = ModContent.ProjectileType<Projectiles.Pets.QuailPet>();
			item.buffType = ModContent.BuffType<Buffs.QuailPetBuff>();
		}

		public override void AddRecipes() {
			//ModRecipe recipe = new ModRecipe(mod);
			//recipe.AddIngredient(ModContent.ItemType<ExampleItem>(), 10);
			//recipe.AddTile(ModContent.TileType<ExampleWorkbench>());
			//recipe.SetResult(this);
			//recipe.AddRecipe();
		}
	}
}