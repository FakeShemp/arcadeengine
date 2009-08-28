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
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using ArcEngine.Asset;
using ArcEngine.Forms;
using ArcEngine.Properties;
using ArcEngine.Providers;
using DigitalRune.Windows.TextEditor;
using DigitalRune.Windows.TextEditor.Completion;
using DigitalRune.Windows.TextEditor.Document;
using DigitalRune.Windows.TextEditor.Folding;
using DigitalRune.Windows.TextEditor.Highlighting;


namespace ArcEngine.Editor
{
	/// <summary>
	/// Edit script
	/// </summary>
	internal partial class ScriptForm : AssetEditor
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public ScriptForm(XmlNode node)
		{
			InitializeComponent();



			Script = new Script();
			Script.Load(node);


			ScriptTxt.Text = Script.SourceCode;



			// Script models
			//ScriptProvider provider = ResourceManager.GetProvider<ScriptProvider>();
			//InsertModelButton.DropDownItems.Clear();
			//foreach (string name in ResourceManager.GetAssets<ScriptModel>())
			//{
			//   InsertModelButton.DropDownItems.Add(new ToolStripMenuItem(name));
			//}


			// Find all interfaces
			InsertModelButton.DropDownItems.Clear();
			foreach (Type t in Assembly.GetEntryAssembly().GetTypes())
			{
				if (t.IsInterface)
				{
					ToolStripMenuItem item = new ToolStripMenuItem(t.Name);
					item.Tag = t;
					item.Click +=new EventHandler(OnInsertCode);
					InsertModelButton.DropDownItems.Add(item);
				}
			}


			// Set the syntax-highlighting for C#
			ScriptTxt.Document.HighlightingStrategy = HighlightingManager.Manager.FindHighlighter("C#");

			// Set a simple folding strategy that folds all "{ ... }" blocks
			ScriptTxt.Document.FoldingManager.FoldingStrategy = new CodeFoldingStrategy();

			// Try to set font, because it's a lot prettier:
			Font font = new Font("Courier New", 9.0f);
			if (font.Name == "Courier New")
				ScriptTxt.Font = font;


		}



		/// <summary>
		/// Code completion
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void CompletionRequest(object sender, CompletionEventArgs e)
		{
			if (ScriptTxt.CompletionWindowVisible)
				return;

			// e.Key contains the key that the user wants to insert and which triggered
			// the CompletionRequest.
			// e.Key == '\0' means that the user triggered the CompletionRequest by pressing <Ctrl> + <Space>.

			if (e.Key == '\0')
			{
				// The user has requested the completion window by pressing <Ctrl> + <Space>.
				ScriptTxt.ShowCompletionWindow(new CodeCompletionDataProvider(), e.Key, false);
			}
			else if (char.IsLetter(e.Key))
			{
				// The user is typing normally. 
				// -> Show the completion to provide suggestions. Automatically close the window if the 
				// word the user is typing does not match the completion data. (Last argument.)
				ScriptTxt.ShowCompletionWindow(new CodeCompletionDataProvider(), e.Key, true);
			}
		}



		/// <summary>
		/// save the asset to the manager
		/// </summary>
		public override void Save()
		{
			Script.SourceCode = ScriptTxt.Text;

			StringBuilder sb = new StringBuilder();
			using (XmlWriter writer = XmlWriter.Create(sb))
				Script.Save(writer);

			string xml = sb.ToString();
			XmlDocument doc = new XmlDocument();
			doc.LoadXml(xml);

			ResourceManager.AddAsset<Script>(Script.Name, doc.DocumentElement);
		}




		#region Events

		/// <summary>
		/// Compiles the script
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Compile_OnClick(object sender, EventArgs e)
		{

			if (Script != null)
			{
				//Log.Send(new LogEventArgs(LogLevel.Info, "Compiling...", null));
				Trace.WriteLine("Compiling...");

				ErrorListView.Items.Clear();

				Script.SourceCode = ScriptTxt.Text;
				if (!Script.Compile())
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
			Script.SourceCode = ScriptTxt.Text;
		}


		/// <summary>
		/// Save the script to the resource
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ToolSave_Click(object sender, EventArgs e)
		{
			Script.SourceCode = ScriptTxt.Text;
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

		/// <summary>
		/// Clear source code
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ClearScriptBox_Click(object sender, EventArgs e)
		{
			ScriptTxt.Document.TextContent = string.Empty;
			ScriptTxt.Update();
		}


		/// <summary>
		/// Insert code
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnInsertCode(object sender, EventArgs e)
		{
			ToolStripMenuItem item = sender as ToolStripMenuItem;
			Type type = item.Tag as Type;

			string code = string.Empty;

			code = "public class Class : " + type.Name + Environment.NewLine;
			code += "{" + Environment.NewLine + "\t" + Environment.NewLine;



			foreach (MethodInfo mi in type.GetMethods())
			{
				code += "\tpublic " + mi.ReturnType.Name + " " + mi.Name + "(";

				foreach (ParameterInfo pi in mi.GetParameters())
				{
					code += pi.ToString() + ", ";
				}

				code = code.Remove(code.Length - 2, 2);
				code += ")" + Environment.NewLine;
				code += "\t{" + Environment.NewLine + "\t}" + Environment.NewLine;

				code += Environment.NewLine;
			}

			code += "}" + Environment.NewLine;

			ScriptTxt.ActiveTextAreaControl.TextArea.InsertString(code);

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
				return Script;
			}
		}

		/// <summary>
		/// Script to edit
		/// </summary>
		Script Script;

		#endregion


	}



	/// <summary>
	/// 
	/// </summary>
	class CodeCompletionDataProvider : AbstractCompletionDataProvider
	{
		/// <summary>
		/// 
		/// </summary>
		private readonly ImageList _imageList;


		/// <summary>
		/// 
		/// </summary>
		public override ImageList ImageList
		{
			get
			{
				return _imageList;
			}
		}


		/// <summary>
		/// 
		/// </summary>
		public CodeCompletionDataProvider()
		{
			// Create the image-list that is needed by the completion windows
			_imageList = new ImageList();
			_imageList.Images.Add(Resources.public_property);
			_imageList.Images.Add(Resources.public_static);
			_imageList.Images.Add(Resources.public_method);
			_imageList.Images.Add(Resources.snippet);


		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="fileName"></param>
		/// <param name="textArea"></param>
		/// <param name="charTyped"></param>
		/// <returns></returns>
		public override ICompletionData[] GenerateCompletionData(string fileName, TextArea textArea, char charTyped)
		{
			// This class provides the data for the Code-Completion-Window.
			// Some random variables and methods are returned as completion data.

			List<ICompletionData> completionData = new List<ICompletionData>
			{
				// Add some random variables
				new DefaultCompletionData("a", "A local variable", 1),
				new DefaultCompletionData("b", "A local variable", 1),
				new DefaultCompletionData("variableX", "A local variable", 1),
				new DefaultCompletionData("variableY", "A local variable", 1),

				// Add some random methods
				new DefaultCompletionData("MethodA", "A simple method.", 2),
				new DefaultCompletionData("MethodB", "A simple method.", 2),
				new DefaultCompletionData("MethodC", "A simple method.", 2)
			};


			// Add some snippets (text templates).
			List<Snippet> snippets = new List<Snippet>
				{
				new Snippet("for", "for (|)\n{\n}", "for loop"),
				new Snippet("if", "if (|)\n{\n}", "if statement"),
				new Snippet("ifel", "if (|)\n{\n}\nelse\n{\n}", "if-else statement"),
				new Snippet("while", "while (|)\n{\n}", "while loop"),
				new Snippet("foreach", "foreach (|)\n{\n}\n", "foreach() statement"),
			};

			// Add the snippets to the completion data
			foreach (Snippet snippet in snippets)
				completionData.Add(new SnippetCompletionData(snippet, 3));

			return completionData.ToArray();
		}
	}



	/// <summary>
	/// 
	/// </summary>
	class CodeFoldingStrategy : IFoldingStrategy
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="document"></param>
		/// <param name="fileName"></param>
		/// <param name="parseInformation"></param>
		/// <returns></returns>
		public List<Fold> GenerateFolds(IDocument document, string fileName, object parseInformation)
		{

			// This is a simple folding strategy.
			// It searches for matching brackets ('{', '}') and creates folds
			// for each region.

			List<Fold> folds = new List<Fold>();
			for (int offset = 0; offset < document.TextLength; ++offset)
			{
				char c = document.GetCharAt(offset);
				if (c == '{')
				{
					int offsetOfClosingBracket = TextHelper.FindClosingBracket(document, offset + 1, '{', '}');
					if (offsetOfClosingBracket > 0)
					{
						int length = offsetOfClosingBracket - offset + 1;
						folds.Add(new Fold(document, offset, length, "{...}", false));
					}
				}
			}
			return folds;
		}
	}

}
