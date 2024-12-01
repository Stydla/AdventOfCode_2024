using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SolverAOC2024_01
{
  internal class Data : DataBase
  {

    private List<int> L1 = new List<int>();
    private List<int> L2 = new List<int>();

    public Data(string input) : base(input)
    {

      Regex r = new Regex("^(\\d*)\\s*(\\d*)$");
      foreach(string line in Lines)
      {
        Match m = r.Match(line);
        L1.Add(int.Parse(m.Groups[1].Value));
        L2.Add(int.Parse(m.Groups[2].Value));
      }
      // optionaly parse data
    }

    public object Solve1()
    {
      L1.Sort();
      L2.Sort();
      int sum = 0;
      for(int i = 0; i < L1.Count; i++)
      {
        
        sum += Math.Abs(L1[i] - L2[i]);
      }
      return sum;
    }

    public object Solve2()
    {
      int sum = 0;
      foreach(var v in L1)
      {
        int cnt = L2.Where(x => x == v).Count();

        sum += (cnt * v);
      }
      return sum;
    }


  }
}
