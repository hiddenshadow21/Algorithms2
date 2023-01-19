namespace labCS;

class Kmp
{
    private int[] _prefixFunction;
    private string _pattern;

    public Kmp(string pattern)
    {
        _pattern = pattern;
        _prefixFunction = new int[pattern.Length];
        var j = 0;
        for (var i = 1; i < pattern.Length; i++)
        {
            while (j > 0 && pattern[i] != pattern[j])
            {
                j = _prefixFunction[j - 1];
            }
            if (pattern[i] == pattern[j])
            {
                j++;
            }
            _prefixFunction[i] = j;
        }
    }

    public List<int> Match(string text)
    {
        var matches = new List<int>();
        var j = 0;
        for (var i = 0; i < text.Length; i++)
        {
            while (j > 0 && text[i] != _pattern[j])
            {
                j = _prefixFunction[j - 1];
            }
            if (text[i] == _pattern[j])
            {
                j++;
            }
            if (j == _pattern.Length)
            {
                matches.Add(i - _pattern.Length + 1);
                j = _prefixFunction[j - 1];
            }
        }
        return matches;
    }

    public static void Start()
    {
        var text = "ABABDABACDABABCABAB";
        var pattern = "ABABCABAB";
        var kmp = new Kmp(pattern);
        var matches = kmp.Match(text);
        foreach (var match in matches)
        {
            Console.WriteLine("Pattern found at index: " + match);
        }
    }
}