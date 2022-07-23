using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Xna.Framework.Graphics
{
	//
	// 摘要:
	//     Intended usage of the buffer.
	public enum ComputeBufferMode
	{
		//
		// 摘要:
		//     Static buffer, only initial upload allowed by the CPU
		Immutable,
		//
		// 摘要:
		//     Dynamic buffer.
		Dynamic,
		//
		// 摘要:
		//     Dynamic, unsynchronized access to the buffer.
		SubUpdates
	}
}
