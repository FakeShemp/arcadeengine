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
using ArcEngine;
using System.Text;
using ArcEngine.Graphic;
using System.Drawing;
using System.Xml;

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


		#region Script

		/// <summary>
		/// A hero interacted with a side of the square
		/// </summary>
		/// <param name="team">Team</param>
		/// <param name="location">Location of the mouse</param>
		/// <param name="side">Wall side</param>
		/// <returns>True if the event is processed</returns>
		public virtual bool OnClick(Team team, Point location, CardinalPoint side)
		{
			return false;
		}


		/// <summary>
		/// Fired when the team enters the square
		/// </summary>
		/// <param name="team">Team handle</param>
		/// <returns>True if event handled</returns>
		public virtual bool OnTeamEnter(Team team)
		{
			return false;
		}


		/// <summary>
		/// Fired when the team leaves the square
		/// </summary>
		/// <param name="team">Team handle</param>
		/// <returns>True if event handled</returns>
		public virtual bool OnTeamLeave(Team team)
		{
			return false;
		}


		/// <summary>
		/// Fired when the team stands on a square
		/// </summary>
		/// <param name="team">Team handle</param>
		/// <returns>True if event handled</returns>
		public virtual bool OnTeamStand(Team team)
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
		/// ACtivates the actor
		/// </summary>
		public virtual void Activate()
		{
		}


		/// <summary>
		/// Deactivates the actor
		/// </summary>
		public virtual void Deactivate()
		{
		}


		/// <summary>
		/// Toggles the actor
		/// </summary>
		public virtual void Toggle()
		{
		}


		/// <summary>
		/// Exchanges the actor state
		/// </summary>
		public virtual void Exchange()
		{
		}



		/// <summary>
		/// Sets to as state
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
		/// <param name="xml">Xml handle</param>
		/// <returns></returns>
		public virtual bool Load(XmlNode xml)
		{
			return false;
			
		}



		/// <summary>
		/// Saves the door definition
		/// </summary>
		/// <param name="writer">XML writer handle</param>
		/// <returns></returns>
		public virtual bool Save(XmlWriter writer)
		{
			return false;
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

		#endregion
	}
}
