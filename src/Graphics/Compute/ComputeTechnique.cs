#region License
/* FNA - XNA4 Reimplementation for Desktop Platforms
 * Copyright 2009-2022 Ethan Lee and the MonoGame Team
 *
 * Released under the Microsoft Public License.
 * See LICENSE for details.
 */
#endregion

#region Using Statements
using System;
#endregion

namespace Microsoft.Xna.Framework.Graphics
{
	public sealed class ComputeTechnique
	{
		#region Public Properties
		#endregion

		#region Internal Properties
		internal ComputeShader parentEffect;
		internal IntPtr techniquePointer;

		#endregion

		#region Internal Constructor

		internal ComputeTechnique(
			ComputeShader parentEffect,
			IntPtr technique
		) {
			this.parentEffect = parentEffect;
			techniquePointer = technique;
		}

		#endregion


		public ComputePass GetPassByName(string name)
		{
			IntPtr pass = FNA3D.FX11_Effect_Technique_GetPassByName(techniquePointer, name);
			return new ComputePass(parentEffect, techniquePointer, pass);
		}
	}
}
