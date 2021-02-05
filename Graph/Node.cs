using System;

namespace DealerOnProblemThree.Graph
{
	public class Node : IComparable
	{
		public char Id { get; set; }
		public int Distance { get; set; }
		public int Heuristic { get; set; }
		public int Depth { get; set; }

		public int CompareTo(object obj)
		{
			Node other = (Node)obj;

			// F = G + H
			// Note if heuristic is 0 we have dijkstra's
			int f = Distance + Heuristic;
			int of = other.Distance + other.Heuristic;

			return f.CompareTo(of);
		}
	}
}
