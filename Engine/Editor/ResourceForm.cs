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
using System.Reflection;
using System.Windows.Forms;
using ArcEngine.Providers;
using ArcEngine.Asset;
using ArcEngine.Forms;
using WeifenLuo.WinFormsUI.Docking;


namespace ArcEngine.Editor
{
	/// <summary>
	/// 
	/// </summary>
	internal partial class ResourceForm : DockContent
	{
		/// <summary>
		/// Cosntructor
		/// </summary>
		public ResourceForm()
		{
			InitializeComponent();
		}



		/// <summary>
		/// Rebuilds the resource TreeView
		/// </summary>
		public void RebuildResourceTree()
		{
			ResourceTree.BeginUpdate();

			ResourceTree.Nodes.Clear();
			TreeNode bank = ResourceTree.Nodes.Add("Assets");

			// For each providers
			foreach (Providers.Provider provider in ResourceManager.Providers)
			{
				// for each registred asset
				foreach(Type type in provider.Assets)
				{
					// Get the number of asset
					MethodInfo mi = provider.GetType().GetMethod("Count").MakeGenericMethod(type);
					int count = (int) mi.Invoke(provider, null);
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

			// Trie le tout
			ResourceTree.Sort();


			// Binaries
			TreeNode sub = bank.Nodes.Insert(0, "Binaries (" + ResourceManager.LoadedBinaries.Count + ")");
			sub.Tag = null;

			ResourceTree.EndUpdate();

			bank.Expand();
		}


		/// <summary>
		/// Collapse resource tree
		/// </summary>
		public void CollapseTree()
		{
			foreach (TreeNode node in ResourceTree.Nodes[0].Nodes)
			{
				node.Collapse();
			}
		}


		/// <summary>
		/// Expand resource tree 
		/// </summary>
		public void ExpandTree()
		{
			ResourceTree.ExpandAll();
		}




		#region Events

		/// <summary>
		/// OnMouseUp ContextMenu
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnMouseUp(object sender, MouseEventArgs e)
		{

			if (e.Button == MouseButtons.Right)
			{
				TreeNode node = ResourceTree.GetNodeAt(e.X, e.Y);
				ContextMenu.Tag = node.Tag;

				ContextMenu.Show(ResourceTree, new Point(e.X, e.Y));
			}
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void EraseMenu_Click(object sender, EventArgs e)
		{
			// Not an editable node
			if (ResourceTree.SelectedNode == null)
				return;


			if (MessageBox.Show("Do you really want to erase \"" + ResourceTree.SelectedNode.Text + "\" ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
				return;

			// Get the provider
			Providers.Provider provider = ResourceManager.GetAssetProvider(ResourceTree.SelectedNode.Tag as Type);
			if (provider != null)
			{


				object[] args = { ResourceTree.SelectedNode.Text };
				Type[] types = new Type[] { typeof(string) };
				MethodInfo mi = provider.GetType().GetMethod("Remove", types).MakeGenericMethod(ResourceTree.SelectedNode.Tag as Type);
				mi.Invoke(provider, args);

			}
			else
			{

				return;
			}

			ResourceTree.Nodes.Remove(ResourceTree.SelectedNode);
		//	ResourceTree.Update();

		}



		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void RemoveAllMenuItem_Click(object sender, EventArgs e)
		{
			// Not an editable node
			if (ResourceTree.SelectedNode == null)
				return;


			string[] names = (ResourceTree.SelectedNode.Tag.ToString().Split('.'));
			if (MessageBox.Show("Do you really want to erase all \"" + names[names.Length-1] + "\" ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
				return;

			Provider provider = ResourceManager.GetAssetProvider(ResourceTree.SelectedNode.Tag as Type);
			if (provider == null)
				return;

			Type type = provider.GetType();
			MethodInfo mi = type.GetMethod("Remove", new Type[] { });

			mi = mi.MakeGenericMethod(ResourceTree.SelectedNode.Tag as Type);
			mi.Invoke(provider, new object[] { });


			ResourceTree.Nodes.Remove(ResourceTree.SelectedNode);
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ContextMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (ResourceTree.SelectedNode.Nodes.Count == 0)
			{
				RemoveMenuItem.Enabled = true;
				RemoveAllMenuItem.Enabled = false;
			}
			else if (ResourceTree.SelectedNode.Nodes.Count > 0)
			{
				RemoveMenuItem.Enabled = false;
				RemoveAllMenuItem.Enabled = true;
			}

			if (ResourceTree.SelectedNode.Tag == null)
			{
				RemoveMenuItem.Enabled = false;
				RemoveAllMenuItem.Enabled = false;
			}
		}



		/// <summary>
		/// When double click on an element in the treeview then open a new window
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnTreeViewDoubleCick(object sender, EventArgs e)
		{
			// Not an editable node
			if (ResourceTree.SelectedNode == null)
				return;

			if (ResourceTree.SelectedNode.Text == "Binaries")
			{
				new BinaryForm().Show(DockPanel, DockState.Document);
			}
			

			// Get the provider
			Providers.Provider provider = ResourceManager.GetAssetProvider(ResourceTree.SelectedNode.Tag as Type);
			if (provider == null)
				return;

			// Edit the asset
			object[] args = { ResourceTree.SelectedNode.Text };
			MethodInfo mi = provider.GetType().GetMethod("EditAsset").MakeGenericMethod(ResourceTree.SelectedNode.Tag as Type);
			AssetEditor form = mi.Invoke(provider, args) as AssetEditor;
			if (form == null) 
				return;

			form.Show(DockPanel, DockState.Document);
		}


		/// <summary>
		/// Changing the name of an asset
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ResourceTree_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
		{
			e.CancelEdit = true;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ResourceTree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			ResourceTree.SelectedNode = e.Node;
		}

		#endregion


	}
}
