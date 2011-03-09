using System;
using System.Collections.Generic;
using System.Text;
using ArcEngine;
using ArcEngine.Graphic;
using System.Drawing;


namespace ArcEngine.Examples.Component
{
	/// <summary>
	/// Fade the screen to a specified color
	/// </summary>
	public class ScreenFader : GameComponent
	{
		/// <summary>
		/// Init
		/// </summary>
		public override void Initialize()
		{
			
		}


		/// <summary>
		/// Update every frame
		/// </summary>
		/// <param name="time">Elapsed game time</param>
		public override void Update(GameTime time)
		{
			// nothing to update
			if (CurrentStep > Speed)
				return;

			// Update current timer
			CurrentStep += time.ElapsedGameTime;
		}


		/// <summary>
		/// Draws the component
		/// </summary>
		public override void Draw()
		{
			// Compute the alpha value
			int alpha = (int) ((CurrentStep.TotalMilliseconds / Speed.TotalMilliseconds) * 255);

			// Get the fade color
			Color color = Color.FromArgb(alpha, FadeColor);

			// Fade the full screen
			//Display.FillRectangle(Display.ViewPort, color);
		}



		/// <summary>
		/// Starts the effect
		/// </summary>
		public void Start()
		{
			CurrentStep = TimeSpan.Zero;
		}



		/// <summary>
		/// Stops the effect
		/// </summary>
		public void Stop()
		{
			CurrentStep = TimeSpan.MaxValue;
		}



		#region Properties

		/// <summary>
		/// Fade speed
		/// </summary>
		public TimeSpan Speed;


		/// <summary>
		/// Current step
		/// </summary>
		TimeSpan CurrentStep;


		/// <summary>
		/// Fading color
		/// </summary>
		public Color FadeColor;

		#endregion

	}
}
