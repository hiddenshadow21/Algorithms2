namespace labCS;

public class Zad9
{
    private UnionFind _unionFind = new();
    public List<Query> Queries = new();

    public enum Color
    {
        White,
        Black
    }

    public class TreeNode
    {
        public int Value { get; }
        public TreeNode Ancestor { get; set; }
        public Color Color { get; set; }
        public List<TreeNode> Children { get; }

        public TreeNode(int value)
        {
            Value = value;
            Children = new List<TreeNode>();
            Color = Color.White;
        }

        public void AddChild(TreeNode child)
        {
            Children.Add(child);
        }

        public IEnumerable<TreeNode> GetAllNodes()
        {
            foreach (var child in Children)
            {
                yield return child;
                foreach (var node in child.GetAllNodes())
                {
                    yield return node;
                }
            }
        }
    }

    public class UnionFind
    {
        public class Subset
        {
            public TreeNode Node;
            public int Rank;
        }
        // Maps each element to its representative element
        private Dictionary<TreeNode, Subset> _representativeMap = new();

        public void MakeSet(TreeNode node)
        {
            _representativeMap[node] = new Subset
            {
                Node = node,
                Rank = 0
            };
        }
        
        public Subset Find(TreeNode node)
        {
            if (_representativeMap[node].Node == node)
                return _representativeMap[node];

            _representativeMap[node] = Find(_representativeMap[node].Node);
            return _representativeMap[node];
        }

        public void Union(TreeNode node1, TreeNode node2)
        {
            var rep1 = Find(node1);
            var rep2 = Find(node2);

            if (rep1.Rank > rep2.Rank)
            {
                _representativeMap[rep2.Node] = rep1;
            }
            else if (rep1.Rank < rep2.Rank)
            {
                _representativeMap[rep1.Node] = rep2;
            }
            else if (rep1.Node != rep2.Node)
            {
                _representativeMap[rep2.Node] = rep1;
                rep1.Rank += 1;
            }
        }
    }

    
    public void Tarjan(TreeNode u)
    {
        _unionFind.MakeSet(u);
        u.Ancestor = u;
        foreach (var v in u.Children)
        {
            Tarjan(v);
            _unionFind.Union(u, v);
            _unionFind.Find(u).Node.Ancestor = u;
        }

        u.Color = Color.Black;
        foreach (var q in Queries.Where(q => q.L == u).ToList())
        {
            if (q.R.Color == Color.Black)
                q.Result = _unionFind.Find(q.R).Node.Ancestor;
        }
        
        foreach (var q in Queries.Where(q => q.R == u).ToList())
        {
            if (q.L.Color == Color.Black)
                q.Result = _unionFind.Find(q.L).Node.Ancestor;
        }
    }
    
    public class Query
    {
        public TreeNode L, R, Result;

        public Query(TreeNode l, TreeNode r)
        {
            L = l;
            R = r;
        }
    }

    public void Start()
    {
        // Create the root node
        var root = new TreeNode(1);

// Create the child nodes
        var node2 = new TreeNode(2);
        var node3 = new TreeNode(3);
        var node4 = new TreeNode(4);
        var node5 = new TreeNode(5);
        var node6 = new TreeNode(6);

// Add the child nodes to the root node
        root.AddChild(node2);
        root.AddChild(node3);

// Add the grandchildren to the root node
        node2.AddChild(node4);
        node3.AddChild(node5);
        node3.AddChild(node6);

        Queries.Add(new Query(root, node2));
        Queries.Add(new Query(node2, node3));
        Queries.Add(new Query(node5, node6));
        Tarjan(root);
        foreach (var query in Queries)
        {
            Console.WriteLine($"({query.L.Value}, {query.R.Value}) = {query.Result.Value}");
        }
    }
}