using System.Collections;
using System.Linq;

namespace Mathematical_Sets
{
	/// <summary>
	/// Implements a mathematical set - elements are not contained with multiplicity. There is no order (i.e. indexing) to the set.
	/// </summary>

	public class Set<T> : IEnumerable<T>
	{
		private List<T> underlyingList = new List<T>();

		/// <summary>
		/// Add an item to the set.
		/// </summary>
		/// <param name="items">The item to add to the set.</param>
		public void Add(T item)
		{
			if (underlyingList.Contains(item) == false) //If the list already contains it, don't add, since sets should only contain stuff once.
			{
				underlyingList.Add(item);
			}
		}

		/// <summary>
		/// Add items to the set.
		/// </summary>
		/// <param name="items">The items to add to the set.</param>
		public void Add(T[] items)
		{
			foreach (T item in items)
			{
				Add(item);
			}
		}

		/// <summary>
		/// Add items to the set.
		/// </summary>
		/// <param name="items">The items to add to the set.</param>
		public void Add(List<T> items)
		{
			Add(items.ToArray());
		}

		/// <summary>
		/// Add items to the set.
		/// </summary>
		/// <param name="items">The items to add to the set.</param>
		public void Add(Set<T> items)
		{
			Add(items.ToArray());
		}

		/// <summary>
		/// Determines whether an element is in the <c>Set<T></c>.
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		public bool Contains(T item)
		{
			return underlyingList.Contains(item);
		}

		/// <summary>
		/// Remove an item from the set.
		/// </summary>
		/// <param name="item">The items to remove from the set.</param>
		public void Remove(T item)
		{
			while (underlyingList.Contains(item))
			{
				underlyingList.Remove(item);
			}

		}

		/// <summary>
		/// Remove items from the set.
		/// </summary>
		/// <param name="items">The items to remove from the set.</param>
		public void Remove(T[] items)
		{
			foreach (T item in items)
			{
				Remove(item);
			}

		}

		/// <summary>
		/// Remove items from the set.
		/// </summary>
		/// <param name="items">The items to remove from the set.</param>
		public void Remove(List<T> items)
		{
			Remove(items);
		}

		/// <summary>
		/// Remove items from the set.
		/// </summary>
		/// <param name="items">The items to remove from the set.</param>
		public void Remove(Set<T> items)
		{
			Remove(items);
		}


		/// <summary>
		/// Returns the cardinality, that is the size, of the set.
		/// </summary>
		public int Cardinality
		{
			get
			{
				return underlyingList.Count;
			}
		}

		/// <summary>
		/// Returns the size, that is the cardinality, of the set.
		/// </summary>
		public int Size
		{
			get
			{
				return Cardinality;
			}
		}

		public override string ToString()
		{
			return "Set<" + typeof(T).Name + ">";
		}

		#region Print out contents
		/// <summary>
		/// Print the contents of the set.
		/// </summary>
		/// <example>
		/// The following example demonstrates how to use the PrintSet() method:
		/// <code>
		/// Set<int> mySet = new Set<int>();
		/// mySet.Add(1);
		/// mySet.Add(2);
		/// mySet.Add(3);
		/// string setContents = mySet.PrintSet();
		/// Console.WriteLine(setContents);
		/// </code>
		/// The output will be:
		/// <code>
		/// {1, 2, 3}
		/// </code>
		/// </example>
		public string PrintSet()
		{
			string str = "{";
			foreach (T item in this)
			{
				str += item.ToString() + ", ";
			}

			str = str.Substring(0, str.Length - 2);
			str += "}";

			return str;
		}

		/// <summary>
		/// Print the contents of the set.
		/// </summary>
		/// <example>
		/// The following example demonstrates how to use the PrintSet() method:
		/// <code>
		/// Set<int> mySet = new Set<int>();
		/// mySet.Add(1);
		/// mySet.Add(2);
		/// mySet.Add(3);
		/// string setContents = mySet.PrintSet();
		/// Console.WriteLine(setContents);
		/// </code>
		/// The output will be:
		/// <code>
		/// {1, 2, 3}
		/// </code>
		/// </example>
		public string ToFormattedString()
		{
			return PrintSet();
		}
		#endregion

		#region Enumeration
		public IEnumerator<T> GetEnumerator()
		{
			return ((IEnumerable<T>)underlyingList).GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable)underlyingList).GetEnumerator();
		}
		#endregion

		#region Subsets
		/// <summary>
		/// Returns whether this set is a subset of the given parameter.
		/// </summary>
		public bool IsSubsetOf(Set<T> otherSet)
		{
			foreach (T item in this)
			{
				if (otherSet.Contains(item) == false)
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>
		/// Less than operator is used as proper subset.
		/// </summary>
		public static bool operator <(Set<T> x, Set<T> y)
		{
			return x.IsProperSubsetOf(y);
		}

		/// <summary>
		/// Less than operator is used as proper subset.
		/// </summary>
		public static bool operator >(Set<T> x, Set<T> y)
		{
			return y < x;
		}

		/// <summary>
		/// Less than or equal to operator is used as subset.
		/// </summary>
		public static bool operator <=(Set<T> x, Set<T> y)
		{
			return x.IsEqualTo(y);
		}

		/// <summary>
		/// Less than or equal to operator is used as subset.
		/// </summary>
		public static bool operator >=(Set<T> x, Set<T> y)
		{
			return y <= x;
		}


		/// <summary>
		/// Returns whether this set is a proper subset of the given parameter.
		/// </summary>
		public bool IsProperSubsetOf(Set<T> otherSet)
		{
			return (otherSet.Size > this.Size) && (this.IsSubsetOf(otherSet));
		}
		#endregion

		#region Equality
		/// <summary>
		/// Returns whether this set is equal to another set.
		/// </summary>
		private bool IsEqualTo(Set<T> otherSet)
		{
			return IsSubsetOf(otherSet) && otherSet.Size == this.Size;
		}

		public override bool Equals(object? obj)
		{
			if (obj is Set<T>)
			{
				return this.IsEqualTo((Set<T>)obj);
			}
			return false;
		}

		public override int GetHashCode()
		{
			return underlyingList.GetHashCode();
		}

		public static bool operator ==(Set<T> x, Set<T> y)
		{
			return x.Equals(y);
		}

		public static bool operator !=(Set<T> x, Set<T> y)
		{
			return !(x == y);
		}
		#endregion

		#region Union

		public void Union(Set<T> otherSet)
		{
			Add(otherSet);
		}


		#endregion

		#region Intersection

		public static Set<T> Intersection(Set<T> x, Set<T> y)
		{
			Set<T> intersection = new Set<T>();
			foreach (T item in x)
			{
				if (y.Contains(item))
				{
					intersection.Add(item);
				}
			}

			return intersection;
		}
		public void Intersection(Set<T> otherSet)
		{
			this.underlyingList = Intersection(this, otherSet).underlyingList;
		}
		#endregion

	}
}