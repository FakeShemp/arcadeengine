using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DungeonEye.Script.Actions;
using DungeonEye.Script;


namespace DungeonEye.Forms
{
	/// <summary>
	/// Script action control base
	/// </summary>
	public partial class ActionBaseControl : UserControl
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public ActionBaseControl()
		{
			InitializeComponent();
		}




		#region Properties


		/// <summary>
		/// Action to execute
		/// </summary>
		public ActionBase Action
		{
			get;
			protected set;
		}



		#endregion
	}
}
