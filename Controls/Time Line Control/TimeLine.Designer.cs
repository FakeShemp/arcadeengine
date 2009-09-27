namespace ArcEngine.Controls.TimeLineControl
{
	partial class TimeLine
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TimeLine));
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.LayerScrollBar = new System.Windows.Forms.VScrollBar();
			this.TimeScrollBar = new System.Windows.Forms.HScrollBar();
			this.ElapsedTimeBox = new System.Windows.Forms.ToolStripStatusLabel();
			this.TimePanel = new System.Windows.Forms.Panel();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.AddLayerbox = new System.Windows.Forms.ToolStripButton();
			this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.panel1 = new System.Windows.Forms.Panel();
			this.toolStrip2 = new System.Windows.Forms.ToolStrip();
			this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
			this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
			this.statusStrip1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.panel1.SuspendLayout();
			this.toolStrip2.SuspendLayout();
			this.SuspendLayout();
			// 
			// statusStrip1
			// 
			this.statusStrip1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.statusStrip1.AutoSize = false;
			this.statusStrip1.Dock = System.Windows.Forms.DockStyle.None;
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ElapsedTimeBox});
			this.statusStrip1.Location = new System.Drawing.Point(210, 226);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(191, 22);
			this.statusStrip1.SizingGrip = false;
			this.statusStrip1.Stretch = false;
			this.statusStrip1.TabIndex = 2;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// LayerScrollBar
			// 
			this.LayerScrollBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
							| System.Windows.Forms.AnchorStyles.Right)));
			this.LayerScrollBar.Location = new System.Drawing.Point(706, 48);
			this.LayerScrollBar.Name = "LayerScrollBar";
			this.LayerScrollBar.Size = new System.Drawing.Size(22, 178);
			this.LayerScrollBar.TabIndex = 1;
			// 
			// TimeScrollBar
			// 
			this.TimeScrollBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
							| System.Windows.Forms.AnchorStyles.Right)));
			this.TimeScrollBar.Location = new System.Drawing.Point(404, 226);
			this.TimeScrollBar.Name = "TimeScrollBar";
			this.TimeScrollBar.Size = new System.Drawing.Size(302, 22);
			this.TimeScrollBar.TabIndex = 0;
			// 
			// ElapsedTimeBox
			// 
			this.ElapsedTimeBox.Name = "ElapsedTimeBox";
			this.ElapsedTimeBox.Size = new System.Drawing.Size(33, 17);
			this.ElapsedTimeBox.Text = "0:00s";
			// 
			// TimePanel
			// 
			this.TimePanel.BackColor = System.Drawing.SystemColors.ControlLight;
			this.TimePanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.TimePanel.Location = new System.Drawing.Point(210, 0);
			this.TimePanel.Name = "TimePanel";
			this.TimePanel.Size = new System.Drawing.Size(518, 28);
			this.TimePanel.TabIndex = 3;
			// 
			// toolStrip1
			// 
			this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddLayerbox,
            this.toolStripButton2,
            this.toolStripSeparator1,
            this.toolStripButton1,
            this.toolStripButton3,
            this.toolStripSeparator2});
			this.toolStrip1.Location = new System.Drawing.Point(0, 223);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.toolStrip1.Size = new System.Drawing.Size(210, 25);
			this.toolStrip1.TabIndex = 1;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// AddLayerbox
			// 
			this.AddLayerbox.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.AddLayerbox.Image = ((System.Drawing.Image)(resources.GetObject("AddLayerbox.Image")));
			this.AddLayerbox.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.AddLayerbox.Name = "AddLayerbox";
			this.AddLayerbox.Size = new System.Drawing.Size(23, 22);
			this.AddLayerbox.Text = "Add a new layer";
			// 
			// toolStripButton2
			// 
			this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
			this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton2.Name = "toolStripButton2";
			this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
			this.toolStripButton2.Text = "Remove layer";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// toolStripButton1
			// 
			this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
			this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton1.Name = "toolStripButton1";
			this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
			this.toolStripButton1.Text = "Move up layer";
			// 
			// toolStripButton3
			// 
			this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
			this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton3.Name = "toolStripButton3";
			this.toolStripButton3.Size = new System.Drawing.Size(23, 22);
			this.toolStripButton3.Text = "Move down layer";
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.toolStrip2);
			this.panel1.Controls.Add(this.toolStrip1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(210, 248);
			this.panel1.TabIndex = 4;
			// 
			// toolStrip2
			// 
			this.toolStrip2.AutoSize = false;
			this.toolStrip2.BackColor = System.Drawing.SystemColors.ControlLight;
			this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton4,
            this.toolStripButton5});
			this.toolStrip2.Location = new System.Drawing.Point(0, 0);
			this.toolStrip2.Name = "toolStrip2";
			this.toolStrip2.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.toolStrip2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.toolStrip2.Size = new System.Drawing.Size(210, 28);
			this.toolStrip2.TabIndex = 2;
			this.toolStrip2.Text = "toolStrip2";
			// 
			// toolStripButton4
			// 
			this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton4.Image")));
			this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton4.Name = "toolStripButton4";
			this.toolStripButton4.Size = new System.Drawing.Size(23, 25);
			this.toolStripButton4.Text = "Lock layers";
			// 
			// toolStripButton5
			// 
			this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButton5.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton5.Image")));
			this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton5.Name = "toolStripButton5";
			this.toolStripButton5.Size = new System.Drawing.Size(23, 25);
			this.toolStripButton5.Text = "Show / hide layers";
			// 
			// TimeLine
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.TimePanel);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.TimeScrollBar);
			this.Controls.Add(this.LayerScrollBar);
			this.Controls.Add(this.statusStrip1);
			this.Name = "TimeLine";
			this.Size = new System.Drawing.Size(728, 248);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.TimeLine_Paint);
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.toolStrip2.ResumeLayout(false);
			this.toolStrip2.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.VScrollBar LayerScrollBar;
		private System.Windows.Forms.HScrollBar TimeScrollBar;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel ElapsedTimeBox;
		private System.Windows.Forms.Panel TimePanel;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton AddLayerbox;
		private System.Windows.Forms.ToolStripButton toolStripButton2;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton toolStripButton1;
		private System.Windows.Forms.ToolStripButton toolStripButton3;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.ToolStrip toolStrip2;
		private System.Windows.Forms.ToolStripButton toolStripButton4;
		private System.Windows.Forms.ToolStripButton toolStripButton5;


	}
}
