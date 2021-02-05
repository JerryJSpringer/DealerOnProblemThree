using System;
using System.Collections.Generic;
using System.Linq;

namespace DealerOnProblemThree.Graph
{
	public class DirectedGraph
	{
		private readonly Dictionary<char, Dictionary<char, int>> _graph;

		public DirectedGraph()
		{
			_graph = new Dictionary<char, Dictionary<char, int>>();
		}

		public void AddEdge(char start, char end, int weight)
		{
			// Checks if the node exists and creates it if necessary
			if (!_graph.ContainsKey(start))
				_graph.Add(start, new Dictionary<char, int>());

			// Add edge from start to end with given weight
			_graph[start].Add(end, weight);
		}

		public string FindRouteDistance(string route)
		{
			// Distance traveled
			int distance = 0;

			// The stops represented by char
			char[] stops = route.ToUpper().ToCharArray().Where(c => c != '-').ToArray();
			char current = stops[0];

			// Iterate through the stops
			for (int i = 1; i < stops.Length; i++)
			{
				// Gets the next stop
				char next = stops[i];

				// Checks if the current stop or the edge to the next stop exist
				if (!_graph.ContainsKey(current) || !_graph[current].ContainsKey(next))
					return "NO SUCH ROUTE";

				// Adds to distance traveled and updates current location
				distance += _graph[current][next];
				current = next;
			}

			return distance.ToString();
		}

		public string FindNumberOfRoutes(char start, char end, Func<Node, bool> accept, Func<Node, bool> reject)
		{
			// The amount of routes found
			int routes = 0;

			// Stack of canidate routes
			var stack = new Stack<Node>();

			// Push the first node on
			stack.Push(new Node
			{
				Id = start,
				Distance = 0,
				Stops = 0
			});

			do
			{
				// Get the next item on the stack
				var current = stack.Pop();

				// For each adjacent node add to stack
				foreach (var item in _graph[current.Id])
				{
					// Creates a new node
					var node = new Node
					{
						Id = item.Key,
						Distance = current.Distance + item.Value,
						Stops = current.Stops + 1
					};

					// If the node path is still acceptable push new node
					if (!reject(node))
						stack.Push(node);
					
					// If the route is at the end and acceptable increment routes
					if (node.Id == end && accept(node))
						routes++;
				}
			} while (stack.Count() != 0); // There are no canidate routes left

			return routes.ToString();
		}
	}
}
