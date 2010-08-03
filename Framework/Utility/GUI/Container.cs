using System;
using System.Collections.Generic;
using System.Text;

using ArcEngine.Graphic;

namespace ArcEngine.Utility.GUI
{
	/// <summary>
	/// Special control that contains other Control types.
	/// </summary>
	public abstract class Container : Control
	{

		/// <summary>
		/// Constructor
		/// </summary>
		public Container()
		{
		}


/*
		/// <summary>
		/// 
		/// </summary>
		/// <param name="manager">Gui manager handle</param>
		/// <param name="time"></param>
		public override void Update(GuiManager manager, GameTime time)
		{
			foreach (Control control in Controls)
				control.Update(manager, time);
		}



		/// <summary>
		/// 
		/// </summary>
		/// <param name="manager">Gui manager handle</param>
		/// <param name="batch"></param>
		public override void Draw(GuiManager manager, SpriteBatch batch)
		{
			if (!Visible)
				return;

			foreach (Control control in Controls)
				control.Draw(manager, batch);
		}
*/

		/// <summary>
		/// Adds a control to the container
		/// </summary>
		/// <param name="control">Control to add</param>
		public void Add(Control control)
		{
			if (control == null)
				return;

			control.Parent = this;
			Controls.Add(control);
		}


		/// <summary>
		/// Removes a control from the container
		/// </summary>
		/// <param name="control">Control to remove</param>
		/// <remarks>The control is no more valid after a successfull to this method</remarks>
		public bool Remove(Control control)
		{
			if (control == null)
				return false;

			if (Controls.Contains(control))
				return false;

			Controls.Remove(control);

			control.Dispose();

			return true;
		}



		/// <summary>
		/// Gets a control by its name
		/// </summary>
		/// <param name="name">Name of the conrtol to find</param>
		/// <returns>Handle to the control or null if not found</returns>
		public Control GetChild(string name)
		{
			if (string.IsNullOrEmpty(name))
				return null;


			foreach (Control control in Controls)
				if (control.Name.Equals(name))
					return control;

			return null;
		}



		#region Properties


		#endregion
	}
}
