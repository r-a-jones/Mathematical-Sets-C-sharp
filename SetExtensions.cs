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
			Set set = new Set();
			set.Add(array);
			return set;
		}

        /// <summary>
        /// Returns all subsets of the set of a given size.
        /// For a set of size N, there will be N choose n such subsets.
        /// </summary>
        /// <param name="set">The set to find subsets of.</param>
        /// <param name="n">The size the subsets should be.</param>
        /// <returns>A list of subsets of the specified size.</returns>
        /// <exception cref="Exception"></exception>
        public static Set[] SubsetsOfSize(this Set set, int n)
        {
            object[][] subsetsAsSubarrays = set.ToArray().SubarraysOfSize(n);

            List<Set> subsets = new List<Set>();

			foreach (object[] array in subsetsAsSubarrays)
			{
                subsets.Add(array.ToSet());
			}

            return subsets.ToArray();
        }

        private static T[][] SubarraysOfSize<T>(this T[] array, int n)
        {
            if (n > array.Length)
            {
                throw new Exception("Given subarray size greater than array");
            }
            else if (n == array.Length)
            {
                return new T[][] { array };
            }

            List<T[]> subarrays = new List<T[]>();
            //we will do this recursively.

            //base case
            if (n == 0)
            {
                subarrays.Add(new T[] { });
                return subarrays.ToArray();
            }

            for (int first = 0; first <= array.Length - n; first++) //index of the first item (in current order) to have in this subarray.
            {

                T[] subarrayAfterFirst = new T[array.Length - first - 1];

                for (int i = 0; i < subarrayAfterFirst.Length; i++)
                {
                    subarrayAfterFirst[i] = array[i + first + 1];
                }

                foreach (T[] subarray in subarrayAfterFirst.SubarraysOfSize(n - 1))
                {
                    T[] newSubarray = new T[subarray.Length + 1];

                    newSubarray[0] = array[first];
                    for (int i = 1; i < newSubarray.Length; i++)
                    {
                        newSubarray[i] = subarray[i - 1];
                    }
                    subarrays.Add(newSubarray);
                }
            }

            return subarrays.ToArray();

        }

        /// <summary>
        /// Returns all subsets of the set of a given size, containing specified elements.
        /// For a set of size N, there will be N choose n such subsets.
        /// </summary>
        /// <param name="set">The set to find subsets of.</param>
        /// <param name="n">The size the subsets should be.</param>
        /// <param name="elementsToContain">The elements that should be contained.</param>
        /// <returns></returns>
        public static Set[] SubarraysOfSizeContaining(this Set set, int n, object[] elementsToContain)
        {
            object[][] subsetsAsSubarrays = set.ToArray().SubarraysOfSizeContaining(n, elementsToContain);

            List<Set> subsets = new List<Set>();

            foreach (object[] array in subsetsAsSubarrays)
            {
                subsets.Add(array.ToSet());
            }

            return subsets.ToArray();
        }
        private static T[][] SubarraysOfSizeContaining<T>(this T[] array, int n, T[] elementsToContain)
        {
            //returns as above function, but with certain elements contained.

            //get the indices of the elements we want to contain.
            int[] elementsToContainIndices = new int[elementsToContain.Length];
            for (int i = 0; i < elementsToContainIndices.Length; i++)
            {
                elementsToContainIndices[i] = -1; //need to set it to -1 since we check if elementsToContainIndices already contains an index. if one of the elementsToContain is at 0 then
                //could be a problem since array initialised to 0
            }

            for (int i = 0; i < elementsToContain.Length; i++)
            {
                T currentElement = elementsToContain[i];

                int k = 0;

                while (array[k].Equals(currentElement) == false || elementsToContainIndices.Contains(k)) //if the indices contains j we may have a duplicate element in elementsToContain, want to have 2 of them contained.
                {
                    k++;
                }
                elementsToContainIndices[i] = k;
            }

            //now make a new list, that contains only the elements we DONT want to necessarily contain.

            T[] elementsNotToContain = new T[array.Length - elementsToContain.Length];

            int index = 0;
            int j = 0;

            foreach (T element in array)
            {
                if (elementsToContainIndices.Contains(index) == false)
                {
                    elementsNotToContain[j] = element;
                    j++;
                }
                index++;
            }

            //now get all subarrays of elementsNotToContain of desired size.

            T[][] subarraysOfNotToContain = SubarraysOfSize(elementsNotToContain, n - elementsToContain.Length);

            //now add the elements to contain to each of the arrays in this.

            List<T[]> result = new List<T[]>();

            for (int i = 0; i < subarraysOfNotToContain.Length; i++)
            {

                result.Add(subarraysOfNotToContain[i].Concat(elementsToContain).ToArray());
            }

            return result.ToArray();
        }
    }
}
