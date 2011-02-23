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
	public partial class ScriptTeleportControl : ScriptActionControlBase
	{
		/// <summary>n
		/// 
		/// </summary>
		public ScriptTeleportControl()
		{
			InitializeComponent();


			Action = new ScriptTeleport();
		}


		#region Events



		#endregion



		#region Properties

		/// <summary>
		/// Target location
		/// </summary>
		public DungeonLocation Target
		{
			get;
			private set;
		}

		#endregion

	}
}
