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
	class RTT : GameBase
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

			// Enable depth test writting
			Display.RenderState.DepthTest = true;

			// Frame buffer
			Buffer = new FrameBuffer(new Size(256, 256));

			// Texture to display
			Texture = new Texture("data/test.png");

			SpriteBatch = new SpriteBatch();
		}



		/// <summary>
		/// Unload
		/// </summary>
		public override void UnloadContent()
		{
			if (Buffer != null)
				Buffer.Dispose();
			Buffer = null;

			if (Texture != null)
				Texture.Dispose();
			Texture = null;

			if (SpriteBatch != null)
				SpriteBatch.Dispose();
			SpriteBatch = null;
		}


		#endregion


		#region Game logic


		/// <summary>
		/// Called when the game determines it is time to draw a frame.
		/// </summary>
		public override void Draw()
		{
			Display.RenderState.ClearColor = Color.Black;
			Display.ClearBuffers();

			Rectangle rect = new Rectangle(1, 1, 100, 100);

			// Bind the render buffer
			Buffer.Bind();
			Display.RenderState.ClearColor = Color.CornflowerBlue;
			Display.ClearBuffers();

			SpriteBatch.Begin();
			SpriteBatch.Draw(Texture, new Vector2(100, 10), Color.White);

		//	Display.DrawRectangle(rect, Color.Red);
		//	Display.DrawCircle(new Point(100, 10), 25, Color.Red);


			// Draw only in the depth buffer
			Display.ColorMask(false, false, false, false);

	//		Display.FillEllipse(new Rectangle(25, 125, 200, 100), Color.Yellow);
	//		Display.DrawRectangle(new Rectangle(25, 125, 200, 100), Color.Red);
			
			Display.ColorMask(true, true, true, true);

			SpriteBatch.End();
			Buffer.End();

			
			// Blit both buffer on the screen
			Display.DrawTexture(Buffer.ColorTexture, new Point(50, 50));
			Display.DrawTexture(Buffer.DepthTexture, new Point(350, 50));


			Display.DrawTexture(Texture, new Point(100, 100));

		}
		#endregion


		#region Properties


		/// <summary>
		/// Frame buffer
		/// </summary>
		FrameBuffer Buffer;


		/// <summary>
		/// SpriteBatch
		/// </summary>
		SpriteBatch SpriteBatch;


		/// <summary>
		/// Texture
		/// </summary>
		Texture Texture;

		#endregion
	}
}
