using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;



namespace ArcEngine.Asset
{
	/// <summary>
	/// Asset base interface
	/// </summary>
	public interface IAsset
	{

		#region IO
		/// <summary>
		/// Loads an asset
		/// </summary>
		/// <param name="xml"></param>
		/// <returns></returns>
		bool Load(XmlNode xml);


		/// <summary>
		/// 
		/// </summary>
		/// <param name="writer"></param>
		/// <returns></returns>
		bool Save(XmlWriter writer);

		#endregion



		#region Properties


		/// <summary>
		/// Name of the asset
		/// </summary>
		string Name
		{
			get;
			set;
		}


		#endregion
	}
}
