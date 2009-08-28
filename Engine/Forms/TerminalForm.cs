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
using System.Windows.Forms;


namespace ArcEngine.Forms
{

	/// <summary>
	/// ProcessCommand delegate
	/// </summary>
	/// <param name="text">Command to process</param>
	public delegate void ProcessCommand(string text);

	/// <summary>
	/// 
	/// </summary>
	public partial class TerminalForm : Form
	{

		/// <summary>
		/// Constructor
		/// </summary>
		public TerminalForm()
		{
			InitializeComponent();

		}



		#region Events


		/// <summary>
		/// OnKeyPress
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void InputBox_KeyPress(object sender, KeyPressEventArgs e)
		{
			switch (e.KeyChar)
			{
			
				// Return
				case (char)13:
				{
					Terminal.ProcessCommand(InputBox.Text);
					InputBox.Text = "";
					e.Handled = true;
				}
				break;

				// Escape
				case (char)27:
				{
					Visible = false;
					e.Handled = true;
				}
				break;

			}

		}




		/// <summary>
		/// Clear the log
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ClearButton_Click(object sender, EventArgs e)
		{
			LogBox.Text = "";
			InputBox.Focus();
		}



		/// <summary>
		/// Copy the log to the Clipboard
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void CopyButton_Click(object sender, EventArgs e)
		{
			InputBox.Focus();

			if (LogBox.Text == null || LogBox.Text == "")
				return;

			Clipboard.SetText(LogBox.Text);
		}



		/// <summary>
		/// Hide the form
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void CloseButton_Click(object sender, EventArgs e)
		{
			Visible = false;
			InputBox.Focus();
		}

		/// <summary>
		/// Execute the command
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ExecuteButton_Click(object sender, EventArgs e)
		{
			Terminal.ProcessCommand(InputBox.Text);
			InputBox.Text = "";
			InputBox.Focus();
		}


		/// <summary>
		/// Close the terminal
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void LogBox_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == (char)27)
			{
				Visible = false;
				e.Handled = true;
			}
		}
		/// <summary>
		/// OnTextChanged
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void LogBox_TextChanged(object sender, EventArgs e)
		{
			LogBox.ScrollToCaret();
		}


		#endregion



	}
}
