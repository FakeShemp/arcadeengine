namespace ArcEngine.Editor
{
	partial class LogForm
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogForm));
			this.LogBox = new System.Windows.Forms.RichTextBox();
			this.LogMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.Clear = new System.Windows.Forms.ToolStripMenuItem();
			this.CopyToClipboard = new System.Windows.Forms.ToolStripMenuItem();
			this.LogMenuStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// LogBox
			// 
			this.LogBox.ContextMenuStrip = this.LogMenuStrip;
			this.LogBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LogBox.Location = new System.Drawing.Point(0, 0);
			this.LogBox.Name = "LogBox";
			this.LogBox.Size = new System.Drawing.Size(292, 273);
			this.LogBox.TabIndex = 0;
			this.LogBox.Text = "";
			// 
			// LogMenuStrip
			// 
			this.LogMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Clear,
            this.CopyToClipboard});
			this.LogMenuStrip.Name = "LogMenuStrip";
			this.LogMenuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.LogMenuStrip.Size = new System.Drawing.Size(170, 70);
			this.LogMenuStrip.Text = "Clear log";
			// 
			// Clear
			// 
			this.Clear.Image = ((System.Drawing.Image)(resources.GetObject("Clear.Image")));
			this.Clear.Name = "Clear";
			this.Clear.Size = new System.Drawing.Size(169, 22);
			this.Clear.Text = "Clear Log";
			this.Clear.Click += new System.EventHandler(this.Clear_Click);
			// 
			// CopyToClipboard
			// 
			this.CopyToClipboard.Image = ((System.Drawing.Image)(resources.GetObject("CopyToClipboard.Image")));
			this.CopyToClipboard.Name = "CopyToClipboard";
			this.CopyToClipboard.Size = new System.Drawing.Size(169, 22);
			this.CopyToClipboard.Text = "Copy to clipboard";
			this.CopyToClipboard.Click += new System.EventHandler(this.CopyToClipboard_Click);
			// 
			// LogForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(292, 273);
			this.ContextMenuStrip = this.LogMenuStrip;
			this.Controls.Add(this.LogBox);
			this.HideOnClose = true;
			this.Name = "LogForm";
			this.TabText = "LogForm";
			this.Text = "LogForm";
			this.LogMenuStrip.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ContextMenuStrip LogMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem Clear;
		private System.Windows.Forms.ToolStripMenuItem CopyToClipboard;
		private System.Windows.Forms.RichTextBox LogBox;



	}
}