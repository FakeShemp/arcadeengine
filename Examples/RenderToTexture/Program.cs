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


		#region Initialization


		/// <summary>
		/// Loads contents
		/// </summary>
		/// <param name="e"></param>
		public override void  LoadContent()
		{
			CreateGameWindow(new Size(1024, 768));
			Window.Text = "Render Buffers test";

			// Write to the depth buffer to have something to display
			Display.DepthTest = true;

			RenderBuffer = new RenderBuffer(new Size(256, 256));
			Texture = new Texture("data/test.png");


			Display.StencilClearValue = 0;
		}



		#endregion


		#region Game logic


		/// <summary>
		/// Called when the game determines it is time to draw a frame.
		/// </summary>
		/// <param name="device">Rendering device</param>
		public override void  Draw()
		{

			Display.ClearColor = Color.DarkViolet;
			Display.ClearBuffers();

			Rectangle rect = new Rectangle(1, 1, 100, 100);

			// Bind the render buffer
			RenderBuffer.Start();
			Display.ClearColor = Color.CornflowerBlue;
			Display.ClearBuffers();

			Texture.Blit(new Point(100, 10));

			Display.DrawRectangle(rect, Color.Red);
			Display.DrawCircle(new Point(100, 10), 25);


			// Draw only in the stencil
			Display.StencilTest = true;
			Display.ColorMask(false, false, false, false);
			Display.StencilFunction(StencilFunction.Always, 1, 1);
			Display.StencilOp(StencilOp.Replace, StencilOp.Replace, StencilOp.Replace);


			Display.FillEllipse(new Rectangle(25, 125, 200, 100), Color.Yellow);
			Display.DrawRectangle(new Rectangle(25, 125, 200, 100), Color.Red);
			
						
			Display.StencilTest = false;
			Display.ColorMask(true, true, true, true);







			RenderBuffer.End();

			
			// Blit both buffer on the screen
			Display.Color = Color.White;
			RenderBuffer.ColorTexture.Blit(new Point(50, 50));
			RenderBuffer.DepthTexture.Blit(new Point(350, 50));
			RenderBuffer.StencilTexture.Blit(new Point(700, 50));

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
