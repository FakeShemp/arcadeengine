namespace DungeonEye.Forms.Script
{
	partial class ActionChooserControl
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
			this.panel1 = new System.Windows.Forms.Panel();
			this.TogglesBox = new System.Windows.Forms.RadioButton();
			this.ActivatesBox = new System.Windows.Forms.RadioButton();
			this.GiveExperienceBox = new System.Windows.Forms.RadioButton();
			this.ChangePictureBox = new System.Windows.Forms.RadioButton();
			this.JoinCharacterBox = new System.Windows.Forms.RadioButton();
			this.DeactivatesBox = new System.Windows.Forms.RadioButton();
			this.EndDialogBox = new System.Windows.Forms.RadioButton();
			this.SetToBox = new System.Windows.Forms.RadioButton();
			this.HealingBox = new System.Windows.Forms.RadioButton();
			this.ExchangesBox = new System.Windows.Forms.RadioButton();
			this.EndChoiceBox = new System.Windows.Forms.RadioButton();
			this.ActDeactBox = new System.Windows.Forms.RadioButton();
			this.GiveItemBox = new System.Windows.Forms.RadioButton();
			this.PlaySoundBox = new System.Windows.Forms.RadioButton();
			this.EnableChoiceBox = new System.Windows.Forms.RadioButton();
			this.StopSoundBox = new System.Windows.Forms.RadioButton();
			this.DisableChoiceBox = new System.Windows.Forms.RadioButton();
			this.DeactActBox = new System.Windows.Forms.RadioButton();
			this.ChangeTextBox = new System.Windows.Forms.RadioButton();
			this.ActionPropertiesBox = new System.Windows.Forms.Panel();
			this.groupBox1.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.panel1);
			this.groupBox1.Controls.Add(this.ActionPropertiesBox);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox1.Location = new System.Drawing.Point(0, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(566, 444);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Action";
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.panel1.AutoScroll = true;
			this.panel1.Controls.Add(this.TogglesBox);
			this.panel1.Controls.Add(this.ActivatesBox);
			this.panel1.Controls.Add(this.GiveExperienceBox);
			this.panel1.Controls.Add(this.ChangePictureBox);
			this.panel1.Controls.Add(this.JoinCharacterBox);
			this.panel1.Controls.Add(this.DeactivatesBox);
			this.panel1.Controls.Add(this.EndDialogBox);
			this.panel1.Controls.Add(this.SetToBox);
			this.panel1.Controls.Add(this.HealingBox);
			this.panel1.Controls.Add(this.ExchangesBox);
			this.panel1.Controls.Add(this.EndChoiceBox);
			this.panel1.Controls.Add(this.ActDeactBox);
			this.panel1.Controls.Add(this.GiveItemBox);
			this.panel1.Controls.Add(this.PlaySoundBox);
			this.panel1.Controls.Add(this.EnableChoiceBox);
			this.panel1.Controls.Add(this.StopSoundBox);
			this.panel1.Controls.Add(this.DisableChoiceBox);
			this.panel1.Controls.Add(this.DeactActBox);
			this.panel1.Controls.Add(this.ChangeTextBox);
			this.panel1.Location = new System.Drawing.Point(6, 19);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(142, 419);
			this.panel1.TabIndex = 4;
			// 
			// TogglesBox
			// 
			this.TogglesBox.Appearance = System.Windows.Forms.Appearance.Button;
			this.TogglesBox.Location = new System.Drawing.Point(3, 3);
			this.TogglesBox.Name = "TogglesBox";
			this.TogglesBox.Size = new System.Drawing.Size(113, 23);
			this.TogglesBox.TabIndex = 3;
			this.TogglesBox.TabStop = true;
			this.TogglesBox.Text = "Toggles";
			this.TogglesBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.TogglesBox.UseVisualStyleBackColor = true;
			this.TogglesBox.CheckedChanged += new System.EventHandler(this.TogglesBox_CheckedChanged);
			// 
			// ActivatesBox
			// 
			this.ActivatesBox.Appearance = System.Windows.Forms.Appearance.Button;
			this.ActivatesBox.Location = new System.Drawing.Point(3, 32);
			this.ActivatesBox.Name = "ActivatesBox";
			this.ActivatesBox.Size = new System.Drawing.Size(113, 23);
			this.ActivatesBox.TabIndex = 3;
			this.ActivatesBox.TabStop = true;
			this.ActivatesBox.Text = "Activates";
			this.ActivatesBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.ActivatesBox.UseVisualStyleBackColor = true;
			this.ActivatesBox.CheckedChanged += new System.EventHandler(this.ActivatesBox_CheckedChanged);
			// 
			// GiveExperienceBox
			// 
			this.GiveExperienceBox.Appearance = System.Windows.Forms.Appearance.Button;
			this.GiveExperienceBox.Location = new System.Drawing.Point(3, 438);
			this.GiveExperienceBox.Name = "GiveExperienceBox";
			this.GiveExperienceBox.Size = new System.Drawing.Size(113, 23);
			this.GiveExperienceBox.TabIndex = 3;
			this.GiveExperienceBox.TabStop = true;
			this.GiveExperienceBox.Text = "Gives Experience";
			this.GiveExperienceBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.GiveExperienceBox.UseVisualStyleBackColor = true;
			this.GiveExperienceBox.CheckedChanged += new System.EventHandler(this.GiveExperienceBox_CheckedChanged);
			// 
			// ChangePictureBox
			// 
			this.ChangePictureBox.Appearance = System.Windows.Forms.Appearance.Button;
			this.ChangePictureBox.Location = new System.Drawing.Point(3, 264);
			this.ChangePictureBox.Name = "ChangePictureBox";
			this.ChangePictureBox.Size = new System.Drawing.Size(113, 23);
			this.ChangePictureBox.TabIndex = 3;
			this.ChangePictureBox.TabStop = true;
			this.ChangePictureBox.Text = "Changes Picture";
			this.ChangePictureBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.ChangePictureBox.UseVisualStyleBackColor = true;
			this.ChangePictureBox.CheckedChanged += new System.EventHandler(this.ChangePictureBox_CheckedChanged);
			// 
			// JoinCharacterBox
			// 
			this.JoinCharacterBox.Appearance = System.Windows.Forms.Appearance.Button;
			this.JoinCharacterBox.Location = new System.Drawing.Point(3, 525);
			this.JoinCharacterBox.Name = "JoinCharacterBox";
			this.JoinCharacterBox.Size = new System.Drawing.Size(113, 23);
			this.JoinCharacterBox.TabIndex = 3;
			this.JoinCharacterBox.TabStop = true;
			this.JoinCharacterBox.Text = "Joins Character";
			this.JoinCharacterBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.JoinCharacterBox.UseVisualStyleBackColor = true;
			this.JoinCharacterBox.CheckedChanged += new System.EventHandler(this.JoinCharacterBox_CheckedChanged);
			// 
			// DeactivatesBox
			// 
			this.DeactivatesBox.Appearance = System.Windows.Forms.Appearance.Button;
			this.DeactivatesBox.Location = new System.Drawing.Point(3, 61);
			this.DeactivatesBox.Name = "DeactivatesBox";
			this.DeactivatesBox.Size = new System.Drawing.Size(113, 23);
			this.DeactivatesBox.TabIndex = 3;
			this.DeactivatesBox.TabStop = true;
			this.DeactivatesBox.Text = "Deactivates";
			this.DeactivatesBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.DeactivatesBox.UseVisualStyleBackColor = true;
			this.DeactivatesBox.CheckedChanged += new System.EventHandler(this.DeactivatesBox_CheckedChanged);
			// 
			// EndDialogBox
			// 
			this.EndDialogBox.Appearance = System.Windows.Forms.Appearance.Button;
			this.EndDialogBox.Location = new System.Drawing.Point(3, 409);
			this.EndDialogBox.Name = "EndDialogBox";
			this.EndDialogBox.Size = new System.Drawing.Size(113, 23);
			this.EndDialogBox.TabIndex = 3;
			this.EndDialogBox.TabStop = true;
			this.EndDialogBox.Text = "End Dialog";
			this.EndDialogBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.EndDialogBox.UseVisualStyleBackColor = true;
			this.EndDialogBox.CheckedChanged += new System.EventHandler(this.EndDialogBox_CheckedChanged);
			// 
			// SetToBox
			// 
			this.SetToBox.Appearance = System.Windows.Forms.Appearance.Button;
			this.SetToBox.Location = new System.Drawing.Point(3, 177);
			this.SetToBox.Name = "SetToBox";
			this.SetToBox.Size = new System.Drawing.Size(113, 23);
			this.SetToBox.TabIndex = 3;
			this.SetToBox.TabStop = true;
			this.SetToBox.Text = "Set To";
			this.SetToBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.SetToBox.UseVisualStyleBackColor = true;
			this.SetToBox.CheckedChanged += new System.EventHandler(this.SetToBox_CheckedChanged);
			// 
			// HealingBox
			// 
			this.HealingBox.Appearance = System.Windows.Forms.Appearance.Button;
			this.HealingBox.Location = new System.Drawing.Point(3, 496);
			this.HealingBox.Name = "HealingBox";
			this.HealingBox.Size = new System.Drawing.Size(113, 23);
			this.HealingBox.TabIndex = 3;
			this.HealingBox.TabStop = true;
			this.HealingBox.Text = "Healing";
			this.HealingBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.HealingBox.UseVisualStyleBackColor = true;
			this.HealingBox.CheckedChanged += new System.EventHandler(this.HealingBox_CheckedChanged);
			// 
			// ExchangesBox
			// 
			this.ExchangesBox.Appearance = System.Windows.Forms.Appearance.Button;
			this.ExchangesBox.Location = new System.Drawing.Point(3, 148);
			this.ExchangesBox.Name = "ExchangesBox";
			this.ExchangesBox.Size = new System.Drawing.Size(113, 23);
			this.ExchangesBox.TabIndex = 3;
			this.ExchangesBox.TabStop = true;
			this.ExchangesBox.Text = "Exchanges";
			this.ExchangesBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.ExchangesBox.UseVisualStyleBackColor = true;
			this.ExchangesBox.CheckedChanged += new System.EventHandler(this.ExchangesBox_CheckedChanged);
			// 
			// EndChoiceBox
			// 
			this.EndChoiceBox.Appearance = System.Windows.Forms.Appearance.Button;
			this.EndChoiceBox.Location = new System.Drawing.Point(3, 380);
			this.EndChoiceBox.Name = "EndChoiceBox";
			this.EndChoiceBox.Size = new System.Drawing.Size(113, 23);
			this.EndChoiceBox.TabIndex = 3;
			this.EndChoiceBox.TabStop = true;
			this.EndChoiceBox.Text = "End Choice";
			this.EndChoiceBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.EndChoiceBox.UseVisualStyleBackColor = true;
			this.EndChoiceBox.CheckedChanged += new System.EventHandler(this.EndChoiceBox_CheckedChanged);
			// 
			// ActDeactBox
			// 
			this.ActDeactBox.Appearance = System.Windows.Forms.Appearance.Button;
			this.ActDeactBox.Location = new System.Drawing.Point(3, 90);
			this.ActDeactBox.Name = "ActDeactBox";
			this.ActDeactBox.Size = new System.Drawing.Size(113, 23);
			this.ActDeactBox.TabIndex = 3;
			this.ActDeactBox.TabStop = true;
			this.ActDeactBox.Text = "Activate/Deactivate";
			this.ActDeactBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.ActDeactBox.UseVisualStyleBackColor = true;
			this.ActDeactBox.CheckedChanged += new System.EventHandler(this.ActDeactBox_CheckedChanged);
			// 
			// GiveItemBox
			// 
			this.GiveItemBox.Appearance = System.Windows.Forms.Appearance.Button;
			this.GiveItemBox.Location = new System.Drawing.Point(3, 467);
			this.GiveItemBox.Name = "GiveItemBox";
			this.GiveItemBox.Size = new System.Drawing.Size(113, 23);
			this.GiveItemBox.TabIndex = 3;
			this.GiveItemBox.TabStop = true;
			this.GiveItemBox.Text = "Gives Item";
			this.GiveItemBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.GiveItemBox.UseVisualStyleBackColor = true;
			this.GiveItemBox.CheckedChanged += new System.EventHandler(this.GiveItemBox_CheckedChanged);
			// 
			// PlaySoundBox
			// 
			this.PlaySoundBox.Appearance = System.Windows.Forms.Appearance.Button;
			this.PlaySoundBox.Location = new System.Drawing.Point(3, 206);
			this.PlaySoundBox.Name = "PlaySoundBox";
			this.PlaySoundBox.Size = new System.Drawing.Size(113, 23);
			this.PlaySoundBox.TabIndex = 3;
			this.PlaySoundBox.TabStop = true;
			this.PlaySoundBox.Text = "Play Sound";
			this.PlaySoundBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.PlaySoundBox.UseVisualStyleBackColor = true;
			this.PlaySoundBox.CheckedChanged += new System.EventHandler(this.PlaySoundBox_CheckedChanged);
			// 
			// EnableChoiceBox
			// 
			this.EnableChoiceBox.Appearance = System.Windows.Forms.Appearance.Button;
			this.EnableChoiceBox.Location = new System.Drawing.Point(3, 351);
			this.EnableChoiceBox.Name = "EnableChoiceBox";
			this.EnableChoiceBox.Size = new System.Drawing.Size(113, 23);
			this.EnableChoiceBox.TabIndex = 3;
			this.EnableChoiceBox.TabStop = true;
			this.EnableChoiceBox.Text = "Enables Choice";
			this.EnableChoiceBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.EnableChoiceBox.UseVisualStyleBackColor = true;
			this.EnableChoiceBox.CheckedChanged += new System.EventHandler(this.EnableChoiceBox_CheckedChanged);
			// 
			// StopSoundBox
			// 
			this.StopSoundBox.Appearance = System.Windows.Forms.Appearance.Button;
			this.StopSoundBox.Location = new System.Drawing.Point(3, 235);
			this.StopSoundBox.Name = "StopSoundBox";
			this.StopSoundBox.Size = new System.Drawing.Size(113, 23);
			this.StopSoundBox.TabIndex = 3;
			this.StopSoundBox.TabStop = true;
			this.StopSoundBox.Text = "Stop Sounds";
			this.StopSoundBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.StopSoundBox.UseVisualStyleBackColor = true;
			this.StopSoundBox.CheckedChanged += new System.EventHandler(this.StopSoundBox_CheckedChanged);
			// 
			// DisableChoiceBox
			// 
			this.DisableChoiceBox.Appearance = System.Windows.Forms.Appearance.Button;
			this.DisableChoiceBox.Location = new System.Drawing.Point(3, 322);
			this.DisableChoiceBox.Name = "DisableChoiceBox";
			this.DisableChoiceBox.Size = new System.Drawing.Size(113, 23);
			this.DisableChoiceBox.TabIndex = 3;
			this.DisableChoiceBox.TabStop = true;
			this.DisableChoiceBox.Text = "Disable Choice";
			this.DisableChoiceBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.DisableChoiceBox.UseVisualStyleBackColor = true;
			this.DisableChoiceBox.CheckedChanged += new System.EventHandler(this.DisableChoiceBox_CheckedChanged);
			// 
			// DeactActBox
			// 
			this.DeactActBox.Appearance = System.Windows.Forms.Appearance.Button;
			this.DeactActBox.Location = new System.Drawing.Point(3, 119);
			this.DeactActBox.Name = "DeactActBox";
			this.DeactActBox.Size = new System.Drawing.Size(113, 23);
			this.DeactActBox.TabIndex = 3;
			this.DeactActBox.TabStop = true;
			this.DeactActBox.Text = "Deactivate/Activate";
			this.DeactActBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.DeactActBox.UseVisualStyleBackColor = true;
			this.DeactActBox.CheckedChanged += new System.EventHandler(this.DeactActBox_CheckedChanged);
			// 
			// ChangeTextBox
			// 
			this.ChangeTextBox.Appearance = System.Windows.Forms.Appearance.Button;
			this.ChangeTextBox.Location = new System.Drawing.Point(3, 293);
			this.ChangeTextBox.Name = "ChangeTextBox";
			this.ChangeTextBox.Size = new System.Drawing.Size(113, 23);
			this.ChangeTextBox.TabIndex = 3;
			this.ChangeTextBox.TabStop = true;
			this.ChangeTextBox.Text = "Changes Text";
			this.ChangeTextBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.ChangeTextBox.UseVisualStyleBackColor = true;
			this.ChangeTextBox.CheckedChanged += new System.EventHandler(this.ChangeTextBox_CheckedChanged);
			// 
			// ActionPropertiesBox
			// 
			this.ActionPropertiesBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.ActionPropertiesBox.Location = new System.Drawing.Point(154, 19);
			this.ActionPropertiesBox.Name = "ActionPropertiesBox";
			this.ActionPropertiesBox.Size = new System.Drawing.Size(406, 419);
			this.ActionPropertiesBox.TabIndex = 1;
			// 
			// ActionChooserControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.groupBox1);
			this.Name = "ActionChooserControl";
			this.Size = new System.Drawing.Size(566, 444);
			this.groupBox1.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Panel ActionPropertiesBox;
		private System.Windows.Forms.RadioButton ExchangesBox;
		private System.Windows.Forms.RadioButton DeactActBox;
		private System.Windows.Forms.RadioButton ActDeactBox;
		private System.Windows.Forms.RadioButton DeactivatesBox;
		private System.Windows.Forms.RadioButton ActivatesBox;
		private System.Windows.Forms.RadioButton TogglesBox;
		private System.Windows.Forms.RadioButton ChangePictureBox;
		private System.Windows.Forms.RadioButton GiveExperienceBox;
		private System.Windows.Forms.RadioButton JoinCharacterBox;
		private System.Windows.Forms.RadioButton StopSoundBox;
		private System.Windows.Forms.RadioButton EndDialogBox;
		private System.Windows.Forms.RadioButton HealingBox;
		private System.Windows.Forms.RadioButton PlaySoundBox;
		private System.Windows.Forms.RadioButton EndChoiceBox;
		private System.Windows.Forms.RadioButton GiveItemBox;
		private System.Windows.Forms.RadioButton SetToBox;
		private System.Windows.Forms.RadioButton EnableChoiceBox;
		private System.Windows.Forms.RadioButton DisableChoiceBox;
		private System.Windows.Forms.RadioButton ChangeTextBox;
		private System.Windows.Forms.Panel panel1;
	}
}
