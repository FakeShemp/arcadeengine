using System.Runtime.InteropServices;
using System.Windows.Forms;

//
// http://www.codeproject.com/KB/system/enum_display_modes.aspx
//
//
//
//
namespace ArcEngine.Forms
{
	/// <summary>
	/// 
	/// </summary>
	public partial class DisplaySettingsWizard : Form
	{
		/// <summary>
		/// 
		/// </summary>
		public DisplaySettingsWizard()
		{
			InitializeComponent();
		}



		/// <summary>
		///  Update all controls on the form
		/// </summary>
		void UpdateControls()
		{
			GfxBox.Items.Clear();



			ResolutionBox.Items.Clear();



			DisplayBox.Items.Clear();
		}





		class User_32
		{
			[DllImport("user32.dll")]
			public static extern int EnumDisplaySettings(string deviceName, int modeNum, ref DEVMODE1 devMode);

			[DllImport("user32.dll")]
			public static extern int ChangeDisplaySettings(ref DEVMODE1 devMode, int flags);

			public const int ENUM_CURRENT_SETTINGS = -1;
			public const int CDS_UPDATEREGISTRY = 0x01;
			public const int CDS_TEST = 0x02;
			public const int CDS_FULLSCREEN = 0x04;
			public const int DISP_CHANGE_SUCCESSFUL = 0;
			public const int DISP_CHANGE_RESTART = 1;
			public const int DISP_CHANGE_FAILED = -1;
		}

		[StructLayout(LayoutKind.Sequential)]
		internal struct DEVMODE1
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
			public string dmDeviceName;
			public short dmSpecVersion;
			public short dmDriverVersion;
			public short dmSize;
			public short dmDriverExtra;
			public int dmFields;

			public short dmOrientation;
			public short dmPaperSize;
			public short dmPaperLength;
			public short dmPaperWidth;

			public short dmScale;
			public short dmCopies;
			public short dmDefaultSource;
			public short dmPrintQuality;
			public short dmColor;
			public short dmDuplex;
			public short dmYResolution;
			public short dmTTOption;
			public short dmCollate;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
			public string dmFormName;
			public short dmLogPixels;
			public short dmBitsPerPel;
			public int dmPelsWidth;
			public int dmPelsHeight;

			public int dmDisplayFlags;
			public int dmDisplayFrequency;

			public int dmICMMethod;
			public int dmICMIntent;
			public int dmMediaType;
			public int dmDitherType;
			public int dmReserved1;
			public int dmReserved2;

			public int dmPanningWidth;
			public int dmPanningHeight;
		};
	}
}
/*using System;
using System.Collections.Generic;
using System.ComponentModel;

using System.Drawing;

using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace WindowsFormsApplication1
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();

			GameScreen screen = GameScreen.PrimaryScreen;
			
			
			int iWidth = 1024;
			int iHeight = 768;
			

			DEVMODE1 dm = new DEVMODE1();
			dm.dmDeviceName = new String (new char[32]);
			dm.dmFormName = new String (new char[32]);
			dm.dmSize = (short)Marshal.SizeOf(dm);

			if (0 != User_32.EnumDisplaySettings (null, User_32.ENUM_CURRENT_SETTINGS, ref dm))
			{
				
				dm.dmPelsWidth = iWidth;
				dm.dmPelsHeight = iHeight;

				int iRet = User_32.ChangeDisplaySettings(ref dm, User_32.CDS_FULLSCREEN);

				if (iRet == User_32.DISP_CHANGE_FAILED)
				{
					MessageBox.Show("Unable to process your request");
					MessageBox.Show("Description: Unable To Process Your Request. Sorry For This Inconvenience.","Information",MessageBoxButtons.OK,MessageBoxIcon.Information);
				}


				System.Threading.Thread.Sleep(1000 * 4);
			}
		}





		private void Form1_KeyDown(object sender, KeyEventArgs e)
		{
			Application.Exit();
		}

	}
}
*/