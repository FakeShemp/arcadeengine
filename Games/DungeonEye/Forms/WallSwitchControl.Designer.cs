namespace DungeonEye
{
	partial class WallSwitchControl
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

		#region Code généré par le Concepteur de composants

		/// <summary> 
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.ActivatedTab = new System.Windows.Forms.TabPage();
			this.label1 = new System.Windows.Forms.Label();
			this.ActivatedGLBox = new OpenTK.GLControl();
			this.ActivatedIdBox = new System.Windows.Forms.NumericUpDown();
			this.DeactivatedTab = new System.Windows.Forms.TabPage();
			this.label2 = new System.Windows.Forms.Label();
			this.DeactivatedGlBox = new OpenTK.GLControl();
			this.DeactivatedIdBox = new System.Windows.Forms.NumericUpDown();
			this.ScriptTab = new System.Windows.Forms.TabPage();
			this.groupBox1.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.ActivatedTab.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.ActivatedIdBox)).BeginInit();
			this.DeactivatedTab.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.DeactivatedIdBox)).BeginInit();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.tabControl1);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox1.Location = new System.Drawing.Point(0, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(874, 580);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Wall switch";
			// 
			// tabControl1
			// 
			this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl1.Controls.Add(this.ActivatedTab);
			this.tabControl1.Controls.Add(this.DeactivatedTab);
			this.tabControl1.Controls.Add(this.ScriptTab);
			this.tabControl1.Location = new System.Drawing.Point(6, 19);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(862, 555);
			this.tabControl1.TabIndex = 1;
			// 
			// ActivatedTab
			// 
			this.ActivatedTab.Controls.Add(this.label1);
			this.ActivatedTab.Controls.Add(this.ActivatedGLBox);
			this.ActivatedTab.Controls.Add(this.ActivatedIdBox);
			this.ActivatedTab.Location = new System.Drawing.Point(4, 22);
			this.ActivatedTab.Name = "ActivatedTab";
			this.ActivatedTab.Padding = new System.Windows.Forms.Padding(3);
			this.ActivatedTab.Size = new System.Drawing.Size(854, 529);
			this.ActivatedTab.TabIndex = 0;
			this.ActivatedTab.Text = "Activated";
			this.ActivatedTab.UseVisualStyleBackColor = true;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(76, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Decoration id :";
			// 
			// ActivatedGLBox
			// 
			this.ActivatedGLBox.BackColor = System.Drawing.Color.Black;
			this.ActivatedGLBox.Location = new System.Drawing.Point(6, 32);
			this.ActivatedGLBox.Name = "ActivatedGLBox";
			this.ActivatedGLBox.Size = new System.Drawing.Size(352, 240);
			this.ActivatedGLBox.TabIndex = 0;
			this.ActivatedGLBox.VSync = false;
			this.ActivatedGLBox.Load += new System.EventHandler(this.ActivatedGLBox_Load);
			this.ActivatedGLBox.Paint += new System.Windows.Forms.PaintEventHandler(this.ActivatedGLBox_Paint);
			// 
			// ActivatedIdBox
			// 
			this.ActivatedIdBox.Location = new System.Drawing.Point(88, 6);
			this.ActivatedIdBox.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
			this.ActivatedIdBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
			this.ActivatedIdBox.Name = "ActivatedIdBox";
			this.ActivatedIdBox.Size = new System.Drawing.Size(74, 20);
			this.ActivatedIdBox.TabIndex = 1;
			this.ActivatedIdBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.ActivatedIdBox.ThousandsSeparator = true;
			this.ActivatedIdBox.Value = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
			this.ActivatedIdBox.ValueChanged += new System.EventHandler(this.ActivatedIdBox_ValueChanged);
			// 
			// DeactivatedTab
			// 
			this.DeactivatedTab.Controls.Add(this.label2);
			this.DeactivatedTab.Controls.Add(this.DeactivatedGlBox);
			this.DeactivatedTab.Controls.Add(this.DeactivatedIdBox);
			this.DeactivatedTab.Location = new System.Drawing.Point(4, 22);
			this.DeactivatedTab.Name = "DeactivatedTab";
			this.DeactivatedTab.Padding = new System.Windows.Forms.Padding(3);
			this.DeactivatedTab.Size = new System.Drawing.Size(854, 529);
			this.DeactivatedTab.TabIndex = 1;
			this.DeactivatedTab.Text = "Deactivated";
			this.DeactivatedTab.UseVisualStyleBackColor = true;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6, 8);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(76, 13);
			this.label2.TabIndex = 5;
			this.label2.Text = "Decoration id :";
			// 
			// DeactivatedGlBox
			// 
			this.DeactivatedGlBox.BackColor = System.Drawing.Color.Black;
			this.DeactivatedGlBox.Location = new System.Drawing.Point(6, 32);
			this.DeactivatedGlBox.Name = "DeactivatedGlBox";
			this.DeactivatedGlBox.Size = new System.Drawing.Size(352, 240);
			this.DeactivatedGlBox.TabIndex = 3;
			this.DeactivatedGlBox.VSync = false;
			this.DeactivatedGlBox.Load += new System.EventHandler(this.DeactivatedGlBox_Load);
			this.DeactivatedGlBox.Paint += new System.Windows.Forms.PaintEventHandler(this.DeactivatedGlBox_Paint);
			// 
			// DeactivatedIdBox
			// 
			this.DeactivatedIdBox.Location = new System.Drawing.Point(88, 6);
			this.DeactivatedIdBox.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
			this.DeactivatedIdBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
			this.DeactivatedIdBox.Name = "DeactivatedIdBox";
			this.DeactivatedIdBox.Size = new System.Drawing.Size(74, 20);
			this.DeactivatedIdBox.TabIndex = 4;
			this.DeactivatedIdBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.DeactivatedIdBox.ThousandsSeparator = true;
			this.DeactivatedIdBox.Value = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
			this.DeactivatedIdBox.ValueChanged += new System.EventHandler(this.DeactivatedIdBox_ValueChanged);
			// 
			// ScriptTab
			// 
			this.ScriptTab.Location = new System.Drawing.Point(4, 22);
			this.ScriptTab.Name = "ScriptTab";
			this.ScriptTab.Size = new System.Drawing.Size(854, 529);
			this.ScriptTab.TabIndex = 2;
			this.ScriptTab.Text = "Script";
			this.ScriptTab.UseVisualStyleBackColor = true;
			// 
			// WallSwitchControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.groupBox1);
			this.Name = "WallSwitchControl";
			this.Size = new System.Drawing.Size(874, 580);
			this.Load += new System.EventHandler(this.WallSwitchControl_Load);
			this.groupBox1.ResumeLayout(false);
			this.tabControl1.ResumeLayout(false);
			this.ActivatedTab.ResumeLayout(false);
			this.ActivatedTab.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.ActivatedIdBox)).EndInit();
			this.DeactivatedTab.ResumeLayout(false);
			this.DeactivatedTab.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.DeactivatedIdBox)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private OpenTK.GLControl ActivatedGLBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.NumericUpDown ActivatedIdBox;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage ActivatedTab;
		private System.Windows.Forms.TabPage DeactivatedTab;
		private System.Windows.Forms.Label label2;
		private OpenTK.GLControl DeactivatedGlBox;
		private System.Windows.Forms.NumericUpDown DeactivatedIdBox;
		private System.Windows.Forms.TabPage ScriptTab;

	}
}
