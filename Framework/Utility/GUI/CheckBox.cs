using System;
using System.Collections.Generic;
using ArcEngine.Graphic;
using System.Text;
using System.Drawing;

using ArcEngine.Asset;


namespace ArcEngine.Utility.GUI
{
	/// <summary>
	/// Checkbox
	/// </summary>
	public class CheckBox : Control
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
			base.Update(manager, time);
		}


		#region

		/// <summary>
		/// Is checked
		/// </summary>
		public bool Checked;



		/// <summary>
		/// Text to display
		/// </summary>
		public string Text;


		#endregion
	}
}
