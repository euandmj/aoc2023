using static System.Console;


int sum = 0;
int product = 0;
foreach (var line in File.ReadAllLines("input.txt"))
{
    var game = ParseGame(line);
    // if(game.IsPossibleWith(12, 13, 14))
    // {
    //     sum += game.Index;
    // }
    var (r,g,b) = game.GetMinimumCubes();
    product += r*g*b;
}

WriteLine(product);

static Game ParseGame(string input)
{
    var index = int.Parse(
        input[4..input.IndexOf(':')]
    );
    var sets = ParseSets(input);
    return new Game(index, sets);
}

static IEnumerable<Set> ParseSets(string input)
{
    input = input[(input.IndexOf(':') + 1)..];
    var sets = input.Split(';');
    foreach (var set in sets)
    {
        var bricks = set.Split(',');
        yield return ParseSet(bricks);
    }
}

static Set ParseSet(string[] bricks)
{
    static string GetNum(string input) => input[..input.IndexOf(' ')];
    int r = default, g = default, b = default;

    foreach (var brick in bricks)
    {
        if (brick.EndsWith("red"))
        {
            r = int.Parse(GetNum(brick.Trim()));
        }
        else if (brick.EndsWith("green"))
        {
            g = int.Parse(GetNum(brick.Trim()));
        }
        else if (brick.EndsWith("blue"))
        {
            b = int.Parse(GetNum(brick.Trim()));
        }
    }
    return new(r, g, b);
}

readonly struct Game
{
    public Game(int index, IEnumerable<Set> sets)
        => (Index, Sets) = (index, sets.ToList());
    public readonly int Index;
    public readonly List<Set> Sets;

    public bool IsPossibleWith(int r, int g, int b)
    {
        return Sets.All(set =>
        {
            return set.Red <= r && set.Green <= g && set.Blue <= b;
        });
    }

    public (int, int, int) GetMinimumCubes()
    {
        int r = 0, g = 0, b = 0;
        foreach(var set in Sets)
        {
            r = Math.Max(r, set.Red);
            g = Math.Max(g, set.Green);
            b = Math.Max(b, set.Blue);
        }
        return (r,g,b);
    }
}

readonly record struct Set(int Red, int Green, int Blue);