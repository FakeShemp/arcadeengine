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
		public LogForm(EditorForm editor)
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


	
		#region Properties


		/// <summary>
		/// Editor form handle
		/// </summary>
		EditorForm Editor;

		#endregion
	}
}
