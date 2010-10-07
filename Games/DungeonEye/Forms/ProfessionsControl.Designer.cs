namespace DungeonEye.Forms
{
	partial class ProfessionsControl
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProfessionsControl));
			this.PropertiesBox = new System.Windows.Forms.GroupBox();
			this.LevelBox = new System.Windows.Forms.TextBox();
			this.XPBox = new System.Windows.Forms.NumericUpDown();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.ClassBox = new System.Windows.Forms.ComboBox();
			this.AddClassBox = new System.Windows.Forms.Button();
			this.RemoveClassBox = new System.Windows.Forms.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.AlertPanel = new System.Windows.Forms.Panel();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.label9 = new System.Windows.Forms.Label();
			this.SummaryBox = new System.Windows.Forms.TextBox();
			this.PropertiesBox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize) (this.XPBox)).BeginInit();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.AlertPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize) (this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// PropertiesBox
			// 
			this.PropertiesBox.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.PropertiesBox.Controls.Add(this.LevelBox);
			this.PropertiesBox.Controls.Add(this.XPBox);
			this.PropertiesBox.Controls.Add(this.label2);
			this.PropertiesBox.Controls.Add(this.label1);
			this.PropertiesBox.Location = new System.Drawing.Point(177, 16);
			this.PropertiesBox.Name = "PropertiesBox";
			this.PropertiesBox.Size = new System.Drawing.Size(170, 87);
			this.PropertiesBox.TabIndex = 0;
			this.PropertiesBox.TabStop = false;
			this.PropertiesBox.Text = "Properties :";
			// 
			// LevelBox
			// 
			this.LevelBox.Location = new System.Drawing.Point(76, 45);
			this.LevelBox.Name = "LevelBox";
			this.LevelBox.ReadOnly = true;
			this.LevelBox.Size = new System.Drawing.Size(48, 20);
			this.LevelBox.TabIndex = 5;
			this.LevelBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// XPBox
			// 
			this.XPBox.Location = new System.Drawing.Point(77, 19);
			this.XPBox.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
			this.XPBox.Name = "XPBox";
			this.XPBox.Size = new System.Drawing.Size(85, 20);
			this.XPBox.TabIndex = 3;
			this.XPBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.XPBox.ThousandsSeparator = true;
			this.XPBox.ValueChanged += new System.EventHandler(this.XPBox_ValueChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(8, 48);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(39, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "Level :";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(5, 21);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(66, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Experience :";
			// 
			// ClassBox
			// 
			this.ClassBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ClassBox.FormattingEnabled = true;
			this.ClassBox.Location = new System.Drawing.Point(6, 19);
			this.ClassBox.Name = "ClassBox";
			this.ClassBox.Size = new System.Drawing.Size(157, 21);
			this.ClassBox.Sorted = true;
			this.ClassBox.TabIndex = 4;
			this.ClassBox.SelectedIndexChanged += new System.EventHandler(this.ClassBox_SelectedIndexChanged);
			// 
			// AddClassBox
			// 
			this.AddClassBox.Location = new System.Drawing.Point(3, 48);
			this.AddClassBox.Name = "AddClassBox";
			this.AddClassBox.Size = new System.Drawing.Size(75, 23);
			this.AddClassBox.TabIndex = 6;
			this.AddClassBox.Text = "Add";
			this.AddClassBox.UseVisualStyleBackColor = true;
			this.AddClassBox.Click += new System.EventHandler(this.AddClassBox_Click);
			// 
			// RemoveClassBox
			// 
			this.RemoveClassBox.Location = new System.Drawing.Point(88, 48);
			this.RemoveClassBox.Name = "RemoveClassBox";
			this.RemoveClassBox.Size = new System.Drawing.Size(75, 23);
			this.RemoveClassBox.TabIndex = 7;
			this.RemoveClassBox.Text = "Remove";
			this.RemoveClassBox.UseVisualStyleBackColor = true;
			this.RemoveClassBox.Click += new System.EventHandler(this.RemoveClassBox_Click);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.groupBox1);
			this.groupBox2.Controls.Add(this.groupBox3);
			this.groupBox2.Controls.Add(this.PropertiesBox);
			this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox2.Location = new System.Drawing.Point(0, 0);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(350, 280);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Professions :";
			// 
			// groupBox3
			// 
			this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox3.Controls.Add(this.RemoveClassBox);
			this.groupBox3.Controls.Add(this.AddClassBox);
			this.groupBox3.Controls.Add(this.ClassBox);
			this.groupBox3.Location = new System.Drawing.Point(3, 16);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(168, 87);
			this.groupBox3.TabIndex = 1;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Class :";
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.SummaryBox);
			this.groupBox1.Controls.Add(this.AlertPanel);
			this.groupBox1.Location = new System.Drawing.Point(6, 109);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(341, 165);
			this.groupBox1.TabIndex = 2;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Summary :";
			// 
			// AlertPanel
			// 
			this.AlertPanel.Controls.Add(this.label9);
			this.AlertPanel.Controls.Add(this.pictureBox1);
			this.AlertPanel.Dock = System.Windows.Forms.DockStyle.Right;
			this.AlertPanel.Location = new System.Drawing.Point(199, 16);
			this.AlertPanel.Name = "AlertPanel";
			this.AlertPanel.Size = new System.Drawing.Size(139, 146);
			this.AlertPanel.TabIndex = 6;
			this.AlertPanel.Visible = false;
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = ((System.Drawing.Image) (resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(64, 18);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(16, 16);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(7, 65);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(120, 13);
			this.label9.TabIndex = 1;
			this.label9.Text = "Illegal multi class Hero !!";
			// 
			// SummaryBox
			// 
			this.SummaryBox.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.SummaryBox.Location = new System.Drawing.Point(6, 19);
			this.SummaryBox.Multiline = true;
			this.SummaryBox.Name = "SummaryBox";
			this.SummaryBox.ReadOnly = true;
			this.SummaryBox.Size = new System.Drawing.Size(187, 140);
			this.SummaryBox.TabIndex = 7;
			// 
			// ProfessionsControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.groupBox2);
			this.MinimumSize = new System.Drawing.Size(350, 280);
			this.Name = "ProfessionsControl";
			this.Size = new System.Drawing.Size(350, 280);
			this.PropertiesBox.ResumeLayout(false);
			this.PropertiesBox.PerformLayout();
			((System.ComponentModel.ISupportInitialize) (this.XPBox)).EndInit();
			this.groupBox2.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.AlertPanel.ResumeLayout(false);
			this.AlertPanel.PerformLayout();
			((System.ComponentModel.ISupportInitialize) (this.pictureBox1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox PropertiesBox;
		private System.Windows.Forms.TextBox LevelBox;
		private System.Windows.Forms.ComboBox ClassBox;
		private System.Windows.Forms.NumericUpDown XPBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button RemoveClassBox;
		private System.Windows.Forms.Button AddClassBox;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Panel AlertPanel;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.TextBox SummaryBox;
	}
}
