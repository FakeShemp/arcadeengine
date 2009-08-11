using System;
using System.Collections.Generic;
using System.Text;
using ArcEngine.Graphic;



namespace ArcEngine.GUI
{

	/// <summary>
	/// 
	/// </summary>
	public class GuiManager
	{


		/// <summary>
		/// Constructor
		/// </summary>
		public GuiManager()
		{
			Elements = new List<GuiBase>();
		}




		#region Update and Draw

		/// <summary>
		/// Updates elements
		/// </summary>
		/// <param name="time"></param>
		internal void Update(GameTime time)
		{
			foreach (GuiBase element in Elements)
				element.Update(time);
		}



		/// <summary>
		/// Draws elements
		/// </summary>
		internal void Draw()
		{
			foreach (GuiBase element in Elements)
				element.Draw();
		}


		#endregion


		
		#region Element management

		/// <summary>
		/// Removes all gui elements
		/// </summary>
		public void Clear()
		{
			Elements.Clear();
		}


		/// <summary>
		/// Adds an element
		/// </summary>
		/// <param name="element"></param>
		public void Add(GuiBase element)
		{
			Elements.Add(element);
		}


		/// <summary>
		/// Removes an element
		/// </summary>
		/// <param name="element"></param>
		public void Remove(GuiBase element)
		{
			Elements.Remove(element);
		}


		#endregion



		#region Properties


		/// <summary>
		/// List of all gui elements
		/// </summary>
		List<GuiBase> Elements;

		#endregion

	}
}
