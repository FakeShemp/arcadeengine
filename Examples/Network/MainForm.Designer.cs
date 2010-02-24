namespace Network
{
	partial class MainForm
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

		#region Code généré par le Concepteur Windows Form

		/// <summary>
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.StartServerBox = new System.Windows.Forms.Button();
			this.StopServerBox = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.textBox3 = new System.Windows.Forms.TextBox();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.CreateClientBox = new System.Windows.Forms.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.LogBox = new System.Windows.Forms.TextBox();
			this.UpdateTimer = new System.Windows.Forms.Timer(this.components);
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.StartServerBox);
			this.groupBox1.Controls.Add(this.StopServerBox);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.textBox3);
			this.groupBox1.Controls.Add(this.textBox2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.textBox1);
			this.groupBox1.Location = new System.Drawing.Point(12, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(192, 134);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Server properties :";
			// 
			// StartServerBox
			// 
			this.StartServerBox.Location = new System.Drawing.Point(30, 105);
			this.StartServerBox.Name = "StartServerBox";
			this.StartServerBox.Size = new System.Drawing.Size(75, 23);
			this.StartServerBox.TabIndex = 4;
			this.StartServerBox.Text = "Start";
			this.StartServerBox.UseVisualStyleBackColor = true;
			this.StartServerBox.Click += new System.EventHandler(this.StartServerBox_Click);
			// 
			// StopServerBox
			// 
			this.StopServerBox.Location = new System.Drawing.Point(111, 105);
			this.StopServerBox.Name = "StopServerBox";
			this.StopServerBox.Size = new System.Drawing.Size(75, 23);
			this.StopServerBox.TabIndex = 5;
			this.StopServerBox.Text = "Stop";
			this.StopServerBox.UseVisualStyleBackColor = true;
			this.StopServerBox.Click += new System.EventHandler(this.StopServerBox_Click);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(44, 74);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(32, 13);
			this.label3.TabIndex = 2;
			this.label3.Text = "Port :";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(44, 48);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(35, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Host :";
			// 
			// textBox3
			// 
			this.textBox3.Location = new System.Drawing.Point(85, 71);
			this.textBox3.Name = "textBox3";
			this.textBox3.Size = new System.Drawing.Size(100, 20);
			this.textBox3.TabIndex = 3;
			this.textBox3.Text = "9050";
			// 
			// textBox2
			// 
			this.textBox2.Location = new System.Drawing.Point(85, 45);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(100, 20);
			this.textBox2.TabIndex = 2;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 22);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(73, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Server name :";
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(85, 19);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(100, 20);
			this.textBox1.TabIndex = 1;
			this.textBox1.Text = "Bradock";
			// 
			// CreateClientBox
			// 
			this.CreateClientBox.Location = new System.Drawing.Point(210, 123);
			this.CreateClientBox.Name = "CreateClientBox";
			this.CreateClientBox.Size = new System.Drawing.Size(75, 23);
			this.CreateClientBox.TabIndex = 6;
			this.CreateClientBox.Text = "Create client";
			this.CreateClientBox.UseVisualStyleBackColor = true;
			this.CreateClientBox.Click += new System.EventHandler(this.CreateClientBox_Click);
			// 
			// groupBox2
			// 
			this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox2.Controls.Add(this.LogBox);
			this.groupBox2.Location = new System.Drawing.Point(12, 152);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(653, 326);
			this.groupBox2.TabIndex = 99;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Log";
			// 
			// LogBox
			// 
			this.LogBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LogBox.Location = new System.Drawing.Point(3, 16);
			this.LogBox.Multiline = true;
			this.LogBox.Name = "LogBox";
			this.LogBox.ReadOnly = true;
			this.LogBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.LogBox.Size = new System.Drawing.Size(647, 307);
			this.LogBox.TabIndex = 7;
			this.LogBox.TabStop = false;
			// 
			// UpdateTimer
			// 
			this.UpdateTimer.Interval = 20;
			this.UpdateTimer.Tick += new System.EventHandler(this.UpdateTimer_Tick);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(677, 490);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.CreateClientBox);
			this.MinimumSize = new System.Drawing.Size(400, 500);
			this.Name = "MainForm";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.Text = "Network test";
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.TextBox LogBox;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBox3;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Button CreateClientBox;
		private System.Windows.Forms.Button StartServerBox;
		private System.Windows.Forms.Button StopServerBox;
		private System.Windows.Forms.Timer UpdateTimer;
	}
}

