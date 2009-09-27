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
using ArcEngine.Asset;
using System.Xml;
using System.Drawing;
using ArcEngine.Graphic;
using ArcEngine;


namespace ArcEngine.Games.Breakout
{

	/// <summary>
	/// 
	/// </summary>
	public class Level : IAsset
	{

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="name">Name of the level</param>
		public Level(string name)
		{
			Name = name;
			Bricks = new Brick[16, 25];
			Balls = new List<Ball>();
			Paddle = new Paddle();

		}



		/// <summary>
		/// Initializes the level
		/// </summary>
		/// <returns></returns>
		public bool Init()
		{
			Background = new Texture("background.png");


			Tileset = ResourceManager.CreateAsset<TileSet>(TilesetName);


			// Balls
			Ball ball = new Ball(this);
			Balls.Add(ball);

			// Paddle
			Paddle.Init();
			Paddle.Balls.Add(ball);


			// Bricks
			Brick.Init();

			return true;
		}



		/// <summary>
		/// 
		/// </summary>
		/// <param name="device"></param>
		public void Draw()
		{


			// Background
			Background.Blit(new Rectangle(160, 0, 640, 600), TextureLayout.Tile);

			// Draw bricks
			for(int y = 0; y < 25; y++)
				for (int x = 0; x < 16; x++)
				{
					if (Bricks[x, y] == null)
						continue;

					Bricks[x, y].Draw(new Point(160 + x * 40, y * 20));
				}



			Point[] points = new Point[Bricks.Length];
			int[] ids = new int[Bricks.Length];
			int id = 0;
			for (int y = 0; y < 25; y++)
				for (int x = 0; x < 16; x++)
				{
					if (Bricks[x, y] == null)
						continue;

					points[id] = new Point(160 + x * 40, y * 20);
					ids[id] = 14; // Bricks[x, y].TileID;

					//Bricks[x, y].Draw(new Point(160 + x * 40, y * 20));
				}


		//	Tileset.Draw(points, ids);
			

			// Draw the paddle
			Paddle.Draw();


			// Draw the balls
			foreach (Ball ball in Balls)
				ball.Draw();




		}



		/// <summary>
		/// 
		/// </summary>
		/// <param name="time"></param>
		public void Update(GameTime gameTime)
		{

			// Update paddle
			Paddle.Update(gameTime);


			// Update balls
			foreach (Ball ball in Balls)
				ball.Update(gameTime);

			// Removes lost balls
			Balls.RemoveAll(delegate(Ball ball)
			{
				return ball.Lost;
			});

		}



		#region	IO operations
		///
		///<summary>
		/// Save the collection to a xml node
		/// </summary>
		///
		public bool Save(XmlWriter xml)
		{
			if (xml == null)
				return false;


			xml.WriteStartElement("tileset");
			xml.WriteAttributeString("name", TilesetName);


			// Loops throughs cells
			foreach(Brick brick in Bricks)
			{
				xml.WriteStartElement("brick");

				//xml.WriteAttributeString("BufferID", val.Key.ToString());

				//xml.WriteStartElement("rectangle");
				//xml.WriteAttributeString("x", val.Value.Rectangle.X.ToString());
				//xml.WriteAttributeString("y", val.Value.Rectangle.Y.ToString());
				//xml.WriteAttributeString("width", val.Value.Rectangle.Width.ToString());
				//xml.WriteAttributeString("height", val.Value.Rectangle.Height.ToString());
				//xml.WriteEndElement();

				//xml.WriteStartElement("hotspot");
				//xml.WriteAttributeString("x", val.Value.HotSpot.X.ToString());
				//xml.WriteAttributeString("y", val.Value.HotSpot.Y.ToString());
				//xml.WriteEndElement();

				//xml.WriteStartElement("collisionbox");
				//xml.WriteAttributeString("x", val.Value.CollisionBox.X.ToString());
				//xml.WriteAttributeString("y", val.Value.CollisionBox.Y.ToString());
				//xml.WriteAttributeString("width", val.Value.CollisionBox.Width.ToString());
				//xml.WriteAttributeString("height", val.Value.CollisionBox.Height.ToString());
				//xml.WriteEndElement();

				xml.WriteEndElement();
			}


			xml.WriteEndElement();

			return true;
		}


		/// <summary>
		/// Loads the collection from a xml node
		/// </summary>
		/// <param name="xml"></param>
		/// <returns></returns>
		public bool Load(XmlNode xml)
		{

			// Process datas
			XmlNodeList nodes = xml.ChildNodes;
			foreach (XmlNode node in nodes)
			{

				switch (node.Name.ToLower())
				{
					case "brick":
					{
						Brick brick = new Brick();
						brick.Bonus = (BrickBonus)Enum.Parse(typeof(BrickBonus), node.Attributes["bonus"].Value, true);
						Bricks[int.Parse(node.Attributes["x"].Value), int.Parse(node.Attributes["y"].Value)] = brick;

					}
					break;


					case "tileset":
					{
						TilesetName = node.Attributes["name"].Value;
					}
					break;
				}
			}

			return true;
		}

		#endregion



		#region Properties
		/// <summary>
		/// Name of the sound
		/// </summary>
		public string Name
		{
			get;
			set;
		}

		/// <summary>
		/// Xml tag of the asset in bank
		/// </summary>
		public string XmlTag
		{
			get
			{
				return "level";
			}
		}



		/// <summary>
		/// Background texture
		/// </summary>
		Texture Background;


		/// <summary>
		/// Paddle
		/// </summary>
		public Paddle Paddle
		{
			get;
			private set;
		}



		/// <summary>
		/// Availables balls
		/// </summary>
		public List<Ball> Balls
		{
			get;
			private set;
		}



		/// <summary>
		/// Bricks in the level
		/// </summary>
		Brick[,] Bricks;



		/// <summary>
		/// Tileset of the level
		/// </summary>
		TileSet Tileset;


		/// <summary>
		/// Name of the tileset to use
		/// </summary>
		string TilesetName;


		/// <summary>
		/// Next level
		/// </summary>
		public string NextLevel;

		#endregion
	}
}
