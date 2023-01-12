namespace labCS;

public class Product
{
    public string Name { get; set; }
    public int Count { get; set; }
    public double Cost { get; set; }

    public override string ToString()
    {
        return $"(C:{Cost};A:{Count};N:{Name})";
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Product p)
            return false;

        return p.Count == Count && p.Cost == Cost && p.Name == Name;
    }
}

public class Zad5
{
    private uint _totalCount;
    private List<object>[] _array;
    private readonly uint[] _bigPrimes = {6700417, 524287, 2147483647};

    private uint Size => (uint) _array.Length;

    public Zad5(int size)
    {
        _array = InitArray(size);
    }

    private static List<object>[] InitArray(int size)
    {
        var array = new List<object>[size];
        for (int i = 0; i < size; i++)
        {
            array[i] = new List<object>();
        }

        return array;
    }


    #region Hashes

    private uint GetHash(string s)
    {
        ulong val = 0;
        ulong pow = 1;
        const uint q = 33;
        foreach (var item in s)
        {
            val += pow * (ulong)(item - 'a');
            pow *= q;
            val ^= _bigPrimes[2];
        }
        
        return (uint)(val % Size);
    }

    private uint GetHash(int x)
    {
        var val = (uint) ((x >> 16) ^ x) * 0x45d9f3b;
        val = ((val >> 16) ^ val) * 0x45d9f3b;
        val = (val >> 16) ^ val;
        return val % Size;
    }

    private uint GetHash(float f)
    {
        var bytes = BitConverter.GetBytes(f);
        var newValue = BitConverter.ToInt32(bytes, 0);
        var value = GetHash(newValue);

        return value % Size;
    }

    private uint GetHash(double d)
    {
        var factor = 0.2147483647;
        var value = (uint) Math.Floor(Size * (factor * d % 1));

        return value % Size;
    }

    private uint GetHash(long l)
    {
        var factor = 0.2147483647;
        var value = (uint) Math.Floor(Size * (factor * l % 1));

        return value % Size;
    }

    private uint GetHash(Product product)
    {
        var value = GetHash(product.Cost);
        value += GetHash(product.Name);
        value += GetHash(product.Count);

        return value % Size;
    }

    private uint GetHashValue(object obj)
    {
        return obj switch
        {
            string s => GetHash(s),
            double d => GetHash(d),
            float f => GetHash(f),
            int i => GetHash(i),
            long l => GetHash(l),
            Product p => GetHash(p),
            _ => 0
        };
    }

    #endregion

    public void Add(params object[] objects)
    {
        var ratio = (_totalCount + (uint) objects.Length) / (float) _array.Length;
        if (ShouldResize(ratio))
            Resize(ratio);

        foreach (var obj in objects)
        {
            var value = GetHashValue(obj);

            if (_array[value].Contains(obj))
                continue;

            _array[value].Add(obj);
            _totalCount++;
        }
    }

    private void Resize(float ratio)
    {
        var newSize = (int) Size;
        if (ratio > 1)
            newSize *= (int) Math.Ceiling(ratio);
        else
            newSize /= 2;

        var objects = _array.SelectMany(list => list.Select(o => o)).ToList();
        _array = InitArray(newSize);

        foreach (var o in objects)
        {
            Add(o);
        }
    }

    private bool ShouldResize(float ratio)
    {
        return ratio > 1.5 || ratio < 0.3;
    }

    public void Remove(params object[] objects)
    {
        foreach (var obj in objects)
        {
            var value = GetHashValue(obj);

            var status = _array[value].Remove(obj);
            if (status)
                _totalCount--;
        }

        var ratio = _totalCount / (float) _array.Length;
        if (ShouldResize(ratio))
            Resize(ratio);
    }

    public void PrintArray()
    {
        for (var index = 0; index < _array.Length; index++)
        {
            var list = _array[index];
            Console.WriteLine($"{index}: {String.Join(';', list.Select(x => x.ToString()).ToList())}");
        }
    }
}