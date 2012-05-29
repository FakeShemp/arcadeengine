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
using System.Xml;
using ArcEngine.Asset;
using ArcEngine.Audio;
using ArcEngine.Forms;
using WeifenLuo.WinFormsUI.Docking;
using ArcEngine.Interface;


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


			// Node icons
			NodeIcons = new Dictionary<Type, int>();
			NodeIcons.Add(typeof(TileSet), 2);
			NodeIcons.Add(typeof(StringTable), 4);
			NodeIcons.Add(typeof(AudioSample), 32);
			NodeIcons.Add(typeof(Script), 24);
		}



		/// <summary>
		/// Rebuilds the resource TreeView
		/// </summary>
		public void RebuildResourceTree()
		{
			ResourceTree.BeginUpdate();

			ResourceTree.Nodes.Clear();
			TreeNode bank = ResourceTree.Nodes.Add("Assets");
			bank.ImageIndex = 31;
			bank.SelectedImageIndex = 31;


			// for each registred asset
			foreach (RegisteredAsset ra in ResourceManager.RegisteredAssets)
			{
				// Get the number of asset
				if (ra.Count == 0)
					continue;

				TreeNode node = bank.Nodes.Add(ra.Tag + " (" + ra.Count.ToString() + ")");
				node.Tag = ra;
				node.ImageIndex = 34;
				node.SelectedImageIndex = 35;

				List<string> list = new List<string>();
				foreach (string str in ra.GetList(list))
				{
					AddNode(node, str, ra.Type);
				}
			}

			// Trie le tout
			ResourceTree.Sort();


			// Binaries
			//TreeNode sub = ResourceTree.Nodes.Insert(0, "Binaries (" + ResourceManager.Binaries.Count + ")");

			ResourceTree.EndUpdate();

			bank.Expand();

		}


		/// <summary>
		/// Adds a node
		/// </summary>
		/// <param name="parent">Parent node</param>
		/// <param name="text">Text to display</param>
		/// <param name="type">Type of the asset</param>
		private void AddNode(TreeNode parent, string text, Type type)
		{
			TreeNode element = parent.Nodes.Add(text);
			element.Tag = type;

			int imgindex = 14;
			if (NodeIcons.ContainsKey(type))
				imgindex = NodeIcons[type];

			element.ImageIndex = imgindex;
			element.SelectedImageIndex = imgindex;
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


		/// <summary>
		/// Edit an asset
		/// </summary>
		/// <param name="node">Tree node</param>
		/// <returns>True if asset is editable</returns>
		private bool EditAsset(TreeNode node)
		{
			// Not an editable node
			if (node == null || node.Nodes.Count > 0)
				return false;

			// Don not edit binaires
			if (node.Text.StartsWith("Binaries"))
			{
				new BinaryForm().Show(DockPanel, DockState.Document);
				return true;
			}

			// Asset not editable
			if (node.Tag == null)
				return false;

			// Get the asset definition
			RegisteredAsset ra = ResourceManager.GetRegisteredByType(node.Tag as Type);
			if (ra == null)
				return false;

			// Get the editor
			AssetEditorBase form = ra.Edit(node.Text);
			if (form == null)
				return false;

			form.Show(DockPanel, DockState.Document);

			return true;
		}


		/// <summary>
		/// Removes an asset
		/// </summary>
		/// <param name="node">Node handle</param>
		/// <returns></returns>
		private bool RemoveAsset(TreeNode node)
		{
			// Not an editable node
			if (node == null || node.Tag == null)
				return false;


			if (node.Nodes.Count == 0)
			{
				if (MessageBox.Show("Do you really want to erase \"" + node.Text + "\" ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
					return false;

				// Get the asset definition
				RegisteredAsset ra = ResourceManager.GetRegisteredByType(node.Tag as Type);
				if (ra == null)
					return false;

				ra.Remove(ResourceTree.SelectedNode.Text);
			}

			else if (node.Nodes.Count >= 0)
			{

				string[] names = (node.Tag.ToString().Split('.'));
				if (MessageBox.Show("Do you really want to erase all \"" + names[names.Length - 1] + "\" ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
					return false;

				RegisteredAsset ra = ResourceManager.GetRegisteredByType(node.Tag as Type);
				if (ra == null)
					return false;

				ra.Clear();
			}


			RemoveNode(node);
			return true;
		}


		/// <summary>
		/// Removes a node
		/// </summary>
		/// <param name="node">Node handle</param>
		private void RemoveNode(TreeNode node)
		{
			if (node == null)
				return;

			TreeNode parent = node.Parent;

			// If children present, the rename the parent
			if (node.Nodes.Count == 0)
			{
				string[] val = node.Parent.Text.Split(new char[] { '(', ')' });
				int count = int.Parse(val[1]) - 1;
				parent.Text = (node.Tag as Type).Name + " (" + count + ")";

			}

			ResourceTree.Nodes.Remove(node);

			// If there is no child, then remove the parent node too
			if (parent.Nodes.Count == 0)
				ResourceTree.Nodes.Remove(parent);

		}



		#region Events


		/// <summary>
		/// OnKeyPreview
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ResourceTree_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
		{
			TreeNode node = ResourceTree.SelectedNode;
			if (node == null)
				return;

			// Delete
			if (e.KeyCode == Keys.Delete)
				RemoveAsset(node);

			// Rename
			if (e.KeyCode == Keys.F2)
				node.BeginEdit();

			// Edit
			if (e.KeyCode == Keys.Enter && !node.IsEditing)
				EditAsset(node);
		}


		/// <summary>
		/// Removes an asset
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void EraseMenu_Click(object sender, EventArgs e)
		{
			RemoveAsset(ResourceTree.SelectedNode);
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void CloneMenuItem_Click(object sender, EventArgs e)
		{
			// Not an editable node
			TreeNode node = ResourceTree.SelectedNode;
			if (node == null || node.Tag == null)
				return;


			// Get the original asset
			MethodInfo me = typeof(ResourceManager).GetMethod("GetAsset", new Type[] { typeof(string) });
			MethodInfo mi = me.MakeGenericMethod(node.Tag as Type);
			XmlNode xml = mi.Invoke(null, new object[] { node.Text }) as XmlNode;
			if (xml == null)
				return;
			xml = xml.Clone();
			xml.Attributes["name"].Value = node.Text + "_1";



			// Adds the new asset
			me = typeof(ResourceManager).GetMethod("AddAsset", new Type[] { typeof(string), typeof(XmlNode) });
			mi = me.MakeGenericMethod(node.Tag as Type);
			mi.Invoke(null, new object[] { xml.Attributes["name"].Value, xml });

			// Adds the node
			AddNode(node.Parent, xml.Attributes["name"].Value, node.Tag as Type);
		}


		/// <summary>
		/// Changing the name of an asset
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ResourceTree_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
		{
			TreeNode node = e.Node;
			if (node == null || node.Tag == null || e.Label == null)
			{
				e.CancelEdit = true;
				return;
			}

			// Can't edit binaries
			if (node.Text.StartsWith("Binaries"))
			{
				e.CancelEdit = true;
				return;
			}


			// Get the original asset
			MethodInfo me = typeof(ResourceManager).GetMethod("GetAsset", new Type[] { typeof(string) });
			MethodInfo mi = me.MakeGenericMethod(node.Tag as Type);
			XmlNode xml = mi.Invoke(null, new object[] { node.Text }) as XmlNode;
			if (xml == null)
				return;
			xml.Attributes["name"].Value = e.Label;



			// Adds the new asset
			me = typeof(ResourceManager).GetMethod("AddAsset", new Type[] { typeof(string), typeof(XmlNode) });
			mi = me.MakeGenericMethod(node.Tag as Type);
			mi.Invoke(null, new object[] { e.Label, xml });


			// Remove the old asset
			me = typeof(ResourceManager).GetMethod("RemoveAsset", new Type[] { typeof(string) });
			mi = me.MakeGenericMethod(node.Tag as Type);
			mi.Invoke(null, new object[] { node.Text });

		}


		/// <summary>
		/// When double click on an element in the treeview then opens up a new window
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnTreeViewDoubleCick(object sender, EventArgs e)
		{
			EditAsset(ResourceTree.SelectedNode);
		}


		/// <summary>
		/// Renames an asset
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void RenameMenuItem_Click(object sender, EventArgs e)
		{
			TreeNode node = ResourceTree.SelectedNode;

			if (node.Nodes.Count == 0)
				node.BeginEdit();

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


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ResourceTree_BeforeLabelEdit(object sender, NodeLabelEditEventArgs e)
		{
			if (e.Node.Nodes.Count != 0)
			{
				e.CancelEdit = true;
				return;
			}

		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ResourceTree_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				Point location = PointToScreen(e.Location);
				TreeNode node = ResourceTree.GetNodeAt(e.Location);
				if (node == null)
					return;

				// Root node
				if (node.Parent == null)
				{

				}
				// Asset type node
				else if (node.GetNodeCount(true) > 0)
				{
					TypeContextMenu.Tag = node.Tag;
					TypeContextMenu.Show(location);
				}

				else
				{
					AssetContextMenu.Tag = node.Tag;
					AssetContextMenu.Show(location);
				}

			}
		}



		#endregion


		#region Type context menu

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void AddToolStripMenuItem_Click(object sender, EventArgs e)
		{
			RegisteredAsset ra = TypeContextMenu.Tag as RegisteredAsset;

			new WizardForm(DockPanel, ra.Tag).ShowDialog();
			RebuildResourceTree();
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ClearToolStripMenuItem_Click(object sender, EventArgs e)
		{

		}

		#endregion


		#region Properties

		/// <summary>
		/// Node icons
		/// </summary>
		Dictionary<Type, int> NodeIcons;

		#endregion


	}
}
