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

using ArcEngine.Network;

namespace Network
{
	public partial class ClientForm : Form
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public ClientForm()
		{
			InitializeComponent();


			Manager = new NetworkManager();
			Manager.Connect("localhost", 9050);


			Packet = new NetPacket();

			UpdateTimer.Start();
		}



		#region Events

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SendMsgBox_Click(object sender, EventArgs e)
		{
			Packet.Reset();
			Packet.Write(MsgBox.Text);
			MsgBox.Text = string.Empty;
		}

		#endregion



		#region Events


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void timer1_Tick(object sender, EventArgs e)
		{
			Manager.Update();
		}



		/// <summary>
		/// form closing
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ClientForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			UpdateTimer.Stop();

			if (Manager != null)
				Manager.Shutdown();

		}

		#endregion



		#region Properties

		/// <summary>
		/// Network manager
		/// </summary>
		NetworkManager Manager;


		/// <summary>
		/// Network packet
		/// </summary>
		NetPacket Packet;

		#endregion

	}
}
