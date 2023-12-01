using static System.Console;


var x = Sum();

WriteLine(x);

static int Sum()
{
    int sum = 0;
    foreach (var line in File.ReadAllLines("input.txt"))
    {
        char a = GetFirstInteger(line), b = GetLastInteger(line);
        var value = int.Parse($"{a}{b}");
        sum += value;
    }
    return sum;
}

static char GetFirstInteger(ReadOnlySpan<char> chars)
{
    for (int i = 0; i < chars.Length; ++i)
    {
        if (char.IsDigit(chars[i]))
        {
            return chars[i];
        }
    }
    throw new Exception("no valid digit found in " + chars.ToString());
}

static char GetLastInteger(ReadOnlySpan<char> chars)
{
    for (int i = chars.Length - 1; i >= 0; --i)
    {
        if (char.IsDigit(chars[i]))
        {
            return chars[i];
        }
    }
    throw new Exception("no valid digit found in " + chars.ToString());
}