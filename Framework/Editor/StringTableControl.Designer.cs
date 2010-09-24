namespace DungeonEye.Forms
{
	partial class StringTableControl
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StringTableControl));
			this.GroupBox = new System.Windows.Forms.GroupBox();
			this.LanguageBox = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.RefreshBox = new System.Windows.Forms.Button();
			this.PreviewBox = new System.Windows.Forms.Label();
			this.PreviewLabelBox = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.StringTableBox = new System.Windows.Forms.ComboBox();
			this.StringIDBox = new System.Windows.Forms.NumericUpDown();
			this.GroupBox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.StringIDBox)).BeginInit();
			this.SuspendLayout();
			// 
			// GroupBox
			// 
			this.GroupBox.Controls.Add(this.LanguageBox);
			this.GroupBox.Controls.Add(this.label1);
			this.GroupBox.Controls.Add(this.RefreshBox);
			this.GroupBox.Controls.Add(this.PreviewBox);
			this.GroupBox.Controls.Add(this.PreviewLabelBox);
			this.GroupBox.Controls.Add(this.label7);
			this.GroupBox.Controls.Add(this.label6);
			this.GroupBox.Controls.Add(this.StringTableBox);
			this.GroupBox.Controls.Add(this.StringIDBox);
			this.GroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.GroupBox.Location = new System.Drawing.Point(0, 0);
			this.GroupBox.Name = "GroupBox";
			this.GroupBox.Size = new System.Drawing.Size(488, 100);
			this.GroupBox.TabIndex = 9;
			this.GroupBox.TabStop = false;
			this.GroupBox.Text = "StringTable :";
			// 
			// LanguageBox
			// 
			this.LanguageBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.LanguageBox.FormattingEnabled = true;
			this.LanguageBox.Location = new System.Drawing.Point(85, 70);
			this.LanguageBox.Name = "LanguageBox";
			this.LanguageBox.Size = new System.Drawing.Size(121, 21);
			this.LanguageBox.Sorted = true;
			this.LanguageBox.TabIndex = 8;
			this.LanguageBox.SelectedIndexChanged += new System.EventHandler(this.LanguageBox_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(8, 73);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(61, 13);
			this.label1.TabIndex = 7;
			this.label1.Text = "Language :";
			// 
			// RefreshBox
			// 
			this.RefreshBox.AutoSize = true;
			this.RefreshBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.RefreshBox.Image = ((System.Drawing.Image)(resources.GetObject("RefreshBox.Image")));
			this.RefreshBox.Location = new System.Drawing.Point(224, 42);
			this.RefreshBox.Name = "RefreshBox";
			this.RefreshBox.Size = new System.Drawing.Size(22, 22);
			this.RefreshBox.TabIndex = 6;
			this.RefreshBox.UseVisualStyleBackColor = true;
			this.RefreshBox.Click += new System.EventHandler(this.RefreshBox_Click);
			// 
			// PreviewBox
			// 
			this.PreviewBox.Dock = System.Windows.Forms.DockStyle.Right;
			this.PreviewBox.Location = new System.Drawing.Point(269, 16);
			this.PreviewBox.Name = "PreviewBox";
			this.PreviewBox.Size = new System.Drawing.Size(216, 81);
			this.PreviewBox.TabIndex = 5;
			this.PreviewBox.Text = "label9";
			// 
			// PreviewLabelBox
			// 
			this.PreviewLabelBox.AutoSize = true;
			this.PreviewLabelBox.Location = new System.Drawing.Point(212, 20);
			this.PreviewLabelBox.Name = "PreviewLabelBox";
			this.PreviewLabelBox.Size = new System.Drawing.Size(51, 13);
			this.PreviewLabelBox.TabIndex = 4;
			this.PreviewLabelBox.Text = "Preview :";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(8, 46);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(54, 13);
			this.label7.TabIndex = 3;
			this.label7.Text = "String ID :";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(7, 20);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(70, 13);
			this.label6.TabIndex = 2;
			this.label6.Text = "String Table :";
			// 
			// StringTableBox
			// 
			this.StringTableBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.StringTableBox.FormattingEnabled = true;
			this.StringTableBox.Location = new System.Drawing.Point(85, 17);
			this.StringTableBox.Name = "StringTableBox";
			this.StringTableBox.Size = new System.Drawing.Size(121, 21);
			this.StringTableBox.Sorted = true;
			this.StringTableBox.TabIndex = 1;
			this.StringTableBox.SelectedIndexChanged += new System.EventHandler(this.StringTableBox_SelectedIndexChanged);
			// 
			// StringIDBox
			// 
			this.StringIDBox.Location = new System.Drawing.Point(85, 44);
			this.StringIDBox.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
			this.StringIDBox.Name = "StringIDBox";
			this.StringIDBox.Size = new System.Drawing.Size(120, 20);
			this.StringIDBox.TabIndex = 0;
			this.StringIDBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.StringIDBox.ThousandsSeparator = true;
			this.StringIDBox.ValueChanged += new System.EventHandler(this.StringIDBox_ValueChanged);
			// 
			// StringTableControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.GroupBox);
			this.Name = "StringTableControl";
			this.Size = new System.Drawing.Size(488, 100);
			this.Load += new System.EventHandler(this.StringTableControl_Load);
			this.GroupBox.ResumeLayout(false);
			this.GroupBox.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.StringIDBox)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox GroupBox;
		private System.Windows.Forms.Label PreviewBox;
		private System.Windows.Forms.Label PreviewLabelBox;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.ComboBox StringTableBox;
		private System.Windows.Forms.NumericUpDown StringIDBox;
		private System.Windows.Forms.Button RefreshBox;
		private System.Windows.Forms.ComboBox LanguageBox;
		private System.Windows.Forms.Label label1;
	}
}
