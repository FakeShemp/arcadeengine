using System;
using System.Collections.Generic;
using ArcEngine.Graphic;
using System.Text;
using System.Drawing;

namespace ArcEngine.Examples.LightTest
{
	/// <summary>
	/// Light manager
	/// </summary>
	public class LightManager : IDisposable
	{

		/// <summary>
		/// Constructor
		/// </summary>
		public LightManager()
		{
			Lights = new List<Light>();
			Walls = new List<Wall>();


			Buffer = BatchBuffer.CreatePositionColorBuffer();
		}


		/// <summary>
		/// Dispose
		/// </summary>
		public void Dispose()
		{
			if (Buffer != null)
				Buffer.Dispose();
			Buffer = null;
		}

		/// <summary>
		/// 
		/// </summary>
		public void Render()
		{
			// Draw walls
			Buffer.Clear();
			foreach (Wall wall in Walls)
				Buffer.AddLine(wall.From, wall.To, Color.Red);

			int count = Buffer.Update();
			Display.DrawBatch(Buffer, 0, count);

			// Draw lights
			foreach (Light light in Lights)
				light.Render(Buffer);


		}



		#region Lights

		/// <summary>
		/// Adds a light
		/// </summary>
		/// <param name="light">Handle</param>
		public void AddLight(Light light)
		{
			if (light == null)
				return;

			Lights.Add(light);
		}


		/// <summary>
		/// Removes a light
		/// </summary>
		/// <param name="light">Handle</param>
		public void RemoveLight(Light light)
		{
			if (Lights.Contains(light))
				Lights.Remove(light);
		}

		#endregion



		#region Walls

		/// <summary>
		/// Adds a light
		/// </summary>
		/// <param name="Wall">Handle</param>
		public void AddWall(Wall wall)
		{
			if (wall == null)
				return;

			Walls.Add(wall);
		}


		/// <summary>
		/// Removes a light
		/// </summary>
		/// <param name="Wall">Handle</param>
		public void RemoveWall(Wall wall)
		{
			if (Walls.Contains(wall))
				Walls.Remove(wall);
		}
		#endregion

		
		#region Properties


		/// <summary>
		/// Draw buffer
		/// </summary>
		BatchBuffer Buffer;


		/// <summary>
		/// List of known lights
		/// </summary>
		List<Light> Lights;


		/// <summary>
		/// List of known walls
		/// </summary>
		List<Wall> Walls;

		#endregion
	}
}
