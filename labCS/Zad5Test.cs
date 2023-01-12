namespace labCS;

public class Zad5Test
{
    public void Start()
    {
        var hash = new Zad5(8);
        Console.WriteLine("List 1");
        hash.PrintArray();

        var objs = new object[]
        {
            2.4f, 245.6f, 3.5435f, 45.2f, 34.5f, 7.223f, 824.6345f, 83451.8f, 931.4f, 1031.78f,
            1.4, 2.1, 3.3, 4.2, 5.4, 6.2, 7.6, 8.8, 9.456, 10.7568,
            "abc", "acb", "cba", "cab", "text", "xett", "ttxe", "input"
        };

        hash.Add(objs);
        Console.WriteLine("\n\n\n\nList 2");

        hash.PrintArray();

        var list = new List<Product>
        {
            new()
            {
                Cost = 123,
                Count = 2,
                Name = "aaaa"
            },
            new()
            {
                Cost = 1.23,
                Count = 1,
                Name = "asdfaa"
            },
            new()
            {
                Cost = 12.3,
                Count = 4,
                Name = "dgh"
            },
            new()
            {
                Cost = 12.4,
                Count = 3,
                Name = "ngsn"
            },
            new()
            {
                Cost = 84.34,
                Count = 5,
                Name = "asdfasdg"
            },
            new()
            {
                Cost = 234.31,
                Count = 2,
                Name = "zcvga"
            },
            new()
            {
                Cost = 134.34,
                Count = 6353,
                Name = "zsdgae"
            }
        };
        
        hash.Add(list.ToArray());
        
        Console.WriteLine("\n\n\n\nList 3");
        hash.PrintArray();
        
        hash.Remove(objs);
        Console.WriteLine("\n\n\n\nList 3");
        hash.PrintArray();
    }
}