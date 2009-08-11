using System;
using System.Collections.Generic;
using System.ComponentModel;

using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ArcEngine.Input;


namespace ArcEngine.Forms
{
	/// <summary>
	/// 
	/// </summary>
	public partial class GameWindow : Form
	{
		/// <summary>
		/// 
		/// </summary>
		public GameWindow()
		{
			InitializeComponent();
		}



		#region Events



		/// <summary>
		/// On PreviewKeyDown
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void Form_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
		{
			Keyboard.KeyDown(e);
		}


		/// <summary>
		/// On KeyUp
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void Form_KeyUp(object sender, KeyEventArgs e)
		{
			Keyboard.KeyUp(e);
		}


		/// <summary>
		/// On MouseUp
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void Form_MouseUp(object sender, MouseEventArgs e)
		{

		}


		/// <summary>
		/// On MouseDoubleClick
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void Form_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			Mouse.DoubleClick(e);
		}


		/// <summary>
		/// On MouseDown
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void Form_MouseDown(object sender, MouseEventArgs e)
		{

		}


		/// <summary>
		/// On MouseMove
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void Form_MouseMove(object sender, MouseEventArgs e)
		{

		}



		#endregion

		#region Properties


		/// <summary>
		/// Gets / sets game window resizable
		/// </summary>
		public bool Resizable
		{
			get
			{
				return FormBorderStyle == FormBorderStyle.Sizable;
			}

			set
			{
				if (value)
					FormBorderStyle = FormBorderStyle.Sizable;
				else
					FormBorderStyle = FormBorderStyle.FixedDialog;
			}
		}

		#endregion
	}
}
