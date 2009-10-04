using System;
using System.Collections.Generic;
using System.Text;
using ArcEngine.Asset;
using ArcEngine.Editor;
using ArcEngine.Forms;
using ArcEngine.Graphic;
using System.Drawing;


namespace PathDemo
{
	class FufuWindow : AssetEditor
	{
		private System.Windows.Forms.Timer timer1;
		private System.ComponentModel.IContainer components;
		private OpenTK.GLControl glControl1;
	
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.glControl1 = new OpenTK.GLControl();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.SuspendLayout();
			// 
			// glControl1
			// 
			this.glControl1.BackColor = System.Drawing.Color.Black;
			this.glControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.glControl1.Location = new System.Drawing.Point(0, 0);
			this.glControl1.Name = "glControl1";
			this.glControl1.Size = new System.Drawing.Size(919, 505);
			this.glControl1.TabIndex = 0;
			this.glControl1.VSync = false;
			this.glControl1.Paint += new System.Windows.Forms.PaintEventHandler(this.glControl1_Paint);
			this.glControl1.Resize += new System.EventHandler(this.glControl1_Resize);
			// 
			// timer1
			// 
			this.timer1.Enabled = true;
			this.timer1.Interval = 25;
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// FufuWindow
			// 
			this.ClientSize = new System.Drawing.Size(919, 505);
			this.Controls.Add(this.glControl1);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "FufuWindow";
			this.Load += new System.EventHandler(this.FufuWindow_Load);
			this.ResumeLayout(false);

		}

		private void glControl1_Resize(object sender, EventArgs e)
		{
			glControl1.MakeCurrent();
			Display.ViewPort = new Rectangle(0, 0, glControl1.Width, glControl1.Height);

		}

		private void FufuWindow_Load(object sender, EventArgs e)
		{
			glControl1.MakeCurrent();
			Display.Init();
			Display.ClearColor = Color.Red;

			timer1.Start();

		}

		private void glControl1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			glControl1.MakeCurrent();
			Display.ClearBuffers();
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			glControl1_Paint(null, null);
		}
	}
}
