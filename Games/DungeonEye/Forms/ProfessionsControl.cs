using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DungeonEye.Forms
{
	/// <summary>
	/// Profession control
	/// </summary>
	public partial class ProfessionsControl : UserControl
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public ProfessionsControl()
		{
			InitializeComponent();

			// Class combo box
			ClassBox.BeginUpdate();
			foreach (string name in Enum.GetNames(typeof(HeroClass)))
			{
				if (name == HeroClass.Undefined.ToString())
					continue;
				ClassBox.Items.Add(name);
			}
			ClassBox.EndUpdate();
			ClassBox.SelectedIndex = 0;
		}



		/// <summary>
		/// Rebuild form
		/// </summary>
		void Rebuild()
		{
			if (Hero == null)
				return;

			ClassBox.SelectedIndex = ClassBox.SelectedIndex;

			BuildSummary();
		}


		/// <summary>
		/// Rebuild summary panel
		/// </summary>
		void BuildSummary()
		{

			string str = "";

			if (Hero != null)
			{
				foreach (Profession prof in Hero.Professions)
				{
					str += prof.Class.ToString() + " : XP: " + prof.Experience.ToString() + " Level: " + prof.Level + Environment.NewLine;
				}
			}

			SummaryBox.Text = str;
		}


		#region Form events

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void XPBox_ValueChanged(object sender, EventArgs e)
		{
			if (Hero == null)
				return;

			HeroClass hclass = (HeroClass) Enum.Parse(typeof(HeroClass), (string) ClassBox.SelectedItem);
			Profession prof = Hero.GetProfession(hclass);
			if (prof != null)
			{
				prof.Experience = (int) XPBox.Value;
				LevelBox.Text = prof.Level.ToString();
			}
			else
			{
				LevelBox.Text = "0";
			}
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void AddClassBox_Click(object sender, EventArgs e)
		{
			if (Hero == null)
				return;

			// Desired class
			HeroClass hclass = (HeroClass) Enum.Parse(typeof(HeroClass), (string) ClassBox.SelectedItem);

			// Does the profesion stil exist ?
			if (Hero.CheckClass(hclass))
				return;

			// Add the profession
			hero.Professions.Add(new Profession(0, hclass));
			PropertiesBox.Enabled = true;
			XPBox.Value = 0;
			AddClassBox.Enabled = false;
			RemoveClassBox.Enabled = true;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void RemoveClassBox_Click(object sender, EventArgs e)
		{

			if (Hero == null)
				return;

			// Desired class
			HeroClass hclass = (HeroClass) Enum.Parse(typeof(HeroClass), (string) ClassBox.SelectedItem);

			// No matching profression
			if (!Hero.CheckClass(hclass))
				return;

			// Remove profession
			hero.Professions.Remove(Hero.GetProfession(hclass));
			PropertiesBox.Enabled = false;
			XPBox.Value = 0;
			AddClassBox.Enabled = true;
			RemoveClassBox.Enabled = false;
		}



		/// <summary>
		/// Change class
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ClassBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (Hero == null)
				return;

			HeroClass hclass = (HeroClass) Enum.Parse(typeof(HeroClass), (string) ClassBox.SelectedItem);

			// Check if hero belongs to the class
			if (Hero.CheckClass(hclass))
			{
				PropertiesBox.Enabled = true;
				AddClassBox.Enabled = false;
				RemoveClassBox.Enabled = true;

				Profession prof = Hero.GetProfession(hclass);
				XPBox.Value = prof.Experience;
			}
			else
			{
				PropertiesBox.Enabled = false;
				AddClassBox.Enabled = true;
				RemoveClassBox.Enabled = false;

				XPBox.Value = 0;
			}
		}


		#endregion


		#region Properties


		/// <summary>
		/// Title of the control
		/// </summary>
		public string Title
		{
			get
			{
				return groupBox2.Text;
			}
			set
			{
				if (value == null)
					return;

				groupBox2.Text = value;
			}
		}


		/// <summary>
		/// Hero to manage
		/// </summary>
		public Hero Hero
		{
			get
			{
				return hero;
			}
			set
			{
				hero = value;
				Rebuild();
			}
		}
		Hero hero;


		/// <summary>
		/// Class currently edited
		/// </summary>
		HeroClass CurrentClass
		{
			get
			{
				return (HeroClass) Enum.Parse(typeof(HeroClass), (string) ClassBox.SelectedItem);
			}
		}


		#endregion
	}
}
