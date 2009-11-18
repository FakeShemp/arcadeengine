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


namespace ArcEngine.Examples.TextDemo
{
	/// <summary>
	/// Main game class
	/// </summary>
	public class EmptyProject : Game
	{

		/// <summary>
		/// Main entry point.
		/// </summary>
		[STAThread]
		static void Main()
		{
			try
			{
				using (EmptyProject game = new EmptyProject())
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
		public EmptyProject()
		{
			CreateGameWindow(new Size(1024, 768));
		}



		/// <summary>
		/// Load contents 
		/// </summary>
		public override void LoadContent()
		{
			Display.ClearColor = Color.CornflowerBlue;

			// Load the bank
			ResourceManager.LoadBank("data/data.bnk");

			// Creates the font
			Font = Font2d.CreateFromTTF(@"c:\windows\fonts\verdana.ttf", 14, FontStyle.Regular);
			
			// Attach the TileSet to the font
			Font.TextTileset = ResourceManager.CreateAsset<TileSet>("TextTileSet");




			//Text = "Simple text to display. <tile id=\"1\"/><br>This is a text to display";
			Text = "Lorem ipsum dolor sit amet,   &lt;  &gt; &amp; consectetur adipiscing elit. Duis gravida mattis mi a euismod. " +
				"Cras et sodales dolor. Nulla iaculis, elit quis mollis tempus, ante dui auctor erat, id aliquet quam sapien in ante. " +
				"<color r=\"255\" g=\"0\" b=\"0\" a=\"255\">Ut ac mi turpis, vel pellentesque nisi.</color> Duis in eleifend turpis. Aliquam vel <tile id=\"7\" />lorem enim. " +
				"Vestibulum auctor iaculis mauris, non gravida libero bibendum eu. Cras lacinia augue vel lectus accumsan dictum mattis lorem mollis. " +
				"Quisque interdum placerat turpis sed vestibulum. Donec luctus pretium metus sit amet gravida. <br><br>" +
				"Vivamus neque sapien, faucibus nec vestibulum ac, pharetra tempus odio. Maecenas porta porta eros in cursus.<br><br>" +
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
			{
				Font.Dispose();
				Font = null;
			}
		}




		/// <summary>
		/// Called when it is time to draw a frame.
		/// </summary>
		public override void Draw()
		{
			// Clears the background
			Display.ClearBuffers();

			Rectangle rectangle = new Rectangle(100, 100, 800, 300);
			Font.DrawText(rectangle, Color.White, Text);

		}




		#region Properties


		/// <summary>
		/// Font
		/// </summary>
		Font2d Font;


		/// <summary>
		/// Text to display
		/// </summary>
		string Text;

		#endregion

	}

}
