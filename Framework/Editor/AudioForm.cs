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
using System.Xml;
using ArcEngine.Asset;
using ArcEngine.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace ArcEngine.Editor
{
	/// <summary>
	/// 
	/// </summary>
	internal partial class AudioForm : AssetEditor 
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public AudioForm(XmlNode node)
		{
			InitializeComponent();



			Sound = new AudioSample();
			Sound.Load(node);


			Build();
		}


		/// <summary>
		/// Rebuild the interface
		/// </summary>
		void Build()
		{

		}


		/// <summary>
		/// save the asset to the manager
		/// </summary>
		public override void Save()
		{
			StringBuilder sb = new StringBuilder();
			using (XmlWriter writer = XmlWriter.Create(sb))
				Sound.Save(writer);

			string xml = sb.ToString();
			XmlDocument doc = new XmlDocument();
			doc.LoadXml(xml);

			ResourceManager.AddAsset<ArcEngine.Asset.StringTable>(Sound.Name, doc.DocumentElement);
		}


		#region Events

		/// <summary>
		/// Closing form
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SoundForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			DialogResult result = MessageBox.Show("Save modifications ?", "ArcEngine Editor", MessageBoxButtons.YesNoCancel);

			
			
			if (result == DialogResult.Yes)
			{
				Save();
			}
			else if (result == DialogResult.Cancel)
			{
				e.Cancel = true;
			}

		}




		#endregion



		#region Properties


		/// <summary>
		/// 
		/// </summary>
		public override IAsset Asset
		{
			get
			{
				return Sound;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		AudioSample Sound;


		#endregion




	}
}
