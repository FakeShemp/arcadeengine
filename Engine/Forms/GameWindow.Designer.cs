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
			this.RenderControl = new OpenTK.GLControl();
			this.SuspendLayout();
			// 
			// RenderControl
			// 
			this.RenderControl.BackColor = System.Drawing.Color.Black;
			this.RenderControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.RenderControl.Location = new System.Drawing.Point(0, 0);
			this.RenderControl.Name = "RenderControl";
			this.RenderControl.Size = new System.Drawing.Size(821, 629);
			this.RenderControl.TabIndex = 0;
			this.RenderControl.VSync = true;
			this.RenderControl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form_MouseMove);
			this.RenderControl.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Form_MouseDoubleClick);
			this.RenderControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form_MouseDown);
			this.RenderControl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form_MouseUp);
			// 
			// GameWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(821, 629);
			this.Controls.Add(this.RenderControl);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "GameWindow";
			this.Text = "ArcEngine : http://www.mimicprod.net";
			this.Load += new System.EventHandler(this.RenderControl_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private OpenTK.GLControl RenderControl;



	}
}