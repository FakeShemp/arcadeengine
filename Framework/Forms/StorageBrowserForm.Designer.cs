namespace ArcEngine.Forms
{
	partial class StorageBrowserForm
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
			this.CancelBox = new System.Windows.Forms.Button();
			this.SelectBox = new System.Windows.Forms.Button();
			this.FileNameBox = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.FilterBox = new System.Windows.Forms.ComboBox();
			this.FilesBox = new System.Windows.Forms.ListView();
			this.NameHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.ModifyHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.TypeHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.SizeHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.DirectoriesBox = new System.Windows.Forms.TreeView();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.StorageBox = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.SuspendLayout();
			// 
			// CancelBox
			// 
			this.CancelBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.CancelBox.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CancelBox.Location = new System.Drawing.Point(793, 467);
			this.CancelBox.Name = "CancelBox";
			this.CancelBox.Size = new System.Drawing.Size(100, 23);
			this.CancelBox.TabIndex = 0;
			this.CancelBox.Text = "Cancel";
			this.CancelBox.UseVisualStyleBackColor = true;
			// 
			// SelectBox
			// 
			this.SelectBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.SelectBox.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.SelectBox.Location = new System.Drawing.Point(687, 467);
			this.SelectBox.Name = "SelectBox";
			this.SelectBox.Size = new System.Drawing.Size(100, 23);
			this.SelectBox.TabIndex = 1;
			this.SelectBox.Text = "Select";
			this.SelectBox.UseVisualStyleBackColor = true;
			// 
			// FileNameBox
			// 
			this.FileNameBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.FileNameBox.Location = new System.Drawing.Point(129, 441);
			this.FileNameBox.Name = "FileNameBox";
			this.FileNameBox.Size = new System.Drawing.Size(552, 20);
			this.FileNameBox.TabIndex = 3;
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(65, 444);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(58, 13);
			this.label1.TabIndex = 4;
			this.label1.Text = "File name :";
			// 
			// FilterBox
			// 
			this.FilterBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.FilterBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.FilterBox.FormattingEnabled = true;
			this.FilterBox.Location = new System.Drawing.Point(687, 440);
			this.FilterBox.Name = "FilterBox";
			this.FilterBox.Size = new System.Drawing.Size(206, 21);
			this.FilterBox.TabIndex = 5;
			// 
			// FilesBox
			// 
			this.FilesBox.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.NameHeader,
            this.ModifyHeader,
            this.TypeHeader,
            this.SizeHeader});
			this.FilesBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.FilesBox.Location = new System.Drawing.Point(0, 0);
			this.FilesBox.Name = "FilesBox";
			this.FilesBox.Size = new System.Drawing.Size(584, 423);
			this.FilesBox.TabIndex = 6;
			this.FilesBox.UseCompatibleStateImageBehavior = false;
			this.FilesBox.View = System.Windows.Forms.View.Details;
			this.FilesBox.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.FilesBox_ItemSelectionChanged);
			this.FilesBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.FilesBox_MouseDoubleClick);
			// 
			// NameHeader
			// 
			this.NameHeader.Text = "Name";
			this.NameHeader.Width = 200;
			// 
			// ModifyHeader
			// 
			this.ModifyHeader.Text = "Modify";
			this.ModifyHeader.Width = 130;
			// 
			// TypeHeader
			// 
			this.TypeHeader.Text = "Type";
			this.TypeHeader.Width = 130;
			// 
			// SizeHeader
			// 
			this.SizeHeader.Text = "Size";
			this.SizeHeader.Width = 120;
			// 
			// DirectoriesBox
			// 
			this.DirectoriesBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.DirectoriesBox.Location = new System.Drawing.Point(0, 0);
			this.DirectoriesBox.Name = "DirectoriesBox";
			this.DirectoriesBox.Size = new System.Drawing.Size(293, 423);
			this.DirectoriesBox.TabIndex = 7;
			// 
			// splitContainer1
			// 
			this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.splitContainer1.Location = new System.Drawing.Point(12, 12);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.DirectoriesBox);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.FilesBox);
			this.splitContainer1.Size = new System.Drawing.Size(881, 423);
			this.splitContainer1.SplitterDistance = 293;
			this.splitContainer1.TabIndex = 8;
			// 
			// StorageBox
			// 
			this.StorageBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.StorageBox.FormattingEnabled = true;
			this.StorageBox.Location = new System.Drawing.Point(129, 467);
			this.StorageBox.Name = "StorageBox";
			this.StorageBox.Size = new System.Drawing.Size(192, 21);
			this.StorageBox.Sorted = true;
			this.StorageBox.TabIndex = 9;
			this.StorageBox.SelectedIndexChanged += new System.EventHandler(this.StorageBox_SelectedIndexChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(73, 472);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(50, 13);
			this.label2.TabIndex = 10;
			this.label2.Text = "Storage :";
			// 
			// StorageBrowserForm
			// 
			this.AcceptButton = this.SelectBox;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.CancelBox;
			this.ClientSize = new System.Drawing.Size(905, 502);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.StorageBox);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.FilterBox);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.FileNameBox);
			this.Controls.Add(this.SelectBox);
			this.Controls.Add(this.CancelBox);
			this.HelpButton = true;
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(600, 400);
			this.Name = "StorageBrowserForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Storage browser";
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button CancelBox;
		private System.Windows.Forms.Button SelectBox;
		private System.Windows.Forms.TextBox FileNameBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox FilterBox;
		private System.Windows.Forms.ListView FilesBox;
		private System.Windows.Forms.ColumnHeader NameHeader;
		private System.Windows.Forms.ColumnHeader ModifyHeader;
		private System.Windows.Forms.ColumnHeader TypeHeader;
		private System.Windows.Forms.ColumnHeader SizeHeader;
		private System.Windows.Forms.TreeView DirectoriesBox;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.ComboBox StorageBox;
		private System.Windows.Forms.Label label2;
	}
}