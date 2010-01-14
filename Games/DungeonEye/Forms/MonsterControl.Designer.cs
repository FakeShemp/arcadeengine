namespace DungeonEye.Forms
{
	partial class MonsterControl
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
			DungeonEye.Dice dice1 = new DungeonEye.Dice();
			this.VisualGroupBox = new System.Windows.Forms.GroupBox();
			this.label11 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.GlControl = new OpenTK.GLControl();
			this.TileIDBox = new System.Windows.Forms.ComboBox();
			this.TileSetBox = new System.Windows.Forms.ComboBox();
			this.PocketGroupBox = new System.Windows.Forms.GroupBox();
			this.PocketItemsBox = new System.Windows.Forms.ListBox();
			this.AddPocketItemBox = new System.Windows.Forms.Button();
			this.ItemsBox = new System.Windows.Forms.ComboBox();
			this.RemovePocketItemBox = new System.Windows.Forms.Button();
			this.groupBox6 = new System.Windows.Forms.GroupBox();
			this.label9 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.HPMaxBox = new System.Windows.Forms.NumericUpDown();
			this.HPActualBox = new System.Windows.Forms.NumericUpDown();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.InterfaceNameBox = new System.Windows.Forms.ComboBox();
			this.ScriptNameBox = new System.Windows.Forms.ComboBox();
			this.DamageBox = new DungeonEye.Forms.DiceForm();
			this.VisualGroupBox.SuspendLayout();
			this.PocketGroupBox.SuspendLayout();
			this.groupBox6.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.HPMaxBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.HPActualBox)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// VisualGroupBox
			// 
			this.VisualGroupBox.Controls.Add(this.label11);
			this.VisualGroupBox.Controls.Add(this.label12);
			this.VisualGroupBox.Controls.Add(this.GlControl);
			this.VisualGroupBox.Controls.Add(this.TileIDBox);
			this.VisualGroupBox.Controls.Add(this.TileSetBox);
			this.VisualGroupBox.Location = new System.Drawing.Point(0, 0);
			this.VisualGroupBox.Name = "VisualGroupBox";
			this.VisualGroupBox.Size = new System.Drawing.Size(232, 260);
			this.VisualGroupBox.TabIndex = 6;
			this.VisualGroupBox.TabStop = false;
			this.VisualGroupBox.Text = "Visual :";
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(12, 53);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(30, 13);
			this.label11.TabIndex = 3;
			this.label11.Text = "Tile :";
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Location = new System.Drawing.Point(12, 25);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(46, 13);
			this.label12.TabIndex = 3;
			this.label12.Text = "TileSet :";
			// 
			// GlControl
			// 
			this.GlControl.BackColor = System.Drawing.Color.Black;
			this.GlControl.Location = new System.Drawing.Point(6, 78);
			this.GlControl.Name = "GlControl";
			this.GlControl.Size = new System.Drawing.Size(220, 176);
			this.GlControl.TabIndex = 1;
			this.GlControl.VSync = true;
			this.GlControl.Load += new System.EventHandler(this.GlControl_Load);
			this.GlControl.Paint += new System.Windows.Forms.PaintEventHandler(this.GlControl_Paint);
			this.GlControl.Resize += new System.EventHandler(this.GlControl_Resize);
			// 
			// TileIDBox
			// 
			this.TileIDBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.TileIDBox.FormattingEnabled = true;
			this.TileIDBox.Location = new System.Drawing.Point(64, 50);
			this.TileIDBox.Name = "TileIDBox";
			this.TileIDBox.Size = new System.Drawing.Size(162, 21);
			this.TileIDBox.TabIndex = 2;
			this.TileIDBox.SelectedIndexChanged += new System.EventHandler(this.TileIDBox_SelectedIndexChanged);
			// 
			// TileSetBox
			// 
			this.TileSetBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.TileSetBox.FormattingEnabled = true;
			this.TileSetBox.Location = new System.Drawing.Point(64, 22);
			this.TileSetBox.Name = "TileSetBox";
			this.TileSetBox.Size = new System.Drawing.Size(162, 21);
			this.TileSetBox.Sorted = true;
			this.TileSetBox.TabIndex = 2;
			this.TileSetBox.SelectedIndexChanged += new System.EventHandler(this.TileSetBox_SelectedIndexChanged);
			// 
			// PocketGroupBox
			// 
			this.PocketGroupBox.Controls.Add(this.PocketItemsBox);
			this.PocketGroupBox.Controls.Add(this.AddPocketItemBox);
			this.PocketGroupBox.Controls.Add(this.ItemsBox);
			this.PocketGroupBox.Controls.Add(this.RemovePocketItemBox);
			this.PocketGroupBox.Location = new System.Drawing.Point(238, 3);
			this.PocketGroupBox.Name = "PocketGroupBox";
			this.PocketGroupBox.Size = new System.Drawing.Size(189, 172);
			this.PocketGroupBox.TabIndex = 7;
			this.PocketGroupBox.TabStop = false;
			this.PocketGroupBox.Text = "Items in pocket :";
			// 
			// PocketItemsBox
			// 
			this.PocketItemsBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
							| System.Windows.Forms.AnchorStyles.Left)
							| System.Windows.Forms.AnchorStyles.Right)));
			this.PocketItemsBox.FormattingEnabled = true;
			this.PocketItemsBox.Location = new System.Drawing.Point(6, 44);
			this.PocketItemsBox.Name = "PocketItemsBox";
			this.PocketItemsBox.Size = new System.Drawing.Size(177, 95);
			this.PocketItemsBox.Sorted = true;
			this.PocketItemsBox.TabIndex = 5;
			this.PocketItemsBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.PocketItemsBox_MouseDoubleClick);
			// 
			// AddPocketItemBox
			// 
			this.AddPocketItemBox.AutoSize = true;
			this.AddPocketItemBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.AddPocketItemBox.Location = new System.Drawing.Point(146, 19);
			this.AddPocketItemBox.Name = "AddPocketItemBox";
			this.AddPocketItemBox.Size = new System.Drawing.Size(36, 23);
			this.AddPocketItemBox.TabIndex = 4;
			this.AddPocketItemBox.Text = "Add";
			this.AddPocketItemBox.UseVisualStyleBackColor = true;
			this.AddPocketItemBox.Click += new System.EventHandler(this.AddPocketItemBox_Click);
			// 
			// ItemsBox
			// 
			this.ItemsBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ItemsBox.FormattingEnabled = true;
			this.ItemsBox.Location = new System.Drawing.Point(6, 19);
			this.ItemsBox.Name = "ItemsBox";
			this.ItemsBox.Size = new System.Drawing.Size(134, 21);
			this.ItemsBox.Sorted = true;
			this.ItemsBox.TabIndex = 3;
			// 
			// RemovePocketItemBox
			// 
			this.RemovePocketItemBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
							| System.Windows.Forms.AnchorStyles.Right)));
			this.RemovePocketItemBox.Location = new System.Drawing.Point(6, 143);
			this.RemovePocketItemBox.Name = "RemovePocketItemBox";
			this.RemovePocketItemBox.Size = new System.Drawing.Size(177, 23);
			this.RemovePocketItemBox.TabIndex = 2;
			this.RemovePocketItemBox.Text = "Remove";
			this.RemovePocketItemBox.UseVisualStyleBackColor = true;
			this.RemovePocketItemBox.Click += new System.EventHandler(this.RemovePocketItemBox_Click);
			// 
			// groupBox6
			// 
			this.groupBox6.Controls.Add(this.label9);
			this.groupBox6.Controls.Add(this.label10);
			this.groupBox6.Controls.Add(this.HPMaxBox);
			this.groupBox6.Controls.Add(this.HPActualBox);
			this.groupBox6.Location = new System.Drawing.Point(238, 267);
			this.groupBox6.Name = "groupBox6";
			this.groupBox6.Size = new System.Drawing.Size(189, 101);
			this.groupBox6.TabIndex = 8;
			this.groupBox6.TabStop = false;
			this.groupBox6.Text = "HP :";
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(7, 47);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(57, 13);
			this.label9.TabIndex = 1;
			this.label9.Text = "Maximum :";
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(7, 25);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(43, 13);
			this.label10.TabIndex = 1;
			this.label10.Text = "Actual :";
			// 
			// HPMaxBox
			// 
			this.HPMaxBox.Location = new System.Drawing.Point(86, 45);
			this.HPMaxBox.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
			this.HPMaxBox.Name = "HPMaxBox";
			this.HPMaxBox.Size = new System.Drawing.Size(54, 20);
			this.HPMaxBox.TabIndex = 0;
			this.HPMaxBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.HPMaxBox.ThousandsSeparator = true;
			this.HPMaxBox.ValueChanged += new System.EventHandler(this.HPMaxBox_ValueChanged);
			// 
			// HPActualBox
			// 
			this.HPActualBox.Location = new System.Drawing.Point(86, 19);
			this.HPActualBox.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
			this.HPActualBox.Name = "HPActualBox";
			this.HPActualBox.Size = new System.Drawing.Size(54, 20);
			this.HPActualBox.TabIndex = 0;
			this.HPActualBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.HPActualBox.ThousandsSeparator = true;
			this.HPActualBox.ValueChanged += new System.EventHandler(this.HPActualBox_ValueChanged);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.InterfaceNameBox);
			this.groupBox1.Controls.Add(this.ScriptNameBox);
			this.groupBox1.Location = new System.Drawing.Point(238, 181);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(189, 80);
			this.groupBox1.TabIndex = 9;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Script :";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6, 50);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(55, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "Interface :";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 23);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(40, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Script :";
			// 
			// InterfaceNameBox
			// 
			this.InterfaceNameBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.InterfaceNameBox.FormattingEnabled = true;
			this.InterfaceNameBox.Location = new System.Drawing.Point(67, 47);
			this.InterfaceNameBox.Name = "InterfaceNameBox";
			this.InterfaceNameBox.Size = new System.Drawing.Size(116, 21);
			this.InterfaceNameBox.TabIndex = 0;
			this.InterfaceNameBox.SelectedIndexChanged += new System.EventHandler(this.InterfaceNameBox_SelectedIndexChanged);
			// 
			// ScriptNameBox
			// 
			this.ScriptNameBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ScriptNameBox.FormattingEnabled = true;
			this.ScriptNameBox.Location = new System.Drawing.Point(67, 20);
			this.ScriptNameBox.Name = "ScriptNameBox";
			this.ScriptNameBox.Size = new System.Drawing.Size(116, 21);
			this.ScriptNameBox.TabIndex = 0;
			this.ScriptNameBox.SelectedIndexChanged += new System.EventHandler(this.ScriptNameBox_SelectedIndexChanged);
			// 
			// DamageBox
			// 
			this.DamageBox.ControlText = "Damage :";
			dice1.Modifier = 0;
			dice1.Faces = 1;
			dice1.Throws = 1;
			this.DamageBox.Dice = dice1;
			this.DamageBox.Location = new System.Drawing.Point(0, 268);
			this.DamageBox.MinimumSize = new System.Drawing.Size(225, 100);
			this.DamageBox.Name = "DamageBox";
			this.DamageBox.Size = new System.Drawing.Size(231, 100);
			this.DamageBox.TabIndex = 10;
			this.DamageBox.ValueChanged += new System.EventHandler(this.DamageBox_ValueChanged);
			// 
			// MonsterControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.DamageBox);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.groupBox6);
			this.Controls.Add(this.PocketGroupBox);
			this.Controls.Add(this.VisualGroupBox);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "MonsterControl";
			this.Size = new System.Drawing.Size(539, 455);
			this.Load += new System.EventHandler(this.MonsterControl_Load);
			this.VisualGroupBox.ResumeLayout(false);
			this.VisualGroupBox.PerformLayout();
			this.PocketGroupBox.ResumeLayout(false);
			this.PocketGroupBox.PerformLayout();
			this.groupBox6.ResumeLayout(false);
			this.groupBox6.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.HPMaxBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.HPActualBox)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox VisualGroupBox;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label12;
		private OpenTK.GLControl GlControl;
		private System.Windows.Forms.ComboBox TileIDBox;
		private System.Windows.Forms.ComboBox TileSetBox;
		private System.Windows.Forms.GroupBox PocketGroupBox;
		private System.Windows.Forms.ListBox PocketItemsBox;
		private System.Windows.Forms.Button AddPocketItemBox;
		private System.Windows.Forms.ComboBox ItemsBox;
		private System.Windows.Forms.Button RemovePocketItemBox;
		private System.Windows.Forms.GroupBox groupBox6;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.NumericUpDown HPMaxBox;
		private System.Windows.Forms.NumericUpDown HPActualBox;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox InterfaceNameBox;
		private System.Windows.Forms.ComboBox ScriptNameBox;
		private DiceForm DamageBox;
	}
}
