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
	public partial class ScriptActivateTargetControl : ScriptActionControlBase
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="script"></param>
		public ScriptActivateTargetControl(ScriptActivateTarget script)
		{
			InitializeComponent();

			if (script != null)
				Action = script;
			else
				Action = new ScriptActivateTarget();
		}



		#region Properties


		#endregion

	}
}
