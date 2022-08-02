using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Xna.Framework.Graphics
{
	public class ComputeTechniqueCollection : IEnumerable<ComputeTechnique>, IEnumerable
	{
		#region Public Properties

		public int Count
		{
			get
			{
				return elements.Count;
			}
		}

		public ComputeTechnique this[int index]
		{
			get
			{
				return elements[index];
			}
		}

		public ComputeTechnique this[string name]
		{
			get
			{
				foreach (ComputeTechnique elem in elements)
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

		private List<ComputeTechnique> elements;

		#endregion

		#region Internal Constructor

		internal ComputeTechniqueCollection(List<ComputeTechnique> value)
		{
			elements = value;
		}

		#endregion

		#region Public Methods

		public List<ComputeTechnique>.Enumerator GetEnumerator()
		{
			return elements.GetEnumerator();
		}

		#endregion

		#region IEnumerator Methods

		IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return elements.GetEnumerator();
		}

		IEnumerator<ComputeTechnique> System.Collections.Generic.IEnumerable<ComputeTechnique>.GetEnumerator()
		{
			return elements.GetEnumerator();
		}

		#endregion
	}
}
