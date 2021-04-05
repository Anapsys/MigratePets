//using MigratePets.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MigratePets.Items
{
    public abstract class AbstractPetItem : ModItem
    {
		public override void SetStaticDefaults()
		{
			// DisplayName and Tooltip are automatically set from the .lang files, but below is how it is done normally.
			// DisplayName.SetDefault("Paper Airplane");
			// Tooltip.SetDefault("Summons a Paper Airplane to follow aimlessly behind you");
		}

		public override void SetDefaults()
		{
			//override me
			item.CloneDefaults(ItemID.AmberMosquito);
			item.damage = 0;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.shoot = ModContent.ProjectileType<Projectiles.Pets.QuailPet>();
			item.buffType = ModContent.BuffType<Buffs.QuailPetBuff>();
		}

		public override void AddRecipes()
		{
			//override me
		}

		public override void UseStyle(Player player)
		{
			if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
			{
				player.AddBuff(item.buffType, 3600, true);
			}
		}
	}
}
