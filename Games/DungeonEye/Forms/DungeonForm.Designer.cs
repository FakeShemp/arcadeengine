namespace DungeonEye.Forms
{
	partial class DungeonForm
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DungeonForm));
			this.DungeonStripBox = new System.Windows.Forms.ToolStrip();
			this.ResetOffsetBox = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.AddMazeButton = new System.Windows.Forms.ToolStripButton();
			this.RemoveMazeButton = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.MazeListBox = new System.Windows.Forms.ToolStripComboBox();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.NoMonstersBox = new System.Windows.Forms.ToolStripButton();
			this.NoGhostsBox = new System.Windows.Forms.ToolStripButton();
			this.ZoneBox = new System.Windows.Forms.ToolStripButton();
			this.AddMonsterBox = new System.Windows.Forms.ToolStripButton();
			this.AddItemBox = new System.Windows.Forms.ToolStripButton();
			this.WallBox = new System.Windows.Forms.ToolStripButton();
			this.StairBox = new System.Windows.Forms.ToolStripButton();
			this.TeleporterBox = new System.Windows.Forms.ToolStripButton();
			this.DoorBox = new System.Windows.Forms.ToolStripButton();
			this.PitBox = new System.Windows.Forms.ToolStripButton();
			this.WrittingBox = new System.Windows.Forms.ToolStripButton();
			this.LauncherBox = new System.Windows.Forms.ToolStripButton();
			this.GeneratorBox = new System.Windows.Forms.ToolStripButton();
			this.SwitchBox = new System.Windows.Forms.ToolStripButton();
			this.FloorSwitchBox = new System.Windows.Forms.ToolStripButton();
			this.FloorDecorationBox = new System.Windows.Forms.ToolStripButton();
			this.DecorationBox = new System.Windows.Forms.ToolStripButton();
			this.EventBox = new System.Windows.Forms.ToolStripButton();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.SquareCoordBox = new System.Windows.Forms.ToolStripStatusLabel();
			this.SquareDescriptionBox = new System.Windows.Forms.ToolStripStatusLabel();
			this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
			this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
			this.DrawTimer = new System.Windows.Forms.Timer(this.components);
			this.glControl = new OpenTK.GLControl();
			this.StrafeRightBox = new System.Windows.Forms.Button();
			this.TurnRightBox = new System.Windows.Forms.Button();
			this.BackwardBox = new System.Windows.Forms.Button();
			this.ForwardBox = new System.Windows.Forms.Button();
			this.StrafeLeftBox = new System.Windows.Forms.Button();
			this.TurnLeftBox = new System.Windows.Forms.Button();
			this.GlPreviewControl = new OpenTK.GLControl();
			this.RightPanel = new System.Windows.Forms.Panel();
			this.PropertiesBox = new System.Windows.Forms.TabControl();
			this.DungeonTab = new System.Windows.Forms.TabPage();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.ItemTileSetBox = new System.Windows.Forms.ComboBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.DungeonNoteBox = new System.Windows.Forms.TextBox();
			this.MazeTab = new System.Windows.Forms.TabPage();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.WallTileSetNameBox = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.DecorationNameBox = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.DefaultDoorBox = new System.Windows.Forms.ComboBox();
			this.LeftPanel = new System.Windows.Forms.Panel();
			this.groupBox5 = new System.Windows.Forms.GroupBox();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.StartLocationBox = new DungeonEye.Forms.TargetControl();
			this.DungeonStripBox.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.RightPanel.SuspendLayout();
			this.PropertiesBox.SuspendLayout();
			this.DungeonTab.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.MazeTab.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.LeftPanel.SuspendLayout();
			this.groupBox5.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.SuspendLayout();
			// 
			// DungeonStripBox
			// 
			this.DungeonStripBox.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ResetOffsetBox,
            this.toolStripSeparator4,
            this.AddMazeButton,
            this.RemoveMazeButton,
            this.toolStripSeparator1,
            this.MazeListBox,
            this.toolStripSeparator2,
            this.NoMonstersBox,
            this.NoGhostsBox,
            this.ZoneBox,
            this.AddMonsterBox,
            this.AddItemBox,
            this.WallBox,
            this.StairBox,
            this.TeleporterBox,
            this.DoorBox,
            this.PitBox,
            this.WrittingBox,
            this.LauncherBox,
            this.GeneratorBox,
            this.SwitchBox,
            this.FloorSwitchBox,
            this.FloorDecorationBox,
            this.DecorationBox,
            this.EventBox});
			this.DungeonStripBox.Location = new System.Drawing.Point(0, 0);
			this.DungeonStripBox.Name = "DungeonStripBox";
			this.DungeonStripBox.Size = new System.Drawing.Size(856, 25);
			this.DungeonStripBox.TabIndex = 2;
			this.DungeonStripBox.Text = "toolStrip1";
			// 
			// ResetOffsetBox
			// 
			this.ResetOffsetBox.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.ResetOffsetBox.Image = ((System.Drawing.Image) (resources.GetObject("ResetOffsetBox.Image")));
			this.ResetOffsetBox.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.ResetOffsetBox.Name = "ResetOffsetBox";
			this.ResetOffsetBox.Size = new System.Drawing.Size(23, 22);
			this.ResetOffsetBox.Text = "Reset offset";
			this.ResetOffsetBox.Click += new System.EventHandler(this.ResetOffsetBox_Click);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
			// 
			// AddMazeButton
			// 
			this.AddMazeButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.AddMazeButton.Image = ((System.Drawing.Image) (resources.GetObject("AddMazeButton.Image")));
			this.AddMazeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.AddMazeButton.Name = "AddMazeButton";
			this.AddMazeButton.Size = new System.Drawing.Size(23, 22);
			this.AddMazeButton.Text = "Adds a maze...";
			this.AddMazeButton.Click += new System.EventHandler(this.AddMazeButton_Click);
			// 
			// RemoveMazeButton
			// 
			this.RemoveMazeButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.RemoveMazeButton.Image = ((System.Drawing.Image) (resources.GetObject("RemoveMazeButton.Image")));
			this.RemoveMazeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.RemoveMazeButton.Name = "RemoveMazeButton";
			this.RemoveMazeButton.Size = new System.Drawing.Size(23, 22);
			this.RemoveMazeButton.Text = "Removes a maze...";
			this.RemoveMazeButton.Click += new System.EventHandler(this.RemoveMazeButton_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// MazeListBox
			// 
			this.MazeListBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.MazeListBox.Name = "MazeListBox";
			this.MazeListBox.Size = new System.Drawing.Size(121, 25);
			this.MazeListBox.Sorted = true;
			this.MazeListBox.SelectedIndexChanged += new System.EventHandler(this.MazeListBox_SelectedIndexChanged);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			// 
			// NoMonstersBox
			// 
			this.NoMonstersBox.CheckOnClick = true;
			this.NoMonstersBox.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.NoMonstersBox.Image = ((System.Drawing.Image) (resources.GetObject("NoMonstersBox.Image")));
			this.NoMonstersBox.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.NoMonstersBox.Name = "NoMonstersBox";
			this.NoMonstersBox.Size = new System.Drawing.Size(23, 22);
			this.NoMonstersBox.Text = "No monsters";
			this.NoMonstersBox.CheckedChanged += new System.EventHandler(this.ToggleStripButtons);
			// 
			// NoGhostsBox
			// 
			this.NoGhostsBox.CheckOnClick = true;
			this.NoGhostsBox.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.NoGhostsBox.Image = ((System.Drawing.Image) (resources.GetObject("NoGhostsBox.Image")));
			this.NoGhostsBox.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.NoGhostsBox.Name = "NoGhostsBox";
			this.NoGhostsBox.Size = new System.Drawing.Size(23, 22);
			this.NoGhostsBox.Text = "No ghost";
			this.NoGhostsBox.CheckedChanged += new System.EventHandler(this.ToggleStripButtons);
			// 
			// ZoneBox
			// 
			this.ZoneBox.CheckOnClick = true;
			this.ZoneBox.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.ZoneBox.Image = ((System.Drawing.Image) (resources.GetObject("ZoneBox.Image")));
			this.ZoneBox.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.ZoneBox.Name = "ZoneBox";
			this.ZoneBox.Size = new System.Drawing.Size(23, 22);
			this.ZoneBox.Text = "Create a new zone...";
			this.ZoneBox.CheckedChanged += new System.EventHandler(this.ToggleStripButtons);
			// 
			// AddMonsterBox
			// 
			this.AddMonsterBox.CheckOnClick = true;
			this.AddMonsterBox.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.AddMonsterBox.Image = ((System.Drawing.Image) (resources.GetObject("AddMonsterBox.Image")));
			this.AddMonsterBox.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.AddMonsterBox.Name = "AddMonsterBox";
			this.AddMonsterBox.Size = new System.Drawing.Size(23, 22);
			this.AddMonsterBox.Text = "Add monster...";
			this.AddMonsterBox.CheckedChanged += new System.EventHandler(this.ToggleStripButtons);
			// 
			// AddItemBox
			// 
			this.AddItemBox.CheckOnClick = true;
			this.AddItemBox.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.AddItemBox.Image = ((System.Drawing.Image) (resources.GetObject("AddItemBox.Image")));
			this.AddItemBox.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.AddItemBox.Name = "AddItemBox";
			this.AddItemBox.Size = new System.Drawing.Size(23, 22);
			this.AddItemBox.Text = "Add item...";
			this.AddItemBox.CheckedChanged += new System.EventHandler(this.ToggleStripButtons);
			// 
			// WallBox
			// 
			this.WallBox.CheckOnClick = true;
			this.WallBox.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.WallBox.Image = ((System.Drawing.Image) (resources.GetObject("WallBox.Image")));
			this.WallBox.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.WallBox.Name = "WallBox";
			this.WallBox.Size = new System.Drawing.Size(23, 22);
			this.WallBox.Text = "Wall";
			this.WallBox.CheckedChanged += new System.EventHandler(this.ToggleStripButtons);
			// 
			// StairBox
			// 
			this.StairBox.CheckOnClick = true;
			this.StairBox.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.StairBox.Image = ((System.Drawing.Image) (resources.GetObject("StairBox.Image")));
			this.StairBox.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.StairBox.Name = "StairBox";
			this.StairBox.Size = new System.Drawing.Size(23, 22);
			this.StairBox.Text = "Stair...";
			this.StairBox.CheckedChanged += new System.EventHandler(this.ToggleStripButtons);
			// 
			// TeleporterBox
			// 
			this.TeleporterBox.CheckOnClick = true;
			this.TeleporterBox.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.TeleporterBox.Image = ((System.Drawing.Image) (resources.GetObject("TeleporterBox.Image")));
			this.TeleporterBox.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.TeleporterBox.Name = "TeleporterBox";
			this.TeleporterBox.Size = new System.Drawing.Size(23, 22);
			this.TeleporterBox.Text = "Teleporter...";
			this.TeleporterBox.CheckedChanged += new System.EventHandler(this.ToggleStripButtons);
			// 
			// DoorBox
			// 
			this.DoorBox.CheckOnClick = true;
			this.DoorBox.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.DoorBox.Image = ((System.Drawing.Image) (resources.GetObject("DoorBox.Image")));
			this.DoorBox.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.DoorBox.Name = "DoorBox";
			this.DoorBox.Size = new System.Drawing.Size(23, 22);
			this.DoorBox.Text = "Door";
			this.DoorBox.CheckedChanged += new System.EventHandler(this.ToggleStripButtons);
			// 
			// PitBox
			// 
			this.PitBox.CheckOnClick = true;
			this.PitBox.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.PitBox.Image = ((System.Drawing.Image) (resources.GetObject("PitBox.Image")));
			this.PitBox.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.PitBox.Name = "PitBox";
			this.PitBox.Size = new System.Drawing.Size(23, 22);
			this.PitBox.Text = "Pit...";
			this.PitBox.CheckedChanged += new System.EventHandler(this.ToggleStripButtons);
			// 
			// WrittingBox
			// 
			this.WrittingBox.CheckOnClick = true;
			this.WrittingBox.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.WrittingBox.Image = ((System.Drawing.Image) (resources.GetObject("WrittingBox.Image")));
			this.WrittingBox.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.WrittingBox.Name = "WrittingBox";
			this.WrittingBox.Size = new System.Drawing.Size(23, 22);
			this.WrittingBox.Text = "Writting...";
			this.WrittingBox.CheckedChanged += new System.EventHandler(this.ToggleStripButtons);
			// 
			// LauncherBox
			// 
			this.LauncherBox.CheckOnClick = true;
			this.LauncherBox.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.LauncherBox.Image = ((System.Drawing.Image) (resources.GetObject("LauncherBox.Image")));
			this.LauncherBox.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.LauncherBox.Name = "LauncherBox";
			this.LauncherBox.Size = new System.Drawing.Size(23, 22);
			this.LauncherBox.Text = "Launcher...";
			this.LauncherBox.CheckedChanged += new System.EventHandler(this.ToggleStripButtons);
			// 
			// GeneratorBox
			// 
			this.GeneratorBox.CheckOnClick = true;
			this.GeneratorBox.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.GeneratorBox.Image = ((System.Drawing.Image) (resources.GetObject("GeneratorBox.Image")));
			this.GeneratorBox.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.GeneratorBox.Name = "GeneratorBox";
			this.GeneratorBox.Size = new System.Drawing.Size(23, 22);
			this.GeneratorBox.Text = "Generator...";
			this.GeneratorBox.CheckedChanged += new System.EventHandler(this.ToggleStripButtons);
			// 
			// SwitchBox
			// 
			this.SwitchBox.CheckOnClick = true;
			this.SwitchBox.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.SwitchBox.Image = ((System.Drawing.Image) (resources.GetObject("SwitchBox.Image")));
			this.SwitchBox.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.SwitchBox.Name = "SwitchBox";
			this.SwitchBox.Size = new System.Drawing.Size(23, 22);
			this.SwitchBox.Text = "Switch...";
			this.SwitchBox.CheckedChanged += new System.EventHandler(this.ToggleStripButtons);
			// 
			// FloorSwitchBox
			// 
			this.FloorSwitchBox.CheckOnClick = true;
			this.FloorSwitchBox.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.FloorSwitchBox.Image = ((System.Drawing.Image) (resources.GetObject("FloorSwitchBox.Image")));
			this.FloorSwitchBox.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.FloorSwitchBox.Name = "FloorSwitchBox";
			this.FloorSwitchBox.Size = new System.Drawing.Size(23, 22);
			this.FloorSwitchBox.Text = "Floor switch...";
			this.FloorSwitchBox.CheckedChanged += new System.EventHandler(this.ToggleStripButtons);
			// 
			// FloorDecorationBox
			// 
			this.FloorDecorationBox.CheckOnClick = true;
			this.FloorDecorationBox.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.FloorDecorationBox.Image = ((System.Drawing.Image) (resources.GetObject("FloorDecorationBox.Image")));
			this.FloorDecorationBox.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.FloorDecorationBox.Name = "FloorDecorationBox";
			this.FloorDecorationBox.Size = new System.Drawing.Size(23, 22);
			this.FloorDecorationBox.Text = "Floor decoration...";
			this.FloorDecorationBox.CheckedChanged += new System.EventHandler(this.ToggleStripButtons);
			// 
			// DecorationBox
			// 
			this.DecorationBox.CheckOnClick = true;
			this.DecorationBox.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.DecorationBox.Image = ((System.Drawing.Image) (resources.GetObject("DecorationBox.Image")));
			this.DecorationBox.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.DecorationBox.Name = "DecorationBox";
			this.DecorationBox.Size = new System.Drawing.Size(23, 22);
			this.DecorationBox.Text = "Decoration...";
			this.DecorationBox.CheckedChanged += new System.EventHandler(this.ToggleStripButtons);
			// 
			// EventBox
			// 
			this.EventBox.CheckOnClick = true;
			this.EventBox.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.EventBox.Image = ((System.Drawing.Image) (resources.GetObject("EventBox.Image")));
			this.EventBox.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.EventBox.Name = "EventBox";
			this.EventBox.Size = new System.Drawing.Size(23, 22);
			this.EventBox.Text = "Event...";
			this.EventBox.CheckedChanged += new System.EventHandler(this.ToggleStripButtons);
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SquareCoordBox,
            this.SquareDescriptionBox});
			this.statusStrip1.Location = new System.Drawing.Point(0, 637);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(1256, 24);
			this.statusStrip1.SizingGrip = false;
			this.statusStrip1.TabIndex = 3;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// SquareCoordBox
			// 
			this.SquareCoordBox.AutoSize = false;
			this.SquareCoordBox.Name = "SquareCoordBox";
			this.SquareCoordBox.Size = new System.Drawing.Size(250, 19);
			this.SquareCoordBox.Text = "...Location...";
			// 
			// SquareDescriptionBox
			// 
			this.SquareDescriptionBox.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides) ((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)));
			this.SquareDescriptionBox.Name = "SquareDescriptionBox";
			this.SquareDescriptionBox.Size = new System.Drawing.Size(991, 19);
			this.SquareDescriptionBox.Spring = true;
			this.SquareDescriptionBox.Text = "...Description...";
			// 
			// hScrollBar1
			// 
			this.hScrollBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.hScrollBar1.Location = new System.Drawing.Point(0, 620);
			this.hScrollBar1.Maximum = 200;
			this.hScrollBar1.Name = "hScrollBar1";
			this.hScrollBar1.Size = new System.Drawing.Size(856, 17);
			this.hScrollBar1.TabIndex = 4;
			// 
			// vScrollBar1
			// 
			this.vScrollBar1.Dock = System.Windows.Forms.DockStyle.Right;
			this.vScrollBar1.Location = new System.Drawing.Point(856, 0);
			this.vScrollBar1.Maximum = 200;
			this.vScrollBar1.Name = "vScrollBar1";
			this.vScrollBar1.Size = new System.Drawing.Size(17, 637);
			this.vScrollBar1.TabIndex = 5;
			// 
			// DrawTimer
			// 
			this.DrawTimer.Tick += new System.EventHandler(this.DrawTimer_Tick);
			// 
			// glControl
			// 
			this.glControl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.glControl.BackColor = System.Drawing.Color.Black;
			this.glControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.glControl.Location = new System.Drawing.Point(0, 0);
			this.glControl.Name = "glControl";
			this.glControl.Size = new System.Drawing.Size(856, 620);
			this.glControl.TabIndex = 7;
			this.glControl.VSync = false;
			this.glControl.Paint += new System.Windows.Forms.PaintEventHandler(this.GlControl_Paint);
			this.glControl.DoubleClick += new System.EventHandler(this.glControl_DoubleClick);
			this.glControl.MouseClick += new System.Windows.Forms.MouseEventHandler(this.glControl_MouseClick);
			this.glControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GlControl_MouseDown);
			this.glControl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.GlControl_MouseMove);
			this.glControl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.GlControl_MouseUp);
			this.glControl.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.GlControl_PreviewKeyDown);
			this.glControl.Resize += new System.EventHandler(this.GlControl_Resize);
			// 
			// StrafeRightBox
			// 
			this.StrafeRightBox.AutoSize = true;
			this.StrafeRightBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.StrafeRightBox.Image = ((System.Drawing.Image) (resources.GetObject("StrafeRightBox.Image")));
			this.StrafeRightBox.Location = new System.Drawing.Point(212, 311);
			this.StrafeRightBox.Name = "StrafeRightBox";
			this.StrafeRightBox.Size = new System.Drawing.Size(46, 40);
			this.StrafeRightBox.TabIndex = 3;
			this.StrafeRightBox.UseVisualStyleBackColor = true;
			this.StrafeRightBox.Click += new System.EventHandler(this.StrafeRightBox_Click);
			// 
			// TurnRightBox
			// 
			this.TurnRightBox.AutoSize = true;
			this.TurnRightBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TurnRightBox.Image = ((System.Drawing.Image) (resources.GetObject("TurnRightBox.Image")));
			this.TurnRightBox.Location = new System.Drawing.Point(212, 265);
			this.TurnRightBox.Name = "TurnRightBox";
			this.TurnRightBox.Size = new System.Drawing.Size(46, 40);
			this.TurnRightBox.TabIndex = 3;
			this.TurnRightBox.UseVisualStyleBackColor = true;
			this.TurnRightBox.Click += new System.EventHandler(this.TurnRightBox_Click);
			// 
			// BackwardBox
			// 
			this.BackwardBox.AutoSize = true;
			this.BackwardBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.BackwardBox.Image = ((System.Drawing.Image) (resources.GetObject("BackwardBox.Image")));
			this.BackwardBox.Location = new System.Drawing.Point(160, 311);
			this.BackwardBox.Name = "BackwardBox";
			this.BackwardBox.Size = new System.Drawing.Size(46, 40);
			this.BackwardBox.TabIndex = 3;
			this.BackwardBox.UseVisualStyleBackColor = true;
			this.BackwardBox.Click += new System.EventHandler(this.BackwardBox_Click);
			// 
			// ForwardBox
			// 
			this.ForwardBox.AutoSize = true;
			this.ForwardBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.ForwardBox.Image = ((System.Drawing.Image) (resources.GetObject("ForwardBox.Image")));
			this.ForwardBox.Location = new System.Drawing.Point(160, 265);
			this.ForwardBox.Name = "ForwardBox";
			this.ForwardBox.Size = new System.Drawing.Size(46, 40);
			this.ForwardBox.TabIndex = 3;
			this.ForwardBox.UseVisualStyleBackColor = true;
			this.ForwardBox.Click += new System.EventHandler(this.ForwardBox_Click);
			// 
			// StrafeLeftBox
			// 
			this.StrafeLeftBox.AutoSize = true;
			this.StrafeLeftBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.StrafeLeftBox.Image = ((System.Drawing.Image) (resources.GetObject("StrafeLeftBox.Image")));
			this.StrafeLeftBox.Location = new System.Drawing.Point(110, 311);
			this.StrafeLeftBox.Name = "StrafeLeftBox";
			this.StrafeLeftBox.Size = new System.Drawing.Size(44, 40);
			this.StrafeLeftBox.TabIndex = 3;
			this.StrafeLeftBox.UseVisualStyleBackColor = true;
			this.StrafeLeftBox.Click += new System.EventHandler(this.StrafeLeftBox_Click);
			// 
			// TurnLeftBox
			// 
			this.TurnLeftBox.AutoSize = true;
			this.TurnLeftBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.TurnLeftBox.Image = ((System.Drawing.Image) (resources.GetObject("TurnLeftBox.Image")));
			this.TurnLeftBox.Location = new System.Drawing.Point(110, 265);
			this.TurnLeftBox.Name = "TurnLeftBox";
			this.TurnLeftBox.Size = new System.Drawing.Size(44, 40);
			this.TurnLeftBox.TabIndex = 3;
			this.TurnLeftBox.UseVisualStyleBackColor = true;
			this.TurnLeftBox.Click += new System.EventHandler(this.TurnLeftBox_Click);
			// 
			// GlPreviewControl
			// 
			this.GlPreviewControl.BackColor = System.Drawing.Color.Black;
			this.GlPreviewControl.Location = new System.Drawing.Point(9, 19);
			this.GlPreviewControl.Name = "GlPreviewControl";
			this.GlPreviewControl.Size = new System.Drawing.Size(352, 240);
			this.GlPreviewControl.TabIndex = 2;
			this.GlPreviewControl.VSync = false;
			this.GlPreviewControl.Load += new System.EventHandler(this.GlPreviewControl_Load);
			this.GlPreviewControl.Paint += new System.Windows.Forms.PaintEventHandler(this.GlPreviewControl_Paint);
			this.GlPreviewControl.Resize += new System.EventHandler(this.GlPreviewControl_Resize);
			// 
			// RightPanel
			// 
			this.RightPanel.Controls.Add(this.DungeonStripBox);
			this.RightPanel.Controls.Add(this.glControl);
			this.RightPanel.Controls.Add(this.hScrollBar1);
			this.RightPanel.Controls.Add(this.vScrollBar1);
			this.RightPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.RightPanel.Location = new System.Drawing.Point(383, 0);
			this.RightPanel.Name = "RightPanel";
			this.RightPanel.Size = new System.Drawing.Size(873, 637);
			this.RightPanel.TabIndex = 9;
			// 
			// PropertiesBox
			// 
			this.PropertiesBox.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.PropertiesBox.Controls.Add(this.DungeonTab);
			this.PropertiesBox.Controls.Add(this.MazeTab);
			this.PropertiesBox.Location = new System.Drawing.Point(9, 19);
			this.PropertiesBox.Name = "PropertiesBox";
			this.PropertiesBox.SelectedIndex = 0;
			this.PropertiesBox.Size = new System.Drawing.Size(359, 240);
			this.PropertiesBox.TabIndex = 11;
			this.PropertiesBox.Enter += new System.EventHandler(this.PropertiesBox_Enter);
			// 
			// DungeonTab
			// 
			this.DungeonTab.Controls.Add(this.groupBox3);
			this.DungeonTab.Controls.Add(this.StartLocationBox);
			this.DungeonTab.Controls.Add(this.groupBox1);
			this.DungeonTab.Location = new System.Drawing.Point(4, 22);
			this.DungeonTab.Name = "DungeonTab";
			this.DungeonTab.Padding = new System.Windows.Forms.Padding(3);
			this.DungeonTab.Size = new System.Drawing.Size(351, 214);
			this.DungeonTab.TabIndex = 0;
			this.DungeonTab.Text = "Dungeon";
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.ItemTileSetBox);
			this.groupBox3.Location = new System.Drawing.Point(187, 6);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(158, 51);
			this.groupBox3.TabIndex = 7;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Item TileSet :";
			// 
			// ItemTileSetBox
			// 
			this.ItemTileSetBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ItemTileSetBox.FormattingEnabled = true;
			this.ItemTileSetBox.Location = new System.Drawing.Point(6, 19);
			this.ItemTileSetBox.Name = "ItemTileSetBox";
			this.ItemTileSetBox.Size = new System.Drawing.Size(146, 21);
			this.ItemTileSetBox.Sorted = true;
			this.ItemTileSetBox.TabIndex = 4;
			this.ItemTileSetBox.SelectedIndexChanged += new System.EventHandler(this.ItemTileSetBox_SelectedIndexChanged);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.DungeonNoteBox);
			this.groupBox1.Location = new System.Drawing.Point(5, 112);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(340, 88);
			this.groupBox1.TabIndex = 2;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Notes :";
			// 
			// DungeonNoteBox
			// 
			this.DungeonNoteBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.DungeonNoteBox.Location = new System.Drawing.Point(3, 16);
			this.DungeonNoteBox.Multiline = true;
			this.DungeonNoteBox.Name = "DungeonNoteBox";
			this.DungeonNoteBox.Size = new System.Drawing.Size(334, 69);
			this.DungeonNoteBox.TabIndex = 0;
			this.DungeonNoteBox.TextChanged += new System.EventHandler(this.DungeonNoteBox_TextChanged);
			// 
			// MazeTab
			// 
			this.MazeTab.Controls.Add(this.groupBox2);
			this.MazeTab.Controls.Add(this.label2);
			this.MazeTab.Controls.Add(this.DefaultDoorBox);
			this.MazeTab.Location = new System.Drawing.Point(4, 22);
			this.MazeTab.Name = "MazeTab";
			this.MazeTab.Padding = new System.Windows.Forms.Padding(3);
			this.MazeTab.Size = new System.Drawing.Size(351, 214);
			this.MazeTab.TabIndex = 1;
			this.MazeTab.Text = "Maze";
			this.MazeTab.UseVisualStyleBackColor = true;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.WallTileSetNameBox);
			this.groupBox2.Controls.Add(this.label3);
			this.groupBox2.Controls.Add(this.DecorationNameBox);
			this.groupBox2.Controls.Add(this.label1);
			this.groupBox2.Location = new System.Drawing.Point(6, 124);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(339, 84);
			this.groupBox2.TabIndex = 2;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "TileSet :";
			// 
			// WallTileSetNameBox
			// 
			this.WallTileSetNameBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.WallTileSetNameBox.FormattingEnabled = true;
			this.WallTileSetNameBox.Location = new System.Drawing.Point(82, 50);
			this.WallTileSetNameBox.Name = "WallTileSetNameBox";
			this.WallTileSetNameBox.Size = new System.Drawing.Size(169, 21);
			this.WallTileSetNameBox.Sorted = true;
			this.WallTileSetNameBox.TabIndex = 0;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(11, 53);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(34, 13);
			this.label3.TabIndex = 1;
			this.label3.Text = "Wall :";
			// 
			// DecorationNameBox
			// 
			this.DecorationNameBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.DecorationNameBox.FormattingEnabled = true;
			this.DecorationNameBox.Location = new System.Drawing.Point(82, 23);
			this.DecorationNameBox.Name = "DecorationNameBox";
			this.DecorationNameBox.Size = new System.Drawing.Size(169, 21);
			this.DecorationNameBox.Sorted = true;
			this.DecorationNameBox.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(11, 26);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(65, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Decoration :";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(11, 9);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(71, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "Default door :";
			// 
			// DefaultDoorBox
			// 
			this.DefaultDoorBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.DefaultDoorBox.FormattingEnabled = true;
			this.DefaultDoorBox.Location = new System.Drawing.Point(88, 6);
			this.DefaultDoorBox.Name = "DefaultDoorBox";
			this.DefaultDoorBox.Size = new System.Drawing.Size(169, 21);
			this.DefaultDoorBox.Sorted = true;
			this.DefaultDoorBox.TabIndex = 0;
			// 
			// LeftPanel
			// 
			this.LeftPanel.Controls.Add(this.groupBox5);
			this.LeftPanel.Controls.Add(this.groupBox4);
			this.LeftPanel.Dock = System.Windows.Forms.DockStyle.Left;
			this.LeftPanel.Location = new System.Drawing.Point(0, 0);
			this.LeftPanel.Name = "LeftPanel";
			this.LeftPanel.Size = new System.Drawing.Size(383, 637);
			this.LeftPanel.TabIndex = 8;
			// 
			// groupBox5
			// 
			this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox5.Controls.Add(this.GlPreviewControl);
			this.groupBox5.Controls.Add(this.StrafeRightBox);
			this.groupBox5.Controls.Add(this.TurnLeftBox);
			this.groupBox5.Controls.Add(this.TurnRightBox);
			this.groupBox5.Controls.Add(this.StrafeLeftBox);
			this.groupBox5.Controls.Add(this.BackwardBox);
			this.groupBox5.Controls.Add(this.ForwardBox);
			this.groupBox5.Location = new System.Drawing.Point(3, 274);
			this.groupBox5.Name = "groupBox5";
			this.groupBox5.Size = new System.Drawing.Size(374, 360);
			this.groupBox5.TabIndex = 1;
			this.groupBox5.TabStop = false;
			this.groupBox5.Text = "Preview :";
			// 
			// groupBox4
			// 
			this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox4.Controls.Add(this.PropertiesBox);
			this.groupBox4.Location = new System.Drawing.Point(3, 3);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(374, 265);
			this.groupBox4.TabIndex = 0;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Properties :";
			// 
			// StartLocationBox
			// 
			this.StartLocationBox.Dungeon = null;
			this.StartLocationBox.Location = new System.Drawing.Point(6, 6);
			this.StartLocationBox.MinimumSize = new System.Drawing.Size(175, 100);
			this.StartLocationBox.Name = "StartLocationBox";
			this.StartLocationBox.Size = new System.Drawing.Size(175, 100);
			this.StartLocationBox.TabIndex = 6;
			this.StartLocationBox.Title = "Start location :";
			this.StartLocationBox.TargetChanged += new DungeonEye.Forms.TargetControl.TargetChangedEventHandler(this.StartLocationBox_TargetChanged);
			// 
			// DungeonForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1256, 661);
			this.Controls.Add(this.RightPanel);
			this.Controls.Add(this.LeftPanel);
			this.Controls.Add(this.statusStrip1);
			this.Name = "DungeonForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.TabText = "Dungeon Form";
			this.Text = "Dungeon Form";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.DungeonForm_FormClosed);
			this.Load += new System.EventHandler(this.DungeonForm_Load);
			this.DungeonStripBox.ResumeLayout(false);
			this.DungeonStripBox.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.RightPanel.ResumeLayout(false);
			this.RightPanel.PerformLayout();
			this.PropertiesBox.ResumeLayout(false);
			this.DungeonTab.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.MazeTab.ResumeLayout(false);
			this.MazeTab.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.LeftPanel.ResumeLayout(false);
			this.groupBox5.ResumeLayout(false);
			this.groupBox5.PerformLayout();
			this.groupBox4.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip DungeonStripBox;
		private System.Windows.Forms.ToolStripButton AddMazeButton;
		private System.Windows.Forms.ToolStripButton RemoveMazeButton;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripComboBox MazeListBox;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel SquareCoordBox;
		private System.Windows.Forms.HScrollBar hScrollBar1;
		private System.Windows.Forms.VScrollBar vScrollBar1;
		private System.Windows.Forms.ToolStripButton ResetOffsetBox;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.Timer DrawTimer;
		private OpenTK.GLControl glControl;
		private OpenTK.GLControl GlPreviewControl;
		private System.Windows.Forms.Button StrafeRightBox;
		private System.Windows.Forms.Button TurnRightBox;
		private System.Windows.Forms.Button BackwardBox;
		private System.Windows.Forms.Button ForwardBox;
		private System.Windows.Forms.Button StrafeLeftBox;
		private System.Windows.Forms.Button TurnLeftBox;
		private System.Windows.Forms.ToolStripStatusLabel SquareDescriptionBox;
		private System.Windows.Forms.Panel RightPanel;
		private System.Windows.Forms.TabControl PropertiesBox;
		private System.Windows.Forms.TabPage DungeonTab;
		private System.Windows.Forms.TabPage MazeTab;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox DungeonNoteBox;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripButton NoMonstersBox;
		private System.Windows.Forms.ToolStripButton NoGhostsBox;
		private System.Windows.Forms.ToolStripButton ZoneBox;
		private System.Windows.Forms.ToolStripButton AddMonsterBox;
		private System.Windows.Forms.ToolStripButton AddItemBox;
		private System.Windows.Forms.ToolStripButton WallBox;
		private System.Windows.Forms.ToolStripButton StairBox;
		private System.Windows.Forms.ToolStripButton TeleporterBox;
		private System.Windows.Forms.ToolStripButton DoorBox;
		private System.Windows.Forms.ToolStripButton PitBox;
		private System.Windows.Forms.ToolStripButton WrittingBox;
		private System.Windows.Forms.ToolStripButton LauncherBox;
		private System.Windows.Forms.ToolStripButton GeneratorBox;
		private System.Windows.Forms.ToolStripButton SwitchBox;
		private System.Windows.Forms.ToolStripButton FloorSwitchBox;
		private System.Windows.Forms.ToolStripButton FloorDecorationBox;
		private System.Windows.Forms.ToolStripButton DecorationBox;
		private System.Windows.Forms.ToolStripButton EventBox;
		private System.Windows.Forms.Panel LeftPanel;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox DefaultDoorBox;
		private System.Windows.Forms.GroupBox groupBox5;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.ComboBox ItemTileSetBox;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.ComboBox WallTileSetNameBox;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox DecorationNameBox;
		private System.Windows.Forms.Label label1;
		private TargetControl StartLocationBox;
		private System.Windows.Forms.GroupBox groupBox3;
	}
}