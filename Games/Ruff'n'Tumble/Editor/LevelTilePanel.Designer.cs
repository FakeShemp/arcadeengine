namespace RuffnTumble.Editor
{
    partial class LevelTilePanel
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LevelTilePanel));
			this.GlControl = new OpenTK.GLControl();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.ShowGridButton = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.PenButton = new System.Windows.Forms.ToolStripButton();
			this.RectangleButton = new System.Windows.Forms.ToolStripButton();
			this.FloodFillTileButton = new System.Windows.Forms.ToolStripButton();
			this.BrushButton = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.ScrollBar = new System.Windows.Forms.VScrollBar();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// GlControl
			// 
			this.GlControl.BackColor = System.Drawing.Color.Black;
			this.GlControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.GlControl.Location = new System.Drawing.Point(0, 25);
			this.GlControl.Name = "GlControl";
			this.GlControl.Size = new System.Drawing.Size(383, 438);
			this.GlControl.TabIndex = 1;
			this.GlControl.VSync = false;
			this.GlControl.Paint += new System.Windows.Forms.PaintEventHandler(this.GlControl_Paint);
			this.GlControl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.GlControl_MouseMove);
			this.GlControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GlControl_MouseDown);
			this.GlControl.Resize += new System.EventHandler(this.GlControl_Resize);
			this.GlControl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.GlControl_MouseUp);
			this.GlControl.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown);
			// 
			// toolStrip1
			// 
			this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ShowGridButton,
            this.toolStripSeparator1,
            this.PenButton,
            this.RectangleButton,
            this.FloodFillTileButton,
            this.BrushButton,
            this.toolStripSeparator2});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.toolStrip1.Size = new System.Drawing.Size(400, 25);
			this.toolStrip1.TabIndex = 2;
			this.toolStrip1.Text = "toolStrip1";
			this.toolStrip1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown);
			// 
			// ShowGridButton
			// 
			this.ShowGridButton.CheckOnClick = true;
			this.ShowGridButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.ShowGridButton.Image = ((System.Drawing.Image)(resources.GetObject("ShowGridButton.Image")));
			this.ShowGridButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.ShowGridButton.Name = "ShowGridButton";
			this.ShowGridButton.Size = new System.Drawing.Size(23, 22);
			this.ShowGridButton.Text = "Show grid";
			this.ShowGridButton.CheckedChanged += new System.EventHandler(this.ShowGridButton_CheckedChanged);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// PenButton
			// 
			this.PenButton.CheckOnClick = true;
			this.PenButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.PenButton.Image = ((System.Drawing.Image)(resources.GetObject("PenButton.Image")));
			this.PenButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.PenButton.Name = "PenButton";
			this.PenButton.Size = new System.Drawing.Size(23, 22);
			this.PenButton.Text = "Paint mode";
			this.PenButton.Click += new System.EventHandler(this.PasteTileButton_OnClick);
			// 
			// RectangleButton
			// 
			this.RectangleButton.CheckOnClick = true;
			this.RectangleButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.RectangleButton.Image = ((System.Drawing.Image)(resources.GetObject("RectangleButton.Image")));
			this.RectangleButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.RectangleButton.Name = "RectangleButton";
			this.RectangleButton.Size = new System.Drawing.Size(23, 22);
			this.RectangleButton.Text = "Rectangle mode";
			this.RectangleButton.Click += new System.EventHandler(this.RectangleButton_OnClick);
			// 
			// FloodFillTileButton
			// 
			this.FloodFillTileButton.CheckOnClick = true;
			this.FloodFillTileButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.FloodFillTileButton.Image = ((System.Drawing.Image)(resources.GetObject("FloodFillTileButton.Image")));
			this.FloodFillTileButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.FloodFillTileButton.Name = "FloodFillTileButton";
			this.FloodFillTileButton.Size = new System.Drawing.Size(23, 22);
			this.FloodFillTileButton.Text = "Brush mode";
			this.FloodFillTileButton.Click += new System.EventHandler(this.FillTileButton_OnClick);
			// 
			// BrushButton
			// 
			this.BrushButton.CheckOnClick = true;
			this.BrushButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.BrushButton.Image = ((System.Drawing.Image)(resources.GetObject("BrushButton.Image")));
			this.BrushButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.BrushButton.Name = "BrushButton";
			this.BrushButton.Size = new System.Drawing.Size(23, 22);
			this.BrushButton.Text = "Selection mode";
			this.BrushButton.Click += new System.EventHandler(this.BrushButton_OnClick);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			// 
			// ScrollBar
			// 
			this.ScrollBar.Dock = System.Windows.Forms.DockStyle.Right;
			this.ScrollBar.Location = new System.Drawing.Point(383, 25);
			this.ScrollBar.Name = "ScrollBar";
			this.ScrollBar.Size = new System.Drawing.Size(17, 438);
			this.ScrollBar.TabIndex = 3;
			// 
			// LevelTilePanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(400, 463);
			this.Controls.Add(this.GlControl);
			this.Controls.Add(this.ScrollBar);
			this.Controls.Add(this.toolStrip1);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.HideOnClose = true;
			this.Name = "LevelTilePanel";
			this.TabText = "Tiles";
			this.Text = "Tiles Panel";
			this.Load += new System.EventHandler(this.LevelTilePanel_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

		private OpenTK.GLControl GlControl;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton ShowGridButton;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton PenButton;
		private System.Windows.Forms.ToolStripButton RectangleButton;
		private System.Windows.Forms.ToolStripButton FloodFillTileButton;
		private System.Windows.Forms.ToolStripButton BrushButton;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.VScrollBar ScrollBar;
    }
}