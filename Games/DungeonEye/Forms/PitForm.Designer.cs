namespace DungeonEye.Forms
{
	partial class PitForm
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
			DungeonEye.Dice dice1 = new DungeonEye.Dice();
			this.DoneBox = new System.Windows.Forms.Button();
			this.DamageBox = new DungeonEye.Forms.DiceControl();
			this.TargetBox = new DungeonEye.Forms.TargetControl();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.IsIllusionBox = new System.Windows.Forms.CheckBox();
			this.IsHiddenBox = new System.Windows.Forms.CheckBox();
			this.label1 = new System.Windows.Forms.Label();
			this.DifficultyBox = new System.Windows.Forms.NumericUpDown();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.DifficultyBox)).BeginInit();
			this.SuspendLayout();
			// 
			// DoneBox
			// 
			this.DoneBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.DoneBox.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.DoneBox.Location = new System.Drawing.Point(343, 162);
			this.DoneBox.Name = "DoneBox";
			this.DoneBox.Size = new System.Drawing.Size(75, 23);
			this.DoneBox.TabIndex = 0;
			this.DoneBox.Text = "Done";
			this.DoneBox.UseVisualStyleBackColor = true;
			// 
			// DamageBox
			// 
			this.DamageBox.ControlText = "Damage :";
			dice1.Faces = 1;
			dice1.Modifier = 0;
			dice1.Throws = 1;
			this.DamageBox.Dice = dice1;
			this.DamageBox.Location = new System.Drawing.Point(12, 12);
			this.DamageBox.MinimumSize = new System.Drawing.Size(230, 100);
			this.DamageBox.Name = "DamageBox";
			this.DamageBox.Size = new System.Drawing.Size(230, 100);
			this.DamageBox.TabIndex = 1;
			this.DamageBox.ValueChanged += new System.EventHandler(this.DamageBox_ValueChanged);
			// 
			// TargetBox
			// 
			this.TargetBox.Dungeon = null;
			this.TargetBox.Location = new System.Drawing.Point(248, 12);
			this.TargetBox.MinimumSize = new System.Drawing.Size(175, 100);
			this.TargetBox.Name = "TargetBox";
			this.TargetBox.Size = new System.Drawing.Size(175, 100);
			this.TargetBox.TabIndex = 2;
			this.TargetBox.TargetChanged += new DungeonEye.Forms.TargetControl.ChangedEventHandler(this.TargetBox_TargetChanged);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.DifficultyBox);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.IsHiddenBox);
			this.groupBox1.Controls.Add(this.IsIllusionBox);
			this.groupBox1.Location = new System.Drawing.Point(12, 118);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(230, 72);
			this.groupBox1.TabIndex = 3;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "groupBox1";
			// 
			// IsIllusionBox
			// 
			this.IsIllusionBox.AutoSize = true;
			this.IsIllusionBox.Location = new System.Drawing.Point(6, 19);
			this.IsIllusionBox.Name = "IsIllusionBox";
			this.IsIllusionBox.Size = new System.Drawing.Size(58, 17);
			this.IsIllusionBox.TabIndex = 0;
			this.IsIllusionBox.Text = "Illusion";
			this.IsIllusionBox.UseVisualStyleBackColor = true;
			this.IsIllusionBox.CheckedChanged += new System.EventHandler(this.IsIllusionBox_CheckedChanged);
			// 
			// IsHiddenBox
			// 
			this.IsHiddenBox.AutoSize = true;
			this.IsHiddenBox.Location = new System.Drawing.Point(6, 42);
			this.IsHiddenBox.Name = "IsHiddenBox";
			this.IsHiddenBox.Size = new System.Drawing.Size(60, 17);
			this.IsHiddenBox.TabIndex = 0;
			this.IsHiddenBox.Text = "Hidden";
			this.IsHiddenBox.UseVisualStyleBackColor = true;
			this.IsHiddenBox.CheckedChanged += new System.EventHandler(this.IsHiddenBox_CheckedChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(118, 20);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(47, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Difficulty";
			// 
			// DifficultyBox
			// 
			this.DifficultyBox.Location = new System.Drawing.Point(171, 18);
			this.DifficultyBox.Name = "DifficultyBox";
			this.DifficultyBox.Size = new System.Drawing.Size(53, 20);
			this.DifficultyBox.TabIndex = 2;
			this.DifficultyBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.DifficultyBox.ThousandsSeparator = true;
			this.DifficultyBox.ValueChanged += new System.EventHandler(this.DifficultyBox_ValueChanged);
			// 
			// PitForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(430, 197);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.TargetBox);
			this.Controls.Add(this.DamageBox);
			this.Controls.Add(this.DoneBox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "PitForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Pit wizard";
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PitForm_KeyDown);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.DifficultyBox)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button DoneBox;
		private DiceControl DamageBox;
		private TargetControl TargetBox;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.NumericUpDown DifficultyBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox IsHiddenBox;
		private System.Windows.Forms.CheckBox IsIllusionBox;
	}
}