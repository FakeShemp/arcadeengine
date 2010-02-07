namespace ArcEngine.Editor
{
	partial class AnimationForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AnimationForm));
			this.PropertyBox = new System.Windows.Forms.PropertyGrid();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.GlPreviewControl = new OpenTK.GLControl();
			this.PreviewToolStrip = new System.Windows.Forms.ToolStrip();
			this.AnimPreviousFrame = new System.Windows.Forms.ToolStripButton();
			this.AnimPlay = new System.Windows.Forms.ToolStripButton();
			this.AnimNextFrame = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.AnimPause = new System.Windows.Forms.ToolStripButton();
			this.AnimStop = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.AnimNoZoom = new System.Windows.Forms.ToolStripButton();
			this.AnimZoomIn = new System.Windows.Forms.ToolStripButton();
			this.AnimZoomOut = new System.Windows.Forms.ToolStripButton();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.GlTilesControl = new OpenTK.GLControl();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.TilesNoZoom = new System.Windows.Forms.ToolStripButton();
			this.TilesZoomIn = new System.Windows.Forms.ToolStripButton();
			this.TilesZoomOut = new System.Windows.Forms.ToolStripButton();
			this.TilesHScroller = new System.Windows.Forms.HScrollBar();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.GlFramesControl = new OpenTK.GLControl();
			this.toolStrip2 = new System.Windows.Forms.ToolStrip();
			this.FramesAdd = new System.Windows.Forms.ToolStripButton();
			this.FramesDelete = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.MoveLeft = new System.Windows.Forms.ToolStripButton();
			this.MoveRight = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.FramesNoZoom = new System.Windows.Forms.ToolStripButton();
			this.FramesZoomIn = new System.Windows.Forms.ToolStripButton();
			this.FramesZoomOut = new System.Windows.Forms.ToolStripButton();
			this.DrawTimer = new System.Windows.Forms.Timer(this.components);
			this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
			this.TileSetNameBox = new System.Windows.Forms.ToolStripComboBox();
			this.groupBox2.SuspendLayout();
			this.PreviewToolStrip.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.toolStrip2.SuspendLayout();
			this.SuspendLayout();
			// 
			// PropertyBox
			// 
			this.PropertyBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PropertyBox.Location = new System.Drawing.Point(3, 16);
			this.PropertyBox.Name = "PropertyBox";
			this.PropertyBox.Size = new System.Drawing.Size(194, 357);
			this.PropertyBox.TabIndex = 1;
			// 
			// groupBox2
			// 
			this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox2.Controls.Add(this.GlPreviewControl);
			this.groupBox2.Controls.Add(this.PreviewToolStrip);
			this.groupBox2.Location = new System.Drawing.Point(206, 0);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(665, 180);
			this.groupBox2.TabIndex = 3;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Preview :";
			// 
			// GlPreviewControl
			// 
			this.GlPreviewControl.BackColor = System.Drawing.Color.Black;
			this.GlPreviewControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.GlPreviewControl.Location = new System.Drawing.Point(3, 41);
			this.GlPreviewControl.Name = "GlPreviewControl";
			this.GlPreviewControl.Size = new System.Drawing.Size(659, 136);
			this.GlPreviewControl.TabIndex = 2;
			this.GlPreviewControl.VSync = false;
			this.GlPreviewControl.Paint += new System.Windows.Forms.PaintEventHandler(this.GlPreviewControl_Paint);
			this.GlPreviewControl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.GlPreviewControl_MouseMove);
			this.GlPreviewControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GlPreviewControl_MouseDown);
			this.GlPreviewControl.Resize += new System.EventHandler(this.GlPreviewControl_Resize);
			this.GlPreviewControl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.GlPreviewControl_MouseUp);
			// 
			// PreviewToolStrip
			// 
			this.PreviewToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.PreviewToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AnimPreviousFrame,
            this.AnimPlay,
            this.AnimNextFrame,
            this.toolStripSeparator4,
            this.AnimPause,
            this.AnimStop,
            this.toolStripSeparator1,
            this.AnimNoZoom,
            this.AnimZoomIn,
            this.AnimZoomOut});
			this.PreviewToolStrip.Location = new System.Drawing.Point(3, 16);
			this.PreviewToolStrip.Name = "PreviewToolStrip";
			this.PreviewToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.PreviewToolStrip.Size = new System.Drawing.Size(659, 25);
			this.PreviewToolStrip.TabIndex = 1;
			this.PreviewToolStrip.Text = "toolStrip1";
			// 
			// AnimPreviousFrame
			// 
			this.AnimPreviousFrame.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.AnimPreviousFrame.Image = ((System.Drawing.Image)(resources.GetObject("AnimPreviousFrame.Image")));
			this.AnimPreviousFrame.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.AnimPreviousFrame.Name = "AnimPreviousFrame";
			this.AnimPreviousFrame.Size = new System.Drawing.Size(23, 22);
			this.AnimPreviousFrame.Text = "Previous frame";
			// 
			// AnimPlay
			// 
			this.AnimPlay.CheckOnClick = true;
			this.AnimPlay.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.AnimPlay.Image = ((System.Drawing.Image)(resources.GetObject("AnimPlay.Image")));
			this.AnimPlay.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.AnimPlay.Name = "AnimPlay";
			this.AnimPlay.Size = new System.Drawing.Size(23, 22);
			this.AnimPlay.Text = "Play";
			this.AnimPlay.Click += new System.EventHandler(this.AnimPlay_Click);
			// 
			// AnimNextFrame
			// 
			this.AnimNextFrame.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.AnimNextFrame.Image = ((System.Drawing.Image)(resources.GetObject("AnimNextFrame.Image")));
			this.AnimNextFrame.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.AnimNextFrame.Name = "AnimNextFrame";
			this.AnimNextFrame.Size = new System.Drawing.Size(23, 22);
			this.AnimNextFrame.Text = "Next frame";
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
			// 
			// AnimPause
			// 
			this.AnimPause.CheckOnClick = true;
			this.AnimPause.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.AnimPause.Image = ((System.Drawing.Image)(resources.GetObject("AnimPause.Image")));
			this.AnimPause.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.AnimPause.Name = "AnimPause";
			this.AnimPause.Size = new System.Drawing.Size(23, 22);
			this.AnimPause.Text = "Pause";
			this.AnimPause.Click += new System.EventHandler(this.AnimPause_Click);
			// 
			// AnimStop
			// 
			this.AnimStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.AnimStop.Image = ((System.Drawing.Image)(resources.GetObject("AnimStop.Image")));
			this.AnimStop.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.AnimStop.Name = "AnimStop";
			this.AnimStop.Size = new System.Drawing.Size(23, 22);
			this.AnimStop.Text = "Stop";
			this.AnimStop.Click += new System.EventHandler(this.AnimStop_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// AnimNoZoom
			// 
			this.AnimNoZoom.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.AnimNoZoom.Image = ((System.Drawing.Image)(resources.GetObject("AnimNoZoom.Image")));
			this.AnimNoZoom.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.AnimNoZoom.Name = "AnimNoZoom";
			this.AnimNoZoom.Size = new System.Drawing.Size(23, 22);
			this.AnimNoZoom.Text = "Actual size";
			// 
			// AnimZoomIn
			// 
			this.AnimZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.AnimZoomIn.Image = ((System.Drawing.Image)(resources.GetObject("AnimZoomIn.Image")));
			this.AnimZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.AnimZoomIn.Name = "AnimZoomIn";
			this.AnimZoomIn.Size = new System.Drawing.Size(23, 22);
			this.AnimZoomIn.Text = "Zoom in";
			// 
			// AnimZoomOut
			// 
			this.AnimZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.AnimZoomOut.Image = ((System.Drawing.Image)(resources.GetObject("AnimZoomOut.Image")));
			this.AnimZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.AnimZoomOut.Name = "AnimZoomOut";
			this.AnimZoomOut.Size = new System.Drawing.Size(23, 22);
			this.AnimZoomOut.Text = "Zoom out";
			// 
			// groupBox3
			// 
			this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox3.Controls.Add(this.GlTilesControl);
			this.groupBox3.Controls.Add(this.toolStrip1);
			this.groupBox3.Controls.Add(this.TilesHScroller);
			this.groupBox3.Location = new System.Drawing.Point(0, 382);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(871, 207);
			this.groupBox3.TabIndex = 4;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Tiles :";
			// 
			// GlTilesControl
			// 
			this.GlTilesControl.BackColor = System.Drawing.Color.Black;
			this.GlTilesControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.GlTilesControl.Location = new System.Drawing.Point(3, 41);
			this.GlTilesControl.Name = "GlTilesControl";
			this.GlTilesControl.Size = new System.Drawing.Size(865, 146);
			this.GlTilesControl.TabIndex = 3;
			this.GlTilesControl.VSync = false;
			this.GlTilesControl.Paint += new System.Windows.Forms.PaintEventHandler(this.GlTilesControl_Paint);
			this.GlTilesControl.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.GlTilesControl_MouseDoubleClick);
			this.GlTilesControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GlTilesControl_MouseDown);
			this.GlTilesControl.Resize += new System.EventHandler(this.GlTilesControl_Resize);
			// 
			// toolStrip1
			// 
			this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TilesNoZoom,
            this.TilesZoomIn,
            this.TilesZoomOut,
            this.toolStripSeparator5,
            this.toolStripLabel1,
            this.TileSetNameBox});
			this.toolStrip1.Location = new System.Drawing.Point(3, 16);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.toolStrip1.Size = new System.Drawing.Size(865, 25);
			this.toolStrip1.TabIndex = 2;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// TilesNoZoom
			// 
			this.TilesNoZoom.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.TilesNoZoom.Image = ((System.Drawing.Image)(resources.GetObject("TilesNoZoom.Image")));
			this.TilesNoZoom.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.TilesNoZoom.Name = "TilesNoZoom";
			this.TilesNoZoom.Size = new System.Drawing.Size(23, 22);
			this.TilesNoZoom.Text = "Actual size";
			// 
			// TilesZoomIn
			// 
			this.TilesZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.TilesZoomIn.Image = ((System.Drawing.Image)(resources.GetObject("TilesZoomIn.Image")));
			this.TilesZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.TilesZoomIn.Name = "TilesZoomIn";
			this.TilesZoomIn.Size = new System.Drawing.Size(23, 22);
			this.TilesZoomIn.Text = "Zoom in";
			// 
			// TilesZoomOut
			// 
			this.TilesZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.TilesZoomOut.Image = ((System.Drawing.Image)(resources.GetObject("TilesZoomOut.Image")));
			this.TilesZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.TilesZoomOut.Name = "TilesZoomOut";
			this.TilesZoomOut.Size = new System.Drawing.Size(23, 22);
			this.TilesZoomOut.Text = "Zoom out";
			// 
			// TilesHScroller
			// 
			this.TilesHScroller.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.TilesHScroller.Location = new System.Drawing.Point(3, 187);
			this.TilesHScroller.Name = "TilesHScroller";
			this.TilesHScroller.Size = new System.Drawing.Size(865, 17);
			this.TilesHScroller.TabIndex = 1;
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.PropertyBox);
			this.groupBox4.Location = new System.Drawing.Point(0, 0);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(200, 376);
			this.groupBox4.TabIndex = 5;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Properties :";
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.GlFramesControl);
			this.groupBox1.Controls.Add(this.toolStrip2);
			this.groupBox1.Location = new System.Drawing.Point(206, 183);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(665, 193);
			this.groupBox1.TabIndex = 6;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Frames :";
			// 
			// GlFramesControl
			// 
			this.GlFramesControl.BackColor = System.Drawing.Color.Black;
			this.GlFramesControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.GlFramesControl.Location = new System.Drawing.Point(3, 41);
			this.GlFramesControl.Name = "GlFramesControl";
			this.GlFramesControl.Size = new System.Drawing.Size(659, 149);
			this.GlFramesControl.TabIndex = 4;
			this.GlFramesControl.VSync = false;
			this.GlFramesControl.Paint += new System.Windows.Forms.PaintEventHandler(this.GlFramesControl_Paint);
			this.GlFramesControl.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.GlFramesControl_MouseDoubleClick);
			this.GlFramesControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GlFramesControl_MouseDown);
			this.GlFramesControl.Resize += new System.EventHandler(this.GlFramesControl_Resize);
			// 
			// toolStrip2
			// 
			this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FramesAdd,
            this.FramesDelete,
            this.toolStripSeparator3,
            this.MoveLeft,
            this.MoveRight,
            this.toolStripSeparator2,
            this.FramesNoZoom,
            this.FramesZoomIn,
            this.FramesZoomOut});
			this.toolStrip2.Location = new System.Drawing.Point(3, 16);
			this.toolStrip2.Name = "toolStrip2";
			this.toolStrip2.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.toolStrip2.Size = new System.Drawing.Size(659, 25);
			this.toolStrip2.TabIndex = 3;
			this.toolStrip2.Text = "toolStrip2";
			// 
			// FramesAdd
			// 
			this.FramesAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.FramesAdd.Image = ((System.Drawing.Image)(resources.GetObject("FramesAdd.Image")));
			this.FramesAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.FramesAdd.Name = "FramesAdd";
			this.FramesAdd.Size = new System.Drawing.Size(23, 22);
			this.FramesAdd.Text = "Add a frame";
			this.FramesAdd.Click += new System.EventHandler(this.FramesAdd_Click);
			// 
			// FramesDelete
			// 
			this.FramesDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.FramesDelete.Image = ((System.Drawing.Image)(resources.GetObject("FramesDelete.Image")));
			this.FramesDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.FramesDelete.Name = "FramesDelete";
			this.FramesDelete.Size = new System.Drawing.Size(23, 22);
			this.FramesDelete.Text = "Remove frame";
			this.FramesDelete.Click += new System.EventHandler(this.FramesDelete_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
			// 
			// MoveLeft
			// 
			this.MoveLeft.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.MoveLeft.Image = ((System.Drawing.Image)(resources.GetObject("MoveLeft.Image")));
			this.MoveLeft.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.MoveLeft.Name = "MoveLeft";
			this.MoveLeft.Size = new System.Drawing.Size(23, 22);
			this.MoveLeft.Text = "Move back";
			this.MoveLeft.Click += new System.EventHandler(this.MoveLeft_Click);
			// 
			// MoveRight
			// 
			this.MoveRight.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.MoveRight.Image = ((System.Drawing.Image)(resources.GetObject("MoveRight.Image")));
			this.MoveRight.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.MoveRight.Name = "MoveRight";
			this.MoveRight.Size = new System.Drawing.Size(23, 22);
			this.MoveRight.Text = "Move forward";
			this.MoveRight.Click += new System.EventHandler(this.MoveRight_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			// 
			// FramesNoZoom
			// 
			this.FramesNoZoom.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.FramesNoZoom.Image = ((System.Drawing.Image)(resources.GetObject("FramesNoZoom.Image")));
			this.FramesNoZoom.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.FramesNoZoom.Name = "FramesNoZoom";
			this.FramesNoZoom.Size = new System.Drawing.Size(23, 22);
			this.FramesNoZoom.Text = "Actual size";
			// 
			// FramesZoomIn
			// 
			this.FramesZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.FramesZoomIn.Image = ((System.Drawing.Image)(resources.GetObject("FramesZoomIn.Image")));
			this.FramesZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.FramesZoomIn.Name = "FramesZoomIn";
			this.FramesZoomIn.Size = new System.Drawing.Size(23, 22);
			this.FramesZoomIn.Text = "Zoom in";
			// 
			// FramesZoomOut
			// 
			this.FramesZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.FramesZoomOut.Image = ((System.Drawing.Image)(resources.GetObject("FramesZoomOut.Image")));
			this.FramesZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.FramesZoomOut.Name = "FramesZoomOut";
			this.FramesZoomOut.Size = new System.Drawing.Size(23, 22);
			this.FramesZoomOut.Text = "Zoom out";
			// 
			// DrawTimer
			// 
			this.DrawTimer.Interval = 50;
			this.DrawTimer.Tick += new System.EventHandler(this.DrawTimer_Tick);
			// 
			// toolStripSeparator5
			// 
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
			// 
			// toolStripLabel1
			// 
			this.toolStripLabel1.Name = "toolStripLabel1";
			this.toolStripLabel1.Size = new System.Drawing.Size(50, 22);
			this.toolStripLabel1.Text = "Tileset : ";
			// 
			// TileSetNameBox
			// 
			this.TileSetNameBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.TileSetNameBox.Name = "TileSetNameBox";
			this.TileSetNameBox.Size = new System.Drawing.Size(121, 25);
			// 
			// AnimationForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(855, 556);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.groupBox4);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox2);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "AnimationForm";
			this.TabText = "Animation";
			this.Text = "Animation";
			this.Load += new System.EventHandler(this.AnimationForm_Load);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AnimationForm_FormClosing);
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.PreviewToolStrip.ResumeLayout(false);
			this.PreviewToolStrip.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.groupBox4.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.toolStrip2.ResumeLayout(false);
			this.toolStrip2.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PropertyGrid PropertyBox;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.ToolStrip PreviewToolStrip;
		private System.Windows.Forms.ToolStripButton AnimPlay;
		private System.Windows.Forms.ToolStripButton AnimPause;
		private System.Windows.Forms.ToolStripButton AnimStop;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.HScrollBar TilesHScroller;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ToolStripButton AnimZoomIn;
		private System.Windows.Forms.ToolStripButton AnimZoomOut;
		private System.Windows.Forms.Timer DrawTimer;
		private System.Windows.Forms.ToolStripButton AnimNoZoom;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton TilesNoZoom;
		private System.Windows.Forms.ToolStripButton TilesZoomIn;
		private System.Windows.Forms.ToolStripButton TilesZoomOut;
		private System.Windows.Forms.ToolStrip toolStrip2;
		private System.Windows.Forms.ToolStripButton FramesAdd;
		private System.Windows.Forms.ToolStripButton FramesDelete;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripButton FramesNoZoom;
		private System.Windows.Forms.ToolStripButton FramesZoomIn;
		private System.Windows.Forms.ToolStripButton FramesZoomOut;
		private System.Windows.Forms.ToolStripButton MoveLeft;
		private System.Windows.Forms.ToolStripButton MoveRight;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripButton AnimPreviousFrame;
		private System.Windows.Forms.ToolStripButton AnimNextFrame;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private OpenTK.GLControl GlPreviewControl;
		private OpenTK.GLControl GlTilesControl;
		private OpenTK.GLControl GlFramesControl;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
		private System.Windows.Forms.ToolStripLabel toolStripLabel1;
		private System.Windows.Forms.ToolStripComboBox TileSetNameBox;
	}
}
