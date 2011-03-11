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
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using ArcEngine.Audio;
using ArcEngine.Editor;
using ArcEngine.Forms;
using ArcEngine.Graphic;
using ArcEngine.Input;
using ArcEngine.PInvoke;
using ArcEngine.Storage;

[assembly: CLSCompliant(true)]
namespace ArcEngine
{
	/// <summary>
	/// Provides basic graphics device initialization, game logic, and rendering code.
	/// </summary>
	public class GameBase : IDisposable
	{

		#region ctor

		/// <summary>
		/// Constructor
		/// </summary>
		public GameBase()
		{
			Trace.WriteDebugLine("[GameBase] Constructor()");
			Trace.TraceInventory();

			Clock = new GameClock();
			GameTime = new GameTime();
			MaximumElapsedTime = TimeSpan.FromMilliseconds(500.0);
			TargetElapsedTime = TimeSpan.FromTicks(166667L);
			IsFixedTimeStep = true;

			Random = new Random((int) DateTime.Now.Ticks);
			Components = new GameComponentCollection();
		}



		#endregion


		#region GameWindow creation

		/// <summary>
		/// Create the GameWindow, with the highest compatible OpenGL version
		/// </summary>
		/// <param name="size">Size of the window</param>
		public void CreateGameWindow(Size size)
		{
			GameWindowParams p = new GameWindowParams();
			p.Size = size;
			CreateGameWindow(p);
		}


		/// <summary>
		/// Create the GameWindow, and use a specific OpenGL version
		/// </summary>
		/// <param name="param">GameWindow creation parameters</param>
		public void CreateGameWindow(GameWindowParams param)
		{
			Trace.WriteDebugLine("[GameBase] CreateGameWindow()");

			// Close the previous game window first
			if (Window != null)
			{
				Trace.WriteDebugLine("[GameBase] CreateGameWindow() : Closing previous window");
				CloseGameWindow();
			}

			Window = new GameWindow(param);


			// Events
			Window.Activated += new EventHandler(Window_Activated);
			Window.Deactivate += new EventHandler(Window_Deactivate);
			Window.Resize += new EventHandler(Window_Resize);
			Window.FormClosing += new FormClosingEventHandler(Window_FormClosing);
			Mouse.Init(Window);
			Gamepad.Init(Window);

			Window.Show();
			Window.Activate();

		}


		/// <summary>
		/// Closes the game window
		/// </summary>
		public void CloseGameWindow()
		{
			if (Window == null)
				return;

			Trace.WriteDebugLine("[GameBase] Closing GameWindow");

			Window.Activated -= Window_Activated;
			Window.Deactivate -= Window_Deactivate;
			Window.Resize -= Window_Resize;

			Window.Hide();
			Window.Close();
			Window.Dispose();
			Window = null;
		}

		#endregion


		#region Event handlers

		/// <summary>
		/// GameWindow resize event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void Window_Resize(object sender, EventArgs e)
		{
			if (OnResize != null)
			{
				OnResize(this);
			}
		}


		/// <summary>
		/// GameWindow loses focus event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void Window_Deactivate(object sender, EventArgs e)
		{
			if (OnDeactivated != null)
			{
				OnDeactivated(this);
			}
		}


		/// <summary>
		/// Gamewindow gains focus event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void Window_Activated(object sender, EventArgs e)
		{
			if (OnActivated != null)
			{
				OnActivated(this);
			}
		}


		/// <summary>
		/// GameWindow is closing
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void Window_FormClosing(object sender, FormClosingEventArgs e)
		{
			Trace.WriteDebugLine("[Game] Form_Closing()");
			UnloadContent();

		}

		#endregion


		#region Virtuals methods

		/// <summary>
		/// Called when the game has determined that game logic needs to be processed.
		/// </summary>
		/// <param name="gameTime">The time passed since the last update.</param>
		public virtual void Update(GameTime gameTime)
		{
			// Check if the Escape key is pressed
			if (Keyboard.IsKeyPress(Keys.Escape))
				Exit();

			if (Keyboard.IsKeyPress(Keys.Insert))
				RunEditor(null);
		}


		/// <summary>
		/// Called when the game determines it is time to draw a frame.
		/// </summary>
		public virtual void Draw()
		{
			Display.RenderState.ClearColor = Color.CornflowerBlue;
			Display.ClearBuffers();
		}


		/// <summary>
		/// Called when graphics resources need to be loaded.
		/// </summary>
		public virtual void LoadContent()
		{
		}


		/// <summary>
		/// Called when graphics resources need to be unloaded.
		/// </summary>
		public virtual void UnloadContent()
		{
		}

		#endregion


		#region Game logic
		private bool ForceElapsedTimeToZero;
		private TimeSpan LastFrameElapsedGameTime;
		private TimeSpan LastFrameElapsedRealTime;
		private TimeSpan AccumulatedElapsedGameTime;
		private int UpdatesSinceRunningSlowly1;
		private int UpdatesSinceRunningSlowly2;
		private bool DrawRunningSlowly;
		private TimeSpan TotalGameTime;
		private bool SuppressDraw;


		/// <summary>
		/// Performs one complete frame for the game.
		/// </summary>
		void Tick()
		{
			if (IsExiting)
			{
				Audio.AudioManager.Release();
				Gamepad.Dispose();
				Mouse.Dispose();

				Application.Exit();
			}

			// if we are exiting or in editor mode, do nothing
			if (EditorMode)
			{
				Thread.Sleep(100);
				return;
			}

			// if we are inactive, sleep for a bit
			if (!IsActive)
				Thread.Sleep(20);


			bool flag = true;

			// Update the clock
			Clock.Step();

			// Update the game time
			GameTime.TotalRealTime = Clock.CurrentTime;
			GameTime.ElapsedRealTime = Clock.ElapsedTime;
			LastFrameElapsedRealTime += Clock.ElapsedTime;
			TimeSpan elapsedAdjustedTime = Clock.ElapsedAdjustedTime;
			if (elapsedAdjustedTime < TimeSpan.Zero)
				elapsedAdjustedTime = TimeSpan.Zero;

			// check if we should force the elapsed time
			if (ForceElapsedTimeToZero)
			{
				GameTime.ElapsedRealTime = LastFrameElapsedRealTime = elapsedAdjustedTime = TimeSpan.Zero;
				ForceElapsedTimeToZero = false;
			}

			// Cap the adjusted time
			if (elapsedAdjustedTime > MaximumElapsedTime)
				elapsedAdjustedTime = MaximumElapsedTime;


			#region Fixed time step
			// check if we are using a fixed or variable time step
			if (IsFixedTimeStep)
			{
				if (Math.Abs((long) (elapsedAdjustedTime.Ticks - TargetElapsedTime.Ticks)) < (TargetElapsedTime.Ticks >> 6))
					elapsedAdjustedTime = TargetElapsedTime;

				// update the timing state
				AccumulatedElapsedGameTime += elapsedAdjustedTime;
				long num = AccumulatedElapsedGameTime.Ticks / TargetElapsedTime.Ticks;
				AccumulatedElapsedGameTime = TimeSpan.FromTicks(AccumulatedElapsedGameTime.Ticks % TargetElapsedTime.Ticks);
				LastFrameElapsedGameTime = TimeSpan.Zero;
				if (num == 0)
				{
					return;
				}
				TimeSpan targetElapsedTime = TargetElapsedTime;

				// check if the ratio is too large (ie. running slowly)
				if (num > 1)
				{
					// Running slowy
					UpdatesSinceRunningSlowly2 = UpdatesSinceRunningSlowly1;
					UpdatesSinceRunningSlowly1 = 0;
				}
				else
				{
					// Not running slowly
					if (UpdatesSinceRunningSlowly1 < int.MaxValue)
						UpdatesSinceRunningSlowly1++;

					if (UpdatesSinceRunningSlowly2 < int.MaxValue)
						UpdatesSinceRunningSlowly2++;

				}

				// check whether we are officially running slowly
				DrawRunningSlowly = UpdatesSinceRunningSlowly2 < 20;


				// update until it's time to draw the next frame
				while ((num > 0) && !IsExiting)
				{
					// Decrement the ratio
					num -= 1;


					// fill out the rest of the game time
					GameTime.ElapsedGameTime = TargetElapsedTime;
					GameTime.TotalGameTime = TotalGameTime;
					GameTime.IsRunningSlowly = DrawRunningSlowly;

					if (Window.HasFocus)
					{
						Keyboard.Update();
						Mouse.Update();
						Gamepad.Update();
					}
					AudioStream.Update();

					// Perform an update
					Update(GameTime);
					for (int i = 0 ; i < Components.Count ; i++)
					{
						GameComponent component = Components[i];
						if (component.Enabled)
							component.Update(GameTime);
					}

					flag &= SuppressDraw;
					SuppressDraw = false;

					// update the total clocks
					LastFrameElapsedGameTime += TargetElapsedTime;
					TotalGameTime += TargetElapsedTime;
				}
			}
			#endregion

			#region Variable time step
			else
			{
				TimeSpan span3 = elapsedAdjustedTime;

				// we can't be running slowly here
				DrawRunningSlowly = false;
				UpdatesSinceRunningSlowly1 = int.MaxValue;
				UpdatesSinceRunningSlowly2 = int.MaxValue;

				// make sure we shouldn't be exiting
				if (!IsExiting)
				{
					try
					{
						// fill out the rest of the game time
						GameTime.ElapsedGameTime = LastFrameElapsedGameTime = span3;
						GameTime.TotalGameTime = TotalGameTime;
						GameTime.IsRunningSlowly = false;

						if (Window.HasFocus)
						{
							Keyboard.Update();
							Mouse.Update();
							Gamepad.Update();
						}
						AudioStream.Update();

						// Perform an update
						Update(GameTime);
						for (int i = 0 ; i < Components.Count ; i++)
						{
							GameComponent component = Components[i];
							if (component.Enabled)
								component.Update(GameTime);
						}

						flag &= SuppressDraw;
						SuppressDraw = false;
					}
					finally
					{
						// Update the total clocks
						TotalGameTime += span3;
					}
				}
			}
			#endregion


			if (!flag)
			{
				if (IsExiting)
					return;

				GameTime.TotalRealTime = Clock.CurrentTime;
				GameTime.ElapsedRealTime = LastFrameElapsedRealTime;
				GameTime.TotalGameTime = TotalGameTime;
				GameTime.ElapsedGameTime = LastFrameElapsedGameTime;
				GameTime.IsRunningSlowly = DrawRunningSlowly;

				Display.Statistics.Reset();

				Draw();
				for (int i = 0 ; i < Components.Count ; i++)
				{
					GameComponent component = Components[i];
					if (component.IsVisible)
						component.Draw();
				}

				Window.SwapBuffers();
			}


		}


		/// <summary>
		/// Runs the game
		/// </summary>
		public void Run()
		{

			try
			{
				Trace.WriteLine("Running the game");


				LoadContent();

				IsRunning = true;

				try
				{
					GameTime.ElapsedGameTime = TimeSpan.Zero;
					GameTime.ElapsedRealTime = TimeSpan.Zero;
					GameTime.TotalGameTime = TotalGameTime;
					GameTime.TotalRealTime = Clock.CurrentTime;
					GameTime.IsRunningSlowly = false;

					Update(GameTime);

					Application.Idle += Application_Idle;
					Application.Run(Window);
				}
				finally
				{
					Application.Idle -= Application_Idle;
					IsRunning = false;
				}


				//Audio.AudioManager.Release();
				//Gamepad.Dispose();
				//CloseGameWindow();

			}
			catch (Exception e)
			{
				Trace.WriteLine("");
				Trace.WriteLine("!!!FATAL ERROR !!!");
				Trace.WriteLine("Message : " + e.Message);
				Trace.WriteLine("Inner exception : " + e.InnerException);
				Trace.WriteLine("Stack trace : " + e.StackTrace);
				Trace.WriteLine("");


				new ExceptionForm(e).ShowDialog();
				Exit();

			}


			ResourceManager.Dispose();
			Trace.Close();
		}

		#endregion


		#region Events


		/// <summary>
		/// Gain focus event handler
		/// </summary>
		/// <param name="game">Game</param>
		public delegate void OnActivatedEventHandler(GameBase game);


		/// <summary>
		/// Raised when the game gains focus.
		/// </summary>
		public event OnActivatedEventHandler OnActivated;


		/// <summary>
		/// Lost focus event handler
		/// </summary>
		/// <param name="game">Game</param>
		public delegate void OnDeactivatedEventHandler(GameBase game);


		/// <summary>
		/// Raised when the game loses focus.
		/// </summary>
		public event OnDeactivatedEventHandler OnDeactivated;


		/// <summary>
		/// Resize event handler
		/// </summary>
		/// <param name="game">Game</param>
		public delegate void OnResizeEventHandler(GameBase game);


		/// <summary>
		/// GameWindow resize event
		/// </summary>
		public event OnResizeEventHandler OnResize;



		#region Editor


		/// <summary>
		/// 
		/// </summary>
		public delegate void EditorEventHandler(object sender, EditorEventArgs e);


		/// <summary>
		/// Event raised when the enter the editor.
		/// </summary>
		public event EditorEventHandler EditorEnter;


		/// <summary>
		/// Method for raising the EditorEnter event.
		/// </summary>
		public virtual void OnEditorEnter(EditorMainForm form)
		{
			if (EditorEnter != null)
			{
				EditorEventArgs evt = new EditorEventArgs(form);
				EditorEnter(this, evt);
			}
		}

		/// <summary>
		/// Event raised when leaving the editor.
		/// </summary>
		public event EventHandler EditorLeave;


		/// <summary>
		/// Method for raising the EditorLeave event.
		/// </summary>
		public virtual void OnEditorLeave()
		{
			if (EditorLeave != null)
				EditorLeave(this, null);
		}


		#endregion



		#endregion



		/// <summary>
		/// Message pump
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void Application_Idle(object sender, EventArgs e)
		{
			User32.NativeMessage message;
			while (!User32.PeekMessage(out message, IntPtr.Zero, 0, 0, 0))
			{
				Tick();
			}
		}




		/// <summary>
		/// Exits the game.
		/// </summary>
		static public void Exit()
		{
			IsExiting = true;

			Trace.WriteDebugLine("[GameBase] Exit requested !");
		}



		#region Time management

		/// <summary>
		/// Is the game running at a fixed time setp
		/// </summary>
		public bool IsFixedTimeStep
		{
			get;
			set;
		}


		/// <summary>
		/// Desired time step in ms
		/// </summary>
		public TimeSpan TargetElapsedTime
		{
			get;
			set;
		}



		/// <summary>
		/// Maximum elapsed time allowed
		/// </summary>
		public TimeSpan MaximumElapsedTime
		{
			get;
			internal set;
		}


		/// <summary>
		/// Game time
		/// </summary>
		public GameTime GameTime
		{
			get;
			private set;
		}



		/// <summary>
		/// Game clock
		/// </summary>
		GameClock Clock;



		#endregion


		#region Editor

		/// <summary>
		/// Launch the editor
		/// </summary>
		/// <param name="storage">Storage to use</param>
		public void RunEditor(StorageBase storage)
		{

			bool mousestate = Mouse.Visible;
			Mouse.Visible = true;

			if (Window != null)
				Window.Hide();

			Editor = new EditorMainForm(storage);
			OnEditorEnter(Editor);
			Editor.ShowDialog();
			Editor = null;
			OnEditorLeave();

			if (Window != null)
			{
				Window.MakeCurrent();
				Window.Show();
				Window.BringToFront();
			}
			Mouse.Visible = mousestate;

		}

		#endregion


		#region Properties

		/// <summary>
		/// Gets the collection of <see cref="GameComponent"/> owned by the game. 
		/// </summary>
		public GameComponentCollection Components
		{
			get;
			private set;
		}


		/// <summary>
		/// Gets the game window.
		/// </summary>
		/// <value>The game window.</value>
		public GameWindow Window
		{
			get;
			private set;
		}



		/// <summary>
		/// Gets whether this Game is exiting.
		/// </summary>
		/// <value>true if exiting; otherwise, false.</value>
		static public bool IsExiting
		{
			get;
			private set;
		}


		/// <summary>
		/// Gets or sets a value indicating whether this instance is running.
		/// </summary>
		/// <value>true if this instance is running; otherwise, false.</value>
		public bool IsRunning
		{
			get;
			private set;
		}


		/// <summary>
		/// Gets or sets a value indicating whether this Game is active.
		/// </summary>
		/// <value>true if active, otherwise, false.</value>
		public bool IsActive
		{
			get;
			internal set;
		}


		/// <summary>
		/// True if in editor mode
		/// </summary>
		public bool EditorMode
		{
			get
			{
				return Editor != null;
			}
		}


		/// <summary>
		/// Editor form handle
		/// </summary>
		public EditorMainForm Editor
		{
			get;
			private set;
		}

		/// <summary>
		/// Random number generator
		/// </summary>
		static public Random Random
		{
			get;
			private set;
		}


		#endregion


		#region Dispose



		/// <summary>
		/// Disposing resources
		/// </summary>
		// http://www.dotnet2themax.com/blogs/fbalena/SearchView.aspx?q=Dispose/Finalize%20
		// http://www.devx.com/dotnet/Article/33167/0/page/3
		public void Dispose()
		{
			Trace.WriteDebugLine("[GameBase] Dispose");
		}


		#endregion

	}
}
