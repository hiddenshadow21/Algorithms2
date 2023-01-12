namespace labCS;
using System;
using System.Collections.Generic;

class FordFulkerson
{
    private int _v;
    private readonly int[,] _graph;
    private int[,] _rG;
    private int[] _parent;
    private bool[] _visited;

    public FordFulkerson(int[,] graph)
    {
        _graph = graph;
        _v = _graph.GetLength(0);
    }

    private bool Bfs(int s, int t)
    {
        for (var i = 0; i < _v; i++)
        {
            _visited[i] = false;
            _parent[i] = -1;
        }

        var q = new Queue<int>();
        q.Enqueue(s);
        _visited[s] = true;

        while (q.Count != 0)
        {
            var u = q.Dequeue();

            for (var v = 0; v < _v; v++)
            {
                if (!_visited[v] && _rG[u, v] > 0)
                {
                    q.Enqueue(v);
                    _visited[v] = true;
                    _parent[v] = u;
                }
            }
        }

        return _visited[t];
    }

    public int GetMaxFlow(int s, int t)
    {
        _rG = new int[_v, _v];
        _parent = new int[_v];
        _visited = new bool[_v];

        for (var i = 0; i < _v; i++)
        {
            for (var j = 0; j < _v; j++)
            {
                _rG[i, j] = _graph[i, j];
            }
        }

        var maxFlow = 0;

        while (Bfs(s, t))
        {
            var pathFlow = int.MaxValue;
            for (var v = t; v != s; v = _parent[v])
            {
                var u = _parent[v];
                pathFlow = Math.Min(pathFlow, _rG[u, v]);
            }

            for (var v = t; v != s; v = _parent[v])
            {
                var u = _parent[v];
                _rG[u, v] -= pathFlow;
                _rG[v, u] += pathFlow;
            }

            maxFlow += pathFlow;
        }

        return maxFlow;
    }

    public static void Start()
    {
        int[,] graph = { { 0, 16, 13, 0, 0, 0 },
                { 0, 0, 10, 12, 0, 0 },
                { 0, 4, 0, 0, 14, 0 },
                { 0, 0, 9, 0, 0, 20 },
                { 0, 0, 0, 7, 0, 4 },
                { 0, 0, 0, 0, 0, 0 }
              };

        var s = 0;
        var t = 5;

        var maxFlow = new FordFulkerson(graph).GetMaxFlow(s, t);

        Console.WriteLine("The maximum possible flow is: " + maxFlow);
    }
}
