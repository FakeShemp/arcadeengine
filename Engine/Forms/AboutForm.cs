using System;
using System.Collections.Generic;
using System.ComponentModel;

using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ArcEngine;

using ArcEngine.Providers;

namespace ArcEngine.Editor.Forms
{
	/// <summary>
	/// 
	/// </summary>
	public partial class AboutForm : Form
	{
		/// <summary>
		/// 
		/// </summary>
		public AboutForm()
		{
			InitializeComponent();

			PluginList.SuspendLayout();
			foreach (Provider provider in ResourceManager.Providers)
			{
				PluginList.Items.Add(provider.Name + "  (version " + provider.Version.ToString() + ")");
			}
			PluginList.ResumeLayout();


/*

			foreach (IPlugin plugin in PluginManager.Plugins)
				PluginList.Items.Add(plugin.Name + " " + plugin.Version.ToString());

*/
		}
	}
}