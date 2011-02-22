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
	public partial class ScriptChangePictureControl : ScriptActionControlBase
	{
		/// <summary>
		/// 
		/// </summary>
		public ScriptChangePictureControl()
		{
			InitializeComponent();

			Action = new ScriptChangePicture();
		}



		#region Properties


		#endregion

	}
}
