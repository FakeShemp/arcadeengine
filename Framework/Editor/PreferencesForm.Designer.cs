namespace ArcEngine.Editor
{
	partial class PreferencesForm
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

		#region Code généré par le Concepteur Windows Form

		/// <summary>
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
			this.OkButton = new System.Windows.Forms.Button();
			this.Cancel_Button = new System.Windows.Forms.Button();
			this.DebugPage = new System.Windows.Forms.TabPage();
			this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.DebugBox = new System.Windows.Forms.ComboBox();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.ApplyButton = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.LevelRefreshRateButton = new System.Windows.Forms.NumericUpDown();
			this.DebugPage.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
			this.tabControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.LevelRefreshRateButton)).BeginInit();
			this.SuspendLayout();
			// 
			// OkButton
			// 
			this.OkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.OkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.OkButton.Location = new System.Drawing.Point(302, 396);
			this.OkButton.Name = "OkButton";
			this.OkButton.Size = new System.Drawing.Size(75, 23);
			this.OkButton.TabIndex = 1;
			this.OkButton.Text = "Ok";
			this.OkButton.UseVisualStyleBackColor = true;
			// 
			// Cancel_Button
			// 
			this.Cancel_Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.Cancel_Button.Location = new System.Drawing.Point(383, 396);
			this.Cancel_Button.Name = "Cancel_Button";
			this.Cancel_Button.Size = new System.Drawing.Size(75, 23);
			this.Cancel_Button.TabIndex = 2;
			this.Cancel_Button.Text = "Cancel";
			this.Cancel_Button.UseVisualStyleBackColor = true;
			// 
			// DebugPage
			// 
			this.DebugPage.Controls.Add(this.LevelRefreshRateButton);
			this.DebugPage.Controls.Add(this.label3);
			this.DebugPage.Controls.Add(this.numericUpDown1);
			this.DebugPage.Controls.Add(this.label2);
			this.DebugPage.Controls.Add(this.label1);
			this.DebugPage.Controls.Add(this.DebugBox);
			this.DebugPage.Location = new System.Drawing.Point(4, 22);
			this.DebugPage.Name = "DebugPage";
			this.DebugPage.Padding = new System.Windows.Forms.Padding(3);
			this.DebugPage.Size = new System.Drawing.Size(438, 352);
			this.DebugPage.TabIndex = 0;
			this.DebugPage.Text = "Misc";
			this.DebugPage.UseVisualStyleBackColor = true;
			// 
			// numericUpDown1
			// 
			this.numericUpDown1.Location = new System.Drawing.Point(143, 42);
			this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numericUpDown1.Name = "numericUpDown1";
			this.numericUpDown1.Size = new System.Drawing.Size(78, 20);
			this.numericUpDown1.TabIndex = 3;
			this.numericUpDown1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.numericUpDown1.ThousandsSeparator = true;
			this.numericUpDown1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6, 44);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(131, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Mouse scroll maxVelocity :";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(70, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Debug level :";
			// 
			// DebugBox
			// 
			this.DebugBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.DebugBox.FormattingEnabled = true;
			this.DebugBox.Items.AddRange(new object[] {
            "Debug",
            "ToDo",
            "Info",
            "Warning",
            "Error",
            "Fatal"});
			this.DebugBox.Location = new System.Drawing.Point(82, 6);
			this.DebugBox.Name = "DebugBox";
			this.DebugBox.Size = new System.Drawing.Size(121, 21);
			this.DebugBox.TabIndex = 0;
			// 
			// tabControl1
			// 
			this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl1.Controls.Add(this.DebugPage);
			this.tabControl1.Location = new System.Drawing.Point(12, 12);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(446, 378);
			this.tabControl1.TabIndex = 0;
			// 
			// ApplyButton
			// 
			this.ApplyButton.DialogResult = System.Windows.Forms.DialogResult.Yes;
			this.ApplyButton.Location = new System.Drawing.Point(221, 396);
			this.ApplyButton.Name = "ApplyButton";
			this.ApplyButton.Size = new System.Drawing.Size(75, 23);
			this.ApplyButton.TabIndex = 3;
			this.ApplyButton.Text = "Apply";
			this.ApplyButton.UseVisualStyleBackColor = true;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(9, 76);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(128, 13);
			this.label3.TabIndex = 4;
			this.label3.Text = "Level refresh rate (in ms) :";
			// 
			// LevelRefreshRateButton
			// 
			this.LevelRefreshRateButton.Location = new System.Drawing.Point(143, 74);
			this.LevelRefreshRateButton.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.LevelRefreshRateButton.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.LevelRefreshRateButton.Name = "LevelRefreshRateButton";
			this.LevelRefreshRateButton.Size = new System.Drawing.Size(78, 20);
			this.LevelRefreshRateButton.TabIndex = 5;
			this.LevelRefreshRateButton.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.LevelRefreshRateButton.ThousandsSeparator = true;
			this.LevelRefreshRateButton.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
			// 
			// PreferencesForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(470, 431);
			this.Controls.Add(this.ApplyButton);
			this.Controls.Add(this.Cancel_Button);
			this.Controls.Add(this.OkButton);
			this.Controls.Add(this.tabControl1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "PreferencesForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.Text = "Preferences";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PreferencesForm_FormClosing);
			this.DebugPage.ResumeLayout(false);
			this.DebugPage.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
			this.tabControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.LevelRefreshRateButton)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button OkButton;
		private System.Windows.Forms.Button Cancel_Button;
		private System.Windows.Forms.TabPage DebugPage;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.ComboBox DebugBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.NumericUpDown numericUpDown1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button ApplyButton;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.NumericUpDown LevelRefreshRateButton;
	}
}