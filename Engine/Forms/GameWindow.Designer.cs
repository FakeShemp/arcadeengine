namespace ArcEngine.Forms
{
	partial class GameWindow
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameWindow));
			this.glControl1 = new OpenTK.GLControl();
			this.SuspendLayout();
			// 
			// glControl1
			// 
			this.glControl1.BackColor = System.Drawing.Color.Black;
			this.glControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.glControl1.Location = new System.Drawing.Point(0, 0);
			this.glControl1.Name = "glControl1";
			this.glControl1.Size = new System.Drawing.Size(821, 629);
			this.glControl1.TabIndex = 0;
			this.glControl1.VSync = false;
			this.glControl1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form_MouseMove);
			this.glControl1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Form_MouseDoubleClick);
			this.glControl1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form_MouseDown);
			this.glControl1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form_MouseUp);
			// 
			// GameWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(821, 629);
			this.Controls.Add(this.glControl1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "GameWindow";
			this.Text = "ArcEngine : http://arcengine.wordpress.com";
			this.ResumeLayout(false);

		}

		#endregion

		/// <summary>
		/// 
		/// </summary>
		public OpenTK.GLControl glControl1;

	}
}