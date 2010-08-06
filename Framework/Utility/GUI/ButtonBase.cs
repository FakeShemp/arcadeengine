using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArcEngine.Utility.GUI
{
	/// <summary>
	/// Implements the basic functionality common to button controls. 
	/// </summary>
	public class ButtonBase : Control
	{
	}


	/// <summary>
	/// Specifies the appearance of a button
	/// </summary>
	[Flags]
	public enum ButtonState
	{
		/// <summary>
		/// All flags except Normal are set
		/// </summary>
		All = 0x4700,

		/// <summary>
		/// The button has a checked or latched appearance. Use this appearance to show that a toggle button has been pressed. 
		/// </summary>
		Checked = 0x400,

		/// <summary>
		/// The button has a flat, two-dimensional appearance. 
		/// </summary>
		Flat = 0x4000,

		/// <summary>
		/// The button is inactive (grayed). 
		/// </summary>
		Inactive = 0x100,

		/// <summary>
		/// The button has its normal appearance (three-dimensional). 
		/// </summary>
		Normal = 0,

		/// <summary>
		/// The button appears pressed.
		/// </summary>
		Pushed = 0x200
	}

}
