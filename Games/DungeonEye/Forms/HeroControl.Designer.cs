namespace DungeonEye.Forms
{
	partial class HeroControl
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
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.HelmetBox = new System.Windows.Forms.ComboBox();
			this.label17 = new System.Windows.Forms.Label();
			this.label16 = new System.Windows.Forms.Label();
			this.label15 = new System.Windows.Forms.Label();
			this.label14 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.NeckBox = new System.Windows.Forms.ComboBox();
			this.FeetBox = new System.Windows.Forms.ComboBox();
			this.SecondaryBox = new System.Windows.Forms.ComboBox();
			this.PrimaryBox = new System.Windows.Forms.ComboBox();
			this.RightRingBox = new System.Windows.Forms.ComboBox();
			this.LeftRingBox = new System.Windows.Forms.ComboBox();
			this.WristBox = new System.Windows.Forms.ComboBox();
			this.ArmorBox = new System.Windows.Forms.ComboBox();
			this.label8 = new System.Windows.Forms.Label();
			this.QuiverBox = new System.Windows.Forms.NumericUpDown();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.PropertiesTab = new System.Windows.Forms.TabPage();
			this.hitPointControl1 = new DungeonEye.Forms.HitPointControl();
			this.ProfessionTab = new System.Windows.Forms.TabPage();
			this.ProfessionBox3 = new DungeonEye.Forms.ProfessionControl();
			this.ProfessionBox2 = new DungeonEye.Forms.ProfessionControl();
			this.ProfessionBox1 = new DungeonEye.Forms.ProfessionControl();
			this.SpellTab = new System.Windows.Forms.TabPage();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.UncheckAllLearnedBox = new System.Windows.Forms.Button();
			this.CheckAllLearnedBox = new System.Windows.Forms.Button();
			this.LearnedSpellBox = new System.Windows.Forms.CheckedListBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.SpellsReadyTab = new System.Windows.Forms.TabControl();
			this.Lvl1Tab = new System.Windows.Forms.TabPage();
			this.Lvl2Tab = new System.Windows.Forms.TabPage();
			this.Lvl3Tab = new System.Windows.Forms.TabPage();
			this.Lvl4Tab = new System.Windows.Forms.TabPage();
			this.Lvl5Tab = new System.Windows.Forms.TabPage();
			this.Lvl6Tab = new System.Windows.Forms.TabPage();
			this.button2 = new System.Windows.Forms.Button();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.button1 = new System.Windows.Forms.Button();
			this.groupBox3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize) (this.QuiverBox)).BeginInit();
			this.tabControl1.SuspendLayout();
			this.PropertiesTab.SuspendLayout();
			this.ProfessionTab.SuspendLayout();
			this.SpellTab.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SpellsReadyTab.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.HelmetBox);
			this.groupBox3.Controls.Add(this.label17);
			this.groupBox3.Controls.Add(this.label16);
			this.groupBox3.Controls.Add(this.label15);
			this.groupBox3.Controls.Add(this.label14);
			this.groupBox3.Controls.Add(this.label13);
			this.groupBox3.Controls.Add(this.label12);
			this.groupBox3.Controls.Add(this.label11);
			this.groupBox3.Controls.Add(this.label10);
			this.groupBox3.Controls.Add(this.label9);
			this.groupBox3.Controls.Add(this.NeckBox);
			this.groupBox3.Controls.Add(this.FeetBox);
			this.groupBox3.Controls.Add(this.SecondaryBox);
			this.groupBox3.Controls.Add(this.PrimaryBox);
			this.groupBox3.Controls.Add(this.RightRingBox);
			this.groupBox3.Controls.Add(this.LeftRingBox);
			this.groupBox3.Controls.Add(this.WristBox);
			this.groupBox3.Controls.Add(this.ArmorBox);
			this.groupBox3.Location = new System.Drawing.Point(22, 89);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(212, 271);
			this.groupBox3.TabIndex = 9;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Equipment :";
			// 
			// HelmetBox
			// 
			this.HelmetBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.HelmetBox.FormattingEnabled = true;
			this.HelmetBox.Location = new System.Drawing.Point(76, 235);
			this.HelmetBox.Name = "HelmetBox";
			this.HelmetBox.Size = new System.Drawing.Size(121, 21);
			this.HelmetBox.Sorted = true;
			this.HelmetBox.TabIndex = 2;
			this.HelmetBox.SelectedIndexChanged += new System.EventHandler(this.HelmetBox_SelectedIndexChanged);
			// 
			// label17
			// 
			this.label17.AutoSize = true;
			this.label17.Location = new System.Drawing.Point(6, 238);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(46, 13);
			this.label17.TabIndex = 5;
			this.label17.Text = "Helmet :";
			// 
			// label16
			// 
			this.label16.AutoSize = true;
			this.label16.Location = new System.Drawing.Point(6, 211);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(39, 13);
			this.label16.TabIndex = 5;
			this.label16.Text = "Neck :";
			// 
			// label15
			// 
			this.label15.AutoSize = true;
			this.label15.Location = new System.Drawing.Point(6, 184);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(34, 13);
			this.label15.TabIndex = 5;
			this.label15.Text = "Feet :";
			// 
			// label14
			// 
			this.label14.AutoSize = true;
			this.label14.Location = new System.Drawing.Point(6, 157);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(64, 13);
			this.label14.TabIndex = 5;
			this.label14.Text = "Secondary :";
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.Location = new System.Drawing.Point(6, 130);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(47, 13);
			this.label13.TabIndex = 5;
			this.label13.Text = "Primary :";
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Location = new System.Drawing.Point(6, 103);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(58, 13);
			this.label12.TabIndex = 5;
			this.label12.Text = "Right ring :";
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(6, 76);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(51, 13);
			this.label11.TabIndex = 5;
			this.label11.Text = "Left ring :";
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(6, 49);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(42, 13);
			this.label10.TabIndex = 5;
			this.label10.Text = "Wrists :";
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(6, 22);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(40, 13);
			this.label9.TabIndex = 5;
			this.label9.Text = "Armor :";
			// 
			// NeckBox
			// 
			this.NeckBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.NeckBox.FormattingEnabled = true;
			this.NeckBox.Location = new System.Drawing.Point(76, 208);
			this.NeckBox.Name = "NeckBox";
			this.NeckBox.Size = new System.Drawing.Size(121, 21);
			this.NeckBox.Sorted = true;
			this.NeckBox.TabIndex = 2;
			this.NeckBox.SelectedIndexChanged += new System.EventHandler(this.NeckBox_SelectedIndexChanged);
			// 
			// FeetBox
			// 
			this.FeetBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.FeetBox.FormattingEnabled = true;
			this.FeetBox.Location = new System.Drawing.Point(76, 181);
			this.FeetBox.Name = "FeetBox";
			this.FeetBox.Size = new System.Drawing.Size(121, 21);
			this.FeetBox.Sorted = true;
			this.FeetBox.TabIndex = 2;
			this.FeetBox.SelectedIndexChanged += new System.EventHandler(this.FeetBox_SelectedIndexChanged);
			// 
			// SecondaryBox
			// 
			this.SecondaryBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.SecondaryBox.FormattingEnabled = true;
			this.SecondaryBox.Location = new System.Drawing.Point(76, 154);
			this.SecondaryBox.Name = "SecondaryBox";
			this.SecondaryBox.Size = new System.Drawing.Size(121, 21);
			this.SecondaryBox.Sorted = true;
			this.SecondaryBox.TabIndex = 2;
			this.SecondaryBox.SelectedIndexChanged += new System.EventHandler(this.SecondaryBox_SelectedIndexChanged);
			// 
			// PrimaryBox
			// 
			this.PrimaryBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.PrimaryBox.FormattingEnabled = true;
			this.PrimaryBox.Location = new System.Drawing.Point(76, 127);
			this.PrimaryBox.Name = "PrimaryBox";
			this.PrimaryBox.Size = new System.Drawing.Size(121, 21);
			this.PrimaryBox.Sorted = true;
			this.PrimaryBox.TabIndex = 2;
			this.PrimaryBox.SelectedIndexChanged += new System.EventHandler(this.PrimaryBox_SelectedIndexChanged);
			// 
			// RightRingBox
			// 
			this.RightRingBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.RightRingBox.FormattingEnabled = true;
			this.RightRingBox.Location = new System.Drawing.Point(76, 100);
			this.RightRingBox.Name = "RightRingBox";
			this.RightRingBox.Size = new System.Drawing.Size(121, 21);
			this.RightRingBox.Sorted = true;
			this.RightRingBox.TabIndex = 2;
			this.RightRingBox.SelectedIndexChanged += new System.EventHandler(this.RightRingBox_SelectedIndexChanged);
			// 
			// LeftRingBox
			// 
			this.LeftRingBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.LeftRingBox.FormattingEnabled = true;
			this.LeftRingBox.Location = new System.Drawing.Point(76, 73);
			this.LeftRingBox.Name = "LeftRingBox";
			this.LeftRingBox.Size = new System.Drawing.Size(121, 21);
			this.LeftRingBox.Sorted = true;
			this.LeftRingBox.TabIndex = 2;
			this.LeftRingBox.SelectedIndexChanged += new System.EventHandler(this.LeftRingBox_SelectedIndexChanged);
			// 
			// WristBox
			// 
			this.WristBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.WristBox.FormattingEnabled = true;
			this.WristBox.Location = new System.Drawing.Point(76, 46);
			this.WristBox.Name = "WristBox";
			this.WristBox.Size = new System.Drawing.Size(121, 21);
			this.WristBox.Sorted = true;
			this.WristBox.TabIndex = 2;
			this.WristBox.SelectedIndexChanged += new System.EventHandler(this.WristBox_SelectedIndexChanged);
			// 
			// ArmorBox
			// 
			this.ArmorBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ArmorBox.FormattingEnabled = true;
			this.ArmorBox.Location = new System.Drawing.Point(76, 19);
			this.ArmorBox.Name = "ArmorBox";
			this.ArmorBox.Size = new System.Drawing.Size(121, 21);
			this.ArmorBox.Sorted = true;
			this.ArmorBox.TabIndex = 2;
			this.ArmorBox.SelectedIndexChanged += new System.EventHandler(this.ArmorBox_SelectedIndexChanged);
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(34, 57);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(44, 13);
			this.label8.TabIndex = 8;
			this.label8.Text = "Quiver :";
			// 
			// QuiverBox
			// 
			this.QuiverBox.Location = new System.Drawing.Point(84, 55);
			this.QuiverBox.Name = "QuiverBox";
			this.QuiverBox.Size = new System.Drawing.Size(53, 20);
			this.QuiverBox.TabIndex = 7;
			this.QuiverBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.QuiverBox.ThousandsSeparator = true;
			this.QuiverBox.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.PropertiesTab);
			this.tabControl1.Controls.Add(this.ProfessionTab);
			this.tabControl1.Controls.Add(this.SpellTab);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(690, 545);
			this.tabControl1.TabIndex = 10;
			// 
			// PropertiesTab
			// 
			this.PropertiesTab.Controls.Add(this.hitPointControl1);
			this.PropertiesTab.Controls.Add(this.label8);
			this.PropertiesTab.Controls.Add(this.groupBox3);
			this.PropertiesTab.Controls.Add(this.QuiverBox);
			this.PropertiesTab.Location = new System.Drawing.Point(4, 22);
			this.PropertiesTab.Name = "PropertiesTab";
			this.PropertiesTab.Padding = new System.Windows.Forms.Padding(3);
			this.PropertiesTab.Size = new System.Drawing.Size(682, 519);
			this.PropertiesTab.TabIndex = 0;
			this.PropertiesTab.Text = "Properties";
			this.PropertiesTab.UseVisualStyleBackColor = true;
			// 
			// hitPointControl1
			// 
			this.hitPointControl1.HitPoint = null;
			this.hitPointControl1.Location = new System.Drawing.Point(284, 74);
			this.hitPointControl1.Name = "hitPointControl1";
			this.hitPointControl1.Size = new System.Drawing.Size(156, 77);
			this.hitPointControl1.TabIndex = 12;
			// 
			// ProfessionTab
			// 
			this.ProfessionTab.Controls.Add(this.ProfessionBox3);
			this.ProfessionTab.Controls.Add(this.ProfessionBox2);
			this.ProfessionTab.Controls.Add(this.ProfessionBox1);
			this.ProfessionTab.Location = new System.Drawing.Point(4, 22);
			this.ProfessionTab.Name = "ProfessionTab";
			this.ProfessionTab.Size = new System.Drawing.Size(682, 519);
			this.ProfessionTab.TabIndex = 2;
			this.ProfessionTab.Text = "Professions";
			this.ProfessionTab.UseVisualStyleBackColor = true;
			// 
			// ProfessionBox3
			// 
			this.ProfessionBox3.Class = DungeonEye.HeroClass.Undefined;
			this.ProfessionBox3.Location = new System.Drawing.Point(3, 177);
			this.ProfessionBox3.Name = "ProfessionBox3";
			this.ProfessionBox3.Size = new System.Drawing.Size(407, 81);
			this.ProfessionBox3.TabIndex = 14;
			// 
			// ProfessionBox2
			// 
			this.ProfessionBox2.Class = DungeonEye.HeroClass.Undefined;
			this.ProfessionBox2.Location = new System.Drawing.Point(3, 90);
			this.ProfessionBox2.Name = "ProfessionBox2";
			this.ProfessionBox2.Size = new System.Drawing.Size(407, 81);
			this.ProfessionBox2.TabIndex = 14;
			// 
			// ProfessionBox1
			// 
			this.ProfessionBox1.Class = DungeonEye.HeroClass.Undefined;
			this.ProfessionBox1.Location = new System.Drawing.Point(3, 3);
			this.ProfessionBox1.Name = "ProfessionBox1";
			this.ProfessionBox1.Size = new System.Drawing.Size(407, 81);
			this.ProfessionBox1.TabIndex = 14;
			// 
			// SpellTab
			// 
			this.SpellTab.Controls.Add(this.groupBox2);
			this.SpellTab.Controls.Add(this.groupBox1);
			this.SpellTab.Location = new System.Drawing.Point(4, 22);
			this.SpellTab.Name = "SpellTab";
			this.SpellTab.Padding = new System.Windows.Forms.Padding(3);
			this.SpellTab.Size = new System.Drawing.Size(682, 519);
			this.SpellTab.TabIndex = 1;
			this.SpellTab.Text = "Spells";
			this.SpellTab.UseVisualStyleBackColor = true;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.UncheckAllLearnedBox);
			this.groupBox2.Controls.Add(this.CheckAllLearnedBox);
			this.groupBox2.Controls.Add(this.LearnedSpellBox);
			this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox2.Location = new System.Drawing.Point(3, 234);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(676, 282);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Spells learned (for mages) : ";
			// 
			// UncheckAllLearnedBox
			// 
			this.UncheckAllLearnedBox.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.UncheckAllLearnedBox.Location = new System.Drawing.Point(81, 256);
			this.UncheckAllLearnedBox.Name = "UncheckAllLearnedBox";
			this.UncheckAllLearnedBox.Size = new System.Drawing.Size(75, 23);
			this.UncheckAllLearnedBox.TabIndex = 2;
			this.UncheckAllLearnedBox.Text = "Uncheck All";
			this.UncheckAllLearnedBox.UseVisualStyleBackColor = true;
			this.UncheckAllLearnedBox.Click += new System.EventHandler(this.UncheckAllLearnedBox_Click);
			// 
			// CheckAllLearnedBox
			// 
			this.CheckAllLearnedBox.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.CheckAllLearnedBox.Location = new System.Drawing.Point(0, 256);
			this.CheckAllLearnedBox.Name = "CheckAllLearnedBox";
			this.CheckAllLearnedBox.Size = new System.Drawing.Size(75, 23);
			this.CheckAllLearnedBox.TabIndex = 1;
			this.CheckAllLearnedBox.Text = "Check All";
			this.CheckAllLearnedBox.UseVisualStyleBackColor = true;
			this.CheckAllLearnedBox.Click += new System.EventHandler(this.CheckAllLearnedBox_Click);
			// 
			// LearnedSpellBox
			// 
			this.LearnedSpellBox.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.LearnedSpellBox.CheckOnClick = true;
			this.LearnedSpellBox.FormattingEnabled = true;
			this.LearnedSpellBox.Location = new System.Drawing.Point(3, 16);
			this.LearnedSpellBox.Name = "LearnedSpellBox";
			this.LearnedSpellBox.Size = new System.Drawing.Size(670, 229);
			this.LearnedSpellBox.Sorted = true;
			this.LearnedSpellBox.TabIndex = 0;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.SpellsReadyTab);
			this.groupBox1.Controls.Add(this.button2);
			this.groupBox1.Controls.Add(this.groupBox4);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
			this.groupBox1.Location = new System.Drawing.Point(3, 3);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(676, 231);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Spells ready to cast :";
			// 
			// SpellsReadyTab
			// 
			this.SpellsReadyTab.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.SpellsReadyTab.Controls.Add(this.Lvl1Tab);
			this.SpellsReadyTab.Controls.Add(this.Lvl2Tab);
			this.SpellsReadyTab.Controls.Add(this.Lvl3Tab);
			this.SpellsReadyTab.Controls.Add(this.Lvl4Tab);
			this.SpellsReadyTab.Controls.Add(this.Lvl5Tab);
			this.SpellsReadyTab.Controls.Add(this.Lvl6Tab);
			this.SpellsReadyTab.Location = new System.Drawing.Point(6, 19);
			this.SpellsReadyTab.Name = "SpellsReadyTab";
			this.SpellsReadyTab.SelectedIndex = 0;
			this.SpellsReadyTab.Size = new System.Drawing.Size(413, 177);
			this.SpellsReadyTab.TabIndex = 0;
			// 
			// Lvl1Tab
			// 
			this.Lvl1Tab.Location = new System.Drawing.Point(4, 22);
			this.Lvl1Tab.Name = "Lvl1Tab";
			this.Lvl1Tab.Padding = new System.Windows.Forms.Padding(3);
			this.Lvl1Tab.Size = new System.Drawing.Size(405, 151);
			this.Lvl1Tab.TabIndex = 0;
			this.Lvl1Tab.Text = "Level 1";
			this.Lvl1Tab.UseVisualStyleBackColor = true;
			// 
			// Lvl2Tab
			// 
			this.Lvl2Tab.Location = new System.Drawing.Point(4, 22);
			this.Lvl2Tab.Name = "Lvl2Tab";
			this.Lvl2Tab.Padding = new System.Windows.Forms.Padding(3);
			this.Lvl2Tab.Size = new System.Drawing.Size(414, 163);
			this.Lvl2Tab.TabIndex = 1;
			this.Lvl2Tab.Text = "Level 2";
			this.Lvl2Tab.UseVisualStyleBackColor = true;
			// 
			// Lvl3Tab
			// 
			this.Lvl3Tab.Location = new System.Drawing.Point(4, 22);
			this.Lvl3Tab.Name = "Lvl3Tab";
			this.Lvl3Tab.Size = new System.Drawing.Size(414, 163);
			this.Lvl3Tab.TabIndex = 2;
			this.Lvl3Tab.Text = "Level 3";
			this.Lvl3Tab.UseVisualStyleBackColor = true;
			// 
			// Lvl4Tab
			// 
			this.Lvl4Tab.Location = new System.Drawing.Point(4, 22);
			this.Lvl4Tab.Name = "Lvl4Tab";
			this.Lvl4Tab.Size = new System.Drawing.Size(414, 163);
			this.Lvl4Tab.TabIndex = 3;
			this.Lvl4Tab.Text = "Level 4";
			this.Lvl4Tab.UseVisualStyleBackColor = true;
			// 
			// Lvl5Tab
			// 
			this.Lvl5Tab.Location = new System.Drawing.Point(4, 22);
			this.Lvl5Tab.Name = "Lvl5Tab";
			this.Lvl5Tab.Size = new System.Drawing.Size(414, 163);
			this.Lvl5Tab.TabIndex = 4;
			this.Lvl5Tab.Text = "Level 5";
			this.Lvl5Tab.UseVisualStyleBackColor = true;
			// 
			// Lvl6Tab
			// 
			this.Lvl6Tab.Location = new System.Drawing.Point(4, 22);
			this.Lvl6Tab.Name = "Lvl6Tab";
			this.Lvl6Tab.Size = new System.Drawing.Size(414, 163);
			this.Lvl6Tab.TabIndex = 5;
			this.Lvl6Tab.Text = "Level 6";
			this.Lvl6Tab.UseVisualStyleBackColor = true;
			// 
			// button2
			// 
			this.button2.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.button2.Location = new System.Drawing.Point(6, 202);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(413, 23);
			this.button2.TabIndex = 0;
			this.button2.Text = "Remove";
			this.button2.UseVisualStyleBackColor = true;
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.button1);
			this.groupBox4.Dock = System.Windows.Forms.DockStyle.Right;
			this.groupBox4.Location = new System.Drawing.Point(425, 16);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(248, 212);
			this.groupBox4.TabIndex = 1;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Add spells :";
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.button1.Location = new System.Drawing.Point(6, 183);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(236, 23);
			this.button1.TabIndex = 1;
			this.button1.Text = "Add";
			this.button1.UseVisualStyleBackColor = true;
			// 
			// HeroControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tabControl1);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
			this.Name = "HeroControl";
			this.Size = new System.Drawing.Size(690, 545);
			this.Load += new System.EventHandler(this.HeroControl_Load);
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			((System.ComponentModel.ISupportInitialize) (this.QuiverBox)).EndInit();
			this.tabControl1.ResumeLayout(false);
			this.PropertiesTab.ResumeLayout(false);
			this.PropertiesTab.PerformLayout();
			this.ProfessionTab.ResumeLayout(false);
			this.SpellTab.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.SpellsReadyTab.ResumeLayout(false);
			this.groupBox4.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.ComboBox HelmetBox;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.ComboBox NeckBox;
		private System.Windows.Forms.ComboBox FeetBox;
		private System.Windows.Forms.ComboBox SecondaryBox;
		private System.Windows.Forms.ComboBox PrimaryBox;
		private System.Windows.Forms.ComboBox RightRingBox;
		private System.Windows.Forms.ComboBox LeftRingBox;
		private System.Windows.Forms.ComboBox WristBox;
		private System.Windows.Forms.ComboBox ArmorBox;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.NumericUpDown QuiverBox;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage PropertiesTab;
		private System.Windows.Forms.TabPage SpellTab;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckedListBox LearnedSpellBox;
		private HitPointControl hitPointControl1;
		private System.Windows.Forms.TabPage ProfessionTab;
		private ProfessionControl ProfessionBox3;
		private ProfessionControl ProfessionBox2;
		private ProfessionControl ProfessionBox1;
		private System.Windows.Forms.Button UncheckAllLearnedBox;
		private System.Windows.Forms.Button CheckAllLearnedBox;
		private System.Windows.Forms.TabControl SpellsReadyTab;
		private System.Windows.Forms.TabPage Lvl1Tab;
		private System.Windows.Forms.TabPage Lvl2Tab;
		private System.Windows.Forms.TabPage Lvl3Tab;
		private System.Windows.Forms.TabPage Lvl4Tab;
		private System.Windows.Forms.TabPage Lvl5Tab;
		private System.Windows.Forms.TabPage Lvl6Tab;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
	}
}
