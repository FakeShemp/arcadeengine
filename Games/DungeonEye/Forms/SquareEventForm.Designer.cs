namespace DungeonEye.Forms
{
	partial class SquareEventForm
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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.MustFaceBox = new System.Windows.Forms.CheckBox();
			this.DirectionBox = new System.Windows.Forms.ComboBox();
			this.PreviewBox = new System.Windows.Forms.PictureBox();
			this.BrowsePictureBox = new System.Windows.Forms.Button();
			this.DisplayBackgroundBox = new System.Windows.Forms.CheckBox();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.LoopSoundBox = new System.Windows.Forms.CheckBox();
			this.BrowseSoundBox = new System.Windows.Forms.Button();
			this.SoundNameBox = new System.Windows.Forms.TextBox();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.PictureTab = new System.Windows.Forms.TabPage();
			this.ChoicesTab = new System.Windows.Forms.TabPage();
			this.CloseBox = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize) (this.PreviewBox)).BeginInit();
			this.groupBox3.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.PictureTab.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.MustFaceBox);
			this.groupBox1.Controls.Add(this.DirectionBox);
			this.groupBox1.Location = new System.Drawing.Point(12, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(127, 79);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Direction :";
			// 
			// MustFaceBox
			// 
			this.MustFaceBox.AutoSize = true;
			this.MustFaceBox.Location = new System.Drawing.Point(6, 23);
			this.MustFaceBox.Name = "MustFaceBox";
			this.MustFaceBox.Size = new System.Drawing.Size(120, 17);
			this.MustFaceBox.TabIndex = 1;
			this.MustFaceBox.Text = "Team must face to :";
			this.MustFaceBox.UseVisualStyleBackColor = true;
			// 
			// DirectionBox
			// 
			this.DirectionBox.FormattingEnabled = true;
			this.DirectionBox.Location = new System.Drawing.Point(6, 48);
			this.DirectionBox.Name = "DirectionBox";
			this.DirectionBox.Size = new System.Drawing.Size(115, 21);
			this.DirectionBox.TabIndex = 0;
			// 
			// PreviewBox
			// 
			this.PreviewBox.Location = new System.Drawing.Point(6, 6);
			this.PreviewBox.Name = "PreviewBox";
			this.PreviewBox.Size = new System.Drawing.Size(352, 240);
			this.PreviewBox.TabIndex = 0;
			this.PreviewBox.TabStop = false;
			// 
			// BrowsePictureBox
			// 
			this.BrowsePictureBox.AutoSize = true;
			this.BrowsePictureBox.Location = new System.Drawing.Point(364, 6);
			this.BrowsePictureBox.Name = "BrowsePictureBox";
			this.BrowsePictureBox.Size = new System.Drawing.Size(98, 23);
			this.BrowsePictureBox.TabIndex = 1;
			this.BrowsePictureBox.Text = "Browse...";
			this.BrowsePictureBox.UseVisualStyleBackColor = true;
			this.BrowsePictureBox.Click += new System.EventHandler(this.BrowsePictureBox_Click);
			// 
			// DisplayBackgroundBox
			// 
			this.DisplayBackgroundBox.AutoSize = true;
			this.DisplayBackgroundBox.Location = new System.Drawing.Point(364, 35);
			this.DisplayBackgroundBox.Name = "DisplayBackgroundBox";
			this.DisplayBackgroundBox.Size = new System.Drawing.Size(120, 17);
			this.DisplayBackgroundBox.TabIndex = 2;
			this.DisplayBackgroundBox.Text = "Display background";
			this.DisplayBackgroundBox.UseVisualStyleBackColor = true;
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.LoopSoundBox);
			this.groupBox3.Controls.Add(this.BrowseSoundBox);
			this.groupBox3.Controls.Add(this.SoundNameBox);
			this.groupBox3.Location = new System.Drawing.Point(295, 12);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(217, 79);
			this.groupBox3.TabIndex = 2;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Sound :";
			// 
			// LoopSoundBox
			// 
			this.LoopSoundBox.AutoSize = true;
			this.LoopSoundBox.Location = new System.Drawing.Point(110, 23);
			this.LoopSoundBox.Name = "LoopSoundBox";
			this.LoopSoundBox.Size = new System.Drawing.Size(50, 17);
			this.LoopSoundBox.TabIndex = 2;
			this.LoopSoundBox.Text = "Loop";
			this.LoopSoundBox.UseVisualStyleBackColor = true;
			// 
			// BrowseSoundBox
			// 
			this.BrowseSoundBox.Location = new System.Drawing.Point(6, 19);
			this.BrowseSoundBox.Name = "BrowseSoundBox";
			this.BrowseSoundBox.Size = new System.Drawing.Size(75, 23);
			this.BrowseSoundBox.TabIndex = 1;
			this.BrowseSoundBox.Text = "Browse...";
			this.BrowseSoundBox.UseVisualStyleBackColor = true;
			this.BrowseSoundBox.Click += new System.EventHandler(this.BrowseSoundBox_Click);
			// 
			// SoundNameBox
			// 
			this.SoundNameBox.Location = new System.Drawing.Point(6, 49);
			this.SoundNameBox.Name = "SoundNameBox";
			this.SoundNameBox.Size = new System.Drawing.Size(205, 20);
			this.SoundNameBox.TabIndex = 0;
			// 
			// tabControl1
			// 
			this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl1.Controls.Add(this.PictureTab);
			this.tabControl1.Controls.Add(this.ChoicesTab);
			this.tabControl1.Location = new System.Drawing.Point(12, 97);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(504, 330);
			this.tabControl1.TabIndex = 3;
			// 
			// PictureTab
			// 
			this.PictureTab.Controls.Add(this.DisplayBackgroundBox);
			this.PictureTab.Controls.Add(this.BrowsePictureBox);
			this.PictureTab.Controls.Add(this.PreviewBox);
			this.PictureTab.Location = new System.Drawing.Point(4, 22);
			this.PictureTab.Name = "PictureTab";
			this.PictureTab.Padding = new System.Windows.Forms.Padding(3);
			this.PictureTab.Size = new System.Drawing.Size(496, 304);
			this.PictureTab.TabIndex = 0;
			this.PictureTab.Text = "Picture";
			this.PictureTab.UseVisualStyleBackColor = true;
			// 
			// ChoicesTab
			// 
			this.ChoicesTab.Location = new System.Drawing.Point(4, 22);
			this.ChoicesTab.Name = "ChoicesTab";
			this.ChoicesTab.Padding = new System.Windows.Forms.Padding(3);
			this.ChoicesTab.Size = new System.Drawing.Size(496, 304);
			this.ChoicesTab.TabIndex = 1;
			this.ChoicesTab.Text = "Choices";
			this.ChoicesTab.UseVisualStyleBackColor = true;
			// 
			// CloseBox
			// 
			this.CloseBox.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.CloseBox.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.CloseBox.Location = new System.Drawing.Point(437, 433);
			this.CloseBox.Name = "CloseBox";
			this.CloseBox.Size = new System.Drawing.Size(75, 23);
			this.CloseBox.TabIndex = 4;
			this.CloseBox.Text = "Close";
			this.CloseBox.UseVisualStyleBackColor = true;
			// 
			// SquareEventForm
			// 
			this.AcceptButton = this.CloseBox;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(528, 468);
			this.Controls.Add(this.CloseBox);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SquareEventForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Square event wizard";
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize) (this.PreviewBox)).EndInit();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.tabControl1.ResumeLayout(false);
			this.PictureTab.ResumeLayout(false);
			this.PictureTab.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckBox MustFaceBox;
		private System.Windows.Forms.ComboBox DirectionBox;
		private System.Windows.Forms.CheckBox DisplayBackgroundBox;
		private System.Windows.Forms.Button BrowsePictureBox;
		private System.Windows.Forms.PictureBox PreviewBox;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Button BrowseSoundBox;
		private System.Windows.Forms.TextBox SoundNameBox;
		private System.Windows.Forms.CheckBox LoopSoundBox;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage PictureTab;
		private System.Windows.Forms.TabPage ChoicesTab;
		private System.Windows.Forms.Button CloseBox;
	}
}