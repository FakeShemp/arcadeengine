using System;
using System.Collections.Generic;
using ArcEngine.Graphic;
using System.Text;
using System.Drawing;

using ArcEngine.Asset;
using ArcEngine.Input;

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


			base.Update(manager, time);
		}


		#region Events

		/// <summary>
		/// Occurs when the value of the Checked property changes. 
		/// </summary>
		public event EventHandler CheckedChanged;


		#endregion



		#region Properties

		/// <summary>
		/// Is checked
		/// </summary>
		public bool Checked;


		#endregion
	}
}
