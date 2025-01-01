public interface IDay {
    void Init(string[] sample, string[] input);
}

public abstract class Day:IDay {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    protected string[] samplePuzzleInput;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    protected string[] puzzleInput;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

    public abstract void Run();

    public void Init(string[] samplePuzzleInput, string[] puzzleInput) {
            this.samplePuzzleInput = samplePuzzleInput;
            this.puzzleInput = puzzleInput;
        }

protected void print(object text) {
        Console.WriteLine(text);
    }
}