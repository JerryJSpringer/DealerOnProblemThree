# Problem #3: Trains
## Approach
The problems given could be broken down into three distinct sub-problems.

1. [Distance of a given route](#distance-route)
2. [Number of routes between two points](#number-of-routes)
3. [Shortest route between two points](#shortest-route)

## Graph Implementation
My implementation was based off of standard adjacency list implementation for a graph. The one major change I made was to use a dictionary instead of some type of list. The reason for this was that I wanted to be able to make the graph without knowing the total amount of nodes, while still maintaining good insertion and read times. As a result this implementation can use any singular character for a node in the graph and could be easily modified to take strings which would be useful if this code was ever used in an application.

## Distance of given route (1-5) <a name="distance-of-route"></a>
The simplest of the three sub-problems that was solved by checking to see if the next value was valid, and if it was adding the value to the distance. If the next stop in the route was ever not valid we would simply return that no routes were found.


## Number of routes between two points (6, 7, 10)<a name="number-of-routes"></a>
At first I considered a dynamic programming solution but decided instead to use a modified DFS due to being able to visit one location more than once. One consideration that needed to be made was that these three problems were actually also each their own sub-problem as listed below.

1. Number of routes with a maximum number of stops (6)
2. Number of routes with an exact amount of stops (7)
3. A route with a maximum allowed distance (10)

To ensure I could reuse the same code I used anonymous functions that would return a boolean value to determine if the given route should be accepted or rejected. From there it was simple to make the conditions for accepting or rejecting a route.

## Shortest route between two points (8,9)<a name="shortest-route"></a>
For this problem I decided to implement the [A* algorithm](http://theory.stanford.edu/~amitp/GameProgramming/AStarComparison.html) as I had recently implemented it for a game development project and as it could outperform Dijkstra's algorithm given that I could find a heuristic.
> **Note:** Given no heuristic A* is identical to Dijkstra's

In general the algorithm works by favoring nodes that are closest to the goal by determining a cost based of the distance traveled and the estimated distance to the goal.
