using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Xna.Framework.Graphics
{
	public class ComputePass
	{
		public string Name { get; private set; }
		private ComputeShader parentEffect;
		internal ComputeTechnique parentTechnique;
		internal IntPtr pass;

		internal ComputePass(
			string name,
			ComputeShader effect,
			ComputeTechnique technique,
			IntPtr pass
		)
		{
			Name = name;
			this.parentEffect = effect;
			this.parentTechnique = technique;
			this.pass = pass;
		}

		public void Apply()
		{
			FNA3D.FX11_Effect_Pass_Apply(parentEffect.GraphicsDevice.GLDevice, this.pass);
		}
	}
}
