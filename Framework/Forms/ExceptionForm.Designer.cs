namespace ArcEngine.Forms
{
	partial class ExceptionForm
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
			this.OkBox = new System.Windows.Forms.Button();
			this.SendReportBox = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.ErrorBox = new System.Windows.Forms.Label();
			this.TraceBox = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// OkBox
			// 
			this.OkBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.OkBox.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.OkBox.Location = new System.Drawing.Point(868, 425);
			this.OkBox.Name = "OkBox";
			this.OkBox.Size = new System.Drawing.Size(75, 23);
			this.OkBox.TabIndex = 0;
			this.OkBox.Text = "OK";
			this.OkBox.UseVisualStyleBackColor = true;
			// 
			// SendReportBox
			// 
			this.SendReportBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.SendReportBox.Location = new System.Drawing.Point(742, 425);
			this.SendReportBox.Name = "SendReportBox";
			this.SendReportBox.Size = new System.Drawing.Size(120, 23);
			this.SendReportBox.TabIndex = 1;
			this.SendReportBox.Text = "Send error report...";
			this.SendReportBox.UseVisualStyleBackColor = true;
			this.SendReportBox.Click += new System.EventHandler(this.SendReportBox_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(129, 13);
			this.label1.TabIndex = 3;
			this.label1.Text = "An internal error occured :";
			// 
			// ErrorBox
			// 
			this.ErrorBox.Location = new System.Drawing.Point(12, 35);
			this.ErrorBox.Name = "ErrorBox";
			this.ErrorBox.Size = new System.Drawing.Size(655, 113);
			this.ErrorBox.TabIndex = 4;
			this.ErrorBox.Text = "label2";
			// 
			// TraceBox
			// 
			this.TraceBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.TraceBox.Location = new System.Drawing.Point(12, 151);
			this.TraceBox.Multiline = true;
			this.TraceBox.Name = "TraceBox";
			this.TraceBox.ReadOnly = true;
			this.TraceBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.TraceBox.Size = new System.Drawing.Size(931, 268);
			this.TraceBox.TabIndex = 5;
			this.TraceBox.WordWrap = false;
			// 
			// ExceptionForm
			// 
			this.AcceptButton = this.OkBox;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(955, 460);
			this.Controls.Add(this.TraceBox);
			this.Controls.Add(this.ErrorBox);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.SendReportBox);
			this.Controls.Add(this.OkBox);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(600, 400);
			this.Name = "ExceptionForm";
			this.Text = "ArcEngine Exception :";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button OkBox;
		private System.Windows.Forms.Button SendReportBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label ErrorBox;
		private System.Windows.Forms.TextBox TraceBox;
	}
}