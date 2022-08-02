using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Xna.Framework.Graphics
{
	public class ComputePassCollection : IEnumerable<ComputePass>, IEnumerable
	{
		#region Public Properties

		public int Count
		{
			get
			{
				return elements.Count;
			}
		}

		public ComputePass this[int index]
		{
			get
			{
				return elements[index];
			}
		}

		public ComputePass this[string name]
		{
			get
			{
				foreach (ComputePass elem in elements)
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

		private List<ComputePass> elements;

		#endregion

		#region Internal Constructor

		internal ComputePassCollection(List<ComputePass> value)
		{
			elements = value;
		}

		#endregion

		#region Public Methods

		public List<ComputePass>.Enumerator GetEnumerator()
		{
			return elements.GetEnumerator();
		}

		#endregion

		#region IEnumerator Methods

		IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return elements.GetEnumerator();
		}

		IEnumerator<ComputePass> System.Collections.Generic.IEnumerable<ComputePass>.GetEnumerator()
		{
			return elements.GetEnumerator();
		}

		#endregion
	}
}
