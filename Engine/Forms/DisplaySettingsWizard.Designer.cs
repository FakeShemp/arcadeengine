namespace ArcEngine.Forms
{
	partial class DisplaySettingsWizard
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

		#region Code généré par le Concepteur Windows Form

		/// <summary>
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.DisplayBox = new System.Windows.Forms.ComboBox();
			this.ResolutionBox = new System.Windows.Forms.ComboBox();
			this.GfxBox = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.CancelButtonBox = new System.Windows.Forms.Button();
			this.OkButtonBox = new System.Windows.Forms.Button();
			this.FullScreenBox = new System.Windows.Forms.RadioButton();
			this.WindowedBox = new System.Windows.Forms.RadioButton();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.DisplayBox);
			this.groupBox1.Controls.Add(this.ResolutionBox);
			this.groupBox1.Controls.Add(this.GfxBox);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Location = new System.Drawing.Point(12, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(238, 115);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Graphic adapter and resolution :";
			// 
			// DisplayBox
			// 
			this.DisplayBox.FormattingEnabled = true;
			this.DisplayBox.Location = new System.Drawing.Point(101, 80);
			this.DisplayBox.Name = "DisplayBox";
			this.DisplayBox.Size = new System.Drawing.Size(121, 21);
			this.DisplayBox.TabIndex = 5;
			// 
			// ResolutionBox
			// 
			this.ResolutionBox.FormattingEnabled = true;
			this.ResolutionBox.Location = new System.Drawing.Point(101, 53);
			this.ResolutionBox.Name = "ResolutionBox";
			this.ResolutionBox.Size = new System.Drawing.Size(121, 21);
			this.ResolutionBox.TabIndex = 4;
			// 
			// GfxBox
			// 
			this.GfxBox.FormattingEnabled = true;
			this.GfxBox.Location = new System.Drawing.Point(101, 26);
			this.GfxBox.Name = "GfxBox";
			this.GfxBox.Size = new System.Drawing.Size(121, 21);
			this.GfxBox.TabIndex = 3;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(48, 83);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(47, 13);
			this.label3.TabIndex = 2;
			this.label3.Text = "Display :";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(32, 56);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(63, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "Resolution :";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 29);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(89, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Graphic adapter :";
			// 
			// groupBox2
			// 
			this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox2.Controls.Add(this.WindowedBox);
			this.groupBox2.Controls.Add(this.FullScreenBox);
			this.groupBox2.Location = new System.Drawing.Point(12, 133);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(238, 51);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Display mode :";
			// 
			// CancelButtonBox
			// 
			this.CancelButtonBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.CancelButtonBox.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CancelButtonBox.Location = new System.Drawing.Point(175, 196);
			this.CancelButtonBox.Name = "CancelButtonBox";
			this.CancelButtonBox.Size = new System.Drawing.Size(75, 23);
			this.CancelButtonBox.TabIndex = 2;
			this.CancelButtonBox.Text = "Cancel";
			this.CancelButtonBox.UseVisualStyleBackColor = true;
			// 
			// OkButtonBox
			// 
			this.OkButtonBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.OkButtonBox.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.OkButtonBox.Location = new System.Drawing.Point(94, 196);
			this.OkButtonBox.Name = "OkButtonBox";
			this.OkButtonBox.Size = new System.Drawing.Size(75, 23);
			this.OkButtonBox.TabIndex = 3;
			this.OkButtonBox.Text = "Ok";
			this.OkButtonBox.UseVisualStyleBackColor = true;
			// 
			// FullScreenBox
			// 
			this.FullScreenBox.AutoSize = true;
			this.FullScreenBox.Location = new System.Drawing.Point(19, 19);
			this.FullScreenBox.Name = "FullScreenBox";
			this.FullScreenBox.Size = new System.Drawing.Size(76, 17);
			this.FullScreenBox.TabIndex = 2;
			this.FullScreenBox.TabStop = true;
			this.FullScreenBox.Text = "Full screen";
			this.FullScreenBox.UseVisualStyleBackColor = true;
			// 
			// WindowedBox
			// 
			this.WindowedBox.AutoSize = true;
			this.WindowedBox.Location = new System.Drawing.Point(146, 19);
			this.WindowedBox.Name = "WindowedBox";
			this.WindowedBox.Size = new System.Drawing.Size(76, 17);
			this.WindowedBox.TabIndex = 3;
			this.WindowedBox.TabStop = true;
			this.WindowedBox.Text = "Windowed";
			this.WindowedBox.UseVisualStyleBackColor = true;
			// 
			// DisplaySettingsWizard
			// 
			this.AcceptButton = this.OkButtonBox;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.CancelButtonBox;
			this.ClientSize = new System.Drawing.Size(262, 229);
			this.Controls.Add(this.OkButtonBox);
			this.Controls.Add(this.CancelButtonBox);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "DisplaySettingsWizard";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Display settings wizard";
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button CancelButtonBox;
		private System.Windows.Forms.Button OkButtonBox;
		private System.Windows.Forms.ComboBox GfxBox;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox DisplayBox;
		private System.Windows.Forms.ComboBox ResolutionBox;
		private System.Windows.Forms.RadioButton WindowedBox;
		private System.Windows.Forms.RadioButton FullScreenBox;
	}
}