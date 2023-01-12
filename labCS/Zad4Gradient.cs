namespace labCS;

public class Zad4Gradient
{
    private int[] col;
    private int[] nw;
    private int[] ne;
    private const int Limit = 1000;

    public Zad4Gradient(int size)
    {
        var r = new Random();
        col = Enumerable.Range(0, size).OrderBy(x => r.Next()).ToArray();
        ne = new int[2 * size - 1];
        nw = new int[2 * size - 1];
        CalculateDiagonals();
    }

    private void CalculateDiagonals()
    {
        for (int i = 0; i < ne.Length; i++)
        {
            ne[i] = nw[i] = 0;
        }
        
        for (int x = 0; x < col.Length; x++)
        {
            var y = col[x];
            nw[y - x + col.Length - 1]++;
            ne[y + x]++;
        }
    }

    public void Start()
    {
        var r = new Random();
        var count = 0;
        while (Solve() == false)
        {
            col = Enumerable.Range(0, col.Length).OrderBy(r.Next).ToArray();
            CalculateDiagonals();
            count++;
        }

        PrintSolution();
        Console.WriteLine($"Changed starting position {count} times");
    }

    private bool Solve()
    {
        var count = 0;
        var conflicts = GetConflictsNumber();
        var startingConflict = conflicts;
        
        while (conflicts != 0)
        {
            if (count > 1 && conflicts == startingConflict)
                return false;
            startingConflict = conflicts;
            for (int i = 0; i < col.Length - 1; i++)
            {
                for (int j = i + 1; j < col.Length; j++)
                {
                    var c1 = i;
                    var c2 = j;
                    SwapColumns(c1, c2);
                    var newConflicts = GetConflictsNumber();
                    if (newConflicts > conflicts)
                        SwapColumns(c1, c2);
                    else
                        conflicts = newConflicts;
                }
            }
            count++;
        }
        
        Console.WriteLine($"Ran {count+1} iterations");
        return true;
    }

    private void SwapColumns(int x1, int x2)
    {
        var y1 = col[x1];
        var y2 = col[x2];

        //swap
        col[x1] = y2;
        col[x2] = y1;

        //remove from old pos
        nw[y1 - x1 + col.Length - 1]--;
        nw[y2 - x2 + col.Length - 1]--;
        ne[y1 + x1]--;
        ne[y2 + x2]--;

        //add in new pos
        nw[y1 - x2 + col.Length - 1]++;
        nw[y2 - x1 + col.Length - 1]++;
        ne[y1 + x2]++;
        ne[y2 + x1]++;
    }

    private int GetConflictsNumber()
    {
        var sum = 0;
        for (int i = 0; i < ne.Length; i++)
        {
            sum += ArithmeticSeqSum(ne[i]);
            sum += ArithmeticSeqSum(nw[i]);
        }

        return sum;
    }

    private int ArithmeticSeqSum(int n)
    {
        return n * (n - 1) / 2;
    }

    private void PrintSolution()
    {
        for (var i = 0; i < col.Length; i++)
        {
            var index = Array.IndexOf(col, i);
            Console.WriteLine("X"
                .PadLeft(index + 1, '-')
                .PadRight(col.Length, '-'));
        }
    }
}