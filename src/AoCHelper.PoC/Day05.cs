namespace AoCHelper.PoC;

public class Day05 : BaseDay
{
    public Day05()
    {
       // throw new SolvingException("Exception in constructor");
    }

    public override ValueTask<string> Solve_1()
    {
        Thread.Sleep(66);
        return new("Solution 5.1");
    }

    public override ValueTask<string> Solve_2()
    {
        Thread.Sleep(6600);
        return new("Solution 5.2");
    }
}
