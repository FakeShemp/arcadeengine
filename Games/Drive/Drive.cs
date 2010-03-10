using System;
using System.Drawing;
using System.Windows.Forms;
using ArcEngine;
using ArcEngine.Asset;
using ArcEngine.Graphic;
using ArcEngine.Input;
using OpenTK.Graphics.OpenGL;



// From CodeSample : http://www.codesampler.com/oglsrc/oglsrc_4.htm

namespace Drive
{
	class Game : GameBase
	{
		/// <summary>
		/// Application entry point
		/// </summary>
		[STAThread]
		static void Main()
		{
			Game game = null;
			try
			{
				using (game = new Game())
					game.Run();
			}
			catch (Exception e)
			{
				Trace.WriteLine("");
				Trace.WriteLine("!!!FATAL ERROR !!!");
				Trace.WriteLine("Message : " + e.Message);
				Trace.WriteLine("StackTrace : " + e.StackTrace);
				Trace.WriteLine("");

				MessageBox.Show(e.StackTrace, e.Message);
			}
		}


		/// <summary>
		/// Constructor
		/// </summary>
		public Game()
		{
		}




		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.
		/// </summary>
		public override void LoadContent()
		{
			GameWindowParams param = new GameWindowParams();
			param.Samples = 0;
			param.Size = new Size(1024, 768);
			CreateGameWindow(param);

			Window.Text = "Drive";
			Window.Resizable = true;


			//tex1 = new Texture("data/dirt_01.png");
			//tex2 = new Texture("data/grass_01.png");
			//tex3 = new Texture("data/road_01.png");


			Font = new BitmapFont();
			Font.LoadTTF(@"data/verdana.ttf", 10, FontStyle.Regular);


			Map = new Map();
			CircleRadius = 32;
		}




		/// <summary>
		/// Called when graphics resources need to be unloaded.
		/// </summary>
		public override void UnloadContent()
		{
			ResourceManager.ClearAssets();
		}




		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

			#region test
/*
			if (Keyboard.IsKeyPress(Keys.F1))
			{
				g_fContributionOfTex0 += 0.01f;
				if (g_fContributionOfTex0 > 1.0f)
					g_fContributionOfTex0 = 1.0f;

				// If the total contribution of textures 0 and 2 is
				// greater than 1.0f after we increased the 
				// contribution of texture 0, we need to reduce the 
				// contribution from texture 2 to balance it out.
				while ((g_fContributionOfTex0 + g_fContributionOfTex2) > 1.0f)
					g_fContributionOfTex2 -= 0.01f;

				if (g_fContributionOfTex2 < 0.0f)
					g_fContributionOfTex2 = 0.0f;
			}

			if (Keyboard.IsKeyPress(Keys.F2))
			{
				g_fContributionOfTex0 -= 0.01f;
				if (g_fContributionOfTex0 < 0.0f)
					g_fContributionOfTex0 = 0.0f;
			}


			if (Keyboard.IsKeyPress(Keys.F3))
			{
				g_fContributionOfTex2 += 0.01f;
				if (g_fContributionOfTex2 > 1.0f)
					g_fContributionOfTex2 = 1.0f;

				// If the total contribution of textures 0 and 2 is
				// greater than 1.0f after we increased the 
				// contribution of texture 2, we need to reduce the 
				// contribution from texture 0 to balance it out.
				while ((g_fContributionOfTex0 + g_fContributionOfTex2) > 1.0f)
					g_fContributionOfTex0 -= 0.01f;

				if (g_fContributionOfTex0 < 0.0f)
					g_fContributionOfTex0 = 0.0f;
			}

			if (Keyboard.IsKeyPress(Keys.F4))
			{
				g_fContributionOfTex2 -= 0.01f;
				if (g_fContributionOfTex2 < 0.0f)
					g_fContributionOfTex2 = 0.0f;
			}
*/
			#endregion

		}



		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		public override void Draw()
		{
			Display.ClearBuffers();

			Map.Draw();

			Display.DrawCircle(Mouse.Location, CircleRadius, Color.Red);

		}

		#region Test
		/*
		private void Visual_01()
		{
			GL.ActiveTexture(TextureUnit.Texture0);
			GL.Enable(EnableCap.Texture2D);
			Display.Texture = tex1;

			GL.ActiveTexture(TextureUnit.Texture1);
			GL.Enable(EnableCap.Texture2D);
			Display.Texture = tex2;

			//glTexEnvi(GL_TEXTURE_ENV, GL_TEXTURE_ENV_MODE, GL_COMBINE_ARB);
			//glTexEnvi(GL_TEXTURE_ENV, GL_COMBINE_RGB_ARB, GL_INTERPOLATE_ARB);
			GL.TexEnv(TextureEnvTarget.TextureEnv, TextureEnvParameter.TextureEnvMode, (int)TextureEnvMode.Combine);
			GL.TexEnv(TextureEnvTarget.TextureEnv, TextureEnvParameter.CombineRgb, (int)TextureEnvModeCombine.Interpolate);

			//glTexEnvi(GL_TEXTURE_ENV, GL_SOURCE0_RGB_ARB, GL_PREVIOUS_ARB);
			//glTexEnvi(GL_TEXTURE_ENV, GL_OPERAND0_RGB_ARB, GL_SRC_COLOR);
			GL.TexEnv(TextureEnvTarget.TextureEnv, TextureEnvParameter.Source0Rgb, (int)TextureEnvModeSource.Previous);
			GL.TexEnv(TextureEnvTarget.TextureEnv, TextureEnvParameter.Operand0Rgb, (int)TextureEnvModeOperandRgb.SrcColor);

			//glTexEnvi(GL_TEXTURE_ENV, GL_SOURCE1_RGB_ARB, GL_TEXTURE);
			//glTexEnvi(GL_TEXTURE_ENV, GL_OPERAND1_RGB_ARB, GL_SRC_COLOR);
			GL.TexEnv(TextureEnvTarget.TextureEnv, TextureEnvParameter.Src1Rgb, (int)TextureEnvModeSource.Texture);
			GL.TexEnv(TextureEnvTarget.TextureEnv, TextureEnvParameter.Operand1Rgb, (int)TextureEnvModeOperandRgb.SrcColor);

			//glTexEnvi(GL_TEXTURE_ENV, GL_SOURCE2_RGB_ARB, GL_PRIMARY_COLOR_ARB);
			//glTexEnvi(GL_TEXTURE_ENV, GL_OPERAND2_RGB_ARB, GL_SRC_ALPHA);
			GL.TexEnv(TextureEnvTarget.TextureEnv, TextureEnvParameter.Src2Rgb, (int)TextureEnvModeSource.PrimaryColor);
			GL.TexEnv(TextureEnvTarget.TextureEnv, TextureEnvParameter.Operand2Rgb, (int)TextureEnvModeOperandRgb.SrcAlpha);



			g_fContributionOfTex1 = 1.0f - (g_fContributionOfTex0 + g_fContributionOfTex2);

			// Do some bounds checking...
			if (g_fContributionOfTex1 < 0.0f)
				g_fContributionOfTex1 = 0.0f;
			if (g_fContributionOfTex1 > 1.0f)
				g_fContributionOfTex1 = 1.0f;

			float rgbValue = g_fContributionOfTex0 / (1.0f - g_fContributionOfTex2);
			float alphaValue = 1.0f - g_fContributionOfTex2;

			// Do some bounds checking...
			if (rgbValue < 0.0f)
				rgbValue = 0.0f;
			if (rgbValue > 1.0f)
				rgbValue = 1.0f;

			if (alphaValue < 0.0f)
				alphaValue = 0.0f;
			if (alphaValue > 1.0f)
				alphaValue = 1.0f;

			Display.Color = Color.FromArgb((int)(alphaValue * 255), (int)(rgbValue * 255), (int)(rgbValue * 255), (int)(rgbValue * 255));


			int x = 50;
			int y = 100;
			int width = 128 * 2;
			int height = 128 * 2;


			GL.Begin(BeginMode.Quads);
			GL.MultiTexCoord2(TextureUnit.Texture0, 0, 0);
			GL.MultiTexCoord2(TextureUnit.Texture1, 0, 0);
			GL.Vertex2(x, y);
			GL.MultiTexCoord2(TextureUnit.Texture0, 0, height);
			GL.MultiTexCoord2(TextureUnit.Texture1, 0, height);
			GL.Vertex2(x, y + height);
			GL.MultiTexCoord2(TextureUnit.Texture0, width, height);
			GL.MultiTexCoord2(TextureUnit.Texture1, width, height);
			GL.Vertex2(x + width, y + height);
			GL.MultiTexCoord2(TextureUnit.Texture0, width, 0);
			GL.MultiTexCoord2(TextureUnit.Texture1, width, 0);
			GL.Vertex2(x + width, y);
			GL.End();


			Display.TextureUnit = 1;
			Display.Texturing = false;


			Display.TextureUnit = 0;

			Font.DrawText(new Point(5, 15), Color.Yellow, "Contribution of each texture for blending:");
			Font.DrawText(new Point(5, 30), Color.White, string.Format("Contribution of Tex 0 = {0} (Change: F1/F2)", g_fContributionOfTex0));
			Font.DrawText(new Point(5, 45), Color.White, string.Format("Contribution of Tex 1 = {0} (Inferred by the values of Tex 0 + Tex 2)", g_fContributionOfTex1));
			Font.DrawText(new Point(5, 60), Color.White, string.Format("Contribution of Tex 2 = {0} (Change: F3/F4)", g_fContributionOfTex2));


			Font.DrawText(new Point(5, 355), Color.Yellow, "RGB values passed for interpolation of texture stage 1");
			Font.DrawText(new Point(5, 370), Color.White, string.Format("Red = {0}", rgbValue));
			Font.DrawText(new Point(5, 385), Color.White, string.Format("Green = {0}", rgbValue));
			Font.DrawText(new Point(5, 400), Color.White, string.Format("Blue = {0}", rgbValue));

			Font.DrawText(new Point(5, 415), Color.Yellow, "ALPHA value passed for interpolation of texture stage 2");
			Font.DrawText(new Point(5, 430), Color.White, string.Format("Alpha = {0}", alphaValue));

		}
*/
		#endregion



		#region Properties


		//Texture tex1;
		//Texture tex2;
		//Texture tex3;
		//float g_fContributionOfTex0 = 0.33f;
		//float g_fContributionOfTex1 = 0.33f;
		//float g_fContributionOfTex2 = 0.33f;

		/// <summary>
		/// 
		/// </summary>
		BitmapFont Font;


		/// <summary>
		/// 
		/// </summary>
		Map Map;


		int CircleRadius;



		#endregion
	}
}
