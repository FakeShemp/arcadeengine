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
using ArcEngine.Input;
using System;
using System.Drawing;
using System.Windows.Forms;
using ArcEngine.Graphic;
using OpenTK.Graphics.OpenGL;

namespace ArcEngine.Examples.RenderToTexture
{
	class RTT : Game
	{

		/// <summary>
		/// Main entry point.
		/// </summary>
		[STAThread]
		static void Main()
		{
			using (RTT rtt = new RTT())
				rtt.Run();
		}


		/// <summary>
		/// Constructor
		/// </summary>
		public RTT()
		{
			
		}


		#region Initialization


		/// <summary>
		/// 
		/// </summary>
		/// <param name="e"></param>
		public override void  LoadContent()
		{
			CreateGameWindow(new Size(1024, 768));


			RenderBuffer = new RenderBuffer(new Size(256, 256));
			Texture = new Texture("data/test.png");
		}



		#endregion


		#region Game logic


		/// <summary>
		/// Called when the game determines it is time to draw a frame.
		/// </summary>
		/// <param name="device">Rendering device</param>
		public override void  Draw()
		{

			Display.ClearColor = Color.Black;
			Display.ClearBuffers();

			Rectangle rect = new Rectangle(1, 1, 100, 100);

			

			// Bind the render buffer
			RenderBuffer.Start();
			Display.ClearColor = Color.CornflowerBlue;
			Display.ClearBuffers();

			Texture.Blit(new Point(200, 10));

			Display.DrawRectangle(rect, Color.Red);
			Display.DrawCircle(new Point(100, 100), 25);
			Display.FillEllipse(new Rectangle(25, 25, 200, 100), Color.Yellow);


			RenderBuffer.End();

			
			// Blit both buffer on the screen
			Display.Color = Color.White;
			RenderBuffer.ColorTexture.Blit(new Point(50, 50));
			RenderBuffer.DepthTexture.Blit(new Point(350, 50));



			//Display.DrawEllipse(new Rectangle(25, 25, 200, 100), Color.Yellow);
			//Display.FillEllipse(new Rectangle(250, 250, 2000, 200), Color.Green);

			// Save it to the disk
	//		RenderBuffer.ColorTexture.SaveToDisk("colorbuffer.png");

		}



		/// <summary>
		/// Called when the game has determined that game logic needs to be processed.
		/// </summary>
		/// <param name="gameTime">The time passed since the last update.</param>
		public override void  Update(GameTime gameTime)
		{

			// Byebye
			if (Keyboard.IsKeyPress(Keys.Escape))
				Exit();
		}


		#endregion


		#region Properties


		/// <summary>
		/// 
		/// </summary>
		RenderBuffer RenderBuffer;



		/// <summary>
		/// 
		/// </summary>
		Texture Texture;

		#endregion
	}
}
