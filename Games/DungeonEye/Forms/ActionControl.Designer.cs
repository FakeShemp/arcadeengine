namespace DungeonEye.Forms
{
	partial class ActionControl
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
			this.EditBox = new System.Windows.Forms.Button();
			this.ActionsBox = new System.Windows.Forms.ListBox();
			this.MoveDownActionBox = new System.Windows.Forms.Button();
			this.MoveUpActionBox = new System.Windows.Forms.Button();
			this.RemoveActionBox = new System.Windows.Forms.Button();
			this.AddActionBox = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.EditBox);
			this.groupBox1.Controls.Add(this.ActionsBox);
			this.groupBox1.Controls.Add(this.MoveDownActionBox);
			this.groupBox1.Controls.Add(this.MoveUpActionBox);
			this.groupBox1.Controls.Add(this.RemoveActionBox);
			this.groupBox1.Controls.Add(this.AddActionBox);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox1.Location = new System.Drawing.Point(0, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(350, 150);
			this.groupBox1.TabIndex = 2;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Actions :";
			// 
			// EditBox
			// 
			this.EditBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.EditBox.Location = new System.Drawing.Point(6, 121);
			this.EditBox.Name = "EditBox";
			this.EditBox.Size = new System.Drawing.Size(75, 23);
			this.EditBox.TabIndex = 2;
			this.EditBox.Text = "Edit";
			this.EditBox.UseVisualStyleBackColor = true;
			this.EditBox.Click += new System.EventHandler(this.EditBox_Click);
			// 
			// ActionsBox
			// 
			this.ActionsBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.ActionsBox.FormattingEnabled = true;
			this.ActionsBox.Location = new System.Drawing.Point(6, 19);
			this.ActionsBox.Name = "ActionsBox";
			this.ActionsBox.Size = new System.Drawing.Size(257, 95);
			this.ActionsBox.TabIndex = 1;
			this.ActionsBox.DoubleClick += new System.EventHandler(this.ActionsBox_DoubleClick);
			// 
			// MoveDownActionBox
			// 
			this.MoveDownActionBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.MoveDownActionBox.Location = new System.Drawing.Point(269, 121);
			this.MoveDownActionBox.Name = "MoveDownActionBox";
			this.MoveDownActionBox.Size = new System.Drawing.Size(75, 23);
			this.MoveDownActionBox.TabIndex = 0;
			this.MoveDownActionBox.Text = "Down";
			this.MoveDownActionBox.UseVisualStyleBackColor = true;
			this.MoveDownActionBox.Click += new System.EventHandler(this.MoveDownActionBox_Click);
			// 
			// MoveUpActionBox
			// 
			this.MoveUpActionBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.MoveUpActionBox.Location = new System.Drawing.Point(269, 92);
			this.MoveUpActionBox.Name = "MoveUpActionBox";
			this.MoveUpActionBox.Size = new System.Drawing.Size(75, 23);
			this.MoveUpActionBox.TabIndex = 0;
			this.MoveUpActionBox.Text = "Up";
			this.MoveUpActionBox.UseVisualStyleBackColor = true;
			this.MoveUpActionBox.Click += new System.EventHandler(this.MoveUpActionBox_Click);
			// 
			// RemoveActionBox
			// 
			this.RemoveActionBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.RemoveActionBox.Location = new System.Drawing.Point(269, 48);
			this.RemoveActionBox.Name = "RemoveActionBox";
			this.RemoveActionBox.Size = new System.Drawing.Size(75, 23);
			this.RemoveActionBox.TabIndex = 0;
			this.RemoveActionBox.Text = "Remove";
			this.RemoveActionBox.UseVisualStyleBackColor = true;
			this.RemoveActionBox.Click += new System.EventHandler(this.RemoveActionBox_Click);
			// 
			// AddActionBox
			// 
			this.AddActionBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.AddActionBox.Location = new System.Drawing.Point(269, 19);
			this.AddActionBox.Name = "AddActionBox";
			this.AddActionBox.Size = new System.Drawing.Size(75, 23);
			this.AddActionBox.TabIndex = 0;
			this.AddActionBox.Text = "Add";
			this.AddActionBox.UseVisualStyleBackColor = true;
			this.AddActionBox.Click += new System.EventHandler(this.AddActionBox_Click);
			// 
			// ActionControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.groupBox1);
			this.MinimumSize = new System.Drawing.Size(350, 150);
			this.Name = "ActionControl";
			this.Size = new System.Drawing.Size(350, 150);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ListBox ActionsBox;
		private System.Windows.Forms.Button MoveDownActionBox;
		private System.Windows.Forms.Button MoveUpActionBox;
		private System.Windows.Forms.Button RemoveActionBox;
		private System.Windows.Forms.Button AddActionBox;
		private System.Windows.Forms.Button EditBox;
	}
}
