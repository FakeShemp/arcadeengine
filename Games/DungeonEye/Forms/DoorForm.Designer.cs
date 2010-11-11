namespace DungeonEye.Forms
{
	partial class DoorForm
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
			this.DoneBox = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.ItemPanel = new System.Windows.Forms.Panel();
			this.PicklockBox = new System.Windows.Forms.NumericUpDown();
			this.ItemNameBox = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.ItemConsumBox = new System.Windows.Forms.CheckBox();
			this.ItemRadioBox = new System.Windows.Forms.RadioButton();
			this.EventRadioBox = new System.Windows.Forms.RadioButton();
			this.ButtonRadioBox = new System.Windows.Forms.RadioButton();
			this.DoorTypeBox = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.BreakValueBox = new System.Windows.Forms.NumericUpDown();
			this.IsBreakableBox = new System.Windows.Forms.CheckBox();
			this.DoorStateBox = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.ItemPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.PicklockBox)).BeginInit();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.BreakValueBox)).BeginInit();
			this.SuspendLayout();
			// 
			// DoneBox
			// 
			this.DoneBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.DoneBox.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.DoneBox.Location = new System.Drawing.Point(252, 195);
			this.DoneBox.Name = "DoneBox";
			this.DoneBox.Size = new System.Drawing.Size(75, 23);
			this.DoneBox.TabIndex = 0;
			this.DoneBox.Text = "Done";
			this.DoneBox.UseVisualStyleBackColor = true;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.ItemPanel);
			this.groupBox1.Controls.Add(this.ItemRadioBox);
			this.groupBox1.Controls.Add(this.EventRadioBox);
			this.groupBox1.Controls.Add(this.ButtonRadioBox);
			this.groupBox1.Location = new System.Drawing.Point(184, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(148, 172);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Opens by :";
			// 
			// ItemPanel
			// 
			this.ItemPanel.Controls.Add(this.PicklockBox);
			this.ItemPanel.Controls.Add(this.ItemNameBox);
			this.ItemPanel.Controls.Add(this.label2);
			this.ItemPanel.Controls.Add(this.ItemConsumBox);
			this.ItemPanel.Location = new System.Drawing.Point(6, 88);
			this.ItemPanel.Name = "ItemPanel";
			this.ItemPanel.Size = new System.Drawing.Size(133, 77);
			this.ItemPanel.TabIndex = 5;
			// 
			// PicklockBox
			// 
			this.PicklockBox.Location = new System.Drawing.Point(66, 50);
			this.PicklockBox.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
			this.PicklockBox.Name = "PicklockBox";
			this.PicklockBox.Size = new System.Drawing.Size(59, 20);
			this.PicklockBox.TabIndex = 4;
			this.PicklockBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.PicklockBox.ThousandsSeparator = true;
			this.PicklockBox.ValueChanged += new System.EventHandler(this.PicklockBox_ValueChanged);
			// 
			// ItemNameBox
			// 
			this.ItemNameBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ItemNameBox.FormattingEnabled = true;
			this.ItemNameBox.Location = new System.Drawing.Point(3, 3);
			this.ItemNameBox.Name = "ItemNameBox";
			this.ItemNameBox.Size = new System.Drawing.Size(121, 21);
			this.ItemNameBox.Sorted = true;
			this.ItemNameBox.TabIndex = 1;
			this.ItemNameBox.SelectedIndexChanged += new System.EventHandler(this.ItemNameBox_SelectedIndexChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(3, 52);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(57, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "Pick lock :";
			// 
			// ItemConsumBox
			// 
			this.ItemConsumBox.AutoSize = true;
			this.ItemConsumBox.Location = new System.Drawing.Point(3, 30);
			this.ItemConsumBox.Name = "ItemConsumBox";
			this.ItemConsumBox.Size = new System.Drawing.Size(70, 17);
			this.ItemConsumBox.TabIndex = 2;
			this.ItemConsumBox.Text = "Consume";
			this.ItemConsumBox.UseVisualStyleBackColor = true;
			this.ItemConsumBox.CheckedChanged += new System.EventHandler(this.ItemDisappearBox_CheckedChanged);
			// 
			// ItemRadioBox
			// 
			this.ItemRadioBox.AutoSize = true;
			this.ItemRadioBox.Location = new System.Drawing.Point(6, 65);
			this.ItemRadioBox.Name = "ItemRadioBox";
			this.ItemRadioBox.Size = new System.Drawing.Size(45, 17);
			this.ItemRadioBox.TabIndex = 0;
			this.ItemRadioBox.TabStop = true;
			this.ItemRadioBox.Text = "Item";
			this.ItemRadioBox.UseVisualStyleBackColor = true;
			this.ItemRadioBox.CheckedChanged += new System.EventHandler(this.OpensBy_CheckedChanged);
			// 
			// EventRadioBox
			// 
			this.EventRadioBox.AutoSize = true;
			this.EventRadioBox.Location = new System.Drawing.Point(6, 42);
			this.EventRadioBox.Name = "EventRadioBox";
			this.EventRadioBox.Size = new System.Drawing.Size(53, 17);
			this.EventRadioBox.TabIndex = 0;
			this.EventRadioBox.TabStop = true;
			this.EventRadioBox.Text = "Event";
			this.EventRadioBox.UseVisualStyleBackColor = true;
			this.EventRadioBox.CheckedChanged += new System.EventHandler(this.OpensBy_CheckedChanged);
			// 
			// ButtonRadioBox
			// 
			this.ButtonRadioBox.AutoSize = true;
			this.ButtonRadioBox.Location = new System.Drawing.Point(6, 19);
			this.ButtonRadioBox.Name = "ButtonRadioBox";
			this.ButtonRadioBox.Size = new System.Drawing.Size(56, 17);
			this.ButtonRadioBox.TabIndex = 0;
			this.ButtonRadioBox.TabStop = true;
			this.ButtonRadioBox.Text = "Button";
			this.ButtonRadioBox.UseVisualStyleBackColor = true;
			this.ButtonRadioBox.CheckedChanged += new System.EventHandler(this.OpensBy_CheckedChanged);
			// 
			// DoorTypeBox
			// 
			this.DoorTypeBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.DoorTypeBox.Location = new System.Drawing.Point(12, 29);
			this.DoorTypeBox.Name = "DoorTypeBox";
			this.DoorTypeBox.Size = new System.Drawing.Size(166, 21);
			this.DoorTypeBox.Sorted = true;
			this.DoorTypeBox.TabIndex = 2;
			this.DoorTypeBox.SelectedIndexChanged += new System.EventHandler(this.DoorTypeBox_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(59, 13);
			this.label1.TabIndex = 3;
			this.label1.Text = "Door type :";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.BreakValueBox);
			this.groupBox2.Controls.Add(this.IsBreakableBox);
			this.groupBox2.Location = new System.Drawing.Point(12, 113);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(166, 105);
			this.groupBox2.TabIndex = 4;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Properties :";
			// 
			// BreakValueBox
			// 
			this.BreakValueBox.Location = new System.Drawing.Point(96, 20);
			this.BreakValueBox.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
			this.BreakValueBox.Name = "BreakValueBox";
			this.BreakValueBox.Size = new System.Drawing.Size(59, 20);
			this.BreakValueBox.TabIndex = 4;
			this.BreakValueBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.BreakValueBox.ThousandsSeparator = true;
			this.BreakValueBox.ValueChanged += new System.EventHandler(this.BreakValueBox_ValueChanged);
			// 
			// IsBreakableBox
			// 
			this.IsBreakableBox.AutoSize = true;
			this.IsBreakableBox.Location = new System.Drawing.Point(6, 21);
			this.IsBreakableBox.Name = "IsBreakableBox";
			this.IsBreakableBox.Size = new System.Drawing.Size(84, 17);
			this.IsBreakableBox.TabIndex = 3;
			this.IsBreakableBox.Text = "Is breakable";
			this.IsBreakableBox.UseVisualStyleBackColor = true;
			this.IsBreakableBox.CheckedChanged += new System.EventHandler(this.IsBreakableBox_CheckedChanged);
			// 
			// DoorStateBox
			// 
			this.DoorStateBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.DoorStateBox.Location = new System.Drawing.Point(12, 78);
			this.DoorStateBox.Name = "DoorStateBox";
			this.DoorStateBox.Size = new System.Drawing.Size(166, 21);
			this.DoorStateBox.Sorted = true;
			this.DoorStateBox.TabIndex = 2;
			this.DoorStateBox.SelectedIndexChanged += new System.EventHandler(this.DoorStateBox_SelectedIndexChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(12, 58);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(38, 13);
			this.label3.TabIndex = 3;
			this.label3.Text = "State :";
			// 
			// DoorForm
			// 
			this.AcceptButton = this.DoneBox;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(339, 230);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.DoorStateBox);
			this.Controls.Add(this.DoorTypeBox);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.DoneBox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "DoorForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Door wizard";
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DoorForm_KeyDown);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ItemPanel.ResumeLayout(false);
			this.ItemPanel.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.PicklockBox)).EndInit();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.BreakValueBox)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button DoneBox;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ComboBox DoorTypeBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.NumericUpDown BreakValueBox;
		private System.Windows.Forms.CheckBox IsBreakableBox;
		private System.Windows.Forms.NumericUpDown PicklockBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.CheckBox ItemConsumBox;
		private System.Windows.Forms.ComboBox ItemNameBox;
		private System.Windows.Forms.RadioButton ItemRadioBox;
		private System.Windows.Forms.RadioButton ButtonRadioBox;
		private System.Windows.Forms.ComboBox DoorStateBox;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.RadioButton EventRadioBox;
		private System.Windows.Forms.Panel ItemPanel;
	}
}