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
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ArcEngine;
using ArcEngine.Asset;
using ArcEngine.Graphic;
using ArcEngine.Input;
using DungeonEye.EventScript;
using DungeonEye.Gui;

namespace DungeonEye
{
	/// <summary>
	/// Scripted dialog
	/// </summary>
	public class ScriptedDialog : DialogBase
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="square">Square</param>
		/// <param name="border">Display picture border</param>
		/// <param name="picture">Picture name</param>
		/// <param name="text">Text to display</param>
		public ScriptedDialog(Square square, bool border, string picture, string text)
		{
			if (square == null)
				throw new ArgumentNullException("Square is null");


			Square = square;
			Picture = new Texture2D(picture);
			Text = text;

			if (border)
			{
				DisplayBorder = true;
				Border = new Texture2D("border.png");
			}
			
			// Buttons
			Buttons = new ScriptButton[3];
			for (int i = 0 ; i < 3 ; i++)
			{
				Buttons[i] = new ScriptButton();
				Buttons[i].Click +=new EventHandler(ButtonClick);
			}

			Choices = new ScriptChoice[3];


	
			// Dummy tests
			ScriptChoice choice1 = new ScriptChoice("Yes");
			choice1.Actions.Add(new ScriptTeleport
			{
				Target = new DungeonLocation("maze_01", new Point(8, 2), CardinalPoint.West, SquarePosition.Center),
				ChangeDirection = true,
			});
			choice1.Actions.Add(new ScriptEndDialog());

			ScriptChoice choice2 = new ScriptChoice("No");
			choice2.Actions.Add(new ScriptTeleport
			{
				Target = new DungeonLocation("Forest", new Point(10, 12), CardinalPoint.North, SquarePosition.Center),
				ChangeDirection = true,
			});
			choice2.Actions.Add(new ScriptEndDialog());

			SetChoices(choice1, choice2);
		}


		/// <summary>
		/// Disposes resources
		/// </summary>
		public override void Dispose()
		{
			if (Picture != null)
				Picture.Dispose();
			Picture = null;

			if (Border != null)
				Border.Dispose();
			Border = null;
		}


		/// <summary>
		/// Update
		/// </summary>
		/// <param name="time">Game time</param>
		public override void Update(GameTime time)
		{
			if (Mouse.IsNewButtonDown(MouseButtons.Middle))
				Exit();


			// Update each choice button
			for (int id = 0; id < Choices.Length; id++)
			{
				if (Choices[id] == null || !Choices[id].Enabled)
					continue;

				Buttons[id].Update(time);
			}

		}


		/// <summary>
		/// Draws the dialog
		/// </summary>
		/// <param name="batch">Spritebatch handle</param>
		public override void Draw(SpriteBatch batch)
		{
			// Border
			if (DisplayBorder)
				batch.Draw(Border, Point.Empty, Color.White);

			// Picture
			batch.Draw(Picture, new Point(16, 16), Color.White);

			// Text
			GUI.DrawSimpleBevel(batch, DisplayCoordinates.ScriptedDialog);
			batch.DrawString(GUI.DialogFont, new Point(4, 250), GameColors.White, Text);


			// Choices
			for (int id = 0 ; id < Choices.Length ; id++)
			{
				if (Choices[id] == null || !Choices[id].Enabled)
					continue;

				Buttons[id].Draw(batch);
			}
			
		}


		#region Button actions

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void ButtonClick(object sender, EventArgs e)
		{
			ScriptButton button = sender as ScriptButton;

			if (button.Tag != null)
			{
				ScriptChoice choice = button.Tag as ScriptChoice;
				choice.Run();

				// Time to quit
				if (Quit)
					return;
			}
		}


		#endregion


		#region Choices

		/// <summary>
		/// Set the choice
		/// </summary>
		/// <param name="id">Choice id (from 1 to 3)</param>
		/// <param name="choice">choice</param>
		public void SetChoices(int id, ScriptChoice choice)
		{
			if (id < 1 || id > 3)
				return;

			Choices[id] = choice;

			Buttons[id].Rectangle = DisplayCoordinates.ScriptedDialogChoices[0];
			Buttons[id].Text = choice.Name;
			Buttons[id].Tag = choice;
		}

		/// <summary>
		/// Set the choice
		/// </summary>
		/// <param name="choice">choice</param>
		public void SetChoices(ScriptChoice choice)
		{
			Choices[0] = choice;
			Choices[1] = null;
			Choices[2] = null;

			Buttons[0].Rectangle = DisplayCoordinates.ScriptedDialogChoices[0];
			Buttons[0].Text = choice.Name;
			Buttons[0].Tag = choice;
		}

		/// <summary>
		/// Set the choices
		/// </summary>
		/// <param name="choice1">First choice</param>
		/// <param name="choice2">Second choice</param>
		public void SetChoices(ScriptChoice choice1, ScriptChoice choice2)
		{
			Choices[0] = choice1;
			Choices[1] = choice2;
			Choices[2] = null;

			Buttons[0].Rectangle = DisplayCoordinates.ScriptedDialogChoices[3];
			Buttons[0].Text = choice1.Name;
			Buttons[0].Tag = choice1;

			Buttons[1].Rectangle = DisplayCoordinates.ScriptedDialogChoices[4];
			Buttons[1].Text = choice2.Name;
			Buttons[1].Tag = choice2;
		}

		/// <summary>
		/// Set the choices
		/// </summary>
		/// <param name="choice1">First choice</param>
		/// <param name="choice2">Second choice</param>
		/// <param name="choice2">Third choice</param>
		public void SetChoice(ScriptChoice choice1, ScriptChoice choice2, ScriptChoice choice3)
		{
			Choices[0] = choice1;
			Choices[1] = choice2;
			Choices[2] = choice3;

			Buttons[0].Rectangle = DisplayCoordinates.ScriptedDialogChoices[6];
			Buttons[0].Text = choice1.Name;
			Buttons[0].Tag = choice1;

			Buttons[1].Rectangle = DisplayCoordinates.ScriptedDialogChoices[7];
			Buttons[1].Text = choice2.Name;
			Buttons[1].Tag = choice2;


			Buttons[2].Rectangle = DisplayCoordinates.ScriptedDialogChoices[8];
			Buttons[2].Text = choice3.Name;
			Buttons[2].Tag = choice3;
		}

		#endregion


		#region Picture

		/// <summary>
		/// Changes the picture
		/// </summary>
		/// <param name="name">Name of the picture</param>
		/// <returns>True on success</returns>
		public bool SetPicture(string name)
		{
			if (Picture != null)
				Picture.Dispose();

			Picture = new Texture2D(name);

			return Picture != null;
		}



		/// <summary>
		/// Changes the picture
		/// </summary>
		/// <param name="name">texture handle</param>
		public void SetPicture(Texture2D handle)
		{
			if (Picture != null)
				Picture.Dispose();

			Picture = handle;
		}

		#endregion


		#region Properties

		/// <summary>
		/// Available choices
		/// </summary>
		ScriptChoice[] Choices;


		/// <summary>
		/// Square
		/// </summary>
		Square Square;


		/// <summary>
		/// Text to display
		/// </summary>
		public string Text
		{
			get;
			set;
		}



		/// <summary>
		/// Picture to display
		/// </summary>
		public Texture2D Picture
		{
			get;
			private set;
		}


		/// <summary>
		/// Display picture border
		/// </summary>
		public bool DisplayBorder
		{
			get;
			set;
		}

		/// <summary>
		/// Border texture
		/// </summary>
		Texture2D Border;


		/// <summary>
		/// Buttons
		/// </summary>
		ScriptButton[] Buttons;

		#endregion

	
	}
}
