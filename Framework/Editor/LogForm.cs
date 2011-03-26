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
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace ArcEngine.Editor
{
	internal partial class LogForm : DockContent
	{

		/// <summary>
		/// Constructor
		/// </summary>
		public LogForm(EditorMainForm editor)
		{
			Editor = editor;

			InitializeComponent();

			Trace.OnTrace += new Trace.OnTraceEvent(Trace_OnTrace);
		}



		/// <summary>
		/// 
		/// </summary>
		/// <param name="message"></param>
		void Trace_OnTrace(string message)
		{
			LogBox.AppendText(message);
			LogBox.Select(LogBox.Text.Length, 0);
			LogBox.ScrollToCaret();
		}



		/// <summary>
		///  Clear the log
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Clear_Click(object sender, EventArgs e)
		{
			LogBox.Text = "";
		}



		/// <summary>
		/// Copy log to the clipboard
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void CopyToClipboard_Click(object sender, EventArgs e)
		{
			if (LogBox.Text != "")
				Clipboard.SetText(LogBox.Text);
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void LogForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			Trace.OnTrace -= new Trace.OnTraceEvent(Trace_OnTrace);
		}
	
		#region Properties


		/// <summary>
		/// Editor form handle
		/// </summary>
		EditorMainForm Editor;

		#endregion
	}
}
