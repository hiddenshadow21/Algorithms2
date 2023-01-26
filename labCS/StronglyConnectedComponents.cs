namespace labCS;

using System;
using System.Collections.Generic;

class StronglyConnectedComponents
{
    // Number of vertices
    private int _v;

    // Array of lists for Adjacency List Representation
    private List<int>[] _adj;
    private List<int>[] _transposedAdj;

    // DFS stack
    private Stack<int> _stack;

    // Visited array
    private bool[] _visited;

    // Array to store the SCCs
    private List<List<int>> _sccs;

    // Function that returns the strongly connected components
    public List<List<int>> GetScCs()
    {
        _sccs = new List<List<int>>();
        _stack = new Stack<int>();
        _visited = new bool[_v];

        // First DFS traversal to fill stack with vertex in decreasing order of finish time
        for (var i = 0; i < _v; i++)
            if (!_visited[i])
                Dfs(i);

        // Second DFS traversal to find SCCs
        _visited = new bool[_v];
        while (_stack.Count > 0)
        {
            var v = _stack.Pop();
            if (!_visited[v])
            {
                var scc = new List<int>();
                Dfs2(v, scc);
                _sccs.Add(scc);
            }
        }

        return _sccs;
    }

// A recursive function to perform DFS traversal
    private void Dfs(int v)
    {
        _visited[v] = true;

        // Recur for all the vertices adjacent to this vertex
        foreach (var w in _adj[v])
        {
            if (!_visited[w])
                Dfs(w);
        }

        // Push current vertex to stack
        _stack.Push(v);
    }

// A recursive function to perform DFS traversal on transposed graph
    private void Dfs2(int v, List<int> scc)
    {
        _visited[v] = true;
        scc.Add(v);

        // Recur for all the vertices adjacent to this vertex
        foreach (var w in _transposedAdj[v])
        {
            if (!_visited[w])
                Dfs2(w, scc);
        }
    }

// Constructor
    public StronglyConnectedComponents(int v, List<int>[] edges)
    {
        _v = v;
        _adj = edges;

        _transposedAdj = new List<int>[v];
        for (var i = 0; i < v; i++)
            _transposedAdj[i] = new List<int>();

        for (var i = 0; i < v; i++)
            foreach (var j in edges[i])
                _transposedAdj[j].Add(i);
    }

    public static void Start()
    {
        var edges = new List<int>[]
        {
            new()
            {
                2, 3
            },
            new()
            {
                0
            },
            new()
            {
                1
            },
            new() {4},
            new()
        };

        var sccs = new StronglyConnectedComponents(5, edges).GetScCs();
        foreach (var scc in sccs)
        {
            Console.WriteLine(string.Join(' ', scc));
        }
    }
}