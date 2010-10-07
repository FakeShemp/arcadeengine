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
			this.QuiverBox = new System.Windows.Forms.NumericUpDown();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.PropertiesTab = new System.Windows.Forms.TabPage();
			this.ProfessionTab = new System.Windows.Forms.TabPage();
			this.SpellTab = new System.Windows.Forms.TabPage();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.AvailableSpellBox = new System.Windows.Forms.ListBox();
			this.button1 = new System.Windows.Forms.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.button4 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.LearnedSpellBox = new System.Windows.Forms.CheckedListBox();
			this.SpellLevelBox = new System.Windows.Forms.ComboBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.SpellReportLabel = new System.Windows.Forms.Label();
			this.SpellReadyBox = new System.Windows.Forms.ListBox();
			this.button2 = new System.Windows.Forms.Button();
			this.groupBox5 = new System.Windows.Forms.GroupBox();
			this.FoodBox = new System.Windows.Forms.TrackBar();
			this.groupBox6 = new System.Windows.Forms.GroupBox();
			this.label2 = new System.Windows.Forms.Label();
			this.HPBox = new DungeonEye.Forms.HitPointControl();
			this.ProfessionsBox = new DungeonEye.Forms.ProfessionsControl();
			this.groupBox3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize) (this.QuiverBox)).BeginInit();
			this.tabControl1.SuspendLayout();
			this.PropertiesTab.SuspendLayout();
			this.ProfessionTab.SuspendLayout();
			this.SpellTab.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.groupBox5.SuspendLayout();
			((System.ComponentModel.ISupportInitialize) (this.FoodBox)).BeginInit();
			this.groupBox6.SuspendLayout();
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
			this.groupBox3.Location = new System.Drawing.Point(6, 6);
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
			// QuiverBox
			// 
			this.QuiverBox.Location = new System.Drawing.Point(82, 20);
			this.QuiverBox.Name = "QuiverBox";
			this.QuiverBox.Size = new System.Drawing.Size(68, 20);
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
			this.tabControl1.Size = new System.Drawing.Size(630, 545);
			this.tabControl1.TabIndex = 10;
			// 
			// PropertiesTab
			// 
			this.PropertiesTab.Controls.Add(this.groupBox6);
			this.PropertiesTab.Controls.Add(this.groupBox5);
			this.PropertiesTab.Controls.Add(this.HPBox);
			this.PropertiesTab.Controls.Add(this.groupBox3);
			this.PropertiesTab.Location = new System.Drawing.Point(4, 22);
			this.PropertiesTab.Name = "PropertiesTab";
			this.PropertiesTab.Padding = new System.Windows.Forms.Padding(3);
			this.PropertiesTab.Size = new System.Drawing.Size(622, 519);
			this.PropertiesTab.TabIndex = 0;
			this.PropertiesTab.Text = "Properties";
			this.PropertiesTab.UseVisualStyleBackColor = true;
			// 
			// ProfessionTab
			// 
			this.ProfessionTab.Controls.Add(this.ProfessionsBox);
			this.ProfessionTab.Location = new System.Drawing.Point(4, 22);
			this.ProfessionTab.Name = "ProfessionTab";
			this.ProfessionTab.Size = new System.Drawing.Size(622, 519);
			this.ProfessionTab.TabIndex = 2;
			this.ProfessionTab.Text = "Professions";
			this.ProfessionTab.UseVisualStyleBackColor = true;
			// 
			// SpellTab
			// 
			this.SpellTab.Controls.Add(this.label1);
			this.SpellTab.Controls.Add(this.groupBox4);
			this.SpellTab.Controls.Add(this.groupBox2);
			this.SpellTab.Controls.Add(this.SpellLevelBox);
			this.SpellTab.Controls.Add(this.groupBox1);
			this.SpellTab.Location = new System.Drawing.Point(4, 22);
			this.SpellTab.Name = "SpellTab";
			this.SpellTab.Padding = new System.Windows.Forms.Padding(3);
			this.SpellTab.Size = new System.Drawing.Size(622, 519);
			this.SpellTab.TabIndex = 1;
			this.SpellTab.Text = "Spells";
			this.SpellTab.UseVisualStyleBackColor = true;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(222, 20);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(39, 13);
			this.label1.TabIndex = 4;
			this.label1.Text = "Level :";
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.AvailableSpellBox);
			this.groupBox4.Controls.Add(this.button1);
			this.groupBox4.Location = new System.Drawing.Point(209, 48);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(200, 214);
			this.groupBox4.TabIndex = 2;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Available spells :";
			// 
			// AvailableSpellBox
			// 
			this.AvailableSpellBox.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.AvailableSpellBox.FormattingEnabled = true;
			this.AvailableSpellBox.Location = new System.Drawing.Point(6, 19);
			this.AvailableSpellBox.Name = "AvailableSpellBox";
			this.AvailableSpellBox.Size = new System.Drawing.Size(188, 147);
			this.AvailableSpellBox.Sorted = true;
			this.AvailableSpellBox.TabIndex = 2;
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.button1.Location = new System.Drawing.Point(6, 182);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(188, 23);
			this.button1.TabIndex = 1;
			this.button1.Text = "Add";
			this.button1.UseVisualStyleBackColor = true;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.button4);
			this.groupBox2.Controls.Add(this.button3);
			this.groupBox2.Controls.Add(this.LearnedSpellBox);
			this.groupBox2.Location = new System.Drawing.Point(415, 48);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(200, 214);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Learned spells :";
			// 
			// button4
			// 
			this.button4.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button4.Location = new System.Drawing.Point(104, 185);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(90, 23);
			this.button4.TabIndex = 2;
			this.button4.Text = "Uncheck All";
			this.button4.UseVisualStyleBackColor = true;
			this.button4.Click += new System.EventHandler(this.UncheckAllLearnedBox_Click);
			// 
			// button3
			// 
			this.button3.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.button3.Location = new System.Drawing.Point(6, 185);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(90, 23);
			this.button3.TabIndex = 1;
			this.button3.Text = "Check All";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.CheckAllLearnedBox_Click);
			// 
			// LearnedSpellBox
			// 
			this.LearnedSpellBox.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.LearnedSpellBox.CheckOnClick = true;
			this.LearnedSpellBox.FormattingEnabled = true;
			this.LearnedSpellBox.Location = new System.Drawing.Point(6, 19);
			this.LearnedSpellBox.Name = "LearnedSpellBox";
			this.LearnedSpellBox.Size = new System.Drawing.Size(188, 154);
			this.LearnedSpellBox.Sorted = true;
			this.LearnedSpellBox.TabIndex = 0;
			this.LearnedSpellBox.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.LearnedSpellBox_ItemCheck);
			// 
			// SpellLevelBox
			// 
			this.SpellLevelBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.SpellLevelBox.FormattingEnabled = true;
			this.SpellLevelBox.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6"});
			this.SpellLevelBox.Location = new System.Drawing.Point(267, 17);
			this.SpellLevelBox.Name = "SpellLevelBox";
			this.SpellLevelBox.Size = new System.Drawing.Size(142, 21);
			this.SpellLevelBox.Sorted = true;
			this.SpellLevelBox.TabIndex = 3;
			this.SpellLevelBox.SelectedIndexChanged += new System.EventHandler(this.SpellLevelBox_SelectedIndexChanged);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.SpellReportLabel);
			this.groupBox1.Controls.Add(this.SpellReadyBox);
			this.groupBox1.Controls.Add(this.button2);
			this.groupBox1.Location = new System.Drawing.Point(3, 48);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(200, 214);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Spells ready to cast :";
			// 
			// SpellReportLabel
			// 
			this.SpellReportLabel.Location = new System.Drawing.Point(6, 156);
			this.SpellReportLabel.Name = "SpellReportLabel";
			this.SpellReportLabel.Size = new System.Drawing.Size(188, 23);
			this.SpellReportLabel.TabIndex = 5;
			this.SpellReportLabel.Text = "X of X spells remaining.";
			this.SpellReportLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// SpellReadyBox
			// 
			this.SpellReadyBox.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.SpellReadyBox.FormattingEnabled = true;
			this.SpellReadyBox.Location = new System.Drawing.Point(6, 19);
			this.SpellReadyBox.Name = "SpellReadyBox";
			this.SpellReadyBox.Size = new System.Drawing.Size(188, 134);
			this.SpellReadyBox.Sorted = true;
			this.SpellReadyBox.TabIndex = 2;
			// 
			// button2
			// 
			this.button2.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.button2.Location = new System.Drawing.Point(6, 182);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(188, 23);
			this.button2.TabIndex = 0;
			this.button2.Text = "Remove";
			this.button2.UseVisualStyleBackColor = true;
			// 
			// groupBox5
			// 
			this.groupBox5.Controls.Add(this.FoodBox);
			this.groupBox5.Location = new System.Drawing.Point(224, 143);
			this.groupBox5.Name = "groupBox5";
			this.groupBox5.Size = new System.Drawing.Size(156, 51);
			this.groupBox5.TabIndex = 13;
			this.groupBox5.TabStop = false;
			this.groupBox5.Text = "Food : ";
			// 
			// FoodBox
			// 
			this.FoodBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.FoodBox.Location = new System.Drawing.Point(3, 16);
			this.FoodBox.Maximum = 100;
			this.FoodBox.Name = "FoodBox";
			this.FoodBox.Size = new System.Drawing.Size(150, 45);
			this.FoodBox.TabIndex = 0;
			this.FoodBox.TickFrequency = 10;
			this.FoodBox.ValueChanged += new System.EventHandler(this.FoodBox_ValueChanged);
			// 
			// groupBox6
			// 
			this.groupBox6.Controls.Add(this.label2);
			this.groupBox6.Controls.Add(this.QuiverBox);
			this.groupBox6.Location = new System.Drawing.Point(224, 89);
			this.groupBox6.Name = "groupBox6";
			this.groupBox6.Size = new System.Drawing.Size(156, 48);
			this.groupBox6.TabIndex = 14;
			this.groupBox6.TabStop = false;
			this.groupBox6.Text = "Quiver :";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6, 22);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(70, 13);
			this.label2.TabIndex = 8;
			this.label2.Text = "Arrow count :";
			// 
			// HPBox
			// 
			this.HPBox.HitPoint = null;
			this.HPBox.Location = new System.Drawing.Point(224, 6);
			this.HPBox.Name = "HPBox";
			this.HPBox.Size = new System.Drawing.Size(156, 77);
			this.HPBox.TabIndex = 12;
			// 
			// ProfessionsBox
			// 
			this.ProfessionsBox.Hero = null;
			this.ProfessionsBox.Location = new System.Drawing.Point(3, 3);
			this.ProfessionsBox.MinimumSize = new System.Drawing.Size(300, 175);
			this.ProfessionsBox.Name = "ProfessionsBox";
			this.ProfessionsBox.Size = new System.Drawing.Size(300, 175);
			this.ProfessionsBox.TabIndex = 0;
			this.ProfessionsBox.Title = "Professions :";
			// 
			// HeroControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tabControl1);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
			this.Name = "HeroControl";
			this.Size = new System.Drawing.Size(630, 545);
			this.Load += new System.EventHandler(this.HeroControl_Load);
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			((System.ComponentModel.ISupportInitialize) (this.QuiverBox)).EndInit();
			this.tabControl1.ResumeLayout(false);
			this.PropertiesTab.ResumeLayout(false);
			this.ProfessionTab.ResumeLayout(false);
			this.SpellTab.ResumeLayout(false);
			this.SpellTab.PerformLayout();
			this.groupBox4.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.groupBox5.ResumeLayout(false);
			this.groupBox5.PerformLayout();
			((System.ComponentModel.ISupportInitialize) (this.FoodBox)).EndInit();
			this.groupBox6.ResumeLayout(false);
			this.groupBox6.PerformLayout();
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
		private System.Windows.Forms.NumericUpDown QuiverBox;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage PropertiesTab;
		private System.Windows.Forms.TabPage SpellTab;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckedListBox LearnedSpellBox;
		private HitPointControl HPBox;
		private System.Windows.Forms.TabPage ProfessionTab;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.ListBox AvailableSpellBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox SpellLevelBox;
		private System.Windows.Forms.ListBox SpellReadyBox;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Label SpellReportLabel;
		private ProfessionsControl ProfessionsBox;
		private System.Windows.Forms.GroupBox groupBox6;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.GroupBox groupBox5;
		private System.Windows.Forms.TrackBar FoodBox;
	}
}
