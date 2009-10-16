namespace RuffnTumble.Editor
{
	partial class WorldPanel
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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.PropertyBox = new System.Windows.Forms.PropertyGrid();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.LevelPreviewButton = new System.Windows.Forms.Button();
			this.PreviewWidth = new System.Windows.Forms.NumericUpDown();
			this.PreviewHeight = new System.Windows.Forms.NumericUpDown();
			this.label1 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.groupBox5 = new System.Windows.Forms.GroupBox();
			this.RemoveLevelBox = new System.Windows.Forms.Button();
			this.AddLevelBox = new System.Windows.Forms.Button();
			this.LevelsBox = new System.Windows.Forms.ListBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.LayerPropertiesBox = new System.Windows.Forms.Button();
			this.LayersBox = new System.Windows.Forms.ComboBox();
			this.OptionsBox = new System.Windows.Forms.CheckedListBox();
			this.AlphaBox = new System.Windows.Forms.TrackBar();
			this.groupBox1.SuspendLayout();
			this.groupBox4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.PreviewWidth)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.PreviewHeight)).BeginInit();
			this.groupBox5.SuspendLayout();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.AlphaBox)).BeginInit();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.PropertyBox);
			this.groupBox1.Location = new System.Drawing.Point(12, 364);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(222, 196);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Advanced properties :";
			// 
			// PropertyBox
			// 
			this.PropertyBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PropertyBox.Location = new System.Drawing.Point(3, 16);
			this.PropertyBox.Name = "PropertyBox";
			this.PropertyBox.Size = new System.Drawing.Size(216, 177);
			this.PropertyBox.TabIndex = 0;
			// 
			// groupBox4
			// 
			this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox4.Controls.Add(this.LevelPreviewButton);
			this.groupBox4.Controls.Add(this.PreviewWidth);
			this.groupBox4.Controls.Add(this.PreviewHeight);
			this.groupBox4.Controls.Add(this.label1);
			this.groupBox4.Controls.Add(this.label7);
			this.groupBox4.Location = new System.Drawing.Point(12, 290);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(222, 68);
			this.groupBox4.TabIndex = 13;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Level preview :";
			// 
			// LevelPreviewButton
			// 
			this.LevelPreviewButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.LevelPreviewButton.Location = new System.Drawing.Point(142, 11);
			this.LevelPreviewButton.Name = "LevelPreviewButton";
			this.LevelPreviewButton.Size = new System.Drawing.Size(74, 23);
			this.LevelPreviewButton.TabIndex = 12;
			this.LevelPreviewButton.Text = "Preview";
			this.LevelPreviewButton.UseVisualStyleBackColor = true;
			this.LevelPreviewButton.Click += new System.EventHandler(this.LevelPreviewButton_Click);
			// 
			// PreviewWidth
			// 
			this.PreviewWidth.Location = new System.Drawing.Point(57, 14);
			this.PreviewWidth.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.PreviewWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.PreviewWidth.Name = "PreviewWidth";
			this.PreviewWidth.Size = new System.Drawing.Size(54, 20);
			this.PreviewWidth.TabIndex = 8;
			this.PreviewWidth.Value = new decimal(new int[] {
            1024,
            0,
            0,
            0});
			// 
			// PreviewHeight
			// 
			this.PreviewHeight.Location = new System.Drawing.Point(57, 40);
			this.PreviewHeight.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.PreviewHeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.PreviewHeight.Name = "PreviewHeight";
			this.PreviewHeight.Size = new System.Drawing.Size(54, 20);
			this.PreviewHeight.TabIndex = 9;
			this.PreviewHeight.Value = new decimal(new int[] {
            768,
            0,
            0,
            0});
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 42);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(44, 13);
			this.label1.TabIndex = 11;
			this.label1.Text = "Height :";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(6, 16);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(41, 13);
			this.label7.TabIndex = 10;
			this.label7.Text = "Width :";
			// 
			// groupBox5
			// 
			this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox5.Controls.Add(this.RemoveLevelBox);
			this.groupBox5.Controls.Add(this.AddLevelBox);
			this.groupBox5.Controls.Add(this.LevelsBox);
			this.groupBox5.Location = new System.Drawing.Point(12, 12);
			this.groupBox5.Name = "groupBox5";
			this.groupBox5.Size = new System.Drawing.Size(222, 97);
			this.groupBox5.TabIndex = 14;
			this.groupBox5.TabStop = false;
			this.groupBox5.Text = "World :";
			// 
			// RemoveLevelBox
			// 
			this.RemoveLevelBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.RemoveLevelBox.Location = new System.Drawing.Point(142, 48);
			this.RemoveLevelBox.Name = "RemoveLevelBox";
			this.RemoveLevelBox.Size = new System.Drawing.Size(74, 23);
			this.RemoveLevelBox.TabIndex = 1;
			this.RemoveLevelBox.Text = "Remove";
			this.RemoveLevelBox.UseVisualStyleBackColor = true;
			// 
			// AddLevelBox
			// 
			this.AddLevelBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.AddLevelBox.Location = new System.Drawing.Point(142, 19);
			this.AddLevelBox.Name = "AddLevelBox";
			this.AddLevelBox.Size = new System.Drawing.Size(74, 23);
			this.AddLevelBox.TabIndex = 1;
			this.AddLevelBox.Text = "Add...";
			this.AddLevelBox.UseVisualStyleBackColor = true;
			// 
			// LevelsBox
			// 
			this.LevelsBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.LevelsBox.FormattingEnabled = true;
			this.LevelsBox.Location = new System.Drawing.Point(10, 19);
			this.LevelsBox.Name = "LevelsBox";
			this.LevelsBox.Size = new System.Drawing.Size(126, 69);
			this.LevelsBox.Sorted = true;
			this.LevelsBox.TabIndex = 0;
			this.LevelsBox.SelectedIndexChanged += new System.EventHandler(this.LevelsBox_SelectedIndexChanged);
			// 
			// groupBox2
			// 
			this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox2.Controls.Add(this.AlphaBox);
			this.groupBox2.Controls.Add(this.OptionsBox);
			this.groupBox2.Controls.Add(this.LayerPropertiesBox);
			this.groupBox2.Controls.Add(this.LayersBox);
			this.groupBox2.Location = new System.Drawing.Point(12, 115);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(222, 169);
			this.groupBox2.TabIndex = 15;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Layers :";
			// 
			// LayerPropertiesBox
			// 
			this.LayerPropertiesBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.LayerPropertiesBox.Location = new System.Drawing.Point(142, 17);
			this.LayerPropertiesBox.Name = "LayerPropertiesBox";
			this.LayerPropertiesBox.Size = new System.Drawing.Size(74, 23);
			this.LayerPropertiesBox.TabIndex = 1;
			this.LayerPropertiesBox.Text = "Properties...";
			this.LayerPropertiesBox.UseVisualStyleBackColor = true;
			this.LayerPropertiesBox.Click += new System.EventHandler(this.LayerPropertiesBox_Click);
			// 
			// LayersBox
			// 
			this.LayersBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.LayersBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.LayersBox.FormattingEnabled = true;
			this.LayersBox.Items.AddRange(new object[] {
            "Tiles",
            "Collisions"});
			this.LayersBox.Location = new System.Drawing.Point(9, 19);
			this.LayersBox.Name = "LayersBox";
			this.LayersBox.Size = new System.Drawing.Size(127, 21);
			this.LayersBox.TabIndex = 0;
			this.LayersBox.SelectedIndexChanged += new System.EventHandler(this.LayersBox_SelectedIndexChanged);
			// 
			// OptionsBox
			// 
			this.OptionsBox.CheckOnClick = true;
			this.OptionsBox.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.OptionsBox.FormattingEnabled = true;
			this.OptionsBox.Items.AddRange(new object[] {
            "Draw layer",
            "Draw entities",
            "Draw spawn points"});
			this.OptionsBox.Location = new System.Drawing.Point(3, 102);
			this.OptionsBox.Name = "OptionsBox";
			this.OptionsBox.Size = new System.Drawing.Size(216, 64);
			this.OptionsBox.TabIndex = 2;
			this.OptionsBox.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.OptionsBox_ItemCheck);
			// 
			// AlphaBox
			// 
			this.AlphaBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.AlphaBox.Location = new System.Drawing.Point(3, 46);
			this.AlphaBox.Maximum = 255;
			this.AlphaBox.Name = "AlphaBox";
			this.AlphaBox.Size = new System.Drawing.Size(213, 45);
			this.AlphaBox.TabIndex = 3;
			this.AlphaBox.TickFrequency = 16;
			this.AlphaBox.Scroll += new System.EventHandler(this.AlphaBox_Scroll);
			// 
			// WorldPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(244, 572);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox5);
			this.Controls.Add(this.groupBox4);
			this.Controls.Add(this.groupBox1);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.HideOnClose = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(250, 600);
			this.Name = "WorldPanel";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.TabText = "Levels";
			this.Text = "World properties";
			this.groupBox1.ResumeLayout(false);
			this.groupBox4.ResumeLayout(false);
			this.groupBox4.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.PreviewWidth)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.PreviewHeight)).EndInit();
			this.groupBox5.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.AlphaBox)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.PropertyGrid PropertyBox;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.NumericUpDown PreviewWidth;
        private System.Windows.Forms.NumericUpDown PreviewHeight;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Button LevelPreviewButton;
		private System.Windows.Forms.GroupBox groupBox5;
		private System.Windows.Forms.Button RemoveLevelBox;
		private System.Windows.Forms.Button AddLevelBox;
		private System.Windows.Forms.ListBox LevelsBox;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.ComboBox LayersBox;
		private System.Windows.Forms.Button LayerPropertiesBox;
		private System.Windows.Forms.CheckedListBox OptionsBox;
		private System.Windows.Forms.TrackBar AlphaBox;
	}
}
