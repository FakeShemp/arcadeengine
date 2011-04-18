namespace DungeonEye.Forms.Script
{
	partial class ActionChooserControl
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
			this.label1 = new System.Windows.Forms.Label();
			this.ActionChooserBox = new System.Windows.Forms.ComboBox();
			this.ActionPropertiesBox = new System.Windows.Forms.Panel();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.ActionChooserBox);
			this.groupBox1.Controls.Add(this.ActionPropertiesBox);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox1.Location = new System.Drawing.Point(0, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(526, 313);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Actions";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 22);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(81, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Type of action :";
			// 
			// ActionChooserBox
			// 
			this.ActionChooserBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ActionChooserBox.FormattingEnabled = true;
			this.ActionChooserBox.Items.AddRange(new object[] {
            "Toggles",
            "Activates",
            "Deactivates",
            "Activates / Deactivates",
            "Deactivates / Activates",
            "Exchanges",
            "Set To",
            "Play Sound",
            "Stop Sounds",
			"Change Picture",
			"Disable Choice",
			"Enable Choice",
			"End Choice",
			"End Dialog",
			"Give Experience",
			"Heals",
			"Join Character",
			"Teleport"});
			this.ActionChooserBox.Location = new System.Drawing.Point(93, 19);
			this.ActionChooserBox.Name = "ActionChooserBox";
			this.ActionChooserBox.Size = new System.Drawing.Size(157, 21);
			this.ActionChooserBox.TabIndex = 0;
			this.ActionChooserBox.SelectedIndexChanged += new System.EventHandler(this.ActionChooserBox_SelectedIndexChanged);
			// 
			// ActionPropertiesBox
			// 
			this.ActionPropertiesBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.ActionPropertiesBox.Location = new System.Drawing.Point(6, 46);
			this.ActionPropertiesBox.Name = "ActionPropertiesBox";
			this.ActionPropertiesBox.Size = new System.Drawing.Size(514, 261);
			this.ActionPropertiesBox.TabIndex = 1;
			// 
			// ScriptActionsControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.groupBox1);
			this.Name = "ScriptActionsControl";
			this.Size = new System.Drawing.Size(526, 313);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Panel ActionPropertiesBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox ActionChooserBox;
	}
}
