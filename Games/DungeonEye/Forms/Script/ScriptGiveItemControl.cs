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
	public partial class ScriptGiveItemControl : ScriptActionControlBase
	{
		/// <summary>
		/// 
		/// </summary>
		public ScriptGiveItemControl()
		{
			InitializeComponent();


			Action = new ScriptGiveItem();
		}



		#region Properties


		#endregion

	}
}
