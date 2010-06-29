using System;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using ArcEngine.Graphic;

namespace ArcEngine.Graphic
{
	/// <summary>
	/// Defines the render state of a graphics device. 
	/// </summary>
	public sealed class RenderState
	{

		/// <summary>
		/// Captures the current value of states that are included in a state block.
		/// </summary>
		/// <returns>State block</returns>
		public StateBlock Capture()
		{
			StateBlock state = new StateBlock();
			state.Blending = Blending;
			state.ClearColor = ClearColor;
			state.Culling = Culling;
			state.DepthClearValue = DepthClearValue;
			state.DepthMask = DepthMask;
			state.DepthTest = DepthTest;
			state.MultiSample = MultiSample;
			state.PointSize = PointSize;
			state.Scissor = Scissor;
			state.StencilClearValue = StencilClearValue;
			state.StencilFail = StencilFail;
			state.StencilFunction = StencilFunction;
			state.StencilMask = StencilMask;
			state.StencilPass = StencilPass;
			state.StencilReference = StencilReference;
			state.StencilTest = StencilTest;
			state.StencilWriteMask = StencilWriteMask;
			state.TwoSidedStencilMode = TwoSidedStencilMode;

			return state;
		}


		/// <summary>
		/// Applies the state block 
		/// </summary>
		/// <param name="state">Stateblock to apply</param>
		public void Apply(StateBlock state)
		{
			Blending = state.Blending;
			ClearColor = state.ClearColor;
			Culling = state.Culling;
			DepthClearValue = state.DepthClearValue;
			DepthMask = state.DepthMask;
			DepthTest = state.DepthTest;
			MultiSample = state.MultiSample;
			PointSize = state.PointSize;
			Scissor = state.Scissor;
			StencilClearValue = state.StencilClearValue;
			StencilFail = state.StencilFail;
			StencilFunction = state.StencilFunction;
			StencilMask = state.StencilMask;
			StencilPass = state.StencilPass;
			StencilReference = state.StencilReference;
			StencilTest = state.StencilTest;
			StencilWriteMask = state.StencilWriteMask;
			TwoSidedStencilMode = state.TwoSidedStencilMode;	
		}

		#region Properties

		/// <summary>
		/// Gets/sets blending state
		/// </summary>
		public bool AlphaTest
		{
			get
			{
				return GL.IsEnabled(EnableCap.AlphaTest);
			}
			set
			{
				if (value)
					GL.Enable(EnableCap.AlphaTest);
				else
					GL.Disable(EnableCap.AlphaTest);
			}
		}


		/// <summary>
		/// Gets/Sets the scissor test
		/// </summary>
		public bool Scissor
		{
			get
			{
				return GL.IsEnabled(EnableCap.ScissorTest);

			}
			set
			{
				if (value)
					GL.Enable(EnableCap.ScissorTest);
				else
					GL.Disable(EnableCap.ScissorTest);
			}
		}


		/// <summary>
		/// Gets/Sets the stipple pattern
		/// </summary>
		[Obsolete("Deprecated")]
		public bool LineStipple
		{
			get
			{
				return GL.IsEnabled(EnableCap.LineStipple);

			}
			set
			{
				if (value)
					GL.Enable(EnableCap.LineStipple);
				else
					GL.Disable(EnableCap.LineStipple);
			}
		}


		/// <summary>
		/// Gets / sets the point size
		/// </summary>
		public int PointSize
		{
			get
			{
				int value;
				GL.GetInteger(GetPName.PointSize, out value);
				return value;
			}
			set
			{
				GL.PointSize(value);
			}
		}


		/// <summary>
		/// Gets / sets the line size
		/// </summary>
		[Obsolete("Deprecated")]
		public int LineWidth
		{
			get
			{
				int value;
				GL.GetInteger(GetPName.LineWidth, out value);
				return value;
			}
			set
			{
				GL.LineWidth(value);
			}
		}



		/// <summary>
		/// Line anti aliasing
		/// </summary>
		[Obsolete("Deprecated")]
		public bool LineSmooth
		{
			get
			{
				return GL.IsEnabled(EnableCap.LineSmooth);
			}
			set
			{
				if (value)
					GL.Enable(EnableCap.LineSmooth);
				else
					GL.Disable(EnableCap.LineSmooth);
			}
		}


		/// <summary>
		/// Point smooth
		/// </summary>
		[Obsolete("Deprecated")]
		public bool PointSmooth
		{
			get
			{
				return GL.IsEnabled(EnableCap.PointSmooth);
			}
			set
			{
				if (value)
					GL.Enable(EnableCap.PointSmooth);
				else
					GL.Disable(EnableCap.PointSmooth);
			}
		}



		/// <summary>
		/// FSAA
		/// </summary>
		public bool MultiSample
		{
			get
			{
				return GL.IsEnabled(EnableCap.Multisample);
			}
			set
			{
				if (value)
					GL.Enable(EnableCap.Multisample);
				else
					GL.Disable(EnableCap.Multisample);
			}
		}


		/// <summary>
		/// Gets/sets the cleacolor
		/// </summary>
		public Color ClearColor
		{
			get
			{
				float[] tab = new float[4];
				GL.GetFloat(GetPName.ColorClearValue, tab);

				return Color.FromArgb((int)(tab[3] * 255), (int)(tab[0] * 255), (int)(tab[1] * 255), (int)(tab[2] * 255));
			}
			set
			{
				GL.ClearColor(value.R / 255.0f, value.G / 255.0f, value.B / 255.0f, value.A / 255.0f);
			}
		}

		/// <summary>
		/// Enables/disables face culling
		/// </summary>
		public bool Culling
		{
			get
			{
				return GL.IsEnabled(EnableCap.CullFace);
			}
			set
			{
				if (value)
					GL.Enable(EnableCap.CullFace);
				else
					GL.Disable(EnableCap.CullFace);
			}
		}


		/// <summary>
		/// Enables/disables depth test
		/// </summary>
		public bool DepthTest
		{
			get
			{
				return GL.IsEnabled(EnableCap.DepthTest);
			}

			set
			{
				if (value)
					GL.Enable(EnableCap.DepthTest);
				else
					GL.Disable(EnableCap.DepthTest);
			}
		}


		/// <summary>
		/// Gets/sets clear value for the depth buffer 
		/// </summary>
		public float DepthClearValue
		{
			get
			{
				int s;
				GL.GetInteger(GetPName.DepthClearValue, out s);
				return s;
			}
			set
			{
				GL.ClearDepth(value);
			}
		}


		/// <summary>
		/// Enable or disable writing into the depth buffer
		/// </summary>
		public bool DepthMask
		{
			get
			{
				bool ret;
				GL.GetBoolean(GetPName.DepthWritemask, out ret);

				return ret;
			}

			set
			{
				GL.DepthMask(value);
			}
		}


		/// <summary>
		/// Gets/sets blending state
		/// </summary>
		public bool Blending
		{
			get
			{
				return GL.IsEnabled(EnableCap.Blend);
			}
			set
			{
				if (value)
					GL.Enable(EnableCap.Blend);
				else
					GL.Disable(EnableCap.Blend);
			}
		}

	
		#region Stencil


		/// <summary>
		/// Gets or sets stencil enabling. The default is false.
		/// </summary>
		public bool StencilTest
		{
			get
			{
				return GL.IsEnabled(EnableCap.StencilTest);
			}

			set
			{
				if (value)
					GL.Enable(EnableCap.StencilTest);
				else
					GL.Disable(EnableCap.StencilTest);
			}
		}


		/// <summary>
		/// Gets/sets clear value for the stencil buffer 
		/// </summary>
		public int StencilClearValue
		{
			get
			{
				int s;
				GL.GetInteger(GetPName.StencilClearValue, out s);
				return s;
			}
			set
			{
				GL.ClearStencil(value);
			}
		}

		/// <summary>
		/// Gets or sets the mask applied to the reference value and each stencil
		/// buffer entry to determine the significant bits for the stencil test. 
		/// </summary>
		public int StencilMask
		{
			get
			{
				int mask;
				GL.GetInteger(GetPName.StencilValueMask, out mask);
				return mask;
			}
			set
			{
				GL.StencilMask(value);
			}
		}


		/// <summary>
		/// Gets or sets the stencil operation to perform if the stencil test passes.
		/// </summary>
		public StencilOperation StencilPass
		{
			get
			{
				return StencilOperation.Keep;
			}
			set
			{
			}
		}


		/// <summary>
		/// Gets or sets the write mask applied to values written into the stencil buffer.
		/// </summary>
		public int StencilWriteMask
		{
			get
			{
				int mask;
				GL.GetInteger(GetPName.StencilWritemask, out mask);
				return mask;
			}
			set
			{

			}
		}


		/// <summary>
		/// Enables or disables two-sided stenciling. 
		/// </summary>
		public bool TwoSidedStencilMode
		{
			get
			{
				return false;
			}
			set
			{
			}
		}


		/// <summary>
		/// Specifies a reference value to use for the stencil test.
		/// </summary>
		public int StencilReference
		{
			get
			{
				int mask;
				GL.GetInteger(GetPName.StencilRef, out mask);
				return mask;
			}
			set
			{
			}
		}

		/*
				/// <summary>
				/// Gets or sets the stencil operation to perform if the stencil and 
				/// z-tests pass for a counterclockwise triangle.
				/// </summary>
				public StencilOperation CounterClockwiseStencilPass 
				{
					get
					{
					}
					set
					{
					}
				}


				/// <summary>
				/// Gets or sets the comparison function to use for counterclockwise stencil tests. 
				/// </summary>
				public CompareFunction CounterClockwiseStencilFunction 
				{
					get
					{
					}
					set
					{
					}
				}


				/// <summary>
				/// Gets or sets the stencil operation to perform if the stencil 
				/// test fails for a counterclockwise triangle. 
				/// </summary>
				public StencilOperation CounterClockwiseStencilFail 
				{
					get
					{
					}
					set
					{
					}
				}


				/// <summary>
				/// Gets or sets the stencil operation to perform if the stencil test passes
				/// and the depth-buffer test fails for a counterclockwise triangle.
				/// </summary>
				public StencilOperation CounterClockwiseStencilDepthBufferFail 
				{
					get
					{
					}
					set
					{
					}
				}
		*/

		/// <summary>
		/// Gets or sets the comparison function for the stencil test. 
		/// </summary>
		public CompareFunction StencilFunction
		{
			get
			{
				return CompareFunction.Never;
			}
			set
			{
				GL.StencilFunc((OpenTK.Graphics.OpenGL.StencilFunction)value, StencilReference, StencilMask);
			}
		}


		/// <summary>
		/// Gets or sets the stencil operation to perform if the stencil test fails. 
		/// </summary>
		public StencilOperation StencilFail
		{
			get
			{
				return StencilOperation.Keep;
			}
			set
			{
			}
		}




		#endregion

		#endregion

	}



	/// <summary>
	/// 
	/// </summary>
	public struct StateBlock
	{

		/// <summary>
		/// Gets/sets blending state
		/// </summary>
		bool AlphaTest;


		/// <summary>
		/// Gets/Sets the scissor test
		/// </summary>
		public bool Scissor;


		/// <summary>
		/// Gets / sets the point size
		/// </summary>
		public int PointSize;


		/// <summary>
		/// FSAA
		/// </summary>
		public bool MultiSample;


		/// <summary>
		/// Gets/sets the cleacolor
		/// </summary>
		public Color ClearColor;

		/// <summary>
		/// Enables/disables face culling
		/// </summary>
		public bool Culling;


		/// <summary>
		/// Enables/disables depth test
		/// </summary>
		public bool DepthTest;


		/// <summary>
		/// Gets/sets clear value for the depth buffer 
		/// </summary>
		public float DepthClearValue;


		/// <summary>
		/// Enable or disable writing into the depth buffer
		/// </summary>
		public bool DepthMask;


		/// <summary>
		/// Gets/sets blending state
		/// </summary>
		public bool Blending;

	
		#region Stencil


		/// <summary>
		/// Gets or sets stencil enabling. The default is false.
		/// </summary>
		public bool StencilTest;


		/// <summary>
		/// Gets/sets clear value for the stencil buffer 
		/// </summary>
		public int StencilClearValue;

		/// <summary>
		/// Gets or sets the mask applied to the reference value and each stencil
		/// buffer entry to determine the significant bits for the stencil test. 
		/// </summary>
		public int StencilMask;


		/// <summary>
		/// Gets or sets the stencil operation to perform if the stencil test passes.
		/// </summary>
		public StencilOperation StencilPass;


		/// <summary>
		/// Gets or sets the write mask applied to values written into the stencil buffer.
		/// </summary>
		public int StencilWriteMask;


		/// <summary>
		/// Enables or disables two-sided stenciling. 
		/// </summary>
		public bool TwoSidedStencilMode;


		/// <summary>
		/// Specifies a reference value to use for the stencil test.
		/// </summary>
		public int StencilReference;


		/// <summary>
		/// Gets or sets the comparison function for the stencil test. 
		/// </summary>
		public CompareFunction StencilFunction;


		/// <summary>
		/// Gets or sets the stencil operation to perform if the stencil test fails. 
		/// </summary>
		public StencilOperation StencilFail;

		#endregion
	}

}
