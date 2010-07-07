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
using System.Drawing;
using System.Net;
using System.Windows.Forms;
using ArcEngine;
using ArcEngine.Graphic;
using ArcEngine.Network;


namespace ArcEngine.Examples.Network
{
	public static class NetworkGame
	{
		/// <summary>
		/// Point d'entrée principal de l'application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new ServerForm());
		}

/*
		/// <summary>
		/// 
		/// </summary>
		public NetworkGame()
		{
			ServerConfig config = new ServerConfig(IPAddress.Any, 9050, "Bradock's paradise !", 4);
			Server = new NetworkManager();
			Server.Server(config);

			Form = new ServerForm(this);
			Form.Show();
		}



		/// <summary>
		/// 
		/// </summary>
		public  void LoadContent()
		{
			GameWindowParams param = new GameWindowParams();
			param.Samples = 0;
			param.Size = new Size(200, 100);
			CreateGameWindow(param);

			Window.Resizable = true;
			Window.Text = "Network server";
			Window.Show();
		}



		/// <summary>
		/// 
		/// </summary>
		/// <param name="time"></param>
		public override void Update(GameTime time)
		{
			Window.Hide();


			Server.Update(time.ElapsedRealTime);

		}




		#region Properties

		/// <summary>
		/// Network manager
		/// </summary>
		public NetworkManager Server
		{
			get;
			private set;
		}



		/// <summary>
		/// Main form
		/// </summary>
		ServerForm Form;


		#endregion
*/
	}
}
