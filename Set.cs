﻿using System.Collections;
using System.Linq;

namespace Mathematical_Sets
{
	/// <summary>
	/// Implements a mathematical set - elements are not contained with multiplicity. There is no order (i.e. indexing) to the set.
	/// </summary>

	public class Set : IEnumerable
	{
		private List<object> underlyingList = new List<object>();

		/// <summary>
		/// Add an item to the set.
		/// </summary>
		/// <param name="items">The item to add to the set.</param>
		public void Add(object item)
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
		public void Add(object[] items)
		{
			foreach (object item in items)
			{
				Add(item);
			}
		}

		/// <summary>
		/// Add items to the set.
		/// </summary>
		/// <param name="items">The items to add to the set.</param>
		public void Add(List<object> items)
		{
			Add(items.ToArray());
		}

		/// <summary>
		/// Add items to the set.
		/// </summary>
		/// <param name="items">The items to add to the set.</param>
		public void Add(Set items)
		{
			Add(items.ToArray());
		}

		/// <summary>
		/// Determines whether an element is in the <c>Set</c>.
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		public bool Contains(object item)
		{
			return underlyingList.Contains(item);
		}

		/// <summary>
		/// Remove an item from the set.
		/// </summary>
		/// <param name="item">The items to remove from the set.</param>
		public void Remove(object item)
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
		public void Remove(object[] items)
		{
			foreach (object item in items)
			{
				Remove(item);
			}

		}

		/// <summary>
		/// Remove items from the set.
		/// </summary>
		/// <param name="items">The items to remove from the set.</param>
		public void Remove(List<object> items)
		{
			Remove(items);
		}

		/// <summary>
		/// Remove items from the set.
		/// </summary>
		/// <param name="items">The items to remove from the set.</param>
		public void Remove(Set items)
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
			foreach (object item in this)
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
		public IEnumerator GetEnumerator()
		{
			return ((IEnumerable)underlyingList).GetEnumerator();
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
		public bool IsSubsetOf(Set otherSet)
		{
			foreach (object item in this)
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
		public static bool operator <(Set x, Set y)
		{
			return x.IsProperSubsetOf(y);
		}

		/// <summary>
		/// Less than operator is used as proper subset.
		/// </summary>
		public static bool operator >(Set x, Set y)
		{
			return y < x;
		}

		/// <summary>
		/// Less than or equal to operator is used as subset.
		/// </summary>
		public static bool operator <=(Set x, Set y)
		{
			return x.IsEqualTo(y);
		}

		/// <summary>
		/// Less than or equal to operator is used as subset.
		/// </summary>
		public static bool operator >=(Set x, Set y)
		{
			return y <= x;
		}


		/// <summary>
		/// Returns whether this set is a proper subset of the given parameter.
		/// </summary>
		public bool IsProperSubsetOf(Set otherSet)
		{
			return (otherSet.Size > this.Size) && (this.IsSubsetOf(otherSet));
		}
		#endregion

		#region Equality
		/// <summary>
		/// Returns whether this set is equal to another set.
		/// </summary>
		private bool IsEqualTo(Set otherSet)
		{
			return IsSubsetOf(otherSet) && otherSet.Size == this.Size;
		}

		public override bool Equals(object? obj)
		{
			if (obj is Set)
			{
				return this.IsEqualTo((Set)obj);
			}
			return false;
		}

		public override int GetHashCode()
		{
			return underlyingList.GetHashCode();
		}

		public static bool operator ==(Set x, Set y)
		{
			return x.Equals(y);
		}

		public static bool operator !=(Set x, Set y)
		{
			return !(x == y);
		}
		#endregion

		#region Union

		public void Union(Set otherSet)
		{
			Add(otherSet);
		}


		#endregion

		#region Intersection

		public static Set Intersection(Set x, Set y)
		{
			Set intersection = new Set();
			foreach (object item in x)
			{
				if (y.Contains(item))
				{
					intersection.Add(item);
				}
			}

			return intersection;
		}
		public void Intersection(Set otherSet)
		{
			this.underlyingList = Intersection(this, otherSet).underlyingList;
		}
		#endregion

		public object[] ToArray()
		{
			return underlyingList.ToArray();
		}
	}
}