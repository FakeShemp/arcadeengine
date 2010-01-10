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
using System.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Xml;
using ArcEngine;
using ArcEngine.Graphic;
using ArcEngine.Asset;
using OpenTK.Graphics.OpenGL;
using DungeonEye.Interfaces;

//
// List of monsters : http://members.tripod.com/~stanislavs/games/eob1mons.htm
// http://dmweb.free.fr/?q=node/1363
//
// http://wiki.themanaworld.org/index.php/User:Crush/Monster_Database
//
// http://wiki.themanaworld.org/index.php/Monster_Database
//
//
//
namespace DungeonEye
{

	/// <summary>
	/// Base interface of all monster in the game
	/// </summary>
	public class Monster : IAsset
	{

		/// <summary>
		/// Default constructor
		/// </summary>
		public Monster()
		{
			Location = new DungeonLocation();
			ItemsInPocket = new List<string>();
			Life = new Life();
			LastHit = new DateTime();
			Damage = new Dice();

			DrawOffsetDuration = TimeSpan.FromSeconds(1.0f + GameBase.Random.NextDouble());
		}


		/// <summary>
		/// Initializes the monster
		/// </summary>
		/// <returns></returns>
		public bool Init()
		{
			Tileset = ResourceManager.CreateSharedAsset<TileSet>(TileSetName, TileSetName);
			Tileset.Scale = new SizeF(2.0f, 2.0f);

			Font = ResourceManager.CreateSharedAsset<Font2d>("inventory", "inventory");

			if (!string.IsNullOrEmpty(ScriptName) && !string.IsNullOrEmpty(InterfaceName))
			{
				Script script = ResourceManager.CreateAsset<Script>(ScriptName);
				script.Compile();

				Interface = script.CreateInstance<IMonster>(InterfaceName);
			}

			return true;
		}



		#region Update & Draw

		/// <summary>
		/// Update the monster logic
		/// </summary>
		public virtual void Update(GameTime time)
		{
			if (Interface != null)
				Interface.OnUpdate(this);


			// Draw offset
			if (LastDrawOffset + DrawOffsetDuration < DateTime.Now)
			{
				DrawOffset = new Point(GameBase.Random.Next(-10, 10), GameBase.Random.Next(-10, 10));
				LastDrawOffset = DateTime.Now;
			}
		}




		/// <summary>
		/// Draw the monster
		/// </summary>
		/// <param name="direction">Team's facing direction</param>
		/// <param name="pos">Position of the monster in the field of view</param>
		public virtual void Draw(CardinalPoint direction, ViewFieldPosition pos)
		{

			TextureEnvMode mode = Display.TexEnv;

			

			// Monster was hit, redraw it
			if (LastHit + TimeSpan.FromSeconds(0.25) > DateTime.Now)
			{
				Display.BlendingFunction(BlendingFactorSrc.SrcAlpha , BlendingFactorDest.OneMinusSrcAlpha);
				Display.TexEnv = TextureEnvMode.Add;
			}


			switch (pos)
			{
				case ViewFieldPosition.B:
				Tileset.Scale = new SizeF(0.75f, 0.75f);
				Display.Color = Color.Gray;
				Tileset.Draw(GetTileID(direction), new Point(0 + DrawOffset.X / 4, 110 + DrawOffset.Y / 4));
				Tileset.Scale = new SizeF(2.0f, 2.0f);
				break;
				case ViewFieldPosition.C:
				Tileset.Scale = new SizeF(0.75f, 0.75f);
				Display.Color = Color.Gray;
				Tileset.Draw(GetTileID(direction), new Point(80 + DrawOffset.X / 4, 110 + DrawOffset.Y / 4));
				Tileset.Scale = new SizeF(2.0f, 2.0f);
				break;
				case ViewFieldPosition.D:
				Tileset.Scale = new SizeF(0.75f, 0.75f);
				Display.Color = Color.Gray;
				Tileset.Draw(GetTileID(direction), new Point(180 + DrawOffset.X / 4, 110 + DrawOffset.Y / 4));
				Tileset.Scale = new SizeF(2.0f, 2.0f);
				break;
				case ViewFieldPosition.E:
				Tileset.Scale = new SizeF(0.75f, 0.75f);
				Display.Color = Color.Gray;
				Tileset.Draw(GetTileID(direction), new Point(270 + DrawOffset.X / 4, 110 + DrawOffset.Y / 4));
				Tileset.Scale = new SizeF(2.0f, 2.0f);
				break;
				case ViewFieldPosition.F:
				Tileset.Scale = new SizeF(0.75f, 0.75f);
				Display.Color = Color.Gray;
				Tileset.Draw(GetTileID(direction), new Point(342 + DrawOffset.X / 4, 110 + DrawOffset.Y / 4));
				Tileset.Scale = new SizeF(2.0f, 2.0f);
				break;



				case ViewFieldPosition.I:
				Tileset.Scale = new SizeF(1.25f, 1.25f);
				Tileset.Draw(GetTileID(direction), new Point(50 + DrawOffset.X / 2, 136 + DrawOffset.Y / 2));
				Tileset.Scale = new SizeF(2.0f, 2.0f);
				break;
				case ViewFieldPosition.J:
				Tileset.Scale = new SizeF(1.25f, 1.25f);
				Tileset.Draw(GetTileID(direction), new Point(180 + DrawOffset.X / 2, 136 + DrawOffset.Y / 2));
				Tileset.Scale = new SizeF(2.0f, 2.0f);
				break;
				case ViewFieldPosition.K:
				Tileset.Scale = new SizeF(1.25f, 1.25f);
				Tileset.Draw(GetTileID(direction), new Point(300 + DrawOffset.X / 2, 136 + DrawOffset.Y / 2));
				Tileset.Scale = new SizeF(2.0f, 2.0f);
				break;


				case ViewFieldPosition.M:
				Tileset.Draw(GetTileID(direction), new Point(-20 + DrawOffset.X, 190));
				break;
				case ViewFieldPosition.N:
				Tileset.Draw(GetTileID(direction), new Point(180 + DrawOffset.X, 190));
				break;
				case ViewFieldPosition.O:
				Tileset.Draw(GetTileID(direction), new Point(370 + DrawOffset.X, 190));
				break;
			}



			Display.Color = Color.White;

			// finish special mode
			if (LastHit + TimeSpan.FromSeconds(0.25) > DateTime.Now)
			{
				Display.TexEnv = mode;
				Display.BlendingFunction(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
			}
		}

		#endregion



		#region Helpers

		/// <summary>
		/// Returns the tile id of the monster depending the view point
		/// </summary>
		/// <param name="from">View direction of the viewer</param>
		/// <returns>ID of the tile to display the monster</returns>
		public int GetTileID(CardinalPoint point)
		{			
			int[,] id = new int[4,4];

			// g	f						LOOKING	  FROM	 VIEW	
			id[0, 0] = 5;			//		N			N			N
			id[0, 1] = 0;			//		N			S			S
			id[0, 2] = 3;			//		N			W			W
			id[0, 3] = 1;			//		N			E			E

			id[1, 0] = 0;			//		S			N			S
			id[1, 1] = 5;			//		S			S			N
			id[1, 2] = 1;			//		S			W			E
			id[1, 3] = 3;			//		S			E			W

			id[2, 0] = 1;			//		W			N			E
			id[2, 1] = 3;			//		W			S			W
			id[2, 2] = 5;			//		W			W			N
			id[2, 3] = 0;			//		W			E			S

			id[3, 0] = 3;			//		E			N			W
			id[3, 1] = 1;			//		E			S			E
			id[3, 2] = 0;			//		E			W			S
			id[3, 3] = 5;			//		E			E			N

			return id[(int)Location.Direction, (int)point] + Tile;
		}

		#endregion



		#region I/O


		/// <summary>
		/// Loads monster definition
		/// </summary>
		/// <param name="xml">XmlNode handle</param>
		/// <returns></returns>
		public bool Load(XmlNode xml)
		{
			if (xml == null || xml.Name.ToLower() != "monster")
				return false;

			Name = xml.Attributes["name"].Value;

			foreach (XmlNode node in xml)
			{
				switch (node.Name.ToLower())
				{
					case "location":
					{
						Location.Load(node);
					}
					break;

					case "life":
					{
						Life.Load(node);
					}
					break;

					case "tiles":
					{
						TileSetName = node.Attributes["name"].Value;
						Tile = int.Parse(node.Attributes["id"].Value);
					}
					break;

					case "pocket":
					{
						ItemsInPocket.Add(node.Attributes["item"].Value);
					}
					break;

					case "script":
					{
						ScriptName = node.Attributes["name"].Value;
						InterfaceName = node.Attributes["interface"].Value;
					}
					break;

					case "damage":
					{
						Damage.Load(node);
					}
					break;
				}

			}

			return true;
		}


		/// <summary>
		/// Saves monster definition
		/// </summary>
		/// <param name="writer">XmlWriter handle</param>
		/// <returns></returns>
		public bool Save(XmlWriter writer)
		{
			if (writer == null)
				return false;

	
			writer.WriteStartElement("monster");
			writer.WriteAttributeString("name", Name);


			writer.WriteStartElement("script");
			writer.WriteAttributeString("name", ScriptName);
			writer.WriteAttributeString("interface", InterfaceName);
			writer.WriteEndElement();


			writer.WriteStartElement("location");
			Location.Save(writer);
			writer.WriteEndElement();

			Damage.Save("damage", writer);

			Life.Save(writer);

			writer.WriteStartElement("tiles");
			writer.WriteAttributeString("name", TileSetName);
			writer.WriteAttributeString("id", Tile.ToString());
			writer.WriteEndElement();

			foreach (string name in ItemsInPocket)
			{
				writer.WriteStartElement("pocket");
				writer.WriteAttributeString("item", name);
				writer.WriteEndElement();
			}

			writer.WriteEndElement();

			return true;
		}



		#endregion



		/// <summary>
		/// Attacks the monster
		/// </summary>
		/// <param name="amount">HP to remove to the monster</param>
		/// <returns>Amount of HP actually removed</returns>
		public virtual int Attack(short amount)
		{
			if (amount <= 0)
				return 0;


			Life.Actual -= amount;
			LastHit = DateTime.Now;

			return amount;
		}



		public override string ToString()
		{
			return string.Format("{0}", Name);
		}


		#region Properties


		/// <summary>
		/// Xml tag of the asset in bank
		/// </summary>
		public string XmlTag
		{
			get
			{
				return "monster";
			}
		}


		/// <summary>
		/// Is the monster dead
		/// </summary>
		public bool IsDead
		{
			get
			{
				return Life.Actual <= 0;
			}
		}



		/// <summary>
		/// Last time the monster was hit
		/// </summary>
		public DateTime LastHit
		{
			get;
			private set;
		}


		/// <summary>
		/// Location of the monster
		/// </summary>
		public DungeonLocation Location
		{
			get;
			set;
		}


		/// <summary>
		/// Name of the monster
		/// </summary>
		public string Name
		{
			get;
			set;
		}


		/// <summary>
		/// TileSet of the monster
		/// </summary>
		[Browsable(false)]
		public TileSet Tileset
		{
			get;
			protected set;
		}


/*
		/// <summary>
		/// Type of the monster
		/// </summary>
		public MonsterType Type
		{
			get;
			set;
		}

*/

		/// <summary>
		/// Damage dice
		/// </summary>
		public Dice Damage
		{
			get;
			private set;
		}


		/// <summary>
		/// Holded items droped if killed
		/// </summary>
		public List<string> ItemsInPocket
		{
			get;
			set;
		}

		/// <summary>
		/// The monster can absorb some items when they are thrown at him
		/// </summary>
		public float AbsorbItems;


		/// <summary>
		/// State of the monster
		/// </summary>
		public MonsterState State
		{
			get;
			set;
		}


		/// <summary>
		/// Use to alternate frames when the monster walk
		/// </summary>
		public bool SwapFrame;


		/// <summary>
		/// Offset when drawing the monster
		/// </summary>
		Point DrawOffset;


		/// <summary>
		/// Last time a draw offset occured
		/// </summary>
		DateTime LastDrawOffset;


		/// <summary>
		/// Duration of a draw offset.
		/// Smaller the duration is, more the feeling of a over excited monster is
		/// </summary>
		TimeSpan DrawOffsetDuration;



		/// <summary>
		/// Life level
		/// </summary>
		public Life Life
		{
			get;
			set;
		}


		/// <summary>
		/// Tile id for this monster
		/// </summary>
		[Category("TileSet")]
		public int Tile
		{
			get;
			set;
		}


		/// <summary>
		/// TileSet name to use for this monster
		/// </summary>
		[TypeConverter(typeof(TileSetEnumerator))]
		[Category("TileSet")]
		public string TileSetName
		{
			get;
			set;
		}


		/// <summary>
		/// 
		/// </summary>
		Font2d Font;

		/// <summary>
		/// Defines the size of the creature on the floor. 
		/// </summary>
		/// <remarks>This value is ignored for non material creatures and the door always closes normally without causing any damage to such creatures</remarks>
		public MonsterSize Size
		{
			get;
			set;
		}





		/// <summary>
		/// The creature will tend to stay in the back row while other creatures 
		/// will step up to the front row when the party is near and they want to attack
		/// </summary>
		public bool BackRowAttack
		{
			get;
			set;
		}




		/// <summary>
		/// If this bit is set to '1', the creature can pass over pits without falling. 
		/// </summary>
		public bool Levitation
		{
			get;
			set;
		}




		/// <summary>
		/// When sets, the creature can see the party even if it is under the effect of the 'Invisibility' spell. 
		/// </summary>
		public bool CanSeeInvisible
		{
			get;
			set;
		}




		/// <summary>
		/// Defines the movement speed of the monster.
		/// This is the minimum of time required between two movements.
		/// </summary>
		public TimeSpan MovementSpeed
		{
			get;
			set;
		}


		/// <summary>
		/// Defines the attack speed of the creature. 
		/// This is the minimum amount of time required between two attacks. 
		/// </summary>
		public TimeSpan AttackSpeed
		{
			get;
			set;
		}


		/// <summary>
		/// Maximum number of tiles between creature and party needed to see the party. 
		/// This applies only if the creature is facing the party.
		/// </summary>
		public byte SightRange
		{
			get;
			set;
		}


		/// <summary>
		/// Maximum number of tiles between creature and party needed to detect 
		/// and "turn" towards the party, perhaps to shoot a projectile. 
		/// This applies even if the creature is not facing the party.
		/// </summary>
		public byte DetectionRange
		{
			get;
			set;
		}




		/// <summary>
		/// Resistance to magical spells like Fireball.
		/// Values range from 0 to 15. Value 15 means the creature is immune.
		/// </summary>
		public byte FireResistance
		{
			get;
			set;
		}



		/// <summary>
		/// Resistance to magical spells involving poison.
		///  Values range from 0 to 15. Value 15 means the creature is immune.
		/// </summary>
		public byte PoisonResistance
		{
			get;
			set;
		}



		/// <summary>
		/// The monster is non material. These creatures ignore normal attacks but take damage from 'Disrupt' spell.
		/// Fire damage is also reduced by a half. All missiles except 'Dispell' pass through these creatures.
		/// These creatures can pass through all doors of any type. 
		/// </summary>
		public bool NonMaterial
		{
			get;
			set;
		}


		/// <summary>
		/// The amount of time while the attack graphic is displayed. 
		/// </summary>
		public TimeSpan AttackDisplayDuration;


		/// <summary>
		/// Name of the sound when the monster move
		/// </summary>
		public string MovementSoundName;

		/// <summary>
		/// Name of the sound when the monster attack
		/// </summary>
		public string AttackSoundName;


		/// <summary>
		/// Script name
		/// </summary>
		public string ScriptName
		{
			get;
			set;
		}

		/// <summary>
		/// Interface name for the script
		/// </summary>
		public string InterfaceName
		{
			get;
			set;
		}


		/// <summary>
		/// Script interface
		/// </summary>
		public IMonster Interface
		{
			get;
			private set;
		}

		#endregion
	}


	/// <summary>
	/// Differents phase of the life of a monster
	/// </summary>
	public enum MonsterState
	{
		/// <summary>
		/// No specific action, wandering in the maze
		/// </summary>
		Inactive,

		/// <summary>
		/// Walking
		/// </summary>
		Walk,

		/// <summary>
		/// Rising weapon to hit the team
		/// </summary>
		RaiseWeapon,

		/// <summary>
		/// Hit the team
		/// </summary>
		Hit,

		/// <summary>
		/// Monster hit by the team
		/// </summary>
		IsHit,


		/// <summary>
		/// The monster try to reach the team to attack them
		/// </summary>
		TrackTeam
	}


/*
	/// <summary>
	/// Type of monster
	/// </summary>
	public enum MonsterType
	{
		DisplacerBeast,
		Drider,
		DrowElf,
		Dwarf,
		Flind,
		GiantLeech,
		GiantSpider,
		HellHound,
		KenKu,
		Kobold,
		KuoToa,
		MantisWarrior,
		MindFlayer,
		RustMonster,
		SkeletalLord,
		Skeleton,
		StoneGolem,
		Xanathar,
		Xorn,
		Zombie
	}
*/

	/// <summary>
	/// Defines the size of the creature on the floor. 
	/// </summary>
	public enum MonsterSize
	{
		/// <summary>
		/// means there can be 4 creatures per tile
		/// </summary>
		Small,


		/// <summary>
		/// means there can be 2 creatures per tile
		/// </summary>
		Normal,

		/// <summary>
		/// means there can be only one creature per tile
		/// </summary>
		Big
	}


	/// <summary>
	/// define the height of the creature. It is used to check if missiles can fly over the creatures (for example Fireballs can fly over small creatures).
	/// This value is also used to define how to animate a door that is closed upon the creature: 
	/// </summary>
	/// <remarks>This value is ignored for non material creatures and the door always closes normally without causing any damage to such creatures</remarks>
	public enum MonsterHeight
	{
		/// <summary>
		/// the door is animated from half of its size to 3/4th of its size. This applies to small creatures. 
		/// </summary>
		Small,

		/// <summary>
		/// the door is animated between 1/4th of its size to half of its size. This applies to medium sized creatures. 
		/// </summary>
		Medium,

		/// <summary>
		/// the door is animated from the top to 1/4th of its size. This applies to tall creatures.
		/// </summary>
		Tall,


		/// <summary>
		/// the door is not animated and stays fully open. The creature still takes damage.
		/// </summary>
		Giant
	}



	/// <summary>
	/// This value defines what kind of attack the creature uses. 
	/// This value affects how the inflicted damage is computed.
	/// </summary>
	public enum DamageType
	{
		/// <summary>
		/// The creature does not attack champions.
		/// </summary>
		None, 


		/// <summary>
		///  the attacked champion's 'Anti-Fire' characteristic is used to compute damage. 
		/// </summary>
		Fire,


		/// <summary>
		/// The 'Armor Strength' value of the attacked champion's armor is used to compute damage.
		/// </summary>
		Critical, 


		/// <summary>
		/// The 'Armor Strength' value of the attacked champion's armor is used to compute damage. 
		/// </summary>
		Normal, 

		/// <summary>
		/// The 'Sharp resistance' value of the attacked champion's armor is used to compute damage. 
		/// </summary>
		Sharp, 


		/// <summary>
		///  the attacked champion's 'Anti-Magic' characteristic is used to compute damage. 
		/// </summary>
		Magic,


		/// <summary>
		///  the attacked champion's 'Wisdom' characteristic is used to compute damage. 
		/// </summary>
		Psychic,

	}
}

