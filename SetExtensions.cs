using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mathematical_Sets
{
	public static class SetExtensions
	{
		/// <summary>
		/// Returns the given array as a set.
		/// </summary>
		public static Set ToSet(this object[] array)
		{
			Set set = new Set();
			set.Add(array);
			return set;
		}

		/// <summary>
		/// Returns the given array as a set.
		/// </summary>
		public static Set ToSet<T>(this T[] array)
		{
			return ToSet(array);
		}
	}
}
