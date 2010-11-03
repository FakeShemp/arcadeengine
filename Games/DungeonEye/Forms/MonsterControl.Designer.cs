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
			DungeonEye.Dice dice2 = new DungeonEye.Dice();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MonsterControl));
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
			this.XPRewardBox = new System.Windows.Forms.NumericUpDown();
			this.label3 = new System.Windows.Forms.Label();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.VisualTab = new System.Windows.Forms.TabPage();
			this.AttributesTab = new System.Windows.Forms.TabPage();
			this.EntityBox = new DungeonEye.Forms.EntityControl();
			this.PropertiesTab = new System.Windows.Forms.TabPage();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.checkBox10 = new System.Windows.Forms.CheckBox();
			this.checkBox9 = new System.Windows.Forms.CheckBox();
			this.checkBox8 = new System.Windows.Forms.CheckBox();
			this.checkBox7 = new System.Windows.Forms.CheckBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
			this.ArmorClassBox = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
			this.checkBox6 = new System.Windows.Forms.CheckBox();
			this.checkBox4 = new System.Windows.Forms.CheckBox();
			this.checkBox3 = new System.Windows.Forms.CheckBox();
			this.checkBox2 = new System.Windows.Forms.CheckBox();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.DamageBox = new DungeonEye.Forms.DiceControl();
			this.MagicTab = new System.Windows.Forms.TabPage();
			this.AudioTab = new System.Windows.Forms.TabPage();
			this.checkBox5 = new System.Windows.Forms.CheckBox();
			this.HasMagicBox = new System.Windows.Forms.GroupBox();
			this.HealMagicBox = new System.Windows.Forms.CheckBox();
			this.HasDrainMagicBox = new System.Windows.Forms.CheckBox();
			this.MagicIntelligenceBox = new System.Windows.Forms.NumericUpDown();
			this.label7 = new System.Windows.Forms.Label();
			this.CastingPowerBox = new System.Windows.Forms.NumericUpDown();
			this.label8 = new System.Windows.Forms.Label();
			this.KnownSpellsBox = new System.Windows.Forms.ListBox();
			this.AddMagicSpellBox = new System.Windows.Forms.Button();
			this.RemoveMagicSpellBox = new System.Windows.Forms.Button();
			this.label9 = new System.Windows.Forms.Label();
			this.AvailableSpellsBox = new System.Windows.Forms.ComboBox();
			this.ClearKnownSpellsBox = new System.Windows.Forms.Button();
			this.label10 = new System.Windows.Forms.Label();
			this.AttackSoundBox = new System.Windows.Forms.TextBox();
			this.MoveSoundBox = new System.Windows.Forms.TextBox();
			this.DeathSoundBox = new System.Windows.Forms.TextBox();
			this.HurtSoundBox = new System.Windows.Forms.TextBox();
			this.label13 = new System.Windows.Forms.Label();
			this.label14 = new System.Windows.Forms.Label();
			this.label15 = new System.Windows.Forms.Label();
			this.LoadAttackSoundBox = new System.Windows.Forms.Button();
			this.PlayAttackSoundBox = new System.Windows.Forms.Button();
			this.LoadMoveSoundBox = new System.Windows.Forms.Button();
			this.PlayMoveSoundBox = new System.Windows.Forms.Button();
			this.LoadDeathSoundBox = new System.Windows.Forms.Button();
			this.PlayDeathSoundBox = new System.Windows.Forms.Button();
			this.LoadHurtSoundBox = new System.Windows.Forms.Button();
			this.PlayHurtSoundBox = new System.Windows.Forms.Button();
			this.VisualGroupBox.SuspendLayout();
			this.PocketGroupBox.SuspendLayout();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize) (this.XPRewardBox)).BeginInit();
			this.tabControl1.SuspendLayout();
			this.VisualTab.SuspendLayout();
			this.AttributesTab.SuspendLayout();
			this.PropertiesTab.SuspendLayout();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize) (this.numericUpDown2)).BeginInit();
			((System.ComponentModel.ISupportInitialize) (this.ArmorClassBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize) (this.numericUpDown1)).BeginInit();
			this.MagicTab.SuspendLayout();
			this.AudioTab.SuspendLayout();
			this.HasMagicBox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize) (this.MagicIntelligenceBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize) (this.CastingPowerBox)).BeginInit();
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
			this.VisualGroupBox.Size = new System.Drawing.Size(309, 343);
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
			this.GlControl.Size = new System.Drawing.Size(297, 259);
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
			this.TileIDBox.Size = new System.Drawing.Size(239, 21);
			this.TileIDBox.TabIndex = 2;
			this.TileIDBox.SelectedIndexChanged += new System.EventHandler(this.TileIDBox_SelectedIndexChanged);
			// 
			// TileSetBox
			// 
			this.TileSetBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.TileSetBox.FormattingEnabled = true;
			this.TileSetBox.Location = new System.Drawing.Point(64, 22);
			this.TileSetBox.Name = "TileSetBox";
			this.TileSetBox.Size = new System.Drawing.Size(239, 21);
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
			this.PocketGroupBox.Location = new System.Drawing.Point(257, 2);
			this.PocketGroupBox.Name = "PocketGroupBox";
			this.PocketGroupBox.Size = new System.Drawing.Size(189, 172);
			this.PocketGroupBox.TabIndex = 7;
			this.PocketGroupBox.TabStop = false;
			this.PocketGroupBox.Text = "Items in pocket :";
			// 
			// PocketItemsBox
			// 
			this.PocketItemsBox.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
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
			this.RemovePocketItemBox.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
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
			this.groupBox1.Location = new System.Drawing.Point(257, 180);
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
			// XPRewardBox
			// 
			this.XPRewardBox.Location = new System.Drawing.Point(105, 135);
			this.XPRewardBox.Maximum = new decimal(new int[] {
            1410065408,
            2,
            0,
            0});
			this.XPRewardBox.Name = "XPRewardBox";
			this.XPRewardBox.Size = new System.Drawing.Size(73, 20);
			this.XPRewardBox.TabIndex = 11;
			this.XPRewardBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.XPRewardBox.ThousandsSeparator = true;
			this.XPRewardBox.ValueChanged += new System.EventHandler(this.ExperienceBox_ValueChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(6, 137);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(62, 13);
			this.label3.TabIndex = 12;
			this.label3.Text = "XP reward :";
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.VisualTab);
			this.tabControl1.Controls.Add(this.AttributesTab);
			this.tabControl1.Controls.Add(this.PropertiesTab);
			this.tabControl1.Controls.Add(this.MagicTab);
			this.tabControl1.Controls.Add(this.AudioTab);
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
			// AttributesTab
			// 
			this.AttributesTab.Controls.Add(this.EntityBox);
			this.AttributesTab.Location = new System.Drawing.Point(4, 22);
			this.AttributesTab.Name = "AttributesTab";
			this.AttributesTab.Padding = new System.Windows.Forms.Padding(3);
			this.AttributesTab.Size = new System.Drawing.Size(531, 429);
			this.AttributesTab.TabIndex = 1;
			this.AttributesTab.Text = "Attributes";
			this.AttributesTab.UseVisualStyleBackColor = true;
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
			this.PropertiesTab.Controls.Add(this.groupBox2);
			this.PropertiesTab.Controls.Add(this.PocketGroupBox);
			this.PropertiesTab.Controls.Add(this.groupBox1);
			this.PropertiesTab.Controls.Add(this.DamageBox);
			this.PropertiesTab.Location = new System.Drawing.Point(4, 22);
			this.PropertiesTab.Name = "PropertiesTab";
			this.PropertiesTab.Size = new System.Drawing.Size(531, 429);
			this.PropertiesTab.TabIndex = 2;
			this.PropertiesTab.Text = "Properties";
			this.PropertiesTab.UseVisualStyleBackColor = true;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.checkBox10);
			this.groupBox2.Controls.Add(this.checkBox9);
			this.groupBox2.Controls.Add(this.checkBox8);
			this.groupBox2.Controls.Add(this.checkBox7);
			this.groupBox2.Controls.Add(this.label6);
			this.groupBox2.Controls.Add(this.label4);
			this.groupBox2.Controls.Add(this.label5);
			this.groupBox2.Controls.Add(this.label3);
			this.groupBox2.Controls.Add(this.numericUpDown2);
			this.groupBox2.Controls.Add(this.ArmorClassBox);
			this.groupBox2.Controls.Add(this.numericUpDown1);
			this.groupBox2.Controls.Add(this.XPRewardBox);
			this.groupBox2.Controls.Add(this.checkBox6);
			this.groupBox2.Controls.Add(this.checkBox4);
			this.groupBox2.Controls.Add(this.checkBox3);
			this.groupBox2.Controls.Add(this.checkBox2);
			this.groupBox2.Controls.Add(this.checkBox1);
			this.groupBox2.Location = new System.Drawing.Point(4, 109);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(248, 249);
			this.groupBox2.TabIndex = 13;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Misc :";
			// 
			// checkBox10
			// 
			this.checkBox10.AutoSize = true;
			this.checkBox10.Location = new System.Drawing.Point(135, 200);
			this.checkBox10.Name = "checkBox10";
			this.checkBox10.Size = new System.Drawing.Size(66, 17);
			this.checkBox10.TabIndex = 14;
			this.checkBox10.Text = "Smart AI";
			this.checkBox10.UseVisualStyleBackColor = true;
			// 
			// checkBox9
			// 
			this.checkBox9.AutoSize = true;
			this.checkBox9.Location = new System.Drawing.Point(135, 223);
			this.checkBox9.Name = "checkBox9";
			this.checkBox9.Size = new System.Drawing.Size(83, 17);
			this.checkBox9.TabIndex = 13;
			this.checkBox9.Text = "Can teleport";
			this.checkBox9.UseVisualStyleBackColor = true;
			// 
			// checkBox8
			// 
			this.checkBox8.AutoSize = true;
			this.checkBox8.Location = new System.Drawing.Point(9, 223);
			this.checkBox8.Name = "checkBox8";
			this.checkBox8.Size = new System.Drawing.Size(53, 17);
			this.checkBox8.TabIndex = 13;
			this.checkBox8.Text = "Flying";
			this.checkBox8.UseVisualStyleBackColor = true;
			// 
			// checkBox7
			// 
			this.checkBox7.AutoSize = true;
			this.checkBox7.Location = new System.Drawing.Point(9, 200);
			this.checkBox7.Name = "checkBox7";
			this.checkBox7.Size = new System.Drawing.Size(72, 17);
			this.checkBox7.TabIndex = 13;
			this.checkBox7.Text = "Use stairs";
			this.checkBox7.UseVisualStyleBackColor = true;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(6, 111);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(69, 13);
			this.label6.TabIndex = 5;
			this.label6.Text = "Steals items :";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(6, 163);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(68, 13);
			this.label4.TabIndex = 12;
			this.label4.Text = "Armor Class :";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(6, 87);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(96, 13);
			this.label5.TabIndex = 5;
			this.label5.Text = "Pickup floor items :";
			// 
			// numericUpDown2
			// 
			this.numericUpDown2.Location = new System.Drawing.Point(105, 109);
			this.numericUpDown2.Name = "numericUpDown2";
			this.numericUpDown2.Size = new System.Drawing.Size(73, 20);
			this.numericUpDown2.TabIndex = 4;
			this.numericUpDown2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// ArmorClassBox
			// 
			this.ArmorClassBox.Location = new System.Drawing.Point(105, 161);
			this.ArmorClassBox.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.ArmorClassBox.Name = "ArmorClassBox";
			this.ArmorClassBox.Size = new System.Drawing.Size(73, 20);
			this.ArmorClassBox.TabIndex = 11;
			this.ArmorClassBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.ArmorClassBox.ThousandsSeparator = true;
			this.ArmorClassBox.ValueChanged += new System.EventHandler(this.ArmorClassBox_ValueChanged);
			// 
			// numericUpDown1
			// 
			this.numericUpDown1.Location = new System.Drawing.Point(105, 85);
			this.numericUpDown1.Name = "numericUpDown1";
			this.numericUpDown1.Size = new System.Drawing.Size(73, 20);
			this.numericUpDown1.TabIndex = 4;
			this.numericUpDown1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// checkBox6
			// 
			this.checkBox6.AutoSize = true;
			this.checkBox6.Location = new System.Drawing.Point(135, 62);
			this.checkBox6.Name = "checkBox6";
			this.checkBox6.Size = new System.Drawing.Size(102, 17);
			this.checkBox6.TabIndex = 3;
			this.checkBox6.Text = "Throw weapons";
			this.checkBox6.UseVisualStyleBackColor = true;
			// 
			// checkBox4
			// 
			this.checkBox4.AutoSize = true;
			this.checkBox4.Location = new System.Drawing.Point(135, 39);
			this.checkBox4.Name = "checkBox4";
			this.checkBox4.Size = new System.Drawing.Size(101, 17);
			this.checkBox4.TabIndex = 3;
			this.checkBox4.Text = "Poison immunity";
			this.checkBox4.UseVisualStyleBackColor = true;
			// 
			// checkBox3
			// 
			this.checkBox3.AutoSize = true;
			this.checkBox3.Location = new System.Drawing.Point(6, 62);
			this.checkBox3.Name = "checkBox3";
			this.checkBox3.Size = new System.Drawing.Size(85, 17);
			this.checkBox3.TabIndex = 2;
			this.checkBox3.Text = "Non-material";
			this.checkBox3.UseVisualStyleBackColor = true;
			// 
			// checkBox2
			// 
			this.checkBox2.AutoSize = true;
			this.checkBox2.Location = new System.Drawing.Point(6, 39);
			this.checkBox2.Name = "checkBox2";
			this.checkBox2.Size = new System.Drawing.Size(73, 17);
			this.checkBox2.TabIndex = 1;
			this.checkBox2.Text = "Fill square";
			this.checkBox2.UseVisualStyleBackColor = true;
			// 
			// checkBox1
			// 
			this.checkBox1.AutoSize = true;
			this.checkBox1.Location = new System.Drawing.Point(6, 19);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(123, 17);
			this.checkBox1.TabIndex = 0;
			this.checkBox1.Text = "Flees after an attack";
			this.checkBox1.UseVisualStyleBackColor = true;
			// 
			// DamageBox
			// 
			this.DamageBox.ControlText = "Damage :";
			dice2.Faces = 1;
			dice2.Modifier = 0;
			dice2.Throws = 1;
			this.DamageBox.Dice = dice2;
			this.DamageBox.Location = new System.Drawing.Point(4, 3);
			this.DamageBox.MinimumSize = new System.Drawing.Size(225, 100);
			this.DamageBox.Name = "DamageBox";
			this.DamageBox.Size = new System.Drawing.Size(247, 100);
			this.DamageBox.TabIndex = 10;
			this.DamageBox.ValueChanged += new System.EventHandler(this.DamageBox_ValueChanged);
			// 
			// MagicTab
			// 
			this.MagicTab.Controls.Add(this.HasMagicBox);
			this.MagicTab.Location = new System.Drawing.Point(4, 22);
			this.MagicTab.Name = "MagicTab";
			this.MagicTab.Size = new System.Drawing.Size(531, 429);
			this.MagicTab.TabIndex = 4;
			this.MagicTab.Text = "Magic";
			this.MagicTab.UseVisualStyleBackColor = true;
			// 
			// AudioTab
			// 
			this.AudioTab.Controls.Add(this.PlayHurtSoundBox);
			this.AudioTab.Controls.Add(this.PlayDeathSoundBox);
			this.AudioTab.Controls.Add(this.PlayMoveSoundBox);
			this.AudioTab.Controls.Add(this.PlayAttackSoundBox);
			this.AudioTab.Controls.Add(this.LoadHurtSoundBox);
			this.AudioTab.Controls.Add(this.LoadDeathSoundBox);
			this.AudioTab.Controls.Add(this.LoadMoveSoundBox);
			this.AudioTab.Controls.Add(this.LoadAttackSoundBox);
			this.AudioTab.Controls.Add(this.HurtSoundBox);
			this.AudioTab.Controls.Add(this.DeathSoundBox);
			this.AudioTab.Controls.Add(this.MoveSoundBox);
			this.AudioTab.Controls.Add(this.AttackSoundBox);
			this.AudioTab.Controls.Add(this.label15);
			this.AudioTab.Controls.Add(this.label14);
			this.AudioTab.Controls.Add(this.label13);
			this.AudioTab.Controls.Add(this.label10);
			this.AudioTab.Location = new System.Drawing.Point(4, 22);
			this.AudioTab.Name = "AudioTab";
			this.AudioTab.Size = new System.Drawing.Size(531, 429);
			this.AudioTab.TabIndex = 3;
			this.AudioTab.Text = "Audio";
			this.AudioTab.UseVisualStyleBackColor = true;
			// 
			// checkBox5
			// 
			this.checkBox5.AutoSize = true;
			this.checkBox5.Location = new System.Drawing.Point(8, 0);
			this.checkBox5.Name = "checkBox5";
			this.checkBox5.Size = new System.Drawing.Size(77, 17);
			this.checkBox5.TabIndex = 4;
			this.checkBox5.Text = "Has Magic";
			this.checkBox5.UseVisualStyleBackColor = true;
			// 
			// HasMagicBox
			// 
			this.HasMagicBox.Controls.Add(this.AvailableSpellsBox);
			this.HasMagicBox.Controls.Add(this.label9);
			this.HasMagicBox.Controls.Add(this.ClearKnownSpellsBox);
			this.HasMagicBox.Controls.Add(this.RemoveMagicSpellBox);
			this.HasMagicBox.Controls.Add(this.AddMagicSpellBox);
			this.HasMagicBox.Controls.Add(this.KnownSpellsBox);
			this.HasMagicBox.Controls.Add(this.label8);
			this.HasMagicBox.Controls.Add(this.CastingPowerBox);
			this.HasMagicBox.Controls.Add(this.label7);
			this.HasMagicBox.Controls.Add(this.MagicIntelligenceBox);
			this.HasMagicBox.Controls.Add(this.HasDrainMagicBox);
			this.HasMagicBox.Controls.Add(this.HealMagicBox);
			this.HasMagicBox.Controls.Add(this.checkBox5);
			this.HasMagicBox.Location = new System.Drawing.Point(3, 3);
			this.HasMagicBox.Name = "HasMagicBox";
			this.HasMagicBox.Size = new System.Drawing.Size(316, 212);
			this.HasMagicBox.TabIndex = 5;
			this.HasMagicBox.TabStop = false;
			this.HasMagicBox.Text = "                         ";
			// 
			// HealMagicBox
			// 
			this.HealMagicBox.AutoSize = true;
			this.HealMagicBox.Location = new System.Drawing.Point(8, 24);
			this.HealMagicBox.Name = "HealMagicBox";
			this.HealMagicBox.Size = new System.Drawing.Size(99, 17);
			this.HealMagicBox.TabIndex = 5;
			this.HealMagicBox.Text = "Has heal magic";
			this.HealMagicBox.UseVisualStyleBackColor = true;
			// 
			// HasDrainMagicBox
			// 
			this.HasDrainMagicBox.AutoSize = true;
			this.HasDrainMagicBox.Location = new System.Drawing.Point(8, 48);
			this.HasDrainMagicBox.Name = "HasDrainMagicBox";
			this.HasDrainMagicBox.Size = new System.Drawing.Size(102, 17);
			this.HasDrainMagicBox.TabIndex = 6;
			this.HasDrainMagicBox.Text = "Has drain magic";
			this.HasDrainMagicBox.UseVisualStyleBackColor = true;
			// 
			// MagicIntelligenceBox
			// 
			this.MagicIntelligenceBox.Location = new System.Drawing.Point(234, 23);
			this.MagicIntelligenceBox.Name = "MagicIntelligenceBox";
			this.MagicIntelligenceBox.Size = new System.Drawing.Size(61, 20);
			this.MagicIntelligenceBox.TabIndex = 8;
			this.MagicIntelligenceBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(161, 25);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(67, 13);
			this.label7.TabIndex = 9;
			this.label7.Text = "Intelligence :";
			// 
			// CastingPowerBox
			// 
			this.CastingPowerBox.Location = new System.Drawing.Point(234, 49);
			this.CastingPowerBox.Name = "CastingPowerBox";
			this.CastingPowerBox.Size = new System.Drawing.Size(61, 20);
			this.CastingPowerBox.TabIndex = 8;
			this.CastingPowerBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(148, 51);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(80, 13);
			this.label8.TabIndex = 9;
			this.label8.Text = "Casting power :";
			// 
			// KnownSpellsBox
			// 
			this.KnownSpellsBox.FormattingEnabled = true;
			this.KnownSpellsBox.Location = new System.Drawing.Point(8, 126);
			this.KnownSpellsBox.Name = "KnownSpellsBox";
			this.KnownSpellsBox.Size = new System.Drawing.Size(216, 69);
			this.KnownSpellsBox.TabIndex = 10;
			// 
			// AddMagicSpellBox
			// 
			this.AddMagicSpellBox.Location = new System.Drawing.Point(234, 97);
			this.AddMagicSpellBox.Name = "AddMagicSpellBox";
			this.AddMagicSpellBox.Size = new System.Drawing.Size(75, 23);
			this.AddMagicSpellBox.TabIndex = 11;
			this.AddMagicSpellBox.Text = "Add";
			this.AddMagicSpellBox.UseVisualStyleBackColor = true;
			// 
			// RemoveMagicSpellBox
			// 
			this.RemoveMagicSpellBox.Location = new System.Drawing.Point(234, 128);
			this.RemoveMagicSpellBox.Name = "RemoveMagicSpellBox";
			this.RemoveMagicSpellBox.Size = new System.Drawing.Size(75, 23);
			this.RemoveMagicSpellBox.TabIndex = 11;
			this.RemoveMagicSpellBox.Text = "Remove";
			this.RemoveMagicSpellBox.UseVisualStyleBackColor = true;
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(5, 83);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(75, 13);
			this.label9.TabIndex = 12;
			this.label9.Text = "Known spells :";
			// 
			// AvailableSpellsBox
			// 
			this.AvailableSpellsBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.AvailableSpellsBox.FormattingEnabled = true;
			this.AvailableSpellsBox.Location = new System.Drawing.Point(8, 99);
			this.AvailableSpellsBox.Name = "AvailableSpellsBox";
			this.AvailableSpellsBox.Size = new System.Drawing.Size(215, 21);
			this.AvailableSpellsBox.TabIndex = 13;
			// 
			// ClearKnownSpellsBox
			// 
			this.ClearKnownSpellsBox.Location = new System.Drawing.Point(234, 172);
			this.ClearKnownSpellsBox.Name = "ClearKnownSpellsBox";
			this.ClearKnownSpellsBox.Size = new System.Drawing.Size(75, 23);
			this.ClearKnownSpellsBox.TabIndex = 11;
			this.ClearKnownSpellsBox.Text = "Clear";
			this.ClearKnownSpellsBox.UseVisualStyleBackColor = true;
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(7, 55);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(40, 13);
			this.label10.TabIndex = 0;
			this.label10.Text = "Move :";
			// 
			// AttackSoundBox
			// 
			this.AttackSoundBox.Location = new System.Drawing.Point(53, 26);
			this.AttackSoundBox.Name = "AttackSoundBox";
			this.AttackSoundBox.Size = new System.Drawing.Size(210, 20);
			this.AttackSoundBox.TabIndex = 1;
			// 
			// MoveSoundBox
			// 
			this.MoveSoundBox.Location = new System.Drawing.Point(53, 52);
			this.MoveSoundBox.Name = "MoveSoundBox";
			this.MoveSoundBox.Size = new System.Drawing.Size(210, 20);
			this.MoveSoundBox.TabIndex = 1;
			// 
			// DeathSoundBox
			// 
			this.DeathSoundBox.Location = new System.Drawing.Point(53, 78);
			this.DeathSoundBox.Name = "DeathSoundBox";
			this.DeathSoundBox.Size = new System.Drawing.Size(210, 20);
			this.DeathSoundBox.TabIndex = 1;
			// 
			// HurtSoundBox
			// 
			this.HurtSoundBox.Location = new System.Drawing.Point(53, 104);
			this.HurtSoundBox.Name = "HurtSoundBox";
			this.HurtSoundBox.Size = new System.Drawing.Size(210, 20);
			this.HurtSoundBox.TabIndex = 1;
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.Location = new System.Drawing.Point(3, 29);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(44, 13);
			this.label13.TabIndex = 0;
			this.label13.Text = "Attack :";
			// 
			// label14
			// 
			this.label14.AutoSize = true;
			this.label14.Location = new System.Drawing.Point(14, 107);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(33, 13);
			this.label14.TabIndex = 0;
			this.label14.Text = "Hurt :";
			// 
			// label15
			// 
			this.label15.AutoSize = true;
			this.label15.Location = new System.Drawing.Point(5, 81);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(42, 13);
			this.label15.TabIndex = 0;
			this.label15.Text = "Death :";
			// 
			// LoadAttackSoundBox
			// 
			this.LoadAttackSoundBox.Location = new System.Drawing.Point(269, 24);
			this.LoadAttackSoundBox.Name = "LoadAttackSoundBox";
			this.LoadAttackSoundBox.Size = new System.Drawing.Size(26, 23);
			this.LoadAttackSoundBox.TabIndex = 2;
			this.LoadAttackSoundBox.Text = "...";
			this.LoadAttackSoundBox.UseVisualStyleBackColor = true;
			// 
			// PlayAttackSoundBox
			// 
			this.PlayAttackSoundBox.Image = ((System.Drawing.Image) (resources.GetObject("PlayAttackSoundBox.Image")));
			this.PlayAttackSoundBox.Location = new System.Drawing.Point(301, 24);
			this.PlayAttackSoundBox.Name = "PlayAttackSoundBox";
			this.PlayAttackSoundBox.Size = new System.Drawing.Size(75, 23);
			this.PlayAttackSoundBox.TabIndex = 3;
			this.PlayAttackSoundBox.UseVisualStyleBackColor = true;
			// 
			// LoadMoveSoundBox
			// 
			this.LoadMoveSoundBox.Location = new System.Drawing.Point(269, 50);
			this.LoadMoveSoundBox.Name = "LoadMoveSoundBox";
			this.LoadMoveSoundBox.Size = new System.Drawing.Size(26, 23);
			this.LoadMoveSoundBox.TabIndex = 2;
			this.LoadMoveSoundBox.Text = "...";
			this.LoadMoveSoundBox.UseVisualStyleBackColor = true;
			// 
			// PlayMoveSoundBox
			// 
			this.PlayMoveSoundBox.Image = ((System.Drawing.Image) (resources.GetObject("PlayMoveSoundBox.Image")));
			this.PlayMoveSoundBox.Location = new System.Drawing.Point(301, 50);
			this.PlayMoveSoundBox.Name = "PlayMoveSoundBox";
			this.PlayMoveSoundBox.Size = new System.Drawing.Size(75, 23);
			this.PlayMoveSoundBox.TabIndex = 3;
			this.PlayMoveSoundBox.UseVisualStyleBackColor = true;
			// 
			// LoadDeathSoundBox
			// 
			this.LoadDeathSoundBox.Location = new System.Drawing.Point(269, 76);
			this.LoadDeathSoundBox.Name = "LoadDeathSoundBox";
			this.LoadDeathSoundBox.Size = new System.Drawing.Size(26, 23);
			this.LoadDeathSoundBox.TabIndex = 2;
			this.LoadDeathSoundBox.Text = "...";
			this.LoadDeathSoundBox.UseVisualStyleBackColor = true;
			// 
			// PlayDeathSoundBox
			// 
			this.PlayDeathSoundBox.Image = ((System.Drawing.Image) (resources.GetObject("PlayDeathSoundBox.Image")));
			this.PlayDeathSoundBox.Location = new System.Drawing.Point(301, 76);
			this.PlayDeathSoundBox.Name = "PlayDeathSoundBox";
			this.PlayDeathSoundBox.Size = new System.Drawing.Size(75, 23);
			this.PlayDeathSoundBox.TabIndex = 3;
			this.PlayDeathSoundBox.UseVisualStyleBackColor = true;
			// 
			// LoadHurtSoundBox
			// 
			this.LoadHurtSoundBox.Location = new System.Drawing.Point(269, 102);
			this.LoadHurtSoundBox.Name = "LoadHurtSoundBox";
			this.LoadHurtSoundBox.Size = new System.Drawing.Size(26, 23);
			this.LoadHurtSoundBox.TabIndex = 2;
			this.LoadHurtSoundBox.Text = "...";
			this.LoadHurtSoundBox.UseVisualStyleBackColor = true;
			// 
			// PlayHurtSoundBox
			// 
			this.PlayHurtSoundBox.Image = ((System.Drawing.Image) (resources.GetObject("PlayHurtSoundBox.Image")));
			this.PlayHurtSoundBox.Location = new System.Drawing.Point(301, 102);
			this.PlayHurtSoundBox.Name = "PlayHurtSoundBox";
			this.PlayHurtSoundBox.Size = new System.Drawing.Size(75, 23);
			this.PlayHurtSoundBox.TabIndex = 3;
			this.PlayHurtSoundBox.UseVisualStyleBackColor = true;
			// 
			// MonsterControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tabControl1);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
			this.Name = "MonsterControl";
			this.Size = new System.Drawing.Size(539, 455);
			this.Load += new System.EventHandler(this.MonsterControl_Load);
			this.VisualGroupBox.ResumeLayout(false);
			this.VisualGroupBox.PerformLayout();
			this.PocketGroupBox.ResumeLayout(false);
			this.PocketGroupBox.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize) (this.XPRewardBox)).EndInit();
			this.tabControl1.ResumeLayout(false);
			this.VisualTab.ResumeLayout(false);
			this.AttributesTab.ResumeLayout(false);
			this.PropertiesTab.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			((System.ComponentModel.ISupportInitialize) (this.numericUpDown2)).EndInit();
			((System.ComponentModel.ISupportInitialize) (this.ArmorClassBox)).EndInit();
			((System.ComponentModel.ISupportInitialize) (this.numericUpDown1)).EndInit();
			this.MagicTab.ResumeLayout(false);
			this.AudioTab.ResumeLayout(false);
			this.AudioTab.PerformLayout();
			this.HasMagicBox.ResumeLayout(false);
			this.HasMagicBox.PerformLayout();
			((System.ComponentModel.ISupportInitialize) (this.MagicIntelligenceBox)).EndInit();
			((System.ComponentModel.ISupportInitialize) (this.CastingPowerBox)).EndInit();
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
		private DiceControl DamageBox;
		private System.Windows.Forms.NumericUpDown XPRewardBox;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage VisualTab;
		private System.Windows.Forms.TabPage AttributesTab;
		private System.Windows.Forms.TabPage PropertiesTab;
		private EntityControl EntityBox;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.NumericUpDown ArmorClassBox;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.NumericUpDown numericUpDown2;
		private System.Windows.Forms.NumericUpDown numericUpDown1;
		private System.Windows.Forms.CheckBox checkBox6;
		private System.Windows.Forms.CheckBox checkBox4;
		private System.Windows.Forms.CheckBox checkBox3;
		private System.Windows.Forms.CheckBox checkBox2;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.TabPage AudioTab;
		private System.Windows.Forms.CheckBox checkBox10;
		private System.Windows.Forms.CheckBox checkBox9;
		private System.Windows.Forms.CheckBox checkBox8;
		private System.Windows.Forms.CheckBox checkBox7;
		private System.Windows.Forms.TabPage MagicTab;
		private System.Windows.Forms.GroupBox HasMagicBox;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.NumericUpDown CastingPowerBox;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.NumericUpDown MagicIntelligenceBox;
		private System.Windows.Forms.CheckBox HasDrainMagicBox;
		private System.Windows.Forms.CheckBox HealMagicBox;
		private System.Windows.Forms.CheckBox checkBox5;
		private System.Windows.Forms.ComboBox AvailableSpellsBox;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Button ClearKnownSpellsBox;
		private System.Windows.Forms.Button RemoveMagicSpellBox;
		private System.Windows.Forms.Button AddMagicSpellBox;
		private System.Windows.Forms.ListBox KnownSpellsBox;
		private System.Windows.Forms.TextBox HurtSoundBox;
		private System.Windows.Forms.TextBox DeathSoundBox;
		private System.Windows.Forms.TextBox MoveSoundBox;
		private System.Windows.Forms.TextBox AttackSoundBox;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Button PlayHurtSoundBox;
		private System.Windows.Forms.Button PlayDeathSoundBox;
		private System.Windows.Forms.Button PlayMoveSoundBox;
		private System.Windows.Forms.Button PlayAttackSoundBox;
		private System.Windows.Forms.Button LoadHurtSoundBox;
		private System.Windows.Forms.Button LoadDeathSoundBox;
		private System.Windows.Forms.Button LoadMoveSoundBox;
		private System.Windows.Forms.Button LoadAttackSoundBox;
	}
}
