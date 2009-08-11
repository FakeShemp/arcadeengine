﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using ArcEngine;
using ArcEngine.Graphic;
using ArcEngine.Asset;


namespace Breakout
{

	/// <summary>
	/// Bonus given by a brick 
	/// </summary>
	public enum BrickBonus
	{
		/// <summary>
		/// No bonus
		/// </summary>
		None,


		/// <summary>
		/// 
		/// </summary>
		ExtraScore_200,


		/// <summary>
		/// 
		/// </summary>
		ExtraScore_500,


		/// <summary>
		/// 
		/// </summary>
		ExtraScore_1000,


		/// <summary>
		/// 
		/// </summary>
		ExtraScore_2000,


		/// <summary>
		/// 
		/// </summary>
		ExtraScore_5000,


		/// <summary>
		/// 
		/// </summary>
		ExtraScore_10K,


		/// <summary>
		/// 
		/// </summary>
		Rainbow,


		/// <summary>
		/// 
		/// </summary>
		ExpandPaddle,


		/// <summary>
		/// 
		/// </summary>
		ExtraLife,


		/// <summary>
		/// 
		/// </summary>
		StickyPaddle,


		/// <summary>
		/// 
		/// </summary>
		EnergyBalls,


		/// <summary>
		/// 
		/// </summary>
		ExtraBall,


		/// <summary>
		/// 
		/// </summary>
		Floor,


		/// <summary>
		/// 
		/// </summary>
		Weapon,


		/// <summary>
		/// 
		/// </summary>
		SpeedDown,


		/// <summary>
		/// 
		/// </summary>
		Joker,


		/// <summary>
		/// 
		/// </summary>
		ExplosiveBalls,


		/// <summary>
		/// 
		/// </summary>
		BonusMagnet,


		/// <summary>
		/// 
		/// </summary>
		Reset,


		/// <summary>
		/// 
		/// </summary>
		TimeAdd,


		/// <summary>
		/// 
		/// </summary>
		RandomExtra,


		/// <summary>
		/// 
		/// </summary>
		SpeedUp,


		/// <summary>
		/// 
		/// </summary>
		FrozenPaddle,


		/// <summary>
		/// 
		/// </summary>
		ShrinkPaddle,


		/// <summary>
		/// 
		/// </summary>
		LightsOut,


		/// <summary>
		/// 
		/// </summary>
		Chaos,


		/// <summary>
		/// 
		/// </summary>
		Ghostly,


		/// <summary>
		/// 
		/// </summary>
		MalusMagnet,


		/// <summary>
		/// 
		/// </summary>
		WeakBalls,
	}



	/// <summary>
	/// 
	/// </summary>
	public class Brick
	{

		/// <summary>
		/// Initialization
		/// </summary>
		/// <returns></returns>
		static public bool Init()
		{
			if (Tileset == null)
				Tileset = ResourceManager.CreateAsset<TileSet>("Bricks");

			Tileset.Scale = new SizeF(2.0f, 2.0f);
			return true;
		}



		/// <summary>
		/// Updates the status of the brick
		/// </summary>
		/// <param name="time"></param>
		public void Update(GameTime time)
		{
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="device"></param>
		/// <param name="location"></param>
		public void Draw(Point location)
		{
			Tileset.Draw(14, location);



		//	Tileset.Draw(15, new Rectangle(location, new Size(164, 64)), TextureLayout.Zoom);
		}



		#region Properties

		/// <summary>
		/// Bonus of the brick
		/// </summary>
		public BrickBonus Bonus;


		/// <summary>
		/// Brick destroyed
		/// </summary>
		public bool Hit;


		/// <summary>
		/// Tileset of the bricks
		/// </summary>
		static TileSet Tileset;


		/// <summary>
		/// Id of the tile
		/// </summary>
		public int TileID
		{
			get;
			protected set;
		}

		#endregion
	}
}
