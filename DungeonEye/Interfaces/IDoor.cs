using System.Drawing;
using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonEye.Interfaces
{


	public interface IDoor
	{

		/// <summary>
		/// Update the door state
		/// </summary>
		/// <param name="door"></param>
		void OnUpdate(Door door);


		/// <summary>
		/// Draw the door
		/// </summary>
		/// <param name="door"></param>
		void OnDraw(Door door);

		/// <summary>
		/// Open the door
		/// </summary>
		/// <param name="door"></param>
		void OnOpen(Door door);



		/// <summary>
		/// Close the door
		/// </summary>
		/// <param name="door"></param>
		void OnClose(Door door);


		/// <summary>
		/// Mouse click on the door
		/// </summary>
		/// <param name="door"></param>
		/// <param name="location"></param>
		void OnClick(Door door, Point location);



	}
}
