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
using System.Collections.Generic;
using System;
using System.Drawing;
using System.Windows.Forms;
using ArcEngine;
using ArcEngine.Forms;
using ArcEngine.Graphic;
using ArcEngine.Asset;


namespace DungeonEye.Forms
{
	/// <summary>
	/// 
	/// </summary>
	public partial class SquareForm : AssetEditorBase
	{

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="maze">Maze</param>
		/// <param name="position">Location of the block</param>
		public SquareForm(Maze maze, Square block)
		{
			InitializeComponent();

			if (block == null)
			{
				Close();
				return;
			}
			MazeBlock = block;
			Maze = maze;


			#region Ground items

			//Itemset = ResourceManager.CreateAsset<ItemSet>("Items");

			NWBox.BeginUpdate();
			NWBox.Items.Clear();
			NEBox.BeginUpdate();
			NEBox.Items.Clear();
			SWBox.BeginUpdate();
			SWBox.Items.Clear();
			SEBox.BeginUpdate();
			SEBox.Items.Clear();

			foreach (Item item in MazeBlock.Items[0])
				NWBox.Items.Add(item.Name);
			foreach (Item item in MazeBlock.Items[1])
				NEBox.Items.Add(item.Name);
			foreach (Item item in MazeBlock.Items[2])
				SWBox.Items.Add(item.Name);
			foreach (Item item in MazeBlock.Items[3])
				SEBox.Items.Add(item.Name);

			NWBox.EndUpdate();
			NEBox.EndUpdate();
			SWBox.EndUpdate();
			SEBox.EndUpdate();


			//if (Itemset != null)
			//{
				NWItemsBox.BeginUpdate();
				NWItemsBox.Items.Clear();
				NEItemsBox.BeginUpdate();
				NEItemsBox.Items.Clear();
				SWItemsBox.BeginUpdate();
				SWItemsBox.Items.Clear();
				SEItemsBox.BeginUpdate();
				SEItemsBox.Items.Clear();

				foreach (string item in ResourceManager.GetAssets<Item>())
				{
					NWItemsBox.Items.Add(item);
					NEItemsBox.Items.Add(item);
					SWItemsBox.Items.Add(item);
					SEItemsBox.Items.Add(item);
				}

				NWItemsBox.EndUpdate();
				NEItemsBox.EndUpdate();
				SWItemsBox.EndUpdate();
				SEItemsBox.EndUpdate();
			//}
			#endregion


			#region Monsters

			// Add templates
			MonsterTemplateBox.BeginUpdate();
			MonsterTemplateBox.Items.Clear();

			foreach (string name in ResourceManager.GetAssets<Monster>())
				MonsterTemplateBox.Items.Add(name);

			MonsterTemplateBox.EndUpdate();


			#endregion


			#region Walls

			#region Door

			DoorStateBox.BeginUpdate();
			DoorStateBox.Items.Clear();
			foreach (string name in Enum.GetNames(typeof(DoorState)))
				DoorStateBox.Items.Add(name);
			DoorStateBox.EndUpdate();

			DoorTypeBox.BeginUpdate();
			DoorTypeBox.Items.Clear();
			foreach (string name in Enum.GetNames(typeof(DoorType)))
				DoorTypeBox.Items.Add(name);
			DoorTypeBox.EndUpdate();

			#endregion 


			#region Floor plate

			List<string> scripts = ResourceManager.GetAssets<Script>();
			FloorPlateScriptBox.BeginUpdate();
			FloorPlateScriptBox.Items.Clear();
			foreach (string name in scripts)
				FloorPlateScriptBox.Items.Add(name);
			FloorPlateScriptBox.EndUpdate();

			//if (MazeBlock.FloorPlate != null)
			//{
			//    if (FloorPlateScriptBox.Items.Contains(MazeBlock.FloorPlate.ScriptName))
			//        FloorPlateScriptBox.SelectedItem = MazeBlock.FloorPlate.ScriptName;

			//    if (OnEnterFloorPlateBox.Items.Contains(MazeBlock.FloorPlate.OnEnterScript))
			//        OnEnterFloorPlateBox.SelectedItem = MazeBlock.FloorPlate.OnEnterScript;

			//    if (OnLeaveFloorPlateBox.Items.Contains(MazeBlock.FloorPlate.OnLeaveScript))
			//        OnLeaveFloorPlateBox.SelectedItem = MazeBlock.FloorPlate.OnLeaveScript;
			//}



			#endregion

			
			#region Force Field
			ForceFieldTypeBox.BeginUpdate();
			ForceFieldTypeBox.Items.Clear();
			foreach (string name in Enum.GetNames(typeof(ForceFieldType)))
				ForceFieldTypeBox.Items.Add(name);
			ForceFieldTypeBox.EndUpdate();

			ForceFieldRotationBox.BeginUpdate();
			ForceFieldRotationBox.Items.Clear();
			foreach (string name in Enum.GetNames(typeof(CompassRotation)))
				ForceFieldRotationBox.Items.Add(name);
			ForceFieldRotationBox.EndUpdate();


			ForceFieldMoveBox.BeginUpdate();
			ForceFieldMoveBox.Items.Clear();
			foreach (string name in Enum.GetNames(typeof(CardinalPoint)))
				ForceFieldMoveBox.Items.Add(name);
			ForceFieldMoveBox.EndUpdate();

			#endregion

			AlcoveNorthButton.Checked = MazeBlock.HasAlcove(CardinalPoint.North);
			AlcoveSouthButton.Checked = MazeBlock.HasAlcove(CardinalPoint.South);
			AlcoveWestButton.Checked = MazeBlock.HasAlcove(CardinalPoint.West);
			AlcoveEastButton.Checked = MazeBlock.HasAlcove(CardinalPoint.East);

			WallTypeBox.BeginUpdate();
			foreach(string name in Enum.GetNames(typeof(SquareType)))
				WallTypeBox.Items.Add(name);
			WallTypeBox.EndUpdate();
			WallTypeBox.SelectedItem = MazeBlock.Type.ToString();
			#endregion

/*
			#region Stairs
			StairTypeBox.BeginUpdate();
			StairTypeBox.Items.Clear();
			foreach (string name in Enum.GetNames(typeof(StairType)))
				StairTypeBox.Items.Add(name);
			if (MazeBlock.Stair != null)
				StairTypeBox.SelectedItem = MazeBlock.Stair.Type.ToString();
			StairTypeBox.EndUpdate();
			#endregion
*/
		}


		#region Items events


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void NWAddItem_Click(object sender, EventArgs e)
		{
			MazeBlock.Items[0].Add(ResourceManager.CreateAsset<Item>(NWItemsBox.SelectedItem as string));
			NWBox.Items.Add(NWItemsBox.SelectedItem as string);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void NEAddItem_Click(object sender, EventArgs e)
		{
			MazeBlock.Items[1].Add(ResourceManager.CreateAsset<Item>(NEItemsBox.SelectedItem as string));
			NEBox.Items.Add(NEItemsBox.SelectedItem as string);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void NERemoveItem_Click(object sender, EventArgs e)
		{
			if (NEBox.SelectedIndex == -1)
				return;

			MazeBlock.Items[1].RemoveAt(NEBox.SelectedIndex);

			NEBox.Items.Clear();
			foreach (Item item in MazeBlock.Items[1])
				NEBox.Items.Add(item.Name);

		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void NWRemoveItem_Click(object sender, EventArgs e)
		{
			if (NWBox.SelectedIndex == -1)
				return;

			MazeBlock.Items[0].RemoveAt(NWBox.SelectedIndex);

			NWBox.Items.Clear();
			foreach (Item item in MazeBlock.Items[0])
				NWBox.Items.Add(item.Name);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SWAddItem_Click(object sender, EventArgs e)
		{
			MazeBlock.Items[2].Add(ResourceManager.CreateAsset<Item>(SWItemsBox.SelectedItem as string));
			SWBox.Items.Add(SWItemsBox.SelectedItem as string);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SEAddItem_Click(object sender, EventArgs e)
		{
			MazeBlock.Items[3].Add(ResourceManager.CreateAsset<Item>(SEItemsBox.SelectedItem as string));
			SEBox.Items.Add(SEItemsBox.SelectedItem as string);

		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SERemoveItem_Click(object sender, EventArgs e)
		{
			if (SEBox.SelectedIndex == -1)
				return;

			MazeBlock.Items[3].RemoveAt(SEBox.SelectedIndex);

			SEBox.Items.Clear();
			foreach (Item item in MazeBlock.Items[3])
				SEBox.Items.Add(item.Name);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SWRemoveItem_Click(object sender, EventArgs e)
		{
			if (SWBox.SelectedIndex == -1)
				return;

			MazeBlock.Items[2].RemoveAt(SWBox.SelectedIndex);

			SWBox.Items.Clear();
			foreach (Item item in MazeBlock.Items[2])
				SWBox.Items.Add(item.Name);
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void NWBox_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (NWBox.SelectedIndex == -1)
				return;

			MazeBlock.Items[0].RemoveAt(NWBox.SelectedIndex);
			NWBox.Items.RemoveAt(NWBox.SelectedIndex);
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void NEBox_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (NEBox.SelectedIndex == -1)
				return;

			MazeBlock.Items[1].RemoveAt(NEBox.SelectedIndex);
			NEBox.Items.RemoveAt(NEBox.SelectedIndex);
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SEBox_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (SEBox.SelectedIndex == -1)
				return;

			MazeBlock.Items[3].RemoveAt(SEBox.SelectedIndex);
			SEBox.Items.RemoveAt(SEBox.SelectedIndex);
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SWBox_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (SWBox.SelectedIndex == -1)
				return;

			MazeBlock.Items[2].RemoveAt(SWBox.SelectedIndex);
			SWBox.Items.RemoveAt(SWBox.SelectedIndex);
		}


		private void AlcoveNorthButton_CheckedChanged(object sender, EventArgs e)
		{

			MazeBlock.SetAlcove(CardinalPoint.North, AlcoveNorthButton.Checked);
		}

		private void AlcoveSouthButton_CheckedChanged(object sender, EventArgs e)
		{
			MazeBlock.SetAlcove(CardinalPoint.South, AlcoveSouthButton.Checked);
		}

		private void AlcoveWestButton_CheckedChanged(object sender, EventArgs e)
		{
			MazeBlock.SetAlcove(CardinalPoint.West, AlcoveWestButton.Checked);
		}

		private void AlcoveEastButton_CheckedChanged(object sender, EventArgs e)
		{
			MazeBlock.SetAlcove(CardinalPoint.East, AlcoveEastButton.Checked);
		}


		#endregion


		#region Form events

		/// <summary>
		/// OnLoad
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MazeBlockForm_Load(object sender, EventArgs e)
		{


		}

		/// <summary>
		/// OnKeyDown
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MazeBlockForm_KeyDown(object sender, KeyEventArgs e)
		{
			// Escape key close the form
			if (e.KeyCode == Keys.Escape)
				Close();
		}


		#endregion


		#region Monster events


		/// <summary>
		/// Applies a template
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ApplyMonsterTemplateBox_Click(object sender, EventArgs e)
		{
			if (GroundLocationBox.SelectedIndex == -1 || MonsterTemplateBox.SelectedItem == null)
				return;
			

			Monster monster = ResourceManager.CreateAsset<Monster>((string)MonsterTemplateBox.SelectedItem);
			//monster.Location = new DungeonLocation(MazeBlock.Location);
			//monster.Location.Position = (SquarePosition)Enum.ToObject(typeof(SquarePosition), GroundLocationBox.SelectedIndex);
			//monster.Location.SetMaze(Maze.Name);
			//monster.Init();

			monster.Teleport(MazeBlock, (SquarePosition) Enum.ToObject(typeof(SquarePosition), GroundLocationBox.SelectedIndex));
			//MazeBlock.SetMonster(monster, (SquarePosition) Enum.ToObject(typeof(SquarePosition), GroundLocationBox.SelectedIndex));

			//MonsterBox.Monster = monster;
		}


		/// <summary>
		/// Selects an existing monster
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GroundLocationBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (GroundLocationBox.SelectedIndex == -1)
				return;

			Monster monster = MazeBlock.GetMonster((SquarePosition) GroundLocationBox.SelectedIndex);
			if (monster == null)
				MonsterBox.Visible = false;
			else
				MonsterBox.Visible = true;

			MonsterBox.SetMonster(monster);
		}



		/// <summary>
		/// Removes a monster 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void RemoveMonsterBox_Click(object sender, EventArgs e)
		{
			if (GroundLocationBox.SelectedIndex == -1)
				return;

			if (MazeBlock == null)
				return;

			MazeBlock.RemoveMonster((SquarePosition)GroundLocationBox.SelectedIndex);
			MonsterBox.Visible = false;
			MonsterBox.SetMonster(null);


		}


		#endregion


		#region Walls


		private void ButtonNorthButton_CheckedChanged(object sender, EventArgs e)
		{

		}

		private void ButtonSouthButton_CheckedChanged(object sender, EventArgs e)
		{

		}

		private void ButtonWestButton_CheckedChanged(object sender, EventArgs e)
		{

		}

		private void ButtonEastButton_CheckedChanged(object sender, EventArgs e)
		{

		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GlWallControl_Load(object sender, EventArgs e)
		{
			GlWallControl.MakeCurrent();
			Display.Init();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GlWallControl_Resize(object sender, EventArgs e)
		{
			GlWallControl.MakeCurrent();
			Display.ViewPort = new Rectangle(Point.Empty, GlWallControl.Size);
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GlWallControl_Paint(object sender, PaintEventArgs e)
		{
			GlWallControl.MakeCurrent();
			Display.ClearBuffers();



			GlWallControl.SwapBuffers();
		}

		/// <summary>
		/// Changes the type of a maze block
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void WallTypeBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			MazeBlock.Type = (SquareType)Enum.Parse(typeof(SquareType), (string)WallTypeBox.SelectedItem);
		}

		#endregion


		#region Specials

		/// <summary>
		/// Change special type of block
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SpecialTypeBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			switch (SpecialTypeBox.SelectedIndex)
			{
				// None
				case 0:
				{
					StairGroupBox.Enabled = false;
					PitGroupBox.Enabled = false;
					DoorGroupBox.Enabled = false;
					ForceFieldGroupBox.Enabled = false;
					TeleporterGroupBox.Enabled = false;
					PlateGroupBox.Enabled = false;

				}
				break;

			}
		}

		/// <summary>
		/// OnShow
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SpecialTab_Enter(object sender, EventArgs e)
		{
			
		}

		#endregion


		#region Doors

		/// <summary>
		/// Door has button
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HasButtonBox_CheckedChanged(object sender, EventArgs e)
		{
			//if (MazeBlock.Door == null)
			//	return;

			//MazeBlock.Door.HasButton = HasButtonBox.Checked;
		}


		/// <summary>
		/// On Door state change
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DoorStateBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			//if (MazeBlock.Door == null)
			//	return;

			//MazeBlock.Door.State = (DoorState)Enum.Parse(typeof(DoorState), DoorStateBox.SelectedItem.ToString());
		}


		/// <summary>
		/// On Door type change
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DoorTypeBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			//if (MazeBlock.Door == null)
			//	return;

			//MazeBlock.Door.Type = (DoorType)Enum.Parse(typeof(DoorType), DoorTypeBox.SelectedItem.ToString());
		}


		#endregion


		#region Floor Plate

		/// <summary>
		/// Shows / hides floor plate
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HiddenPlateBox_CheckedChanged(object sender, EventArgs e)
		{
			//if (MazeBlock.FloorPlate == null)
			//    return;


			//MazeBlock.FloorPlate.Invisible = HiddenPlateBox.Checked;
		}



		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void FloorPlateScriptBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			//if (FloorPlateScriptBox.SelectedIndex == -1)
			//    return;

			//MazeBlock.FloorPlate.ScriptName = FloorPlateScriptBox.SelectedItem as string;

			//OnEnterFloorPlateBox.Items.Clear();
			//OnLeaveFloorPlateBox.Items.Clear();

			//Script script = ResourceManager.CreateAsset<Script>(FloorPlateScriptBox.SelectedItem as string);
			//if (script == null)
			//    return;

			//OnEnterFloorPlateBox.BeginUpdate();
			//OnLeaveFloorPlateBox.BeginUpdate();

			//List<string> methods = script.GetMethods();
			//foreach (string name in methods)
			//{
			//    OnEnterFloorPlateBox.Items.Add(name);
			//    OnLeaveFloorPlateBox.Items.Add(name);
			//}

			//OnEnterFloorPlateBox.EndUpdate();
			//OnLeaveFloorPlateBox.EndUpdate();

		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnEnterFloorPlateBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			//if (OnEnterFloorPlateBox.SelectedIndex == -1)
			//    return;

			//MazeBlock.FloorPlate.OnEnterScript = OnEnterFloorPlateBox.SelectedItem as string;

		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnLeaveFloorPlateBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			//if (OnLeaveFloorPlateBox.SelectedIndex == -1)
			//    return;

			//MazeBlock.FloorPlate.OnLeaveScript = OnLeaveFloorPlateBox.SelectedItem as string;

		}



		#endregion


		#region Pit

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void PitTargetBox_Click(object sender, EventArgs e)
		{
			if (MazeBlock == null)
				return;

			Pit pit = MazeBlock.Actor as Pit;
			if (pit == null)
				return;

			DungeonLocationForm form = new DungeonLocationForm(Maze.Dungeon, pit.Target.MazeName, pit.Target.Coordinate);
			if (form.ShowDialog() != DialogResult.OK)
				return;

			pit.Target = form.Target;
			PitTargetLabel.Text = "Target : " + pit.Target.ToString();
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HiddenBox_CheckedChanged(object sender, EventArgs e)
		{
			if (MazeBlock == null)
				return;

			Pit pit = MazeBlock.Actor as Pit;
			if (pit == null)
				return;


			pit.IsHidden = HiddenPitBox.Checked;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DiffcultyBox_ValueChanged(object sender, EventArgs e)
		{
			if (MazeBlock == null)
				return;

			Pit pit = MazeBlock.Actor as Pit;
			if (pit == null)
				return;

			pit.Difficulty = (int) PitDiffcultyBox.Value;
		}

		private void diceForm1_ValueChanged(object sender, EventArgs e)
		{
			if (MazeBlock == null)
				return;

			Pit pit = MazeBlock.Actor as Pit;
			if (pit == null)
				return;


			pit.Damage.Modifier = PitDamageBox.Dice.Modifier;
			pit.Damage.Faces = PitDamageBox.Dice.Faces;
			pit.Damage.Throws = PitDamageBox.Dice.Throws;
		}

		#endregion


		#region Force Field

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ForceFieldTypeBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			//if (MazeBlock.ForceField == null)
			//    return;
			//MazeBlock.ForceField.Type = (ForceFieldType)Enum.Parse(typeof(ForceFieldType), ForceFieldTypeBox.SelectedItem as string);

		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ForceFieldRotationBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			//if (MazeBlock.ForceField == null)
			//    return;

			//MazeBlock.ForceField.Rotation = (CompassRotation)Enum.Parse(typeof(CompassRotation), ForceFieldRotationBox.SelectedItem as string);
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ForceFieldMoveBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			//if (MazeBlock.ForceField == null)
			//    return;

			//MazeBlock.ForceField.Move = (CardinalPoint)Enum.Parse(typeof(CardinalPoint), ForceFieldMoveBox.SelectedItem as string);
		}


		#endregion


		#region Stairs


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void StairTargetBox_Click(object sender, EventArgs e)
		{
			//if (MazeBlock.Stair == null)
			//    return;

			//DungeonLocationForm form = new DungeonLocationForm(Maze.Dungeon, MazeBlock.Stair.Target);
			//if (form.ShowDialog() != DialogResult.OK)
			//    return;

			//MazeBlock.Stair.Target = form.Target;
			//StairTargetLabel.Text = "Target : " + MazeBlock.Stair.Target.ToString();
		}


		/// <summary>
		/// Stair direction
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void StairTypeBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			//if (MazeBlock.Stair == null)
			//    return;

			//MazeBlock.Stair.Type = (StairType)Enum.Parse(typeof(StairType), (string)StairTypeBox.SelectedItem);
		}


		#endregion



		#region Properties

		/// <summary>
		/// 
		/// </summary>
		Maze Maze;

		/// <summary>
		/// 
		/// </summary>
		Square MazeBlock;


		#endregion


	}
}