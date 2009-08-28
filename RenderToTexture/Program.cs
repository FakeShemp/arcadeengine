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
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ArcEngine;
using ArcEngine.Graphic;
using OpenTK;
using OpenTK.Graphics;

namespace RenderToTexture
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
			Window.ClientSize = new Size(650, 350);
			
		}


		#region Initialization


		/// <summary>
		/// 
		/// </summary>
		/// <param name="e"></param>
		public override void  LoadContent()
		{

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




			RenderBuffer.Start();

			Display.ClearColor = Color.CornflowerBlue;
			Display.ClearBuffers();

			//Texture.Blit(new Point(10, 10));

	//		Display.Rectangle(new Rectangle(1,1, 10, 10), true);

			Rectangle rect = new Rectangle(1, 1, 10, 10);
			GL.Begin(BeginMode.Quads);
			GL.Vertex2(rect.X, rect.Y);
			GL.Vertex2(rect.X, rect.Bottom);
			GL.Vertex2(rect.Right, rect.Bottom);
			GL.Vertex2(rect.Right, rect.Y);
			GL.End();


			RenderBuffer.End();

			
			RenderBuffer.ColorTexture.Blit(new Point(50, 50));
			RenderBuffer.DepthTexture.Blit(new Point(350, 50));


			RenderBuffer.ColorTexture.Save("colorbuffer.png");

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
