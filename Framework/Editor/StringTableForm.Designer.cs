namespace ArcEngine.Editor
{
	partial class StringTableForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StringTableForm));
			this.TranslatedTextBox = new System.Windows.Forms.TextBox();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.ClearStringBox = new System.Windows.Forms.ToolStripButton();
			this.OpenBox = new System.Windows.Forms.ToolStripButton();
			this.SaveBox = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.WriteStringBox = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.AddAsNewStringBox = new System.Windows.Forms.ToolStripButton();
			this.DeleteStringBox = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
			this.DefaultLanguageBox = new System.Windows.Forms.ToolStripComboBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.StringListBox = new System.Windows.Forms.ListView();
			this.IDColumn = new System.Windows.Forms.ColumnHeader();
			this.TextColumn = new System.Windows.Forms.ColumnHeader();
			this.toolStrip2 = new System.Windows.Forms.ToolStrip();
			this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
			this.CurrentLanguageBox = new System.Windows.Forms.ToolStripComboBox();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.RemoveLanguageBox = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
			this.NewLanguageBox = new System.Windows.Forms.ToolStripTextBox();
			this.AddNewLanguageBox = new System.Windows.Forms.ToolStripButton();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.Default = new System.Windows.Forms.GroupBox();
			this.OriginalTextBox = new System.Windows.Forms.TextBox();
			this.toolStrip1.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.toolStrip2.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.Default.SuspendLayout();
			this.SuspendLayout();
			// 
			// TranslatedTextBox
			// 
			this.TranslatedTextBox.AcceptsReturn = true;
			this.TranslatedTextBox.AcceptsTab = true;
			this.TranslatedTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TranslatedTextBox.Location = new System.Drawing.Point(3, 16);
			this.TranslatedTextBox.Multiline = true;
			this.TranslatedTextBox.Name = "TranslatedTextBox";
			this.TranslatedTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.TranslatedTextBox.Size = new System.Drawing.Size(949, 88);
			this.TranslatedTextBox.TabIndex = 2;
			this.TranslatedTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TranslatedTextBox_KeyDown);
			// 
			// toolStrip1
			// 
			this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ClearStringBox,
            this.OpenBox,
            this.SaveBox,
            this.toolStripSeparator1,
            this.WriteStringBox,
            this.toolStripSeparator3,
            this.AddAsNewStringBox,
            this.DeleteStringBox,
            this.toolStripSeparator2,
            this.toolStripLabel1,
            this.DefaultLanguageBox});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(955, 25);
			this.toolStrip1.TabIndex = 3;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// ClearStringBox
			// 
			this.ClearStringBox.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.ClearStringBox.Image = ((System.Drawing.Image)(resources.GetObject("ClearStringBox.Image")));
			this.ClearStringBox.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.ClearStringBox.Name = "ClearStringBox";
			this.ClearStringBox.Size = new System.Drawing.Size(23, 22);
			this.ClearStringBox.Text = "Clear string";
			this.ClearStringBox.Click += new System.EventHandler(this.ClearStringBox_Click);
			// 
			// OpenBox
			// 
			this.OpenBox.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.OpenBox.Image = ((System.Drawing.Image)(resources.GetObject("OpenBox.Image")));
			this.OpenBox.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.OpenBox.Name = "OpenBox";
			this.OpenBox.Size = new System.Drawing.Size(23, 22);
			this.OpenBox.Text = "Loads text from file...";
			// 
			// SaveBox
			// 
			this.SaveBox.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.SaveBox.Image = ((System.Drawing.Image)(resources.GetObject("SaveBox.Image")));
			this.SaveBox.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.SaveBox.Name = "SaveBox";
			this.SaveBox.Size = new System.Drawing.Size(23, 22);
			this.SaveBox.Text = "Save text";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// WriteStringBox
			// 
			this.WriteStringBox.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.WriteStringBox.Image = ((System.Drawing.Image)(resources.GetObject("WriteStringBox.Image")));
			this.WriteStringBox.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.WriteStringBox.Name = "WriteStringBox";
			this.WriteStringBox.Size = new System.Drawing.Size(23, 22);
			this.WriteStringBox.Text = "toolStripButton1";
			this.WriteStringBox.Click += new System.EventHandler(this.WriteStringBox_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
			// 
			// AddAsNewStringBox
			// 
			this.AddAsNewStringBox.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.AddAsNewStringBox.Image = ((System.Drawing.Image)(resources.GetObject("AddAsNewStringBox.Image")));
			this.AddAsNewStringBox.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.AddAsNewStringBox.Name = "AddAsNewStringBox";
			this.AddAsNewStringBox.Size = new System.Drawing.Size(23, 22);
			this.AddAsNewStringBox.Text = "Add a new string";
			this.AddAsNewStringBox.Click += new System.EventHandler(this.OnAddNewString);
			// 
			// DeleteStringBox
			// 
			this.DeleteStringBox.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.DeleteStringBox.Image = ((System.Drawing.Image)(resources.GetObject("DeleteStringBox.Image")));
			this.DeleteStringBox.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.DeleteStringBox.Name = "DeleteStringBox";
			this.DeleteStringBox.Size = new System.Drawing.Size(23, 22);
			this.DeleteStringBox.Text = "Delete selected strings";
			this.DeleteStringBox.Click += new System.EventHandler(this.DeleteString_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			// 
			// toolStripLabel1
			// 
			this.toolStripLabel1.Name = "toolStripLabel1";
			this.toolStripLabel1.Size = new System.Drawing.Size(51, 22);
			this.toolStripLabel1.Text = "Default :";
			// 
			// DefaultLanguageBox
			// 
			this.DefaultLanguageBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.DefaultLanguageBox.Name = "DefaultLanguageBox";
			this.DefaultLanguageBox.Size = new System.Drawing.Size(121, 25);
			this.DefaultLanguageBox.Sorted = true;
			this.DefaultLanguageBox.ToolTipText = "Default language to use";
			this.DefaultLanguageBox.SelectedIndexChanged += new System.EventHandler(this.DefaultLanguageBox_SelectedIndexChanged);
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
							| System.Windows.Forms.AnchorStyles.Left)
							| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.StringListBox);
			this.groupBox1.Controls.Add(this.toolStrip2);
			this.groupBox1.Location = new System.Drawing.Point(0, 214);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(955, 369);
			this.groupBox1.TabIndex = 4;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Language strings :";
			// 
			// StringListBox
			// 
			this.StringListBox.AllowColumnReorder = true;
			this.StringListBox.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.IDColumn,
            this.TextColumn});
			this.StringListBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.StringListBox.FullRowSelect = true;
			this.StringListBox.GridLines = true;
			this.StringListBox.LabelEdit = true;
			this.StringListBox.Location = new System.Drawing.Point(3, 41);
			this.StringListBox.Name = "StringListBox";
			this.StringListBox.Size = new System.Drawing.Size(949, 325);
			this.StringListBox.Sorting = System.Windows.Forms.SortOrder.Ascending;
			this.StringListBox.TabIndex = 2;
			this.StringListBox.UseCompatibleStateImageBehavior = false;
			this.StringListBox.View = System.Windows.Forms.View.Details;
			this.StringListBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.StringListBox_MouseDoubleClick);
			this.StringListBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.StringListBox_MouseClick);
			this.StringListBox.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.StringListBox_AfterLabelEdit);
			this.StringListBox.SelectedIndexChanged += new System.EventHandler(this.StringListBox_SelectedIndexChanged);
			this.StringListBox.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.StringListBox_ColumnClick);
			// 
			// IDColumn
			// 
			this.IDColumn.Text = "ID";
			// 
			// TextColumn
			// 
			this.TextColumn.Text = "Text";
			this.TextColumn.Width = 600;
			// 
			// toolStrip2
			// 
			this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel3,
            this.CurrentLanguageBox,
            this.toolStripSeparator4,
            this.RemoveLanguageBox,
            this.toolStripSeparator5,
            this.toolStripLabel2,
            this.NewLanguageBox,
            this.AddNewLanguageBox});
			this.toolStrip2.Location = new System.Drawing.Point(3, 16);
			this.toolStrip2.Name = "toolStrip2";
			this.toolStrip2.Size = new System.Drawing.Size(949, 25);
			this.toolStrip2.TabIndex = 1;
			this.toolStrip2.Text = "toolStrip2";
			// 
			// toolStripLabel3
			// 
			this.toolStripLabel3.Name = "toolStripLabel3";
			this.toolStripLabel3.Size = new System.Drawing.Size(105, 22);
			this.toolStripLabel3.Text = "Current language :";
			// 
			// CurrentLanguageBox
			// 
			this.CurrentLanguageBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.CurrentLanguageBox.Name = "CurrentLanguageBox";
			this.CurrentLanguageBox.Size = new System.Drawing.Size(121, 25);
			this.CurrentLanguageBox.Sorted = true;
			this.CurrentLanguageBox.SelectedIndexChanged += new System.EventHandler(this.CurrentLanguageBox_SelectedIndexChanged);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
			// 
			// RemoveLanguageBox
			// 
			this.RemoveLanguageBox.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.RemoveLanguageBox.Image = ((System.Drawing.Image)(resources.GetObject("RemoveLanguageBox.Image")));
			this.RemoveLanguageBox.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.RemoveLanguageBox.Name = "RemoveLanguageBox";
			this.RemoveLanguageBox.Size = new System.Drawing.Size(23, 22);
			this.RemoveLanguageBox.Text = "Remove current language";
			this.RemoveLanguageBox.Click += new System.EventHandler(this.RemoveLanguageBox_Click);
			// 
			// toolStripSeparator5
			// 
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
			// 
			// toolStripLabel2
			// 
			this.toolStripLabel2.Name = "toolStripLabel2";
			this.toolStripLabel2.Size = new System.Drawing.Size(121, 22);
			this.toolStripLabel2.Text = "Add a new language :";
			// 
			// NewLanguageBox
			// 
			this.NewLanguageBox.Name = "NewLanguageBox";
			this.NewLanguageBox.Size = new System.Drawing.Size(100, 25);
			// 
			// AddNewLanguageBox
			// 
			this.AddNewLanguageBox.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.AddNewLanguageBox.Image = ((System.Drawing.Image)(resources.GetObject("AddNewLanguageBox.Image")));
			this.AddNewLanguageBox.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.AddNewLanguageBox.Name = "AddNewLanguageBox";
			this.AddNewLanguageBox.Size = new System.Drawing.Size(23, 22);
			this.AddNewLanguageBox.Text = "Adds a new language";
			this.AddNewLanguageBox.Click += new System.EventHandler(this.AddNewLanguageBox_Click);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.TranslatedTextBox);
			this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
			this.groupBox2.Location = new System.Drawing.Point(0, 25);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(955, 107);
			this.groupBox2.TabIndex = 5;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Translated text :";
			// 
			// Default
			// 
			this.Default.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
							| System.Windows.Forms.AnchorStyles.Right)));
			this.Default.Controls.Add(this.OriginalTextBox);
			this.Default.Location = new System.Drawing.Point(0, 135);
			this.Default.Name = "Default";
			this.Default.Size = new System.Drawing.Size(955, 73);
			this.Default.TabIndex = 7;
			this.Default.TabStop = false;
			this.Default.Text = "Default text :";
			// 
			// OriginalTextBox
			// 
			this.OriginalTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.OriginalTextBox.Location = new System.Drawing.Point(3, 16);
			this.OriginalTextBox.Multiline = true;
			this.OriginalTextBox.Name = "OriginalTextBox";
			this.OriginalTextBox.ReadOnly = true;
			this.OriginalTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.OriginalTextBox.Size = new System.Drawing.Size(949, 54);
			this.OriginalTextBox.TabIndex = 0;
			// 
			// StringTableForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(955, 581);
			this.Controls.Add(this.Default);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.toolStrip1);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "StringTableForm";
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.toolStrip2.ResumeLayout(false);
			this.toolStrip2.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.Default.ResumeLayout(false);
			this.Default.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox TranslatedTextBox;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton OpenBox;
		private System.Windows.Forms.ToolStripButton SaveBox;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.ToolStrip toolStrip2;
		private System.Windows.Forms.GroupBox Default;
		private System.Windows.Forms.TextBox OriginalTextBox;
		private System.Windows.Forms.ToolStripLabel toolStripLabel3;
		private System.Windows.Forms.ToolStripComboBox CurrentLanguageBox;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ListView StringListBox;
		private System.Windows.Forms.ColumnHeader IDColumn;
		private System.Windows.Forms.ColumnHeader TextColumn;
		private System.Windows.Forms.ToolStripButton AddAsNewStringBox;
		private System.Windows.Forms.ToolStripButton DeleteStringBox;
		private System.Windows.Forms.ToolStripLabel toolStripLabel1;
		private System.Windows.Forms.ToolStripComboBox DefaultLanguageBox;
		private System.Windows.Forms.ToolStripLabel toolStripLabel2;
		private System.Windows.Forms.ToolStripTextBox NewLanguageBox;
		private System.Windows.Forms.ToolStripButton AddNewLanguageBox;
		private System.Windows.Forms.ToolStripButton ClearStringBox;
		private System.Windows.Forms.ToolStripButton WriteStringBox;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripButton RemoveLanguageBox;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;

	}
}
