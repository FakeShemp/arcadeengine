﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DungeonEye.Script.Actions;


namespace DungeonEye.Forms
{
	/// <summary>
	/// 
	/// </summary>
	public partial class ToggleTargetControl : ActionControlBase
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="script"></param>
		/// <param name="dungeon">Dungeon handle</param>
		public ToggleTargetControl(ActionToggleTarget script, Dungeon dungeon)
		{
			InitializeComponent();

			if (script != null)
				Action = script;
			else
				Action = new ActionToggleTarget();

			TargetBox.SetTarget(dungeon, Action.Target);
		}


		#region Form events

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="target"></param>
		private void TargetBox_TargetChanged(object sender, DungeonLocation target)
		{
			if (Action == null)
				return;

			((ActionToggleTarget)Action).Target = target;
		}

		#endregion



		#region Properties


		#endregion

	}
}
