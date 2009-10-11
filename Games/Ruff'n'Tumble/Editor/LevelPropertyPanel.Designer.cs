namespace RuffnTumble.Editor
{
	partial class LevelPropertyPanel
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
			this.DesiredLevelWidth = new System.Windows.Forms.NumericUpDown();
			this.DesiredLevelHeight = new System.Windows.Forms.NumericUpDown();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.ResizeLevelButton = new System.Windows.Forms.Button();
			this.LevelHeightLabel = new System.Windows.Forms.Label();
			this.LevelWidthLabel = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.ResizeBlockButton = new System.Windows.Forms.Button();
			this.DesiredBlockWidth = new System.Windows.Forms.NumericUpDown();
			this.BlockHeightLabel = new System.Windows.Forms.Label();
			this.DesiredBlockHeight = new System.Windows.Forms.NumericUpDown();
			this.label6 = new System.Windows.Forms.Label();
			this.BlockWidthLabel = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.LevelPreviewButton = new System.Windows.Forms.Button();
			this.PreviewWidth = new System.Windows.Forms.NumericUpDown();
			this.PreviewHeight = new System.Windows.Forms.NumericUpDown();
			this.label1 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.DesiredLevelWidth)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.DesiredLevelHeight)).BeginInit();
			this.groupBox3.SuspendLayout();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.DesiredBlockWidth)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.DesiredBlockHeight)).BeginInit();
			this.groupBox4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.PreviewWidth)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.PreviewHeight)).BeginInit();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.PropertyBox);
			this.groupBox1.Location = new System.Drawing.Point(12, 298);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(212, 274);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Advanced properties :";
			// 
			// PropertyBox
			// 
			this.PropertyBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PropertyBox.Location = new System.Drawing.Point(3, 16);
			this.PropertyBox.Name = "PropertyBox";
			this.PropertyBox.Size = new System.Drawing.Size(206, 255);
			this.PropertyBox.TabIndex = 0;
			// 
			// DesiredLevelWidth
			// 
			this.DesiredLevelWidth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.DesiredLevelWidth.Location = new System.Drawing.Point(137, 14);
			this.DesiredLevelWidth.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.DesiredLevelWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.DesiredLevelWidth.Name = "DesiredLevelWidth";
			this.DesiredLevelWidth.Size = new System.Drawing.Size(66, 20);
			this.DesiredLevelWidth.TabIndex = 3;
			this.DesiredLevelWidth.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// DesiredLevelHeight
			// 
			this.DesiredLevelHeight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.DesiredLevelHeight.Location = new System.Drawing.Point(137, 40);
			this.DesiredLevelHeight.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.DesiredLevelHeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.DesiredLevelHeight.Name = "DesiredLevelHeight";
			this.DesiredLevelHeight.Size = new System.Drawing.Size(66, 20);
			this.DesiredLevelHeight.TabIndex = 4;
			this.DesiredLevelHeight.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(86, 42);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(44, 13);
			this.label3.TabIndex = 7;
			this.label3.Text = "Height :";
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(86, 16);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(41, 13);
			this.label2.TabIndex = 6;
			this.label2.Text = "Width :";
			// 
			// groupBox3
			// 
			this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox3.Controls.Add(this.ResizeLevelButton);
			this.groupBox3.Controls.Add(this.DesiredLevelWidth);
			this.groupBox3.Controls.Add(this.LevelHeightLabel);
			this.groupBox3.Controls.Add(this.DesiredLevelHeight);
			this.groupBox3.Controls.Add(this.label3);
			this.groupBox3.Controls.Add(this.LevelWidthLabel);
			this.groupBox3.Controls.Add(this.label2);
			this.groupBox3.Controls.Add(this.label5);
			this.groupBox3.Controls.Add(this.label4);
			this.groupBox3.Location = new System.Drawing.Point(12, 12);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(214, 100);
			this.groupBox3.TabIndex = 10;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Level size :";
			// 
			// ResizeLevelButton
			// 
			this.ResizeLevelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.ResizeLevelButton.Location = new System.Drawing.Point(128, 66);
			this.ResizeLevelButton.Name = "ResizeLevelButton";
			this.ResizeLevelButton.Size = new System.Drawing.Size(75, 23);
			this.ResizeLevelButton.TabIndex = 10;
			this.ResizeLevelButton.Text = "Resize";
			this.ResizeLevelButton.UseVisualStyleBackColor = true;
			this.ResizeLevelButton.Click += new System.EventHandler(this.ResizeButton_Click);
			// 
			// LevelHeightLabel
			// 
			this.LevelHeightLabel.AutoSize = true;
			this.LevelHeightLabel.Location = new System.Drawing.Point(53, 38);
			this.LevelHeightLabel.Name = "LevelHeightLabel";
			this.LevelHeightLabel.Size = new System.Drawing.Size(16, 13);
			this.LevelHeightLabel.TabIndex = 9;
			this.LevelHeightLabel.Text = "   ";
			// 
			// LevelWidthLabel
			// 
			this.LevelWidthLabel.AutoSize = true;
			this.LevelWidthLabel.Location = new System.Drawing.Point(53, 16);
			this.LevelWidthLabel.Name = "LevelWidthLabel";
			this.LevelWidthLabel.Size = new System.Drawing.Size(16, 13);
			this.LevelWidthLabel.TabIndex = 8;
			this.LevelWidthLabel.Text = "   ";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(3, 38);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(44, 13);
			this.label5.TabIndex = 7;
			this.label5.Text = "Height :";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(6, 16);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(41, 13);
			this.label4.TabIndex = 6;
			this.label4.Text = "Width :";
			// 
			// groupBox2
			// 
			this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox2.Controls.Add(this.ResizeBlockButton);
			this.groupBox2.Controls.Add(this.DesiredBlockWidth);
			this.groupBox2.Controls.Add(this.BlockHeightLabel);
			this.groupBox2.Controls.Add(this.DesiredBlockHeight);
			this.groupBox2.Controls.Add(this.label6);
			this.groupBox2.Controls.Add(this.BlockWidthLabel);
			this.groupBox2.Controls.Add(this.label8);
			this.groupBox2.Controls.Add(this.label9);
			this.groupBox2.Controls.Add(this.label10);
			this.groupBox2.Location = new System.Drawing.Point(12, 118);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(214, 100);
			this.groupBox2.TabIndex = 12;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Block size :";
			// 
			// ResizeBlockButton
			// 
			this.ResizeBlockButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.ResizeBlockButton.Location = new System.Drawing.Point(128, 66);
			this.ResizeBlockButton.Name = "ResizeBlockButton";
			this.ResizeBlockButton.Size = new System.Drawing.Size(75, 23);
			this.ResizeBlockButton.TabIndex = 10;
			this.ResizeBlockButton.Text = "Resize";
			this.ResizeBlockButton.UseVisualStyleBackColor = true;
			this.ResizeBlockButton.Click += new System.EventHandler(this.ResizeBlockButton_Click);
			// 
			// DesiredBlockWidth
			// 
			this.DesiredBlockWidth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.DesiredBlockWidth.Location = new System.Drawing.Point(137, 14);
			this.DesiredBlockWidth.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.DesiredBlockWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.DesiredBlockWidth.Name = "DesiredBlockWidth";
			this.DesiredBlockWidth.Size = new System.Drawing.Size(66, 20);
			this.DesiredBlockWidth.TabIndex = 3;
			this.DesiredBlockWidth.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// BlockHeightLabel
			// 
			this.BlockHeightLabel.AutoSize = true;
			this.BlockHeightLabel.Location = new System.Drawing.Point(53, 38);
			this.BlockHeightLabel.Name = "BlockHeightLabel";
			this.BlockHeightLabel.Size = new System.Drawing.Size(16, 13);
			this.BlockHeightLabel.TabIndex = 9;
			this.BlockHeightLabel.Text = "   ";
			// 
			// DesiredBlockHeight
			// 
			this.DesiredBlockHeight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.DesiredBlockHeight.Location = new System.Drawing.Point(137, 40);
			this.DesiredBlockHeight.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.DesiredBlockHeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.DesiredBlockHeight.Name = "DesiredBlockHeight";
			this.DesiredBlockHeight.Size = new System.Drawing.Size(66, 20);
			this.DesiredBlockHeight.TabIndex = 4;
			this.DesiredBlockHeight.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// label6
			// 
			this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(86, 42);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(44, 13);
			this.label6.TabIndex = 7;
			this.label6.Text = "Height :";
			// 
			// BlockWidthLabel
			// 
			this.BlockWidthLabel.AutoSize = true;
			this.BlockWidthLabel.Location = new System.Drawing.Point(53, 16);
			this.BlockWidthLabel.Name = "BlockWidthLabel";
			this.BlockWidthLabel.Size = new System.Drawing.Size(16, 13);
			this.BlockWidthLabel.TabIndex = 8;
			this.BlockWidthLabel.Text = "   ";
			// 
			// label8
			// 
			this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(86, 16);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(41, 13);
			this.label8.TabIndex = 6;
			this.label8.Text = "Width :";
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(3, 38);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(44, 13);
			this.label9.TabIndex = 7;
			this.label9.Text = "Height :";
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(6, 16);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(41, 13);
			this.label10.TabIndex = 6;
			this.label10.Text = "Width :";
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
			this.groupBox4.Location = new System.Drawing.Point(12, 224);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(214, 68);
			this.groupBox4.TabIndex = 13;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Level preview :";
			// 
			// LevelPreviewButton
			// 
			this.LevelPreviewButton.Location = new System.Drawing.Point(133, 39);
			this.LevelPreviewButton.Name = "LevelPreviewButton";
			this.LevelPreviewButton.Size = new System.Drawing.Size(75, 23);
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
			this.PreviewWidth.Size = new System.Drawing.Size(66, 20);
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
			this.PreviewHeight.Size = new System.Drawing.Size(66, 20);
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
			// LevelPropertyPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(236, 584);
			this.Controls.Add(this.groupBox4);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.HideOnClose = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "LevelPropertyPanel";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.TabText = "Level";
			this.Text = "Level properties";
			this.groupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.DesiredLevelWidth)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.DesiredLevelHeight)).EndInit();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.DesiredBlockWidth)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.DesiredBlockHeight)).EndInit();
			this.groupBox4.ResumeLayout(false);
			this.groupBox4.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.PreviewWidth)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.PreviewHeight)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.NumericUpDown DesiredLevelWidth;
		private System.Windows.Forms.NumericUpDown DesiredLevelHeight;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Label LevelHeightLabel;
		private System.Windows.Forms.Label LevelWidthLabel;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button ResizeLevelButton;
        private System.Windows.Forms.PropertyGrid PropertyBox;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button ResizeBlockButton;
		private System.Windows.Forms.NumericUpDown DesiredBlockWidth;
		private System.Windows.Forms.Label BlockHeightLabel;
		private System.Windows.Forms.NumericUpDown DesiredBlockHeight;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label BlockWidthLabel;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.NumericUpDown PreviewWidth;
        private System.Windows.Forms.NumericUpDown PreviewHeight;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Button LevelPreviewButton;
	}
}
