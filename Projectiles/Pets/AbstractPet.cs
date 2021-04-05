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
		protected const float gravity = 3.0f;
		protected const float maxGravity = 10.0f;
		public override void SetStaticDefaults()
		{
			Main.projFrames[projectile.type] = 6; //this MUST MATCH the hardcoded number of frames in whatever AI you're copying. >_>
			Main.projPet[projectile.type] = true;
		}
		public override void SetDefaults()
		{
			projectile.CloneDefaults(ProjectileID.BabyDino);
			//projectile.tileCollide = true;
			aiType = ProjectileID.BlackCat; //26;
			projectile.timeLeft = 2;
		}
		public override bool PreAI() //amazingly, this whole thing must be overridden in each pet.
		{
			Player player = Main.player[projectile.owner];
			player.blackCat = false; // Relic from aiType

			#region Active check
			// This is the "active check", makes sure the minion is alive while the player is alive, and despawns if not
			SimpleModPlayer modPlayer = player.GetModPlayer<SimpleModPlayer>();

			if (player.dead || !player.active)
			{
				player.ClearBuff(ModContent.BuffType<Buffs.QuailPetBuff>());
				modPlayer.quailPet = false;
				projectile.active = false;
			}
			if (player.HasBuff(ModContent.BuffType<Buffs.QuailPetBuff>()))
			{
				projectile.timeLeft = 2;
			}
			#endregion

			return true;
		}
	}
}
