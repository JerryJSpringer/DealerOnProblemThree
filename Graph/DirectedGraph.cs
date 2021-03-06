﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace DealerOnProblemThree.Graph
{
	/// <summary>
	/// Directed graph that can find the distance of a path, the number
	/// of paths between two points, and the shortest distance between
	/// two points.
	/// </summary>
	public class DirectedGraph
	{
		private static readonly string NO_ROUTE = "NO SUCH ROUTE";

		private readonly Dictionary<char, Dictionary<char, int>> _graph;

		/// <summary>
		/// Creates a new directed graph.
		/// </summary>
		public DirectedGraph()
		{
			_graph = new Dictionary<char, Dictionary<char, int>>();
		}

		/// <summary>
		/// Adds an edge to the graph with a weight.
		/// </summary>
		/// <param name="start">The starting node.</param>
		/// <param name="end">The ending node.</param>
		/// <param name="weight">The weight of the edge.</param>
		public void AddEdge(char start, char end, int weight)
		{
			// Checks if the node exists and creates it if necessary
			if (!_graph.ContainsKey(start))
				_graph.Add(start, new Dictionary<char, int>());
			if (!_graph.ContainsKey(end))
				_graph.Add(end, new Dictionary<char, int>());

			// Add edge from start to end with given weight
			if (_graph[start].ContainsKey(end))
				_graph[start][end] = weight;
			else
				_graph[start].Add(end, weight);
		}

		/// <summary>
		/// Finds the distance of a route of form "A-B-C" where there
		/// can be any number of elements and returns that there is no
		/// route if any node is not in the graph.
		/// </summary>
		/// <param name="route">Route of the form "A-B-C".</param>
		/// <returns>The length of the route if possilbe else no path found.</returns>
		public string FindRouteDistance(string route)
		{
			// Distance traveled
			int distance = 0;

			// The stops represented by char
			char[] stops = route.ToUpper().ToCharArray().Where(c => c != '-').ToArray();
			char current = stops[0];

			if (!_graph.ContainsKey(current))
				return NO_ROUTE;

			// Iterate through the stops
			for (int i = 1; i < stops.Length; i++)
			{
				// Gets the next stop
				char next = stops[i];

				// Checks if the current stop or the edge to the next stop exist
				if (!_graph[current].ContainsKey(next))
					return NO_ROUTE;

				// Adds to distance traveled and updates current location
				distance += _graph[current][next];
				current = next;
			}

			return distance.ToString();
		}

		/// <summary>
		/// Finds the number of routes between two points in the graph. Conditions
		/// need to be specified for whether the route should be accepted or rejected.
		/// </summary>
		/// <param name="start">The starting node.</param>
		/// <param name="end">The ending node.</param>
		/// <param name="accept">Conditions for a solution beind accepted.</param>
		/// <param name="reject">Conditions for a solution being rejected.</param>
		/// <returns></returns>
		public string FindNumberOfRoutes(char start, char end, Func<Node, bool> accept, Func<Node, bool> reject)
		{
			// Check for invalid route
			if (!_graph.ContainsKey(start) || !_graph.ContainsKey(end))
				return NO_ROUTE;

			// The amount of routes found
			int routes = 0;

			// Stack of canidate routes
			var stack = new Stack<Node>();

			// Push the first node on
			stack.Push(new Node
			{
				Id = start,
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
						Depth = current.Depth + 1
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

		/// <summary>
		/// Finds the shortest route between two points using an implementation of A*.
		/// </summary>
		/// <param name="start">The starting node.</param>
		/// <param name="end">The ending node.</param>
		/// <returns>The length of the shortest route or no path available.</returns>
		public string FindShortestRoute(char start, char end)
		{
			// Check for invalid route
			if (!_graph.ContainsKey(start) || !_graph.ContainsKey(end))
				return NO_ROUTE;

			// Setup open and closed lists
			var open = new Dictionary<char, bool>();
			var closed = new Dictionary<char, bool>();

			// Setup queue and map of nodes
			var queue = new PriorityQueue<Node>();
			var map = new Dictionary<char, Node>
			{
				{ start, new Node() { Id = start } }
			};
			queue.Add(map[start]);
			open[start] = false;
			closed[start] = false;

			// Will be the current node in the algo
			Node current;

			// While there are paths to consider
			while (queue.Size != 0)
			{
				// Get the least ranked node
				current = queue.Remove();

				// Adds the current to closed
				if (!closed.ContainsKey(current.Id))
					closed.Add(current.Id, true);

				// Iterate through all the neighboring nodes
				foreach(var item in _graph[current.Id])
				{
					// Calculates the cost to move to the node
					int distance = current.Distance + item.Value;

					// Make the node and add it to the map if needed
					Node neighbor;
					if (!map.ContainsKey(item.Key))
					{
						neighbor = new Node() { Id = item.Key };
						map.Add(item.Key, neighbor);
						open[item.Key] = false;
						closed[item.Key] = false;
					} else
					{
						neighbor = map[item.Key];
					}

					// New path is better than old path
					if (distance < neighbor.Distance && open[neighbor.Id])
						open[neighbor.Id] = false;

					// Node is not in open or closed and therfore
					// Has not been considered and could be part of path
					if (!open[neighbor.Id] && !closed[neighbor.Id])
					{
						neighbor.Distance = distance;
						neighbor.Heuristic = 0;
						neighbor.Parent = current;
						queue.Add(neighbor);
						open[neighbor.Id] = true;
					}
				}
			}

			// Return shortest distance if the route exists
			if (map[end].Parent != null)
				return map[end].Distance.ToString();
			else
				return NO_ROUTE;
		}
	}
}
