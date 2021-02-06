using DealerOnProblemThree.Graph;
using System;
using System.IO;
using System.Linq;

namespace DealerOnProblemThree
{
	/// <summary>
	/// Solution for DealerOn Development Candidate Test Problem #3: Trains.
	/// </summary>
	class Program
	{
		private static int _problem;

		/// <summary>
		/// Takes file Problem.txt and uses the first line of form "AB#, CD#, EF# GH#, ..."
		/// where each entry is an edge in a directed graph. For example AB# is an edge from
		/// A to B with weight #. Then any problems listed are processed in order as specified
		/// In the parse problem method. Following this user input can be used to add additional
		/// edges and try different problems.
		/// </summary>
		static void Main()
		{
			// Gets problem.txt from the parent directory
			string path = Path.Combine(
				Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, 
				@"Data\Problem.txt");
			string[] files = File.ReadAllLines(path);

			// Strip all whitespace from the string
			string input = new string(files[0].ToCharArray()
				.Where(c => !char.IsWhiteSpace(c))
				.ToArray());

			// Split the string into individual routes
			string[] routes = input.Split(',');

			// Make the graph
			var graph = new DirectedGraph();

			// Go through all routes
			foreach (var route in routes)
				graph.AddEdge(route[0], route[1], int.Parse(route.Substring(2)));

			// Parses all problems in the file
			for (int i = 1; i < files.Length; i++)
				ParseProblem(files[i], graph);

			Console.WriteLine("\nType 'quit' to exit and 'h' for help");

			// Read file input
			string line = "";
			while (!(line = Console.ReadLine()).Equals("quit"))
				ParseProblem(line, graph);
		}

		/// <summary>
		/// Parses a problem statement and outputs the answer if possible.
		/// Problems are of the following form and are operated on the graph.
		/// 1. distance A-B-C: Finds distance of the route
		/// 2. stops-max A B #: Finds number of routes from A to B with maximum # stops
		/// 3. stops-exact A B #: Finds number of routes from A to B with exactly # stops
		/// 4. stops-distance A B #: Finds number of routes from A to B with less than # distance traveled
		/// 5. shortest A B: Finds shortest route from A to B
		/// 
		/// Additionally edges can be added to the graph if needed as follows.
		/// 7. add-edge A B #: Adds an edge from A to B with distance #
		/// </summary>
		/// <param name="line">A line containing a directed graph problem.</param>
		/// <param name="graph">The graph that is the problem domain.</param>
		private static void ParseProblem(string line, DirectedGraph graph)
		{
			// Not a great implementation but it works for now (too messy)
			string[] args = line.Split(' ');
			try
			{
				switch (args[0])
				{
					case "distance":
						PrintAnswer(graph.FindRouteDistance(args[1]));
						break;
					case "stops-max":
						PrintAnswer(graph.FindNumberOfRoutes(args[1][0], args[2][0],
							(node) => { return node.Depth <= int.Parse(args[3]); },
							(node) => { return node.Depth > int.Parse(args[3]); }));
						break;
					case "stops-exact":
						PrintAnswer(graph.FindNumberOfRoutes(args[1][0], args[2][0],
							(node) => { return node.Depth == int.Parse(args[3]); },
							(node) => { return node.Depth >= int.Parse(args[3]); }));
						break;
					case "stops-distance":
						PrintAnswer(graph.FindNumberOfRoutes(args[1][0], args[2][0],
							(node) => { return node.Distance < int.Parse(args[3]); },
							(node) => { return node.Distance > int.Parse(args[3]); }));
						break;
					case "shortest":
						PrintAnswer(graph.FindShortestRoute(args[1][0], args[2][0]));
						break;
					case "add-edge":
						graph.AddEdge(args[1][0], args[2][0], int.Parse(args[3]));
						break;
					case "h":
						Console.WriteLine("distance A-B-C: Finds distance of the route");
						Console.WriteLine("stops-max A B #: Finds number of routes from A to B with maximum # stops");
						Console.WriteLine("stops-exact A B #: Finds number of routes from A to B with exactly # stops");
						Console.WriteLine("stops-distance A B #: Finds number of routes from A to B with less than # distance traveled");
						Console.WriteLine("shortest A B: Finds shortest route from A to B");
						Console.WriteLine("add-edge A B #: Adds an edge from A to B with distance #");
						break;
					case "":
						break;
					default:
						Console.WriteLine("Invalid command.");
						break;
				}
			}
			catch (IndexOutOfRangeException)
			{
				Console.WriteLine("Invalid command.");
			}
		}

		/// <summary>
		/// Prints the answer using the problem number and answer output.
		/// </summary>
		/// <param name="problem">The problem number.</param>
		/// <param name="value">The answer to the problem.</param>
		private static void PrintAnswer(string value)
		{
			Console.WriteLine("Output #" + ++_problem + ": " + value);
		}
	}
}
