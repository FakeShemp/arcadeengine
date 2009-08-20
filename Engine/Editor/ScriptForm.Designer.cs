namespace ArcEngine.Editor
{
	partial class ScriptForm
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("");
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScriptForm));
			this.ErrorListView = new System.Windows.Forms.ListView();
			this.ErrorColumn = new System.Windows.Forms.ColumnHeader();
			this.LineColumn = new System.Windows.Forms.ColumnHeader();
			this.ReportStrip = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
			this.StatusReport = new System.Windows.Forms.ToolStripStatusLabel();
			this.LogStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.ScriptTxt = new DigitalRune.Windows.TextEditor.TextEditorControl();
			this.CommandStrip = new System.Windows.Forms.ToolStrip();
			this.NewBox = new System.Windows.Forms.ToolStripButton();
			this.OpenBox = new System.Windows.Forms.ToolStripButton();
			this.SaveBox = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
			this.CutBox = new System.Windows.Forms.ToolStripButton();
			this.CopyBox = new System.Windows.Forms.ToolStripButton();
			this.PasteBox = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.UndoBox = new System.Windows.Forms.ToolStripButton();
			this.ReDoBox = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.CompileButton = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.InsertModelButton = new System.Windows.Forms.ToolStripSplitButton();
			this.ReportStrip.SuspendLayout();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.CommandStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// ErrorListView
			// 
			this.ErrorListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ErrorColumn,
            this.LineColumn});
			this.ErrorListView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ErrorListView.GridLines = true;
			this.ErrorListView.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem2});
			this.ErrorListView.Location = new System.Drawing.Point(0, 0);
			this.ErrorListView.MultiSelect = false;
			this.ErrorListView.Name = "ErrorListView";
			this.ErrorListView.Size = new System.Drawing.Size(732, 121);
			this.ErrorListView.TabIndex = 1;
			this.ErrorListView.UseCompatibleStateImageBehavior = false;
			this.ErrorListView.View = System.Windows.Forms.View.Details;
			this.ErrorListView.ItemActivate += new System.EventHandler(this.ListViewError_OnItemActive);
			// 
			// ErrorColumn
			// 
			this.ErrorColumn.Text = "Error";
			this.ErrorColumn.Width = 548;
			// 
			// LineColumn
			// 
			this.LineColumn.Text = "Line";
			this.LineColumn.Width = 118;
			// 
			// ReportStrip
			// 
			this.ReportStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel2,
            this.StatusReport,
            this.LogStatusLabel,
            this.toolStripStatusLabel1});
			this.ReportStrip.Location = new System.Drawing.Point(0, 455);
			this.ReportStrip.Name = "ReportStrip";
			this.ReportStrip.ShowItemToolTips = true;
			this.ReportStrip.Size = new System.Drawing.Size(732, 22);
			this.ReportStrip.SizingGrip = false;
			this.ReportStrip.TabIndex = 5;
			// 
			// toolStripStatusLabel2
			// 
			this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
			this.toolStripStatusLabel2.Size = new System.Drawing.Size(489, 17);
			this.toolStripStatusLabel2.Spring = true;
			// 
			// StatusReport
			// 
			this.StatusReport.Name = "StatusReport";
			this.StatusReport.Size = new System.Drawing.Size(86, 17);
			this.StatusReport.Text = "Offset xxx : xxxx";
			// 
			// LogStatusLabel
			// 
			this.LogStatusLabel.Image = ((System.Drawing.Image)(resources.GetObject("LogStatusLabel.Image")));
			this.LogStatusLabel.Name = "LogStatusLabel";
			this.LogStatusLabel.Size = new System.Drawing.Size(61, 17);
			this.LogStatusLabel.Text = "Error(s)";
			// 
			// toolStripStatusLabel1
			// 
			this.toolStripStatusLabel1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripStatusLabel1.Image")));
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			this.toolStripStatusLabel1.Size = new System.Drawing.Size(81, 17);
			this.toolStripStatusLabel1.Text = "Warning(s)";
			this.toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.ScriptTxt);
			this.splitContainer1.Panel1.Controls.Add(this.CommandStrip);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.ErrorListView);
			this.splitContainer1.Size = new System.Drawing.Size(732, 455);
			this.splitContainer1.SplitterDistance = 330;
			this.splitContainer1.TabIndex = 7;
			// 
			// ScriptTxt
			// 
			this.ScriptTxt.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ScriptTxt.Location = new System.Drawing.Point(0, 25);
			this.ScriptTxt.Name = "ScriptTxt";
			this.ScriptTxt.ShowVRuler = false;
			this.ScriptTxt.Size = new System.Drawing.Size(732, 305);
			this.ScriptTxt.TabIndex = 8;
			this.ScriptTxt.Text = "Source code...";
			this.ScriptTxt.CompletionRequest += new System.EventHandler<DigitalRune.Windows.TextEditor.Completion.CompletionEventArgs>(this.CompletionRequest);
			// 
			// CommandStrip
			// 
			this.CommandStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.CommandStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewBox,
            this.OpenBox,
            this.SaveBox,
            this.toolStripSeparator,
            this.CutBox,
            this.CopyBox,
            this.PasteBox,
            this.toolStripSeparator4,
            this.UndoBox,
            this.ReDoBox,
            this.toolStripSeparator2,
            this.toolStripSeparator1,
            this.CompileButton,
            this.toolStripSeparator3,
            this.InsertModelButton});
			this.CommandStrip.Location = new System.Drawing.Point(0, 0);
			this.CommandStrip.Name = "CommandStrip";
			this.CommandStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.CommandStrip.Size = new System.Drawing.Size(732, 25);
			this.CommandStrip.TabIndex = 6;
			this.CommandStrip.Text = "toolStrip1";
			// 
			// NewBox
			// 
			this.NewBox.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.NewBox.Image = ((System.Drawing.Image)(resources.GetObject("NewBox.Image")));
			this.NewBox.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.NewBox.Name = "NewBox";
			this.NewBox.Size = new System.Drawing.Size(23, 22);
			this.NewBox.Text = "New";
			this.NewBox.Click += new System.EventHandler(this.ClearScriptBox_Click);
			// 
			// OpenBox
			// 
			this.OpenBox.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.OpenBox.Image = ((System.Drawing.Image)(resources.GetObject("OpenBox.Image")));
			this.OpenBox.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.OpenBox.Name = "OpenBox";
			this.OpenBox.Size = new System.Drawing.Size(23, 22);
			this.OpenBox.Text = "Open file...";
			this.OpenBox.Click += new System.EventHandler(this.LoadButton_Click);
			// 
			// SaveBox
			// 
			this.SaveBox.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.SaveBox.Image = ((System.Drawing.Image)(resources.GetObject("SaveBox.Image")));
			this.SaveBox.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.SaveBox.Name = "SaveBox";
			this.SaveBox.Size = new System.Drawing.Size(23, 22);
			this.SaveBox.Text = "Save source to file...";
			// 
			// toolStripSeparator
			// 
			this.toolStripSeparator.Name = "toolStripSeparator";
			this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
			// 
			// CutBox
			// 
			this.CutBox.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.CutBox.Image = ((System.Drawing.Image)(resources.GetObject("CutBox.Image")));
			this.CutBox.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.CutBox.Name = "CutBox";
			this.CutBox.Size = new System.Drawing.Size(23, 22);
			this.CutBox.Text = "Cut";
			// 
			// CopyBox
			// 
			this.CopyBox.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.CopyBox.Image = ((System.Drawing.Image)(resources.GetObject("CopyBox.Image")));
			this.CopyBox.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.CopyBox.Name = "CopyBox";
			this.CopyBox.Size = new System.Drawing.Size(23, 22);
			this.CopyBox.Text = "Copy";
			// 
			// PasteBox
			// 
			this.PasteBox.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.PasteBox.Image = ((System.Drawing.Image)(resources.GetObject("PasteBox.Image")));
			this.PasteBox.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.PasteBox.Name = "PasteBox";
			this.PasteBox.Size = new System.Drawing.Size(23, 22);
			this.PasteBox.Text = "Paste";
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
			// 
			// UndoBox
			// 
			this.UndoBox.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.UndoBox.Image = ((System.Drawing.Image)(resources.GetObject("UndoBox.Image")));
			this.UndoBox.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.UndoBox.Name = "UndoBox";
			this.UndoBox.Size = new System.Drawing.Size(23, 22);
			this.UndoBox.Text = "Undo";
			// 
			// ReDoBox
			// 
			this.ReDoBox.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.ReDoBox.Image = ((System.Drawing.Image)(resources.GetObject("ReDoBox.Image")));
			this.ReDoBox.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.ReDoBox.Name = "ReDoBox";
			this.ReDoBox.Size = new System.Drawing.Size(23, 22);
			this.ReDoBox.Text = "Redo";
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// CompileButton
			// 
			this.CompileButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.CompileButton.Image = ((System.Drawing.Image)(resources.GetObject("CompileButton.Image")));
			this.CompileButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.CompileButton.Name = "CompileButton";
			this.CompileButton.Size = new System.Drawing.Size(23, 22);
			this.CompileButton.Text = "Compile...";
			this.CompileButton.Click += new System.EventHandler(this.Compile_OnClick);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
			// 
			// InsertModelButton
			// 
			this.InsertModelButton.Name = "InsertModelButton";
			this.InsertModelButton.Size = new System.Drawing.Size(130, 22);
			this.InsertModelButton.Text = "Implement Interface";
			// 
			// ScriptForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(732, 477);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.ReportStrip);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "ScriptForm";
			this.ShowInTaskbar = false;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ScriptForm_FormClosing);
			this.ReportStrip.ResumeLayout(false);
			this.ReportStrip.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel1.PerformLayout();
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			this.CommandStrip.ResumeLayout(false);
			this.CommandStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListView ErrorListView;
		private System.Windows.Forms.ColumnHeader ErrorColumn;
		private System.Windows.Forms.ColumnHeader LineColumn;
		private System.Windows.Forms.StatusStrip ReportStrip;
		private System.Windows.Forms.ToolStripStatusLabel StatusReport;
		private System.Windows.Forms.ToolStripStatusLabel LogStatusLabel;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.ToolStrip CommandStrip;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton CompileButton;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
		private DigitalRune.Windows.TextEditor.TextEditorControl ScriptTxt;
		private System.Windows.Forms.ToolStripButton NewBox;
		private System.Windows.Forms.ToolStripButton OpenBox;
		private System.Windows.Forms.ToolStripButton SaveBox;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
		private System.Windows.Forms.ToolStripButton CutBox;
		private System.Windows.Forms.ToolStripButton CopyBox;
		private System.Windows.Forms.ToolStripButton PasteBox;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripButton UndoBox;
		private System.Windows.Forms.ToolStripButton ReDoBox;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripSplitButton InsertModelButton;
	}
}
