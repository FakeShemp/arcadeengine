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
using ArcEngine.Input;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Xml;


namespace ArcEngine.Examples.Bunny
{
	/// <summary>
	/// Main game class
	/// </summary>
	public class DrawBuffers : GameBase
	{

		/// <summary>
		/// Main entry point.
		/// </summary>
		[STAThread]
		static void Main()
		{
			try
			{
				using (DrawBuffers game = new DrawBuffers())
					game.Run();
			}
			catch (Exception e)
			{
				// Oops, an error happened !
				MessageBox.Show(e.StackTrace, e.Message);
				Trace.WriteLine(e.Message);
				Trace.WriteLine(e.StackTrace);
			}
		}


		/// <summary>
		/// Constructor
		/// </summary>
		public DrawBuffers()
		{
			CreateGameWindow(new Size(1024, 768));
			Window.Text = "Bunny example";
		}



		/// <summary>
		/// Loads contents 
		/// </summary>
		public override void LoadContent()
		{
			Display.ClearColor = Color.CornflowerBlue;


			#region Buffer

			// Create index buffer
			Index = new IndexBuffer();


			// Creates vertex buffer
			Buffer = new BatchBuffer();
			Buffer.AddDeclaration("in_position", 3, sizeof(float) * 10, 0);
			Buffer.AddDeclaration("in_normal", 3, sizeof(float) * 10, sizeof(float) * 3);
			Buffer.AddDeclaration("in_color", 4, sizeof(float) * 10, sizeof(float) * 7);

			LoadBunny("data/bunny.xml");

			#endregion


			#region Font

			Font = BitmapFont.CreateFromTTF("c:\\windows\\fonts\\verdana.ttf", 16, FontStyle.Regular);

			#endregion


			#region Matrices

			float aspectRatio = (float)Display.ViewPort.Width / (float)Display.ViewPort.Height;
			Display.ProjectionMatrix = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 4.0f, aspectRatio, 0.1f, 20.0f);


			Position = new Vector3(0.0f, 0.0f, -1.5f);
			Display.ModelViewMatrix = Matrix4.LookAt(
				Position, 
				Vector3.Zero, 
				Vector3.UnitY);

			#endregion

		}


		/// <summary>
		/// Loads bunny data
		/// </summary>
		/// <param name="filename">File name to load</param>
		void LoadBunny(string filename)
		{
			XmlDocument doc = new XmlDocument();
			doc.Load(filename);
			XmlElement root = doc.DocumentElement;
			if (root.Name.ToLower() != "bunny")
				return;

			float[] buffer = null;
			int[] indices = null;

			foreach (XmlNode node in root.ChildNodes)
			{

				switch (node.Name.ToLower())
				{
					case "vertex":
					{
						buffer = new float[int.Parse(node.Attributes["count"].Value) * 10];
						string[] split = node.InnerText.Split(new char[] { '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries);

						for (int i = 0; i < split.Length / 3; i++)
						{
							buffer[i * 10] = float.Parse(split[i]);
							buffer[i * 10 + 1] = float.Parse(split[i + 1]);
							buffer[i * 10 + 2] = float.Parse(split[i + 2]);
						}
					}
					break;


					case "index":
					{
						indices = new int[int.Parse(node.Attributes["count"].Value) * 3];
						string[] split = node.InnerText.Split(new char[] { '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries);

						for (int i = 0; i < split.Length; i++)
							indices[i] = int.Parse(split[i]);

					}
					break;


					case "normal":
					{
						string[] split = node.InnerText.Split(new char[] { '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries);

						for (int i = 0; i < split.Length / 3; i++)
						{
							buffer[i * 10 + 3] = float.Parse(split[i]);
							buffer[i * 10 + 4] = float.Parse(split[i + 1]);
							buffer[i * 10 + 5] = float.Parse(split[i + 2]);
						}
					}
					break;
				}
			}

			// Update index buffer
			Index.Update(indices);

			// Update vertex buffer
			Buffer.SetVertices(buffer);

		}


		/// <summary>
		/// Unload contents
		/// </summary>
		public override void UnloadContent()
		{
			if (Index != null)
				Index.Dispose();
			Index = null;

			if (Buffer != null)
				Buffer.Dispose();
			Buffer = null;

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
			// Check if the Escape key is pressed
			if (Keyboard.IsKeyPress(Keys.Escape))
				Exit();


/*
			if (Keyboard.IsKeyPress(Keys.Q))
				Position.X -= Speed;

			if (Keyboard.IsKeyPress(Keys.D))
				Position.X += Speed;

			if (Keyboard.IsKeyPress(Keys.Z))
				Position.Y -= Speed;

			if (Keyboard.IsKeyPress(Keys.S))
				Position.Y += Speed;


			Display.ModelViewMatrix = Matrix4.LookAt(
				Position,
				Vector3.Zero,
				Vector3.UnitY);
*/

			Display.ModelViewMatrix = Display.ModelViewMatrix * Matrix4.CreateRotationZ(0.01f);
		}



		/// <summary>
		/// Called when it is time to draw a frame.
		/// </summary>
		public override void Draw()
		{
			// Clears the background
			Display.ClearBuffers();

			// Some dummy text
			Font.DrawText(new Point(100, 25), Color.White, "Here's an example of draw buffers.");

			// Draws with the index buffer
			Display.DrawIndexBuffer(Buffer, BeginMode.Triangles, Index);

			//Display.DrawBatch(Buffer, 0, 5000);

			//Font.DrawText(new Point(10, 100), Color.White, Position.ToString());
		}



		#region Properties

		/// <summary>
		/// Index buffer
		/// </summary>
		IndexBuffer Index;

		/// <summary>
		/// Index buffer
		/// </summary>
		BatchBuffer Buffer;


		/// <summary>
		/// Font
		/// </summary>
		BitmapFont Font;


		/// <summary>
		/// 
		/// </summary>
		Vector3 Position;


		/// <summary>
		/// 
		/// </summary>
		float Speed = 1.0f / 60.0f;

		#endregion

	}
}
