using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Ionic.Zip;
using System.Xml;

namespace ArcEngine.Storage
{
	/// <summary>
	/// Bank storage (zip) class
	/// </summary>
	public class BankStorage : StorageBase
	{

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="bankname">Bank name</param>
		public BankStorage(string bankname) : this(bankname, "")
		{
		}


		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="bankname">Bank name</param>
		/// <param name="password">Password</param>
		public BankStorage(string bankname, string password)
		{
			BankName = bankname;
			Password = password;
		}


		/// <summary>
		/// Process each entry in the bank
		/// </summary>
		/// <returns>True on success</returns>
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
		/// Flush pending resources
		/// </summary>
		public override void Flush()
		{
			if (Zip == null)
				return;

			Trace.WriteDebugLine("[BankStorage] Flush()");

			Zip.Save();
		}


		/// <summary>
		/// Dispose resources
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
		/// <param name="access"></param>
		/// <returns>Stream handle or null</returns>
		public override Stream OpenFile(string name, FileAccess access)
		{
			// Foreach each entry
			foreach (ZipEntry entry in Zip)
			{
				if (entry.FileName != name)
					continue;

				MemoryStream ms = new MemoryStream();
				entry.Extract(ms);
				ms.Seek(0, SeekOrigin.Begin);
				return ms;
			}

			// Read only access
			if (access == FileAccess.Read)
				return null;

			// Creates the file
			return new IonicStream(Zip, Path.GetFileName(name));
		}


		/// <summary>
		/// Creates a new file in the archive
		/// </summary>
		/// <param name="file">Entry name</param>
		/// <returns>Stream handle or null</returns>
		public override Stream CreateFile(string file)
		{
			return new IonicStream(Zip, file);
		}


		/// <summary>
		/// ToString()
		/// </summary>
		/// <returns>Name</returns>
		public override string ToString()
		{
			return string.Format("Bank storage ({0})", BankName);
		}


		#region Properties

		/// <summary>
		/// Bank name
		/// </summary>
		string BankName;

		/// <summary>
		/// Password to the zip file
		/// </summary>
		string Password;


		/// <summary>
		/// Zip Handle
		/// </summary>
		ZipFile Zip;

		#endregion

	}


	/// <summary>
	/// BankStorage stream to memory
	/// </summary>
	class IonicStream : MemoryStream
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="handle">Zip bank handle</param>
		/// <param name="filename">Filename in the bank</param>
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
			// No handle...
			if (Handle == null)
			{
				Trace.WriteLine("[BankStorage] IonicStream.Dispose() : Handle is null !");
				return;
			}


			// Rewind
			Seek(0, SeekOrigin.Begin);

			try
			{
				// Remove entry if exist
				if (Handle.ContainsEntry(FileName))
					Handle.RemoveEntry(FileName);

				Handle.AddEntry(FileName, this);

			}
			catch (Exception e)
			{
				Trace.WriteLine("[BankStorage] IonicStream.Dispose() Error : " + e.Message);
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
