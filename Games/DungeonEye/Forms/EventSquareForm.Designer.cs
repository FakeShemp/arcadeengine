namespace DungeonEye.Forms
{
	partial class EventSquareForm
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
			this.PropertiesTab = new System.Windows.Forms.TabPage();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.label1 = new System.Windows.Forms.Label();
			this.IntelligenceBox = new System.Windows.Forms.NumericUpDown();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.MessageBox = new System.Windows.Forms.TextBox();
			this.PictureTab = new System.Windows.Forms.TabPage();
			this.PictureNameBox = new System.Windows.Forms.TextBox();
			this.ChoicesTab = new System.Windows.Forms.TabPage();
			this.CloseBox = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.PreviewBox)).BeginInit();
			this.groupBox3.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.PropertiesTab.SuspendLayout();
			this.groupBox4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.IntelligenceBox)).BeginInit();
			this.groupBox2.SuspendLayout();
			this.PictureTab.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.MustFaceBox);
			this.groupBox1.Controls.Add(this.DirectionBox);
			this.groupBox1.Location = new System.Drawing.Point(3, 3);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(132, 79);
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
			this.MustFaceBox.CheckedChanged += new System.EventHandler(this.MustFaceBox_CheckedChanged);
			// 
			// DirectionBox
			// 
			this.DirectionBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.DirectionBox.FormattingEnabled = true;
			this.DirectionBox.Location = new System.Drawing.Point(6, 48);
			this.DirectionBox.Name = "DirectionBox";
			this.DirectionBox.Size = new System.Drawing.Size(115, 21);
			this.DirectionBox.TabIndex = 0;
			this.DirectionBox.SelectedIndexChanged += new System.EventHandler(this.DirectionBox_SelectedIndexChanged);
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
			this.DisplayBackgroundBox.Location = new System.Drawing.Point(364, 61);
			this.DisplayBackgroundBox.Name = "DisplayBackgroundBox";
			this.DisplayBackgroundBox.Size = new System.Drawing.Size(120, 17);
			this.DisplayBackgroundBox.TabIndex = 2;
			this.DisplayBackgroundBox.Text = "Display background";
			this.DisplayBackgroundBox.UseVisualStyleBackColor = true;
			this.DisplayBackgroundBox.CheckedChanged += new System.EventHandler(this.DisplayBackgroundBox_CheckedChanged);
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.LoopSoundBox);
			this.groupBox3.Controls.Add(this.BrowseSoundBox);
			this.groupBox3.Controls.Add(this.SoundNameBox);
			this.groupBox3.Location = new System.Drawing.Point(141, 3);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(149, 79);
			this.groupBox3.TabIndex = 2;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Sound :";
			// 
			// LoopSoundBox
			// 
			this.LoopSoundBox.AutoSize = true;
			this.LoopSoundBox.Location = new System.Drawing.Point(87, 23);
			this.LoopSoundBox.Name = "LoopSoundBox";
			this.LoopSoundBox.Size = new System.Drawing.Size(50, 17);
			this.LoopSoundBox.TabIndex = 2;
			this.LoopSoundBox.Text = "Loop";
			this.LoopSoundBox.UseVisualStyleBackColor = true;
			this.LoopSoundBox.CheckedChanged += new System.EventHandler(this.LoopSoundBox_CheckedChanged);
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
			this.SoundNameBox.Size = new System.Drawing.Size(131, 20);
			this.SoundNameBox.TabIndex = 0;
			this.SoundNameBox.TextChanged += new System.EventHandler(this.SoundNameBox_TextChanged);
			// 
			// tabControl1
			// 
			this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl1.Controls.Add(this.PropertiesTab);
			this.tabControl1.Controls.Add(this.PictureTab);
			this.tabControl1.Controls.Add(this.ChoicesTab);
			this.tabControl1.Location = new System.Drawing.Point(12, 12);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(570, 369);
			this.tabControl1.TabIndex = 3;
			// 
			// PropertiesTab
			// 
			this.PropertiesTab.Controls.Add(this.groupBox4);
			this.PropertiesTab.Controls.Add(this.groupBox2);
			this.PropertiesTab.Controls.Add(this.groupBox1);
			this.PropertiesTab.Controls.Add(this.groupBox3);
			this.PropertiesTab.Location = new System.Drawing.Point(4, 22);
			this.PropertiesTab.Name = "PropertiesTab";
			this.PropertiesTab.Size = new System.Drawing.Size(562, 343);
			this.PropertiesTab.TabIndex = 2;
			this.PropertiesTab.Text = "Properties";
			this.PropertiesTab.UseVisualStyleBackColor = true;
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.label1);
			this.groupBox4.Controls.Add(this.IntelligenceBox);
			this.groupBox4.Location = new System.Drawing.Point(3, 173);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(132, 52);
			this.groupBox4.TabIndex = 6;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Intelligence :";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 21);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(54, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Minimum :";
			// 
			// IntelligenceBox
			// 
			this.IntelligenceBox.Location = new System.Drawing.Point(66, 19);
			this.IntelligenceBox.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
			this.IntelligenceBox.Name = "IntelligenceBox";
			this.IntelligenceBox.Size = new System.Drawing.Size(55, 20);
			this.IntelligenceBox.TabIndex = 0;
			this.IntelligenceBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.IntelligenceBox.ThousandsSeparator = true;
			this.IntelligenceBox.ValueChanged += new System.EventHandler(this.IntelligenceBox_ValueChanged);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.MessageBox);
			this.groupBox2.Location = new System.Drawing.Point(3, 88);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(287, 79);
			this.groupBox2.TabIndex = 5;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Display message :";
			// 
			// MessageBox
			// 
			this.MessageBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.MessageBox.Location = new System.Drawing.Point(6, 19);
			this.MessageBox.Multiline = true;
			this.MessageBox.Name = "MessageBox";
			this.MessageBox.Size = new System.Drawing.Size(275, 54);
			this.MessageBox.TabIndex = 0;
			this.MessageBox.TextChanged += new System.EventHandler(this.MessageBox_TextChanged);
			// 
			// PictureTab
			// 
			this.PictureTab.Controls.Add(this.PictureNameBox);
			this.PictureTab.Controls.Add(this.DisplayBackgroundBox);
			this.PictureTab.Controls.Add(this.BrowsePictureBox);
			this.PictureTab.Controls.Add(this.PreviewBox);
			this.PictureTab.Location = new System.Drawing.Point(4, 22);
			this.PictureTab.Name = "PictureTab";
			this.PictureTab.Padding = new System.Windows.Forms.Padding(3);
			this.PictureTab.Size = new System.Drawing.Size(562, 343);
			this.PictureTab.TabIndex = 0;
			this.PictureTab.Text = "Picture";
			this.PictureTab.UseVisualStyleBackColor = true;
			// 
			// PictureNameBox
			// 
			this.PictureNameBox.Location = new System.Drawing.Point(364, 35);
			this.PictureNameBox.Name = "PictureNameBox";
			this.PictureNameBox.Size = new System.Drawing.Size(192, 20);
			this.PictureNameBox.TabIndex = 3;
			this.PictureNameBox.TextChanged += new System.EventHandler(this.PictureNameBox_TextChanged);
			// 
			// ChoicesTab
			// 
			this.ChoicesTab.Location = new System.Drawing.Point(4, 22);
			this.ChoicesTab.Name = "ChoicesTab";
			this.ChoicesTab.Padding = new System.Windows.Forms.Padding(3);
			this.ChoicesTab.Size = new System.Drawing.Size(562, 343);
			this.ChoicesTab.TabIndex = 1;
			this.ChoicesTab.Text = "Choices";
			this.ChoicesTab.UseVisualStyleBackColor = true;
			// 
			// CloseBox
			// 
			this.CloseBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.CloseBox.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.CloseBox.Location = new System.Drawing.Point(507, 387);
			this.CloseBox.Name = "CloseBox";
			this.CloseBox.Size = new System.Drawing.Size(75, 23);
			this.CloseBox.TabIndex = 4;
			this.CloseBox.Text = "Close";
			this.CloseBox.UseVisualStyleBackColor = true;
			// 
			// EventSquareForm
			// 
			this.AcceptButton = this.CloseBox;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(594, 422);
			this.Controls.Add(this.CloseBox);
			this.Controls.Add(this.tabControl1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(550, 450);
			this.Name = "EventSquareForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Square event wizard";
			this.Load += new System.EventHandler(this.EventSquareForm_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.EventSquareForm_KeyDown);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.PreviewBox)).EndInit();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.tabControl1.ResumeLayout(false);
			this.PropertiesTab.ResumeLayout(false);
			this.groupBox4.ResumeLayout(false);
			this.groupBox4.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.IntelligenceBox)).EndInit();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
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
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.TextBox MessageBox;
		private System.Windows.Forms.TextBox PictureNameBox;
		private System.Windows.Forms.TabPage PropertiesTab;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.NumericUpDown IntelligenceBox;
	}
}