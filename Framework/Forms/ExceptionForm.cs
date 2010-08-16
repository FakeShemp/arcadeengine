using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ArcEngine.Forms
{
	public partial class ExceptionForm : Form
	{

		/// <summary>
		/// 
		/// </summary>
		public ExceptionForm(Exception e)
		{
			InitializeComponent();
			E = e;

			ErrorBox.Text = E.Message;
			TraceBox.Text = E.StackTrace;
		}



		/// <summary>
		/// Send report
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SendReportBox_Click(object sender, EventArgs e)
		{
			string filename = Trace.FileName;

			string log = "";

			Trace.Close();
			System.IO.StreamReader stream = new System.IO.StreamReader(filename);
			if (stream != null)
			{
				log = stream.ReadToEnd();
				stream.Dispose();

				log = log.Replace(Environment.NewLine, "%0A");
			}


			System.Diagnostics.Process.Start("mailto:bugreport@mimicprod.net?subject=ArcEngine bug report&body=" + log);

		}




		#region Properties


		Exception E;

		#endregion
	}
}
