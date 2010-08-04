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
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Xml;
using ArcEngine.Graphic;
using ArcEngine.Asset;
using ArcEngine.Input;


namespace ArcEngine.Utility.GUI
{

	/// <summary>
	/// Defines the base class for controls, which are components with visual representation
	/// </summary>
	public struct Message
	{
		/// <summary>
		/// Creates a new Message. 
		/// </summary>
		/// <param name="msg">The message ID</param>
		/// <param name="param1">Param 1</param>
		/// <param name="param2">Param 2</param>
		/// <returns></returns>
		static public Message Create(ControlMessage msg, object param1, object param2)
		{
			Message message = new Message();

			message.Msg = msg;
			message.Param1 = param1;
			message.Param2 = param2;

			return message;
		}



		/// <summary>
		/// Creates a new Message. 
		/// </summary>
		/// <param name="msg">The message ID</param>
		/// <returns></returns>
		static public Message Create(ControlMessage msg)
		{
			return Create(msg, null, null);
		}




		#region Properties

		/// <summary>
		/// Gets or sets the ID number for the message. 
		/// </summary>
		public ControlMessage Msg
		{
			get;
			private set;
		}


		/// <summary>
		/// The message param 1 field
		/// </summary>
		public object Param1
		{
			get;
			private set;
		}

		/// <summary>
		/// The message param 2 field
		/// </summary>
		public object Param2
		{
			get;
			private set;
		}


		#endregion
	}


	/// <summary>
	/// 
	/// </summary>
	public enum ControlMessage
	{
		/// <summary>
		/// Paint the control
		/// </summary>
		Paint,

		/// <summary>
		/// Occurs when the mouse pointer leaves the control.
		/// </summary>
		MouseLeave,


		/// <summary>
		/// Occurs when the mouse pointer enters the control.
		/// </summary>
		MouseEnter,

		/// <summary>
		/// 
		/// </summary>
		MouseDown,

		/// <summary>
		/// 
		/// </summary>
		MouseUp,

		/// <summary>
		/// 
		/// </summary>
		MouseHover,


		/// <summary>
		/// 
		/// </summary>
		MouseClick,
	}
}