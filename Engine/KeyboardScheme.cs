using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using System.Reflection;
using ArcEngine.Asset;

namespace ArcEngine
{

	/// <summary>
	/// Keyboard scheme
	/// </summary>
	public class KeyboardScheme : IAsset
	{

		/// <summary>
		/// 
		/// </summary>
		public KeyboardScheme()
		{
			Inputs = new Dictionary<string, Keys>();
		}


		/// <summary>
		/// Load input scheme
		/// </summary>
		/// <param name="xml"></param>
		/// <returns></returns>
		public bool Load(XmlNode xml)
		{
			Name = xml.Attributes["name"].Value;

			foreach (XmlNode node in xml)
			{
				if (node.NodeType == XmlNodeType.Comment)
					continue;


				switch (node.Name.ToLower())
				{
					// Level
					case "input":
					{
						Inputs[node.Attributes["name"].Value] = (Keys)Enum.Parse(typeof(Keys), node.Attributes["key"].Value);
					}
					break;

				}


			}

			return true;
		}




		/// <summary>
		/// Save input scheme
		/// </summary>
		/// <param name="writer"></param>
		/// <returns></returns>
		public bool Save(XmlWriter writer)
		{
			return false;
		}


		/// <summary>
		/// Defaults all inputs
		/// </summary>
		public void Reset()
		{
			Inputs.Clear();
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public Keys this[string name]
		{
			get
			{
				return Inputs[name];
			}
			set
			{
				Inputs[name] = value;
			}
		}

		#region Properties


		/// <summary>
		/// 
		/// </summary>
		Dictionary<string, Keys> Inputs;


		/// <summary>
		/// Name of the Scheme
		/// </summary>
		public string Name
		{
			get;
			set;
		}

		#endregion
	}
}
