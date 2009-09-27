namespace ArcEngine.Games.RuffnTumble.Editor.Wizards
{
	partial class FindMemoryLevelWizard
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FindMemoryLevelWizard));
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.MemoryButton = new System.Windows.Forms.Button();
			this.TextureButton = new System.Windows.Forms.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.BlockHeightBox = new System.Windows.Forms.NumericUpDown();
			this.BlockWidthBox = new System.Windows.Forms.NumericUpDown();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.label4 = new System.Windows.Forms.Label();
			this.MapHeightBox = new System.Windows.Forms.NumericUpDown();
			this.label3 = new System.Windows.Forms.Label();
			this.MapWidthBox = new System.Windows.Forms.NumericUpDown();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.MemoryLocationBox = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
			this.LevelGlControl = new OpenTK.GLControl();
			this.OpenTileDlg = new System.Windows.Forms.OpenFileDialog();
			this.OpenDatatDlg = new System.Windows.Forms.OpenFileDialog();
			this.TrackPosition = new System.Windows.Forms.TrackBar();
			this.label7 = new System.Windows.Forms.Label();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.BlockHeightBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.BlockWidthBox)).BeginInit();
			this.groupBox3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.MapHeightBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.MapWidthBox)).BeginInit();
			this.groupBox4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.TrackPosition)).BeginInit();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.MemoryButton);
			this.groupBox1.Controls.Add(this.TextureButton);
			this.groupBox1.Location = new System.Drawing.Point(12, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(126, 94);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Options :";
			// 
			// MemoryButton
			// 
			this.MemoryButton.Image = ((System.Drawing.Image)(resources.GetObject("MemoryButton.Image")));
			this.MemoryButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.MemoryButton.Location = new System.Drawing.Point(7, 50);
			this.MemoryButton.Name = "MemoryButton";
			this.MemoryButton.Size = new System.Drawing.Size(103, 23);
			this.MemoryButton.TabIndex = 1;
			this.MemoryButton.Text = "Load memory...";
			this.MemoryButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.MemoryButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.MemoryButton.UseVisualStyleBackColor = true;
			this.MemoryButton.Click += new System.EventHandler(this.MemoryButton_Click);
			// 
			// TextureButton
			// 
			this.TextureButton.Image = ((System.Drawing.Image)(resources.GetObject("TextureButton.Image")));
			this.TextureButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.TextureButton.Location = new System.Drawing.Point(7, 19);
			this.TextureButton.Name = "TextureButton";
			this.TextureButton.Size = new System.Drawing.Size(103, 23);
			this.TextureButton.TabIndex = 0;
			this.TextureButton.Text = "Load texture...";
			this.TextureButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.TextureButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.TextureButton.UseVisualStyleBackColor = true;
			this.TextureButton.Click += new System.EventHandler(this.TextureButton_Click);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Controls.Add(this.label1);
			this.groupBox2.Controls.Add(this.BlockHeightBox);
			this.groupBox2.Controls.Add(this.BlockWidthBox);
			this.groupBox2.Location = new System.Drawing.Point(144, 12);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(122, 94);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Block properties :";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(7, 47);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(38, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "Height";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(10, 21);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(35, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Width";
			// 
			// BlockHeightBox
			// 
			this.BlockHeightBox.Location = new System.Drawing.Point(51, 45);
			this.BlockHeightBox.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
			this.BlockHeightBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.BlockHeightBox.Name = "BlockHeightBox";
			this.BlockHeightBox.Size = new System.Drawing.Size(48, 20);
			this.BlockHeightBox.TabIndex = 1;
			this.BlockHeightBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.BlockHeightBox.ThousandsSeparator = true;
			this.BlockHeightBox.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
			// 
			// BlockWidthBox
			// 
			this.BlockWidthBox.Location = new System.Drawing.Point(51, 19);
			this.BlockWidthBox.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
			this.BlockWidthBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.BlockWidthBox.Name = "BlockWidthBox";
			this.BlockWidthBox.Size = new System.Drawing.Size(48, 20);
			this.BlockWidthBox.TabIndex = 0;
			this.BlockWidthBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.BlockWidthBox.ThousandsSeparator = true;
			this.BlockWidthBox.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.label4);
			this.groupBox3.Controls.Add(this.MapHeightBox);
			this.groupBox3.Controls.Add(this.label3);
			this.groupBox3.Controls.Add(this.MapWidthBox);
			this.groupBox3.Location = new System.Drawing.Point(272, 12);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(111, 94);
			this.groupBox3.TabIndex = 2;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Map properties :";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(2, 50);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(38, 13);
			this.label4.TabIndex = 3;
			this.label4.Text = "Height";
			// 
			// MapHeightBox
			// 
			this.MapHeightBox.Location = new System.Drawing.Point(46, 48);
			this.MapHeightBox.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
			this.MapHeightBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.MapHeightBox.Name = "MapHeightBox";
			this.MapHeightBox.Size = new System.Drawing.Size(48, 20);
			this.MapHeightBox.TabIndex = 1;
			this.MapHeightBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.MapHeightBox.ThousandsSeparator = true;
			this.MapHeightBox.Value = new decimal(new int[] {
            84,
            0,
            0,
            0});
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(5, 24);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(35, 13);
			this.label3.TabIndex = 2;
			this.label3.Text = "Width";
			// 
			// MapWidthBox
			// 
			this.MapWidthBox.Location = new System.Drawing.Point(46, 22);
			this.MapWidthBox.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
			this.MapWidthBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.MapWidthBox.Name = "MapWidthBox";
			this.MapWidthBox.Size = new System.Drawing.Size(48, 20);
			this.MapWidthBox.TabIndex = 0;
			this.MapWidthBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.MapWidthBox.ThousandsSeparator = true;
			this.MapWidthBox.Value = new decimal(new int[] {
            300,
            0,
            0,
            0});
			// 
			// groupBox4
			// 
			this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox4.Controls.Add(this.comboBox1);
			this.groupBox4.Controls.Add(this.label7);
			this.groupBox4.Controls.Add(this.TrackPosition);
			this.groupBox4.Controls.Add(this.MemoryLocationBox);
			this.groupBox4.Controls.Add(this.label5);
			this.groupBox4.Controls.Add(this.label6);
			this.groupBox4.Controls.Add(this.numericUpDown1);
			this.groupBox4.Location = new System.Drawing.Point(389, 12);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(614, 94);
			this.groupBox4.TabIndex = 3;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Advanced :";
			// 
			// MemoryLocationBox
			// 
			this.MemoryLocationBox.Location = new System.Drawing.Point(103, 25);
			this.MemoryLocationBox.Name = "MemoryLocationBox";
			this.MemoryLocationBox.Size = new System.Drawing.Size(91, 20);
			this.MemoryLocationBox.TabIndex = 1;
			this.MemoryLocationBox.TextChanged += new System.EventHandler(this.MemoryLocationBox_TextChanged);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(7, 28);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(90, 13);
			this.label5.TabIndex = 0;
			this.label5.Text = "Memory location :";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(200, 28);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(51, 13);
			this.label6.TabIndex = 2;
			this.label6.Text = "Skip byte";
			// 
			// numericUpDown1
			// 
			this.numericUpDown1.Location = new System.Drawing.Point(257, 25);
			this.numericUpDown1.Maximum = new decimal(new int[] {
            512,
            0,
            0,
            0});
			this.numericUpDown1.Name = "numericUpDown1";
			this.numericUpDown1.Size = new System.Drawing.Size(74, 20);
			this.numericUpDown1.TabIndex = 0;
			this.numericUpDown1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.numericUpDown1.ThousandsSeparator = true;
			// 
			// LevelGlControl
			// 
			this.LevelGlControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.LevelGlControl.BackColor = System.Drawing.Color.Black;
			this.LevelGlControl.Location = new System.Drawing.Point(12, 110);
			this.LevelGlControl.Name = "LevelGlControl";
			this.LevelGlControl.Size = new System.Drawing.Size(991, 418);
			this.LevelGlControl.TabIndex = 0;
			this.LevelGlControl.Paint += new System.Windows.Forms.PaintEventHandler(this.LevelGlControl_Paint);
			this.LevelGlControl.Resize += new System.EventHandler(this.LevelGlControl_Resize);
			// 
			// OpenTileDlg
			// 
			this.OpenTileDlg.DefaultExt = "png";
			this.OpenTileDlg.Filter = "PNG file|*.png|All files|*.*";
			this.OpenTileDlg.RestoreDirectory = true;
			// 
			// OpenDatatDlg
			// 
			this.OpenDatatDlg.Filter = "All file (*.*)|*.*";
			// 
			// TrackPosition
			// 
			this.TrackPosition.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.TrackPosition.Location = new System.Drawing.Point(10, 47);
			this.TrackPosition.Maximum = 0;
			this.TrackPosition.Name = "TrackPosition";
			this.TrackPosition.Size = new System.Drawing.Size(598, 45);
			this.TrackPosition.TabIndex = 3;
			this.TrackPosition.ValueChanged += new System.EventHandler(this.TrackPosition_Scroll);
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(338, 28);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(27, 13);
			this.label7.TabIndex = 4;
			this.label7.Text = "Size";
			// 
			// comboBox1
			// 
			this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.Items.AddRange(new object[] {
            "Byte",
            "Word",
            "Long"});
			this.comboBox1.Location = new System.Drawing.Point(379, 25);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(84, 21);
			this.comboBox1.TabIndex = 5;
			// 
			// FindMemoryLevelWizard
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1015, 540);
			this.Controls.Add(this.LevelGlControl);
			this.Controls.Add(this.groupBox4);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Name = "FindMemoryLevelWizard";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Find level wizard - UNDER HEAVY CONSTRUCTION !!!!";
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.BlockHeightBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.BlockWidthBox)).EndInit();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.MapHeightBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.MapWidthBox)).EndInit();
			this.groupBox4.ResumeLayout(false);
			this.groupBox4.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.TrackPosition)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button TextureButton;
		private System.Windows.Forms.Button MemoryButton;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.NumericUpDown BlockHeightBox;
		private System.Windows.Forms.NumericUpDown BlockWidthBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.NumericUpDown MapHeightBox;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.NumericUpDown MapWidthBox;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.TextBox MemoryLocationBox;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.NumericUpDown numericUpDown1;
		private OpenTK.GLControl LevelGlControl;
		private System.Windows.Forms.OpenFileDialog OpenTileDlg;
		private System.Windows.Forms.OpenFileDialog OpenDatatDlg;
		private System.Windows.Forms.TrackBar TrackPosition;
		private System.Windows.Forms.ComboBox comboBox1;
		private System.Windows.Forms.Label label7;
	}
}