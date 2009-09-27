namespace ArcEngine.Games.RuffnTumble.Editor.Wizards
{
	partial class NewLevelWizard
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
			this.label1 = new System.Windows.Forms.Label();
			this.LevelName = new System.Windows.Forms.TextBox();
			this.CreateButton = new System.Windows.Forms.Button();
			this.ButtonCancel = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.BlockHeightButton = new System.Windows.Forms.NumericUpDown();
			this.BlockWidthButton = new System.Windows.Forms.NumericUpDown();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.TextureBox = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.LevelHeightButton = new System.Windows.Forms.NumericUpDown();
			this.LevelWidthButton = new System.Windows.Forms.NumericUpDown();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.BlockHeightButton)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.BlockWidthButton)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.LevelHeightButton)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.LevelWidthButton)).BeginInit();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(7, 22);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(41, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Name :";
			// 
			// LevelName
			// 
			this.LevelName.Location = new System.Drawing.Point(54, 19);
			this.LevelName.Name = "LevelName";
			this.LevelName.Size = new System.Drawing.Size(220, 20);
			this.LevelName.TabIndex = 1;
			// 
			// CreateButton
			// 
			this.CreateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.CreateButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.CreateButton.Location = new System.Drawing.Point(135, 221);
			this.CreateButton.Name = "CreateButton";
			this.CreateButton.Size = new System.Drawing.Size(75, 23);
			this.CreateButton.TabIndex = 7;
			this.CreateButton.Text = "Create";
			this.CreateButton.UseVisualStyleBackColor = true;
			// 
			// ButtonCancel
			// 
			this.ButtonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.ButtonCancel.Location = new System.Drawing.Point(217, 220);
			this.ButtonCancel.Name = "ButtonCancel";
			this.ButtonCancel.Size = new System.Drawing.Size(75, 23);
			this.ButtonCancel.TabIndex = 8;
			this.ButtonCancel.Text = "Cancel";
			this.ButtonCancel.UseVisualStyleBackColor = true;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.BlockHeightButton);
			this.groupBox1.Controls.Add(this.BlockWidthButton);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.TextureBox);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.LevelHeightButton);
			this.groupBox1.Controls.Add(this.LevelWidthButton);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Location = new System.Drawing.Point(12, 79);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(283, 125);
			this.groupBox1.TabIndex = 5;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Layer properties :";
			// 
			// BlockHeightButton
			// 
			this.BlockHeightButton.Location = new System.Drawing.Point(219, 89);
			this.BlockHeightButton.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.BlockHeightButton.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.BlockHeightButton.Name = "BlockHeightButton";
			this.BlockHeightButton.Size = new System.Drawing.Size(55, 20);
			this.BlockHeightButton.TabIndex = 6;
			this.BlockHeightButton.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
			// 
			// BlockWidthButton
			// 
			this.BlockWidthButton.Location = new System.Drawing.Point(80, 89);
			this.BlockWidthButton.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.BlockWidthButton.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.BlockWidthButton.Name = "BlockWidthButton";
			this.BlockWidthButton.Size = new System.Drawing.Size(55, 20);
			this.BlockWidthButton.TabIndex = 5;
			this.BlockWidthButton.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(141, 91);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(72, 13);
			this.label6.TabIndex = 6;
			this.label6.Text = "Block height :";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(7, 91);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(68, 13);
			this.label5.TabIndex = 5;
			this.label5.Text = "Block width :";
			// 
			// TextureBox
			// 
			this.TextureBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.TextureBox.FormattingEnabled = true;
			this.TextureBox.Location = new System.Drawing.Point(57, 19);
			this.TextureBox.Name = "TextureBox";
			this.TextureBox.Size = new System.Drawing.Size(217, 21);
			this.TextureBox.TabIndex = 2;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(7, 22);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(49, 13);
			this.label2.TabIndex = 4;
			this.label2.Text = "Texture :";
			// 
			// LevelHeightButton
			// 
			this.LevelHeightButton.Location = new System.Drawing.Point(219, 54);
			this.LevelHeightButton.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.LevelHeightButton.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.LevelHeightButton.Name = "LevelHeightButton";
			this.LevelHeightButton.Size = new System.Drawing.Size(55, 20);
			this.LevelHeightButton.TabIndex = 4;
			this.LevelHeightButton.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// LevelWidthButton
			// 
			this.LevelWidthButton.Location = new System.Drawing.Point(80, 54);
			this.LevelWidthButton.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.LevelWidthButton.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.LevelWidthButton.Name = "LevelWidthButton";
			this.LevelWidthButton.Size = new System.Drawing.Size(55, 20);
			this.LevelWidthButton.TabIndex = 3;
			this.LevelWidthButton.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(141, 56);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(71, 13);
			this.label4.TabIndex = 1;
			this.label4.Text = "Level height :";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(7, 56);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(67, 13);
			this.label3.TabIndex = 0;
			this.label3.Text = "Level width :";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.label1);
			this.groupBox2.Controls.Add(this.LevelName);
			this.groupBox2.Location = new System.Drawing.Point(12, 12);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(283, 61);
			this.groupBox2.TabIndex = 6;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Level properties :";
			// 
			// NewLevelWizard
			// 
			this.AcceptButton = this.CreateButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.ButtonCancel;
			this.ClientSize = new System.Drawing.Size(304, 255);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.ButtonCancel);
			this.Controls.Add(this.CreateButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.HelpButton = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "NewLevelWizard";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "New level wizard";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnFormClosing);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.BlockHeightButton)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.BlockWidthButton)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.LevelHeightButton)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.LevelWidthButton)).EndInit();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox LevelName;
		private System.Windows.Forms.Button CreateButton;
		private System.Windows.Forms.Button ButtonCancel;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.NumericUpDown LevelHeightButton;
		private System.Windows.Forms.NumericUpDown LevelWidthButton;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.ComboBox TextureBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.NumericUpDown BlockHeightButton;
		private System.Windows.Forms.NumericUpDown BlockWidthButton;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label5;
	}
}