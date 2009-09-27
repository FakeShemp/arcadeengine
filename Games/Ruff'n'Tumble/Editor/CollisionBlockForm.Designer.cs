namespace ArcEngine.Games.RuffnTumble.Editor
{
	partial class CollisionBlockForm
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
			this.CollisionSideBox = new System.Windows.Forms.CheckedListBox();
			this.ButtonCancel = new System.Windows.Forms.Button();
			this.OkButton = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.CollisionSideBox);
			this.groupBox1.Location = new System.Drawing.Point(12, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(92, 85);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Collision side :";
			// 
			// CollisionSideBox
			// 
			this.CollisionSideBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.CollisionSideBox.FormattingEnabled = true;
			this.CollisionSideBox.Items.AddRange(new object[] {
            "Top",
            "Left",
            "Right",
            "Bottom"});
			this.CollisionSideBox.Location = new System.Drawing.Point(3, 16);
			this.CollisionSideBox.Name = "CollisionSideBox";
			this.CollisionSideBox.Size = new System.Drawing.Size(86, 64);
			this.CollisionSideBox.TabIndex = 0;
			// 
			// ButtonCancel
			// 
			this.ButtonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.ButtonCancel.Location = new System.Drawing.Point(235, 231);
			this.ButtonCancel.Name = "ButtonCancel";
			this.ButtonCancel.Size = new System.Drawing.Size(75, 23);
			this.ButtonCancel.TabIndex = 1;
			this.ButtonCancel.Text = "Cancel";
			this.ButtonCancel.UseVisualStyleBackColor = true;
			// 
			// OkButton
			// 
			this.OkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.OkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.OkButton.Location = new System.Drawing.Point(154, 231);
			this.OkButton.Name = "OkButton";
			this.OkButton.Size = new System.Drawing.Size(75, 23);
			this.OkButton.TabIndex = 2;
			this.OkButton.Text = "Ok";
			this.OkButton.UseVisualStyleBackColor = true;
			// 
			// CollisionBlockForm
			// 
			this.AcceptButton = this.OkButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(322, 266);
			this.Controls.Add(this.OkButton);
			this.Controls.Add(this.ButtonCancel);
			this.Controls.Add(this.groupBox1);
			this.Name = "CollisionBlockForm";
			this.Text = "Collision block properties :";
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckedListBox CollisionSideBox;
		private System.Windows.Forms.Button ButtonCancel;
		private System.Windows.Forms.Button OkButton;
	}
}