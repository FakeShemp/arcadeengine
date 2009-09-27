namespace ArcEngine.Games.RuffnTumble.Editor
{
	partial class LevelBrushPanel
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LevelBrushPanel));
			this.GlControl = new OpenTK.GLControl();
			this.MainToolStrip = new System.Windows.Forms.ToolStrip();
			this.AddBrushButton = new System.Windows.Forms.ToolStripButton();
			this.DeleteBrushButton = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.VScroller = new System.Windows.Forms.VScrollBar();
			this.MainToolStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// GlControl
			// 
			this.GlControl.BackColor = System.Drawing.Color.Black;
			this.GlControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.GlControl.Location = new System.Drawing.Point(0, 25);
			this.GlControl.Name = "GlControl";
			this.GlControl.Size = new System.Drawing.Size(267, 447);
			this.GlControl.TabIndex = 0;
			this.GlControl.Paint += new System.Windows.Forms.PaintEventHandler(this.GlControl_Paint);
			this.GlControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GlControl_MouseDown);
			this.GlControl.Resize += new System.EventHandler(this.GlControl_Resize);
			// 
			// MainToolStrip
			// 
			this.MainToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.MainToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddBrushButton,
            this.DeleteBrushButton,
            this.toolStripSeparator1});
			this.MainToolStrip.Location = new System.Drawing.Point(0, 0);
			this.MainToolStrip.Name = "MainToolStrip";
			this.MainToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.MainToolStrip.Size = new System.Drawing.Size(284, 25);
			this.MainToolStrip.TabIndex = 1;
			this.MainToolStrip.Text = "toolStrip1";
			// 
			// AddBrushButton
			// 
			this.AddBrushButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.AddBrushButton.Image = ((System.Drawing.Image)(resources.GetObject("AddBrushButton.Image")));
			this.AddBrushButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.AddBrushButton.Name = "AddBrushButton";
			this.AddBrushButton.Size = new System.Drawing.Size(23, 22);
			this.AddBrushButton.Text = "Add brush...";
			this.AddBrushButton.Click += new System.EventHandler(this.AddBrushButton_Click);
			// 
			// DeleteBrushButton
			// 
			this.DeleteBrushButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.DeleteBrushButton.Image = ((System.Drawing.Image)(resources.GetObject("DeleteBrushButton.Image")));
			this.DeleteBrushButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.DeleteBrushButton.Name = "DeleteBrushButton";
			this.DeleteBrushButton.Size = new System.Drawing.Size(23, 22);
			this.DeleteBrushButton.Text = "Delete brush";
			this.DeleteBrushButton.Click += new System.EventHandler(this.DeleteBrushButton_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// VScroller
			// 
			this.VScroller.Dock = System.Windows.Forms.DockStyle.Right;
			this.VScroller.Location = new System.Drawing.Point(267, 25);
			this.VScroller.Name = "VScroller";
			this.VScroller.Size = new System.Drawing.Size(17, 447);
			this.VScroller.TabIndex = 2;
			// 
			// LevelBrushPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 472);
			this.Controls.Add(this.GlControl);
			this.Controls.Add(this.VScroller);
			this.Controls.Add(this.MainToolStrip);
			this.HideOnClose = true;
			this.Name = "LevelBrushPanel";
			this.TabText = "Brushes";
			this.Text = "Brushes";
			this.MainToolStrip.ResumeLayout(false);
			this.MainToolStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private OpenTK.GLControl GlControl;
		private System.Windows.Forms.ToolStrip MainToolStrip;
		private System.Windows.Forms.ToolStripButton AddBrushButton;
		private System.Windows.Forms.ToolStripButton DeleteBrushButton;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.VScrollBar VScroller;
	}
}