namespace ArcEngine.Editor.Forms
{
	partial class AboutForm
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
			this.ButtonOk = new System.Windows.Forms.Button();
			this.LabelLink = new System.Windows.Forms.LinkLabel();
			this.PluginList = new System.Windows.Forms.ListBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(64, 30);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(189, 25);
			this.label1.TabIndex = 0;
			this.label1.Text = "ArcEngine Editor";
			// 
			// ButtonOk
			// 
			this.ButtonOk.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.ButtonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.ButtonOk.Location = new System.Drawing.Point(139, 280);
			this.ButtonOk.Name = "ButtonOk";
			this.ButtonOk.Size = new System.Drawing.Size(75, 23);
			this.ButtonOk.TabIndex = 1;
			this.ButtonOk.Text = "Ok";
			this.ButtonOk.UseVisualStyleBackColor = true;
			// 
			// LabelLink
			// 
			this.LabelLink.AutoSize = true;
			this.LabelLink.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.LabelLink.Location = new System.Drawing.Point(97, 250);
			this.LabelLink.Name = "LabelLink";
			this.LabelLink.Size = new System.Drawing.Size(145, 20);
			this.LabelLink.TabIndex = 2;
			this.LabelLink.TabStop = true;
			this.LabelLink.Text = "www.mimicprod.net";
			// 
			// PluginList
			// 
			this.PluginList.FormattingEnabled = true;
			this.PluginList.Location = new System.Drawing.Point(12, 87);
			this.PluginList.Name = "PluginList";
			this.PluginList.Size = new System.Drawing.Size(324, 160);
			this.PluginList.Sorted = true;
			this.PluginList.TabIndex = 3;
			// 
			// AboutForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(348, 315);
			this.Controls.Add(this.PluginList);
			this.Controls.Add(this.LabelLink);
			this.Controls.Add(this.ButtonOk);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "AboutForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "About...";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button ButtonOk;
		private System.Windows.Forms.LinkLabel LabelLink;
		private System.Windows.Forms.ListBox PluginList;
	}
}