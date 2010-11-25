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

using System.IO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using Diag = System.Diagnostics;

namespace ArcEngine
{
	/// <summary>
	/// Provides a set of methods and properties that help you trace the execution of your code.
	/// </summary>
	public static class Trace
	{
		///// <summary>
		///// Constructor
		///// </summary>
		//static Trace()
		//{
		//    //Start("log.html");
		//}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="filename">Output filename</param>
		/// <param name="title">Title of the log</param>
		/// <returns></returns>
		static public bool Start(string filename, string title)
		{
			FileName = filename;

			if (System.IO.File.Exists(FileName))
				System.IO.File.Delete(FileName);


			Stream = new StreamWriter(FileName);


			//Diag.Trace.Listeners.Add(new Diag.TextWriterTraceListener(Stream));
			Diag.Trace.AutoFlush = true;


			string header = @"<html>
				<head>
				<meta http-equiv='Content-Type' content='text/html; charset=utf-8' />
				<title>ArcEngine Log</title>
				<style type='text/css'>
				body, html {
				background: #000000;
				width: 95%;
				font-family: Verdana;
				font-size: 12px;
				color: #C0C0C0;
				}
				table {
				background: #292929;
				font-family: Verdana;
				font-size: 12px;
				width = 100%
				color: #C0C0C0;
				}
				h1 {
				color : #FFFFFF;
				border-bottom : 1px dotted #888888;
				}
				pre {
				font-family : Verdana;
				margin : 0;
				}
				.box {
				border : 1px dotted #818286;
				padding : 5px;
				margin: 5px;
				width: 100%;
				background-color : #292929;
				}
				.err {
				color: #EE1100;
				font-weight: bold
				}
				.warn {
				color: #FFCC00;
				font-weight: bold
				}
				.info {
				color: #C0C0C0;
				}
				.debug {
				color: #CCA0A0;
				}
				</style>
				</head>

				<body>
				<h1>ArcEngine : <a href='http://www.mimicprod.net'>www.mimicprod.net</a></h1>
				<h3>" + title + @"</h3>
				<div class='box'>
				<table>";

			Stream.WriteLine(header);

			return true;
		}



		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		static public bool Close()
		{
			string footer = @"</tr>
				</table>
				</div>
				</body>
				</html>";

			WriteLine(footer);

			Diag.Trace.Flush();
			Diag.Trace.Close();

			if (Stream != null)
			{
				Stream.Close();
				Stream.Dispose();
				Stream = null;
			}

			FileName = "";

			return true;
		}


		#region Inventory


		/// <summary>
		/// Log inventory 
		/// </summary>
		public static void TraceInventory()
		{
			WriteLine(DateTime.Now.ToString());
			WriteLine("Hardware informations :");
			Indent();
			WriteLine("OS : {0}", Environment.OSVersion.ToString());
			WriteLine("Platform : {0}", IntPtr.Size == 4 ? "x86" : "x64");
			WriteLine("SP : {0}", Environment.OSVersion.ServicePack);
			WriteLine("Processor count : {0}", Environment.ProcessorCount);
			Unindent();

			WriteLine("Software informations :");
			Indent();
			WriteLine("CLR : {0}", Environment.Version.ToString());
			Unindent();

			Unindent();

			WriteLine("ArcEngine assembly :");
			Indent();
			Assembly asb = Assembly.GetCallingAssembly();
			WriteLine("{0}", asb.ToString());

			FileInfo info = new FileInfo(asb.Location);
			WriteLine("Creation time : {0}", info.CreationTime.ToString());


			Unindent();

			// Look through each loaded dll
			List<string> list = new List<string>();
			foreach (AssemblyName name in Assembly.GetExecutingAssembly().GetReferencedAssemblies())
				list.Add(name.FullName);
			list.Sort();

			WriteLine("Referenced assemblies :");
			Indent();
			foreach (string name in list)
				WriteLine(" - {0}", name);
			Unindent();
		}

		#endregion


		#region Assert

		/// <summary>
		/// Checks for a condition, and outputs the call stack if the condition is false.
		/// </summary>
		/// <param name="condition">Condition</param>
		public static void Assert(bool condition)
		{
			Diag.Trace.Assert(condition);

			if (OnAssert != null)
				OnAssert();
		}


		/// <summary>
		/// Checks for a condition, and displays a message if the condition is false.
		/// </summary>
		/// <param name="condition">Condition</param>
		/// <param name="message">Message to trace</param>
		public static void Assert(bool condition, string message)
		{
			Diag.Trace.Assert(condition, message);

			if (OnAssert != null)
				OnAssert();
		}

		#endregion


		#region Write

		/// <summary>
		/// Writes a message
		/// </summary>
		/// <param name="message">Message to write</param>
		public static void Write(string message)
		{
			// Need to start a new line
			if (!NewLineStarted)
			{
				Diag.Trace.Write("<tr><td>");
				NewLineStarted = true;
			}

			Diag.Trace.Write(message);

			if (OnTrace != null)
				OnTrace(message);
		}



		/// <summary>
		/// Writes to the log a formated string
		/// </summary>
		/// <param name="format">String format</param>
		/// <param name="args">Arguments</param>
		public static void Write(string format, params object[] args)
		{
			Write(string.Format(format, args));
		}



		/// <summary>
		/// Writes a conditional message
		/// </summary>
		/// <param name="condition">Condition</param>
		/// <param name="message">Message</param>
		public static void WriteIf(bool condition, string message)
		{
			if (condition)
				Write(message);


			// Need to start a new line
			//if (!NewLineStarted)
			//{
			//    Diag.Trace.Write("<tr><td>");
			//    NewLineStarted = true;
			//}

			//Diag.Trace.WriteIf(condition, message);

			//if (OnTrace != null)
			//    OnTrace(message);
		}


		/// <summary>
		/// Writes a conditional message
		/// </summary>
		/// <param name="condition">Condition</param>
		/// <param name="format">String format</param>
		/// <param name="args">Arguments</param>
		public static void WriteIf(bool condition, string format, params object[] args)
		{
			WriteIf(condition, string.Format(format, args));
		}


		#endregion


		#region WriteLine

		/// <summary>
		/// Writes a line to the log
		/// </summary>
		/// <param name="message">Message</param>
		public static void WriteLine(string message)
		{
			// Need to start a new line
			if (!NewLineStarted)
				Stream.Write("<tr><td>");


			Stream.WriteLine(message + "</td></tr>");

			Diag.Trace.WriteLine(message);
			NewLineStarted = false;

			if (OnTrace != null)
				OnTrace(message + Environment.NewLine);
		}


		/// <summary>
		/// Writes a line to the log
		/// </summary>
		/// <param name="format">String format</param>
		/// <param name="args">Arguments</param>
		public static void WriteLine(string format, params object[] args)
		{
			WriteLine(string.Format(format, args));
		}


		/// <summary>
		/// Writes a conditional line
		/// </summary>
		/// <param name="condition">Condition</param>
		/// <param name="message">Message</param>
		public static void WriteLineIf(bool condition, string message)
		{
			if (condition)
				WriteLine(message);

			//// Need to start a new line
			//if (!NewLineStarted)
			//    Diag.Trace.Write("<tr><td>");

			//Diag.Trace.WriteLineIf(condition, "<tr><td>" + message + "</td></tr>\n");
			//NewLineStarted = false;

			//if (OnTrace != null)
			//    OnTrace(message + Environment.NewLine);
		}


		/// <summary>
		/// Writes a conditional line to the log
		/// </summary>
		/// <param name="condition">Condition</param>
		/// <param name="format">String format</param>
		/// <param name="args">Arguments</param>
		public static void WriteLineIf(bool condition, string format, params object[] args)
		{
			WriteLineIf(condition, string.Format(format, args));
		}



		#endregion


		#region Indent

		/// <summary>
		/// Indent
		/// </summary>
		public static void Indent()
		{
			Diag.Trace.Indent();
		}


		/// <summary>
		/// Unindent
		/// </summary>
		public static void Unindent()
		{
			Diag.Trace.Unindent();
		}



		#endregion


		#region Events

		/// <summary>
		/// Event fired when a trace occure
		/// </summary>
		/// <param name="message"></param>
		/// <returns></returns>
		public delegate void OnTraceEvent(string message);

		/// <summary>
		/// Event fired when a trace occure
		/// </summary>
		public static event OnTraceEvent OnTrace;


		/// <summary>
		/// Event fired when an assert occure
		/// </summary>
		/// <returns></returns>
		public delegate void OnAssertEvent();

		/// <summary>
		/// Event fired when an assert occure
		/// </summary>
		public static event OnAssertEvent OnAssert;




		#endregion


		#region Debug

		/// <summary>
		/// Writes a line to the log
		/// </summary>
		/// <param name="message">Message</param>
		[Conditional("DEBUG")]
		public static void WriteDebugLine(string message)
		{
			WriteLine(message);

			// Need to close the line
			//if (NewLineStarted)
			//    Stream.Write("</td></tr>");

			//Stream.WriteLine(message + "</td></tr>");

			//Diag.Trace.WriteLine(message);
			//NewLineStarted = false;

			//if (OnTrace != null)
			//    OnTrace(message + Environment.NewLine);
		}

		/// <summary>
		/// Writes a line to the log
		/// </summary>
		/// <param name="format">String format</param>
		/// <param name="args">Arguments</param>
		[Conditional("DEBUG")]
		public static void WriteDebugLine(string format, params object[] args)
		{
			WriteLine(string.Format(format, args));
		}


		/// <summary>
		/// Writes a conditional line
		/// </summary>
		/// <param name="condition">Condition</param>
		/// <param name="message">Message</param>
		[Conditional("DEBUG")]
		public static void WriteDebugLineIf(bool condition, string message)
		{
			if (condition)
				WriteDebugLine(message);

			// Need to close the line
			//if (NewLineStarted)
			//{
			//    Diag.Trace.Write("</td></tr>");
			//    NewLineStarted = false;
			//}

			//Diag.Trace.WriteLine("<tr><td>" + message + "</td></tr>\n");

			//if (OnTrace != null)
			//    OnTrace(message + Environment.NewLine);
		}

		/// <summary>
		/// Writes a conditional line to the log
		/// </summary>
		/// <param name="condition">Condition</param>
		/// <param name="format">String format</param>
		/// <param name="args">Arguments</param>
		[Conditional("DEBUG")]
		public static void WriteDebugLineIf(bool condition, string format, params object[] args)
		{
			WriteLineIf(condition, string.Format(format, args));
		}



		#endregion


		#region Properties


		/// <summary>
		/// Filename of the log
		/// </summary>
		public static string FileName
		{
			get;
			private set;
		}


		/// <summary>
		/// Output stream
		/// </summary>
		static StreamWriter Stream;


		/// <summary>
		/// True if a new line is started
		/// </summary>
		static bool NewLineStarted;


		#endregion
	}
}
