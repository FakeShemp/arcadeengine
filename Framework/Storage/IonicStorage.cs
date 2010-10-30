using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Ionic.Zip;
using System.Xml;

namespace ArcEngine.Storage
{
	/// <summary>
	/// 
	/// </summary>
	public class IonicStorage : StorageBase
	{

		/// <summary>
		/// 
		/// </summary>
		/// <param name="bankname"></param>
		public IonicStorage(string bankname)
			: this(bankname, "")
		{
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="bankname"></param>
		/// <param name="password">Password</param>
		public IonicStorage(string bankname, string password)
		{
			BankName = bankname;
			Password = password;

			Process();
		}


		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public override bool Process()
		{
			if (Zip != null)
				Zip.Dispose();

			Zip = new ZipFile(BankName);
			if (Zip == null)
				return false;

			foreach(ZipEntry entry in Zip)
			{
				// Add file
				if (entry.IsDirectory)
				{
					Directories.Add(entry.FileName);
					continue;
				}
				else
					Files.Add(entry.FileName);
		

				if (!entry.FileName.EndsWith(".xml"))
					continue;

				// Convert to xml
				XmlDocument doc = new XmlDocument();

				// Uncompress data to a buffer
				using (MemoryStream ms = new MemoryStream())
				{
					//int size = 0;
					//byte[] data = new byte[4096];
					
					
					// Copy data
					//while ((size = Zip.Read(data, 0, data.Length)) > 0)
					//	ms.Write(data, 0, size);

					entry.Extract(ms);
					string src = ASCIIEncoding.ASCII.GetString(ms.ToArray());
					doc.LoadXml(src);
				}

				// Process asset
				ProcessAsset(doc.DocumentElement);
			}


			return true;
		}


		/// <summary>
		/// 
		/// </summary>
		public override void Flush()
		{
			if (Zip == null)
				return;
			
			Trace.WriteLine("[IonicStorage] Flush()");

			Zip.Save();
		}


		/// <summary>
		/// 
		/// </summary>
		public override void Dispose()
		{
			Flush();

			if (Zip != null)
				Zip.Dispose();
			Zip = null;
		}


		/// <summary>
		/// Opens a file at a specified path 
		/// </summary>
		/// <param name="name">Relative path of the file </param>
		/// <returns>Stream handle or null</returns>
		public override Stream OpenFile(string name)
		{

			foreach (ZipEntry entry in Zip)
			{
				if (entry.FileName != name)
					continue;

				MemoryStream ms = new MemoryStream();
				entry.Extract(ms);
				ms.Seek(0, SeekOrigin.Begin);
				return ms;
			}

			
			return null;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="file"></param>
		/// <returns></returns>
		public override Stream CreateFile(string file)
		{
			return new IonicStream(Zip, file);
		}


		#region Properties

		/// <summary>
		/// Bank name
		/// </summary>
		string BankName;

		/// <summary>
		/// 
		/// </summary>
		string Password;


		/// <summary>
		/// Zip Handle
		/// </summary>
		ZipFile Zip;

		#endregion

	}


	/// <summary>
	/// 
	/// </summary>
	class IonicStream : MemoryStream
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="handle"></param>
		/// <param name="filename"></param>
		public IonicStream(ZipFile handle, string filename)
		{
			Handle = handle;
			FileName = filename;
		}

		/// <summary>
		/// Dispose
		/// </summary>
		// http://community.sharpdevelop.net/forums/p/6210/17755.aspx#17755
		protected override void Dispose(bool disposing)
		{
			if (Handle == null)
			{
				Trace.WriteLine("Handle is null !");
			}


			// Rewind
			Seek(0, SeekOrigin.Begin);

			try
			{

				Handle.RemoveEntry(FileName);

				Handle.AddEntry(FileName, this);
			//	Handle.Save();

			}
			catch (Exception e)
			{
				Trace.WriteLine("[BankStorage] Error : " + e.Message);
			}

		}



		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			if (Handle == null)
				return "Empty...";

			return Handle.Name;
		}




		#region Properties


		/// <summary>
		/// Bank name
		/// </summary>
		ZipFile Handle;

		/// <summary>
		/// File name in the zip
		/// </summary>
		string FileName;

		#endregion
	}

}
