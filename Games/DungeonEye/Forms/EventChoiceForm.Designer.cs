namespace DungeonEye.Forms
{
	partial class EventChoiceForm
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
			this.CloseBox = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.NameBox = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.VisibleBox = new System.Windows.Forms.CheckBox();
			this.AutoTriggerBox = new System.Windows.Forms.CheckBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.ItemsBox = new System.Windows.Forms.ListBox();
			this.AddItemBox = new System.Windows.Forms.Button();
			this.RemoveItemBox = new System.Windows.Forms.Button();
			this.KeepItemBox = new System.Windows.Forms.CheckBox();
			this.AddActionBox = new System.Windows.Forms.Button();
			this.RemoveActionBox = new System.Windows.Forms.Button();
			this.MoveUpActionBox = new System.Windows.Forms.Button();
			this.MoveDownActionBox = new System.Windows.Forms.Button();
			this.ActionsBox = new System.Windows.Forms.ListBox();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.SuspendLayout();
			// 
			// CloseBox
			// 
			this.CloseBox.Anchor = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.CloseBox.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.CloseBox.Location = new System.Drawing.Point(421, 431);
			this.CloseBox.Name = "CloseBox";
			this.CloseBox.Size = new System.Drawing.Size(75, 23);
			this.CloseBox.TabIndex = 0;
			this.CloseBox.Text = "Close";
			this.CloseBox.UseVisualStyleBackColor = true;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.ActionsBox);
			this.groupBox1.Controls.Add(this.MoveDownActionBox);
			this.groupBox1.Controls.Add(this.MoveUpActionBox);
			this.groupBox1.Controls.Add(this.RemoveActionBox);
			this.groupBox1.Controls.Add(this.AddActionBox);
			this.groupBox1.Location = new System.Drawing.Point(12, 201);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(484, 219);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Actions :";
			// 
			// NameBox
			// 
			this.NameBox.Location = new System.Drawing.Point(132, 12);
			this.NameBox.Name = "NameBox";
			this.NameBox.Size = new System.Drawing.Size(269, 20);
			this.NameBox.TabIndex = 4;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(51, 15);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(75, 13);
			this.label1.TabIndex = 5;
			this.label1.Text = "Choice name :";
			// 
			// VisibleBox
			// 
			this.VisibleBox.AutoSize = true;
			this.VisibleBox.Location = new System.Drawing.Point(6, 19);
			this.VisibleBox.Name = "VisibleBox";
			this.VisibleBox.Size = new System.Drawing.Size(56, 17);
			this.VisibleBox.TabIndex = 6;
			this.VisibleBox.Text = "Visible";
			this.VisibleBox.UseVisualStyleBackColor = true;
			// 
			// AutoTriggerBox
			// 
			this.AutoTriggerBox.AutoSize = true;
			this.AutoTriggerBox.Location = new System.Drawing.Point(6, 42);
			this.AutoTriggerBox.Name = "AutoTriggerBox";
			this.AutoTriggerBox.Size = new System.Drawing.Size(177, 17);
			this.AutoTriggerBox.TabIndex = 6;
			this.AutoTriggerBox.Text = "Choice is automatically triggered";
			this.AutoTriggerBox.UseVisualStyleBackColor = true;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.VisibleBox);
			this.groupBox2.Controls.Add(this.AutoTriggerBox);
			this.groupBox2.Location = new System.Drawing.Point(12, 38);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(234, 157);
			this.groupBox2.TabIndex = 7;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Properties :";
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.KeepItemBox);
			this.groupBox3.Controls.Add(this.RemoveItemBox);
			this.groupBox3.Controls.Add(this.AddItemBox);
			this.groupBox3.Controls.Add(this.ItemsBox);
			this.groupBox3.Location = new System.Drawing.Point(252, 38);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(244, 157);
			this.groupBox3.TabIndex = 8;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Required items :";
			// 
			// ItemsBox
			// 
			this.ItemsBox.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.ItemsBox.FormattingEnabled = true;
			this.ItemsBox.Location = new System.Drawing.Point(6, 19);
			this.ItemsBox.Name = "ItemsBox";
			this.ItemsBox.Size = new System.Drawing.Size(120, 121);
			this.ItemsBox.Sorted = true;
			this.ItemsBox.TabIndex = 0;
			// 
			// AddItemBox
			// 
			this.AddItemBox.Location = new System.Drawing.Point(132, 19);
			this.AddItemBox.Name = "AddItemBox";
			this.AddItemBox.Size = new System.Drawing.Size(75, 23);
			this.AddItemBox.TabIndex = 1;
			this.AddItemBox.Text = "Add";
			this.AddItemBox.UseVisualStyleBackColor = true;
			// 
			// RemoveItemBox
			// 
			this.RemoveItemBox.Location = new System.Drawing.Point(132, 49);
			this.RemoveItemBox.Name = "RemoveItemBox";
			this.RemoveItemBox.Size = new System.Drawing.Size(75, 23);
			this.RemoveItemBox.TabIndex = 2;
			this.RemoveItemBox.Text = "Remove";
			this.RemoveItemBox.UseVisualStyleBackColor = true;
			// 
			// KeepItemBox
			// 
			this.KeepItemBox.AutoSize = true;
			this.KeepItemBox.Location = new System.Drawing.Point(132, 78);
			this.KeepItemBox.Name = "KeepItemBox";
			this.KeepItemBox.Size = new System.Drawing.Size(99, 17);
			this.KeepItemBox.TabIndex = 3;
			this.KeepItemBox.Text = "Remove item(s)";
			this.KeepItemBox.UseVisualStyleBackColor = true;
			// 
			// AddActionBox
			// 
			this.AddActionBox.Location = new System.Drawing.Point(403, 19);
			this.AddActionBox.Name = "AddActionBox";
			this.AddActionBox.Size = new System.Drawing.Size(75, 23);
			this.AddActionBox.TabIndex = 0;
			this.AddActionBox.Text = "Add";
			this.AddActionBox.UseVisualStyleBackColor = true;
			// 
			// RemoveActionBox
			// 
			this.RemoveActionBox.Location = new System.Drawing.Point(403, 48);
			this.RemoveActionBox.Name = "RemoveActionBox";
			this.RemoveActionBox.Size = new System.Drawing.Size(75, 23);
			this.RemoveActionBox.TabIndex = 0;
			this.RemoveActionBox.Text = "Remove";
			this.RemoveActionBox.UseVisualStyleBackColor = true;
			// 
			// MoveUpActionBox
			// 
			this.MoveUpActionBox.Location = new System.Drawing.Point(403, 150);
			this.MoveUpActionBox.Name = "MoveUpActionBox";
			this.MoveUpActionBox.Size = new System.Drawing.Size(75, 23);
			this.MoveUpActionBox.TabIndex = 0;
			this.MoveUpActionBox.Text = "Up";
			this.MoveUpActionBox.UseVisualStyleBackColor = true;
			// 
			// MoveDownActionBox
			// 
			this.MoveDownActionBox.Location = new System.Drawing.Point(403, 179);
			this.MoveDownActionBox.Name = "MoveDownActionBox";
			this.MoveDownActionBox.Size = new System.Drawing.Size(75, 23);
			this.MoveDownActionBox.TabIndex = 0;
			this.MoveDownActionBox.Text = "Down";
			this.MoveDownActionBox.UseVisualStyleBackColor = true;
			// 
			// ActionsBox
			// 
			this.ActionsBox.FormattingEnabled = true;
			this.ActionsBox.Location = new System.Drawing.Point(6, 19);
			this.ActionsBox.Name = "ActionsBox";
			this.ActionsBox.Size = new System.Drawing.Size(391, 186);
			this.ActionsBox.TabIndex = 1;
			// 
			// EventChoiceForm
			// 
			this.AcceptButton = this.CloseBox;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(508, 466);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.NameBox);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.CloseBox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "EventChoiceForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Event choice wizard";
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button CloseBox;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox NameBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox VisibleBox;
		private System.Windows.Forms.CheckBox AutoTriggerBox;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.CheckBox KeepItemBox;
		private System.Windows.Forms.Button RemoveItemBox;
		private System.Windows.Forms.Button AddItemBox;
		private System.Windows.Forms.ListBox ItemsBox;
		private System.Windows.Forms.ListBox ActionsBox;
		private System.Windows.Forms.Button MoveDownActionBox;
		private System.Windows.Forms.Button MoveUpActionBox;
		private System.Windows.Forms.Button RemoveActionBox;
		private System.Windows.Forms.Button AddActionBox;
	}
}