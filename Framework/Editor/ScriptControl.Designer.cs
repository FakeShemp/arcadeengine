namespace ArcEngine.Editor
{
	partial class ScriptControl
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScriptControl));
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.RefreshBox = new System.Windows.Forms.Button();
			this.label16 = new System.Windows.Forms.Label();
			this.InterfaceNameBox = new System.Windows.Forms.ComboBox();
			this.label17 = new System.Windows.Forms.Label();
			this.ScriptNameBox = new System.Windows.Forms.ComboBox();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.RefreshBox);
			this.groupBox2.Controls.Add(this.label16);
			this.groupBox2.Controls.Add(this.InterfaceNameBox);
			this.groupBox2.Controls.Add(this.label17);
			this.groupBox2.Controls.Add(this.ScriptNameBox);
			this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox2.Location = new System.Drawing.Point(0, 0);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(200, 70);
			this.groupBox2.TabIndex = 7;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Interface :";
			// 
			// RefreshBox
			// 
			this.RefreshBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.RefreshBox.AutoSize = true;
			this.RefreshBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.RefreshBox.Image = ((System.Drawing.Image)(resources.GetObject("RefreshBox.Image")));
			this.RefreshBox.Location = new System.Drawing.Point(172, 13);
			this.RefreshBox.Name = "RefreshBox";
			this.RefreshBox.Size = new System.Drawing.Size(22, 22);
			this.RefreshBox.TabIndex = 3;
			this.RefreshBox.UseVisualStyleBackColor = true;
			this.RefreshBox.Click += new System.EventHandler(this.RefreshBox_Click);
			// 
			// label16
			// 
			this.label16.AutoSize = true;
			this.label16.Location = new System.Drawing.Point(6, 16);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(69, 13);
			this.label16.TabIndex = 0;
			this.label16.Text = "Script name :";
			// 
			// InterfaceNameBox
			// 
			this.InterfaceNameBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.InterfaceNameBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.InterfaceNameBox.FormattingEnabled = true;
			this.InterfaceNameBox.Location = new System.Drawing.Point(81, 40);
			this.InterfaceNameBox.Name = "InterfaceNameBox";
			this.InterfaceNameBox.Size = new System.Drawing.Size(85, 21);
			this.InterfaceNameBox.Sorted = true;
			this.InterfaceNameBox.TabIndex = 2;
			this.InterfaceNameBox.SelectedIndexChanged += new System.EventHandler(this.InterfaceNameBox_SelectedIndexChanged);
			// 
			// label17
			// 
			this.label17.AutoSize = true;
			this.label17.Location = new System.Drawing.Point(20, 43);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(55, 13);
			this.label17.TabIndex = 0;
			this.label17.Text = "Interface :";
			// 
			// ScriptNameBox
			// 
			this.ScriptNameBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.ScriptNameBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ScriptNameBox.FormattingEnabled = true;
			this.ScriptNameBox.Location = new System.Drawing.Point(81, 13);
			this.ScriptNameBox.Name = "ScriptNameBox";
			this.ScriptNameBox.Size = new System.Drawing.Size(85, 21);
			this.ScriptNameBox.Sorted = true;
			this.ScriptNameBox.TabIndex = 1;
			this.ScriptNameBox.SelectedIndexChanged += new System.EventHandler(this.ScriptNameBox_SelectedIndexChanged);
			// 
			// ScriptControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.groupBox2);
			this.MinimumSize = new System.Drawing.Size(200, 70);
			this.Name = "ScriptControl";
			this.Size = new System.Drawing.Size(200, 70);
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.ComboBox InterfaceNameBox;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.ComboBox ScriptNameBox;
		private System.Windows.Forms.Button RefreshBox;
	}
}
