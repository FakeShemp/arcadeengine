using System;
using System.Drawing;
using ArcEngine.Asset;
using ArcEngine.Graphic;
using ArcEngine.Input;


using System.Windows.Forms;


namespace ArcEngine.Utility.GUI
{
	/// <summary>
	/// A standard Check Box.
	/// </summary>
	public class CheckBox : ButtonBase
	{
		/// <summary>
		/// 
		/// </summary>
		public CheckBox()
		{
			Rectangle.Size = new Size(10, 10);
			Text = "Checkbox";
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

			Rectangle dst = Rectangle;

			if (Parent != null)
				dst.Offset(Parent.Location);

			if (manager.TileSet != null)
			{
				int tileid = Checked ? 9 : 10;

				Tile tile = manager.TileSet.GetTile(tileid);

				batch.DrawTile(manager.TileSet, tileid, dst.Location);

				dst.Offset(tile.Size.Width + 4, 0);
			}


			batch.DrawString(Font != null ? Font : manager.Font, dst.Location, Color.White, Text);
			
		}



		/// <summary>
		/// 
		/// </summary>
		/// <param name="manager">Gui manager handle</param>
		/// <param name="time"></param>
		public override void Update(GuiManager manager, GameTime time)
		{
/*
			// Mouse outside the control
			Rectangle dst = Rectangle;
			if (Parent != null)
				dst.Offset(Parent.Location);

			if (!dst.Contains(Mouse.Location))
			{
				base.Update(manager, time);
				return;
			}
			

			if (Mouse.IsNewButtonUp(System.Windows.Forms.MouseButtons.Left))
			{
				if (Checked)
				{
				}
				else
				{
				}

				if (CheckedChanged != null)
					CheckedChanged(this, new EventArgs());

				Checked = !Checked;
			}
*/

			base.Update(manager, time);
		}


		#region 


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

		#endregion


		#region Events

		/// <summary>
		/// Occurs when the value of the Checked property changes. 
		/// </summary>
		public event EventHandler CheckedChanged;




		#endregion


		#region

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
		public bool Checked;


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
}
