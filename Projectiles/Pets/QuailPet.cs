using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MigratePets;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MigratePets.Projectiles.Pets
{
	public class QuailPet : ModProjectile
	{
		private const int range = 500;
		private readonly int rangeHypoteneus = (int)System.Math.Sqrt(range * range + range * range);

		public override void SetStaticDefaults() {
			//DisplayName.SetDefault("Paper Airplane"); // Automatic from .lang files
			Main.projFrames[projectile.type] = 6;
			Main.projPet[projectile.type] = true;
		}

		public override void SetDefaults() {
			projectile.CloneDefaults(ProjectileID.BabyDino);
			aiType = 0;// ProjectileID.BabyDino;
			projectile.timeLeft = 2;
		}
		public override bool PreAI()
		{
			Player player = Main.player[projectile.owner];
			player.zephyrfish = false; // Relic from aiType
			return true;
		}
		
		public override void AI() {
			Player player = Main.player[projectile.owner];

			#region Active check
			// This is the "active check", makes sure the minion is alive while the player is alive, and despawns if not
			if (player.dead || !player.active)
			{
				player.ClearBuff(ModContent.BuffType<Buffs.QuailPetBuff>());
			}
			if (player.HasBuff(ModContent.BuffType<Buffs.QuailPetBuff>()))
			{
				projectile.timeLeft = 2;
			}
			#endregion

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

		public virtual string Texture => (base.GetType().Namespace + "." + this.Name).Replace('.', '/');
		public virtual string Texture2 => (base.GetType().Namespace + "." + this.Name+"Walk").Replace('.', '/');
		public virtual string Texture3 => (base.GetType().Namespace + "." + this.Name+"Fly").Replace('.', '/');
		public virtual string Texture4 => (base.GetType().Namespace + "." + this.Name+"Fall").Replace('.', '/');

		public override bool? CanCutTiles()
		{
			return false;
		}

		public override bool MinionContactDamage()
		{
			return true;
		}

		// Some advanced drawing because the texture image isn't centered or symetrical.
		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			SpriteEffects spriteEffects = SpriteEffects.None;
			if (projectile.spriteDirection == -1)
			{
				spriteEffects = SpriteEffects.FlipHorizontally;
			}
			Texture2D anim1 = ModContent.GetTexture(this.Texture2); //walk
			Texture2D anim2 = ModContent.GetTexture(this.Texture3); //fly
			Texture2D anim3 = ModContent.GetTexture(this.Texture4); //fall

			Texture2D texture = Main.projectileTexture[projectile.type];
			if ( System.Math.Abs(projectile.velocity.X) > 0) texture = anim1;
			if ( projectile.velocity.Y >= 0.04f ) texture = anim3;
			if ( projectile.velocity.Y < 0f ) texture = anim2;


			int frameHeight = Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type];
			int startY = frameHeight * projectile.frame;
			Rectangle sourceRectangle = new Rectangle(0, startY, texture.Width, frameHeight);
			Vector2 origin = sourceRectangle.Size() / 2f;
			origin.X = (float)(projectile.spriteDirection == 1 ? sourceRectangle.Width - 20 : 20);

			Color drawColor = projectile.GetAlpha(lightColor);
			Main.spriteBatch.Draw(texture,
				projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY),
				sourceRectangle, 
				drawColor, 
				projectile.rotation,
				origin, 
				projectile.scale, 
				spriteEffects, 
				0f);

			return false;
		}
	}
}