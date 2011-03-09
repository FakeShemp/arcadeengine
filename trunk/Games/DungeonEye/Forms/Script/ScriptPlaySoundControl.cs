﻿using System;
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
	public partial class ScriptPlaySoundControl : ScriptActionControlBase
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="script"></param>
		public ScriptPlaySoundControl(ScriptPlaySound script)
		{
			InitializeComponent();

			if (script != null)
				Action = script;
			else
				Action = new ScriptPlaySound();
		}



		#region Properties


		#endregion

	}
}