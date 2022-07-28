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

		public ComputeTechnique GetTechnique(string name)
		{
			IntPtr technique = FNA3D.FX11_Effect_GetTechniqueByName(glComputeShader, name);
			return new ComputeTechnique(this, technique);
		}

		public ComputeParameter GetParameter(string name)
		{
			IntPtr variable = FNA3D.FX11_Effect_GetVariableByName(glComputeShader, name);
			return new ComputeParameter(this, variable);
		}
	}
}
