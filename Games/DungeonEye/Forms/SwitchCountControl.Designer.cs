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
			this.ResetBox = new System.Windows.Forms.CheckBox();
			this.RemainingBox = new System.Windows.Forms.NumericUpDown();
			this.label2 = new System.Windows.Forms.Label();
			this.NeededBox = new System.Windows.Forms.NumericUpDown();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.RemainingBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.NeededBox)).BeginInit();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.ResetBox);
			this.groupBox1.Controls.Add(this.RemainingBox);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.NeededBox);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox1.Location = new System.Drawing.Point(0, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(130, 100);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Switch count";
			// 
			// ResetBox
			// 
			this.ResetBox.Appearance = System.Windows.Forms.Appearance.Button;
			this.ResetBox.AutoSize = true;
			this.ResetBox.Location = new System.Drawing.Point(31, 66);
			this.ResetBox.Name = "ResetBox";
			this.ResetBox.Size = new System.Drawing.Size(92, 23);
			this.ResetBox.TabIndex = 2;
			this.ResetBox.Text = "Reset on trigger";
			this.ResetBox.UseVisualStyleBackColor = true;
			this.ResetBox.CheckedChanged += new System.EventHandler(this.ResetBox_CheckedChanged);
			// 
			// RemainingBox
			// 
			this.RemainingBox.Location = new System.Drawing.Point(69, 40);
			this.RemainingBox.Maximum = new decimal(new int[] {
            -1530494977,
            232830,
            0,
            0});
			this.RemainingBox.Name = "RemainingBox";
			this.RemainingBox.Size = new System.Drawing.Size(54, 20);
			this.RemainingBox.TabIndex = 1;
			this.RemainingBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.RemainingBox.ThousandsSeparator = true;
			this.RemainingBox.ValueChanged += new System.EventHandler(this.RemainingBox_ValueChanged);
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
			// NeededBox
			// 
			this.NeededBox.Location = new System.Drawing.Point(69, 14);
			this.NeededBox.Maximum = new decimal(new int[] {
            -1530494977,
            232830,
            0,
            0});
			this.NeededBox.Name = "NeededBox";
			this.NeededBox.Size = new System.Drawing.Size(54, 20);
			this.NeededBox.TabIndex = 1;
			this.NeededBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.NeededBox.ThousandsSeparator = true;
			this.NeededBox.ValueChanged += new System.EventHandler(this.NeededBox_ValueChanged);
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
			((System.ComponentModel.ISupportInitialize)(this.RemainingBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.NeededBox)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.NumericUpDown RemainingBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.NumericUpDown NeededBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox ResetBox;
	}
}
