namespace ArcEngine.Games.RuffnTumble.Editor.Wizards
{
	partial class ImportLevelWizard
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImportLevelWizard));
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.PreviewBox = new System.Windows.Forms.PictureBox();
			this.ButtonClose = new System.Windows.Forms.Button();
			this.ButtonGenerate = new System.Windows.Forms.Button();
			this.LoadPictureButton = new System.Windows.Forms.Button();
			this.BlockWidthBox = new System.Windows.Forms.NumericUpDown();
			this.BlockHeightBox = new System.Windows.Forms.NumericUpDown();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.LevelNameBox = new System.Windows.Forms.TextBox();
			this.TextureNameBox = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.StatusStripBox = new System.Windows.Forms.StatusStrip();
			this.ReportLabelBox = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.ProgressBarBox = new System.Windows.Forms.ToolStripProgressBar();
			this.dlg = new System.Windows.Forms.OpenFileDialog();
			this.BgWorker = new System.ComponentModel.BackgroundWorker();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.PreviewBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.BlockWidthBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.BlockHeightBox)).BeginInit();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.StatusStripBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.PreviewBox);
			this.groupBox1.Location = new System.Drawing.Point(12, 97);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(489, 325);
			this.groupBox1.TabIndex = 3;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Level map preview :";
			// 
			// PreviewBox
			// 
			this.PreviewBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PreviewBox.Location = new System.Drawing.Point(3, 16);
			this.PreviewBox.Name = "PreviewBox";
			this.PreviewBox.Size = new System.Drawing.Size(483, 306);
			this.PreviewBox.TabIndex = 0;
			this.PreviewBox.TabStop = false;
			this.PreviewBox.Paint += new System.Windows.Forms.PaintEventHandler(this.PreviewBox_Paint);
			// 
			// ButtonClose
			// 
			this.ButtonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.ButtonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.ButtonClose.Location = new System.Drawing.Point(429, 428);
			this.ButtonClose.Name = "ButtonClose";
			this.ButtonClose.Size = new System.Drawing.Size(75, 23);
			this.ButtonClose.TabIndex = 7;
			this.ButtonClose.Text = "Close";
			this.ButtonClose.UseVisualStyleBackColor = true;
			// 
			// ButtonGenerate
			// 
			this.ButtonGenerate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.ButtonGenerate.Location = new System.Drawing.Point(14, 51);
			this.ButtonGenerate.Name = "ButtonGenerate";
			this.ButtonGenerate.Size = new System.Drawing.Size(82, 23);
			this.ButtonGenerate.TabIndex = 6;
			this.ButtonGenerate.Text = "Generate !";
			this.ButtonGenerate.UseVisualStyleBackColor = true;
			this.ButtonGenerate.Click += new System.EventHandler(this.ButtonGenerate_Click);
			// 
			// LoadPictureButton
			// 
			this.LoadPictureButton.Image = ((System.Drawing.Image)(resources.GetObject("LoadPictureButton.Image")));
			this.LoadPictureButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.LoadPictureButton.Location = new System.Drawing.Point(14, 19);
			this.LoadPictureButton.Name = "LoadPictureButton";
			this.LoadPictureButton.Size = new System.Drawing.Size(82, 23);
			this.LoadPictureButton.TabIndex = 3;
			this.LoadPictureButton.Text = "Load file...";
			this.LoadPictureButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.LoadPictureButton.UseVisualStyleBackColor = true;
			this.LoadPictureButton.Click += new System.EventHandler(this.LoadPictureButton_Click);
			// 
			// BlockWidthBox
			// 
			this.BlockWidthBox.Location = new System.Drawing.Point(56, 20);
			this.BlockWidthBox.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.BlockWidthBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.BlockWidthBox.Name = "BlockWidthBox";
			this.BlockWidthBox.Size = new System.Drawing.Size(46, 20);
			this.BlockWidthBox.TabIndex = 4;
			this.BlockWidthBox.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
			this.BlockWidthBox.ValueChanged += new System.EventHandler(this.BlockSize_ValueChanged);
			// 
			// BlockHeightBox
			// 
			this.BlockHeightBox.Location = new System.Drawing.Point(56, 49);
			this.BlockHeightBox.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.BlockHeightBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.BlockHeightBox.Name = "BlockHeightBox";
			this.BlockHeightBox.Size = new System.Drawing.Size(46, 20);
			this.BlockHeightBox.TabIndex = 5;
			this.BlockHeightBox.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(10, 22);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(41, 13);
			this.label1.TabIndex = 9;
			this.label1.Text = "Width :";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6, 51);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(44, 13);
			this.label2.TabIndex = 10;
			this.label2.Text = "Height :";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(6, 16);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(68, 13);
			this.label3.TabIndex = 11;
			this.label3.Text = "Level name :";
			// 
			// LevelNameBox
			// 
			this.LevelNameBox.Location = new System.Drawing.Point(90, 13);
			this.LevelNameBox.Name = "LevelNameBox";
			this.LevelNameBox.Size = new System.Drawing.Size(169, 20);
			this.LevelNameBox.TabIndex = 1;
			// 
			// TextureNameBox
			// 
			this.TextureNameBox.Location = new System.Drawing.Point(90, 40);
			this.TextureNameBox.Name = "TextureNameBox";
			this.TextureNameBox.Size = new System.Drawing.Size(169, 20);
			this.TextureNameBox.TabIndex = 2;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(6, 43);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(78, 13);
			this.label4.TabIndex = 13;
			this.label4.Text = "Texture name :";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Controls.Add(this.BlockWidthBox);
			this.groupBox2.Controls.Add(this.BlockHeightBox);
			this.groupBox2.Controls.Add(this.label1);
			this.groupBox2.Location = new System.Drawing.Point(399, 12);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(110, 80);
			this.groupBox2.TabIndex = 14;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Blocks size :";
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.LoadPictureButton);
			this.groupBox3.Controls.Add(this.ButtonGenerate);
			this.groupBox3.Location = new System.Drawing.Point(283, 12);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(110, 80);
			this.groupBox3.TabIndex = 15;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Source file :";
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.label3);
			this.groupBox4.Controls.Add(this.LevelNameBox);
			this.groupBox4.Controls.Add(this.label4);
			this.groupBox4.Controls.Add(this.TextureNameBox);
			this.groupBox4.Location = new System.Drawing.Point(12, 12);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(265, 80);
			this.groupBox4.TabIndex = 1;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Resources name :";
			// 
			// StatusStripBox
			// 
			this.StatusStripBox.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ReportLabelBox,
            this.toolStripStatusLabel1,
            this.ProgressBarBox});
			this.StatusStripBox.Location = new System.Drawing.Point(0, 454);
			this.StatusStripBox.Name = "StatusStripBox";
			this.StatusStripBox.Size = new System.Drawing.Size(516, 22);
			this.StatusStripBox.TabIndex = 16;
			// 
			// ReportLabelBox
			// 
			this.ReportLabelBox.Name = "ReportLabelBox";
			this.ReportLabelBox.Size = new System.Drawing.Size(49, 17);
			this.ReportLabelBox.Text = "message";
			this.ReportLabelBox.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// toolStripStatusLabel1
			// 
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			this.toolStripStatusLabel1.Size = new System.Drawing.Size(452, 17);
			this.toolStripStatusLabel1.Spring = true;
			// 
			// ProgressBarBox
			// 
			this.ProgressBarBox.Name = "ProgressBarBox";
			this.ProgressBarBox.Size = new System.Drawing.Size(100, 16);
			this.ProgressBarBox.Visible = false;
			// 
			// dlg
			// 
			this.dlg.DefaultExt = "png";
			this.dlg.RestoreDirectory = true;
			// 
			// BgWorker
			// 
			this.BgWorker.WorkerReportsProgress = true;
			this.BgWorker.WorkerSupportsCancellation = true;
			this.BgWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BgWorker_DoWork);
			this.BgWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BgWorker_RunWorkerCompleted);
			this.BgWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BgWorker_ProgressChanged);
			// 
			// ImportLevelWizard
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.ButtonClose;
			this.ClientSize = new System.Drawing.Size(516, 476);
			this.Controls.Add(this.StatusStripBox);
			this.Controls.Add(this.groupBox4);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.ButtonClose);
			this.Controls.Add(this.groupBox1);
			this.MinimizeBox = false;
			this.Name = "ImportLevelWizard";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Import level from game map wizard";
			this.groupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.PreviewBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.BlockWidthBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.BlockHeightBox)).EndInit();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.groupBox4.ResumeLayout(false);
			this.groupBox4.PerformLayout();
			this.StatusStripBox.ResumeLayout(false);
			this.StatusStripBox.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button ButtonClose;
		private System.Windows.Forms.Button ButtonGenerate;
		private System.Windows.Forms.Button LoadPictureButton;
		private System.Windows.Forms.NumericUpDown BlockWidthBox;
		private System.Windows.Forms.NumericUpDown BlockHeightBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.PictureBox PreviewBox;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox LevelNameBox;
		private System.Windows.Forms.TextBox TextureNameBox;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.StatusStrip StatusStripBox;
		private System.Windows.Forms.ToolStripProgressBar ProgressBarBox;
		private System.Windows.Forms.OpenFileDialog dlg;
		private System.Windows.Forms.ToolStripStatusLabel ReportLabelBox;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
		private System.ComponentModel.BackgroundWorker BgWorker;
	}
}