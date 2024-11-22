using System;
using System.Collections.Generic;
using System.Linq;
namespace AoCHelper;
public class ProblemLoader
{
  public static (Type, Func<object?>)[] LoadAllProblems()
  {
    return new (Type, Func<object?>)[]
    {
      (typeof(AoCHelper.PoC.Problem03), () => new AoCHelper.PoC.Problem03()),
      (typeof(AoCHelper.PoC.Problem04), () => new AoCHelper.PoC.Problem04()),
      (typeof(AoCHelper.PoC.RandomProblem), () => new AoCHelper.PoC.RandomProblem())
    };
  }
}
