using AoCHelper;
using AoCHelper.PoC.Library;

internal class Program
{
    private static async Task Main(string[] args)
    {
        await Solver.SolveAll(static options =>
{
    options.ShowConstructorElapsedTime = true;
    options.ShowTotalElapsedTimePerDay = true;
    options.ShowOverallResults = true;
    options.ClearConsole = false;
    options.ProblemAssemblies = [System.Reflection.Assembly.GetAssembly(typeof(BaseLibraryDay))!, .. options.ProblemAssemblies];
    options.ProblemLoader = ProblemLoader.LoadAllProblems;
});
    }
}