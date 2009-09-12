namespace ArcEngine.Editor
{
	partial class BinaryForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BinaryForm));
			this.ListViewBox = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.AddBinaryBox = new System.Windows.Forms.ToolStripButton();
			this.SaveBinaryBox = new System.Windows.Forms.ToolStripButton();
			this.EraseBox = new System.Windows.Forms.ToolStripButton();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// ListViewBox
			// 
			this.ListViewBox.AllowDrop = true;
			this.ListViewBox.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
			this.ListViewBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ListViewBox.FullRowSelect = true;
			this.ListViewBox.GridLines = true;
			this.ListViewBox.Location = new System.Drawing.Point(0, 25);
			this.ListViewBox.Name = "ListViewBox";
			this.ListViewBox.Size = new System.Drawing.Size(747, 333);
			this.ListViewBox.Sorting = System.Windows.Forms.SortOrder.Ascending;
			this.ListViewBox.TabIndex = 0;
			this.ListViewBox.UseCompatibleStateImageBehavior = false;
			this.ListViewBox.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Name";
			this.columnHeader1.Width = 250;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Size";
			this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.columnHeader2.Width = 100;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Type";
			this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.columnHeader3.Width = 100;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "Date";
			this.columnHeader4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.columnHeader4.Width = 100;
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddBinaryBox,
            this.SaveBinaryBox,
            this.EraseBox});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(747, 25);
			this.toolStrip1.TabIndex = 1;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// AddBinaryBox
			// 
			this.AddBinaryBox.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.AddBinaryBox.Image = ((System.Drawing.Image)(resources.GetObject("AddBinaryBox.Image")));
			this.AddBinaryBox.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.AddBinaryBox.Name = "AddBinaryBox";
			this.AddBinaryBox.Size = new System.Drawing.Size(23, 22);
			this.AddBinaryBox.Text = "Add...";
			// 
			// SaveBinaryBox
			// 
			this.SaveBinaryBox.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.SaveBinaryBox.Image = ((System.Drawing.Image)(resources.GetObject("SaveBinaryBox.Image")));
			this.SaveBinaryBox.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.SaveBinaryBox.Name = "SaveBinaryBox";
			this.SaveBinaryBox.Size = new System.Drawing.Size(23, 22);
			this.SaveBinaryBox.Text = "Save...";
			// 
			// EraseBox
			// 
			this.EraseBox.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.EraseBox.Image = ((System.Drawing.Image)(resources.GetObject("EraseBox.Image")));
			this.EraseBox.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.EraseBox.Name = "EraseBox";
			this.EraseBox.Size = new System.Drawing.Size(23, 22);
			this.EraseBox.Text = "Erase";
			// 
			// BinaryForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(747, 358);
			this.Controls.Add(this.ListViewBox);
			this.Controls.Add(this.toolStrip1);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "BinaryForm";
			this.Text = "Binaries";
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListView ListViewBox;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton AddBinaryBox;
		private System.Windows.Forms.ToolStripButton SaveBinaryBox;
		private System.Windows.Forms.ToolStripButton EraseBox;
	}
}