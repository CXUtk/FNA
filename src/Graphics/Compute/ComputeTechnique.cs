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
using System.Collections.Generic;
#endregion

namespace Microsoft.Xna.Framework.Graphics
{
	public sealed class ComputeTechnique
	{
		#region Public Properties
		public string Name { get; private set; }
		public ComputePassCollection Passes { get; private set; }
		#endregion

		#region Internal Properties
		internal ComputeShader parentEffect;
		internal IntPtr techniquePointer;
		#endregion

		#region Internal Constructor

		internal ComputeTechnique(
			string name,
			ComputeShader parentEffect,
			IntPtr technique
		) {
			Name = name;
			this.parentEffect = parentEffect;
			techniquePointer = technique;
		}

		#endregion

		internal void SetPasses(List<ComputePass> passes)
		{
			Passes = new ComputePassCollection(passes);
		}

	}
}
