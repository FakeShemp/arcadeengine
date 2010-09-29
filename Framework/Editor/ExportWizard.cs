#region Licence
//
//This file is part of ArcEngine.
//Copyright (C)2008-2010 Adrien Hémery ( iliak@mimicprod.net )
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
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using ArcEngine.Asset;
using System.IO;



namespace ArcEngine.Editor
{
	/// <summary>
	/// Export assets
	/// </summary>
	public partial class ExportWizard : Form
	{
		/// <summary>
		/// 
		/// </summary>
		public ExportWizard()
		{
			InitializeComponent();

			TreeNode bank = TreeviewBox.Nodes.Add("Assets");
			bank.Expand();

			TreeviewBox.BeginUpdate();
			// For each providers
			foreach (Provider provider in ResourceManager.Providers)
			{
				// for each registred asset
				foreach (Type type in provider.Assets)
				{
					// Get the number of asset
					MethodInfo mi = provider.GetType().GetMethod("Count").MakeGenericMethod(type);
					int count = (int)mi.Invoke(provider, null);
					if (count == 0)
						continue;

					TreeNode node = bank.Nodes.Add(type.Name + " (" + count.ToString() + ")");
					node.Tag = type;

					// Invoke the generic method like this : provider.GetAssets<[Asset Type]>();
					mi = provider.GetType().GetMethod("GetAssets").MakeGenericMethod(type);
					List<string> list = mi.Invoke(provider, null) as List<string>;

					foreach (string str in list)
					{
						TreeNode element = node.Nodes.Add(str);
						element.Tag = type;
					}
				}
			}

			TreeNode bins = TreeviewBox.Nodes.Insert(0, "Binaries");
			bins.Tag = null;
			foreach (string name in ResourceManager.Binaries)
				bins.Nodes.Add(name);
			bins.Checked = true;

			TreeviewBox.Sort();
			TreeviewBox.EndUpdate();

			TreeviewBox.Nodes[0].Checked = true;
		}


		/// <summary>
		/// Export selected assets
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ExportBox_Click(object sender, EventArgs e)
		{
			Close();

			XmlWriter doc = null;
			MemoryStream ms = null;

			XmlWriterSettings settings = new XmlWriterSettings();
			settings.Indent = true;
			settings.OmitXmlDeclaration = false;
			settings.IndentChars = "\t";
			settings.Encoding = ASCIIEncoding.ASCII;

	
			if (ToBankBox.Checked)
			{
				// Export to a bank
				if (SaveFileBox.ShowDialog() != DialogResult.OK)
					return;



			}
			else
			{
				// Export to a folder
				if (FolderBox.ShowDialog() != DialogResult.OK)
					return;

				// For each node
				foreach (TreeNode node in TreeviewBox.Nodes[0].Nodes)
				{
					// Number of asset saved
					int assetcount = 0;

					ms = new MemoryStream();
					doc = XmlWriter.Create(ms, settings);
					doc.WriteStartDocument(true);
					doc.WriteStartElement("bank");



					// For each subnode checked
					foreach (TreeNode subnode in node.Nodes)
					{
						if (!subnode.Checked)
							continue;


						// Invoke the generic method like this : provider.Save<[Asset Type]>(XmlNode node);
						object[] args = { doc };
						Type[] types = new Type[] { typeof(XmlWriter) };
					//	mi = provider.GetType().GetMethod("Save", types).MakeGenericMethod(type);
					//	mi.Invoke(provider, args);

						
						ResourceManager.GetAsset<BitmapFont>(subnode.Text);

						assetcount++;
					}

					// No asset saved
					if (assetcount == 0)
						continue;


					doc.WriteEndElement();
					doc.WriteEndDocument();
					doc.Flush();
					//SaveResource(zip, type.Name + ".xml", ms.ToArray());
					

				}
			}


		}


		#region Events

		/// <summary>
		/// Checks or unchecks all subnodes
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TreeviewBox_AfterCheck(object sender, TreeViewEventArgs e)
		{
			foreach (TreeNode node in e.Node.Nodes)
			{
				node.Checked = e.Node.Checked;
			}
		}


		#endregion
	}
}
