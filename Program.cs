using DealerOnProblemThree.Graph;
using System;
using System.Linq;

namespace DealerOnProblemThree
{
	class Program
	{
		static void Main(string[] args)
		{
			// Strip all whitespace from the string
			string input = new string("AB5, BC4, CD8, DC8, DE6, AD5, CE2, EB3, AE7".ToCharArray()
				.Where(c => !char.IsWhiteSpace(c))
				.ToArray());

			// Split the string into individual routes
			string[] routes = input.Split(',');

			// Make the graph
			var graph = new DirectedGraph();

			// Go through all routes
			foreach (var route in routes)
				graph.AddEdge(route[0], route[1], route[2] - '0');

			// Route distance
			PrintAnswer(1, graph.FindRouteDistance("A-B-C"));
			PrintAnswer(2, graph.FindRouteDistance("A-D"));
			PrintAnswer(3, graph.FindRouteDistance("A-D-C"));
			PrintAnswer(4, graph.FindRouteDistance("A-E-B-C-D"));
			PrintAnswer(5, graph.FindRouteDistance("A-E-D"));

			// Number of routes with max stops
			PrintAnswer(6, graph.FindNumberOfRoutes('C', 'C', 
				(node) => { return node.Depth <= 3; }, 
				(node) => { return node.Depth > 3; }));
			// Number of routes with exact amount of stops
			PrintAnswer(7, graph.FindNumberOfRoutes('A', 'C',
				(node) => { return node.Depth == 4; },
				(node) => { return node.Depth >= 4; }));

			// Shortest path
			PrintAnswer(8, graph.FindShortestRoute('A', 'C'));
			PrintAnswer(9, graph.FindShortestRoute('B', 'B'));

			// Route with max distance
			PrintAnswer(10, graph.FindNumberOfRoutes('C', 'C',
				(node) => { return node.Distance < 30; },
				(node) => { return node.Distance > 30; }));
		}

		private static void PrintAnswer(int problem, string value)
		{
			Console.WriteLine("Output #" + problem + ": " + value);
		}
	}
}
