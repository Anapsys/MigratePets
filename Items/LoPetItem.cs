//using MigratePets.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace MigratePets.Items
{
	public class LoPetItem : AbstractPetItem
	{
		public override void SetDefaults()
		{
			item.CloneDefaults(ItemID.AmberMosquito);
			item.damage = 0;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.shoot = ModContent.ProjectileType<Projectiles.Pets.LoPet>();
			item.buffType = ModContent.BuffType<Buffs.LoPetBuff>();
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.SoulofFlight, 7);
			recipe.AddIngredient(ItemID.SoulofLight, 7);
			recipe.AddIngredient(ItemID.Feather, 5);
			recipe.AddIngredient(ItemID.GiantHarpyFeather, 1);
			recipe.AddIngredient(ItemID.AngelWings, 1);
			//recipe.AddTile(ModContent.TileType<ExampleWorkbench>());
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}