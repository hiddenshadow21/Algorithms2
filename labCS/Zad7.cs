namespace labCS;

class Zad7
{
    // Number of variables
    private readonly int _v;

    // Array of lists for Adjacency List Representation
    private readonly List<int>[] _adj;

    private List<List<int>> _sccs;

    // Function that returns true if the 2-SAT problem is satisfiable
    public bool IsSatisfiable()
    {
        _sccs = new StronglyConnectedComponents(_v, _adj).GetSccs();

        // Check if there are any SCCs with both x1 and x2
        for (var i = 0; i < _sccs.Count; i++)
        {
            var scc = _sccs[i];
            if (scc.Contains(scc[0] ^ 1))
                return false;
        }
        
        Console.WriteLine("scc:");
        foreach (var scc in _sccs)
        {
            Console.WriteLine(string.Join(' ', scc));
        }

        return true;
    }
    
    // Function that returns the values of the variables in the 2-SAT problem
    public bool[] GetVariableValues()
    {
        var variableValues = new bool[_v];
        for (var i = 0; i < _sccs.Count; i++)
        {
            var scc = _sccs[i];
            var v = scc[0];
            variableValues[v] = true;
            variableValues[v ^ 1] = false;
        }

        return variableValues;
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
        var edges = new List<int>[v];
        for (var i = 0; i < v; i++)
            edges[i] = new List<int>();

// Add edges to the graph based on the clauses in the 2-SAT problem
        edges[0].Add(2);
        edges[1].Add(2);
        edges[3].Add(0);
        edges[3].Add(1);

// Create an instance of the class
        var solver = new Zad7(v, edges);

// Check if the 2-SAT problem is satisfiable
        var result = solver.IsSatisfiable();

        if (result)
        {
            Console.WriteLine("The 2-SAT problem is satisfiable.");
            var variableValues = solver.GetVariableValues();
            for (int i = 0; i < variableValues.Length; i++)
            {
                Console.WriteLine($"x{i}: {variableValues[i]}");
            }
        }
        else
            Console.WriteLine("The 2-SAT problem is not satisfiable.");

    }
}