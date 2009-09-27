namespace ArcEngine.Games.RuffnTumble.Editor
{
	partial class ModelForm
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
			this.PropertyBox = new System.Windows.Forms.PropertyGrid();
			this.SuspendLayout();
			// 
			// PropertyBox
			// 
			this.PropertyBox.Dock = System.Windows.Forms.DockStyle.Left;
			this.PropertyBox.Location = new System.Drawing.Point(0, 0);
			this.PropertyBox.Name = "PropertyBox";
			this.PropertyBox.Size = new System.Drawing.Size(251, 445);
			this.PropertyBox.TabIndex = 0;
			// 
			// ModelForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.PropertyBox);
			this.Name = "ModelForm";
			this.Size = new System.Drawing.Size(526, 445);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PropertyGrid PropertyBox;

	}
}