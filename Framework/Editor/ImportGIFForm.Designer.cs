namespace ArcEngine.Editor
{
	partial class ImportGIFForm
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
			this.CancelBox = new System.Windows.Forms.Button();
			this.ImportBox = new System.Windows.Forms.Button();
			this.PreviewBox = new System.Windows.Forms.PictureBox();
			this.TextureGroup = new System.Windows.Forms.GroupBox();
			this.TextureSizeLabel = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.WidthBox = new System.Windows.Forms.ComboBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.LoadAnimationBox = new System.Windows.Forms.Button();
			this.FramesGroup = new System.Windows.Forms.GroupBox();
			this.LastFrameBox = new System.Windows.Forms.NumericUpDown();
			this.FirstFrameBox = new System.Windows.Forms.NumericUpDown();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.AnimSizeLabel = new System.Windows.Forms.Label();
			this.FrameCountLabel = new System.Windows.Forms.Label();
			this.NamesGroup = new System.Windows.Forms.GroupBox();
			this.AnimationNameBox = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.TileSetNameBox = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.TextureNameBox = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize) (this.PreviewBox)).BeginInit();
			this.TextureGroup.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.FramesGroup.SuspendLayout();
			((System.ComponentModel.ISupportInitialize) (this.LastFrameBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize) (this.FirstFrameBox)).BeginInit();
			this.NamesGroup.SuspendLayout();
			this.SuspendLayout();
			// 
			// CancelBox
			// 
			this.CancelBox.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.CancelBox.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CancelBox.Location = new System.Drawing.Point(643, 387);
			this.CancelBox.Name = "CancelBox";
			this.CancelBox.Size = new System.Drawing.Size(75, 23);
			this.CancelBox.TabIndex = 0;
			this.CancelBox.Text = "Cancel";
			this.CancelBox.UseVisualStyleBackColor = true;
			// 
			// ImportBox
			// 
			this.ImportBox.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.ImportBox.Location = new System.Drawing.Point(562, 387);
			this.ImportBox.Name = "ImportBox";
			this.ImportBox.Size = new System.Drawing.Size(75, 23);
			this.ImportBox.TabIndex = 1;
			this.ImportBox.Text = "Import";
			this.ImportBox.UseVisualStyleBackColor = true;
			this.ImportBox.Click += new System.EventHandler(this.OkBox_Click);
			// 
			// PreviewBox
			// 
			this.PreviewBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.PreviewBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PreviewBox.Location = new System.Drawing.Point(3, 16);
			this.PreviewBox.Name = "PreviewBox";
			this.PreviewBox.Size = new System.Drawing.Size(438, 350);
			this.PreviewBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.PreviewBox.TabIndex = 2;
			this.PreviewBox.TabStop = false;
			// 
			// TextureGroup
			// 
			this.TextureGroup.Controls.Add(this.TextureSizeLabel);
			this.TextureGroup.Controls.Add(this.label1);
			this.TextureGroup.Controls.Add(this.WidthBox);
			this.TextureGroup.Enabled = false;
			this.TextureGroup.Location = new System.Drawing.Point(12, 41);
			this.TextureGroup.Name = "TextureGroup";
			this.TextureGroup.Size = new System.Drawing.Size(256, 82);
			this.TextureGroup.TabIndex = 3;
			this.TextureGroup.TabStop = false;
			this.TextureGroup.Text = "Texture :";
			// 
			// TextureSizeLabel
			// 
			this.TextureSizeLabel.AutoSize = true;
			this.TextureSizeLabel.Location = new System.Drawing.Point(7, 53);
			this.TextureSizeLabel.Name = "TextureSizeLabel";
			this.TextureSizeLabel.Size = new System.Drawing.Size(73, 13);
			this.TextureSizeLabel.TabIndex = 3;
			this.TextureSizeLabel.Text = "Texture size : ";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 22);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(77, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Texture width :";
			// 
			// WidthBox
			// 
			this.WidthBox.FormattingEnabled = true;
			this.WidthBox.Items.AddRange(new object[] {
            "8",
            "16",
            "32",
            "64",
            "128",
            "256",
            "512",
            "1024"});
			this.WidthBox.Location = new System.Drawing.Point(112, 19);
			this.WidthBox.Name = "WidthBox";
			this.WidthBox.Size = new System.Drawing.Size(138, 21);
			this.WidthBox.TabIndex = 1;
			this.WidthBox.SelectedValueChanged += new System.EventHandler(this.WidthBox_SelectedValueChanged);
			// 
			// groupBox2
			// 
			this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox2.Controls.Add(this.PreviewBox);
			this.groupBox2.Location = new System.Drawing.Point(274, 12);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(444, 369);
			this.groupBox2.TabIndex = 4;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Preview :";
			// 
			// LoadAnimationBox
			// 
			this.LoadAnimationBox.Location = new System.Drawing.Point(22, 12);
			this.LoadAnimationBox.Name = "LoadAnimationBox";
			this.LoadAnimationBox.Size = new System.Drawing.Size(246, 23);
			this.LoadAnimationBox.TabIndex = 0;
			this.LoadAnimationBox.Text = "Load animation";
			this.LoadAnimationBox.UseVisualStyleBackColor = true;
			this.LoadAnimationBox.Click += new System.EventHandler(this.LoadAnimationBox_Click);
			// 
			// FramesGroup
			// 
			this.FramesGroup.Controls.Add(this.LastFrameBox);
			this.FramesGroup.Controls.Add(this.FirstFrameBox);
			this.FramesGroup.Controls.Add(this.label6);
			this.FramesGroup.Controls.Add(this.label5);
			this.FramesGroup.Controls.Add(this.AnimSizeLabel);
			this.FramesGroup.Controls.Add(this.FrameCountLabel);
			this.FramesGroup.Enabled = false;
			this.FramesGroup.Location = new System.Drawing.Point(12, 250);
			this.FramesGroup.Name = "FramesGroup";
			this.FramesGroup.Size = new System.Drawing.Size(256, 131);
			this.FramesGroup.TabIndex = 5;
			this.FramesGroup.TabStop = false;
			this.FramesGroup.Text = "Frames :";
			// 
			// LastFrameBox
			// 
			this.LastFrameBox.Location = new System.Drawing.Point(112, 97);
			this.LastFrameBox.Name = "LastFrameBox";
			this.LastFrameBox.Size = new System.Drawing.Size(120, 20);
			this.LastFrameBox.TabIndex = 3;
			this.LastFrameBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.LastFrameBox.ThousandsSeparator = true;
			// 
			// FirstFrameBox
			// 
			this.FirstFrameBox.Location = new System.Drawing.Point(112, 64);
			this.FirstFrameBox.Name = "FirstFrameBox";
			this.FirstFrameBox.Size = new System.Drawing.Size(120, 20);
			this.FirstFrameBox.TabIndex = 3;
			this.FirstFrameBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.FirstFrameBox.ThousandsSeparator = true;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(7, 99);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(62, 13);
			this.label6.TabIndex = 1;
			this.label6.Text = "Last frame :";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(7, 66);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(61, 13);
			this.label5.TabIndex = 1;
			this.label5.Text = "First frame :";
			// 
			// AnimSizeLabel
			// 
			this.AnimSizeLabel.AutoSize = true;
			this.AnimSizeLabel.Location = new System.Drawing.Point(7, 16);
			this.AnimSizeLabel.Name = "AnimSizeLabel";
			this.AnimSizeLabel.Size = new System.Drawing.Size(83, 13);
			this.AnimSizeLabel.TabIndex = 0;
			this.AnimSizeLabel.Text = "Animation size : ";
			// 
			// FrameCountLabel
			// 
			this.FrameCountLabel.AutoSize = true;
			this.FrameCountLabel.Location = new System.Drawing.Point(7, 38);
			this.FrameCountLabel.Name = "FrameCountLabel";
			this.FrameCountLabel.Size = new System.Drawing.Size(75, 13);
			this.FrameCountLabel.TabIndex = 0;
			this.FrameCountLabel.Text = "Frame count : ";
			// 
			// NamesGroup
			// 
			this.NamesGroup.Controls.Add(this.AnimationNameBox);
			this.NamesGroup.Controls.Add(this.label4);
			this.NamesGroup.Controls.Add(this.TileSetNameBox);
			this.NamesGroup.Controls.Add(this.label3);
			this.NamesGroup.Controls.Add(this.TextureNameBox);
			this.NamesGroup.Controls.Add(this.label2);
			this.NamesGroup.Enabled = false;
			this.NamesGroup.Location = new System.Drawing.Point(12, 129);
			this.NamesGroup.Name = "NamesGroup";
			this.NamesGroup.Size = new System.Drawing.Size(256, 115);
			this.NamesGroup.TabIndex = 6;
			this.NamesGroup.TabStop = false;
			this.NamesGroup.Text = "Asset\'s names :";
			// 
			// AnimationNameBox
			// 
			this.AnimationNameBox.Location = new System.Drawing.Point(112, 80);
			this.AnimationNameBox.Name = "AnimationNameBox";
			this.AnimationNameBox.Size = new System.Drawing.Size(138, 20);
			this.AnimationNameBox.TabIndex = 1;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(7, 83);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(95, 13);
			this.label4.TabIndex = 0;
			this.label4.Text = "Animation\'s name :";
			// 
			// TileSetNameBox
			// 
			this.TileSetNameBox.Location = new System.Drawing.Point(112, 54);
			this.TileSetNameBox.Name = "TileSetNameBox";
			this.TileSetNameBox.Size = new System.Drawing.Size(138, 20);
			this.TileSetNameBox.TabIndex = 1;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(7, 57);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(82, 13);
			this.label3.TabIndex = 0;
			this.label3.Text = "TileSet\'s name :";
			// 
			// TextureNameBox
			// 
			this.TextureNameBox.Location = new System.Drawing.Point(112, 28);
			this.TextureNameBox.Name = "TextureNameBox";
			this.TextureNameBox.Size = new System.Drawing.Size(138, 20);
			this.TextureNameBox.TabIndex = 1;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(7, 31);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(85, 13);
			this.label2.TabIndex = 0;
			this.label2.Text = "Texture\'s name :";
			// 
			// ImportGIFForm
			// 
			this.AcceptButton = this.ImportBox;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.CancelBox;
			this.ClientSize = new System.Drawing.Size(730, 422);
			this.Controls.Add(this.NamesGroup);
			this.Controls.Add(this.FramesGroup);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.TextureGroup);
			this.Controls.Add(this.ImportBox);
			this.Controls.Add(this.LoadAnimationBox);
			this.Controls.Add(this.CancelBox);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(500, 460);
			this.Name = "ImportGIFForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Import GIF";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ImportGIFForm_FormClosing);
			((System.ComponentModel.ISupportInitialize) (this.PreviewBox)).EndInit();
			this.TextureGroup.ResumeLayout(false);
			this.TextureGroup.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.FramesGroup.ResumeLayout(false);
			this.FramesGroup.PerformLayout();
			((System.ComponentModel.ISupportInitialize) (this.LastFrameBox)).EndInit();
			((System.ComponentModel.ISupportInitialize) (this.FirstFrameBox)).EndInit();
			this.NamesGroup.ResumeLayout(false);
			this.NamesGroup.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button CancelBox;
		private System.Windows.Forms.Button ImportBox;
		private System.Windows.Forms.PictureBox PreviewBox;
		private System.Windows.Forms.GroupBox TextureGroup;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox WidthBox;
		private System.Windows.Forms.Button LoadAnimationBox;
		private System.Windows.Forms.Label TextureSizeLabel;
		private System.Windows.Forms.GroupBox FramesGroup;
		private System.Windows.Forms.GroupBox NamesGroup;
		private System.Windows.Forms.TextBox AnimationNameBox;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox TileSetNameBox;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox TextureNameBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label FrameCountLabel;
		private System.Windows.Forms.NumericUpDown LastFrameBox;
		private System.Windows.Forms.NumericUpDown FirstFrameBox;
		private System.Windows.Forms.Label AnimSizeLabel;
	}
}