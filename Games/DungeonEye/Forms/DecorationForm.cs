#region Licence
//
//This file is part of ArcEngine.
//Copyright (C)2008-2011 Adrien Hémery ( iliak@mimicprod.net )
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
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using ArcEngine;
using ArcEngine.Asset;
using ArcEngine.Forms;
using ArcEngine.Graphic;
using ArcEngine.Interface;
using System.Drawing;


namespace DungeonEye.Forms
{
	/// <summary>
	/// Decoration form
	/// </summary>
	public partial class DecorationForm : AssetEditorBase
	{

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="node">xmlNode handle</param>
		public DecorationForm(XmlNode node)
		{
			InitializeComponent();

			BgTileSet = new TileSet();

			Decoration = new Decoration();
			Decoration.Load(node);
		}



		/// <summary>
		/// Change the background tileset
		/// </summary>
		/// <param name="name">Name of the tileset</param>
		/// <returns>True on success</returns>
		bool ChangeBackgroundTileSet(string name)
		{
			if (BgTileSet != null)
				BgTileSet.Dispose();

			if (string.IsNullOrEmpty(name))
				return false;

			BgTileSet = ResourceManager.CreateAsset<TileSet>(name);

			Decoration.BackgroundTileset = name;

			return BgTileSet != null;
		}


		/// <summary>
		/// Changes the decoration tileset
		/// </summary>
		/// <param name="name">TileSet name</param>
		/// <returns>True on success</returns>
		bool ChangeDecorationTileSet(string name)
		{
			return Decoration.LoadTileSet(name);
		}


		/// <summary>
		/// Render the scene
		/// </summary>
		void RenderScene()
		{
			OpenGLBox.MakeCurrent();
			Display.ClearBuffers();

			Batch.Begin();

			// Background
			Batch.DrawTile(BgTileSet, 0, Point.Empty);

			// Render the walls
			if (DisplayWallBox.Checked)
			{
				foreach (TileDrawing tmp in DisplayCoordinates.GetWalls(ViewPositionBox.Position))
					Batch.DrawTile(BgTileSet, tmp.ID, tmp.Location, Color.White, 0.0f, tmp.Effect, 0.0f);
			}


			// Draw the decoration
			DecorationInfo dec = Decoration.GetDecoration((int)TileIdBox.Value);
			if (dec != null)
			{
				Batch.DrawTile(Decoration.Tileset, dec.TileId, dec.Location);


			}
			
			Batch.End();

			OpenGLBox.SwapBuffers();
		}


		/// <summary>
		/// Save the asset to the manager
		/// </summary>
		public override void Save()
		{
			ResourceManager.AddAsset<Decoration>(Name, ResourceManager.ConvertAsset(Decoration));
		}



		#region Form events

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DecorationForm_Load(object sender, System.EventArgs e)
		{
			OpenGLBox.MakeCurrent();
			Display.ViewPort = new Rectangle(Point.Empty, OpenGLBox.Size);
			Display.RenderState.ClearColor = Color.Black;
			Display.RenderState.Blending = true;
			Display.BlendingFunction(BlendingFactorSource.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

			Batch = new SpriteBatch();

			// Collect all Tileset definition
			List<string> list = ResourceManager.GetAssets<TileSet>();
			BackgroundTileSetBox.Items.AddRange(list.ToArray());


			// Background tileset
			if (list.Contains(Decoration.BackgroundTileset))
				BackgroundTileSetBox.SelectedItem = Decoration.BackgroundTileset;

			else if (BackgroundTileSetBox.Items.Count > 0)
			{
				ChangeBackgroundTileSet((string)BackgroundTileSetBox.Items[0]);
				BackgroundTileSetBox.SelectedIndex = 0;
			}


			// Decoration TileSet
			list = ResourceManager.GetAssets<TileSet>();
			TilesetBox.Items.AddRange(list.ToArray());
			if (list.Contains(Decoration.TileSetName))
				TilesetBox.SelectedItem = Decoration.TileSetName;

			RenderScene();
		}


		/// <summary>
		/// Form closing
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DecorationForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			if (Batch != null)
				Batch.Dispose();
			Batch = null;

			if (BgTileSet != null)
				BgTileSet.Dispose();
			BgTileSet = null;
			
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TileSetListBox_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (BackgroundTileSetBox.SelectedIndex == -1)
				return;

			ChangeBackgroundTileSet((string)BackgroundTileSetBox.SelectedItem);

			RenderScene();
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OpenGLBox_Paint(object sender, PaintEventArgs e)
		{
			RenderScene();
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="position"></param>
		private void ViewPositionBox_PositionChanged(object sender, ViewFieldPosition position)
		{
			RenderScene();
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TilesetBox_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (TilesetBox.SelectedIndex == -1)
				return;

			ChangeDecorationTileSet((string)TilesetBox.SelectedItem);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DisplayWallBox_CheckedChanged(object sender, System.EventArgs e)
		{
			RenderScene();
		}

		#endregion



		#region Properties

		/// <summary>
		/// 
		/// </summary>
		public override IAsset Asset
		{
			get
			{
				return Decoration;
			}
		}


		/// <summary>
		/// 
		/// </summary>
		Decoration Decoration;


		/// <summary>
		/// Sprite batch
		/// </summary>
		SpriteBatch Batch;


		/// <summary>
		/// Background tileset
		/// </summary>
		TileSet BgTileSet;


		#endregion


	}
}
