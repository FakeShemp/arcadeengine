﻿#region Licence
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
using DungeonEye.Events;
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

			ScriptChoice choice = new ScriptChoice("Yes");
			choice.Enabled = true;
			choice.Button.Rectangle = DisplayCoordinates.ScriptedDialogChoices[6];
			Choices.Add(choice);

			choice = new ScriptChoice("No");
			choice.Enabled = true;
			choice.Button.Rectangle = DisplayCoordinates.ScriptedDialogChoices[7];
			Choices.Add(choice);
			

			choice = new ScriptChoice("Dunno");
			choice.Enabled = true;
			choice.Button.Rectangle = DisplayCoordinates.ScriptedDialogChoices[8];
			Choices.Add(choice);




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

			//Font = null;
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
			foreach (ScriptChoice choice in Choices)
			{
				if (!choice.Enabled)
					continue;

				choice.Button.Update(time);
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
			DrawSimpleBevel(batch, DisplayCoordinates.ScriptedDialog);
			batch.DrawString(GUI.DialogFont, new Point(4, 250), GameColors.White, Text);


			// Choices
			DrawChoices(batch);			
			
		}


		/// <summary>
		/// Display available choices
		/// </summary>
		/// <param name="batch">Spritebatch handle</param>
		void DrawChoices(SpriteBatch batch)
		{
			// No batch or choice
			if (batch == null || Choices.Count == 0)
				return;


			// Update each choice button
			foreach (ScriptChoice choice in Choices)
			{
				if (!choice.Enabled)
					continue;

				choice.Button.Draw(batch);
			}

			return;

			// Count the number of choice
			int start = 0;
			foreach (ScriptChoice choice in Choices)
				if (choice.Enabled)
					start++;
			start = Math.Min(start, 3) - 1;


			int pos = 0;
			foreach (ScriptChoice choice in Choices)
			{
				if (!choice.Enabled)
					continue;

				DrawSimpleBevel(batch, DisplayCoordinates.ScriptedDialogChoices[start * 3 + pos]);
				batch.DrawString(GUI.DialogFont, DisplayCoordinates.ScriptedDialogChoices[start * 3 + pos].Location, Color.White, choice.Name);

				pos++;
			}
		}


		#region Choices

		/// <summary>
		/// Set the choice
		/// </summary>
		/// <param name="choice">choice</param>
		public void SetChoices(ScriptChoice choice)
		{
			Choices.Clear();
			Choices.Add(choice);

		}

		/// <summary>
		/// Set the choices
		/// </summary>
		/// <param name="choice1">First choice</param>
		/// <param name="choice2">Second choice</param>
		public void SetChoices(ScriptChoice choice1, ScriptChoice choice2)
		{
			Choices.Clear();
			Choices.Add(choice1);
			Choices.Add(choice2);
		}

		/// <summary>
		/// Set the choices
		/// </summary>
		/// <param name="choice1">First choice</param>
		/// <param name="choice2">Second choice</param>
		/// <param name="choice2">Third choice</param>
		public void SetChoice(ScriptChoice choice1, ScriptChoice choice2, ScriptChoice choice3)
		{
			Choices.Clear();
			Choices.Add(choice1);
			Choices.Add(choice2);
			Choices.Add(choice3);
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
		List<ScriptChoice> Choices = new List<ScriptChoice>();


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
		/// Font to use
		/// </summary>
	//	BitmapFont Font;


		#endregion

	
	}
}