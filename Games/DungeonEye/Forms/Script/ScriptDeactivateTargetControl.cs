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
	public partial class ScriptDeactivateTargetControl : ScriptActionControlBase
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="script"></param>
		public ScriptDeactivateTargetControl(ScriptDeactivateTarget script)
		{
			InitializeComponent();

			if (script != null)
				Action = script;
			else
				Action = new ScriptDeactivateTarget();
		}



		#region Properties


		#endregion

	}
}
