namespace ArcEngine.Editor
{
	partial class AudioForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AudioForm));
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.RewindBox = new System.Windows.Forms.ToolStripButton();
			this.PlayBox = new System.Windows.Forms.ToolStripButton();
			this.PauseBox = new System.Windows.Forms.ToolStripButton();
			this.StopBox = new System.Windows.Forms.ToolStripButton();
			this.ForwardBox = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.LoopControlBox = new System.Windows.Forms.ToolStripButton();
			this.textBox3 = new System.Windows.Forms.TextBox();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.TypeBox = new System.Windows.Forms.ComboBox();
			this.label5 = new System.Windows.Forms.Label();
			this.FileBox = new System.Windows.Forms.ComboBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.toolStrip2 = new System.Windows.Forms.ToolStrip();
			this.OpenFileBox = new System.Windows.Forms.ToolStripButton();
			this.groupBox1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.toolStrip2.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.toolStrip1);
			this.groupBox1.Controls.Add(this.textBox3);
			this.groupBox1.Controls.Add(this.textBox2);
			this.groupBox1.Controls.Add(this.textBox1);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.TypeBox);
			this.groupBox1.Location = new System.Drawing.Point(12, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(194, 165);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Settings :";
			// 
			// toolStrip1
			// 
			this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RewindBox,
            this.PlayBox,
            this.PauseBox,
            this.StopBox,
            this.ForwardBox,
            this.toolStripSeparator1,
            this.LoopControlBox});
			this.toolStrip1.Location = new System.Drawing.Point(3, 16);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.toolStrip1.Size = new System.Drawing.Size(188, 25);
			this.toolStrip1.TabIndex = 0;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// RewindBox
			// 
			this.RewindBox.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.RewindBox.Image = ((System.Drawing.Image)(resources.GetObject("RewindBox.Image")));
			this.RewindBox.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.RewindBox.Name = "RewindBox";
			this.RewindBox.Size = new System.Drawing.Size(23, 22);
			this.RewindBox.Text = "Rewind";
			// 
			// PlayBox
			// 
			this.PlayBox.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.PlayBox.Image = ((System.Drawing.Image)(resources.GetObject("PlayBox.Image")));
			this.PlayBox.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.PlayBox.Name = "PlayBox";
			this.PlayBox.Size = new System.Drawing.Size(23, 22);
			this.PlayBox.Text = "Play";
			// 
			// PauseBox
			// 
			this.PauseBox.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.PauseBox.Image = ((System.Drawing.Image)(resources.GetObject("PauseBox.Image")));
			this.PauseBox.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.PauseBox.Name = "PauseBox";
			this.PauseBox.Size = new System.Drawing.Size(23, 22);
			this.PauseBox.Text = "Pause";
			// 
			// StopBox
			// 
			this.StopBox.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.StopBox.Image = ((System.Drawing.Image)(resources.GetObject("StopBox.Image")));
			this.StopBox.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.StopBox.Name = "StopBox";
			this.StopBox.Size = new System.Drawing.Size(23, 22);
			this.StopBox.Text = "Stop";
			// 
			// ForwardBox
			// 
			this.ForwardBox.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.ForwardBox.Image = ((System.Drawing.Image)(resources.GetObject("ForwardBox.Image")));
			this.ForwardBox.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.ForwardBox.Name = "ForwardBox";
			this.ForwardBox.Size = new System.Drawing.Size(23, 22);
			this.ForwardBox.Text = "Forward";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// LoopControlBox
			// 
			this.LoopControlBox.CheckOnClick = true;
			this.LoopControlBox.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.LoopControlBox.Image = ((System.Drawing.Image)(resources.GetObject("LoopControlBox.Image")));
			this.LoopControlBox.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.LoopControlBox.Name = "LoopControlBox";
			this.LoopControlBox.Size = new System.Drawing.Size(23, 22);
			this.LoopControlBox.Text = "Loop sound";
			// 
			// textBox3
			// 
			this.textBox3.Location = new System.Drawing.Point(79, 132);
			this.textBox3.Name = "textBox3";
			this.textBox3.Size = new System.Drawing.Size(100, 20);
			this.textBox3.TabIndex = 6;
			this.textBox3.Text = "1.0";
			// 
			// textBox2
			// 
			this.textBox2.Location = new System.Drawing.Point(79, 106);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(100, 20);
			this.textBox2.TabIndex = 6;
			this.textBox2.Text = "0.0";
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(79, 80);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(100, 20);
			this.textBox1.TabIndex = 6;
			this.textBox1.Text = "1.0";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(7, 135);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(56, 13);
			this.label4.TabIndex = 5;
			this.label4.Text = "Max gain :";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(7, 109);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(53, 13);
			this.label3.TabIndex = 4;
			this.label3.Text = "Min gain :";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(7, 83);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(37, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "Pitch :";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(7, 52);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(37, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Type :";
			// 
			// TypeBox
			// 
			this.TypeBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.TypeBox.FormattingEnabled = true;
			this.TypeBox.Location = new System.Drawing.Point(79, 49);
			this.TypeBox.Name = "TypeBox";
			this.TypeBox.Size = new System.Drawing.Size(100, 21);
			this.TypeBox.TabIndex = 1;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(6, 52);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(29, 13);
			this.label5.TabIndex = 7;
			this.label5.Text = "File :";
			// 
			// FileBox
			// 
			this.FileBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.FileBox.FormattingEnabled = true;
			this.FileBox.Location = new System.Drawing.Point(41, 49);
			this.FileBox.Name = "FileBox";
			this.FileBox.Size = new System.Drawing.Size(171, 21);
			this.FileBox.TabIndex = 8;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.toolStrip2);
			this.groupBox2.Controls.Add(this.FileBox);
			this.groupBox2.Controls.Add(this.label5);
			this.groupBox2.Location = new System.Drawing.Point(212, 12);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(224, 165);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "File :";
			// 
			// toolStrip2
			// 
			this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenFileBox});
			this.toolStrip2.Location = new System.Drawing.Point(3, 16);
			this.toolStrip2.Name = "toolStrip2";
			this.toolStrip2.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.toolStrip2.Size = new System.Drawing.Size(218, 25);
			this.toolStrip2.TabIndex = 9;
			this.toolStrip2.Text = "toolStrip2";
			// 
			// OpenFileBox
			// 
			this.OpenFileBox.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.OpenFileBox.Image = ((System.Drawing.Image)(resources.GetObject("OpenFileBox.Image")));
			this.OpenFileBox.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.OpenFileBox.Name = "OpenFileBox";
			this.OpenFileBox.Size = new System.Drawing.Size(23, 22);
			this.OpenFileBox.Text = "&Ouvrir";
			// 
			// AudioForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(955, 581);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "AudioForm";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SoundForm_FormClosing);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.toolStrip2.ResumeLayout(false);
			this.toolStrip2.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox textBox3;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox TypeBox;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton RewindBox;
		private System.Windows.Forms.ToolStripButton PlayBox;
		private System.Windows.Forms.ToolStripButton PauseBox;
		private System.Windows.Forms.ToolStripButton StopBox;
		private System.Windows.Forms.ToolStripButton ForwardBox;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton LoopControlBox;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.ComboBox FileBox;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.ToolStrip toolStrip2;
		private System.Windows.Forms.ToolStripButton OpenFileBox;


	}
}
