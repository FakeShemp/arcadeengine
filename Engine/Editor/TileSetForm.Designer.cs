namespace ArcEngine.Editor
{
	partial class TileSetForm
	{
		/// <summary> 
		/// Variable nécessaire au concepteur.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Nettoyage des ressources utilisées.
		/// </summary>
		/// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Code généré par le Concepteur de composants

		/// <summary> 
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TileSetForm));
			this.GLTextureControl = new OpenTK.GLControl();
			this.TileGroupBox = new System.Windows.Forms.GroupBox();
			this.CollisionButton2 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
			this.TilePropertyGrid = new System.Windows.Forms.PropertyGrid();
			this.TileToolStrip = new System.Windows.Forms.ToolStrip();
			this.AddTileButton = new System.Windows.Forms.ToolStripButton();
			this.TilesBox = new System.Windows.Forms.ToolStripComboBox();
			this.EraseTileButton = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			this.HotSpotButton = new System.Windows.Forms.ToolStripButton();
			this.CollisionButton = new System.Windows.Forms.ToolStripButton();
			this.GLTileControl = new OpenTK.GLControl();
			this.TileSetGroupBox = new System.Windows.Forms.GroupBox();
			this.TileSetPropertyGrid = new System.Windows.Forms.PropertyGrid();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.VertScroller = new System.Windows.Forms.VScrollBar();
			this.HScroller = new System.Windows.Forms.HScrollBar();
			this.TextureToolStrip = new System.Windows.Forms.ToolStrip();
			this.ZoomOutButton = new System.Windows.Forms.ToolStripButton();
			this.ZoomBox = new System.Windows.Forms.ToolStripComboBox();
			this.ZoomInButton = new System.Windows.Forms.ToolStripButton();
			this.ActualSizeButton = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.PositionLabel = new System.Windows.Forms.ToolStripLabel();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.SizeLabel = new System.Windows.Forms.ToolStripLabel();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.BgColorButton = new System.Windows.Forms.ToolStripButton();
			this.RenderTimer = new System.Windows.Forms.Timer(this.components);
			this.TexturesBox = new System.Windows.Forms.ToolStripComboBox();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
			this.TileGroupBox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
			this.TileToolStrip.SuspendLayout();
			this.TileSetGroupBox.SuspendLayout();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.TextureToolStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// GLTextureControl
			// 
			this.GLTextureControl.BackColor = System.Drawing.Color.Black;
			this.GLTextureControl.Cursor = System.Windows.Forms.Cursors.Cross;
			this.GLTextureControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.GLTextureControl.Location = new System.Drawing.Point(200, 25);
			this.GLTextureControl.Name = "GLTextureControl";
			this.GLTextureControl.Size = new System.Drawing.Size(523, 258);
			this.GLTextureControl.TabIndex = 1;
			this.GLTextureControl.VSync = false;
			this.GLTextureControl.Paint += new System.Windows.Forms.PaintEventHandler(this.GLTextureControl_Paint);
			this.GLTextureControl.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.OnPreviewKeyDown);
			this.GLTextureControl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.GLTextureControl_MouseMove);
			this.GLTextureControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GLTextureControl_MouseDown);
			this.GLTextureControl.Resize += new System.EventHandler(this.GLTextureControl_Resize);
			this.GLTextureControl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.GLTextureControl_MouseUp);
			// 
			// TileGroupBox
			// 
			this.TileGroupBox.Controls.Add(this.CollisionButton2);
			this.TileGroupBox.Controls.Add(this.button2);
			this.TileGroupBox.Controls.Add(this.button1);
			this.TileGroupBox.Controls.Add(this.numericUpDown1);
			this.TileGroupBox.Controls.Add(this.TilePropertyGrid);
			this.TileGroupBox.Controls.Add(this.TileToolStrip);
			this.TileGroupBox.Dock = System.Windows.Forms.DockStyle.Left;
			this.TileGroupBox.Location = new System.Drawing.Point(0, 0);
			this.TileGroupBox.Name = "TileGroupBox";
			this.TileGroupBox.Size = new System.Drawing.Size(200, 220);
			this.TileGroupBox.TabIndex = 9;
			this.TileGroupBox.TabStop = false;
			this.TileGroupBox.Text = "Tile :";
			// 
			// CollisionButton2
			// 
			this.CollisionButton2.Image = ((System.Drawing.Image)(resources.GetObject("CollisionButton2.Image")));
			this.CollisionButton2.Location = new System.Drawing.Point(129, 44);
			this.CollisionButton2.Name = "CollisionButton2";
			this.CollisionButton2.Size = new System.Drawing.Size(23, 22);
			this.CollisionButton2.TabIndex = 4;
			this.CollisionButton2.UseVisualStyleBackColor = true;
			// 
			// button2
			// 
			this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
			this.button2.Location = new System.Drawing.Point(100, 44);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(23, 22);
			this.button2.TabIndex = 4;
			this.button2.UseVisualStyleBackColor = true;
			// 
			// button1
			// 
			this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
			this.button1.Location = new System.Drawing.Point(71, 44);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(23, 22);
			this.button1.TabIndex = 4;
			this.button1.UseVisualStyleBackColor = true;
			// 
			// numericUpDown1
			// 
			this.numericUpDown1.Location = new System.Drawing.Point(6, 44);
			this.numericUpDown1.Name = "numericUpDown1";
			this.numericUpDown1.Size = new System.Drawing.Size(59, 20);
			this.numericUpDown1.TabIndex = 3;
			// 
			// TilePropertyGrid
			// 
			this.TilePropertyGrid.Location = new System.Drawing.Point(3, 83);
			this.TilePropertyGrid.Name = "TilePropertyGrid";
			this.TilePropertyGrid.Size = new System.Drawing.Size(194, 149);
			this.TilePropertyGrid.TabIndex = 2;
			this.TilePropertyGrid.ToolbarVisible = false;
			// 
			// TileToolStrip
			// 
			this.TileToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.TileToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddTileButton,
            this.TilesBox,
            this.EraseTileButton,
            this.toolStripSeparator5,
            this.HotSpotButton,
            this.CollisionButton});
			this.TileToolStrip.Location = new System.Drawing.Point(3, 16);
			this.TileToolStrip.Name = "TileToolStrip";
			this.TileToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.TileToolStrip.Size = new System.Drawing.Size(194, 25);
			this.TileToolStrip.TabIndex = 1;
			// 
			// AddTileButton
			// 
			this.AddTileButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.AddTileButton.Image = ((System.Drawing.Image)(resources.GetObject("AddTileButton.Image")));
			this.AddTileButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.AddTileButton.Name = "AddTileButton";
			this.AddTileButton.Size = new System.Drawing.Size(23, 22);
			this.AddTileButton.Text = "Add a tile";
			this.AddTileButton.Click += new System.EventHandler(this.AddTileButton_Click);
			// 
			// TilesBox
			// 
			this.TilesBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.TilesBox.DropDownWidth = 50;
			this.TilesBox.Name = "TilesBox";
			this.TilesBox.Size = new System.Drawing.Size(75, 25);
			this.TilesBox.SelectedIndexChanged += new System.EventHandler(this.TilesBox_SelectedIndexChanged);
			// 
			// EraseTileButton
			// 
			this.EraseTileButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.EraseTileButton.Image = ((System.Drawing.Image)(resources.GetObject("EraseTileButton.Image")));
			this.EraseTileButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.EraseTileButton.Name = "EraseTileButton";
			this.EraseTileButton.Size = new System.Drawing.Size(23, 22);
			this.EraseTileButton.Text = "Remove tile";
			this.EraseTileButton.Click += new System.EventHandler(this.EraseTileButton_Click);
			// 
			// toolStripSeparator5
			// 
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
			// 
			// HotSpotButton
			// 
			this.HotSpotButton.CheckOnClick = true;
			this.HotSpotButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.HotSpotButton.Image = ((System.Drawing.Image)(resources.GetObject("HotSpotButton.Image")));
			this.HotSpotButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.HotSpotButton.Name = "HotSpotButton";
			this.HotSpotButton.Size = new System.Drawing.Size(23, 22);
			this.HotSpotButton.Text = "HotSpot";
			this.HotSpotButton.Click += new System.EventHandler(this.HotSpotButton_Click);
			// 
			// CollisionButton
			// 
			this.CollisionButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.CollisionButton.Image = ((System.Drawing.Image)(resources.GetObject("CollisionButton.Image")));
			this.CollisionButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.CollisionButton.Name = "CollisionButton";
			this.CollisionButton.Size = new System.Drawing.Size(23, 22);
			this.CollisionButton.Text = "toolStripButton1";
			// 
			// GLTileControl
			// 
			this.GLTileControl.BackColor = System.Drawing.Color.Black;
			this.GLTileControl.Cursor = System.Windows.Forms.Cursors.Cross;
			this.GLTileControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.GLTileControl.Location = new System.Drawing.Point(200, 0);
			this.GLTileControl.Name = "GLTileControl";
			this.GLTileControl.Size = new System.Drawing.Size(540, 220);
			this.GLTileControl.TabIndex = 11;
			this.GLTileControl.VSync = false;
			this.GLTileControl.Paint += new System.Windows.Forms.PaintEventHandler(this.GLTileControl_Paint);
			this.GLTileControl.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.OnPreviewKeyDown);
			this.GLTileControl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.GLTileControl_MouseMove);
			this.GLTileControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GLTileControl_MouseDown);
			this.GLTileControl.Resize += new System.EventHandler(this.GLTileControl_Resize);
			this.GLTileControl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.GLTileControl_MouseUp);
			// 
			// TileSetGroupBox
			// 
			this.TileSetGroupBox.Controls.Add(this.TileSetPropertyGrid);
			this.TileSetGroupBox.Dock = System.Windows.Forms.DockStyle.Left;
			this.TileSetGroupBox.Location = new System.Drawing.Point(0, 0);
			this.TileSetGroupBox.Name = "TileSetGroupBox";
			this.TileSetGroupBox.Size = new System.Drawing.Size(200, 300);
			this.TileSetGroupBox.TabIndex = 10;
			this.TileSetGroupBox.TabStop = false;
			this.TileSetGroupBox.Text = "TileSet :";
			// 
			// TileSetPropertyGrid
			// 
			this.TileSetPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TileSetPropertyGrid.Location = new System.Drawing.Point(3, 16);
			this.TileSetPropertyGrid.Name = "TileSetPropertyGrid";
			this.TileSetPropertyGrid.Size = new System.Drawing.Size(194, 281);
			this.TileSetPropertyGrid.TabIndex = 0;
			this.TileSetPropertyGrid.ToolbarVisible = false;
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.GLTextureControl);
			this.splitContainer1.Panel1.Controls.Add(this.VertScroller);
			this.splitContainer1.Panel1.Controls.Add(this.HScroller);
			this.splitContainer1.Panel1.Controls.Add(this.TextureToolStrip);
			this.splitContainer1.Panel1.Controls.Add(this.TileSetGroupBox);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.GLTileControl);
			this.splitContainer1.Panel2.Controls.Add(this.TileGroupBox);
			this.splitContainer1.Size = new System.Drawing.Size(740, 524);
			this.splitContainer1.SplitterDistance = 300;
			this.splitContainer1.TabIndex = 11;
			// 
			// VertScroller
			// 
			this.VertScroller.Dock = System.Windows.Forms.DockStyle.Right;
			this.VertScroller.Location = new System.Drawing.Point(723, 25);
			this.VertScroller.Name = "VertScroller";
			this.VertScroller.Size = new System.Drawing.Size(17, 258);
			this.VertScroller.TabIndex = 13;
			// 
			// HScroller
			// 
			this.HScroller.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.HScroller.Location = new System.Drawing.Point(200, 283);
			this.HScroller.Name = "HScroller";
			this.HScroller.Size = new System.Drawing.Size(540, 17);
			this.HScroller.TabIndex = 12;
			// 
			// TextureToolStrip
			// 
			this.TextureToolStrip.AutoSize = false;
			this.TextureToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.TextureToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ZoomOutButton,
            this.ZoomBox,
            this.ZoomInButton,
            this.ActualSizeButton,
            this.toolStripSeparator2,
            this.toolStripLabel1,
            this.TexturesBox,
            this.toolStripSeparator1,
            this.PositionLabel,
            this.toolStripSeparator3,
            this.SizeLabel,
            this.toolStripSeparator4,
            this.BgColorButton});
			this.TextureToolStrip.Location = new System.Drawing.Point(200, 0);
			this.TextureToolStrip.Name = "TextureToolStrip";
			this.TextureToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.TextureToolStrip.Size = new System.Drawing.Size(540, 25);
			this.TextureToolStrip.TabIndex = 11;
			this.TextureToolStrip.Text = "toolStrip2";
			// 
			// ZoomOutButton
			// 
			this.ZoomOutButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.ZoomOutButton.Image = ((System.Drawing.Image)(resources.GetObject("ZoomOutButton.Image")));
			this.ZoomOutButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.ZoomOutButton.Name = "ZoomOutButton";
			this.ZoomOutButton.Size = new System.Drawing.Size(23, 22);
			this.ZoomOutButton.Text = "Zoom out";
			this.ZoomOutButton.Click += new System.EventHandler(this.ZoomOutButton_Click);
			// 
			// ZoomBox
			// 
			this.ZoomBox.DropDownHeight = 140;
			this.ZoomBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ZoomBox.DropDownWidth = 80;
			this.ZoomBox.IntegralHeight = false;
			this.ZoomBox.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
			this.ZoomBox.Name = "ZoomBox";
			this.ZoomBox.Size = new System.Drawing.Size(80, 25);
			// 
			// ZoomInButton
			// 
			this.ZoomInButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.ZoomInButton.Image = ((System.Drawing.Image)(resources.GetObject("ZoomInButton.Image")));
			this.ZoomInButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.ZoomInButton.Name = "ZoomInButton";
			this.ZoomInButton.Size = new System.Drawing.Size(23, 22);
			this.ZoomInButton.Text = "Zoom in";
			this.ZoomInButton.Click += new System.EventHandler(this.ZoomInButton_Click);
			// 
			// ActualSizeButton
			// 
			this.ActualSizeButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.ActualSizeButton.Image = ((System.Drawing.Image)(resources.GetObject("ActualSizeButton.Image")));
			this.ActualSizeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.ActualSizeButton.Name = "ActualSizeButton";
			this.ActualSizeButton.Size = new System.Drawing.Size(23, 22);
			this.ActualSizeButton.Text = "Actual size";
			this.ActualSizeButton.Click += new System.EventHandler(this.ActualSizeButton_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			// 
			// PositionLabel
			// 
			this.PositionLabel.AutoSize = false;
			this.PositionLabel.Image = ((System.Drawing.Image)(resources.GetObject("PositionLabel.Image")));
			this.PositionLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.PositionLabel.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.PositionLabel.Name = "PositionLabel";
			this.PositionLabel.Padding = new System.Windows.Forms.Padding(0, 0, 75, 0);
			this.PositionLabel.Size = new System.Drawing.Size(100, 22);
			this.PositionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.PositionLabel.ToolTipText = "Offset of the cursor";
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
			// 
			// SizeLabel
			// 
			this.SizeLabel.AutoSize = false;
			this.SizeLabel.Image = ((System.Drawing.Image)(resources.GetObject("SizeLabel.Image")));
			this.SizeLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.SizeLabel.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.SizeLabel.Name = "SizeLabel";
			this.SizeLabel.Padding = new System.Windows.Forms.Padding(0, 0, 75, 0);
			this.SizeLabel.Size = new System.Drawing.Size(100, 22);
			this.SizeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
			// 
			// BgColorButton
			// 
			this.BgColorButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.BgColorButton.Image = ((System.Drawing.Image)(resources.GetObject("BgColorButton.Image")));
			this.BgColorButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.BgColorButton.Name = "BgColorButton";
			this.BgColorButton.Size = new System.Drawing.Size(23, 20);
			this.BgColorButton.Text = "Background clear color...";
			this.BgColorButton.Click += new System.EventHandler(this.BgColorButton_Click);
			// 
			// RenderTimer
			// 
			this.RenderTimer.Enabled = true;
			this.RenderTimer.Interval = 66;
			this.RenderTimer.Tick += new System.EventHandler(this.RenderTimer_Tick);
			// 
			// TexturesBox
			// 
			this.TexturesBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.TexturesBox.Name = "TexturesBox";
			this.TexturesBox.Size = new System.Drawing.Size(121, 25);
			this.TexturesBox.SelectedIndexChanged += new System.EventHandler(this.TexturesBox_SelectedIndexChanged);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// toolStripLabel1
			// 
			this.toolStripLabel1.Name = "toolStripLabel1";
			this.toolStripLabel1.Size = new System.Drawing.Size(52, 22);
			this.toolStripLabel1.Text = "Texture :";
			// 
			// TileSetForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(740, 524);
			this.Controls.Add(this.splitContainer1);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "TileSetForm";
			this.Load += new System.EventHandler(this.TileSetForm_Load);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TileSetForm_FormClosing);
			this.TileGroupBox.ResumeLayout(false);
			this.TileGroupBox.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
			this.TileToolStrip.ResumeLayout(false);
			this.TileToolStrip.PerformLayout();
			this.TileSetGroupBox.ResumeLayout(false);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			this.TextureToolStrip.ResumeLayout(false);
			this.TextureToolStrip.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private OpenTK.GLControl GLTextureControl;
		private System.Windows.Forms.GroupBox TileGroupBox;
		private System.Windows.Forms.GroupBox TileSetGroupBox;
		private System.Windows.Forms.PropertyGrid TileSetPropertyGrid;
		private System.Windows.Forms.PropertyGrid TilePropertyGrid;
		private OpenTK.GLControl GLTileControl;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.ToolStrip TextureToolStrip;
		private System.Windows.Forms.ToolStripButton ZoomOutButton;
		private System.Windows.Forms.ToolStripComboBox ZoomBox;
		private System.Windows.Forms.ToolStripButton ZoomInButton;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripLabel PositionLabel;
		private System.Windows.Forms.ToolStripLabel SizeLabel;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.Timer RenderTimer;
		private System.Windows.Forms.VScrollBar VertScroller;
		private System.Windows.Forms.HScrollBar HScroller;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripButton BgColorButton;
		private System.Windows.Forms.ToolStripButton ActualSizeButton;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.NumericUpDown numericUpDown1;
		private System.Windows.Forms.Button CollisionButton2;
		private System.Windows.Forms.ToolStrip TileToolStrip;
		private System.Windows.Forms.ToolStripButton AddTileButton;
		private System.Windows.Forms.ToolStripComboBox TilesBox;
		private System.Windows.Forms.ToolStripButton EraseTileButton;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
		private System.Windows.Forms.ToolStripButton HotSpotButton;
		private System.Windows.Forms.ToolStripButton CollisionButton;
		private System.Windows.Forms.ToolStripComboBox TexturesBox;
		private System.Windows.Forms.ToolStripLabel toolStripLabel1;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
	}
}
