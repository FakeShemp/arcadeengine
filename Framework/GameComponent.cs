using System;
using System.Collections.Generic;

namespace ArcEngine
{
	/// <summary>
	/// Provides a modular way of adding functionality to a game.
	/// </summary>
	public class GameComponent
	{
		public GameComponent()
		{
			IsVisible = true;
			Enabled = true;


		}

		/// <summary>
		/// Called when the GameComponent needs to be initialized. 
		/// </summary>
		public virtual void Initialize()
		{
		}


		/// <summary>
		/// Called when the GameComponent needs to be updated. 
		/// </summary>
		/// <param name="time">Time elapsed since the last call to Update</param>
		public virtual void Update(GameTime time)
		{
		}


		/// <summary>
		/// Called when the DrawableGameComponent needs to be drawn. 
		/// </summary>
		public virtual void Draw()
		{
		}


		/// <summary>
		/// Indicates the order in which the GameComponent should be updated relative to other GameComponent instances. 
		/// Lower values are updated first. 
		/// </summary>
		public int UpdateOrder
		{
			get;
			protected set;
		}


		/// <summary>
		/// Order in which the component should be drawn.
		/// Lower values are updated first. 
		/// </summary>
		public int DrawOrder
		{
			get;
			protected set;
		}

		/// <summary>
		/// Indicates whether should be called when Game.Update is called.
		/// </summary>
		public bool Enabled
		{
			get;
			protected set;
		}


		/// <summary>
		/// Indicates whether Draw should be called.
		/// </summary>
		public bool IsVisible
		{
			get;
			protected set;
		}
	}
}
