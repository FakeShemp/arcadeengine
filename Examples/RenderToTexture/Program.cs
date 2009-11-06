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
using ArcEngine.Graphic;

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
		public override void  LoadContent()
		{
			CreateGameWindow(new Size(1024, 768));
			Window.Text = "Frame Buffer example";

			// Enable depth test
			Display.DepthTest = true;

			Buffer = new FrameBuffer(new Size(256, 256));
			Texture = new Texture("data/test.png");
		}



		#endregion


		#region Game logic


		/// <summary>
		/// Called when the game determines it is time to draw a frame.
		/// </summary>
		public override void Draw()
		{
			Display.ClearColor = Color.Black;
			Display.ClearBuffers();

			Rectangle rect = new Rectangle(1, 1, 100, 100);

			// Bind the render buffer
			Buffer.Bind();
			Display.ClearColor = Color.CornflowerBlue;
			Display.ClearBuffers();

			Texture.Blit(new Point(100, 10));

			Display.DrawRectangle(rect, Color.Red);
			Display.DrawCircle(new Point(100, 10), 25);


			// Draw only in the depth buffer
			Display.ColorMask(false, false, false, false);

			Display.FillEllipse(new Rectangle(25, 125, 200, 100), Color.Yellow);
			Display.DrawRectangle(new Rectangle(25, 125, 200, 100), Color.Red);
			
			Display.ColorMask(true, true, true, true);

			Buffer.End();

			
			// Blit both buffer on the screen
			Display.Color = Color.White;
			Buffer.ColorTexture.Blit(new Point(50, 50));
			Buffer.DepthTexture.Blit(new Point(350, 50));

		}
		#endregion


		#region Properties


		/// <summary>
		/// Frame buffer
		/// </summary>
		FrameBuffer Buffer;



		/// <summary>
		/// Texture
		/// </summary>
		Texture Texture;

		#endregion
	}
}
