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
				return techniquesList[index];
			}
		}

		public ComputeTechnique this[string name]
		{
			get
			{
				if (elements.TryGetValue(name, out var value))
				{
					return value;
				}
				throw new ArgumentException($"Parameter '{name}' does not exist");
			}
		}

		#endregion

		#region Private Variables

		private List<ComputeTechnique> techniquesList;
		private Dictionary<string, ComputeTechnique> elements;

		#endregion

		#region Internal Constructor

		internal ComputeTechniqueCollection(List<ComputeTechnique> value)
		{
			techniquesList = value;
			elements = new Dictionary<string, ComputeTechnique>();
			foreach (ComputeTechnique elem in value)
			{
				elements.Add(elem.Name, elem);
			}
		}

		#endregion

		#region Public Methods

		public List<ComputeTechnique>.Enumerator GetEnumerator()
		{
			return elements.Values.ToList().GetEnumerator();
		}

		#endregion

		#region IEnumerator Methods

		IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return elements.Values.ToList().GetEnumerator();
		}

		IEnumerator<ComputeTechnique> System.Collections.Generic.IEnumerable<ComputeTechnique>.GetEnumerator()
		{
			return elements.Values.ToList().GetEnumerator();
		}
		#endregion
	}
}
