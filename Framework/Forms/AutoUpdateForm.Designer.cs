namespace ArcEngine.Forms
{
	partial class AutoUpdateForm
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
			this.UpgradeLaterBox = new System.Windows.Forms.Button();
			this.UpgradeNowBox = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.CurrentVersionBox = new System.Windows.Forms.Label();
			this.RecentVersionBox = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.UriBox = new System.Windows.Forms.LinkLabel();
			this.SuspendLayout();
			// 
			// UpgradeLaterBox
			// 
			this.UpgradeLaterBox.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.UpgradeLaterBox.DialogResult = System.Windows.Forms.DialogResult.Ignore;
			this.UpgradeLaterBox.Location = new System.Drawing.Point(12, 133);
			this.UpgradeLaterBox.Name = "UpgradeLaterBox";
			this.UpgradeLaterBox.Size = new System.Drawing.Size(150, 23);
			this.UpgradeLaterBox.TabIndex = 0;
			this.UpgradeLaterBox.Text = "Upgrade &Later";
			this.UpgradeLaterBox.UseVisualStyleBackColor = true;
			// 
			// UpgradeNowBox
			// 
			this.UpgradeNowBox.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.UpgradeNowBox.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.UpgradeNowBox.Location = new System.Drawing.Point(185, 133);
			this.UpgradeNowBox.Name = "UpgradeNowBox";
			this.UpgradeNowBox.Size = new System.Drawing.Size(150, 23);
			this.UpgradeNowBox.TabIndex = 1;
			this.UpgradeNowBox.Text = "Upgrade &Now";
			this.UpgradeNowBox.UseVisualStyleBackColor = true;
			this.UpgradeNowBox.Click += new System.EventHandler(this.UpgradeNowBox_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(71, 53);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(118, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Your current version is :";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(73, 75);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(116, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Most recent version is :";
			// 
			// CurrentVersionBox
			// 
			this.CurrentVersionBox.AutoSize = true;
			this.CurrentVersionBox.Location = new System.Drawing.Point(195, 53);
			this.CurrentVersionBox.Name = "CurrentVersionBox";
			this.CurrentVersionBox.Size = new System.Drawing.Size(35, 13);
			this.CurrentVersionBox.TabIndex = 3;
			this.CurrentVersionBox.Text = "label3";
			// 
			// RecentVersionBox
			// 
			this.RecentVersionBox.AutoSize = true;
			this.RecentVersionBox.Location = new System.Drawing.Point(195, 75);
			this.RecentVersionBox.Name = "RecentVersionBox";
			this.RecentVersionBox.Size = new System.Drawing.Size(35, 13);
			this.RecentVersionBox.TabIndex = 3;
			this.RecentVersionBox.Text = "label3";
			// 
			// label5
			// 
			this.label5.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label5.Location = new System.Drawing.Point(12, 9);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(323, 33);
			this.label5.TabIndex = 4;
			this.label5.Text = "A new update is available for %s";
			this.label5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// UriBox
			// 
			this.UriBox.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.UriBox.Location = new System.Drawing.Point(12, 98);
			this.UriBox.Name = "UriBox";
			this.UriBox.Size = new System.Drawing.Size(323, 23);
			this.UriBox.TabIndex = 5;
			this.UriBox.TabStop = true;
			this.UriBox.Text = "linkLabel1";
			this.UriBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.UriBox.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.UriBox_LinkClicked);
			// 
			// AutoUpdateForm
			// 
			this.AcceptButton = this.UpgradeNowBox;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.UpgradeLaterBox;
			this.ClientSize = new System.Drawing.Size(347, 168);
			this.Controls.Add(this.UriBox);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.RecentVersionBox);
			this.Controls.Add(this.CurrentVersionBox);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.UpgradeNowBox);
			this.Controls.Add(this.UpgradeLaterBox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "AutoUpdateForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "An update is available";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button UpgradeLaterBox;
		private System.Windows.Forms.Button UpgradeNowBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label CurrentVersionBox;
		private System.Windows.Forms.Label RecentVersionBox;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.LinkLabel UriBox;
	}
}