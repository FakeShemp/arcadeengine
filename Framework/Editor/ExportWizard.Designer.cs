namespace ArcEngine.Editor
{
	partial class ExportWizard
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.TreeviewBox = new System.Windows.Forms.TreeView();
			this.CancelBox = new System.Windows.Forms.Button();
			this.ExportBox = new System.Windows.Forms.Button();
			this.SaveFileBox = new System.Windows.Forms.SaveFileDialog();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.ToFolderBox = new System.Windows.Forms.RadioButton();
			this.ToBankBox = new System.Windows.Forms.RadioButton();
			this.FolderBox = new System.Windows.Forms.FolderBrowserDialog();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// TreeviewBox
			// 
			this.TreeviewBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.TreeviewBox.CheckBoxes = true;
			this.TreeviewBox.Location = new System.Drawing.Point(12, 12);
			this.TreeviewBox.Name = "TreeviewBox";
			this.TreeviewBox.Size = new System.Drawing.Size(260, 346);
			this.TreeviewBox.TabIndex = 0;
			this.TreeviewBox.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.TreeviewBox_AfterCheck);
			// 
			// CancelBox
			// 
			this.CancelBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.CancelBox.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CancelBox.Location = new System.Drawing.Point(197, 427);
			this.CancelBox.Name = "CancelBox";
			this.CancelBox.Size = new System.Drawing.Size(75, 23);
			this.CancelBox.TabIndex = 1;
			this.CancelBox.Text = "Cancel";
			this.CancelBox.UseVisualStyleBackColor = true;
			// 
			// ExportBox
			// 
			this.ExportBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.ExportBox.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.ExportBox.Location = new System.Drawing.Point(116, 427);
			this.ExportBox.Name = "ExportBox";
			this.ExportBox.Size = new System.Drawing.Size(75, 23);
			this.ExportBox.TabIndex = 2;
			this.ExportBox.Text = "Export";
			this.ExportBox.UseVisualStyleBackColor = true;
			this.ExportBox.Click += new System.EventHandler(this.ExportBox_Click);
			// 
			// SaveFileBox
			// 
			this.SaveFileBox.DefaultExt = "bnk";
			this.SaveFileBox.Filter = "Bank files|*.bnk|All files|*.*";
			this.SaveFileBox.RestoreDirectory = true;
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.ToBankBox);
			this.groupBox1.Controls.Add(this.ToFolderBox);
			this.groupBox1.Location = new System.Drawing.Point(12, 364);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(260, 57);
			this.groupBox1.TabIndex = 3;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Export mode :";
			// 
			// ToFolderBox
			// 
			this.ToFolderBox.AutoSize = true;
			this.ToFolderBox.Checked = true;
			this.ToFolderBox.Location = new System.Drawing.Point(149, 19);
			this.ToFolderBox.Name = "ToFolderBox";
			this.ToFolderBox.Size = new System.Drawing.Size(105, 17);
			this.ToFolderBox.TabIndex = 0;
			this.ToFolderBox.TabStop = true;
			this.ToFolderBox.Text = "Export to a folder";
			this.ToFolderBox.UseVisualStyleBackColor = true;
			// 
			// ToBankBox
			// 
			this.ToBankBox.AutoSize = true;
			this.ToBankBox.Enabled = false;
			this.ToBankBox.Location = new System.Drawing.Point(6, 19);
			this.ToBankBox.Name = "ToBankBox";
			this.ToBankBox.Size = new System.Drawing.Size(103, 17);
			this.ToBankBox.TabIndex = 0;
			this.ToBankBox.Text = "Export to a bank";
			this.ToBankBox.UseVisualStyleBackColor = true;
			// 
			// ExportWizard
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.CancelBox;
			this.ClientSize = new System.Drawing.Size(284, 462);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.ExportBox);
			this.Controls.Add(this.CancelBox);
			this.Controls.Add(this.TreeviewBox);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(300, 500);
			this.Name = "ExportWizard";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Export wizard";
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TreeView TreeviewBox;
		private System.Windows.Forms.Button CancelBox;
		private System.Windows.Forms.Button ExportBox;
		private System.Windows.Forms.SaveFileDialog SaveFileBox;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.RadioButton ToBankBox;
		private System.Windows.Forms.RadioButton ToFolderBox;
		private System.Windows.Forms.FolderBrowserDialog FolderBox;
	}
}