using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using ArcEngine.Asset;
using ArcEngine.Forms;
using WeifenLuo.WinFormsUI.Docking;


namespace ArcEngine.Editor
{
	internal partial class ResourceForm : DockContent
	{
		/// <summary>
		/// 
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
			RessourceTree.BeginUpdate();

			RessourceTree.Nodes.Clear();
			TreeNode bank = RessourceTree.Nodes.Add("Assets");

			// For each providers
			foreach (Providers.Provider provider in ResourceManager.Providers)
			{
				// for each registred asset
				foreach(Type type in provider.Assets)
				{
					TreeNode node = bank.Nodes.Add(type.Name);

					// Invoke the generic method like this : provider.GetAssets<[Asset Type]>();
					MethodInfo mi = provider.GetType().GetMethod("GetAssets").MakeGenericMethod(type);
					List<string> list = mi.Invoke(provider, null) as List<string>;

					foreach (string str in list)
					{
						TreeNode element = node.Nodes.Add(str);
						element.Tag = type;
					}
				}
			}

			// Binaries
			TreeNode sub = bank.Nodes.Add("Binaries");
			foreach (string name in ResourceManager.LoadedBinaries)
				sub.Nodes.Add(name);

			// Trie le tout
			RessourceTree.Sort();
			RessourceTree.EndUpdate();

			bank.Expand();
		}


		/// <summary>
		/// Collapse resource tree
		/// </summary>
		public void CollapseTree()
		{
			foreach (TreeNode node in RessourceTree.Nodes[0].Nodes)
			{
				node.Collapse();
			}
		}


		/// <summary>
		/// Expand resource tree 
		/// </summary>
		public void ExpandTree()
		{
			RessourceTree.ExpandAll();
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
				TreeNode node = RessourceTree.GetNodeAt(e.X, e.Y);
				BankContextMenu.Tag = node.Tag;

				BankContextMenu.Show(RessourceTree, new Point(e.X, e.Y));
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
			if (RessourceTree.SelectedNode == null)
				return;


			if (MessageBox.Show("Do you really want to erase \"" + RessourceTree.SelectedNode.Text + "\" ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
				return;

			// Get the provider
			Providers.Provider provider = ResourceManager.GetAssetProvider(RessourceTree.SelectedNode.Tag as Type);
			if (provider == null)
				return;
			
			
			object[] args = { RessourceTree.SelectedNode.Text };
			Type[] types = new Type[]{typeof(string)};
			MethodInfo mi = provider.GetType().GetMethod("Remove", types).MakeGenericMethod(RessourceTree.SelectedNode.Tag as Type);
			mi.Invoke(provider, args);


			RessourceTree.Nodes.Remove(RessourceTree.SelectedNode);
			RessourceTree.Update();

		}


		/// <summary>
		/// When double click on an element in the treeview then open a new window
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnTreeViewDoubleCick(object sender, EventArgs e)
		{
			// Not an editable node
			if (RessourceTree.SelectedNode == null)
				return;

			// Get the provider
			Providers.Provider provider = ResourceManager.GetAssetProvider(RessourceTree.SelectedNode.Tag as Type);
			if (provider == null)
				return;

			// Edit the asset
			object[] args = { RessourceTree.SelectedNode.Text };
			MethodInfo mi = provider.GetType().GetMethod("EditAsset").MakeGenericMethod(RessourceTree.SelectedNode.Tag as Type);
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
		private void RessourceTree_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
		{
			e.CancelEdit = true;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void RessourceTree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			RessourceTree.SelectedNode = e.Node;
		}

		#endregion



	}
}
