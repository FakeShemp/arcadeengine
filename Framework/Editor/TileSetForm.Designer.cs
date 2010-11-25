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
			this.TileGroupBox = new System.Windows.Forms.GroupBox();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.TileIDBox = new ArcEngine.Forms.ToolStripNumberControl();
			this.EraseTileBox = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			this.SelectionBox = new System.Windows.Forms.ToolStripButton();
			this.HotSpotBox = new System.Windows.Forms.ToolStripButton();
			this.ColisionBox = new System.Windows.Forms.ToolStripButton();
			this.TilePropertyGrid = new System.Windows.Forms.PropertyGrid();
			this.VertScroller = new System.Windows.Forms.VScrollBar();
			this.HScroller = new System.Windows.Forms.HScrollBar();
			this.TextureToolStrip = new System.Windows.Forms.ToolStrip();
			this.ZoomOutButton = new System.Windows.Forms.ToolStripButton();
			this.ZoomBox = new System.Windows.Forms.ToolStripComboBox();
			this.ZoomInButton = new System.Windows.Forms.ToolStripButton();
			this.ActualSizeButton = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
			this.TextureNameBox = new System.Windows.Forms.ToolStripTextBox();
			this.ChangeTextureBox = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.PositionLabel = new System.Windows.Forms.ToolStripLabel();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.SizeLabel = new System.Windows.Forms.ToolStripLabel();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.RenderTimer = new System.Windows.Forms.Timer(this.components);
			this.GLTileControl = new OpenTK.GLControl();
			this.GLTextureControl = new OpenTK.GLControl();
			this.TileGroupBox.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.TextureToolStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// TileGroupBox
			// 
			this.TileGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.TileGroupBox.Controls.Add(this.toolStrip1);
			this.TileGroupBox.Controls.Add(this.TilePropertyGrid);
			this.TileGroupBox.Location = new System.Drawing.Point(0, 288);
			this.TileGroupBox.Name = "TileGroupBox";
			this.TileGroupBox.Size = new System.Drawing.Size(200, 345);
			this.TileGroupBox.TabIndex = 9;
			this.TileGroupBox.TabStop = false;
			this.TileGroupBox.Text = "Tile :";
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TileIDBox,
            this.EraseTileBox,
            this.toolStripSeparator5,
            this.SelectionBox,
            this.HotSpotBox,
            this.ColisionBox});
			this.toolStrip1.Location = new System.Drawing.Point(3, 16);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(194, 25);
			this.toolStrip1.TabIndex = 18;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// TileIDBox
			// 
			this.TileIDBox.AutoSize = false;
			this.TileIDBox.Maximum = new decimal(new int[] {
            -1530494977,
            232830,
            0,
            0});
			this.TileIDBox.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this.TileIDBox.Name = "TileIDBox";
			this.TileIDBox.Size = new System.Drawing.Size(60, 22);
			this.TileIDBox.Text = "0";
			this.TileIDBox.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this.TileIDBox.ValueChanged += new System.EventHandler(this.TilesBox_SelectedIndexChanged);
			// 
			// EraseTileBox
			// 
			this.EraseTileBox.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.EraseTileBox.Image = ((System.Drawing.Image) (resources.GetObject("EraseTileBox.Image")));
			this.EraseTileBox.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.EraseTileBox.Name = "EraseTileBox";
			this.EraseTileBox.Size = new System.Drawing.Size(23, 22);
			this.EraseTileBox.Text = "toolStripButton1";
			this.EraseTileBox.Click += new System.EventHandler(this.EraseTileButton_Click);
			// 
			// toolStripSeparator5
			// 
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
			// 
			// SelectionBox
			// 
			this.SelectionBox.CheckOnClick = true;
			this.SelectionBox.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.SelectionBox.Image = ((System.Drawing.Image) (resources.GetObject("SelectionBox.Image")));
			this.SelectionBox.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.SelectionBox.Name = "SelectionBox";
			this.SelectionBox.Size = new System.Drawing.Size(23, 22);
			this.SelectionBox.Text = "Change selection";
			this.SelectionBox.CheckedChanged += new System.EventHandler(this.ToggleStripButtons);
			// 
			// HotSpotBox
			// 
			this.HotSpotBox.CheckOnClick = true;
			this.HotSpotBox.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.HotSpotBox.Image = ((System.Drawing.Image) (resources.GetObject("HotSpotBox.Image")));
			this.HotSpotBox.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.HotSpotBox.Name = "HotSpotBox";
			this.HotSpotBox.Size = new System.Drawing.Size(23, 22);
			this.HotSpotBox.Text = "Set hotspot";
			this.HotSpotBox.CheckedChanged += new System.EventHandler(this.ToggleStripButtons);
			// 
			// ColisionBox
			// 
			this.ColisionBox.CheckOnClick = true;
			this.ColisionBox.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.ColisionBox.Image = ((System.Drawing.Image) (resources.GetObject("ColisionBox.Image")));
			this.ColisionBox.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.ColisionBox.Name = "ColisionBox";
			this.ColisionBox.Size = new System.Drawing.Size(23, 22);
			this.ColisionBox.Text = "Set colision box";
			this.ColisionBox.CheckedChanged += new System.EventHandler(this.ToggleStripButtons);
			// 
			// TilePropertyGrid
			// 
			this.TilePropertyGrid.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TilePropertyGrid.Location = new System.Drawing.Point(12, 44);
			this.TilePropertyGrid.Name = "TilePropertyGrid";
			this.TilePropertyGrid.Size = new System.Drawing.Size(182, 314);
			this.TilePropertyGrid.TabIndex = 2;
			this.TilePropertyGrid.ToolbarVisible = false;
			// 
			// VertScroller
			// 
			this.VertScroller.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.VertScroller.Location = new System.Drawing.Point(901, 25);
			this.VertScroller.Name = "VertScroller";
			this.VertScroller.Size = new System.Drawing.Size(17, 257);
			this.VertScroller.TabIndex = 13;
			// 
			// HScroller
			// 
			this.HScroller.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.HScroller.Location = new System.Drawing.Point(206, 288);
			this.HScroller.Name = "HScroller";
			this.HScroller.Size = new System.Drawing.Size(695, 17);
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
            this.TextureNameBox,
            this.ChangeTextureBox,
            this.toolStripSeparator1,
            this.PositionLabel,
            this.toolStripSeparator3,
            this.SizeLabel,
            this.toolStripSeparator4});
			this.TextureToolStrip.Location = new System.Drawing.Point(0, 0);
			this.TextureToolStrip.Name = "TextureToolStrip";
			this.TextureToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.TextureToolStrip.Size = new System.Drawing.Size(918, 25);
			this.TextureToolStrip.TabIndex = 11;
			this.TextureToolStrip.Text = "toolStrip2";
			// 
			// ZoomOutButton
			// 
			this.ZoomOutButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.ZoomOutButton.Image = ((System.Drawing.Image) (resources.GetObject("ZoomOutButton.Image")));
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
			this.ZoomInButton.Image = ((System.Drawing.Image) (resources.GetObject("ZoomInButton.Image")));
			this.ZoomInButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.ZoomInButton.Name = "ZoomInButton";
			this.ZoomInButton.Size = new System.Drawing.Size(23, 22);
			this.ZoomInButton.Text = "Zoom in";
			this.ZoomInButton.Click += new System.EventHandler(this.ZoomInButton_Click);
			// 
			// ActualSizeButton
			// 
			this.ActualSizeButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.ActualSizeButton.Image = ((System.Drawing.Image) (resources.GetObject("ActualSizeButton.Image")));
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
			// toolStripLabel1
			// 
			this.toolStripLabel1.Name = "toolStripLabel1";
			this.toolStripLabel1.Size = new System.Drawing.Size(52, 22);
			this.toolStripLabel1.Text = "Texture :";
			// 
			// TextureNameBox
			// 
			this.TextureNameBox.Name = "TextureNameBox";
			this.TextureNameBox.ReadOnly = true;
			this.TextureNameBox.Size = new System.Drawing.Size(160, 25);
			// 
			// ChangeTextureBox
			// 
			this.ChangeTextureBox.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.ChangeTextureBox.Image = ((System.Drawing.Image) (resources.GetObject("ChangeTextureBox.Image")));
			this.ChangeTextureBox.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.ChangeTextureBox.Name = "ChangeTextureBox";
			this.ChangeTextureBox.Size = new System.Drawing.Size(23, 22);
			this.ChangeTextureBox.Text = "Change texture...";
			this.ChangeTextureBox.Click += new System.EventHandler(this.ChangeTextureBox_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// PositionLabel
			// 
			this.PositionLabel.AutoSize = false;
			this.PositionLabel.Image = ((System.Drawing.Image) (resources.GetObject("PositionLabel.Image")));
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
			this.SizeLabel.Image = ((System.Drawing.Image) (resources.GetObject("SizeLabel.Image")));
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
			// RenderTimer
			// 
			this.RenderTimer.Enabled = true;
			this.RenderTimer.Interval = 66;
			this.RenderTimer.Tick += new System.EventHandler(this.RenderTimer_Tick);
			// 
			// GLTileControl
			// 
			this.GLTileControl.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.GLTileControl.BackColor = System.Drawing.Color.Black;
			this.GLTileControl.Location = new System.Drawing.Point(206, 308);
			this.GLTileControl.Name = "GLTileControl";
			this.GLTileControl.Size = new System.Drawing.Size(700, 338);
			this.GLTileControl.TabIndex = 15;
			this.GLTileControl.VSync = false;
			this.GLTileControl.Paint += new System.Windows.Forms.PaintEventHandler(this.GLTileControl_Paint);
			this.GLTileControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GLTileControl_MouseDown);
			this.GLTileControl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.GLTileControl_MouseMove);
			this.GLTileControl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.GLTileControl_MouseUp);
			this.GLTileControl.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.OnPreviewKeyDown);
			this.GLTileControl.Resize += new System.EventHandler(this.GLTileControl_Resize);
			// 
			// GLTextureControl
			// 
			this.GLTextureControl.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.GLTextureControl.BackColor = System.Drawing.Color.Black;
			this.GLTextureControl.Location = new System.Drawing.Point(3, 28);
			this.GLTextureControl.Name = "GLTextureControl";
			this.GLTextureControl.Size = new System.Drawing.Size(895, 254);
			this.GLTextureControl.TabIndex = 16;
			this.GLTextureControl.VSync = false;
			this.GLTextureControl.Paint += new System.Windows.Forms.PaintEventHandler(this.GLTextureControl_Paint);
			this.GLTextureControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GLTextureControl_MouseDown);
			this.GLTextureControl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.GLTextureControl_MouseMove);
			this.GLTextureControl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.GLTextureControl_MouseUp);
			this.GLTextureControl.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.OnPreviewKeyDown);
			this.GLTextureControl.Resize += new System.EventHandler(this.GLTextureControl_Resize);
			// 
			// TileSetForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(918, 658);
			this.Controls.Add(this.GLTextureControl);
			this.Controls.Add(this.GLTileControl);
			this.Controls.Add(this.VertScroller);
			this.Controls.Add(this.HScroller);
			this.Controls.Add(this.TextureToolStrip);
			this.Controls.Add(this.TileGroupBox);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
			this.Name = "TileSetForm";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TileSetForm_FormClosing);
			this.Load += new System.EventHandler(this.TileSetForm_Load);
			this.TileGroupBox.ResumeLayout(false);
			this.TileGroupBox.PerformLayout();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.TextureToolStrip.ResumeLayout(false);
			this.TextureToolStrip.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox TileGroupBox;
		private System.Windows.Forms.PropertyGrid TilePropertyGrid;
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
		private System.Windows.Forms.ToolStripButton ActualSizeButton;
		private System.Windows.Forms.ToolStripLabel toolStripLabel1;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private OpenTK.GLControl GLTileControl;
		private OpenTK.GLControl GLTextureControl;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private ArcEngine.Forms.ToolStripNumberControl TileIDBox;
		private System.Windows.Forms.ToolStripButton EraseTileBox;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
		private System.Windows.Forms.ToolStripButton SelectionBox;
		private System.Windows.Forms.ToolStripButton HotSpotBox;
		private System.Windows.Forms.ToolStripButton ColisionBox;
		private System.Windows.Forms.ToolStripTextBox TextureNameBox;
		private System.Windows.Forms.ToolStripButton ChangeTextureBox;
	}
}
