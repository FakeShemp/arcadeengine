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
using ArcEngine;

namespace ArcEngine.Examples.Network
{
	public partial class ServerForm : Form
	{
		/// <summary>
		/// 
		/// </summary>
		public ServerForm()
		{
			InitializeComponent();


			ServerConfig config = new ServerConfig(IPAddress.Any, 9050, "Bradock's paradise !", 4);
			Server = new NetServer();
			Server.Server(config);
			Server.OnPlayerJoin += new OnPlayerJoinHandler(Server_OnClientConnect);


			LastUpdate = DateTime.Now;
			UpdateTimer.Start();
		}



		/// <summary>
		/// 
		/// </summary>
		/// <param name="client"></param>
		/// <returns></returns>
		bool Server_OnClientConnect(NetPlayer client)
		{
			LogBox.Text += "New client connected" + Environment.NewLine;


			return true;
		}





		#region Events


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void CreateClientBox_Click(object sender, EventArgs e)
		{
			new ClientForm().Show();
		}

	


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void UpdateTimer_Tick(object sender, EventArgs e)
		{
			TimeSpan elapsed = DateTime.Now - LastUpdate;
			LastUpdate = DateTime.Now;

			Server.Update(elapsed);

		}

		#endregion


		#region Properties

		/// <summary>
		/// Network manager
		/// </summary>
		public NetServer Server
		{
			get;
			private set;
		}



		/// <summary>
		/// Last update
		/// </summary>
		DateTime LastUpdate;

		#endregion
	}
}
