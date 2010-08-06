using System;
using System.Collections.Generic;
using System.Text;

using ArcEngine.Graphic;

namespace ArcEngine.Utility.GUI
{
	/// <summary>
	/// Special control that contains other Control types.
	/// </summary>
	public abstract class ContainerControl : Control
	{

		/// <summary>
		/// Constructor
		/// </summary>
		public ContainerControl()
		{
		}


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
