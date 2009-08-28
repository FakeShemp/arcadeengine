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
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using ArcEngine.Input;
using System.Windows.Forms;
using ArcEngine.Graphic;
using ArcEngine.Forms;

namespace ArcEngine
{
	/// <summary>
	/// 
	/// </summary>
	public static class Terminal
	{



		/// <summary>
		/// Initializes the console
		/// </summary>
		/// <returns></returns>
		static Terminal()
		{
			form = new TerminalForm();
		}



		#region Methods

/*
		/// <summary>
		/// Adds an event to the terminal
		/// </summary>
		/// <param name="e"></param>
		static public void Log(LogEventArgs e)
		{
			form.LogBox.Text += e.Message + Environment.NewLine;
			form.LogBox.ScrollToCaret();
		}
*/

		/// <summary>
		/// Adds a message to the terminal
		/// </summary>
		/// <param name="str"></param>
		static public void Log(string str)
		{
			form.LogBox.Text += str + Environment.NewLine;
			form.LogBox.ScrollToCaret();
		}


		/// <summary>
		/// Clear the terminal
		/// </summary>
		static public void Clear()
		{
			form.LogBox.Text = "";
		}

		#endregion


		#region Properties


		/// <summary>
		/// Gets/sets the size of the console
		/// </summary>
		public static Size Size
		{
			get
			{
				return size;
			}
			set
			{
				size = value;
			}
		}
		static Size size;


		/// <summary>
		/// Handle to the TerminalForm;
		/// </summary>
		static TerminalForm form = null;



		/// <summary>
		/// Shows/hides the console
		/// </summary>
		static public bool Visible
		{
			get
			{
				return visible;
			}
			set
			{
				visible = value;

				if (visible)
					form.Show();
				else
					form.Hide();
			}
		}
		static bool visible;



		/// <summary>
		/// Gets/sets the transparency of the console
		/// </summary>
		static public double Opacity
		{
			get
			{
				return form.Opacity;
			}
			set
			{
				form.Opacity = value;
			}
		}



		/// <summary>
		/// Enables/disables the CommandConsole
		/// </summary>
		static public bool Enable
		{
			get
			{
				return enable;
			}
			set
			{
				enable = value;
			}
		}
		static bool enable = false;
 
         


		/// <summary>
		/// Gets/sets the toggle comand key
		/// </summary>
		static public Keys ToggleKey
		{
			get
			{
				return toggleKey;
			}
			set
			{
				toggleKey = value;
			}
		}
		static Keys toggleKey = Keys.Tab;
 
         
 
         
         
		#endregion


		#region Events

		/// <summary>
		/// Process command
		/// </summary>
		/// <param name="str"></param>
		static internal void ProcessCommand(string str)
		{
			if (str == null || str == "")
				return;

			if (OnProcessCommand != null)
				OnProcessCommand(str);
		}

	
		/// <summary>
		///  Event fired when a command need to be processed
		/// </summary>
		public static event ProcessCommand OnProcessCommand;

		#endregion

	}
}
