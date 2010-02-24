namespace Network
{
	partial class ClientForm
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
			this.SendMsgBox = new System.Windows.Forms.Button();
			this.MsgBox = new System.Windows.Forms.TextBox();
			this.LogBox = new System.Windows.Forms.TextBox();
			this.UpdateTimer = new System.Windows.Forms.Timer(this.components);
			this.SuspendLayout();
			// 
			// SendMsgBox
			// 
			this.SendMsgBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.SendMsgBox.Location = new System.Drawing.Point(277, 277);
			this.SendMsgBox.Name = "SendMsgBox";
			this.SendMsgBox.Size = new System.Drawing.Size(75, 23);
			this.SendMsgBox.TabIndex = 0;
			this.SendMsgBox.Text = "Send";
			this.SendMsgBox.UseVisualStyleBackColor = true;
			this.SendMsgBox.Click += new System.EventHandler(this.SendMsgBox_Click);
			// 
			// MsgBox
			// 
			this.MsgBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.MsgBox.Location = new System.Drawing.Point(12, 280);
			this.MsgBox.Name = "MsgBox";
			this.MsgBox.Size = new System.Drawing.Size(259, 20);
			this.MsgBox.TabIndex = 1;
			// 
			// LogBox
			// 
			this.LogBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.LogBox.Location = new System.Drawing.Point(12, 12);
			this.LogBox.Multiline = true;
			this.LogBox.Name = "LogBox";
			this.LogBox.ReadOnly = true;
			this.LogBox.Size = new System.Drawing.Size(340, 259);
			this.LogBox.TabIndex = 2;
			// 
			// UpdateTimer
			// 
			this.UpdateTimer.Interval = 20;
			this.UpdateTimer.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// ClientForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(364, 312);
			this.Controls.Add(this.LogBox);
			this.Controls.Add(this.MsgBox);
			this.Controls.Add(this.SendMsgBox);
			this.Name = "ClientForm";
			this.Text = "Network client";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ClientForm_FormClosed);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button SendMsgBox;
		private System.Windows.Forms.TextBox MsgBox;
		private System.Windows.Forms.TextBox LogBox;
		private System.Windows.Forms.Timer UpdateTimer;
	}
}