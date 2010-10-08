#region Licence
//
//This file is part of ArcEngine.
//Copyright (C)2008-2009 Adrien Hémery ( iliak@mimicprod.net )
//
//ArcEngine is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//any later version.
//
//ArcEngine is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.
//
//You should have received a copy of the GNU General Public License
//along with Foobar.  If not, see <http://www.gnu.org/licenses/>.
//
#endregion

using System;
using System.CodeDom.Compiler;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using ArcEngine.Asset;
using ArcEngine.Forms;
using WeifenLuo.WinFormsUI.Docking;
using ArcEngine;

using System.Collections.Generic;

namespace ArcEngine.Editor
{
	/// <summary>
	/// Edit script model
	/// </summary>
	internal partial class ScriptModelForm : AssetEditorBase
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public ScriptModelForm(XmlNode node)
		{
			InitializeComponent();



			Model = new ScriptModel();
			Model.Load(node);


			ScriptTxt.Text = Model.Source;


			//
			PropertyBox.SelectedObject = Model;
		}


		/// <summary>
		/// save the asset to the manager
		/// </summary>
		public override void Save()
		{
			Model.Source = ScriptTxt.Text;

			StringBuilder sb = new StringBuilder();
			using (XmlWriter writer = XmlWriter.Create(sb))
				Model.Save(writer);

			string xml = sb.ToString();
			XmlDocument doc = new XmlDocument();
			doc.LoadXml(xml);

			ResourceManager.AddAsset<ScriptModel>(Model.Name, doc.DocumentElement);
		}



		#region Events

		/// <summary>
		/// Saves the source code of the script
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SaveButton_OnClick(object sender, EventArgs e)
		{

			if (Model != null)
			{
				Model.Source = ScriptTxt.Text;
			}
		}


		/// <summary>
		/// Compiles the script
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Compile_OnClick(object sender, EventArgs e)
		{
/*
			if (Script != null)
			{
				//Log.Send(new LogEventArgs(LogLevel.Info, "Compiling...", null));
				Trace.WriteLine("Compiling...");

				ErrorListView.Items.Clear();

				Script.SourceCode = ScriptTxt.Text;
				if (!Script.Compile(true))
				{

					ListViewItem l;

					// Add each error as a listview item with its line number
					foreach (CompilerError err in Script.Errors)
					{
						l = new ListViewItem(err.ErrorText);
						l.SubItems.Add(err.Line.ToString());
						ErrorListView.Items.Add(l);
					}
				}

				string txt = "Compile complete -- ";
				if (Script.HasErrors)
				{
					txt += Script.Errors.Count + " error(s).";
					LogStatusLabel.Text = Script.Errors.Count + " error(s).";
				}
				else
				{
					txt += " 0 error.";
					LogStatusLabel.Text = "0 error.";
				}

				//Log.Send(new LogEventArgs(LogLevel.Info, txt, null));
				Trace.WriteLine(txt);



			}
*/
		}


		/// <summary>
		/// Double click on the error bring on the error's line
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ListViewError_OnItemActive(object sender, EventArgs e)
		{
			return;

			int l = Convert.ToInt32(ErrorListView.SelectedItems[0].SubItems[1].Text);
			int i, pos;

			if (l != 0)
			{
				i = 1;
				pos = 0;
				while (i < l)
				{
					pos = ScriptTxt.Text.IndexOf(Environment.NewLine, pos + 1);
					i++;
				}
				//ScriptTxt.SelectionStart = pos;
				//ScriptTxt.SelectionLength = ScriptTxt.Text.IndexOf(Environment.NewLine, pos + 1) - pos;
			}

			ScriptTxt.Focus();

		}


		/// <summary>
		/// Insert IEntity model script
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void InsertIEntityButton_Click(object sender, EventArgs e)
		{
			ScriptTxt.Text = @"using System;
using System.Drawing;
using ArcEngine;
using ArcEngine.Audio;
using ArcEngine.Core;
using ArcEngine.Graphic;
using ArcEngine.Input;
using ArcEngine.Network;

using ArcEngine.Time;


public class entity : IEntity
{
	///
	/// Handle to the entity
	///
	Entity Entity;

	/// <summary>
	/// 
	/// </summary>
	/// <param name=""entity""></param>
	public bool Init(Entity entity)
	{
		Entity = entity;
		return true;
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name=""elapsed""></param>
	public void Update(int elapsed)
	{
	}
}


";
		}


		/// <summary>
		/// Load a script from a file
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void LoadButton_Click(object sender, EventArgs e)
		{
			OpenFileDialog dlg = new OpenFileDialog();
			dlg.Filter = "Script file (*.cs; *.txt; *.vb)|*.cs;*.txt;*.vb|All Files (*.*)|*.*";
			dlg.Title = "Select script file to open...";
			dlg.DefaultExt = ".cs";

			DialogResult res = dlg.ShowDialog();
			if (res != DialogResult.OK)
				return;

			// Open the stream and read it back.
			if (File.Exists(dlg.FileName))
			{
				string s = "";
				using (StreamReader sr = File.OpenText(dlg.FileName))
				{
					while (!sr.EndOfStream)
						s += sr.ReadLine() + Environment.NewLine;

					ScriptTxt.Text = s;
				}
			}

			// Back in the good directory
			Environment.CurrentDirectory = Application.StartupPath;

		}



		/// <summary>
		/// Text changed
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ScriptTxt_TextChanged(object sender, EventArgs e)
		{
			Model.Source = ScriptTxt.Text;
		}


		/// <summary>
		/// Save the script to the resource
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ToolSave_Click(object sender, EventArgs e)
		{
			Model.Source = ScriptTxt.Text;
		}


		/// <summary>
		/// Form closing
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ScriptForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			DialogResult result = MessageBox.Show("Script Editor", "Save modifciations ?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

			if (result == DialogResult.Yes)
			{
				Save();
			}
			else if (result == DialogResult.Cancel)
			{
				e.Cancel = true;
			}
		}

		#endregion




		#region Properties

		/// <summary>
		/// 
		/// </summary>
		public override IAsset Asset
		{
			get
			{
				return Model;
			}
		}

		/// <summary>
		/// ScriptModel to edit
		/// </summary>
		ScriptModel Model;

		#endregion

	}


}
