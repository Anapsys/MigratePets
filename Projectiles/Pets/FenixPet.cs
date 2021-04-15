using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MigratePets;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MigratePets.Projectiles.Pets
{
	public class FenixPet : AbstractPet
	{
		public override void SetStaticDefaults()
		{
			Main.projFrames[projectile.type] = 11; //this MUST MATCH the hardcoded number of frames in whatever AI you're copying. >_>
			Main.projPet[projectile.type] = true;
		}
		public override void SetDefaults()
		{
			projectile.CloneDefaults(ProjectileID.Puppy);
			//projectile.tileCollide = true;
			aiType = ProjectileID.Puppy;
			projectile.timeLeft = 4;
			projectile.scale = 2;
			drawOriginOffsetY -= 3;
			projectile.width = 14;
			projectile.height = 15;
		}
		public override bool PreAI()
		{
			Player player = Main.player[projectile.owner];
			player.zephyrfish = false; // Relic from aiType

			#region Active check
			// This is the "active check", makes sure the minion is alive while the player is alive, and despawns if not
			SimpleModPlayer modPlayer = player.GetModPlayer<SimpleModPlayer>();

			if (player.dead || !player.active)
			{
				player.ClearBuff(ModContent.BuffType<Buffs.FenixPetBuff>());
				modPlayer.FenixPet = false;
			}
			if (player.HasBuff(ModContent.BuffType<Buffs.FenixPetBuff>()))
			{
				projectile.timeLeft = 2;
			}
			#endregion
			return true;
		}
	}
}