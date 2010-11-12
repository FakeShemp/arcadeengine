namespace DungeonEye.Forms
{
	partial class FloorSwitchForm
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
			this.DoneBox = new System.Windows.Forms.Button();
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
			// DoneBox
			// 
			this.DoneBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.DoneBox.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.DoneBox.Location = new System.Drawing.Point(44, 93);
			this.DoneBox.Name = "DoneBox";
			this.DoneBox.Size = new System.Drawing.Size(75, 23);
			this.DoneBox.TabIndex = 1;
			this.DoneBox.Text = "Done";
			this.DoneBox.UseVisualStyleBackColor = true;
			// 
			// FloorSwitchForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(131, 128);
			this.Controls.Add(this.DoneBox);
			this.Controls.Add(this.groupBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FloorSwitchForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Floor switch wizard";
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FloorSwitchForm_KeyDown);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckBox HiddenBox;
		private System.Windows.Forms.Button DoneBox;
	}
}