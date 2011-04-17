namespace DungeonEye.Forms
{
	partial class SwitchCountControl
	{
		/// <summary> 
		/// Variable nécessaire au concepteur.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Nettoyage des ressources utilisées.
		/// </summary>
		/// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Code généré par le Concepteur de composants

		/// <summary> 
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.PanelBox = new System.Windows.Forms.Panel();
			this.TargetBox = new System.Windows.Forms.NumericUpDown();
			this.label1 = new System.Windows.Forms.Label();
			this.ResetBox = new System.Windows.Forms.CheckBox();
			this.label2 = new System.Windows.Forms.Label();
			this.CountBox = new System.Windows.Forms.NumericUpDown();
			this.EnabledBox = new System.Windows.Forms.CheckBox();
			this.groupBox1.SuspendLayout();
			this.PanelBox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.TargetBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.CountBox)).BeginInit();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.PanelBox);
			this.groupBox1.Controls.Add(this.EnabledBox);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox1.Location = new System.Drawing.Point(0, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(130, 130);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Switch count";
			// 
			// PanelBox
			// 
			this.PanelBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.PanelBox.Controls.Add(this.TargetBox);
			this.PanelBox.Controls.Add(this.label1);
			this.PanelBox.Controls.Add(this.ResetBox);
			this.PanelBox.Controls.Add(this.label2);
			this.PanelBox.Controls.Add(this.CountBox);
			this.PanelBox.Location = new System.Drawing.Point(6, 42);
			this.PanelBox.Name = "PanelBox";
			this.PanelBox.Size = new System.Drawing.Size(118, 85);
			this.PanelBox.TabIndex = 4;
			// 
			// TargetBox
			// 
			this.TargetBox.Location = new System.Drawing.Point(48, 3);
			this.TargetBox.Maximum = new decimal(new int[] {
            -1530494977,
            232830,
            0,
            0});
			this.TargetBox.Name = "TargetBox";
			this.TargetBox.Size = new System.Drawing.Size(54, 20);
			this.TargetBox.TabIndex = 1;
			this.TargetBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.TargetBox.ThousandsSeparator = true;
			this.TargetBox.ValueChanged += new System.EventHandler(this.NeededBox_ValueChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(4, 5);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(38, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Target";
			// 
			// ResetBox
			// 
			this.ResetBox.Appearance = System.Windows.Forms.Appearance.Button;
			this.ResetBox.AutoSize = true;
			this.ResetBox.Location = new System.Drawing.Point(10, 55);
			this.ResetBox.Name = "ResetBox";
			this.ResetBox.Size = new System.Drawing.Size(92, 23);
			this.ResetBox.TabIndex = 2;
			this.ResetBox.Text = "Reset on trigger";
			this.ResetBox.UseVisualStyleBackColor = true;
			this.ResetBox.CheckedChanged += new System.EventHandler(this.ResetBox_CheckedChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(7, 31);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(35, 13);
			this.label2.TabIndex = 0;
			this.label2.Text = "Count";
			// 
			// CountBox
			// 
			this.CountBox.Location = new System.Drawing.Point(48, 29);
			this.CountBox.Maximum = new decimal(new int[] {
            -1530494977,
            232830,
            0,
            0});
			this.CountBox.Name = "CountBox";
			this.CountBox.Size = new System.Drawing.Size(54, 20);
			this.CountBox.TabIndex = 1;
			this.CountBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.CountBox.ThousandsSeparator = true;
			this.CountBox.ValueChanged += new System.EventHandler(this.RemainingBox_ValueChanged);
			// 
			// EnabledBox
			// 
			this.EnabledBox.AutoSize = true;
			this.EnabledBox.Location = new System.Drawing.Point(6, 19);
			this.EnabledBox.Name = "EnabledBox";
			this.EnabledBox.Size = new System.Drawing.Size(65, 17);
			this.EnabledBox.TabIndex = 3;
			this.EnabledBox.Text = "Enabled";
			this.EnabledBox.UseVisualStyleBackColor = true;
			this.EnabledBox.CheckedChanged += new System.EventHandler(this.EnabledBox_CheckedChanged);
			// 
			// SwitchCountControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.groupBox1);
			this.MinimumSize = new System.Drawing.Size(130, 130);
			this.Name = "SwitchCountControl";
			this.Size = new System.Drawing.Size(130, 130);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.PanelBox.ResumeLayout(false);
			this.PanelBox.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.TargetBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.CountBox)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.NumericUpDown CountBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.NumericUpDown TargetBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox ResetBox;
		private System.Windows.Forms.CheckBox EnabledBox;
		private System.Windows.Forms.Panel PanelBox;
	}
}
