namespace DungeonEye.Forms
{
	partial class AlcoveControl
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
			this.GLControl = new OpenTK.GLControl();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.ClearBox = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.DecorationBox = new System.Windows.Forms.NumericUpDown();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.AcceptBigItemsBox = new System.Windows.Forms.CheckBox();
			this.HideItemsBox = new System.Windows.Forms.CheckBox();
			this.DirectionBox = new DungeonEye.Forms.CardinalPointControl();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.ItemsBox = new System.Windows.Forms.ComboBox();
			this.ItemLocationBox = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.DecorationBox)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.SuspendLayout();
			// 
			// GLControl
			// 
			this.GLControl.BackColor = System.Drawing.Color.Black;
			this.GLControl.Location = new System.Drawing.Point(6, 19);
			this.GLControl.Name = "GLControl";
			this.GLControl.Size = new System.Drawing.Size(352, 240);
			this.GLControl.TabIndex = 1;
			this.GLControl.VSync = false;
			this.GLControl.Load += new System.EventHandler(this.GLControl_Load);
			this.GLControl.Paint += new System.Windows.Forms.PaintEventHandler(this.GLControl_Paint);
			this.GLControl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.GLControl_MouseMove);
			this.GLControl.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.GLControl_PreviewKeyDown);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.GLControl);
			this.groupBox2.Location = new System.Drawing.Point(6, 19);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(365, 270);
			this.groupBox2.TabIndex = 2;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Visual properties";
			// 
			// ClearBox
			// 
			this.ClearBox.Location = new System.Drawing.Point(133, 67);
			this.ClearBox.Name = "ClearBox";
			this.ClearBox.Size = new System.Drawing.Size(48, 23);
			this.ClearBox.TabIndex = 4;
			this.ClearBox.Text = "Clear";
			this.ClearBox.UseVisualStyleBackColor = true;
			this.ClearBox.Click += new System.EventHandler(this.ClearBox_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(10, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(59, 13);
			this.label1.TabIndex = 3;
			this.label1.Text = "Decoration";
			// 
			// DecorationBox
			// 
			this.DecorationBox.Location = new System.Drawing.Point(75, 22);
			this.DecorationBox.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
			this.DecorationBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
			this.DecorationBox.Name = "DecorationBox";
			this.DecorationBox.Size = new System.Drawing.Size(67, 20);
			this.DecorationBox.TabIndex = 2;
			this.DecorationBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.DecorationBox.ThousandsSeparator = true;
			this.DecorationBox.Value = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
			this.DecorationBox.ValueChanged += new System.EventHandler(this.DecorationBox_ValueChanged);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.AcceptBigItemsBox);
			this.groupBox1.Controls.Add(this.HideItemsBox);
			this.groupBox1.Controls.Add(this.ClearBox);
			this.groupBox1.Controls.Add(this.DecorationBox);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Location = new System.Drawing.Point(6, 295);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(187, 99);
			this.groupBox1.TabIndex = 5;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Properties";
			// 
			// AcceptBigItemsBox
			// 
			this.AcceptBigItemsBox.AutoSize = true;
			this.AcceptBigItemsBox.Location = new System.Drawing.Point(6, 71);
			this.AcceptBigItemsBox.Name = "AcceptBigItemsBox";
			this.AcceptBigItemsBox.Size = new System.Drawing.Size(104, 17);
			this.AcceptBigItemsBox.TabIndex = 6;
			this.AcceptBigItemsBox.Text = "Accept big items";
			this.AcceptBigItemsBox.UseVisualStyleBackColor = true;
			this.AcceptBigItemsBox.CheckedChanged += new System.EventHandler(this.AcceptBigItemsBox_CheckedChanged);
			// 
			// HideItemsBox
			// 
			this.HideItemsBox.AutoSize = true;
			this.HideItemsBox.Location = new System.Drawing.Point(6, 48);
			this.HideItemsBox.Name = "HideItemsBox";
			this.HideItemsBox.Size = new System.Drawing.Size(75, 17);
			this.HideItemsBox.TabIndex = 5;
			this.HideItemsBox.Text = "Hide items";
			this.HideItemsBox.UseVisualStyleBackColor = true;
			this.HideItemsBox.CheckedChanged += new System.EventHandler(this.HideItemsBox_CheckedChanged);
			// 
			// DirectionBox
			// 
			this.DirectionBox.Direction = DungeonEye.CardinalPoint.North;
			this.DirectionBox.Location = new System.Drawing.Point(377, 19);
			this.DirectionBox.MinimumSize = new System.Drawing.Size(125, 115);
			this.DirectionBox.Name = "DirectionBox";
			this.DirectionBox.Size = new System.Drawing.Size(125, 115);
			this.DirectionBox.TabIndex = 4;
			this.DirectionBox.Title = "Side";
			this.DirectionBox.DirectionChanged += new DungeonEye.Forms.CardinalPointControl.ChangedEventHandler(this.DirectionBox_DirectionChanged);
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.ItemsBox);
			this.groupBox3.Controls.Add(this.ItemLocationBox);
			this.groupBox3.Controls.Add(this.label2);
			this.groupBox3.Location = new System.Drawing.Point(199, 295);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(172, 99);
			this.groupBox3.TabIndex = 6;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Preview";
			// 
			// ItemsBox
			// 
			this.ItemsBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ItemsBox.FormattingEnabled = true;
			this.ItemsBox.Location = new System.Drawing.Point(6, 19);
			this.ItemsBox.Name = "ItemsBox";
			this.ItemsBox.Size = new System.Drawing.Size(159, 21);
			this.ItemsBox.TabIndex = 2;
			this.ItemsBox.SelectedIndexChanged += new System.EventHandler(this.ItemsBox_SelectedIndexChanged);
			// 
			// ItemLocationBox
			// 
			this.ItemLocationBox.AutoSize = true;
			this.ItemLocationBox.Location = new System.Drawing.Point(63, 49);
			this.ItemLocationBox.Name = "ItemLocationBox";
			this.ItemLocationBox.Size = new System.Drawing.Size(52, 13);
			this.ItemLocationBox.TabIndex = 1;
			this.ItemLocationBox.Text = "position...";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(3, 49);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(54, 13);
			this.label2.TabIndex = 0;
			this.label2.Text = "Location :";
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.groupBox2);
			this.groupBox4.Controls.Add(this.groupBox3);
			this.groupBox4.Controls.Add(this.DirectionBox);
			this.groupBox4.Controls.Add(this.groupBox1);
			this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox4.Location = new System.Drawing.Point(0, 0);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(788, 651);
			this.groupBox4.TabIndex = 7;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Alcove";
			// 
			// AlcoveControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.groupBox4);
			this.Name = "AlcoveControl";
			this.Size = new System.Drawing.Size(788, 651);
			this.Load += new System.EventHandler(this.AlcoveControl_Load);
			this.groupBox2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.DecorationBox)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.groupBox4.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private OpenTK.GLControl GLControl;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.NumericUpDown DecorationBox;
		private System.Windows.Forms.Button ClearBox;
		private CardinalPointControl DirectionBox;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckBox HideItemsBox;
		private System.Windows.Forms.CheckBox AcceptBigItemsBox;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Label ItemLocationBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox ItemsBox;
		private System.Windows.Forms.GroupBox groupBox4;
	}
}
