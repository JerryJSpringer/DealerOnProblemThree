using System;
using System.Diagnostics.CodeAnalysis;

namespace DealerOnProblemThree.Graph
{
	public class PriorityQueue<T> where T :IComparable
	{
		private static readonly int INITAL_SIZE = 10;

		private PriorityQueueItem[] _array;
		public int Size { get; private set; }

		public PriorityQueue()
		{
			_array = new PriorityQueueItem[INITAL_SIZE];
			Size = 0;
		}

		public void Add (T item)
		{
			_array[Size] = new PriorityQueueItem(item);

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

		public T Remove()
		{
			if (Size == 0)
				return default;

			PriorityQueueItem node = _array[0];
			_array[0] = _array[Size - 1];
			_array[Size - 1] = null;
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

			return node.item;
		}

		private void Swap(int first, int second)
		{
			PriorityQueueItem temp = _array[first];
			_array[first] = _array[second];
			_array[second] = temp;
		}

		private void Resize()
		{
			PriorityQueueItem[] newArray = new PriorityQueueItem[Size * 2];
			Array.Copy(_array, newArray, Size);
			_array = newArray;
		}

		public int CompareTo(object obj)
		{
			throw new NotImplementedException();
		}

		internal class PriorityQueueItem : IComparable<PriorityQueueItem>
		{
			internal readonly T item;

			public PriorityQueueItem(T item)
			{
				this.item = item;
			}

			public int CompareTo([AllowNull] PriorityQueueItem other)
			{
				return item.CompareTo(other.item);
			}
		}
	}
}
