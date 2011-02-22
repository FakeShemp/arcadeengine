using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DungeonEye.EventScript;


namespace DungeonEye.Forms
{
	/// <summary>
	/// 
	/// </summary>
	public partial class ScriptEnableChoiceControl : ScriptActionControlBase
	{
		/// <summary>
		/// 
		/// </summary>
		public ScriptEnableChoiceControl()
		{
			InitializeComponent();

			Action = new ScriptEnableChoice();
		}



		#region Properties


		#endregion

	}
}
