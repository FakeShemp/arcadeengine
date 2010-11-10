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
using System.Xml;
using ArcEngine;
using ArcEngine.Forms;
using ArcEngine.Interface;

namespace DungeonEye
{
	/// <summary>
	/// Main provider
	/// </summary>
	public class Providers : Provider
	{

		/// <summary>
		/// Constructor
		/// </summary>
		public Providers()
		{
			Dungeons = new Dictionary<string, XmlNode>(StringComparer.OrdinalIgnoreCase);
			SharedDungeons = new Dictionary<string, Dungeon>(StringComparer.OrdinalIgnoreCase);
			Monsters = new Dictionary<string, XmlNode>(StringComparer.OrdinalIgnoreCase);
			WallDecorations = new Dictionary<string, XmlNode>(StringComparer.OrdinalIgnoreCase);
			Items = new Dictionary<string, XmlNode>(StringComparer.OrdinalIgnoreCase);
			SharedItems = new Dictionary<string, Item>(StringComparer.OrdinalIgnoreCase);
			Spells = new Dictionary<string, XmlNode>(StringComparer.OrdinalIgnoreCase);
			Heroes = new Dictionary<string, XmlNode>(StringComparer.OrdinalIgnoreCase);


			Name = "Dungeon Eye";
			Tags = new string[] { "dungeon", "item", "monster", "walldecoration", "spell", "hero" };
			Assets = new Type[] { typeof(Dungeon), typeof(Item), typeof(Monster), typeof(WallDecoration), typeof(Spell), typeof(Hero) };
			Version = new Version(0, 1);
		}


		#region Init & Close


		/// <summary>
		/// Initialization
		/// </summary>
		/// <returns></returns>
		public override bool Init()
		{
			return true;
		}



		/// <summary>
		/// Dispose all opened resources
		/// </summary>
		public override void Dispose()
		{

		}

		#endregion


		#region IO routines


		/// <summary>
		/// Saves all assets
		/// </summary>
		/// <param name="type"></param>
		/// <param name="xml"></param>
		/// <returns></returns>
		public override bool Save<T>(XmlWriter writer)
		{


			if (typeof(T) == typeof(Dungeon))
			{
				foreach (XmlNode node in Dungeons.Values)
					node.WriteTo(writer);
			}
			else if (typeof(T) == typeof(Item))
			{
				foreach (XmlNode node in Items.Values)
					node.WriteTo(writer);
			}
			else if (typeof(T) == typeof(Monster))
			{
				foreach (XmlNode node in Monsters.Values)
					node.WriteTo(writer);
			}
			else if (typeof(T) == typeof(Spell))
			{
				foreach (XmlNode node in Spells.Values)
					node.WriteTo(writer);
			}
			else if (typeof(T) == typeof(Hero))
			{
				foreach (XmlNode node in Heroes.Values)
					node.WriteTo(writer);
			}


			return true;
		}




		/// <summary>
		/// Loads a script
		/// </summary>
		/// <param name="xml"></param>
		/// <returns></returns>
		public override bool Load(XmlNode xml)
		{
			if (xml == null)
				return false;

			switch (xml.Name.ToLower())
			{
				case "dungeon":
				{
					Dungeons[xml.Attributes["name"].Value] = xml;
				}
				break;

				case "monster":
				{
					Monsters[xml.Attributes["name"].Value] = xml;
				}
				break;

				case "item":
				{
					Items[xml.Attributes["name"].Value] = xml;
				}
				break;

				case "hero":
				{
					Heroes[xml.Attributes["name"].Value] = xml;
				}
				break;

				case "walldecoration":
				{
					WallDecorations[xml.Attributes["name"].Value] = xml;
				}
				break;

				case "spell":
				{
					Spells[xml.Attributes["name"].Value] = xml;
				}
				break;
			}

			return true;
		}



		#endregion


		#region Editor


		/// <summary>
		/// Edits an asset
		/// </summary>
		/// <param name="type">Asset's type</param>
		/// <param name="name">Name of the asset</param>
		/// <returns>Handle to the edit form</returns>
		public override AssetEditorBase EditAsset<T>(string name)
		{
			AssetEditorBase form = null;
			XmlNode node = null;

			if (typeof(T) == typeof(Dungeon))
			{
				if (Dungeons.ContainsKey(name))
					node = Dungeons[name];

				form = new Forms.DungeonForm(node);
			}
			else if (typeof(T) == typeof(Monster))
			{
				if (Monsters.ContainsKey(name))
					node = Monsters[name];

				form = new Forms.MonsterForm(node);
			}
			else if (typeof(T) == typeof(Item))
			{
				if (Items.ContainsKey(name))
					node = Items[name];

				form = new Forms.ItemForm(node);
			}
			else if (typeof(T) == typeof(Spell))
			{
				if (Spells.ContainsKey(name))
					node = Spells[name];

				form = new Forms.SpellForm(node);
			}

			else if (typeof(T) == typeof(Hero))
			{
				if (Heroes.ContainsKey(name))
					node = Heroes[name];

				form = new Forms.HeroForm(node);
			}


			form.TabText = name;
			return form;
		}


		#endregion


		#region Assets

		/// <summary>
		/// Adds an asset definition to the provider
		/// </summary>
		/// <typeparam name="T">Type of the asset</typeparam>
		/// <param name="name">Name of the asset</param>
		/// <param name="node">Xml node definition</param>
		public override void Add<T>(string name, XmlNode node)
		{
			CheckValue<T>(name);

			if (typeof(Dungeon) == typeof(T))
				Dungeons[name] = node;
			else if (typeof(Monster) == typeof(T))
				Monsters[name] = node;
			else if (typeof(Item) == typeof(T))
				Items[name] = node;
			else if (typeof(Spell) == typeof(T))
				Spells[name] = node;
			else if (typeof(Hero) == typeof(T))
				Heroes[name] = node;

		}


		/// <summary>
		/// Returns an array of all available assets
		/// </summary>
		/// <typeparam name="T">Type of the asset</typeparam>
		/// <returns>Asset's name array</returns>
		public override List<string> GetAssets<T>()
		{
			List<string> list = new List<string>();

			if (typeof(T) == typeof(Dungeon))
				foreach (string key in Dungeons.Keys)
					list.Add(key);

			else if (typeof(T) == typeof(Monster))
				foreach (string key in Monsters.Keys)
					list.Add(key);

			else if (typeof(T) == typeof(Item))
				foreach (string key in Items.Keys)
					list.Add(key);

			else if (typeof(T) == typeof(Spell))
				foreach (string key in Spells.Keys)
					list.Add(key);

			else if (typeof(T) == typeof(Hero))
				foreach (string key in Heroes.Keys)
					list.Add(key);

			list.Sort();
			return list;
		}


		/// <summary>
		/// Creates an asset
		/// </summary>
		/// <typeparam name="T">Type of the asset</typeparam>
		/// <param name="name">Name of the asset</param>
		/// <returns></returns>
		public override T Create<T>(string name)
		{

			CheckValue<T>(name);


			if (typeof(T) == typeof(Dungeon) && Dungeons.ContainsKey(name))
			{
				Dungeon dungeon = new Dungeon();
				dungeon.Load(Dungeons[name]);
				return (T)(object)dungeon;
			}

			if (typeof(T) == typeof(Monster) && Monsters.ContainsKey(name))
			{
				Monster monster = new Monster(null);
				monster.Load(Monsters[name]);
				return (T)(object)monster;
			}


			if (typeof(T) == typeof(Item) && Items.ContainsKey(name))
			{
				Item item = new Item();
				item.Load(Items[name]);
				return (T) (object) item;
			}

			if (typeof(T) == typeof(Spell) && Spells.ContainsKey(name))
			{
				Spell spell = new Spell();
				spell.Load(Spells[name]);
				return (T) (object) spell;
			}

			if (typeof(T) == typeof(Hero) && Heroes.ContainsKey(name))
			{
				Hero hero = new Hero(null);
				hero.Load(Heroes[name]);
				return (T) (object) hero;
			}

			return default(T);

		}



		/// <summary>
		/// Returns an asset definition
		/// </summary>
		/// <typeparam name="T">Type of the asset</typeparam>
		/// <param name="name">Asset's name</param>
		/// <returns></returns>
		public override XmlNode Get<T>(string name)
		{
			CheckValue<T>(name);

			if (typeof(Dungeon) == typeof(T) && Dungeons.ContainsKey(name))
				return Dungeons[name];

			else if (typeof(Monster) == typeof(T) && Monsters.ContainsKey(name))
				return Monsters[name];

			else if (typeof(Item) == typeof(T) && Items.ContainsKey(name))
				return Items[name];

			else if (typeof(Spell) == typeof(T) && Spells.ContainsKey(name))
				return Items[name];

			else if (typeof(Hero) == typeof(T) && Heroes.ContainsKey(name))
				return Heroes[name];

			return null;
		}



		/// <summary>
		/// Flush unused assets
		/// </summary>
		/// <typeparam name="T"></typeparam>
		public override void Remove<T>()
		{
			if (typeof(Dungeon) == typeof(T))
				Dungeons.Clear();

			else if (typeof(Monster) == typeof(T))
				Monsters.Clear();

			else if (typeof(Item) == typeof(T))
				Items.Clear();

			else if (typeof(Spell) == typeof(T))
				Spells.Clear();

			else if (typeof(Hero) == typeof(T))
				Heroes.Clear();
		}


		/// <summary>
		/// Removes a specific asset
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="name"></param>
		public override void Remove<T>(string name)
		{
			if (typeof(Dungeon) == typeof(T) && Dungeons.ContainsKey(name))
				Dungeons.Remove(name);

			else if (typeof(Monster) == typeof(T) && Monsters.ContainsKey(name))
				Monsters.Remove(name);

			else if (typeof(Item) == typeof(T) && Items.ContainsKey(name))
				Items.Remove(name);

			else if (typeof(Spell) == typeof(T) && Spells.ContainsKey(name))
				Spells.Remove(name);

			else if (typeof(Hero) == typeof(T) && Heroes.ContainsKey(name))
				Heroes.Remove(name);
		}



		/// <summary>
		/// Removes all assets
		/// </summary>
		public override void Clear()
		{
			Dungeons.Clear();
			Monsters.Clear();
			Items.Clear();
			Spells.Clear();
			Heroes.Clear();
		}


		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public override int Count<T>()
		{
			if (typeof(T) == typeof(Dungeon))
				return Dungeons.Count;

			if (typeof(T) == typeof(Monster))
				return Monsters.Count;

			if (typeof(T) == typeof(Item))
				return Items.Count;

			if (typeof(T) == typeof(Spell))
				return Spells.Count;

			if (typeof(T) == typeof(Hero))
				return Heroes.Count;

			return 0;
		}


		#endregion


		#region Shared assets

		/// <summary>
		/// Adds an asset as Shared
		/// </summary>
		/// <typeparam name="T">Asset type</typeparam>
		/// <param name="name">Name of the asset to register</param>
		/// <param name="asset">Asset's handle</param>
		public override void AddShared<T>(string name, IAsset asset)
		{
			if (string.IsNullOrEmpty(name))
				return;

			if (typeof(T) == typeof(Dungeon))
				SharedDungeons[name] = asset as Dungeon;

			if (typeof(T) == typeof(Item))
				SharedItems[name] = asset as Item;
		}

		/// <summary>
		/// Gets a shared resource
		/// </summary>
		/// <typeparam name="T">Asset type</typeparam>
		/// <param name="name">Name of the shared asset</param>
		/// <returns>Asset handle or null</returns>
		public override T GetShared<T>(string name)
		{
			if (typeof(T) == typeof(Dungeon))
			{
				if (SharedDungeons.ContainsKey(name))
					return (T)(object)SharedDungeons[name];
			}

			if (typeof(T) == typeof(Item))
			{
				if (SharedItems.ContainsKey(name))
					return (T)(object)SharedItems[name];
			}

			return default(T);
		}



		/// <summary>
		/// Removes a shared asset
		/// </summary>
		/// <typeparam name="T">Type of the asset</typeparam>
		/// <param name="name">Name of the asset</param>
		public override void RemoveShared<T>(string name)
		{
			if (typeof(T) == typeof(Dungeon))
			{
				Dungeon dungeon = GetShared<Dungeon>(name);
				if (dungeon != null)
					dungeon.Dispose();

				SharedDungeons.Remove(Name);
			}

			else if (typeof(T) == typeof(Item))
				SharedItems.Remove(name); ;
		}




		/// <summary>
		/// Removes a specific type of sharedassets
		/// </summary>
		/// <typeparam name="T">Type of the asset to remove</typeparam>
		public override void RemoveShared<T>()
		{
			if (typeof(T) == typeof(Dungeon))
			{
				foreach (Dungeon dungeon in SharedDungeons.Values)
					dungeon.Dispose();


				SharedDungeons.Clear();
			}

			else if (typeof(T) == typeof(Item))
			{
				SharedItems.Clear();
			}
		}



		/// <summary>
		/// Erases all shared assets
		/// </summary>
		public override void ClearShared()
		{
			SharedDungeons.Clear();
			SharedItems.Clear();
		}



		#endregion


		#region Properties


		/// <summary>
		/// Dungeons description
		/// </summary>
		Dictionary<string, XmlNode> Dungeons;

		/// <summary>
		/// Shared dungeons
		/// </summary>
		Dictionary<string, Dungeon> SharedDungeons;



		/// <summary>
		/// ItemSet
		/// </summary>
		Dictionary<string, XmlNode> Items;

		/// <summary>
		/// Shared ItemSet
		/// </summary>
		Dictionary<string, Item> SharedItems;




		/// <summary>
		/// Monsters definition
		/// </summary>
		Dictionary<string, XmlNode> Monsters;


		/// <summary>
		/// Spells
		/// </summary>
		Dictionary<string, XmlNode> Spells;



		/// <summary>
		/// Heroes
		/// </summary>
		Dictionary<string, XmlNode> Heroes;


		/// <summary>
		/// WallDecoration definition
		/// </summary>
		Dictionary<string, XmlNode> WallDecorations;



		#endregion
	}
}

