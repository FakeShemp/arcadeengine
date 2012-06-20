#region Licence
//
//This file is part of ArcEngine.
//Copyright (C)2008-2009 Adrien Hémery ( iliak@mimicprod.net )
//
//ArcEngine is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//any later version.
//
//ArcEngine is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.
//
//You should have received a copy of the GNU General Public License
//along with Foobar.  If not, see <http://www.gnu.org/licenses/>.
//
#endregion
using ArcEngine;
using RuffnTumble.Interface;
using System.ComponentModel;
using System;
using System.Drawing;
using System.Xml;
using ArcEngine.Graphic;
using ArcEngine.Asset;
using ArcEngine.Input;
using System.Windows.Forms;

namespace RuffnTumble
{
	/// <summary>
	/// The hero of the Game
	/// </summary>
	public class Player : Entity
	{

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="level">Level handle</param>
		public Player(Level level)
		{
			if (level == null)
				throw new ArgumentNullException("level", "Level handle is null");

			Level = level;
			position = new Vector2(200, 100);
			localBounds = new Rectangle(0, 0, 32, 64);
		}


		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public override bool Init()
		{
			isAlive = true;

			return true;
		}


		/// <summary>
		/// Update the player logic
		/// </summary>
		/// <param name="time"></param>
		public override void Update(GameTime time)
		{
			#region Get inputs

			// Left or right movement
			if (Keyboard.IsKeyPress(Keys.Left))
				Movement = -1.0f;
			else if (Keyboard.IsKeyPress(Keys.Right))
				Movement = 1.0f;

			// Jump
			if (Keyboard.IsKeyPress(Keys.Up))
				isJumping = true;

			#endregion

			// Apply physic law
			ApplyPhysic(time);


			// Play the animation
			if (isOnGround && isAlive)
			{
				if (Math.Abs(Velocity.X) - 0.02f > 0)
				{
					//sprite.PlayAnimation(runAnimation);
				}
				else
				{
					//sprite.PlayAnimation(idleAnimation);
				}
			}

			// Debug
			if (Keyboard.IsNewKeyPress(Keys.P))
			{
				position = new Vector2(400, 100);
				velocity = Vector2.Zero;
				Movement = 0.0f;
			}

			// Clear input.
			Movement = 0.0f;
			isJumping = false;
	
		}


		/// <summary>
		/// Upatde player's velocity and position
		/// </summary>
		/// <param name="time">Elapsed game time</param>
		void ApplyPhysic(GameTime time)
		{
			float elapsed = (float)time.ElapsedGameTime.TotalSeconds;

			Vector2 previousPosition = Position;

			// Base velocity is a combination of horizontal movement control and
			// acceleration downward due to gravity.
			velocity.X += Movement * MoveAcceleration * elapsed;
			velocity.Y = MathHelper.Clamp(velocity.Y + GravityAcceleration * elapsed, -MaxFallSpeed, MaxFallSpeed);

			velocity.Y = DoJump(velocity.Y, time);

			// Apply pseudo-drag horizontally.
			if (IsOnGround)
				velocity.X *= GroundDragFactor;
			else
				velocity.X *= AirDragFactor;

			// Prevent the player from running faster than his top speed.            
			velocity.X = MathHelper.Clamp(velocity.X, -MaxMoveSpeed, MaxMoveSpeed);

			// Apply velocity.
			Position += velocity * elapsed;
			Position = new Vector2((float)Math.Round(Position.X), (float)Math.Round(Position.Y));

			// If the player is now colliding with the level, separate them.
			HandleCollisions();

			// If the collision stopped us from moving, reset the velocity to zero.
			if (Position.X == previousPosition.X)
				velocity.X = 0;

			if (Position.Y == previousPosition.Y)
				velocity.Y = 0;
		}


		/// <summary>
		/// Calculates the Y velocity accounting for jumping and animates accordingly.
		/// </summary>
		/// <remarks>
		/// During the accent of a jump, the Y velocity is completely
		/// overridden by a power curve. During the decent, gravity takes
		/// over. The jump velocity is controlled by the jumpTime field
		/// which measures time into the accent of the current jump.
		/// </remarks>
		/// <param name="velocityY">The player's current velocity along the Y axis.</param>
		/// <returns>
		/// A new Y velocity if beginning or continuing a jump.
		/// Otherwise, the existing Y velocity.
		/// </returns>
		private float DoJump(float velocityY, GameTime gameTime)
		{
			// If the player wants to jump
			if (isJumping)
			{
				// Begin or continue a jump
				if ((!wasJumping && IsOnGround) || jumpTime > 0.0f)
				{
					//if (jumpTime == 0.0f)
					//	jumpSound.Play();

					jumpTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
					//sprite.PlayAnimation(jumpAnimation);
				}

				// If we are in the ascent of the jump
				if (0.0f < jumpTime && jumpTime <= MaxJumpTime)
				{
					// Fully override the vertical velocity with a power curve that gives players more control over the top of the jump
					velocityY = JumpLaunchVelocity * (1.0f - (float)Math.Pow(jumpTime / MaxJumpTime, JumpControlPower));
				}
				else
				{
					// Reached the apex of the jump
					jumpTime = 0.0f;
				}
			}
			else
			{
				// Continues not jumping or cancels a jump in progress
				jumpTime = 0.0f;
			}

			wasJumping = isJumping;

			return velocityY;
		}


		/// <summary>
		/// Detects and resolves all collisions between the player and his neighboring
		/// tiles. When a collision is detected, the player is pushed away along one
		/// axis to prevent overlapping. There is some special logic for the Y axis to
		/// handle platforms which behave differently depending on direction of movement.
		/// </summary>
		private void HandleCollisions()
		{
			// Get the player's bounding rectangle and find neighboring tiles.
			Rectangle bounds = BoundingRectangle;
			int leftTile = (int)Math.Floor((float)bounds.Left / Level.BlockSize.Width);
			int rightTile = (int)Math.Ceiling(((float)bounds.Right / Level.BlockSize.Width)) - 1;
			int topTile = (int)Math.Floor((float)bounds.Top / Level.BlockSize.Height);
			int bottomTile = (int)Math.Ceiling(((float)bounds.Bottom / Level.BlockSize.Height)) - 1;

			// Reset flag to search for ground collision.
			isOnGround = false;

			// For each potentially colliding tile,
			for (int y = topTile; y <= bottomTile; ++y)
			{
				for (int x = leftTile; x <= rightTile; ++x)
				{
					// If this tile is collidable,
					TileCollision collision = Level.GetCollision(x, y);
					if (collision == TileCollision.Passable)
						continue;

					// Death tile
					if (collision == TileCollision.Death)
					{
						isAlive = false;
						return;
					}
					
					// Determine collision depth (with direction) and magnitude.
					Rectangle tileBounds = Level.GetBounds(x, y);
					Vector2 depth = MathHelper.GetIntersectionDepth(bounds, tileBounds);
					if (depth == Vector2.Zero)
						continue;

					
					float absDepthX = Math.Abs(depth.X);
					float absDepthY = Math.Abs(depth.Y);

					// Resolve the collision along the shallow axis.
					if (absDepthY < absDepthX || collision == TileCollision.Platform)
					{
						// If we crossed the top of a tile, we are on the ground.
						if (previousBottom <= tileBounds.Top)
							isOnGround = true;

						// Ignore platforms, unless we are on the ground.
						if (collision == TileCollision.Impassable || IsOnGround)
						{
							// Resolve the collision along the Y axis.
							Position = new Vector2(Position.X, Position.Y + depth.Y);

							// Perform further collisions with the new bounds.
							bounds = BoundingRectangle;
						}
					}
					else if (collision == TileCollision.Impassable) // Ignore platforms.
					{
						// Resolve the collision along the X axis.
						Position = new Vector2(Position.X + depth.X, Position.Y);

						// Perform further collisions with the new bounds.
						bounds = BoundingRectangle;
					}
				}
				
				
			}

			// Save the new bounds bottom.
			previousBottom = bounds.Bottom;
		}



		/// <summary>
		/// Render the player
		/// </summary>
		public override void Draw(SpriteBatch batch, Camera camera)
		{
			Vector4 pos = new Vector4(Position.X - localBounds.Width / 2.0f, Position.Y - localBounds.Height, localBounds.Width, localBounds.Height);
			pos.Offset(-camera.Location);
			batch.FillRectangle(pos, Color.Blue);

			Vector2 loc = Position;
			loc.Offset(-camera.Location);
			batch.DrawPoint(loc, Color.Red);
		}



		#region Properties

		// Animation
		Animation IdleAnimation;
		Animation RunAnimation;
		Animation JumpAnimation;
		Animation DieAnimation;

		/// <summary>
		/// Current user movement input.
		/// </summary>
		private float Movement;


		// Constants for controling horizontal movement
		private const float MoveAcceleration = 13000.0f;
		private const float MaxMoveSpeed = 2000.0f;
		private const float GroundDragFactor = 0.60f;
		private const float AirDragFactor = 0.57f;

		// Constants for controlling vertical movement
		private const float MaxJumpTime = 0.35f;
		private const float JumpLaunchVelocity = -3500.0f;
		private const float GravityAcceleration = 3400.0f;
		private const float MaxFallSpeed = 550.0f;
		private const float JumpControlPower = 0.15f;


		// Jumping state
		private bool isJumping;
		private bool wasJumping;
		private float jumpTime;
		private float previousBottom;


		/// <summary>
		/// 
		/// </summary>
		public bool IsAlive
		{
			get { return isAlive; }
		}
		bool isAlive;


		/// <summary>
		/// Level handle
		/// </summary>
		private Level Level;


		/// <summary>
		/// Gets a rectangle which bounds this player in world space.
		/// </summary>
		public Rectangle BoundingRectangle
		{
			get
			{
				int left = (int)Math.Round(Position.X - 16) + localBounds.X;
				int top = (int)Math.Round(Position.Y - 64) + localBounds.Y;

				return new Rectangle(left, top, localBounds.Width, localBounds.Height);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		private Rectangle localBounds;

		#endregion
	}
}
