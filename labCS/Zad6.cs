namespace labCS;

public class Edge
{
    public int Node1 { get; set; }
    public int Node2 { get; set; }
    public int Weight { get; set; }
}

public class Zad6
{
    private int _size;
    private int[] _representatives;
    private List<Edge> _edges = new();

    public Zad6(string file)
    {
        Read(file);
    }

    private void Read(string path)
    {
        var input = File.ReadAllLines(path).ToList();
        _size = int.Parse(input[0]);
        _representatives = Enumerable.Repeat(-1, _size).ToArray();
        input.RemoveAt(0);
        
        foreach (var line in input)
        {
            var splitted = line.Split(' ');
            _edges.Add(new Edge
            {
                Node1 = int.Parse(splitted[0]),
                Node2 = int.Parse(splitted[1]),
                Weight = int.Parse(splitted[2])
            });
        }

        _edges = _edges.OrderBy(x => x.Weight).ToList();
    }

    public void Start()
    {
        var mst = new List<Edge>(_size);
        
        foreach (var edge in _edges)
        {
            if(CycleOccured(edge))
                continue;
            
            mst.Add(edge);
            Union(edge);
        }

        mst = mst.OrderBy(x => x.Node1).ToList();
        foreach (var edge in mst)
        {
            Console.WriteLine($"{edge.Node1} - {edge.Node2}");
        }
        
        Console.WriteLine(string.Join(' ', _representatives));
    }

    private void Union(Edge edge)
    {
        var r = FindRepresentativeWithCollapse(edge.Node2);
        _representatives[r - 1] = edge.Node1;
    }

    private bool CycleOccured(Edge edge)
    {
        var r1 = FindRepresentativeWithCollapse(edge.Node1);
        var r2 = FindRepresentativeWithCollapse(edge.Node2);

        return r1 == r2;
    }

    private int FindRepresentativeWithCollapse(int nodeNumber)
    {
        if (_representatives[nodeNumber - 1] < 0)
            return nodeNumber;
        
        _representatives[nodeNumber - 1] = FindRepresentativeWithCollapse(_representatives[nodeNumber - 1]);
        
        return _representatives[nodeNumber - 1];
    }
}