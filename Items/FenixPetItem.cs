//using MigratePets.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace MigratePets.Items
{
	public class FenixPetItem : AbstractPetItem
	{
		public override void SetDefaults()
		{
			item.CloneDefaults(ItemID.AmberMosquito);
			item.damage = 0;
			item.useStyle = ItemUseStyleID.SwingThrow;
			//item.UseSound = new LegacySoundStyle(SoundID.Bird, 1 );
			item.shoot = ModContent.ProjectileType<Projectiles.Pets.FenixPet>();
			item.buffType = ModContent.BuffType<Buffs.FenixPetBuff>();
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.SoulofFlight, 7);
			recipe.AddIngredient(ItemID.SoulofNight, 7);
			recipe.AddIngredient(ItemID.Feather, 5);
			recipe.AddIngredient(ItemID.FireFeather, 1);
			recipe.AddIngredient(ItemID.FlameWings, 1);
			//recipe.AddTile(ModContent.TileType<ExampleWorkbench>());
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}