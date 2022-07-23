using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Microsoft.Xna.Framework.Graphics
{
	public class ComputeBuffer : GraphicsResource
	{
		public int Count
		{
			get;
			protected set;
		}

		public int Stride
		{
			get;
			protected set;
		}

		internal IntPtr buffer;

		public ComputeBuffer(GraphicsDevice graphicsDevice,
			int count, int stride) : this(graphicsDevice, count, stride, ComputeBufferType.Default, ComputeBufferMode.Immutable)
		{

		}
		public ComputeBuffer(GraphicsDevice graphicsDevice,
			int count, int stride, ComputeBufferType type, ComputeBufferMode mode)
		{
			if (graphicsDevice == null)
			{
				throw new ArgumentNullException("graphicsDevice");
			}

			GraphicsDevice = graphicsDevice;
			Count = count;
			Stride = stride;

			buffer = FNA3D.FNA3D_GenComputeBuffer(GraphicsDevice.GLDevice,
				(byte)((mode == ComputeBufferMode.Dynamic) ? 1 : 0),
				type,
				BufferUsage.None,
				count,
				stride);
		}
	}
}
