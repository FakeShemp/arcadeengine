using System;
using System.Drawing;
using System.Windows.Forms;
using ArcEngine.Graphic;

namespace ArcEngine.Input
{

	/// <summary>
	/// Mouse 
	/// </summary>
	public static class Mouse
	{
		/// <summary>
		/// Initialize the mouse handler
		/// </summary>
		/// <param name="form"></param>
		static internal void Init(Form form)
		{
			Trace.WriteLine("Init mouse...");
			Trace.Indent();
			if (form == null)
				throw new ArgumentNullException("form");

			Form = form;

			Trace.Unindent();
			Trace.WriteLine("OK");
		}



		/// <summary>
		/// Updates the mouse state
		/// </summary>
		static internal void Update()
		{
			PreviousState = Buttons;
			Buttons = Form.MouseButtons;
			
		}


		/// <summary>
		/// New mouse button Up
		/// </summary>
		/// <param name="button"></param>
		/// <returns></returns>
		static public bool IsNewButtonUp(MouseButtons button)
		{
			return (PreviousState == button) && (Buttons != button);
		}


		/// <summary>
		/// New mouse button Down
		/// </summary>
		/// <param name="button"></param>
		/// <returns></returns>
		static public bool IsNewButtonDown(MouseButtons button)
		{
			return (PreviousState != button) && (Buttons == button);
		}






		#region Updaters

		/// <summary>
		/// ButtonDown event
		/// </summary>
		/// <param name="e"></param>
		internal static void ButtonDown(MouseEventArgs e)
		{
			if (OnButtonDown != null)
				OnButtonDown(null, e);
		}


		/// <summary>
		/// ButtonUp event
		/// </summary>
		/// <param name="e"></param>
		internal static void ButtonUp(MouseEventArgs e)
		{
			if (OnButtonUp != null)
				OnButtonUp(null, e);
		}


		/// <summary>
		/// Move event
		/// </summary>
		/// <param name="e"></param>
		internal static void Move(MouseEventArgs e)
		{
			lastPostion = e.Location;

			if (OnMove != null)
				OnMove(null, e);
		}


		/// <summary>
		/// DoubleClick event
		/// </summary>
		/// <param name="e"></param>
		internal static void DoubleClick(MouseEventArgs e)
		{
			if (OnDoubleClick != null)
				OnDoubleClick(null, e);
		}

		#endregion


		#region Properties


		/// <summary>
		/// Form capturing mouse input
		/// </summary>
		static Form Form;


		/// <summary>
		/// Gets/sets the mouse Offset
		/// </summary>
		public static Point Location
		{
			get
			{
				if (Form == null)
					throw new ArgumentNullException("Form", "You must initialize the class first !");
				return Form.PointToClient(Cursor.Position);
			}
			set
			{
				Cursor.Position = Form.PointToScreen(value);
			}
		}


		/// <summary>
		/// Last known location
		/// </summary>
		static Point lastPostion = Point.Empty;


		/// <summary>
		///  Gets/sets the cursor visibility
		/// </summary>
		public static bool Visible
		{
			get
			{
				return visible;
			}
			set
			{
				visible = value;
				if (value)
					Cursor.Show();
				else
					Cursor.Hide();
			}
		}
		static bool visible;
 

		/// <summary>
		/// Gets and sets whether or not the mouse cursor is visible. 
		/// </summary>
		public static Cursor Cursor
		{
			get
			{
				if (Form == null)
					throw new ArgumentNullException("Form", "You must initialize the class first !");
				
				return Form.Cursor;
			}
			set
			{
				if (Form == null)
					throw new ArgumentNullException("Form", "You must initialize the class first !");
				Form.Cursor = value;
			}
		}


		/// <summary>
		/// Gets the mouse button states
		/// </summary>
		static public MouseButtons Buttons
		{
			get;
			//{
			//   if (Form == null)
			//      throw new ArgumentNullException("Form", "You must initialize the class first !");

			//   return buttons;
			//}
			private set;
		}
//		static MouseButtons buttons = new MouseButtons();

		/// <summary>
		/// Previous mouse buttons state
		/// </summary>
		static MouseButtons PreviousState;

		#endregion


		#region Events
		/// <summary>
		///  Event fired when a ButtonDown occur
		/// </summary>
		public static event EventHandler<MouseEventArgs> OnButtonDown;


		/// <summary>
		/// Event fired when a ButtonUp occurs
		/// </summary>
		public static event EventHandler<MouseEventArgs> OnButtonUp;

		/// <summary>
		/// Event fired when a mouse move occurs
		/// </summary>
		public static event EventHandler<MouseEventArgs> OnMove;

		/// <summary>
		/// Event fired when a double click occurs
		/// </summary>
		public static event EventHandler<MouseEventArgs> OnDoubleClick;

		#endregion

	}



}
