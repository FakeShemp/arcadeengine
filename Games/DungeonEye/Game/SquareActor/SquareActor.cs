#region Licence
//
//This file is part of ArcEngine.
//Copyright (C)2008-2010 Adrien Hémery ( iliak@mimicprod.net )
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
using System.Drawing;
using System.Text;
using System.Xml;
using ArcEngine;
using ArcEngine.Graphic;
using DungeonEye.Script;
using DungeonEye.Script.Actions;


namespace DungeonEye
{
	/// <summary>
	/// Base class for square actors
	/// </summary>
	abstract public class SquareActor
	{

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="square">Square handle</param>
		public SquareActor(Square square)
		{
			Square = square;
			//Actions = new List<ActionBase>();
			//Count = new SwitchCount();
			Scripts = new List<ScriptBase>();
		}


		/// <summary>
		/// Draw the actor
		/// </summary>
		/// <param name="batch">Spritebatch to use</param>
		/// <param name="field">View field</param>
		/// <param name="position">Position in the view field</param>
		/// <param name="view">Looking direction of the team</param>
		public virtual void Draw(SpriteBatch batch, ViewField field, ViewFieldPosition position, CardinalPoint direction)
		{
		}



		/// <summary>
		/// Update the actor
		/// </summary>
		/// <param name="time">Elpased time</param>
		public virtual void Update(GameTime time)
		{
		}


		/// <summary>
		/// Loads action script definitions
		/// </summary>
		/// <param name="node">XmlNode handle</param>
		bool LoadActions(XmlNode xml)
		{
			if (xml == null)
				return false;

			ActionBase script = null;

			foreach (XmlNode node in xml)
			{
				switch (node.Name.ToLower())
				{
					case "toggletarget":
					{
						script = new ToggleTarget();
					}
					break;

					case "activatetarget":
					{
						script = new ActivateTarget();
					}
					break;

					case "deactivatetarget":
					{
						script = new DeactivateTarget();
					}
					break;
				}


				if (script != null)
				{
					//Actions.Add(script);
					script.Load(node);
				}
			}


			return true;
		}


		#region Script

		/// <summary>
		/// A hero interacted with a side of the square
		/// </summary>
		/// <param name="location">Location of the mouse</param>
		/// <param name="side">Wall side</param>
		/// <returns>True if the event is processed</returns>
		public virtual bool OnClick(Point location, CardinalPoint side)
		{
			return false;
		}


		/// <summary>
		/// Fired when the team enters the square
		/// </summary>
		/// <returns>True if event handled</returns>
		public virtual bool OnTeamEnter()
		{
			return false;
		}


		/// <summary>
		/// Fired when the team leaves the square
		/// </summary>
		/// <returns>True if event handled</returns>
		public virtual bool OnTeamLeave()
		{
			return false;
		}


		/// <summary>
		/// Fired when the team stands on a square
		/// </summary>
		/// <returns>True if event handled</returns>
		public virtual bool OnTeamStand()
		{
			return false;
		}


		/// <summary>
		/// Fired when a monster enters the square
		/// </summary>
		/// <param name="monster">Monster handle</param>
		/// <returns>True if event handled</returns>
		public virtual bool OnMonsterEnter(Monster monster)
		{
			return false;
		}


		/// <summary>
		/// Fired when a monster leaves the square
		/// </summary>
		/// <param name="monster">Monster handle</param>
		/// <returns>True if event handled</returns>
		public virtual bool OnMonsterLeave(Monster monster)
		{
			return false;
		}


		/// <summary>
		/// Fired when a monster stands on a square
		/// </summary>
		/// <param name="monster">Monster handle</param>
		/// <returns>True if event handled</returns>
		public virtual bool OnMonsterStand(Monster monster)
		{
			return false;
		}


		/// <summary>
		/// Fired when an item is added to the square
		/// </summary>
		/// <param name="monster">Item handle</param>
		/// <returns>True if event handled</returns>
		public virtual bool OnItemDropped(Item item)
		{
			return false;
		}


		/// <summary>
		/// Fired when an item is removed from the square
		/// </summary>
		/// <param name="monster">Item handle</param>
		/// <returns>True if event handled</returns>
		public virtual bool OnItemCollected(Item item)
		{
			return false;
		}


		#endregion


		#region Events

		/// <summary>
		/// Activates the actor
		/// </summary>
		public virtual void Activate()
		{
			IsActivated = true;

			//if (Count.Activate())
			//    Run();
		}


		/// <summary>
		/// Deactivates the actor
		/// </summary>
		public virtual void Deactivate()
		{
			IsActivated = false;

			//if (Count.Deactivate())
			//    Run();
		}



		/// <summary>
		/// Runs actions
		/// </summary>
		public void Run()
		{
			// Run each action
			foreach (ScriptBase script in Scripts)
			{
				script.Run();
			}

		}


		/// <summary>
		/// Toggles the actor
		/// </summary>
		public virtual void Toggle()
		{
			if (IsActivated)
				Deactivate();
			else
				Activate();
		}


		/// <summary>
		/// Exchanges the actor state
		/// </summary>
		public virtual void Exchange()
		{
		}



		/// <summary>
		/// Sets to a state
		/// </summary>
		public virtual void SetTo()
		{
		}


		/// <summary>
		/// Plays a sound
		/// </summary>
		public virtual void PlaySound()
		{
		}


		/// <summary>
		/// Stops a sound
		/// </summary>
		public virtual void StopSound()
		{
		}


		#endregion


		#region IO

		/// <summary>
		/// Loads the door's definition from a bank
		/// </summary>
		/// <param name="node">Xml handle</param>
		/// <returns></returns>
		public virtual bool Load(XmlNode node)
		{
			if (node == null)
				return false;


			switch (node.Name)
			{
				case "isactivated":
				{
					IsActivated = bool.Parse(node.InnerXml);
				}
				break;

				case "actions":
				{
					LoadActions(node);
				}
				break;

				//case "switch":
				//{
				//    Count.Load(node);
				//}
				//break;

				default:
				{
					Trace.WriteLine("[SquareActor] Load() : Unknown node \"" + node.Name + "\" found @ " + Square.Location.ToStringShort() + ".");
				}
				break;
			}

			return true;
		}



		/// <summary>
		/// Saves the door definition
		/// </summary>
		/// <param name="writer">XML writer handle</param>
		/// <returns></returns>
		public virtual bool Save(XmlWriter writer)
		{
			if (writer == null)
				return false;

			writer.WriteElementString("isactivated", IsActivated.ToString());

			if (Scripts.Count > 0)
			{
				writer.WriteStartElement("actions");
				foreach(ScriptBase script in Scripts)
					script.Save(writer);
				writer.WriteEndElement();
			}

	//		Count.Save("switch", writer);

			return true;
		}

		#endregion


		#region Properties

		/// <summary>
		/// Parent square
		/// </summary>
		public Square Square
		{
			get;
			private set;
		}


		/// <summary>
		/// Does the items can pass through
		/// </summary>
		public virtual bool CanPassThrough
		{
			get;
			protected set;
		}


		/// <summary>
		/// Does the square is blocking for monster or the team
		/// </summary>
		public virtual bool IsBlocking
		{
			get;
			protected set;
		}


		/// <summary>
		/// Does the square accept items
		/// </summary>
		public bool AcceptItems
		{
			get;
			protected set;
		}

/*
		/// <summary>
		/// Target
		/// </summary>
		public DungeonLocation Target
		{
			get;
			set;
		}
*/

		/// <summary>
		/// Does the actor is activated
		/// </summary>
		public bool IsActivated
		{
			get;
			protected set;
		}


/*
		/// <summary>
		/// Registered actions
		/// </summary>
		public List<ActionBase> Actions
		{
			get;
			private set;
		}
*/

		/// <summary>
		/// Registred scripts
		/// </summary>
		public List<ScriptBase> Scripts
		{
			get;
			private set;
		}

/*
		/// <summary>
		/// Switch count
		/// </summary>
		public SwitchCount Count
		{
			get;
			set;
		}
*/
		#endregion
	}
}
