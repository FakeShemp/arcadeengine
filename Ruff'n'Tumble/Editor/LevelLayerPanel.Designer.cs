namespace ArcEngine.Games.RuffnTumble.Editor
{
    partial class LevelLayerPanel
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LevelLayerPanel));
			this.PropertiesBox = new System.Windows.Forms.GroupBox();
			this.propertyGridBox = new System.Windows.Forms.PropertyGrid();
			this.PropertyToolStrip = new System.Windows.Forms.ToolStrip();
			this.FindButton = new System.Windows.Forms.ToolStripButton();
			this.DeleteButton = new System.Windows.Forms.ToolStripButton();
			this.LayerPropertiesBox = new System.Windows.Forms.GroupBox();
			this.LayerPropertyGrid = new System.Windows.Forms.PropertyGrid();
			this.LayerBox = new System.Windows.Forms.GroupBox();
			this.LayersBox = new System.Windows.Forms.ListBox();
			this.LayerToolStrip = new System.Windows.Forms.ToolStrip();
			this.ToolAddLayer = new System.Windows.Forms.ToolStripButton();
			this.ToolRemoveLayer = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.LayerUpButton = new System.Windows.Forms.ToolStripButton();
			this.LayerDownButton = new System.Windows.Forms.ToolStripButton();
			this.PropertiesBox.SuspendLayout();
			this.PropertyToolStrip.SuspendLayout();
			this.LayerPropertiesBox.SuspendLayout();
			this.LayerBox.SuspendLayout();
			this.LayerToolStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// PropertiesBox
			// 
			this.PropertiesBox.Controls.Add(this.propertyGridBox);
			this.PropertiesBox.Controls.Add(this.PropertyToolStrip);
			this.PropertiesBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PropertiesBox.Location = new System.Drawing.Point(0, 368);
			this.PropertiesBox.Name = "PropertiesBox";
			this.PropertiesBox.Size = new System.Drawing.Size(231, 283);
			this.PropertiesBox.TabIndex = 18;
			this.PropertiesBox.TabStop = false;
			this.PropertiesBox.Text = "Properties :";
			// 
			// propertyGridBox
			// 
			this.propertyGridBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.propertyGridBox.Location = new System.Drawing.Point(3, 41);
			this.propertyGridBox.Name = "propertyGridBox";
			this.propertyGridBox.Size = new System.Drawing.Size(225, 239);
			this.propertyGridBox.TabIndex = 0;
			this.propertyGridBox.ToolbarVisible = false;
			// 
			// PropertyToolStrip
			// 
			this.PropertyToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.PropertyToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FindButton,
            this.DeleteButton});
			this.PropertyToolStrip.Location = new System.Drawing.Point(3, 16);
			this.PropertyToolStrip.Name = "PropertyToolStrip";
			this.PropertyToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.PropertyToolStrip.Size = new System.Drawing.Size(225, 25);
			this.PropertyToolStrip.TabIndex = 1;
			this.PropertyToolStrip.Text = "toolStrip1";
			// 
			// FindButton
			// 
			this.FindButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.FindButton.Image = ((System.Drawing.Image)(resources.GetObject("FindButton.Image")));
			this.FindButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.FindButton.Name = "FindButton";
			this.FindButton.Size = new System.Drawing.Size(23, 22);
			this.FindButton.ToolTipText = "Find the selected object";
			this.FindButton.Click += new System.EventHandler(this.FindButton_Click);
			// 
			// DeleteButton
			// 
			this.DeleteButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.DeleteButton.Image = ((System.Drawing.Image)(resources.GetObject("DeleteButton.Image")));
			this.DeleteButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.DeleteButton.Name = "DeleteButton";
			this.DeleteButton.Size = new System.Drawing.Size(23, 22);
			this.DeleteButton.ToolTipText = "Erase the selected object";
			this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
			// 
			// LayerPropertiesBox
			// 
			this.LayerPropertiesBox.Controls.Add(this.LayerPropertyGrid);
			this.LayerPropertiesBox.Dock = System.Windows.Forms.DockStyle.Top;
			this.LayerPropertiesBox.Location = new System.Drawing.Point(0, 138);
			this.LayerPropertiesBox.Name = "LayerPropertiesBox";
			this.LayerPropertiesBox.Size = new System.Drawing.Size(231, 230);
			this.LayerPropertiesBox.TabIndex = 17;
			this.LayerPropertiesBox.TabStop = false;
			this.LayerPropertiesBox.Text = "Layer properties :";
			// 
			// LayerPropertyGrid
			// 
			this.LayerPropertyGrid.CommandsVisibleIfAvailable = false;
			this.LayerPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LayerPropertyGrid.Location = new System.Drawing.Point(3, 16);
			this.LayerPropertyGrid.Name = "LayerPropertyGrid";
			this.LayerPropertyGrid.Size = new System.Drawing.Size(225, 211);
			this.LayerPropertyGrid.TabIndex = 24;
			this.LayerPropertyGrid.ToolbarVisible = false;
			// 
			// LayerBox
			// 
			this.LayerBox.Controls.Add(this.LayersBox);
			this.LayerBox.Controls.Add(this.LayerToolStrip);
			this.LayerBox.Dock = System.Windows.Forms.DockStyle.Top;
			this.LayerBox.Location = new System.Drawing.Point(0, 0);
			this.LayerBox.Name = "LayerBox";
			this.LayerBox.Size = new System.Drawing.Size(231, 138);
			this.LayerBox.TabIndex = 16;
			this.LayerBox.TabStop = false;
			this.LayerBox.Text = "Layers :";
			// 
			// LayersBox
			// 
			this.LayersBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LayersBox.Location = new System.Drawing.Point(3, 16);
			this.LayersBox.Name = "LayersBox";
			this.LayersBox.Size = new System.Drawing.Size(225, 82);
			this.LayersBox.TabIndex = 0;
			this.LayersBox.SelectedIndexChanged += new System.EventHandler(this.LayerBox_OnSelectedIndexChanged);
			// 
			// LayerToolStrip
			// 
			this.LayerToolStrip.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.LayerToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.LayerToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolAddLayer,
            this.ToolRemoveLayer,
            this.toolStripSeparator2,
            this.LayerUpButton,
            this.LayerDownButton});
			this.LayerToolStrip.Location = new System.Drawing.Point(3, 110);
			this.LayerToolStrip.Name = "LayerToolStrip";
			this.LayerToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.LayerToolStrip.Size = new System.Drawing.Size(225, 25);
			this.LayerToolStrip.Stretch = true;
			this.LayerToolStrip.TabIndex = 1;
			this.LayerToolStrip.Text = "toolStrip1";
			// 
			// ToolAddLayer
			// 
			this.ToolAddLayer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.ToolAddLayer.Image = ((System.Drawing.Image)(resources.GetObject("ToolAddLayer.Image")));
			this.ToolAddLayer.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.ToolAddLayer.Name = "ToolAddLayer";
			this.ToolAddLayer.Size = new System.Drawing.Size(23, 22);
			this.ToolAddLayer.Text = "Add a new layer...";
			this.ToolAddLayer.Click += new System.EventHandler(this.ToolAddLayer_Click);
			// 
			// ToolRemoveLayer
			// 
			this.ToolRemoveLayer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.ToolRemoveLayer.Image = ((System.Drawing.Image)(resources.GetObject("ToolRemoveLayer.Image")));
			this.ToolRemoveLayer.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.ToolRemoveLayer.Name = "ToolRemoveLayer";
			this.ToolRemoveLayer.Size = new System.Drawing.Size(23, 22);
			this.ToolRemoveLayer.Text = "Remove layer";
			this.ToolRemoveLayer.Click += new System.EventHandler(this.ToolRemoveLayer_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			// 
			// LayerUpButton
			// 
			this.LayerUpButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.LayerUpButton.Image = ((System.Drawing.Image)(resources.GetObject("LayerUpButton.Image")));
			this.LayerUpButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.LayerUpButton.Name = "LayerUpButton";
			this.LayerUpButton.Size = new System.Drawing.Size(23, 22);
			this.LayerUpButton.Text = "Increase layer priority order";
			// 
			// LayerDownButton
			// 
			this.LayerDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.LayerDownButton.Image = ((System.Drawing.Image)(resources.GetObject("LayerDownButton.Image")));
			this.LayerDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.LayerDownButton.Name = "LayerDownButton";
			this.LayerDownButton.Size = new System.Drawing.Size(23, 22);
			this.LayerDownButton.Text = "Decrease layer priority order";
			// 
			// LevelLayerPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(231, 651);
			this.Controls.Add(this.PropertiesBox);
			this.Controls.Add(this.LayerPropertiesBox);
			this.Controls.Add(this.LayerBox);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.HideOnClose = true;
			this.Name = "LevelLayerPanel";
			this.TabText = "Layers";
			this.Text = "Level Layer Panel";
			this.PropertiesBox.ResumeLayout(false);
			this.PropertiesBox.PerformLayout();
			this.PropertyToolStrip.ResumeLayout(false);
			this.PropertyToolStrip.PerformLayout();
			this.LayerPropertiesBox.ResumeLayout(false);
			this.LayerBox.ResumeLayout(false);
			this.LayerBox.PerformLayout();
			this.LayerToolStrip.ResumeLayout(false);
			this.LayerToolStrip.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox PropertiesBox;
        private System.Windows.Forms.PropertyGrid propertyGridBox;
        private System.Windows.Forms.ToolStrip PropertyToolStrip;
        private System.Windows.Forms.ToolStripButton FindButton;
        private System.Windows.Forms.ToolStripButton DeleteButton;
        private System.Windows.Forms.GroupBox LayerPropertiesBox;
		private System.Windows.Forms.PropertyGrid LayerPropertyGrid;
        private System.Windows.Forms.GroupBox LayerBox;
        private System.Windows.Forms.ListBox LayersBox;
        private System.Windows.Forms.ToolStrip LayerToolStrip;
        private System.Windows.Forms.ToolStripButton ToolAddLayer;
        private System.Windows.Forms.ToolStripButton ToolRemoveLayer;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton LayerUpButton;
        private System.Windows.Forms.ToolStripButton LayerDownButton;
    }
}