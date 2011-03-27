namespace DungeonEye.Forms
{
	partial class DecorationSetForm
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
			this.OpenGLBox = new OpenTK.GLControl();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.DisplayWallBox = new System.Windows.Forms.CheckBox();
			this.label1 = new System.Windows.Forms.Label();
			this.BackgroundTileSetBox = new System.Windows.Forms.ComboBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.LocationBox = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.TileIdBox = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
			this.label2 = new System.Windows.Forms.Label();
			this.TilesetBox = new System.Windows.Forms.ComboBox();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.ViewPositionBox = new DungeonEye.Forms.ViewPositionControl();
			this.DrawTimer = new System.Windows.Forms.Timer(this.components);
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.TileIdBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
			this.groupBox3.SuspendLayout();
			this.SuspendLayout();
			// 
			// OpenGLBox
			// 
			this.OpenGLBox.BackColor = System.Drawing.Color.Black;
			this.OpenGLBox.Location = new System.Drawing.Point(6, 77);
			this.OpenGLBox.Name = "OpenGLBox";
			this.OpenGLBox.Size = new System.Drawing.Size(352, 240);
			this.OpenGLBox.TabIndex = 0;
			this.OpenGLBox.VSync = false;
			this.OpenGLBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OpenGLBox_MouseDown);
			this.OpenGLBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OpenGLBox_MouseMove);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.DisplayWallBox);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.BackgroundTileSetBox);
			this.groupBox1.Controls.Add(this.OpenGLBox);
			this.groupBox1.Location = new System.Drawing.Point(263, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(369, 323);
			this.groupBox1.TabIndex = 2;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Preview :";
			// 
			// DisplayWallBox
			// 
			this.DisplayWallBox.AutoSize = true;
			this.DisplayWallBox.Checked = true;
			this.DisplayWallBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.DisplayWallBox.Location = new System.Drawing.Point(116, 46);
			this.DisplayWallBox.Name = "DisplayWallBox";
			this.DisplayWallBox.Size = new System.Drawing.Size(86, 17);
			this.DisplayWallBox.TabIndex = 3;
			this.DisplayWallBox.Text = "Display walls";
			this.DisplayWallBox.UseVisualStyleBackColor = true;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 22);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(101, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Background tileset :";
			// 
			// BackgroundTileSetBox
			// 
			this.BackgroundTileSetBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.BackgroundTileSetBox.FormattingEnabled = true;
			this.BackgroundTileSetBox.Location = new System.Drawing.Point(116, 19);
			this.BackgroundTileSetBox.Name = "BackgroundTileSetBox";
			this.BackgroundTileSetBox.Size = new System.Drawing.Size(242, 21);
			this.BackgroundTileSetBox.Sorted = true;
			this.BackgroundTileSetBox.TabIndex = 1;
			this.BackgroundTileSetBox.SelectedIndexChanged += new System.EventHandler(this.BgTileSetBox_SelectedIndexChanged);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.LocationBox);
			this.groupBox2.Controls.Add(this.label4);
			this.groupBox2.Controls.Add(this.label5);
			this.groupBox2.Controls.Add(this.label3);
			this.groupBox2.Controls.Add(this.TileIdBox);
			this.groupBox2.Controls.Add(this.numericUpDown1);
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Controls.Add(this.TilesetBox);
			this.groupBox2.Location = new System.Drawing.Point(12, 175);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(245, 160);
			this.groupBox2.TabIndex = 3;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Properties :";
			// 
			// LocationBox
			// 
			this.LocationBox.Location = new System.Drawing.Point(88, 126);
			this.LocationBox.Name = "LocationBox";
			this.LocationBox.Size = new System.Drawing.Size(151, 23);
			this.LocationBox.TabIndex = 5;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(32, 126);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(50, 13);
			this.label4.TabIndex = 4;
			this.label4.Text = "Position :";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(41, 105);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(41, 13);
			this.label5.TabIndex = 3;
			this.label5.Text = "Tile id :";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(6, 48);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(76, 13);
			this.label3.TabIndex = 3;
			this.label3.Text = "Decoration id :";
			// 
			// TileIdBox
			// 
			this.TileIdBox.Location = new System.Drawing.Point(91, 103);
			this.TileIdBox.Maximum = new decimal(new int[] {
            9999999,
            0,
            0,
            0});
			this.TileIdBox.Name = "TileIdBox";
			this.TileIdBox.Size = new System.Drawing.Size(148, 20);
			this.TileIdBox.TabIndex = 2;
			this.TileIdBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.TileIdBox.ThousandsSeparator = true;
			this.TileIdBox.ValueChanged += new System.EventHandler(this.TileIdBox_ValueChanged);
			// 
			// numericUpDown1
			// 
			this.numericUpDown1.Location = new System.Drawing.Point(91, 46);
			this.numericUpDown1.Maximum = new decimal(new int[] {
            9999999,
            0,
            0,
            0});
			this.numericUpDown1.Name = "numericUpDown1";
			this.numericUpDown1.Size = new System.Drawing.Size(148, 20);
			this.numericUpDown1.TabIndex = 2;
			this.numericUpDown1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.numericUpDown1.ThousandsSeparator = true;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(38, 22);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(44, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "Tileset :";
			// 
			// TilesetBox
			// 
			this.TilesetBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.TilesetBox.FormattingEnabled = true;
			this.TilesetBox.Location = new System.Drawing.Point(91, 19);
			this.TilesetBox.Name = "TilesetBox";
			this.TilesetBox.Size = new System.Drawing.Size(148, 21);
			this.TilesetBox.Sorted = true;
			this.TilesetBox.TabIndex = 0;
			this.TilesetBox.SelectedIndexChanged += new System.EventHandler(this.TilesetBox_SelectedIndexChanged);
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.ViewPositionBox);
			this.groupBox3.Location = new System.Drawing.Point(12, 12);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(245, 157);
			this.groupBox3.TabIndex = 4;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "View point :";
			// 
			// ViewPositionBox
			// 
			this.ViewPositionBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ViewPositionBox.Location = new System.Drawing.Point(3, 16);
			this.ViewPositionBox.Name = "ViewPositionBox";
			this.ViewPositionBox.Position = DungeonEye.ViewFieldPosition.Team;
			this.ViewPositionBox.Size = new System.Drawing.Size(239, 138);
			this.ViewPositionBox.TabIndex = 0;
			// 
			// DrawTimer
			// 
			this.DrawTimer.Interval = 66;
			this.DrawTimer.Tick += new System.EventHandler(this.DrawTimer_Tick);
			// 
			// DecorationSetForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1129, 673);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Name = "DecorationSetForm";
			this.Text = "DecorationForm";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.DecorationForm_FormClosed);
			this.Load += new System.EventHandler(this.DecorationForm_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.TileIdBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
			this.groupBox3.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private OpenTK.GLControl OpenGLBox;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.GroupBox groupBox3;
		private ViewPositionControl ViewPositionBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox BackgroundTileSetBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox TilesetBox;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.NumericUpDown numericUpDown1;
		private System.Windows.Forms.Label LocationBox;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.NumericUpDown TileIdBox;
		private System.Windows.Forms.CheckBox DisplayWallBox;
		private System.Windows.Forms.Timer DrawTimer;
	}
}