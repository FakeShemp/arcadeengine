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
using ArcEngine.Asset;
using ArcEngine.Graphic;
using ArcEngine.Storage;

namespace ArcEngine.Examples.TextDemo
{
	/// <summary>
	/// Main game class
	/// </summary>
	public class Program : GameBase
	{

		/// <summary>
		/// Main entry point.
		/// </summary>
		[STAThread]
		static void Main()
		{
			try
			{
				using (Program game = new Program())
					game.Run();
			}
			catch (Exception e)
			{
				// Oops, an error happened !
				MessageBox.Show(e.StackTrace, e.Message);
			}
		}


		/// <summary>
		/// Constructor
		/// </summary>
		public Program()
		{
			CreateGameWindow(new Size(1024, 768));
		}



		/// <summary>
		/// Load contents 
		/// </summary>
		public override void LoadContent()
		{
			Display.RenderState.ClearColor = Color.CornflowerBlue;

			// Load the bank
			ResourceManager.AddStorage(new BankStorage("data/data.bnk"));

			// Creates the font
			Font = BitmapFont.CreateFromTTF(@"c:\windows\fonts\verdana.ttf", 14, FontStyle.Regular);
			
			// Attach the TileSet to the font
			Font.TextTileset = ResourceManager.CreateAsset<TileSet>("TextTileSet");

            SpriteBatch = new SpriteBatch();


			//Text = "Simple text to display. <tile id=\"1\"/><br>This is a text to display";
			Text = @"Lorem ipsum dolor sit amet,   &lt;  &gt; &amp; consectetur adipiscing elit. Duis gravida mattis mi a euismod. " +
				"Cras et sodales dolor. Nulla iaculis, elit quis mollis tempus, ante dui auctor erat, id aliquet quam sapien in ante. " +
				"<color r=\"255\" g=\"0\" b=\"0\" a=\"255\">Ut ac mi turpis, vel pellentesque nisi.</color> Duis in eleifend turpis. Aliquam vel <tile id=\"7\" />lorem enim. " +
				"Vestibulum auctor iaculis mauris, non gravida libero bibendum eu. Cras lacinia augue vel lectus accumsan dictum mattis lorem mollis. " +
				"Quisque interdum placerat turpis sed vestibulum. Donec luctus pretium metus sit amet gravida. <br /><br />" +
				"Vivamus neque sapien, faucibus nec vestibulum ac, pharetra tempus odio. Maecenas porta porta eros in cursus.<br /><br />" +
				"Sed tristique velit id diam varius aliquet. <color r=\"128\" g=\"0\" b=\"200\" a=\"255\">Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. " +
				"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer eu dui tu</color>rpis, ut porta nibh. " +
				"Quisque lacinia convallis elit, ac dapibus turpis tristique et. ";
		}



		/// <summary>
		/// Unload contents
		/// </summary>
		public override void UnloadContent()
		{
			if (Font != null)
				Font.Dispose();
			Font = null;

            if (SpriteBatch != null)
                SpriteBatch.Dispose();
            SpriteBatch = null;

		}




		/// <summary>
		/// Called when it is time to draw a frame.
		/// </summary>
		public override void Draw()
		{
			// Clears the background
			Display.ClearBuffers();

            SpriteBatch.Begin();

			Rectangle rectangle = new Rectangle(100, 100, 800, 300);
			SpriteBatch.DrawString(Font, rectangle, Color.White, Text);

            SpriteBatch.End();
		}




		#region Properties


		/// <summary>
		/// Font
		/// </summary>
		BitmapFont Font;


        /// <summary>
        /// 
        /// </summary>
        SpriteBatch SpriteBatch;


		/// <summary>
		/// Text to display
		/// </summary>
		string Text;

		#endregion

	}

}
