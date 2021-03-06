using Terraria;
using Terraria.ModLoader;

namespace MigratePets.Buffs
{
	public class LoPetLargeBuff : ModBuff
	{
		public override void SetDefaults() 
		{
			// DisplayName and Description are automatically set from the .lang files, but below is how it is done normally.
			// DisplayName.SetDefault("Paper Airplane");
			// Description.SetDefault("\"Let this pet be an example to you!\"");
			Main.buffNoTimeDisplay[Type] = true;
			Main.vanityPet[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex) 
		{
			player.buffTime[buffIndex] = 18000;
			player.GetModPlayer<SimpleModPlayer>().LoPet = true;
			bool petProjectileNotSpawned = player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.Pets.LoPetLarge>()] <= 0;
			if (petProjectileNotSpawned && player.whoAmI == Main.myPlayer)
			{
				Projectile.NewProjectile(player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 0f, 0f, ModContent.ProjectileType<Projectiles.Pets.LoPetLarge>(), 0, 0f, player.whoAmI, 0f, 0f);
			}
		}
	}
}