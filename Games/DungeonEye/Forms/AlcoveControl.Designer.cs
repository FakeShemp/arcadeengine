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
			this.GLControl = new OpenTK.GLControl();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.ClearBox = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.DecorationBox = new System.Windows.Forms.NumericUpDown();
			this.NorthBox = new System.Windows.Forms.Button();
			this.SouthBox = new System.Windows.Forms.Button();
			this.WestBox = new System.Windows.Forms.Button();
			this.EastBox = new System.Windows.Forms.Button();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.DecorationBox)).BeginInit();
			this.SuspendLayout();
			// 
			// GLControl
			// 
			this.GLControl.BackColor = System.Drawing.Color.Black;
			this.GLControl.Location = new System.Drawing.Point(6, 19);
			this.GLControl.Name = "GLControl";
			this.GLControl.Size = new System.Drawing.Size(352, 240);
			this.GLControl.TabIndex = 1;
			this.GLControl.VSync = false;
			this.GLControl.Load += new System.EventHandler(this.GLControl_Load);
			this.GLControl.Paint += new System.Windows.Forms.PaintEventHandler(this.GLControl_Paint);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.ClearBox);
			this.groupBox2.Controls.Add(this.label1);
			this.groupBox2.Controls.Add(this.DecorationBox);
			this.groupBox2.Controls.Add(this.GLControl);
			this.groupBox2.Location = new System.Drawing.Point(3, 32);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(365, 292);
			this.groupBox2.TabIndex = 2;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Visual properties";
			// 
			// ClearBox
			// 
			this.ClearBox.Location = new System.Drawing.Point(283, 262);
			this.ClearBox.Name = "ClearBox";
			this.ClearBox.Size = new System.Drawing.Size(75, 23);
			this.ClearBox.TabIndex = 4;
			this.ClearBox.Text = "Clear";
			this.ClearBox.UseVisualStyleBackColor = true;
			this.ClearBox.Click += new System.EventHandler(this.ClearBox_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 267);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(59, 13);
			this.label1.TabIndex = 3;
			this.label1.Text = "Decoration";
			// 
			// DecorationBox
			// 
			this.DecorationBox.Location = new System.Drawing.Point(71, 265);
			this.DecorationBox.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
			this.DecorationBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
			this.DecorationBox.Name = "DecorationBox";
			this.DecorationBox.Size = new System.Drawing.Size(67, 20);
			this.DecorationBox.TabIndex = 2;
			this.DecorationBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.DecorationBox.ThousandsSeparator = true;
			this.DecorationBox.Value = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
			this.DecorationBox.ValueChanged += new System.EventHandler(this.DecorationBox_ValueChanged);
			// 
			// NorthBox
			// 
			this.NorthBox.Location = new System.Drawing.Point(3, 3);
			this.NorthBox.Name = "NorthBox";
			this.NorthBox.Size = new System.Drawing.Size(75, 23);
			this.NorthBox.TabIndex = 3;
			this.NorthBox.Text = "North";
			this.NorthBox.UseVisualStyleBackColor = true;
			this.NorthBox.Click += new System.EventHandler(this.NorthBox_Click);
			// 
			// SouthBox
			// 
			this.SouthBox.Location = new System.Drawing.Point(84, 3);
			this.SouthBox.Name = "SouthBox";
			this.SouthBox.Size = new System.Drawing.Size(75, 23);
			this.SouthBox.TabIndex = 3;
			this.SouthBox.Text = "South";
			this.SouthBox.UseVisualStyleBackColor = true;
			this.SouthBox.Click += new System.EventHandler(this.SouthBox_Click);
			// 
			// WestBox
			// 
			this.WestBox.Location = new System.Drawing.Point(165, 3);
			this.WestBox.Name = "WestBox";
			this.WestBox.Size = new System.Drawing.Size(75, 23);
			this.WestBox.TabIndex = 3;
			this.WestBox.Text = "West";
			this.WestBox.UseVisualStyleBackColor = true;
			this.WestBox.Click += new System.EventHandler(this.WestBox_Click);
			// 
			// EastBox
			// 
			this.EastBox.Location = new System.Drawing.Point(246, 3);
			this.EastBox.Name = "EastBox";
			this.EastBox.Size = new System.Drawing.Size(75, 23);
			this.EastBox.TabIndex = 3;
			this.EastBox.Text = "East";
			this.EastBox.UseVisualStyleBackColor = true;
			this.EastBox.Click += new System.EventHandler(this.EastBox_Click);
			// 
			// AlcoveControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.EastBox);
			this.Controls.Add(this.WestBox);
			this.Controls.Add(this.SouthBox);
			this.Controls.Add(this.NorthBox);
			this.Controls.Add(this.groupBox2);
			this.Name = "AlcoveControl";
			this.Size = new System.Drawing.Size(788, 651);
			this.Load += new System.EventHandler(this.AlcoveControl_Load);
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.DecorationBox)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private OpenTK.GLControl GLControl;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.NumericUpDown DecorationBox;
		private System.Windows.Forms.Button ClearBox;
		private System.Windows.Forms.Button NorthBox;
		private System.Windows.Forms.Button SouthBox;
		private System.Windows.Forms.Button WestBox;
		private System.Windows.Forms.Button EastBox;
	}
}
