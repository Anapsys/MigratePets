using MigratePets;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MigratePets.Projectiles.Pets
{
	public class LoPet : ModProjectile
	{
		private const int range = 500;
		private readonly int rangeHypoteneus = (int)System.Math.Sqrt(range * range + range * range);

		public override void SetStaticDefaults() {
			//DisplayName.SetDefault("Paper Airplane"); // Automatic from .lang files
			Main.projFrames[projectile.type] = 4;
			Main.projPet[projectile.type] = true;
		}

		public override void SetDefaults() {
			projectile.CloneDefaults(ProjectileID.ZephyrFish);
			aiType = ProjectileID.ZephyrFish;
		}

		public override bool PreAI() {
			Player player = Main.player[projectile.owner];
			player.zephyrfish = false; // Relic from aiType
			return true;
		}

		public override void AI() {
			Player player = Main.player[projectile.owner];

			Vector2 idlePosition = player.Center;
			idlePosition.Y -= 48f;
			float minionPositionOffsetX = (10 + projectile.minionPos * 40) * -player.direction;
			idlePosition.X += minionPositionOffsetX;

			Vector2 vectorToIdlePosition = idlePosition - projectile.Center;
			float distanceToIdlePosition = vectorToIdlePosition.Length();

			//ExamplePlayer modPlayer = player.GetModPlayer<ExamplePlayer>();
			if (player.dead) {
				//player.examplePet = false;
			}

			if (Vector2.Distance(player.Center, projectile.Center) > rangeHypoteneus)
			{
				projectile.Center = new Vector2(Main.rand.Next((int)player.Center.X - range, (int)player.Center.X + range), Main.rand.Next((int)player.Center.Y - range, (int)player.Center.Y + range));
				projectile.ai[0] = 0;
				Vector2 vectorToPlayer = player.Center - projectile.Center;
				projectile.velocity += 2f * Vector2.Normalize(vectorToPlayer);
			}
		}
		public override bool? CanCutTiles()
		{
			return false;
		}

		public override bool MinionContactDamage()
		{
			return true;
		}
	}
}