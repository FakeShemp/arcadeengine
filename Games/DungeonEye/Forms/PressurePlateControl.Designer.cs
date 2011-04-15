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
			this.AffectItemsBox = new System.Windows.Forms.CheckBox();
			this.AffectMonstersBox = new System.Windows.Forms.CheckBox();
			this.AffectTeamBox = new System.Windows.Forms.CheckBox();
			this.HiddenBox = new System.Windows.Forms.CheckBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.ActionBox = new DungeonEye.Forms.ActionControl();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.AffectItemsBox);
			this.groupBox1.Controls.Add(this.AffectMonstersBox);
			this.groupBox1.Controls.Add(this.AffectTeamBox);
			this.groupBox1.Controls.Add(this.HiddenBox);
			this.groupBox1.Location = new System.Drawing.Point(6, 175);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(350, 146);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Properties :";
			// 
			// AffectItemsBox
			// 
			this.AffectItemsBox.AutoSize = true;
			this.AffectItemsBox.Location = new System.Drawing.Point(92, 63);
			this.AffectItemsBox.Name = "AffectItemsBox";
			this.AffectItemsBox.Size = new System.Drawing.Size(81, 17);
			this.AffectItemsBox.TabIndex = 1;
			this.AffectItemsBox.Text = "Affect items";
			this.AffectItemsBox.UseVisualStyleBackColor = true;
			this.AffectItemsBox.CheckedChanged += new System.EventHandler(this.AffectItemsBox_CheckedChanged);
			// 
			// AffectMonstersBox
			// 
			this.AffectMonstersBox.AutoSize = true;
			this.AffectMonstersBox.Location = new System.Drawing.Point(92, 40);
			this.AffectMonstersBox.Name = "AffectMonstersBox";
			this.AffectMonstersBox.Size = new System.Drawing.Size(99, 17);
			this.AffectMonstersBox.TabIndex = 1;
			this.AffectMonstersBox.Text = "Affect monsters";
			this.AffectMonstersBox.UseVisualStyleBackColor = true;
			this.AffectMonstersBox.CheckedChanged += new System.EventHandler(this.AffectMonstersBox_CheckedChanged);
			// 
			// AffectTeamBox
			// 
			this.AffectTeamBox.AutoSize = true;
			this.AffectTeamBox.Location = new System.Drawing.Point(92, 19);
			this.AffectTeamBox.Name = "AffectTeamBox";
			this.AffectTeamBox.Size = new System.Drawing.Size(84, 17);
			this.AffectTeamBox.TabIndex = 1;
			this.AffectTeamBox.Text = "Affect Team";
			this.AffectTeamBox.UseVisualStyleBackColor = true;
			this.AffectTeamBox.CheckedChanged += new System.EventHandler(this.AffectTeamBox_CheckedChanged);
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
			this.ActionBox.Actions = null;
			this.ActionBox.Dungeon = null;
			this.ActionBox.Location = new System.Drawing.Point(6, 19);
			this.ActionBox.MinimumSize = new System.Drawing.Size(350, 150);
			this.ActionBox.Name = "ActionBox";
			this.ActionBox.Size = new System.Drawing.Size(350, 150);
			this.ActionBox.TabIndex = 1;
			this.ActionBox.Title = "Actions :";
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
		private ActionControl ActionBox;
		private System.Windows.Forms.CheckBox AffectItemsBox;
		private System.Windows.Forms.CheckBox AffectMonstersBox;
		private System.Windows.Forms.CheckBox AffectTeamBox;
	}
}