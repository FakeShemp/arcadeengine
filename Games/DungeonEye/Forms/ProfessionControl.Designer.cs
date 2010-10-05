namespace DungeonEye.Forms
{
	partial class ProfessionControl
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
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.XPBox = new System.Windows.Forms.NumericUpDown();
			this.ClassBox = new System.Windows.Forms.ComboBox();
			this.LevelBox = new System.Windows.Forms.TextBox();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize) (this.XPBox)).BeginInit();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.LevelBox);
			this.groupBox1.Controls.Add(this.ClassBox);
			this.groupBox1.Controls.Add(this.XPBox);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox1.Location = new System.Drawing.Point(0, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(407, 81);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Profession :";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 21);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(66, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Experience :";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6, 45);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(39, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "Level :";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(217, 22);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(38, 13);
			this.label3.TabIndex = 2;
			this.label3.Text = "Class :";
			// 
			// XPBox
			// 
			this.XPBox.Location = new System.Drawing.Point(78, 19);
			this.XPBox.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
			this.XPBox.Name = "XPBox";
			this.XPBox.Size = new System.Drawing.Size(120, 20);
			this.XPBox.TabIndex = 3;
			this.XPBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.XPBox.ThousandsSeparator = true;
			// 
			// ClassBox
			// 
			this.ClassBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ClassBox.FormattingEnabled = true;
			this.ClassBox.Location = new System.Drawing.Point(261, 19);
			this.ClassBox.Name = "ClassBox";
			this.ClassBox.Size = new System.Drawing.Size(121, 21);
			this.ClassBox.TabIndex = 4;
			// 
			// LevelBox
			// 
			this.LevelBox.Location = new System.Drawing.Point(78, 45);
			this.LevelBox.Name = "LevelBox";
			this.LevelBox.ReadOnly = true;
			this.LevelBox.Size = new System.Drawing.Size(48, 20);
			this.LevelBox.TabIndex = 5;
			// 
			// ProfessionControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.groupBox1);
			this.Name = "ProfessionControl";
			this.Size = new System.Drawing.Size(407, 81);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize) (this.XPBox)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox LevelBox;
		private System.Windows.Forms.ComboBox ClassBox;
		private System.Windows.Forms.NumericUpDown XPBox;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
	}
}
