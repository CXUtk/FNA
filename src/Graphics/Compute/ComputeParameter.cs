using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Xna.Framework.Graphics
{
	public class ComputeParameter
	{
		internal IntPtr variablePtr;
		internal ComputeShader parentEffect;

		public ComputeParameter(ComputeShader effect, IntPtr variablePtr)
		{
			this.parentEffect = effect;
			this.variablePtr = variablePtr;
		}

		public void SetBuffer(ComputeBuffer buffer)
		{
			FNA3D.FX11_Effect_Variable_SetUnorderedAccessView_ComputeBuffer(variablePtr, buffer.buffer);
		}
	}
}
