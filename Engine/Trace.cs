using System;
using System.Collections.Generic;
using System.Text;
using Diag = System.Diagnostics;



namespace ArcEngine
{
	/// <summary>
	/// Provides a set of methods and properties that help you trace the execution of your code.
	/// </summary>
	public static class Trace
	{
		/// <summary>
		/// 
		/// </summary>
		static Trace()
		{
			FileName = "trace.log";

			if (System.IO.File.Exists(FileName))
				System.IO.File.Delete(FileName);

			Diag.Trace.Listeners.Add(new Diag.TextWriterTraceListener(FileName));
			Diag.Trace.AutoFlush = true;
		}


		#region Assert

		/// <summary>
		/// Checks for a condition, and outputs the call stack if the condition is false.
		/// </summary>
		/// <param name="condition"></param>
		public static void Assert(bool condition)
		{
			Diag.Trace.Assert(condition);

			if (OnAssert != null)
				OnAssert();
		}


		/// <summary>
		/// Checks for a condition, and displays a message if the condition is false.
		/// </summary>
		/// <param name="condition"></param>
		/// <param name="message"></param>
		public static void Assert(bool condition, string message)
		{
			Diag.Trace.Assert(condition, message);

			if (OnAssert != null)
				OnAssert();
		}

		#endregion


		#region Write

		/// <summary>
		/// 
		/// </summary>
		/// <param name="message"></param>
		public static void Write(string message)
		{
			Diag.Trace.Write(message);

			if (OnTrace != null)
				OnTrace(message);
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="condition"></param>
		/// <param name="message"></param>
		public static void WriteIf(bool condition, string message)
		{
			Diag.Trace.WriteIf(condition, message);

			if (OnTrace != null)
				OnTrace(message);
		}



		#endregion


		#region WriteLine

		/// <summary>
		/// 
		/// </summary>
		/// <param name="message"></param>
		public static void WriteLine(string message)
		{
			Diag.Trace.WriteLine(message);

			if (OnTrace != null)
				OnTrace(message + Environment.NewLine);
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="condition"></param>
		/// <param name="message"></param>
		public static void WriteLineIf(bool condition, string message)
		{
			Diag.Trace.WriteLineIf(condition, message);

			if (OnTrace != null)
				OnTrace(message + Environment.NewLine);
		}



		#endregion


		#region Indent

		/// <summary>
		/// 
		/// </summary>
		public static void Indent()
		{
			Diag.Trace.Indent();
		}


		/// <summary>
		/// 
		/// </summary>
		public static void Unindent()
		{
			Diag.Trace.Unindent();
		}



		#endregion


		#region Events

		/// <summary>
		/// 
		/// </summary>
		/// <param name="message"></param>
		/// <returns></returns>
		public delegate void OnTraceEvent(string message);

		/// <summary>
		/// Event fired when a trace occure
		/// </summary>
		public static event OnTraceEvent OnTrace;


		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public delegate void OnAssertEvent();

		/// <summary>
		/// Event fired when an assert occure
		/// </summary>
		public static event OnAssertEvent OnAssert;




		#endregion

		#region Properties


		/// <summary>
		/// Filename of the log
		/// </summary>
		static string FileName;

		#endregion
	}
}
