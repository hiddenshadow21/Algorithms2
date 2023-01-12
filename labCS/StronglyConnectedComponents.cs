namespace labCS;

using System;
using System.Collections.Generic;

class StronglyConnectedComponents
{
    // Number of vertices
    private readonly int _v;

    // Array of lists for Adjacency List Representation
    private readonly List<int>[] _adj;

    // DFS stack
    private Stack<int> _stack;

    // Visited array
    private bool[] _visited;

    // Array to store the SCCs
    private List<List<int>> _sccs;

    // Function that returns the strongly connected components
    public List<List<int>> GetSccs()
    {
        _sccs = new List<List<int>>();
        _stack = new Stack<int>();
        _visited = new bool[_v];

        // Initialize all vertices as not visited
        for (var i = 0; i < _v; i++)
            _visited[i] = false;

        // Call the DFS function for each vertex
        for (var i = 0; i < _v; i++)
            if (!_visited[i])
                Dfs(i);

        return _sccs;
    }

    // A recursive function to print DFS starting from v
    private void Dfs(int v)
    {
        _visited[v] = true;
        _stack.Push(v);

        // Recur for all the vertices adjacent to this vertex
        foreach (var w in _adj[v])
        {
            if (!_visited[w])
                Dfs(w);
        }

        // If v is the root of an SCC
        var stackRoot = _stack.Reverse().ToArray()[0];
        
        if (stackRoot == v)
        {
            var scc = new List<int>();
            int w;
            do
            {
                w = _stack.Pop();
                scc.Add(w);
            } while (w != v);

            _sccs.Add(scc);
        }
    }

    // Constructor
    public StronglyConnectedComponents(int v, List<int>[] edges)
    {
        _v = v;
        _adj = edges;
    }

    public static void Start()
    {
        // Number of vertices
        var v = 8;

// Array of lists for Adjacency List Representation
        List<int>[] edges = new List<int>[v];
        for (var i = 0; i < v; i++)
            edges[i] = new List<int>();

// Add edges to the graph
        edges[0].Add(1);
        edges[1].Add(0);
        edges[2].Add(4);
        edges[2].Add(7);
        edges[3].Add(2);
        edges[4].Add(3);
        edges[5].Add(4);
        edges[5].Add(6);
        edges[6].Add(5);
        edges[7].Add(4);

// Create an instance of the class
        var scc = new StronglyConnectedComponents(v, edges);

// Get the strongly connected components
        List<List<int>> result = scc.GetSccs();

// Print the strongly connected components
        foreach (var sccl in result)
        {
            Console.Write("SCC: ");
            foreach (var vertex in sccl)
                Console.Write(vertex + " ");
            Console.WriteLine();
        }
    }
}