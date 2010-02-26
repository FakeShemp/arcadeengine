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


using System.Windows.Forms;
using System.Timers;
using System;


//
//
// http://blogs.msdn.com/shawnhar/archive/2007/07/25/understanding-gametime.aspx
// Mise en place d'un timer : http://www.opentk.com/doc/chapter/2/glcontrol
//
namespace ArcEngine
{

	/// <summary>
	/// Snapshot of the game timing states
	/// </summary>
	public class GameTime
	{

		#region ctor


		/// <summary>
		/// Default constructor
		/// </summary>
		public GameTime()
		{
			Clock = new GameClock();
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="totalRealTime"></param>
		/// <param name="elapsedRealTime"></param>
		/// <param name="totalGameTime"></param>
		/// <param name="elapsedGameTime"></param>
		public GameTime(TimeSpan totalRealTime, TimeSpan elapsedRealTime, TimeSpan totalGameTime, TimeSpan elapsedGameTime)
			: this(totalRealTime, elapsedRealTime, totalGameTime, elapsedGameTime, false)
		{
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="totalRealTime"></param>
		/// <param name="elapsedRealTime"></param>
		/// <param name="totalGameTime"></param>
		/// <param name="elapsedGameTime"></param>
		/// <param name="isRunningSlowly"></param>
		public GameTime(TimeSpan totalRealTime, TimeSpan elapsedRealTime, TimeSpan totalGameTime, TimeSpan elapsedGameTime, bool isRunningSlowly) : base()
		{
			TotalRealTime = totalRealTime;
			ElapsedRealTime = elapsedRealTime;
			TotalGameTime = totalGameTime;
			ElapsedGameTime = elapsedGameTime;
			IsRunningSlowly = isRunningSlowly;
		}

		#endregion



		/// <summary>
		/// 
		/// </summary>
		public void Update()
		{
			Clock.Step();


			TotalRealTime = Clock.CurrentTime;
			ElapsedRealTime = Clock.ElapsedTime;

		}


		#region Properties

		/// <summary>
		/// The amount of elapsed game time since the last update.
		/// </summary>
		/// <value>Elapsed game time since the last update.</value>
		public TimeSpan ElapsedGameTime
		{
			get;
			internal set;
		}


		/// <summary>
		/// The amount of elapsed real time (wall clock) since the last frame.
		/// </summary>
		/// <value>Elapsed real time since the last frame.</value>
		public TimeSpan ElapsedRealTime
		{
			get;
			internal set;
		}


		/// <summary>
		/// Gets a value indicating that the game loop is taking longer than its Game.TargetElapsedTime.
		/// In this case, the game loop can be considered to be running too slowly and should do something to "catch up." 
		/// </summary>
		/// <value>true if the game loop is taking too long; false otherwise.</value>
		public bool IsRunningSlowly
		{
			get;
			internal set;
		}



		/// <summary>
		/// The amount of game time since the start of the game. Reference page contains links to related code samples.
		/// </summary>
		/// <value>Game time since the start of the game.</value>
		public TimeSpan TotalGameTime
		{
			get;
			internal set;
		}


		/// <summary>
		/// The amount of real time (wall clock) since the start of the game. Reference page contains links to related code samples.
		/// </summary>
		/// <value>Real time since the start of the game.</value>
		public TimeSpan TotalRealTime
		{
			get;
			internal set;
		}


		/// <summary>
		/// Returns the current time
		/// </summary>
		public DateTime Now
		{
			get
			{
				return DateTime.Now;
			}
		}



		/// <summary>
		/// 
		/// </summary>
		GameClock Clock;


		#endregion
	}

}
