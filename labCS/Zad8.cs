namespace labCS;

public class Zad8
{
    private int _time;

    public (List<Tuple<int, int>> bridges, HashSet<int> articulationPoints) FindBridgesAndArticulationPoints(List<int>[] graph, int n)
    {
        _time = 0;
        var bridges = new List<Tuple<int, int>>();
        HashSet<int> articulationPoints = new();
        var visited = new bool[n];
        var disc = new int[n];
        var low = new int[n];
        var parent = new int[n];

        for (var i = 0; i < n; i++)
        {
            if (!visited[i])
            {
                DFS(graph, i, visited, disc, low, parent, bridges, articulationPoints);
            }
        }

        return (bridges, articulationPoints);
    }

    private void DFS(List<int>[] graph,
        int node,
        bool[] visited,
        int[] disc,
        int[] low,
        int[] parent,
        List<Tuple<int, int>> bridges,
        HashSet<int> articulationPoints)
    {
        visited[node] = true;
        disc[node] = low[node] = _time++;

        foreach (var neighbour in graph[node])
        {
            if (!visited[neighbour])
            {
                parent[neighbour] = node;
                DFS(graph, neighbour, visited, disc, low, parent, bridges, articulationPoints);
                low[node] = Math.Min(low[node], low[neighbour]);

                if (low[neighbour] > disc[node])
                {
                    bridges.Add(new Tuple<int, int>(node, neighbour));
                    articulationPoints.Add(node);
                }
            }
            else if (neighbour != parent[node])
            {
                low[node] = Math.Min(low[node], disc[neighbour]);
            }

            if (node != 0 && low[neighbour] >= disc[node])
                articulationPoints.Add(node);
        }
    }

    public void Start()
    {
        List<int>[] graph =
        {
            new() {1, 2}, //0
            new() {0, 2}, //1
            new() {0, 1, 3, 4}, //2
            new() {2, 4}, //3
            new() {2, 3} //4
        };
        var (bridges, articulationPoints) = FindBridgesAndArticulationPoints(graph, 5);

        Console.WriteLine("Bridges:");
        foreach (var bridge in bridges)
        {
            Console.WriteLine($"{bridge.Item1} --- {bridge.Item2}");
        }

        Console.WriteLine($"Articulation points:\n{string.Join(',', articulationPoints)}");
    }
}