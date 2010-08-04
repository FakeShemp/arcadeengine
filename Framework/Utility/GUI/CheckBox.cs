using System;
using System.Drawing;
using ArcEngine.Asset;
using ArcEngine.Graphic;
using ArcEngine.Input;



namespace ArcEngine.Utility.GUI
{
	/// <summary>
	/// A standard Check Box.
	/// </summary>
	public class CheckBox : ButtonBase
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public CheckBox()
		{
			Rectangle.Size = new Size(10, 10);
			Text = "Checkbox";
		}



		#region 


		protected override void OnMouseEnter(EventArgs e)
		{
			base.OnMouseEnter(e);
			Text = "Enter";
		}


		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			Text = "Leave";
		}

		/// <summary>
		/// Raises the Click event.
		/// </summary>
		/// <param name="e">An EventArgs that contains the event data.</param>
		protected override void OnMouseClick(EventArgs e)
		{
			switch (checkState)
			{
				case CheckState.Checked:
					CheckState = CheckState.Unchecked;
				break;
				case CheckState.Unchecked:
					CheckState = CheckState.Checked;
				break;
				default:
					CheckState = CheckState.Unchecked;
				break;
			}


			base.OnMouseClick(e);
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="e"></param>
		protected override void OnPaint(PaintEventArgs e)
		{
			if (!Visible)
				return;

			Rectangle dst = Rectangle;

			if (Parent != null)
				dst.Offset(Parent.Location);

			if (e.Manager.TileSet != null)
			{
				int tileid = Checked ? 9 : 10;

				Tile tile = e.Manager.TileSet.GetTile(tileid);

				e.Batch.DrawTile(e.Manager.TileSet, tileid, dst.Location);

				dst.Offset(tile.Size.Width + 4, 0);
			}


			e.Batch.DrawString(Font != null ? Font : e.Manager.Font, dst.Location, Color.White, Text);
			
			
			
			
			base.OnPaint(e);
		}


		#endregion


		#region Events

		/// <summary>
		/// Occurs when the value of the Checked property changes. 
		/// </summary>
		public event EventHandler CheckedChanged;




		#endregion


		#region OnEvent

		/// <summary>
		/// Raises the Click event. 
		/// </summary>
		/// <param name="e">An EventArgs that contains the event data</param>
		protected virtual void OnCheckChanged(EventArgs e)
		{
			if (CheckedChanged!= null)
				CheckedChanged(this, e);
		}



		#endregion


		#region Properties

		/// <summary>
		/// Is checked
		/// </summary>
		public bool Checked
		{
			get
			{
				return checkState == CheckState.Checked;
			}
			set
			{
				CheckState = value ? CheckState.Checked : CheckState.Unchecked;
			}
		}


		/// <summary>
		/// Gets or sets the state of the CheckBox. 
		/// </summary>
		public CheckState CheckState
		{
			get
			{
				return checkState;
			}
			set
			{
				if (checkState == value)
					return;

				checkState = value;
				OnCheckChanged(EventArgs.Empty);
			}
		}
		CheckState checkState;

		#endregion
	}


	/// <summary>
	/// Specifies the state of a control, such as a check box, that can be checked, unchecked.
	/// </summary>
	public enum CheckState
	{
		/// <summary>
		/// The control is checked.
		/// </summary>
		Checked,

		/// <summary>
		/// The control is unchecked.
		/// </summary>
		Unchecked,
	}
}
