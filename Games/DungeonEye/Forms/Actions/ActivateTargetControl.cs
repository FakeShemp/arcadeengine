using System;
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
	public partial class ActivateTargetControl : ActionControlBase
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="script"></param>
		public ActivateTargetControl(ScriptActivateTarget script, Dungeon dungeon)
		{
			InitializeComponent();

			if (script != null)
				Action = script;
			else
				Action = new ScriptActivateTarget();

			TargetBox.SetTarget(dungeon, Action.Target);
		}


		#region Events

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="target"></param>
		private void TargetBox_TargetChanged(object sender, DungeonLocation target)
		{
			if (Action == null)
				return;

			((ScriptActivateTarget)Action).Target = target;
		}

		#endregion


		#region Properties


		#endregion

	}
}
