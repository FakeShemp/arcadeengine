namespace ArcEngine.Games.RuffnTumble.Editor.Wizards
{
	partial class LevelResizeWizard
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
			this.ButtonCancel = new System.Windows.Forms.Button();
			this.ResizeButton = new System.Windows.Forms.Button();
			this.DesiredWidth = new System.Windows.Forms.NumericUpDown();
			this.DesiredHeight = new System.Windows.Forms.NumericUpDown();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.LevelHeightLabel = new System.Windows.Forms.Label();
			this.LevelWidthLabel = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			((System.ComponentModel.ISupportInitialize)(this.DesiredWidth)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.DesiredHeight)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// ButtonCancel
			// 
			this.ButtonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.ButtonCancel.Location = new System.Drawing.Point(241, 114);
			this.ButtonCancel.Name = "ButtonCancel";
			this.ButtonCancel.Size = new System.Drawing.Size(75, 23);
			this.ButtonCancel.TabIndex = 0;
			this.ButtonCancel.Text = "Cancel";
			this.ButtonCancel.UseVisualStyleBackColor = true;
			// 
			// ResizeButton
			// 
			this.ResizeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.ResizeButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.ResizeButton.Location = new System.Drawing.Point(160, 114);
			this.ResizeButton.Name = "ResizeButton";
			this.ResizeButton.Size = new System.Drawing.Size(75, 23);
			this.ResizeButton.TabIndex = 1;
			this.ResizeButton.Text = "Resize";
			this.ResizeButton.UseVisualStyleBackColor = true;
			this.ResizeButton.Click += new System.EventHandler(this.ResizeButton_Click);
			// 
			// DesiredWidth
			// 
			this.DesiredWidth.Location = new System.Drawing.Point(59, 25);
			this.DesiredWidth.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.DesiredWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.DesiredWidth.Name = "DesiredWidth";
			this.DesiredWidth.Size = new System.Drawing.Size(66, 20);
			this.DesiredWidth.TabIndex = 3;
			this.DesiredWidth.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// DesiredHeight
			// 
			this.DesiredHeight.Location = new System.Drawing.Point(59, 51);
			this.DesiredHeight.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.DesiredHeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.DesiredHeight.Name = "DesiredHeight";
			this.DesiredHeight.Size = new System.Drawing.Size(66, 20);
			this.DesiredHeight.TabIndex = 4;
			this.DesiredHeight.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(8, 27);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(41, 13);
			this.label2.TabIndex = 6;
			this.label2.Text = "Width :";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(8, 53);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(44, 13);
			this.label3.TabIndex = 7;
			this.label3.Text = "Height :";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.LevelHeightLabel);
			this.groupBox1.Controls.Add(this.LevelWidthLabel);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Location = new System.Drawing.Point(12, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(161, 89);
			this.groupBox1.TabIndex = 8;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Current level size :";
			// 
			// LevelHeightLabel
			// 
			this.LevelHeightLabel.AutoSize = true;
			this.LevelHeightLabel.Location = new System.Drawing.Point(56, 53);
			this.LevelHeightLabel.Name = "LevelHeightLabel";
			this.LevelHeightLabel.Size = new System.Drawing.Size(16, 13);
			this.LevelHeightLabel.TabIndex = 9;
			this.LevelHeightLabel.Text = "   ";
			// 
			// LevelWidthLabel
			// 
			this.LevelWidthLabel.AutoSize = true;
			this.LevelWidthLabel.Location = new System.Drawing.Point(56, 27);
			this.LevelWidthLabel.Name = "LevelWidthLabel";
			this.LevelWidthLabel.Size = new System.Drawing.Size(16, 13);
			this.LevelWidthLabel.TabIndex = 8;
			this.LevelWidthLabel.Text = "   ";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(6, 53);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(44, 13);
			this.label5.TabIndex = 7;
			this.label5.Text = "Height :";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(9, 27);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(41, 13);
			this.label4.TabIndex = 6;
			this.label4.Text = "Width :";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.DesiredWidth);
			this.groupBox2.Controls.Add(this.DesiredHeight);
			this.groupBox2.Controls.Add(this.label3);
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Location = new System.Drawing.Point(179, 12);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(136, 89);
			this.groupBox2.TabIndex = 9;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Desired size :";
			// 
			// LevelResizeWizard
			// 
			this.AcceptButton = this.ResizeButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.ButtonCancel;
			this.ClientSize = new System.Drawing.Size(328, 149);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.ResizeButton);
			this.Controls.Add(this.ButtonCancel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "LevelResizeWizard";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Level resize wizard ";
			((System.ComponentModel.ISupportInitialize)(this.DesiredWidth)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.DesiredHeight)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button ButtonCancel;
		private System.Windows.Forms.Button ResizeButton;
		private System.Windows.Forms.NumericUpDown DesiredWidth;
		private System.Windows.Forms.NumericUpDown DesiredHeight;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label LevelHeightLabel;
		private System.Windows.Forms.Label LevelWidthLabel;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.GroupBox groupBox2;
	}
}