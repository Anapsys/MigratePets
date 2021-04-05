using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MigratePets;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MigratePets.Projectiles.Pets
{
    public abstract class AdvancedPet : AbstractPet
    {
        #region texture paths
        public virtual string Texture => (base.GetType().Namespace + "." + this.Name).Replace('.', '/');
		public virtual string TexturePathWalk => (base.GetType().Namespace + "." + this.Name + "Walk").Replace('.', '/');
		public virtual string TexturePathFly => (base.GetType().Namespace + "." + this.Name + "Fly").Replace('.', '/');
		public virtual string TexturePathFall => (base.GetType().Namespace + "." + this.Name + "Fall").Replace('.', '/');
		#endregion

		public override void SetDefaults()
		{
			projectile.CloneDefaults(ProjectileID.BabyDino);
			//projectile.tileCollide = true;
			aiType = 0;//ProjectileID.BabyDino; //26;
			projectile.timeLeft = 2;
		}

		public override void AI()
		{
			#region AI... which should be all that's in this function. wtf.
			int testXPos = (int)(projectile.position.X + (float)(projectile.width / 2)) / 16;
			int testYPos = (int)(projectile.position.Y + (float)(projectile.height / 2)) / 16 + 1;
			Vector2 tileTestPos = new Vector2(testXPos, testYPos);
			bool grounded = WorldGen.SolidTile(testXPos, testYPos);
			if (grounded)
			{
				//throw new NoSuitableGraphicsDeviceException();
				projectile.velocity.Y = System.Math.Max(0f, projectile.velocity.Y);
				//Collision.StepDown(ref tileTestPos, ref projectile.velocity, projectile.width, projectile.height, ref projectile.stepSpeed, ref projectile.gfxOffY);
			}
			else if (projectile.oldVelocity.Y > 0)
			{
				projectile.velocity.Y += gravity;
				if (projectile.velocity.Y > maxGravity) projectile.velocity.Y = maxGravity;
			}

			Collision.StepUp(ref tileTestPos, ref projectile.velocity, projectile.width, projectile.height, ref projectile.stepSpeed, ref projectile.gfxOffY);
			//Collision.StepDown(ref tileTestPos, ref projectile.velocity, projectile.width, projectile.height, ref projectile.stepSpeed, ref projectile.gfxOffY);
			#endregion
		}

		public override void PostAI()
		{
			#region Animation and visuals... which should not be in the AI code...
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
			//Lighting.AddLight(projectile.Center, Color.White.ToVector3() * 0.78f);
			#endregion
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			SpriteEffects spriteEffects = SpriteEffects.None;
			if (projectile.spriteDirection == -1)
			{
				spriteEffects = SpriteEffects.FlipHorizontally;
			}
			Texture2D texture = Main.projectileTexture[projectile.type]; //idle
			Texture2D anim1 = ModContent.GetTexture(this.TexturePathWalk); //walk
			Texture2D anim2 = ModContent.GetTexture(this.TexturePathFly); //fly
			Texture2D anim3 = ModContent.GetTexture(this.TexturePathFall); //fall
			int numFrames = Main.projFrames[projectile.type];

			//given some checks, override the default idle anim with....
			if (System.Math.Abs(projectile.velocity.X) > 0)
			{
				texture = anim1;
				//numFrames = 6; if an animation has a different number of frames, set it like so
			}
			if (projectile.velocity.Y >= 0.04f)
			{
				texture = anim3;
			}
			if (projectile.velocity.Y < 0f)
			{
				texture = anim2;
			}


			int frameHeight = texture.Height / numFrames;
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
