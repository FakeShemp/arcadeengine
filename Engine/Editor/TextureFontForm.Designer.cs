namespace ArcEngine.Editor
{
	partial class TextureFontForm
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
			this.components = new System.ComponentModel.Container();
			this.PreviewBox = new System.Windows.Forms.GroupBox();
			this.GlControl = new OpenTK.GLControl();
			this.PreviewTextBox = new System.Windows.Forms.TextBox();
			this.FontPropertyBox = new System.Windows.Forms.PropertyGrid();
			this.PropertyBox = new System.Windows.Forms.GroupBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.StyleBox = new System.Windows.Forms.ComboBox();
			this.SizeBox = new System.Windows.Forms.NumericUpDown();
			this.FontNameBox = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.GenerateButton = new System.Windows.Forms.Button();
			this.DrawTimer = new System.Windows.Forms.Timer(this.components);
			this.PreviewBox.SuspendLayout();
			this.PropertyBox.SuspendLayout();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.SizeBox)).BeginInit();
			this.SuspendLayout();
			// 
			// PreviewBox
			// 
			this.PreviewBox.Controls.Add(this.GlControl);
			this.PreviewBox.Controls.Add(this.PreviewTextBox);
			this.PreviewBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PreviewBox.Location = new System.Drawing.Point(201, 0);
			this.PreviewBox.Name = "PreviewBox";
			this.PreviewBox.Size = new System.Drawing.Size(576, 512);
			this.PreviewBox.TabIndex = 0;
			this.PreviewBox.TabStop = false;
			this.PreviewBox.Text = "Text preview :";
			// 
			// GlControl
			// 
			this.GlControl.BackColor = System.Drawing.Color.Black;
			this.GlControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.GlControl.Location = new System.Drawing.Point(3, 36);
			this.GlControl.Name = "GlControl";
			this.GlControl.Size = new System.Drawing.Size(570, 473);
			this.GlControl.TabIndex = 0;
			this.GlControl.VSync = true;
			this.GlControl.Paint += new System.Windows.Forms.PaintEventHandler(this.GlControl_Paint);
			this.GlControl.Resize += new System.EventHandler(this.GlControl_Resize);
			// 
			// PreviewTextBox
			// 
			this.PreviewTextBox.Dock = System.Windows.Forms.DockStyle.Top;
			this.PreviewTextBox.Location = new System.Drawing.Point(3, 16);
			this.PreviewTextBox.Name = "PreviewTextBox";
			this.PreviewTextBox.Size = new System.Drawing.Size(570, 20);
			this.PreviewTextBox.TabIndex = 1;
			this.PreviewTextBox.Text = "This is a test of the selected font.";
			// 
			// FontPropertyBox
			// 
			this.FontPropertyBox.Dock = System.Windows.Forms.DockStyle.Top;
			this.FontPropertyBox.Location = new System.Drawing.Point(3, 16);
			this.FontPropertyBox.Name = "FontPropertyBox";
			this.FontPropertyBox.Size = new System.Drawing.Size(195, 397);
			this.FontPropertyBox.TabIndex = 1;
			// 
			// PropertyBox
			// 
			this.PropertyBox.Controls.Add(this.groupBox1);
			this.PropertyBox.Controls.Add(this.FontPropertyBox);
			this.PropertyBox.Dock = System.Windows.Forms.DockStyle.Left;
			this.PropertyBox.Location = new System.Drawing.Point(0, 0);
			this.PropertyBox.Name = "PropertyBox";
			this.PropertyBox.Size = new System.Drawing.Size(201, 512);
			this.PropertyBox.TabIndex = 1;
			this.PropertyBox.TabStop = false;
			this.PropertyBox.Text = "Properties :";
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.groupBox1.Controls.Add(this.StyleBox);
			this.groupBox1.Controls.Add(this.SizeBox);
			this.groupBox1.Controls.Add(this.FontNameBox);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.GenerateButton);
			this.groupBox1.Location = new System.Drawing.Point(3, 375);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(195, 134);
			this.groupBox1.TabIndex = 3;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Change properties";
			// 
			// StyleBox
			// 
			this.StyleBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.StyleBox.FormattingEnabled = true;
			this.StyleBox.Location = new System.Drawing.Point(76, 73);
			this.StyleBox.Name = "StyleBox";
			this.StyleBox.Size = new System.Drawing.Size(113, 21);
			this.StyleBox.TabIndex = 8;
			// 
			// SizeBox
			// 
			this.SizeBox.Location = new System.Drawing.Point(76, 45);
			this.SizeBox.Name = "SizeBox";
			this.SizeBox.Size = new System.Drawing.Size(112, 20);
			this.SizeBox.TabIndex = 7;
			// 
			// FontNameBox
			// 
			this.FontNameBox.FormattingEnabled = true;
			this.FontNameBox.Location = new System.Drawing.Point(76, 17);
			this.FontNameBox.Name = "FontNameBox";
			this.FontNameBox.Size = new System.Drawing.Size(113, 21);
			this.FontNameBox.TabIndex = 6;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(34, 76);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(36, 13);
			this.label3.TabIndex = 5;
			this.label3.Text = "Style :";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(34, 47);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(33, 13);
			this.label2.TabIndex = 4;
			this.label2.Text = "Size :";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(7, 20);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(63, 13);
			this.label1.TabIndex = 3;
			this.label1.Text = "Font name :";
			// 
			// GenerateButton
			// 
			this.GenerateButton.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.GenerateButton.Location = new System.Drawing.Point(3, 108);
			this.GenerateButton.Name = "GenerateButton";
			this.GenerateButton.Size = new System.Drawing.Size(189, 23);
			this.GenerateButton.TabIndex = 2;
			this.GenerateButton.Text = "Generate";
			this.GenerateButton.UseVisualStyleBackColor = true;
			this.GenerateButton.Click += new System.EventHandler(this.GenerateButton_Click);
			// 
			// DrawTimer
			// 
			this.DrawTimer.Enabled = true;
			this.DrawTimer.Tick += new System.EventHandler(this.DrawTimer_Tick);
			// 
			// TextureFontForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(777, 512);
			this.Controls.Add(this.PreviewBox);
			this.Controls.Add(this.PropertyBox);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "TextureFontForm";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnFormClosing);
			this.PreviewBox.ResumeLayout(false);
			this.PreviewBox.PerformLayout();
			this.PropertyBox.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.SizeBox)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox PreviewBox;
		private OpenTK.GLControl GlControl;
		private System.Windows.Forms.PropertyGrid FontPropertyBox;
		private System.Windows.Forms.GroupBox PropertyBox;
		private System.Windows.Forms.TextBox PreviewTextBox;
		private System.Windows.Forms.Button GenerateButton;
		private System.Windows.Forms.Timer DrawTimer;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ComboBox StyleBox;
		private System.Windows.Forms.NumericUpDown SizeBox;
		private System.Windows.Forms.ComboBox FontNameBox;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
	}
}
