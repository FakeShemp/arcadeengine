#region Licence
//
//This file is part of ArcEngine.
//Copyright (C)2008-2009 Adrien Hémery ( iliak@mimicprod.net )
//
//ArcEngine is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//any later version.
//
//ArcEngine is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.
//
//You should have received a copy of the GNU General Public License
//along with Foobar.  If not, see <http://www.gnu.org/licenses/>.
//
#endregion
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using ArcEngine;
using ArcEngine.Graphic;
using ArcEngine.Asset;


namespace Breakout
{
	/// <summary>
	/// 
	/// </summary>
	public class Brick
	{
/*
		/// <summary>
		/// Initialization
		/// </summary>
		/// <returns></returns>
		static public bool Init()
		{
			if (Tileset == null)
				Tileset = ResourceManager.CreateSharedAsset<TileSet>("Bricks");

			return true;
		}
*/


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

}
