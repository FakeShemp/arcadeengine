using System;
using System.Collections.Generic;
using System.Text;


namespace ArcEngine
{

	/// <summary>
	/// 
	/// </summary>
	internal class GameClock
	{




		/// <summary>
		/// Constructor
		/// </summary>
		public GameClock()
		{
			this.Reset();
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="delta"></param>
		/// <returns></returns>
		private static TimeSpan CounterToTimeSpan(long delta)
		{
			return TimeSpan.FromTicks((delta * 10000000L) / Frequency);
		}



		/// <summary>
		/// 
		/// </summary>
		internal void Reset()
		{
			this.currentTimeBase = TimeSpan.Zero;
			this.currentTimeOffset = TimeSpan.Zero;
			this.baseRealTime = Counter;
			this.lastRealTimeValid = false;
		}


		/// <summary>
		/// 
		/// </summary>
		internal void Resume()
		{
			this.suspendCount--;
			if (this.suspendCount <= 0)
			{
				long counter = Counter;
				this.timeLostToSuspension += counter - this.suspendStartTime;
				this.suspendStartTime = 0L;
			}
		}


		/// <summary>
		/// 
		/// </summary>
		internal void Step()
		{
			long counter = Counter;
			if (!this.lastRealTimeValid)
			{
				this.lastRealTime = counter;
				this.lastRealTimeValid = true;
			}
			try
			{
				this.currentTimeOffset = CounterToTimeSpan(counter - this.baseRealTime);
			}
			catch (OverflowException)
			{
				this.currentTimeBase += this.currentTimeOffset;
				this.baseRealTime = this.lastRealTime;
				try
				{
					this.currentTimeOffset = CounterToTimeSpan(counter - this.baseRealTime);
				}
				catch (OverflowException)
				{
					this.baseRealTime = counter;
					this.currentTimeOffset = TimeSpan.Zero;
				}
			}
			try
			{
				this.elapsedTime = CounterToTimeSpan(counter - this.lastRealTime);
			}
			catch (OverflowException)
			{
				this.elapsedTime = TimeSpan.Zero;
			}
			try
			{
				long num2 = this.lastRealTime + this.timeLostToSuspension;
				this.elapsedAdjustedTime = CounterToTimeSpan(counter - num2);
				this.timeLostToSuspension = 0L;
			}
			catch (OverflowException)
			{
				this.elapsedAdjustedTime = TimeSpan.Zero;
			}
			this.lastRealTime = counter;
		}


		/// <summary>
		/// 
		/// </summary>
		internal void Suspend()
		{
			this.suspendCount++;
			if (this.suspendCount == 1)
			{
				this.suspendStartTime = Counter;
			}
		}



		#region Properties

		/// <summary>
		/// 
		/// </summary>
		internal static long Counter
		{
			get
			{
				return System.Diagnostics.Stopwatch.GetTimestamp();
			}
		}


		/// <summary>
		/// 
		/// </summary>
		internal TimeSpan CurrentTime
		{
			get
			{
				return (this.currentTimeBase + this.currentTimeOffset);
			}
		}


		/// <summary>
		/// 
		/// </summary>
		internal TimeSpan ElapsedAdjustedTime
		{
			get
			{
				return this.elapsedAdjustedTime;
			}
		}


		/// <summary>
		/// 
		/// </summary>
		internal TimeSpan ElapsedTime
		{
			get
			{
				return this.elapsedTime;
			}
		}



		/// <summary>
		/// 
		/// </summary>
		internal static long Frequency
		{
			get
			{
				return System.Diagnostics.Stopwatch.Frequency;
			}
		}




		/// <summary>
		/// 
		/// </summary>
		private long baseRealTime;


		/// <summary>
		/// 
		/// </summary>
		private TimeSpan currentTimeBase;


		/// <summary>
		/// 
		/// </summary>
		private TimeSpan currentTimeOffset;


		/// <summary>
		/// 
		/// </summary>
		private TimeSpan elapsedAdjustedTime;


		/// <summary>
		/// 
		/// </summary>
		private TimeSpan elapsedTime;


		/// <summary>
		/// 
		/// </summary>
		private long lastRealTime;


		/// <summary>
		/// 
		/// </summary>
		private bool lastRealTimeValid;


		/// <summary>
		/// 
		/// </summary>
		private int suspendCount;


		/// <summary>
		/// 
		/// </summary>
		private long suspendStartTime;

		/// <summary>
		/// 
		/// </summary>
		private long timeLostToSuspension;

		#endregion

	}

}
