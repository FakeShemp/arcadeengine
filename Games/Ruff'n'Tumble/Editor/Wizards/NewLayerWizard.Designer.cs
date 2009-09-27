namespace ArcEngine.Games.RuffnTumble.Editor.Wizards
{
	partial class NewLayerWizard
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
			this.label1 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.LayerNameBox = new System.Windows.Forms.TextBox();
			this.TextureBox = new System.Windows.Forms.ComboBox();
			this.CreateButton = new System.Windows.Forms.Button();
			this.ButtonCancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(22, 18);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(68, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Layer name :";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(12, 47);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(81, 13);
			this.label4.TabIndex = 3;
			this.label4.Text = "Texture to use :";
			// 
			// LayerNameBox
			// 
			this.LayerNameBox.Location = new System.Drawing.Point(99, 15);
			this.LayerNameBox.Name = "LayerNameBox";
			this.LayerNameBox.Size = new System.Drawing.Size(233, 20);
			this.LayerNameBox.TabIndex = 1;
			// 
			// TextureBox
			// 
			this.TextureBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.TextureBox.FormattingEnabled = true;
			this.TextureBox.Location = new System.Drawing.Point(96, 44);
			this.TextureBox.Name = "TextureBox";
			this.TextureBox.Size = new System.Drawing.Size(236, 21);
			this.TextureBox.Sorted = true;
			this.TextureBox.TabIndex = 4;
			// 
			// CreateButton
			// 
			this.CreateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.CreateButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.CreateButton.Location = new System.Drawing.Point(176, 76);
			this.CreateButton.Name = "CreateButton";
			this.CreateButton.Size = new System.Drawing.Size(75, 23);
			this.CreateButton.TabIndex = 7;
			this.CreateButton.Text = "Create";
			this.CreateButton.UseVisualStyleBackColor = true;
			// 
			// ButtonCancel
			// 
			this.ButtonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.ButtonCancel.Location = new System.Drawing.Point(257, 76);
			this.ButtonCancel.Name = "ButtonCancel";
			this.ButtonCancel.Size = new System.Drawing.Size(75, 23);
			this.ButtonCancel.TabIndex = 8;
			this.ButtonCancel.Text = "Cancel";
			this.ButtonCancel.UseVisualStyleBackColor = true;
			// 
			// NewLayerWizard
			// 
			this.AcceptButton = this.CreateButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.ButtonCancel;
			this.ClientSize = new System.Drawing.Size(344, 111);
			this.Controls.Add(this.ButtonCancel);
			this.Controls.Add(this.CreateButton);
			this.Controls.Add(this.TextureBox);
			this.Controls.Add(this.LayerNameBox);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.HelpButton = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "NewLayerWizard";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "New layer wizard";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnFormClosing);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox LayerNameBox;
		private System.Windows.Forms.ComboBox TextureBox;
		private System.Windows.Forms.Button CreateButton;
		private System.Windows.Forms.Button ButtonCancel;
	}
}