using System.Numerics;
using System.Xml.XPath;

public class Day24a : Day
{
    public override void Run()
    {
        var sample = false;
        var input = sample ? samplePuzzleInput : puzzleInput;

        foreach (var line in input.TakeWhile(x => x != ""))
        {
            var split = line.Split(":");
            Piece.pieces[split[0]] = new StaticPiece(split[1].Trim() == "1");
        }

        foreach (var line in input.SkipWhile(x => x != "").Skip(1))
        {

            var split1 = line.Split(" -> ");
            var split2 = split1[0].Split(" ");
            var piece = split2[1] switch
            {
                "XOR" => (Piece)new XorPiece(split2[0], split2[2]),
                "AND" => new AndPiece(split2[0], split2[2]),
                "OR" => new OrPiece(split2[0], split2[2]),
                _ => throw new Exception("Unknown")
            };
            Piece.pieces[split1[1]] = piece;
        }

        ulong result = 0;

        foreach (var z_piece_key in Piece.pieces.Keys.Where(x => x.StartsWith("z")).OrderByDescending(x => x))
        {
            result = result * 2 + (ulong)(Piece.pieces[z_piece_key].Out ? 1 : 0);
        }

        print(result);
    }

}
public abstract class Piece
{
    public static Dictionary<string, Piece> pieces = [];
    public abstract bool Out { get; }
}
public class AndPiece(string pieceName1, string pieceName2) : Piece
{
    public override bool Out => pieces[pieceName1].Out && pieces[pieceName2].Out;
}
public class OrPiece(string pieceName1, string pieceName2) : Piece
{
    public override bool Out => pieces[pieceName1].Out || pieces[pieceName2].Out;
}
public class XorPiece(string pieceName1, string pieceName2) : Piece
{
    public override bool Out => pieces[pieceName1].Out ^ pieces[pieceName2].Out;
}
public class StaticPiece(bool value) : Piece
{
    public override bool Out { get; } = value;
}
