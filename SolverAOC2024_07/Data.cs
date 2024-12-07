using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2024_07
{
  internal class Data : DataBase
  {

    public List<Equation> Equations = new List<Equation>();

    public Data(string input) : base(input)
    {
      foreach(string line in Lines)
      {
        Equations.Add(new Equation(line));
      }
    }

    public object Solve1()
    {

      long sum = 0;
      foreach(Equation eq in Equations)
      {
        if(eq.IsValid())
        {
          sum += eq.Result;
        }
      }

      return sum;
    }

    public object Solve2()
    {
      long sum = 0;
      foreach (Equation eq in Equations)
      {
        if (eq.IsValid2())
        {
          sum += eq.Result;
        }
      }

      return sum;
    }


  }
}
