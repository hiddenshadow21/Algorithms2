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
        _sccs = new StronglyConnectedComponents(_v, _adj).GetScCs();

        // Check if there are any SCCs with both x1 and x2
        foreach (var scc in _sccs)
        {
            foreach (var node in scc)
            {
                if (scc.Contains(ReversedValue(node)))
                    return false;
            }
        }

        Console.WriteLine("scc:");
        foreach (var scc in _sccs)
        {
            Console.WriteLine(string.Join(' ', scc.Select(GetValueFromIndex)));
        }

        return true;
    }

    private int GetValueFromIndex(int index)
    {
        if (index < _v / 2)
            return index + 1;

        return index - _v;
    }

    // Function that returns the values of the variables in the 2-SAT problem
    public int[] GetVariableValues()
    {
        var variableValues = new int[_v];
        foreach (var scc in _sccs)
        {
            foreach (var node in scc)
            {
                if (variableValues[node] != 0)
                {
                    variableValues[ReversedValue(node)] = variableValues[node] * -1;
                    continue;
                }

                variableValues[node] = 1;
                variableValues[ReversedValue(node)] = -1;
            }
        }

        return variableValues;
    }

    // Constructor
    public Zad7(int v, List<Tuple<int, int>> problem)
    {
        _v = v;

// Array of lists for Adjacency List Representation
        var edges = new List<int>[v * 2 + 1];
        for (var i = 0; i < v * 2; i++)
            edges[i] = new List<int>();

// Add edges to the graph based on the clauses in the 2-SAT problem
        foreach (var tuple in problem)
        {
            edges[GetIndex(tuple.Item1 * -1)].Add(GetIndex(tuple.Item2));
            edges[GetIndex(tuple.Item2 * -1)].Add(GetIndex(tuple.Item1));
        }

        _adj = edges;
    }

    public static void Start()
    {
        var problem = new List<Tuple<int, int>>
        {
            new(1, 2),
            new(-1, 3),
            new(-3,2)
        };
        // Number of variables
        var v = 3;
        var solver = new Zad7(v * 2, problem);

        // Check if the 2-SAT problem is satisfiable
        var result = solver.IsSatisfiable();

        if (result)
        {
            Console.WriteLine("The 2-SAT problem is satisfiable.");
            var variableValues = solver.GetVariableValues();
            for (int i = 0; i < variableValues.Length; i++)
            {
                Console.WriteLine($"x{(i < v ? i + 1 : i - v * 2)}: {variableValues[i]}");
            }
        }
        else
            Console.WriteLine("The 2-SAT problem is not satisfiable.");
    }

    public int GetIndex(int value)
    {
        if (value > 0)
            return value - 1;

        return _v + value;
    }

    private int ReversedValue(int value)
    {
        return _v - value - 1;
    }
}