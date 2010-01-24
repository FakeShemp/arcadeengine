namespace DungeonEye.Forms
{
	partial class EntityControl
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
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.CharismaBox = new System.Windows.Forms.NumericUpDown();
			this.WisdomBox = new System.Windows.Forms.NumericUpDown();
			this.IntelligenceBox = new System.Windows.Forms.NumericUpDown();
			this.ConstitutionBox = new System.Windows.Forms.NumericUpDown();
			this.DexterityBox = new System.Windows.Forms.NumericUpDown();
			this.StrengthBox = new System.Windows.Forms.NumericUpDown();
			this.AlignmentBox = new System.Windows.Forms.ComboBox();
			this.label7 = new System.Windows.Forms.Label();
			this.QuiverBox = new System.Windows.Forms.NumericUpDown();
			this.label8 = new System.Windows.Forms.Label();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.ArmorBox = new System.Windows.Forms.ComboBox();
			this.WristBox = new System.Windows.Forms.ComboBox();
			this.LeftRingBox = new System.Windows.Forms.ComboBox();
			this.RightRingBox = new System.Windows.Forms.ComboBox();
			this.PrimaryBox = new System.Windows.Forms.ComboBox();
			this.SecondaryBox = new System.Windows.Forms.ComboBox();
			this.FeetBox = new System.Windows.Forms.ComboBox();
			this.NeckBox = new System.Windows.Forms.ComboBox();
			this.HelmetBox = new System.Windows.Forms.ComboBox();
			this.label9 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.label14 = new System.Windows.Forms.Label();
			this.label15 = new System.Windows.Forms.Label();
			this.label16 = new System.Windows.Forms.Label();
			this.label17 = new System.Windows.Forms.Label();
			this.hitPointControl1 = new DungeonEye.Forms.HitPointControl();
			this.RollAbilitiesBox = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.CharismaBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.WisdomBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.IntelligenceBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ConstitutionBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.DexterityBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.StrengthBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.QuiverBox)).BeginInit();
			this.groupBox3.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.groupBox3);
			this.groupBox1.Controls.Add(this.label8);
			this.groupBox1.Controls.Add(this.QuiverBox);
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.Controls.Add(this.AlignmentBox);
			this.groupBox1.Controls.Add(this.groupBox2);
			this.groupBox1.Controls.Add(this.hitPointControl1);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox1.Location = new System.Drawing.Point(0, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(621, 430);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Entity :";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.RollAbilitiesBox);
			this.groupBox2.Controls.Add(this.label6);
			this.groupBox2.Controls.Add(this.label5);
			this.groupBox2.Controls.Add(this.label4);
			this.groupBox2.Controls.Add(this.label3);
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Controls.Add(this.label1);
			this.groupBox2.Controls.Add(this.CharismaBox);
			this.groupBox2.Controls.Add(this.WisdomBox);
			this.groupBox2.Controls.Add(this.IntelligenceBox);
			this.groupBox2.Controls.Add(this.ConstitutionBox);
			this.groupBox2.Controls.Add(this.DexterityBox);
			this.groupBox2.Controls.Add(this.StrengthBox);
			this.groupBox2.Location = new System.Drawing.Point(6, 102);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(168, 210);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Abilities :";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(6, 151);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(56, 13);
			this.label6.TabIndex = 1;
			this.label6.Text = "Charisma :";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(6, 125);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(51, 13);
			this.label5.TabIndex = 1;
			this.label5.Text = "Wisdom :";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(6, 99);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(67, 13);
			this.label4.TabIndex = 1;
			this.label4.Text = "Intelligence :";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(6, 73);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(68, 13);
			this.label3.TabIndex = 1;
			this.label3.Text = "Constitution :";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6, 47);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(54, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "Dexterity :";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 21);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(53, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Strength :";
			// 
			// CharismaBox
			// 
			this.CharismaBox.Location = new System.Drawing.Point(80, 149);
			this.CharismaBox.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.CharismaBox.Name = "CharismaBox";
			this.CharismaBox.Size = new System.Drawing.Size(66, 20);
			this.CharismaBox.TabIndex = 0;
			this.CharismaBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.CharismaBox.ValueChanged += new System.EventHandler(this.CharismaBox_ValueChanged);
			// 
			// WisdomBox
			// 
			this.WisdomBox.Location = new System.Drawing.Point(80, 123);
			this.WisdomBox.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.WisdomBox.Name = "WisdomBox";
			this.WisdomBox.Size = new System.Drawing.Size(66, 20);
			this.WisdomBox.TabIndex = 0;
			this.WisdomBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.WisdomBox.ValueChanged += new System.EventHandler(this.WisdomBox_ValueChanged);
			// 
			// IntelligenceBox
			// 
			this.IntelligenceBox.Location = new System.Drawing.Point(80, 97);
			this.IntelligenceBox.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.IntelligenceBox.Name = "IntelligenceBox";
			this.IntelligenceBox.Size = new System.Drawing.Size(66, 20);
			this.IntelligenceBox.TabIndex = 0;
			this.IntelligenceBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.IntelligenceBox.ValueChanged += new System.EventHandler(this.IntelligenceBox_ValueChanged);
			// 
			// ConstitutionBox
			// 
			this.ConstitutionBox.Location = new System.Drawing.Point(80, 71);
			this.ConstitutionBox.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.ConstitutionBox.Name = "ConstitutionBox";
			this.ConstitutionBox.Size = new System.Drawing.Size(66, 20);
			this.ConstitutionBox.TabIndex = 0;
			this.ConstitutionBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.ConstitutionBox.ValueChanged += new System.EventHandler(this.ConstitutionBox_ValueChanged);
			// 
			// DexterityBox
			// 
			this.DexterityBox.Location = new System.Drawing.Point(80, 45);
			this.DexterityBox.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.DexterityBox.Name = "DexterityBox";
			this.DexterityBox.Size = new System.Drawing.Size(66, 20);
			this.DexterityBox.TabIndex = 0;
			this.DexterityBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.DexterityBox.ValueChanged += new System.EventHandler(this.DexterityBox_ValueChanged);
			// 
			// StrengthBox
			// 
			this.StrengthBox.Location = new System.Drawing.Point(80, 19);
			this.StrengthBox.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.StrengthBox.Name = "StrengthBox";
			this.StrengthBox.Size = new System.Drawing.Size(66, 20);
			this.StrengthBox.TabIndex = 0;
			this.StrengthBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.StrengthBox.ValueChanged += new System.EventHandler(this.StrengthBox_ValueChanged);
			// 
			// AlignmentBox
			// 
			this.AlignmentBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.AlignmentBox.FormattingEnabled = true;
			this.AlignmentBox.Location = new System.Drawing.Point(245, 37);
			this.AlignmentBox.Name = "AlignmentBox";
			this.AlignmentBox.Size = new System.Drawing.Size(121, 21);
			this.AlignmentBox.TabIndex = 2;
			this.AlignmentBox.SelectedIndexChanged += new System.EventHandler(this.AlignmentBox_SelectedIndexChanged);
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(180, 40);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(59, 13);
			this.label7.TabIndex = 3;
			this.label7.Text = "Alignment :";
			// 
			// QuiverBox
			// 
			this.QuiverBox.Location = new System.Drawing.Point(245, 68);
			this.QuiverBox.Name = "QuiverBox";
			this.QuiverBox.Size = new System.Drawing.Size(53, 20);
			this.QuiverBox.TabIndex = 4;
			this.QuiverBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.QuiverBox.ThousandsSeparator = true;
			this.QuiverBox.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(195, 70);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(44, 13);
			this.label8.TabIndex = 5;
			this.label8.Text = "Quiver :";
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
			this.groupBox3.Location = new System.Drawing.Point(183, 102);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(212, 271);
			this.groupBox3.TabIndex = 6;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Equipment :";
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
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(6, 22);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(40, 13);
			this.label9.TabIndex = 5;
			this.label9.Text = "Armor :";
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(6, 49);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(37, 13);
			this.label10.TabIndex = 5;
			this.label10.Text = "Wrist :";
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
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Location = new System.Drawing.Point(6, 103);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(58, 13);
			this.label12.TabIndex = 5;
			this.label12.Text = "Right ring :";
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
			// label14
			// 
			this.label14.AutoSize = true;
			this.label14.Location = new System.Drawing.Point(6, 157);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(64, 13);
			this.label14.TabIndex = 5;
			this.label14.Text = "Secondary :";
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
			// label16
			// 
			this.label16.AutoSize = true;
			this.label16.Location = new System.Drawing.Point(6, 211);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(39, 13);
			this.label16.TabIndex = 5;
			this.label16.Text = "Neck :";
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
			// hitPointControl1
			// 
			this.hitPointControl1.HitPoint = null;
			this.hitPointControl1.Location = new System.Drawing.Point(6, 19);
			this.hitPointControl1.Name = "hitPointControl1";
			this.hitPointControl1.Size = new System.Drawing.Size(168, 77);
			this.hitPointControl1.TabIndex = 0;
			// 
			// RollAbilitiesBox
			// 
			this.RollAbilitiesBox.Location = new System.Drawing.Point(71, 175);
			this.RollAbilitiesBox.Name = "RollAbilitiesBox";
			this.RollAbilitiesBox.Size = new System.Drawing.Size(75, 23);
			this.RollAbilitiesBox.TabIndex = 2;
			this.RollAbilitiesBox.Text = "Reroll";
			this.RollAbilitiesBox.UseVisualStyleBackColor = true;
			this.RollAbilitiesBox.Click += new System.EventHandler(this.RollAbilitiesBox_Click);
			// 
			// EntityControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.groupBox1);
			this.Name = "EntityControl";
			this.Size = new System.Drawing.Size(621, 430);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.CharismaBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.WisdomBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.IntelligenceBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ConstitutionBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.DexterityBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.StrengthBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.QuiverBox)).EndInit();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private HitPointControl hitPointControl1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.NumericUpDown CharismaBox;
		private System.Windows.Forms.NumericUpDown WisdomBox;
		private System.Windows.Forms.NumericUpDown IntelligenceBox;
		private System.Windows.Forms.NumericUpDown ConstitutionBox;
		private System.Windows.Forms.NumericUpDown DexterityBox;
		private System.Windows.Forms.NumericUpDown StrengthBox;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.ComboBox AlignmentBox;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.NumericUpDown QuiverBox;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.ComboBox FeetBox;
		private System.Windows.Forms.ComboBox SecondaryBox;
		private System.Windows.Forms.ComboBox PrimaryBox;
		private System.Windows.Forms.ComboBox RightRingBox;
		private System.Windows.Forms.ComboBox LeftRingBox;
		private System.Windows.Forms.ComboBox WristBox;
		private System.Windows.Forms.ComboBox ArmorBox;
		private System.Windows.Forms.ComboBox HelmetBox;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.ComboBox NeckBox;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Button RollAbilitiesBox;
	}
}
