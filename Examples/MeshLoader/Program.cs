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
using ArcEngine.Graphic;
using ArcEngine.Input;
using ArcEngine.Examples.MeshLoader.MD5;

namespace ArcEngine.Examples.MeshLoader
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
			using (Program game = new Program())
				game.Run();
		}


		/// <summary>
		/// Constructor
		/// </summary>
		public Program()
		{
			CreateGameWindow(new Size(1024, 768));
			Window.Text = "Mesh loader";
		}



		/// <summary>
		/// Load contents 
		/// </summary>
		public override void LoadContent()
		{
			Display.RenderState.ClearColor = Color.CornflowerBlue;


		//	ColladaLoader loader = new ColladaLoader();
		//	loader.Load("data/seymourplane_triangulate.dae");
		//	Shape = loader.GenerateShape("propShape");


			MD5 = new MD5Mesh();
			MD5.Load(@"data/md5/zfat.md5mesh");

		}



		/// <summary>
		/// Unload contents
		/// </summary>
		public override void UnloadContent()
		{
			if (Collada != null)
				Collada.Dispose();
			Collada = null;

			if (MD5 != null)
				MD5.Dispose();
			MD5 = null;

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


		}



		/// <summary>
		/// Called when it is time to draw a frame.
		/// </summary>
		public override void Draw()
		{
			// Clears the background
			Display.ClearBuffers();


			#region Matrices
			Vector3 CameraPostion = new Vector3(0.0f, 0.1f, 3.0f);
			Vector3 target = Vector3.Add(CameraPostion, new Vector3(0.0f, 0.0f, -1.0f));

			float aspectRatio = (float)Display.ViewPort.Width / (float)Display.ViewPort.Height;
			Matrix4 ProjectionMatrix = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45.0f), aspectRatio, 0.1f, 100.0f);
			Matrix4 ModelViewMatrix = Matrix4.LookAt(CameraPostion, target, Vector3.UnitY);
			Matrix4 mvp = ModelViewMatrix * ProjectionMatrix;
			#endregion


			if (Collada != null)
			{
				//Collada.Draw(Shader);
			}


			if (MD5 != null)
			{
				Display.Shader = MD5.Shader;
				Display.Shader.SetUniform("mvpMatrix", Matrix4.CreateTranslation(new Vector3(0.0f, 0.0f, -5.0f)) * mvp);
				Display.Shader.SetUniform("mvMatrix", ModelViewMatrix);
				MD5.Draw();
			}
		}




		#region Properties


		/// <summary>
		/// 
		/// </summary>
		MD5Mesh MD5;


		/// <summary>
		/// 
		/// </summary>
		Mesh Collada;


		#endregion

	}

}
