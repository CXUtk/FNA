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

		public ComputeShader(GraphicsDevice graphicsDevice, byte[] shaderCode, string entryPoint)
		{
			GraphicsDevice = graphicsDevice;

			FNA3D.FNA3D_CreateComputeShader(
				graphicsDevice.GLDevice,
				shaderCode,
				shaderCode.Length,
				entryPoint,
				"cs_5_0",
				out glComputeShader
			);
		}

		public void SetBuffer(string name, ComputeBuffer buffer)
		{
			
		}
	}
}
