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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.InterfaceNameBox = new System.Windows.Forms.ComboBox();
			this.ScriptNameBox = new System.Windows.Forms.ComboBox();
			this.ExperienceBox = new System.Windows.Forms.NumericUpDown();
			this.label3 = new System.Windows.Forms.Label();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.VisualTab = new System.Windows.Forms.TabPage();
			this.EntityTab = new System.Windows.Forms.TabPage();
			this.EntityBox = new DungeonEye.Forms.EntityControl();
			this.PropertiesTab = new System.Windows.Forms.TabPage();
			this.DamageBox = new DungeonEye.Forms.DiceForm();
			this.ArmorClassBox = new System.Windows.Forms.NumericUpDown();
			this.label4 = new System.Windows.Forms.Label();
			this.VisualGroupBox.SuspendLayout();
			this.PocketGroupBox.SuspendLayout();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.ExperienceBox)).BeginInit();
			this.tabControl1.SuspendLayout();
			this.VisualTab.SuspendLayout();
			this.EntityTab.SuspendLayout();
			this.PropertiesTab.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.ArmorClassBox)).BeginInit();
			this.SuspendLayout();
			// 
			// VisualGroupBox
			// 
			this.VisualGroupBox.Controls.Add(this.label11);
			this.VisualGroupBox.Controls.Add(this.label12);
			this.VisualGroupBox.Controls.Add(this.GlControl);
			this.VisualGroupBox.Controls.Add(this.TileIDBox);
			this.VisualGroupBox.Controls.Add(this.TileSetBox);
			this.VisualGroupBox.Location = new System.Drawing.Point(6, 6);
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
			this.PocketGroupBox.Location = new System.Drawing.Point(240, 3);
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
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.InterfaceNameBox);
			this.groupBox1.Controls.Add(this.ScriptNameBox);
			this.groupBox1.Location = new System.Drawing.Point(240, 181);
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
			// ExperienceBox
			// 
			this.ExperienceBox.Location = new System.Drawing.Point(115, 109);
			this.ExperienceBox.Maximum = new decimal(new int[] {
            1410065408,
            2,
            0,
            0});
			this.ExperienceBox.Name = "ExperienceBox";
			this.ExperienceBox.Size = new System.Drawing.Size(99, 20);
			this.ExperienceBox.TabIndex = 11;
			this.ExperienceBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.ExperienceBox.ThousandsSeparator = true;
			this.ExperienceBox.ValueChanged += new System.EventHandler(this.ExperienceBox_ValueChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(12, 111);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(97, 13);
			this.label3.TabIndex = 12;
			this.label3.Text = "Experience points :";
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.VisualTab);
			this.tabControl1.Controls.Add(this.EntityTab);
			this.tabControl1.Controls.Add(this.PropertiesTab);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(539, 455);
			this.tabControl1.TabIndex = 13;
			// 
			// VisualTab
			// 
			this.VisualTab.Controls.Add(this.VisualGroupBox);
			this.VisualTab.Location = new System.Drawing.Point(4, 22);
			this.VisualTab.Name = "VisualTab";
			this.VisualTab.Padding = new System.Windows.Forms.Padding(3);
			this.VisualTab.Size = new System.Drawing.Size(531, 429);
			this.VisualTab.TabIndex = 0;
			this.VisualTab.Text = "Visual";
			this.VisualTab.UseVisualStyleBackColor = true;
			// 
			// EntityTab
			// 
			this.EntityTab.Controls.Add(this.EntityBox);
			this.EntityTab.Location = new System.Drawing.Point(4, 22);
			this.EntityTab.Name = "EntityTab";
			this.EntityTab.Padding = new System.Windows.Forms.Padding(3);
			this.EntityTab.Size = new System.Drawing.Size(531, 429);
			this.EntityTab.TabIndex = 1;
			this.EntityTab.Text = "Entity";
			this.EntityTab.UseVisualStyleBackColor = true;
			// 
			// EntityBox
			// 
			this.EntityBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.EntityBox.Entity = null;
			this.EntityBox.Location = new System.Drawing.Point(3, 3);
			this.EntityBox.Name = "EntityBox";
			this.EntityBox.Size = new System.Drawing.Size(525, 423);
			this.EntityBox.TabIndex = 0;
			// 
			// PropertiesTab
			// 
			this.PropertiesTab.Controls.Add(this.label4);
			this.PropertiesTab.Controls.Add(this.label3);
			this.PropertiesTab.Controls.Add(this.PocketGroupBox);
			this.PropertiesTab.Controls.Add(this.ArmorClassBox);
			this.PropertiesTab.Controls.Add(this.ExperienceBox);
			this.PropertiesTab.Controls.Add(this.groupBox1);
			this.PropertiesTab.Controls.Add(this.DamageBox);
			this.PropertiesTab.Location = new System.Drawing.Point(4, 22);
			this.PropertiesTab.Name = "PropertiesTab";
			this.PropertiesTab.Size = new System.Drawing.Size(531, 429);
			this.PropertiesTab.TabIndex = 2;
			this.PropertiesTab.Text = "Properties";
			this.PropertiesTab.UseVisualStyleBackColor = true;
			// 
			// DamageBox
			// 
			this.DamageBox.ControlText = "Damage :";
			dice1.Faces = 1;
			dice1.Modifier = 0;
			dice1.Throws = 1;
			this.DamageBox.Dice = dice1;
			this.DamageBox.Location = new System.Drawing.Point(4, 3);
			this.DamageBox.MinimumSize = new System.Drawing.Size(225, 100);
			this.DamageBox.Name = "DamageBox";
			this.DamageBox.Size = new System.Drawing.Size(230, 100);
			this.DamageBox.TabIndex = 10;
			this.DamageBox.ValueChanged += new System.EventHandler(this.DamageBox_ValueChanged);
			// 
			// ArmorClassBox
			// 
			this.ArmorClassBox.Location = new System.Drawing.Point(115, 135);
			this.ArmorClassBox.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.ArmorClassBox.Name = "ArmorClassBox";
			this.ArmorClassBox.Size = new System.Drawing.Size(99, 20);
			this.ArmorClassBox.TabIndex = 11;
			this.ArmorClassBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.ArmorClassBox.ThousandsSeparator = true;
			this.ArmorClassBox.ValueChanged += new System.EventHandler(this.ArmorClassBox_ValueChanged);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(12, 137);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(68, 13);
			this.label4.TabIndex = 12;
			this.label4.Text = "Armor Class :";
			// 
			// MonsterControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tabControl1);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "MonsterControl";
			this.Size = new System.Drawing.Size(539, 455);
			this.Load += new System.EventHandler(this.MonsterControl_Load);
			this.VisualGroupBox.ResumeLayout(false);
			this.VisualGroupBox.PerformLayout();
			this.PocketGroupBox.ResumeLayout(false);
			this.PocketGroupBox.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.ExperienceBox)).EndInit();
			this.tabControl1.ResumeLayout(false);
			this.VisualTab.ResumeLayout(false);
			this.EntityTab.ResumeLayout(false);
			this.PropertiesTab.ResumeLayout(false);
			this.PropertiesTab.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.ArmorClassBox)).EndInit();
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
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox InterfaceNameBox;
		private System.Windows.Forms.ComboBox ScriptNameBox;
		private DiceForm DamageBox;
		private System.Windows.Forms.NumericUpDown ExperienceBox;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage VisualTab;
		private System.Windows.Forms.TabPage EntityTab;
		private System.Windows.Forms.TabPage PropertiesTab;
		private EntityControl EntityBox;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.NumericUpDown ArmorClassBox;
	}
}
