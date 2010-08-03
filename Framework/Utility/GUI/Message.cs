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
		/// <param name="param"></param>
		/// <returns></returns>
		static public Message Create(ControlMessage msg, object param)
		{
			Message message = new Message();

			message.Msg = msg;
			message.Param = param;

			return message;
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
		/// The message param field
		/// </summary>
		public object Param
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
		/// Perform update logic
		/// </summary>
		PerformLogic,

		/// <summary>
		/// Paint the control
		/// </summary>
		Paint,


	}
}