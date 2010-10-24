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

using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System;
using System.Drawing;


namespace ArcEngine.Asset
{
	/// <summary>
	/// Script Enumerator for PropertyGrids
	/// </summary>
	public class ScriptEnumerator : StringConverter
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true; // display drop
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return true; // drop-down vs combo
		}


		/// <summary>
		/// Retourne la liste de toutes les texture pour permettre de les selectionner
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			List<string> list = ResourceManager.GetAssets<Script>();
			list.Insert(0, "");
			return new StandardValuesCollection(list);
		}

	}

/*	
	/// <summary>
	/// Texture Enumerator for PropertyGrids
	/// </summary>
	public class TextureEnumerator : StringConverter
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true; // display drop
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return true; // drop-down vs combo
		}


		/// <summary>
		/// Retourne la liste de toutes les texture pour permettre de les selectionner
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			List<string> list = ResourceManager.GetAssets<Texture>();
			list.Insert(0, "");
			return new StandardValuesCollection(list);
		}

	}
*/

	/// <summary>
	/// Binary Enumerator for PropertyGrids
	/// </summary>
	public class BinaryEnumerator : StringConverter
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true; // display drop
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return true; // drop-down vs combo
		}


		/// <summary>
		/// Retourne la liste de toutes les texture pour permettre de les selectionner
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			List<string> list = new List<string>(); //ResourceManager.Binaries;
			list.Insert(0, "");
			return new StandardValuesCollection(list);
		}

	}




	/// <summary>
	/// TTF Enumerator for PropertyGrids
	/// </summary>
	public class TTFFileEnumerator : StringConverter
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true; // display drop
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return true; // drop-down vs combo
		}


		/// <summary>
		/// Retourne la liste de toutes les texture pour permettre de les selectionner
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			List<string> bin = new List<string>();;

			//foreach (string name in ResourceManager.Binaries)
			//{
			//    if (name.EndsWith(".ttf", true, CultureInfo.CurrentCulture))
			//        bin.Add(name);
			//}
			

			return new StandardValuesCollection(bin);
		}

	}


	/// <summary>
	/// TileSet Enumerator for PropertyGrids
	/// </summary>
	public class TileSetEnumerator : StringConverter
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true; // display drop
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return true; // drop-down vs combo
		}


		/// <summary>
		/// Retourne la liste de toutes les texture pour permettre de les selectionner
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			List<string> list = ResourceManager.GetAssets<TileSet>();
			list.Insert(0, "");
			return new StandardValuesCollection(list);
		}

	}

/*
	/// <summary>
	/// TileSet Enumerator for PropertyGrids
	/// </summary>
	internal class TileEnumerator : StringConverter
	{
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true; // display drop
		}
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return true; // drop-down vs combo
		}


		/// <summary>
		/// Retourne la liste de toutes les texture pour permettre de les selectionner
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			TileSet tileset = (context.Instance) as TileSet;
			return new StandardValuesCollection(tileset.Tiles);
		}

	}
*/


/*
	/// <summary>
	/// PointF enumerator
	/// </summary>
	internal class PointFConverter : ExpandableObjectConverter
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="context"></param>
		/// <param name="sourceType"></param>
		/// <returns></returns>
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			if (sourceType == typeof(string))
				return true;

			return base.CanConvertFrom(context, sourceType);
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="context"></param>
		/// <param name="culture"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			string text = value as string;
			if (text == null)
				return base.ConvertFrom(context, culture, value);


			return new PointF(1, 1);
		}
	}
*/
}
