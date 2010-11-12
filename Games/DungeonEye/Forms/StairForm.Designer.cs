namespace DungeonEye.Forms
{
	partial class StairForm
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
			this.DirectionBox = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.DoneBox = new System.Windows.Forms.Button();
			this.TargetBox = new DungeonEye.Forms.TargetControl();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.DirectionBox);
			this.groupBox1.Location = new System.Drawing.Point(12, 118);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(175, 60);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Properties :";
			// 
			// DirectionBox
			// 
			this.DirectionBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.DirectionBox.FormattingEnabled = true;
			this.DirectionBox.Location = new System.Drawing.Point(64, 19);
			this.DirectionBox.Name = "DirectionBox";
			this.DirectionBox.Size = new System.Drawing.Size(105, 21);
			this.DirectionBox.TabIndex = 0;
			this.DirectionBox.SelectedIndexChanged += new System.EventHandler(this.DirectionBox_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 22);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(52, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Direction ";
			// 
			// DoneBox
			// 
			this.DoneBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.DoneBox.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.DoneBox.Location = new System.Drawing.Point(108, 192);
			this.DoneBox.Name = "DoneBox";
			this.DoneBox.Size = new System.Drawing.Size(75, 23);
			this.DoneBox.TabIndex = 2;
			this.DoneBox.Text = "Done";
			this.DoneBox.UseVisualStyleBackColor = true;
			// 
			// TargetBox
			// 
			this.TargetBox.Dungeon = null;
			this.TargetBox.Location = new System.Drawing.Point(12, 12);
			this.TargetBox.MinimumSize = new System.Drawing.Size(175, 100);
			this.TargetBox.Name = "TargetBox";
			this.TargetBox.Size = new System.Drawing.Size(175, 100);
			this.TargetBox.TabIndex = 0;
			this.TargetBox.TargetChanged += new DungeonEye.Forms.TargetControl.ChangedEventHandler(this.TargetBox_TargetChanged);
			// 
			// StairForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(195, 227);
			this.Controls.Add(this.DoneBox);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.TargetBox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "StairForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Stair wizard";
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private TargetControl TargetBox;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox DirectionBox;
		private System.Windows.Forms.Button DoneBox;
	}
}