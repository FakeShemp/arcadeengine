using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ArcEngine.Controls.TimeLineControl
{

	/// <summary>
	/// 
	/// </summary>
	public partial class TimeLine : UserControl
	{
		/// <summary>
		/// 
		/// </summary>
		public TimeLine()
		{
			InitializeComponent();
		}



		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TimeLine_Paint(object sender, PaintEventArgs e)
		{
			Graphics graphic = e.Graphics;

			//base.OnPaint(e);
		}
	}
}
