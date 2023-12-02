using static System.Console;

var x = Sum();

WriteLine(x);

static int Sum()
{
    int sum = 0;
    foreach (var line in File.ReadAllLines("input.txt"))
    {
        int a = GetFirstInteger(line);
        int b = GetLastInteger(line);
        var value = int.Parse($"{a}{b}");
        sum += value;
    }
    return sum;
}

static int GetFirstInteger(ReadOnlySpan<char> chars)
{
    for (int i = 0; i < chars.Length; ++i)
    {
        if (char.IsDigit(chars[i]))
        {
            return chars[i] - 48;
        }
        if (TryParseNumber(chars[i..], out var num))
        {
            return num;
        }
    }
    throw new Exception("no valid digit found in " + chars.ToString());
}
static int GetLastInteger(ReadOnlySpan<char> chars)
{
    for (int i = chars.Length - 1; i >= 0; --i)
    {
        if (char.IsDigit(chars[i]))
        {
            return chars[i] - 48;
        }
        if (TryParseNumber(chars[i..], out var num))
        {
            return num;
        }
    }
    throw new Exception("no valid digit found in " + chars.ToString());
}

static bool TryParseNumber(ReadOnlySpan<char> chars, out int num)
{
    num = default;
    if (chars.Length < 3)
    {
        return false;
    }
    for (int i = 3; i <= chars.Length; i++)
    {
        num = chars[..i] switch
        {
            "one" => 1,
            "two" => 2,
            "three" => 3,
            "four" => 4,
            "five" => 5,
            "six" => 6,
            "seven" => 7,
            "eight" => 8,
            "nine" => 9,
            _ => -1,
        };
        if (num != -1)
        {
            return true;
        }
    }
    return false;
}
