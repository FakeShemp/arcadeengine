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
using System;
using System.Drawing;
using System.Windows.Forms;
using ArcEngine;
using ArcEngine.Asset;
using ArcEngine.Graphic;
using ArcEngine.Input;
using FarseerGames.FarseerPhysics;
using FarseerGames.FarseerPhysics.Collisions;
using FarseerGames.FarseerPhysics.Controllers;
using FarseerGames.FarseerPhysics.Dynamics;
using FarseerGames.FarseerPhysics.Dynamics.Joints;
using FarseerGames.FarseerPhysics.Factories;
using FarseerGames.FarseerPhysics.Interfaces;
using FarseerGames.FarseerPhysics.Mathematics;
using OpenTK.Graphics.OpenGL;


//
// http://nerg4l.ne.funpic.de/doku.php/wiki:tutorials:nergal:quickstartsample
//
namespace ArcEngine.Games.ProjectT
{
	/// <summary>
	/// Main game class
	/// </summary>
	public class Template : GameBase
	{


		/// <summary>
		/// Main entry point.
		/// </summary>
		[STAThread]
		static void Main()
		{
			try
			{
				using (Template game = new Template())
					game.Run();
			}
			catch (Exception e)
			{
				MessageBox.Show(e.StackTrace, e.Message);
			}
		}


		/// <summary>
		/// Constructor
		/// </summary>
		public Template()
		{
			CreateGameWindow(new Size(1024, 768));
			Window.Text = "Simple Physic";

		}


		float torqueAmount = 500000;
			Size size = new Size(200, 200);

		/// <summary>
		/// Load contents 
		/// </summary>
		public override void LoadContent()
		{
			Display.ClearColor = Color.CornflowerBlue;

			Font = new BitmapFont();
			Font.LoadTTF(@"data/verdana.ttf", 12, FontStyle.Regular);



			//create the physicsSimulator
			physicsSimulator = new PhysicsSimulator(new Vector2(0, 0));
			physicsSimulator.Iterations = 10;


			//create body for box1
			boxBody1 = BodyFactory.Instance.CreateRectangleBody(physicsSimulator, size.Width, size.Height, 1);
			boxBody1.Position = new Vector2(400, 100);

			//create geometry for box1
			boxGeom1 = GeomFactory.Instance.CreateRectangleGeom(physicsSimulator, boxBody1, size.Width, size.Height);

			//same procedure for box2
			boxBody2 = BodyFactory.Instance.CreateRectangleBody(physicsSimulator, size.Width, size.Height, 1);
			boxBody2.Position = new Vector2(400, 400);

			boxGeom2 = GeomFactory.Instance.CreateRectangleGeom(physicsSimulator, boxBody2, size.Width, size.Height);

			//create a revolute joint between the two boxes
			revoluteJoint = JointFactory.Instance.CreateRevoluteJoint(physicsSimulator, boxBody1, boxBody2, new Vector2(200, 300));




		}


		/// <summary>
		/// Unload contents
		/// </summary>
		public override void UnloadContent()
		{
			if (Font != null)
				Font.Dispose();
			Font = null;
		}


		/// <summary>
		/// Update the game logic
		/// </summary>
		/// <param name="gameTime"></param>
		public override void Update(GameTime gameTime)
		{
			// Byebye
			if (Keyboard.IsKeyPress(Keys.Escape))
				Exit();


			//apply some simple forces to box2 when the arrow keys are pressed
			if (Keyboard.IsKeyPress(Keys.Left)) 
				boxBody2.ApplyForce(new Vector2(-400, 0));
			if (Keyboard.IsKeyPress(Keys.Right)) 
				boxBody2.ApplyForce(new Vector2(400, 0));
			if (Keyboard.IsKeyPress(Keys.Up))
				boxBody2.ApplyForce(new Vector2(0, -400));
			if (Keyboard.IsKeyPress(Keys.Down)) 
				boxBody2.ApplyForce(new Vector2(0, 400));


			float torque = 0;
			if (Keyboard.IsKeyPress(Keys.K))
			{
				torque -= torqueAmount;
			}
			if (Keyboard.IsKeyPress(Keys.L))
			{
				torque += torqueAmount;
			}
			boxBody2.ApplyTorque(torque);


			if (Keyboard.IsNewKeyPress(Keys.Space))
			{
				boxBody2.IsStatic = !boxBody2.IsStatic;
				if (boxBody2.IsStatic)
				{
					boxBody2.ClearTorque();
					boxBody2.ClearForce();
					boxBody2.ClearImpulse();
				}
			}


			//step the simulator. must convert the value of dt to seconds
			physicsSimulator.Update(gameTime.ElapsedGameTime.Milliseconds * 0.001f);


			angle += 1.0f;
			if (angle > 360)
				angle = 0.0f;

		}



		/// <summary>
		/// Called when it is time to draw a frame.
		/// </summary>
		/// <param name="device"></param>
		public override void Draw()
		{
			// Clears the background
			Display.ClearBuffers();


			// box 1
			Display.FillRectangle(new Rectangle((int)boxBody1.Position.X, (int)boxBody1.Position.Y, 190, 190), 
				Color.Red, boxBody1.TotalRotation, new Point(size.Width / 2, size.Height / 2));
			Display.DrawRectangle(new Rectangle((int)boxBody1.Position.X, (int)boxBody1.Position.Y, 190, 190), 
				Color.Black, boxBody1.TotalRotation, new Point(size.Width / 2, size.Height / 2));

	
			// box 2
			Display.FillRectangle(new Rectangle((int)boxBody2.Position.X, (int)boxBody2.Position.Y, 190, 190),
				Color.Green, boxBody2.TotalRotation, new Point(size.Width / 2, size.Height / 2));
			Display.DrawRectangle(new Rectangle((int)boxBody2.Position.X, (int)boxBody2.Position.Y, 190, 190), 
				Color.Black, boxBody2.TotalRotation, new Point(size.Width / 2, size.Height / 2));



			Rectangle rect = new Rectangle(200, 200, 100, 100);
			Display.FillRectangle(rect, Color.Blue, angle, new Point(50, 50));
			Display.DrawRectangle(rect, Color.Blue);


			Font.DrawText(new Point(10, 100), Color.White, boxBody2.TotalRotation.ToString());
		}




		#region Properties

		//declare physics variables
		PhysicsSimulator physicsSimulator;

		Body boxBody1;
		Body boxBody2;

		Geom boxGeom1;
		Geom boxGeom2;

		RevoluteJoint revoluteJoint;


		float angle = 0;


		/// <summary>
		/// 
		/// </summary>
		BitmapFont Font;

		#endregion

	}


}
