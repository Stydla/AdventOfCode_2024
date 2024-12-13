using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2024_13
{
  internal class Data : DataBase
  {

    public List<ClawMachine> ClawMachines = new List<ClawMachine>();

    public Data(string input) : base(input)
    {
      // optionaly parse data

      int currentLine = 0;

      while (currentLine < Lines.Count)
      {
        ClawMachine cm = new ClawMachine(Lines, ref currentLine);
        ClawMachines.Add(cm);
      }

    }

    public object Solve1()
    {
      int sum = 0;
      foreach(var cm in ClawMachines)
      {
        bool found = false;
        int tokens = cm.MinimumToknes(100, out found);
        if(found)
        {
          sum += tokens;
        }
      }
      return sum;
    }

    public object Solve2()
    {
      long sum = 0;
      foreach(var cm in ClawMachines)
      {
        bool found = false;
        long tokens = cm.MinimumToknes(out found);
        if (found)
        {
          sum += tokens;
        }
      }

      return sum;
    }


  }
}
