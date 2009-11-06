using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;

using ArcEngine.Graphic;

namespace ArcEngine.Utility
{
	/// <summary>
	/// Class to access the Webcam
	/// </summary>
	/// 
	/// http://www.songho.ca/opengl/gl_pbo.html (streaming texture update)
	public class Webcam
	{

		/// <summary>
		/// Open a webcam
		/// </summary>
		/// <param name="id">ID of the device</param>
		/// <param name="size">Size of the video stream</param>
		public Webcam(int id, Size size)
		{
			ID = id;
			Size = size;
		}


		/// <summary>
		/// List all available devices
		/// </summary>
		/// <returns>A list of devices</returns>
		public static List<string> GetAllDevices()
		{
			String dName = "".PadRight(100);
			String dVersion = "".PadRight(100);

			List<string> list = new List<string>();

			for (short i = 0; i < 10; i++)
			{
				if (!capGetDriverDescriptionA(i, ref dName, 100, ref dVersion, 100))
					break;

				list.Add(dName.Split(new char[]{'\0'})[0]);
			}

			return list;
		}




		/*
				private void OpenCapture()
				{

					int intWidth = 200;
					int intHeight = 200;
					int intDevice = 0;
					string refDevice = intDevice.ToString();
					int hHwnd = capCreateCaptureWindowA(ref   refDevice, 1342177280, 0, 0, 640, 480, 0, 0);
					//      this.LbSysMsg.Text = ""; 
					//      this.LbSysMsg.Text += "é©±åŠ¨:" + refDevice; 
					if (Form1.SendMessage(hHwnd, 0x40a, intDevice, 0) > 0)
					{
						Form1.SendMessage(this.hHwnd, 0x435, -1, 0);
						Form1.SendMessage(this.hHwnd, 0x434, 0x42, 0);
						Form1.SendMessage(this.hHwnd, 0x432, -1, 0);
						Form1.SetWindowPos(this.hHwnd, 1, 0, 0, intWidth, intHeight, 6);
						//        this.BtnCapTure.Enabled = false; 
						//        this.BtnStop.Enabled = true; 
					}
					else
					{
						Form1.DestroyWindow(this.hHwnd);
						//      this.BtnCapTure.Enabled = false; 
						//     this.BtnStop.Enabled = true; 
					}
				} 
		*/

		#region Properties


		/// <summary>
		/// ID of the device
		/// </summary>
		public int ID
		{
			get;
			private set;
		}


		/// <summary>
		/// Size of the video
		/// </summary>
		public Size Size
		{
			get;
			private set;
		}


		#endregion


		#region PInvoke

		private const short WM_CAP = 0x400;
		private const int WM_CAP_DRIVER_CONNECT = 0x40a;
		private const int WM_CAP_DRIVER_DISCONNECT = 0x40b;
		private const int WM_CAP_EDIT_COPY = 0x41e;
		private const int WM_CAP_SET_PREVIEW = 0x432;
		private const int WM_CAP_SET_OVERLAY = 0x433;
		private const int WM_CAP_SET_PREVIEWRATE = 0x434;
		private const int WM_CAP_SET_SCALE = 0x435;
		private const int WS_CHILD = 0x40000000;
		private const int WS_VISIBLE = 0x10000000;
		
		
		
		/// <summary>
		/// 
		/// </summary>
		internal struct videohdr_tag
		{
			public byte[] lpData;
			public int dwBufferLength;
			public int dwBytesUsed;
			public int dwTimeCaptured;
			public int dwUser;
			public int dwFlags;
			public int[] dwReserved;

		} 

		//private System.ComponentModel.Container components = null; 
		[DllImport("avicap32.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		static extern int capCreateCaptureWindowA([MarshalAs(UnmanagedType.VBByRefStr)]   ref   string lpszWindowName, int dwStyle, int x, int y, int nWidth, short nHeight, int hWndParent, int nID);
		[DllImport("avicap32.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		static extern bool capGetDriverDescriptionA(short wDriver, [MarshalAs(UnmanagedType.VBByRefStr)]   ref   string lpszName, int cbName, [MarshalAs(UnmanagedType.VBByRefStr)]   ref   string lpszVer, int cbVer);
		[DllImport("user32", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		static extern bool DestroyWindow(int hndw);
		[DllImport("user32", EntryPoint = "SendMessageA", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		static extern int SendMessage(int hwnd, int wMsg, int wParam, [MarshalAs(UnmanagedType.AsAny)]   object lParam);
		[DllImport("user32", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		static extern int SetWindowPos(int hwnd, int hWndInsertAfter, int x, int y, int cx, int cy, int wFlags);
		[DllImport("vfw32.dll")]
		static extern string capVideoStreamCallback(int hwnd, videohdr_tag videohdr_tag);
		[DllImport("vicap32.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		static extern bool capSetCallbackOnFrame(int hwnd, string s);

		#endregion
	}
}
