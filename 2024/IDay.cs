public interface IDay {
    void Init(string[] sample, string[] input);
}

public abstract class Day:IDay {
    protected string[] samplePuzzleInput;
    protected string[] puzzleInput;

    public abstract void Run();

    public void Init(string[] samplePuzzleInput, string[] puzzleInput) {
            this.samplePuzzleInput = samplePuzzleInput;
            this.puzzleInput = puzzleInput;
        }

protected void print(object text) {
        Console.WriteLine(text);
    }
}