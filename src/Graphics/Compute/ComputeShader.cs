using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Xna.Framework.Graphics
{
	public class ComputeShader : GraphicsResource
	{
		internal IntPtr glComputeShader;

		public ComputeShader(GraphicsDevice graphicsDevice, byte[] effectBinaryCode, uint effectCodeLength)
		{
			GraphicsDevice = graphicsDevice;

			FNA3D.FNA3D_CreateNewEffect(
				graphicsDevice.GLDevice,
				effectBinaryCode,
				effectCodeLength,
				out glComputeShader
			);
		}

		public void SetBuffer(string name, ComputeBuffer buffer)
		{
			
		}
	}
}
