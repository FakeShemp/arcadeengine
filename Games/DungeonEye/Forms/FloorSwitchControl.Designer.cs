namespace DungeonEye.Forms
{
	partial class FloorSwitchControl
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
			this.HiddenBox = new System.Windows.Forms.CheckBox();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.HiddenBox);
			this.groupBox1.Location = new System.Drawing.Point(12, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(102, 64);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Properties :";
			// 
			// HiddenBox
			// 
			this.HiddenBox.AutoSize = true;
			this.HiddenBox.Location = new System.Drawing.Point(6, 19);
			this.HiddenBox.Name = "HiddenBox";
			this.HiddenBox.Size = new System.Drawing.Size(60, 17);
			this.HiddenBox.TabIndex = 0;
			this.HiddenBox.Text = "Hidden";
			this.HiddenBox.UseVisualStyleBackColor = true;
			this.HiddenBox.CheckedChanged += new System.EventHandler(this.HiddenBox_CheckedChanged);
			// 
			// FloorSwitchControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.groupBox1);
			this.Name = "FloorSwitchControl";
			this.Size = new System.Drawing.Size(131, 128);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckBox HiddenBox;
	}
}