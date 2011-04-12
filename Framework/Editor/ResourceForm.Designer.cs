namespace ArcEngine.Editor
{
	partial class ResourceForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ResourceForm));
			this.ResourceTree = new System.Windows.Forms.TreeView();
			this.MainImageList = new System.Windows.Forms.ImageList(this.components);
			this.AssetContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.CloneMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.RenameMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.RemoveMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.TypeContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.ClearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.AddToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.AssetContextMenu.SuspendLayout();
			this.TypeContextMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// ResourceTree
			// 
			this.ResourceTree.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ResourceTree.FullRowSelect = true;
			this.ResourceTree.HideSelection = false;
			this.ResourceTree.HotTracking = true;
			this.ResourceTree.ImageIndex = 12;
			this.ResourceTree.ImageList = this.MainImageList;
			this.ResourceTree.LabelEdit = true;
			this.ResourceTree.Location = new System.Drawing.Point(0, 0);
			this.ResourceTree.Name = "ResourceTree";
			this.ResourceTree.SelectedImageIndex = 12;
			this.ResourceTree.ShowRootLines = false;
			this.ResourceTree.Size = new System.Drawing.Size(187, 273);
			this.ResourceTree.TabIndex = 1;
			this.ResourceTree.TabStop = false;
			this.ResourceTree.BeforeLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.ResourceTree_BeforeLabelEdit);
			this.ResourceTree.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.ResourceTree_AfterLabelEdit);
			this.ResourceTree.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.ResourceTree_NodeMouseClick);
			this.ResourceTree.DoubleClick += new System.EventHandler(this.OnTreeViewDoubleCick);
			this.ResourceTree.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ResourceTree_MouseClick);
			this.ResourceTree.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.ResourceTree_PreviewKeyDown);
			// 
			// MainImageList
			// 
			this.MainImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("MainImageList.ImageStream")));
			this.MainImageList.TransparentColor = System.Drawing.Color.Transparent;
			this.MainImageList.Images.SetKeyName(0, "AlignToGrid.png");
			this.MainImageList.Images.SetKeyName(1, "delete.png");
			this.MainImageList.Images.SetKeyName(2, "Font.png");
			this.MainImageList.Images.SetKeyName(3, "Hand.png");
			this.MainImageList.Images.SetKeyName(4, "New.png");
			this.MainImageList.Images.SetKeyName(5, "Ruler.png");
			this.MainImageList.Images.SetKeyName(6, "Script.png");
			this.MainImageList.Images.SetKeyName(7, "Texture.png");
			this.MainImageList.Images.SetKeyName(8, "");
			this.MainImageList.Images.SetKeyName(9, "");
			this.MainImageList.Images.SetKeyName(10, "");
			this.MainImageList.Images.SetKeyName(11, "");
			this.MainImageList.Images.SetKeyName(12, "");
			this.MainImageList.Images.SetKeyName(13, "");
			this.MainImageList.Images.SetKeyName(14, "");
			this.MainImageList.Images.SetKeyName(15, "");
			this.MainImageList.Images.SetKeyName(16, "");
			this.MainImageList.Images.SetKeyName(17, "");
			this.MainImageList.Images.SetKeyName(18, "");
			this.MainImageList.Images.SetKeyName(19, "");
			this.MainImageList.Images.SetKeyName(20, "");
			this.MainImageList.Images.SetKeyName(21, "");
			this.MainImageList.Images.SetKeyName(22, "");
			this.MainImageList.Images.SetKeyName(23, "");
			this.MainImageList.Images.SetKeyName(24, "");
			this.MainImageList.Images.SetKeyName(25, "");
			this.MainImageList.Images.SetKeyName(26, "");
			this.MainImageList.Images.SetKeyName(27, "");
			this.MainImageList.Images.SetKeyName(28, "");
			this.MainImageList.Images.SetKeyName(29, "");
			this.MainImageList.Images.SetKeyName(30, "");
			this.MainImageList.Images.SetKeyName(31, "");
			this.MainImageList.Images.SetKeyName(32, "");
			this.MainImageList.Images.SetKeyName(33, "");
			this.MainImageList.Images.SetKeyName(34, "");
			this.MainImageList.Images.SetKeyName(35, "");
			// 
			// AssetContextMenu
			// 
			this.AssetContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CloneMenuItem,
            this.RenameMenuItem,
            this.toolStripMenuItem1,
            this.RemoveMenuItem});
			this.AssetContextMenu.Name = "BankcontextMenu";
			this.AssetContextMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.AssetContextMenu.Size = new System.Drawing.Size(118, 76);
			this.AssetContextMenu.Text = "Resource";
			// 
			// CloneMenuItem
			// 
			this.CloneMenuItem.Name = "CloneMenuItem";
			this.CloneMenuItem.Size = new System.Drawing.Size(117, 22);
			this.CloneMenuItem.Text = "Clone";
			this.CloneMenuItem.Click += new System.EventHandler(this.CloneMenuItem_Click);
			// 
			// RenameMenuItem
			// 
			this.RenameMenuItem.Name = "RenameMenuItem";
			this.RenameMenuItem.Size = new System.Drawing.Size(117, 22);
			this.RenameMenuItem.Text = "Rename";
			this.RenameMenuItem.Click += new System.EventHandler(this.RenameMenuItem_Click);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(114, 6);
			// 
			// RemoveMenuItem
			// 
			this.RemoveMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("RemoveMenuItem.Image")));
			this.RemoveMenuItem.Name = "RemoveMenuItem";
			this.RemoveMenuItem.Size = new System.Drawing.Size(117, 22);
			this.RemoveMenuItem.Text = "Remove";
			this.RemoveMenuItem.Click += new System.EventHandler(this.EraseMenu_Click);
			// 
			// TypeContextMenu
			// 
			this.TypeContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddToolStripMenuItem,
            this.ClearToolStripMenuItem});
			this.TypeContextMenu.Name = "TypeContextMenu";
			this.TypeContextMenu.Size = new System.Drawing.Size(153, 70);
			// 
			// ClearToolStripMenuItem
			// 
			this.ClearToolStripMenuItem.Name = "ClearToolStripMenuItem";
			this.ClearToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.ClearToolStripMenuItem.Text = "Clear";
			this.ClearToolStripMenuItem.Click += new System.EventHandler(this.ClearToolStripMenuItem_Click);
			// 
			// AddToolStripMenuItem
			// 
			this.AddToolStripMenuItem.Name = "AddToolStripMenuItem";
			this.AddToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.AddToolStripMenuItem.Text = "Add...";
			this.AddToolStripMenuItem.Click += new System.EventHandler(this.AddToolStripMenuItem_Click);
			// 
			// ResourceForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(187, 273);
			this.Controls.Add(this.ResourceTree);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.HideOnClose = true;
			this.Name = "ResourceForm";
			this.ShowInTaskbar = false;
			this.TabText = "Assets :";
			this.Text = "Assets :";
			this.AssetContextMenu.ResumeLayout(false);
			this.TypeContextMenu.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		public System.Windows.Forms.TreeView ResourceTree;
		private System.Windows.Forms.ContextMenuStrip AssetContextMenu;
		private System.Windows.Forms.ToolStripMenuItem RemoveMenuItem;
		private System.Windows.Forms.ImageList MainImageList;
		private System.Windows.Forms.ToolStripMenuItem CloneMenuItem;
		private System.Windows.Forms.ToolStripMenuItem RenameMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ContextMenuStrip TypeContextMenu;
		private System.Windows.Forms.ToolStripMenuItem ClearToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem AddToolStripMenuItem;
	}
}