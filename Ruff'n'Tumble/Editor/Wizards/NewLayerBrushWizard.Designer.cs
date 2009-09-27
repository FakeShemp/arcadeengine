namespace ArcEngine.Games.RuffnTumble.Editor.Wizards
{
	partial class NewLayerBrushWizard
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
			this.ButtonCancel = new System.Windows.Forms.Button();
			this.CreateButton = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.NameBox = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// ButtonCancel
			// 
			this.ButtonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.ButtonCancel.Location = new System.Drawing.Point(138, 42);
			this.ButtonCancel.Name = "ButtonCancel";
			this.ButtonCancel.Size = new System.Drawing.Size(75, 23);
			this.ButtonCancel.TabIndex = 0;
			this.ButtonCancel.Text = "Cancel";
			this.ButtonCancel.UseVisualStyleBackColor = true;
			// 
			// CreateButton
			// 
			this.CreateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.CreateButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.CreateButton.Location = new System.Drawing.Point(57, 42);
			this.CreateButton.Name = "CreateButton";
			this.CreateButton.Size = new System.Drawing.Size(75, 23);
			this.CreateButton.TabIndex = 1;
			this.CreateButton.Text = "Create";
			this.CreateButton.UseVisualStyleBackColor = true;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 15);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(41, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Name :";
			// 
			// NameBox
			// 
			this.NameBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.NameBox.Location = new System.Drawing.Point(59, 12);
			this.NameBox.Name = "NameBox";
			this.NameBox.Size = new System.Drawing.Size(154, 20);
			this.NameBox.TabIndex = 3;
			// 
			// NewLayerBrushWizard
			// 
			this.AcceptButton = this.CreateButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.ButtonCancel;
			this.ClientSize = new System.Drawing.Size(225, 77);
			this.Controls.Add(this.NameBox);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.CreateButton);
			this.Controls.Add(this.ButtonCancel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "NewLayerBrushWizard";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "New Layer Brush Wizard";
			this.Shown += new System.EventHandler(this.NewLayerBrushWizard_Shown);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.NewLayerBrushWizard_FormClosing);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button ButtonCancel;
		private System.Windows.Forms.Button CreateButton;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox NameBox;
	}
}