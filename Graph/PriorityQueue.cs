using System;

namespace DealerOnProblemThree.Graph
{
	/// <summary>
	/// Priority Queue implementation using a min heap. Values can be inserted to
	/// quickly retrieve the minimum value.
	/// </summary>
	/// <typeparam name="T">A type that inherits the IComparable interface.</typeparam>
	public class PriorityQueue<T> where T :IComparable
	{
		private static readonly int INITAL_SIZE = 10;

		private T[] _array;
		public int Size { get; private set; }

		/// <summary>
		/// Creates a new priority queue.
		/// </summary>
		public PriorityQueue()
		{
			_array = new T[INITAL_SIZE];
			Size = 0;
		}

		/// <summary>
		/// Adds an item that is then put into the correct position on the heap.
		/// </summary>
		/// <param name="item">The item being added.</param>
		public void Add (T item)
		{
			_array[Size] = item;

			// Swaps the new item until it is in the correct position
			for (int parentIndex = (Size - 1) / 2; parentIndex >= 0; parentIndex = (parentIndex - 1) / 2)
			{
				if (_array[Size].CompareTo(_array[parentIndex]) == -1)
					Swap(Size, parentIndex);

				if (parentIndex == 0)
					break;
			}

			Size++;

			// Resize the array if needed
			if (Size >= _array.Length)
				Resize();
		}

		/// <summary>
		/// Removes the minimum value from the heap than restructures
		/// the tree as necessary to maintain minimum value in root.
		/// </summary>
		/// <returns>The minimum value in the priority queue.</returns>
		public T Remove()
		{
			if (Size == 0)
				return default;

			T node = _array[0];
			_array[0] = _array[Size - 1];
			_array[Size - 1] = default;
			Size--;

			// Used to see if we need to keep swapping
			// Values in the min heap
			bool swapped = true;

			// Holds the current index in the swap
			int current = 0;

			while (swapped)
			{
				swapped = false;

				// Values of the child nodes in the array
				int left = (current * 2) + 1;
				int right = (current * 2) + 2;

				// If the left value exists in the array
				if (left < Size)
				{
					// If the right value exists in the array
					if (right < Size) 
					{
						// Swap with right value if current is larger
						if (_array[right].CompareTo(_array[left]) == -1
							&& _array[current].CompareTo(_array[right]) == 1)
						{
							Swap(current, right);
							current = right;
							swapped = true;
							continue;
						}
					}
					
					// Swap with left value if current is larger
					if (_array[current].CompareTo(_array[left]) == 1)
					{
						Swap(current, left);
						current = left;
						swapped = true;
					}
				}
			}

			return node;
		}

		/// <summary>
		/// Swaps two elements in the array.
		/// </summary>
		/// <param name="first">First element being swapped.</param>
		/// <param name="second">Second element being swapped.</param>
		private void Swap(int first, int second)
		{
			T temp = _array[first];
			_array[first] = _array[second];
			_array[second] = temp;
		}

		/// <summary>
		/// Resizes the array to twice it's current size.
		/// </summary>
		private void Resize()
		{
			T[] newArray = new T[Size * 2];
			Array.Copy(_array, newArray, Size);
			_array = newArray;
		}
	}
}
