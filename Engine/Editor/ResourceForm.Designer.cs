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
			this.ContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.RemoveMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.RemoveAllMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.MainImageList = new System.Windows.Forms.ImageList(this.components);
			this.ContextMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// ResourceTree
			// 
			this.ResourceTree.ContextMenuStrip = this.ContextMenu;
			this.ResourceTree.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ResourceTree.FullRowSelect = true;
			this.ResourceTree.HideSelection = false;
			this.ResourceTree.HotTracking = true;
			this.ResourceTree.ImageIndex = 0;
			this.ResourceTree.ImageList = this.MainImageList;
			this.ResourceTree.LabelEdit = true;
			this.ResourceTree.Location = new System.Drawing.Point(0, 0);
			this.ResourceTree.Name = "ResourceTree";
			this.ResourceTree.SelectedImageIndex = 0;
			this.ResourceTree.ShowRootLines = false;
			this.ResourceTree.Size = new System.Drawing.Size(187, 273);
			this.ResourceTree.TabIndex = 1;
			this.ResourceTree.TabStop = false;
			this.ResourceTree.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.ResourceTree_AfterLabelEdit);
			this.ResourceTree.DoubleClick += new System.EventHandler(this.OnTreeViewDoubleCick);
			this.ResourceTree.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnMouseUp);
			this.ResourceTree.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.ResourceTree_NodeMouseClick);
			// 
			// ContextMenu
			// 
			this.ContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RemoveMenuItem,
            this.RemoveAllMenuItem});
			this.ContextMenu.Name = "BankcontextMenu";
			this.ContextMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.ContextMenu.Size = new System.Drawing.Size(153, 70);
			this.ContextMenu.Text = "Resource";
			this.ContextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.ContextMenu_Opening);
			// 
			// RemoveMenuItem
			// 
			this.RemoveMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("RemoveMenuItem.Image")));
			this.RemoveMenuItem.Name = "RemoveMenuItem";
			this.RemoveMenuItem.Size = new System.Drawing.Size(152, 22);
			this.RemoveMenuItem.Text = "Remove";
			this.RemoveMenuItem.Click += new System.EventHandler(this.EraseMenu_Click);
			// 
			// RemoveAllMenuItem
			// 
			this.RemoveAllMenuItem.Name = "RemoveAllMenuItem";
			this.RemoveAllMenuItem.Size = new System.Drawing.Size(152, 22);
			this.RemoveAllMenuItem.Text = "Remove all";
			this.RemoveAllMenuItem.Click += new System.EventHandler(this.RemoveAllMenuItem_Click);
			// 
			// MainImageList
			// 
			this.MainImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("MainImageList.ImageStream")));
			this.MainImageList.TransparentColor = System.Drawing.Color.Transparent;
			this.MainImageList.Images.SetKeyName(0, "ZoomOut.png");
			this.MainImageList.Images.SetKeyName(1, "Add.png");
			this.MainImageList.Images.SetKeyName(2, "AlignToGrid.png");
			this.MainImageList.Images.SetKeyName(3, "delete.png");
			this.MainImageList.Images.SetKeyName(4, "Font.png");
			this.MainImageList.Images.SetKeyName(5, "Hand.png");
			this.MainImageList.Images.SetKeyName(6, "Level.png");
			this.MainImageList.Images.SetKeyName(7, "Model.png");
			this.MainImageList.Images.SetKeyName(8, "MoveDown.png");
			this.MainImageList.Images.SetKeyName(9, "MoveFirst.png");
			this.MainImageList.Images.SetKeyName(10, "MoveLast.png");
			this.MainImageList.Images.SetKeyName(11, "MoveNext.png");
			this.MainImageList.Images.SetKeyName(12, "MovePrevious.png");
			this.MainImageList.Images.SetKeyName(13, "MoveUp.png");
			this.MainImageList.Images.SetKeyName(14, "New.png");
			this.MainImageList.Images.SetKeyName(15, "Open.png");
			this.MainImageList.Images.SetKeyName(16, "PageDown.png");
			this.MainImageList.Images.SetKeyName(17, "PageUp.png");
			this.MainImageList.Images.SetKeyName(18, "Pause.png");
			this.MainImageList.Images.SetKeyName(19, "Play.png");
			this.MainImageList.Images.SetKeyName(20, "Repeat.png");
			this.MainImageList.Images.SetKeyName(21, "Ressource.png");
			this.MainImageList.Images.SetKeyName(22, "Ruler.png");
			this.MainImageList.Images.SetKeyName(23, "Save.png");
			this.MainImageList.Images.SetKeyName(24, "Script.png");
			this.MainImageList.Images.SetKeyName(25, "Sprite.png");
			this.MainImageList.Images.SetKeyName(26, "Stop.png");
			this.MainImageList.Images.SetKeyName(27, "Texture.png");
			this.MainImageList.Images.SetKeyName(28, "Tiles.png");
			this.MainImageList.Images.SetKeyName(29, "Zoom.png");
			this.MainImageList.Images.SetKeyName(30, "ZoomIn.png");
			this.MainImageList.Images.SetKeyName(31, "Library.png");
			this.MainImageList.Images.SetKeyName(32, "Audio.png");
			this.MainImageList.Images.SetKeyName(33, "Attachment.png");
			this.MainImageList.Images.SetKeyName(34, "FolderClosed.png");
			this.MainImageList.Images.SetKeyName(35, "FolderOpen.png");
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
			this.ContextMenu.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		public System.Windows.Forms.TreeView ResourceTree;
		private System.Windows.Forms.ContextMenuStrip ContextMenu;
		private System.Windows.Forms.ToolStripMenuItem RemoveMenuItem;
		private System.Windows.Forms.ImageList MainImageList;
		private System.Windows.Forms.ToolStripMenuItem RemoveAllMenuItem;
	}
}