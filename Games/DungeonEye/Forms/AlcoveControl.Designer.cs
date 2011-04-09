namespace DungeonEye.Forms
{
	partial class AlcoveControl
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
			this.NorthBox = new System.Windows.Forms.CheckBox();
			this.WestBox = new System.Windows.Forms.CheckBox();
			this.EastBox = new System.Windows.Forms.CheckBox();
			this.SouthBox = new System.Windows.Forms.CheckBox();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.SouthBox);
			this.groupBox1.Controls.Add(this.EastBox);
			this.groupBox1.Controls.Add(this.WestBox);
			this.groupBox1.Controls.Add(this.NorthBox);
			this.groupBox1.Location = new System.Drawing.Point(3, 3);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(150, 98);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Sides";
			// 
			// NorthBox
			// 
			this.NorthBox.Appearance = System.Windows.Forms.Appearance.Button;
			this.NorthBox.AutoSize = true;
			this.NorthBox.Location = new System.Drawing.Point(54, 19);
			this.NorthBox.Name = "NorthBox";
			this.NorthBox.Size = new System.Drawing.Size(43, 23);
			this.NorthBox.TabIndex = 0;
			this.NorthBox.Text = "North";
			this.NorthBox.UseVisualStyleBackColor = true;
			this.NorthBox.CheckedChanged += new System.EventHandler(this.NorthBox_CheckedChanged);
			// 
			// WestBox
			// 
			this.WestBox.Appearance = System.Windows.Forms.Appearance.Button;
			this.WestBox.AutoSize = true;
			this.WestBox.Location = new System.Drawing.Point(6, 38);
			this.WestBox.Name = "WestBox";
			this.WestBox.Size = new System.Drawing.Size(42, 23);
			this.WestBox.TabIndex = 0;
			this.WestBox.Text = "West";
			this.WestBox.UseVisualStyleBackColor = true;
			this.WestBox.CheckedChanged += new System.EventHandler(this.WestBox_CheckedChanged);
			// 
			// EastBox
			// 
			this.EastBox.Appearance = System.Windows.Forms.Appearance.Button;
			this.EastBox.AutoSize = true;
			this.EastBox.Location = new System.Drawing.Point(103, 38);
			this.EastBox.Name = "EastBox";
			this.EastBox.Size = new System.Drawing.Size(38, 23);
			this.EastBox.TabIndex = 0;
			this.EastBox.Text = "East";
			this.EastBox.UseVisualStyleBackColor = true;
			this.EastBox.CheckedChanged += new System.EventHandler(this.EastBox_CheckedChanged);
			// 
			// SouthBox
			// 
			this.SouthBox.Appearance = System.Windows.Forms.Appearance.Button;
			this.SouthBox.AutoSize = true;
			this.SouthBox.Location = new System.Drawing.Point(54, 67);
			this.SouthBox.Name = "SouthBox";
			this.SouthBox.Size = new System.Drawing.Size(45, 23);
			this.SouthBox.TabIndex = 0;
			this.SouthBox.Text = "South";
			this.SouthBox.UseVisualStyleBackColor = true;
			this.SouthBox.CheckedChanged += new System.EventHandler(this.SouthBox_CheckedChanged);
			// 
			// AlcoveControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.groupBox1);
			this.Name = "AlcoveControl";
			this.Size = new System.Drawing.Size(400, 338);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckBox SouthBox;
		private System.Windows.Forms.CheckBox EastBox;
		private System.Windows.Forms.CheckBox WestBox;
		private System.Windows.Forms.CheckBox NorthBox;
	}
}
