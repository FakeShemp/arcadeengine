using System;
using System.Collections.Generic;
using System.Drawing;
using ArcEngine.Graphic;
using ArcEngine;
using ArcEngine.Asset;


namespace DungeonEye.Gui
{
	/// <summary>
	/// Gui buttons
	/// </summary>
	public class ScreenButton
	{

		/// <summary>
		/// 
		/// </summary>
		/// <param name="text"></param>
		public ScreenButton(string text, Rectangle rectangle)
		{
			Text = text;
			Rectangle = rectangle;
		}





		#region Events


		/// <summary>
		/// Event raised when the menu is selected.
		/// </summary>
		public event EventHandler Selected;


		/// <summary>
		/// Method for raising the Selected event.
		/// </summary>
		public virtual void OnSelectEntry()
		{
			if (Selected != null)
				Selected(this, null);
		}


		#endregion



		#region Properties

		/// <summary>
		/// Text of the button
		/// </summary>
		public string Text;


		/// <summary>
		/// Rectangle
		/// </summary>
		public Rectangle Rectangle;


		/// <summary>
		/// Color of the text
		/// </summary>
		public Color TextColor;


		#endregion
	}
}
