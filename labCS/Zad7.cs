namespace labCS;

class Zad7
{
    // Number of variables
    private readonly int _v;

    // Array of lists for Adjacency List Representation
    private readonly List<int>[] _adj;

    // Function that returns true if the 2-SAT problem is satisfiable
    public bool IsSatisfiable()
    {
        var sccs = new StronglyConnectedComponents(_v, _adj).GetSccs();

        // Check if there are any SCCs with both x1 and x2
        for (var i = 0; i < sccs.Count; i++)
        {
            var scc = sccs[i];
            if (scc.Contains(scc[0] ^ 1))
                return false;
        }

        return true;
    }

    // Constructor
    public Zad7(int v, List<int>[] edges)
    {
        _v = v;
        _adj = edges;
    }

    public static void Start()
    {
        // Number of variables
        var v = 4;

// Array of lists for Adjacency List Representation
        List<int>[] edges = new List<int>[v];
        for (var i = 0; i < v; i++)
            edges[i] = new List<int>();

// Add edges to the graph based on the clauses in the 2-SAT problem
        edges[0].Add(1);
        edges[2].Add(3);
        edges[1].Add(2);
        edges[3].Add(0);

// Create an instance of the class
        var solver = new Zad7(v, edges);

// Check if the 2-SAT problem is satisfiable
        var result = solver.IsSatisfiable();

        Console.WriteLine(result ? "The 2-SAT problem is satisfiable." : "The 2-SAT problem is not satisfiable.");
    }
}