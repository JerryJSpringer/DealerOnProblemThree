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
				graph.AddEdge(route[0], route[1], (int)(route[2]-'0'));

			PrintAnswer(1, graph.FindRouteDistance("A-B-C"));
			PrintAnswer(2, graph.FindRouteDistance("A-D"));
			PrintAnswer(3, graph.FindRouteDistance("A-D-C"));
			PrintAnswer(4, graph.FindRouteDistance("A-E-B-C-D"));
			PrintAnswer(5, graph.FindRouteDistance("A-E-D"));
		}

		private static void PrintAnswer(int problem, string value)
		{
			Console.WriteLine("Output #" + problem + ": " + value);
		}
	}
}
