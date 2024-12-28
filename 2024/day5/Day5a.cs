using System.Data;

public class Day05a : Day
{
    private class Rule
    {
        public int Before = 0;
        public int After = 0;
        public Rule(string ruleText)
        {
            var split = ruleText.Split("|");
            Before = int.Parse(split[0]);
            After = int.Parse(split[1]);
        }
    }
    public override void Run()
    {
        var input = puzzleInput;

        var parse_rules = true;
        var rules = new List<Rule>();
        var updates = new List<int[]>();
        foreach (var line in input)
        {
            if (line == "")
            {
                parse_rules = false;
            }
            else if (parse_rules)
            {
                rules.Add(new Rule(line));
            }
            else
            {
                updates.Add(line.Split(",").Select(int.Parse).ToArray());
            }
        }

        var rules_by_before = rules.ToLookup(x => x.Before, x => x.After);
        var sum = 0;
        foreach (var update in updates)
        {
            if (isValid(update, rules_by_before))
            {
                print("Valid");
                sum += update[(update.Length - 1) / 2];
            } else {
                print("InValid");
            }
        }
        print(sum);
    }

    private bool isValid(int[] update, ILookup<int, int> after_by_before)
    {
        var prev_pages = new List<int>();
        foreach (var page in update)
        {
            foreach (int after in after_by_before[page])
            {
                if (prev_pages.Contains(after)) return false;
            }
            prev_pages.Add(page);
        }
        return true;
    }

    // public int checkXMAS(int x, int y, int dx, int dy)
    // {

    //     foreach (var letter in "XMAS")
    //     {
    //         if (grid[x, y] != letter) return 0;
    //         x += dx;
    //         y += dy;
    //     }
    //     return 1;
    // }
}

