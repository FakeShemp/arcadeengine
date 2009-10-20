using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics;


namespace ArcEngine.Forms
{
	/// <summary>
	/// Personal GLControl control
	/// </summary>
	class RenderControl : GLControl
	{
		/// <summary>
		/// Create a new GLControl with a 8b stencil buffer
		/// </summary>
		public RenderControl()
			: base(new GraphicsMode(new ColorFormat(32), 24, 8))
		{
		}
	}
}
