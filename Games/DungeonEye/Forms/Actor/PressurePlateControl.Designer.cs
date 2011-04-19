namespace DungeonEye.Forms
{
	partial class PressurePlateControl
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
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.ActionBox = new DungeonEye.Forms.WallSwitchScriptListControl();
			this.ConditionBox = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.ConditionBox);
			this.groupBox1.Controls.Add(this.HiddenBox);
			this.groupBox1.Location = new System.Drawing.Point(6, 19);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(350, 100);
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
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.ActionBox);
			this.groupBox2.Controls.Add(this.groupBox1);
			this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox2.Location = new System.Drawing.Point(0, 0);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(706, 507);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Pressure plate";
			// 
			// ActionBox
			// 
			this.ActionBox.Dungeon = null;
			this.ActionBox.Location = new System.Drawing.Point(6, 125);
			this.ActionBox.MinimumSize = new System.Drawing.Size(350, 150);
			this.ActionBox.Name = "ActionBox";
			this.ActionBox.Size = new System.Drawing.Size(350, 150);
			this.ActionBox.TabIndex = 1;
			this.ActionBox.Title = "Actions :";
			// 
			// ConditionBox
			// 
			this.ConditionBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ConditionBox.FormattingEnabled = true;
			this.ConditionBox.Location = new System.Drawing.Point(200, 15);
			this.ConditionBox.Name = "ConditionBox";
			this.ConditionBox.Size = new System.Drawing.Size(144, 21);
			this.ConditionBox.TabIndex = 2;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(143, 18);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(51, 13);
			this.label1.TabIndex = 3;
			this.label1.Text = "Condition";
			// 
			// PressurePlateControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.groupBox2);
			this.Name = "PressurePlateControl";
			this.Size = new System.Drawing.Size(706, 507);
			this.Load += new System.EventHandler(this.PressurePlateControl_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckBox HiddenBox;
		private System.Windows.Forms.GroupBox groupBox2;
		private WallSwitchScriptListControl ActionBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox ConditionBox;
	}
}