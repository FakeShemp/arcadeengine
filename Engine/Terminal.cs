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
