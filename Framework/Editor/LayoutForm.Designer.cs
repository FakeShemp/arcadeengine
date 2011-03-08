namespace ArcEngine.Editor
{
    partial class LayoutForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LayoutForm));
			this.RenderControl = new OpenTK.GLControl();
			this.panel1 = new System.Windows.Forms.Panel();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.ElementsBox = new System.Windows.Forms.ListBox();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.DeleteButton = new System.Windows.Forms.ToolStripButton();
			this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
			this.AddButtonButton = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.ResizeToFitButton = new System.Windows.Forms.ToolStripButton();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.ElementPropertyBox = new System.Windows.Forms.PropertyGrid();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.GuiPropertyBox = new System.Windows.Forms.PropertyGrid();
			this.DrawTimer = new System.Windows.Forms.Timer(this.components);
			this.panel1.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// RenderControl
			// 
			this.RenderControl.BackColor = System.Drawing.Color.Black;
			this.RenderControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.RenderControl.Location = new System.Drawing.Point(202, 0);
			this.RenderControl.Name = "RenderControl";
			this.RenderControl.Size = new System.Drawing.Size(632, 692);
			this.RenderControl.TabIndex = 0;
			this.RenderControl.VSync = true;
			this.RenderControl.Paint += new System.Windows.Forms.PaintEventHandler(this.RenderControl_Paint);
			this.RenderControl.MouseClick += new System.Windows.Forms.MouseEventHandler(this.RenderControl_MouseClick);
			this.RenderControl.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.RenderControl_MouseDoubleClick);
			this.RenderControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RenderControl_MouseDown);
			this.RenderControl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.RenderControl_MouseMove);
			this.RenderControl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RenderControl_MouseUp);
			this.RenderControl.Resize += new System.EventHandler(this.RenderControl_Resize);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.groupBox3);
			this.panel1.Controls.Add(this.groupBox2);
			this.panel1.Controls.Add(this.groupBox1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(202, 692);
			this.panel1.TabIndex = 1;
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.ElementsBox);
			this.groupBox3.Controls.Add(this.toolStrip1);
			this.groupBox3.Location = new System.Drawing.Point(0, 0);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(200, 170);
			this.groupBox3.TabIndex = 3;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Elements :";
			// 
			// ElementsBox
			// 
			this.ElementsBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ElementsBox.FormattingEnabled = true;
			this.ElementsBox.Location = new System.Drawing.Point(3, 41);
			this.ElementsBox.Name = "ElementsBox";
			this.ElementsBox.Size = new System.Drawing.Size(194, 126);
			this.ElementsBox.Sorted = true;
			this.ElementsBox.TabIndex = 0;
			this.ElementsBox.SelectedIndexChanged += new System.EventHandler(this.ElementsBox_SelectedIndexChanged);
			// 
			// toolStrip1
			// 
			this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DeleteButton,
            this.toolStripDropDownButton1,
            this.toolStripSeparator1,
            this.ResizeToFitButton});
			this.toolStrip1.Location = new System.Drawing.Point(3, 16);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.toolStrip1.Size = new System.Drawing.Size(194, 25);
			this.toolStrip1.TabIndex = 2;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// DeleteButton
			// 
			this.DeleteButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.DeleteButton.Image = ((System.Drawing.Image) (resources.GetObject("DeleteButton.Image")));
			this.DeleteButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.DeleteButton.Name = "DeleteButton";
			this.DeleteButton.Size = new System.Drawing.Size(23, 22);
			this.DeleteButton.Text = "Remove the selected element";
			this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
			// 
			// toolStripDropDownButton1
			// 
			this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddButtonButton});
			this.toolStripDropDownButton1.Image = ((System.Drawing.Image) (resources.GetObject("toolStripDropDownButton1.Image")));
			this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
			this.toolStripDropDownButton1.Size = new System.Drawing.Size(29, 22);
			this.toolStripDropDownButton1.Text = "toolStripDropDownButton1";
			// 
			// AddButtonButton
			// 
			this.AddButtonButton.Image = ((System.Drawing.Image) (resources.GetObject("AddButtonButton.Image")));
			this.AddButtonButton.Name = "AddButtonButton";
			this.AddButtonButton.Size = new System.Drawing.Size(144, 22);
			this.AddButtonButton.Text = "Add a Button";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// ResizeToFitButton
			// 
			this.ResizeToFitButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.ResizeToFitButton.Image = ((System.Drawing.Image) (resources.GetObject("ResizeToFitButton.Image")));
			this.ResizeToFitButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.ResizeToFitButton.Name = "ResizeToFitButton";
			this.ResizeToFitButton.Size = new System.Drawing.Size(23, 22);
			this.ResizeToFitButton.Text = "toolStripButton1";
			this.ResizeToFitButton.Click += new System.EventHandler(this.ResizeToFitButton_Click);
			// 
			// groupBox2
			// 
			this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.groupBox2.Controls.Add(this.ElementPropertyBox);
			this.groupBox2.Location = new System.Drawing.Point(0, 179);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(200, 212);
			this.groupBox2.TabIndex = 2;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Element properties :";
			// 
			// ElementPropertyBox
			// 
			this.ElementPropertyBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ElementPropertyBox.Location = new System.Drawing.Point(3, 16);
			this.ElementPropertyBox.Name = "ElementPropertyBox";
			this.ElementPropertyBox.Size = new System.Drawing.Size(194, 193);
			this.ElementPropertyBox.TabIndex = 0;
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.groupBox1.Controls.Add(this.GuiPropertyBox);
			this.groupBox1.Location = new System.Drawing.Point(0, 397);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(200, 292);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Layout properties :";
			// 
			// GuiPropertyBox
			// 
			this.GuiPropertyBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.GuiPropertyBox.Location = new System.Drawing.Point(3, 16);
			this.GuiPropertyBox.Name = "GuiPropertyBox";
			this.GuiPropertyBox.Size = new System.Drawing.Size(194, 273);
			this.GuiPropertyBox.TabIndex = 0;
			// 
			// DrawTimer
			// 
			this.DrawTimer.Interval = 50;
			this.DrawTimer.Tick += new System.EventHandler(this.DrawTimer_Tick);
			// 
			// LayoutForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(834, 692);
			this.Controls.Add(this.RenderControl);
			this.Controls.Add(this.panel1);
			this.Name = "LayoutForm";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.LayoutForm_FormClosed);
			this.Load += new System.EventHandler(this.LayoutForm_Load);
			this.panel1.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        private OpenTK.GLControl RenderControl;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.PropertyGrid GuiPropertyBox;
        private System.Windows.Forms.Timer DrawTimer;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.PropertyGrid ElementPropertyBox;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.ListBox ElementsBox;
		private System.Windows.Forms.ToolStripButton DeleteButton;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
		private System.Windows.Forms.ToolStripMenuItem AddButtonButton;
		private System.Windows.Forms.ToolStripButton ResizeToFitButton;
    }
}
