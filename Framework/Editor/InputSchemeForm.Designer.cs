namespace ArcEngine.Editor
{
	partial class InputSchemeForm
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.KeyBox = new System.Windows.Forms.ComboBox();
			this.NameBox = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.DeleteBox = new System.Windows.Forms.Button();
			this.ApplyBox = new System.Windows.Forms.Button();
			this.ListViewBox = new System.Windows.Forms.ListView();
			this.NameColumn = new System.Windows.Forms.ColumnHeader();
			this.KeyColumn = new System.Windows.Forms.ColumnHeader();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.KeyBox);
			this.groupBox1.Controls.Add(this.NameBox);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.DeleteBox);
			this.groupBox1.Controls.Add(this.ApplyBox);
			this.groupBox1.Location = new System.Drawing.Point(12, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(252, 108);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Edit :";
			// 
			// KeyBox
			// 
			this.KeyBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.KeyBox.FormattingEnabled = true;
			this.KeyBox.Location = new System.Drawing.Point(54, 48);
			this.KeyBox.Name = "KeyBox";
			this.KeyBox.Size = new System.Drawing.Size(108, 21);
			this.KeyBox.Sorted = true;
			this.KeyBox.TabIndex = 6;
			// 
			// NameBox
			// 
			this.NameBox.Location = new System.Drawing.Point(54, 17);
			this.NameBox.Name = "NameBox";
			this.NameBox.Size = new System.Drawing.Size(189, 20);
			this.NameBox.TabIndex = 5;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(10, 51);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(31, 13);
			this.label2.TabIndex = 4;
			this.label2.Text = "Key :";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(7, 20);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(41, 13);
			this.label1.TabIndex = 3;
			this.label1.Text = "Name :";
			// 
			// DeleteBox
			// 
			this.DeleteBox.Location = new System.Drawing.Point(168, 77);
			this.DeleteBox.Name = "DeleteBox";
			this.DeleteBox.Size = new System.Drawing.Size(75, 23);
			this.DeleteBox.TabIndex = 2;
			this.DeleteBox.Text = "Delete";
			this.DeleteBox.UseVisualStyleBackColor = true;
			this.DeleteBox.Click += new System.EventHandler(this.Delete_Click);
			// 
			// ApplyBox
			// 
			this.ApplyBox.Location = new System.Drawing.Point(168, 48);
			this.ApplyBox.Name = "ApplyBox";
			this.ApplyBox.Size = new System.Drawing.Size(75, 23);
			this.ApplyBox.TabIndex = 0;
			this.ApplyBox.Text = "Apply";
			this.ApplyBox.UseVisualStyleBackColor = true;
			this.ApplyBox.Click += new System.EventHandler(this.Apply_Click);
			// 
			// ListViewBox
			// 
			this.ListViewBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
							| System.Windows.Forms.AnchorStyles.Left)
							| System.Windows.Forms.AnchorStyles.Right)));
			this.ListViewBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.ListViewBox.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.NameColumn,
            this.KeyColumn});
			this.ListViewBox.FullRowSelect = true;
			this.ListViewBox.Location = new System.Drawing.Point(270, 12);
			this.ListViewBox.Name = "ListViewBox";
			this.ListViewBox.Size = new System.Drawing.Size(306, 259);
			this.ListViewBox.TabIndex = 1;
			this.ListViewBox.UseCompatibleStateImageBehavior = false;
			this.ListViewBox.View = System.Windows.Forms.View.Details;
			this.ListViewBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ListViewBox_MouseDoubleClick);
			// 
			// NameColumn
			// 
			this.NameColumn.Text = "Name";
			this.NameColumn.Width = 200;
			// 
			// KeyColumn
			// 
			this.KeyColumn.Text = "Key";
			// 
			// KeyboardSchemeForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(588, 283);
			this.Controls.Add(this.ListViewBox);
			this.Controls.Add(this.groupBox1);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "KeyboardSchemeForm";
			this.ShowInTaskbar = false;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.KeyboardSchemeForm_FormClosing);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button DeleteBox;
		private System.Windows.Forms.Button ApplyBox;
		private System.Windows.Forms.ComboBox KeyBox;
		private System.Windows.Forms.TextBox NameBox;
		private System.Windows.Forms.ListView ListViewBox;
		private System.Windows.Forms.ColumnHeader NameColumn;
		private System.Windows.Forms.ColumnHeader KeyColumn;

	}
}
