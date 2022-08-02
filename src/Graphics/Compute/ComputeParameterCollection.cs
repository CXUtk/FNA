#region License
/* FNA - XNA4 Reimplementation for Desktop Platforms
 * Copyright 2009-2022 Ethan Lee and the MonoGame Team
 *
 * Released under the Microsoft Public License.
 * See LICENSE for details.
 */
#endregion

#region Using Statements
using System.Collections;
using System.Collections.Generic;
#endregion

namespace Microsoft.Xna.Framework.Graphics
{
	public sealed class ComputeParameterCollection : IEnumerable<ComputeParameter>, IEnumerable
	{
		#region Public Properties

		public int Count
		{
			get
			{
				return elements.Count;
			}
		}

		public ComputeParameter this[int index]
		{
			get
			{
				return elements[index];
			}
		}

		public ComputeParameter this[string name]
		{
			get
			{
				foreach (ComputeParameter elem in elements)
				{
					if (name.Equals(elem.Name))
					{
						return elem;
					}
				}
				return null; // FIXME: ArrayIndexOutOfBounds? -flibit
			}
		}

		#endregion

		#region Private Variables

		private List<ComputeParameter> elements;

		#endregion

		#region Internal Constructor

		internal ComputeParameterCollection(List<ComputeParameter> value)
		{
			elements = value;
		}

		#endregion

		#region Public Methods

		public List<ComputeParameter>.Enumerator GetEnumerator()
		{
			return elements.GetEnumerator();
		}

		//public ComputeParameter GetParameterBySemantic(string semantic)
		//{
		//	foreach (ComputeParameter elem in elements)
		//	{
		//		if (semantic.Equals(elem.Semantic))
		//		{
		//			return elem;
		//		}
		//	}
		//	return null;
		//}

		#endregion

		#region IEnumerator Methods

		IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return elements.GetEnumerator();
		}

		IEnumerator<ComputeParameter> System.Collections.Generic.IEnumerable<ComputeParameter>.GetEnumerator()
		{
			return elements.GetEnumerator();
		}

		#endregion
	}
}
