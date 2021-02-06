using System;

namespace DealerOnProblemThree.Graph
{
	/// <summary>
	/// Represents a node for use in a graph or queue.
	/// </summary>
	public class Node : IComparable
	{
		public char Id { get; set; }
		public Node Parent { get; set; }
		public int Distance { get; set; }
		public int Heuristic { get; set; }
		public int Depth { get; set; }

		/// <summary>
		/// Used to compare nodes based off of distance and heuristic value.
		/// </summary>
		/// <param name="obj">Another object being compared to.</param>
		/// <returns>If this node is greater than, less than, or equal</returns>
		public int CompareTo(object obj)
		{
			Node other = (Node)obj;

			// F = G + H
			// Note if heuristic is 0 we have dijkstra's
			int f = Distance + Heuristic;
			int of = other.Distance + other.Heuristic;

			// Compare using int compare to
			return f.CompareTo(of);
		}
	}
}
