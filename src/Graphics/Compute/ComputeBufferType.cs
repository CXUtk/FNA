using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Xna.Framework.Graphics
{
	//
	// 摘要:
	//     ComputeBuffer type.
	[Flags]
	public enum ComputeBufferType
	{
		//
		// 摘要:
		//     Default ComputeBuffer type (structured buffer).
		Default = 0x0,
		//
		// 摘要:
		//     Raw ComputeBuffer type (byte address buffer).
		Raw = 0x1,
		//
		// 摘要:
		//     Append-consume ComputeBuffer type.
		Append = 0x2,
		//
		// 摘要:
		//     ComputeBuffer with a counter.
		Counter = 0x4
	}
}
