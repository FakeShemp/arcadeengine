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
			this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
			this.label2 = new System.Windows.Forms.Label();
			this.CountBox = new System.Windows.Forms.NumericUpDown();
			this.label1 = new System.Windows.Forms.Label();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.CountBox)).BeginInit();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.checkBox1);
			this.groupBox1.Controls.Add(this.numericUpDown1);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.CountBox);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox1.Location = new System.Drawing.Point(0, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(130, 100);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Switch count";
			// 
			// numericUpDown1
			// 
			this.numericUpDown1.Location = new System.Drawing.Point(69, 40);
			this.numericUpDown1.Maximum = new decimal(new int[] {
            -1530494977,
            232830,
            0,
            0});
			this.numericUpDown1.Name = "numericUpDown1";
			this.numericUpDown1.Size = new System.Drawing.Size(54, 20);
			this.numericUpDown1.TabIndex = 1;
			this.numericUpDown1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.numericUpDown1.ThousandsSeparator = true;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6, 42);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(57, 13);
			this.label2.TabIndex = 0;
			this.label2.Text = "Remaining";
			// 
			// CountBox
			// 
			this.CountBox.Location = new System.Drawing.Point(69, 14);
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
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(18, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(45, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Needed";
			// 
			// checkBox1
			// 
			this.checkBox1.Appearance = System.Windows.Forms.Appearance.Button;
			this.checkBox1.AutoSize = true;
			this.checkBox1.Location = new System.Drawing.Point(31, 66);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(92, 23);
			this.checkBox1.TabIndex = 2;
			this.checkBox1.Text = "Reset on trigger";
			this.checkBox1.UseVisualStyleBackColor = true;
			// 
			// SwitchCountControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.groupBox1);
			this.MinimumSize = new System.Drawing.Size(130, 100);
			this.Name = "SwitchCountControl";
			this.Size = new System.Drawing.Size(130, 100);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.CountBox)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.NumericUpDown numericUpDown1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.NumericUpDown CountBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox checkBox1;
	}
}
