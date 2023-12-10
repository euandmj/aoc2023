using System.Text;
using static System.Console;

var board = ParseBoard("input.txt");
HashSet<(int begin, int end, int y)> parts = new();
for (int y = 0; y < board.Count; y++)
{
    for (int x = 0; x < board[0].Count; x++)
    {
        var isPart = board[y][x] switch
        {
            '.' => false,
            char c when char.IsDigit(c) => IsPart(x, y, board),
            _ => false
        };

        if (isPart)
        {
            var coords = GetPartNumber(x, board[y]);
            parts.Add((coords.Item1, coords.Item2, y));
        }
    }
}

int sum = 0;
foreach (var (begin, end, y) in parts)
{
    var row = board[y];
    StringBuilder num = new(row.Count);
    for (int i = begin; i <= end; i++)
    {
        num.Append(row[i]);
    }
    sum += int.Parse(num.ToString());
}

WriteLine(sum);

static (int, int) GetPartNumber(int x, List<char> row)
{
    // crawl back until nondigit.
    // crawl forwards until nondigit
    int begin = x, end = x;
    for (int xx = x; xx >= 0 && char.IsDigit(row[xx]); xx--)
    {
        begin = xx;
    }
    for (int xx = begin; xx < row.Count && char.IsDigit(row[xx]); xx++)
    {
        end = xx;
    }
    return (begin, end);
}

static bool IsPart(int x, int y, List<List<char>> board)
{
    foreach (var (yy, xx) in Util.Adjacents)
    {
        var coord = (y + yy, x + xx);
        if (coord.Item1 < 0 || coord.Item1 >= board.Count ||
            coord.Item2 < 0 || coord.Item2 >= board[0].Count)
        {
            continue;
        }
        var adjacent = board[coord.Item1][coord.Item2];
        if (!char.IsLetterOrDigit(adjacent) && adjacent != '.')
        {
            return true;
        }
    }
    return false;
}




static List<List<char>> ParseBoard(string v)
{
    var board = new List<List<char>>();
    foreach (var line in File.ReadAllLines(v))
    {
        var row = new List<char>(line.Length);
        foreach (var c in line)
        {
            row.Add(c);
        }
        board.Add(row);
    }
    return board;
}


public static class Util
{
    public static readonly (int, int)[] Adjacents = new (int, int)[]
    {
        (-1, -1),
        (-1,  0),
        (-1,  1),
        ( 0, -1),
        ( 0,  1),
        ( 1, -1),
        ( 1,  0),
        ( 1,  1)
    };
}