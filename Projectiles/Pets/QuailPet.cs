using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MigratePets;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MigratePets.Projectiles.Pets
{
	public class QuailPet : AdvancedPet
	{
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

		// Some advanced drawing because the texture image isn't centered or symetrical.
		/*
		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			SpriteEffects spriteEffects = SpriteEffects.None;
			if (projectile.spriteDirection == -1)
			{
				spriteEffects = SpriteEffects.FlipHorizontally;
			}
			Texture2D anim1 = ModContent.GetTexture(this.TexturePathWalk); //walk
			Texture2D anim2 = ModContent.GetTexture(this.TexturePathFly); //fly
			Texture2D anim3 = ModContent.GetTexture(this.TexturePathFall); //fall
			Texture2D texture = Main.projectileTexture[projectile.type]; //idle

			if (System.Math.Abs(projectile.velocity.X) > 0)
			{
				texture = anim1;
			}
			if (projectile.velocity.Y >= 0.04f)
			{
				texture = anim3;
			}
			if (projectile.velocity.Y < 0f)
			{
				texture = anim2;
			}


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
		*/
	}
}