using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MigratePets;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MigratePets.Projectiles.Pets
{
    public abstract class AbstractPet : ModProjectile
    {
		private const int range = 500;
		private readonly int rangeHypoteneus = (int)System.Math.Sqrt(range * range + range * range);
		public override void SetDefaults()
		{
			projectile.CloneDefaults(ProjectileID.BabyDino);
			aiType = 0;//ProjectileID.BabyDino;
			projectile.timeLeft = 2;
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
				player.ClearBuff(ModContent.BuffType<Buffs.QuailPetBuff>());
				modPlayer.quailPet = false;
			}
			if (player.HasBuff(ModContent.BuffType<Buffs.QuailPetBuff>()))
			{
				projectile.timeLeft = 2;
			}
			#endregion

			return true;
		}

		public override void AI()
		{
			#region AI... which should be all that's in this function. wtf.
			
			#endregion
		}

        public override void PostAI()
        {
			#region Animation and visuals
			// So it will lean slightly towards the direction it's moving
			projectile.rotation = projectile.velocity.X * 0.08f;

			// This is a simple "loop through all frames from top to bottom" animation
			int frameSpeed = 8;
			projectile.frameCounter++;
			if (projectile.frameCounter >= frameSpeed)
			{
				projectile.frameCounter = 0;
				projectile.frame++;
				if (projectile.frame >= Main.projFrames[projectile.type])
				{
					projectile.frame = 0;
				}
			}

			// Some visuals here
			Lighting.AddLight(projectile.Center, Color.White.ToVector3() * 0.78f);
			#endregion
		}
	}
}
