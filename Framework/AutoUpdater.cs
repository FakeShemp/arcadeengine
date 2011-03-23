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

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Serialization;
using ArcEngine.Forms;


// Application.ProductVersion
// http://themech.net/2008/05/adding-check-for-update-option-in-csharp/

namespace ArcEngine
{
	/// <summary>
	/// Check for update utility class
	/// </summary>
	static public class AutoUpdater
	{

		/// <summary>
		/// 
		/// </summary>
		static AutoUpdater()
		{
			ShowDialog = true;
		}



		/// <summary>
		/// Checks for a new version
		/// </summary>
		/// <param name="url">Url of the file description</param>
		static public void CheckForNewVersion(string url)
		{
			Thread th = new Thread(new ParameterizedThreadStart(Check));
			th.Start(url);
		}


		/// <summary>
		/// Check for updates
		/// </summary>
		/// <param name="url">Uri path</param>
		static void Check(object url)
		{
			try
			{
				WebRequest req = WebRequest.Create((string)url);
				WebResponse response = req.GetResponse();

				XmlSerializer deserializer = new XmlSerializer(typeof(ProductVersions));
				ProductVersions products = (ProductVersions)deserializer.Deserialize(response.GetResponseStream());

				// Check if a new version is present
				ProductVersion pv = (from p in products.Products
									where p.Name == Application.ProductName
									&& new Version(p.Version) > new Version(Application.ProductVersion)
									orderby p.Version descending
									select p).FirstOrDefault();

				if (pv == null)
					return;

				OnNewVersion(pv);
			}
			catch (Exception e)
			{
				Trace.WriteLine("[AutoUpdate]Check : Exception : " + e.Message);
			}

		}


		#region Events

		/// <summary>
		/// New version event
		/// </summary>
		/// <param name="pv"></param>
		static void OnNewVersion(ProductVersion pv)
		{
			if (NewVersion != null)
				NewVersion(pv);
			else if (ShowDialog)
			{
				new AutoUpdateForm(pv).ShowDialog();
			}
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="product"></param>
		public delegate void NewVersionHandler(ProductVersion product);


		/// <summary>
		///  Event fired when a new version is found
		/// </summary>
		static public event NewVersionHandler NewVersion;

		#endregion



		#region Properties

		/// <summary>
		/// Shows a dialog window informing that an update is availabe
		/// </summary>
		static public bool ShowDialog
		{
			get;
			set;
		}

		#endregion
	}


	/// <summary>
	/// 
	/// </summary>
	[XmlRoot("Products")]
	public  class ProductVersions
	{
		/// <summary>
		/// 
		/// </summary>
		public ProductVersions()
		{
			Products = new List<ProductVersion>();
		}


		/// <summary>
		/// 
		/// </summary>
		[XmlElement("Product")]
		public List<ProductVersion> Products;
	}


	/// <summary>
	/// Product update description
	/// </summary>
	[XmlRoot(ElementName="Product")]
	public class ProductVersion
	{

		/// <summary>
		/// Name of the product
		/// </summary>
		[XmlElement("Name")]
		public string Name { get; set; }


		/// <summary>
		/// Version
		/// </summary>
		[XmlElement("Version")]
		public string Version { get; set; }


		/// <summary>
		/// Download url
		/// </summary>
		[XmlElement("Url")]
		public string Url { get; set; }


		/// <summary>
		/// Direct download url
		/// </summary>
		[XmlElement("DirectDownload")]
		public string DirectDownload { get; set; }
	}
	
}
