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
using System.Net;
using System.Windows.Forms;
using ArcEngine.Network;


namespace Network
{
	public partial class MainForm : Form
	{
		/// <summary>
		/// 
		/// </summary>
		public MainForm()
		{
			InitializeComponent();

			Server = new NetworkManager();
			Server.OnLog += new NetworkManager.LogDelegate(Server_OnLog);
			Server.Listen(IPAddress.Any, 9050);
			UpdateTimer.Start();
		}



		/// <summary>
		/// 
		/// </summary>
		/// <param name="msg"></param>
		void Server_OnLog(string msg)
		{
			LogBox.Text += msg + Environment.NewLine;
		}




		#region Events

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void UpdateTimer_Tick(object sender, EventArgs e)
		{
			if (!Server.IsRunning)
				return;

			Server.Update();
		}




		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void StartServerBox_Click(object sender, EventArgs e)
		{
			Server.Listen(IPAddress.Any, 9050);
			UpdateTimer.Start();
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void StopServerBox_Click(object sender, EventArgs e)
		{
			UpdateTimer.Stop();
			Server.Shutdown();
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void CreateClientBox_Click(object sender, EventArgs e)
		{
			ClientForm form = new ClientForm();
			form.Show();
		}

	
		#endregion



		#region Properties

		/// <summary>
		/// 
		/// </summary>
		NetworkManager Server;

		#endregion
	}
}
