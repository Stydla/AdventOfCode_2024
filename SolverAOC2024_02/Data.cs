using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2024_02
{
  internal class Data : DataBase
  {

    List<List<int>> Reports = new List<List<int>>();

    public Data(string input) : base(input)
    {
      foreach (string line in Lines)
      {
        List<int> l = new List<int>();
        foreach (string s in line.Split(' '))
        {
          l.Add(int.Parse(s));
        }
        Reports.Add(l);
      }
    }

    public object Solve1()
    {

      int safe = 0;
      foreach (List<int> r in Reports)
      {
        bool tmpSafe = SolveReport(r);


        if (tmpSafe)
        {
          safe++;
        }

      }

      return safe;

    }


    private bool SolveReport(List<int> report)
    {
      bool tmpSafe = true;
      for (int i = 0; i < report.Count - 1; i++)
      {
        int i1 = report[i];
        int i2 = report[i + 1];

        int diff = Math.Abs(i1 - i2);
        if (diff < 1 || diff > 3)
        {
          tmpSafe = false;
        }
      }

      List<int> o1 = report.OrderBy(x => x).ToList();
      List<int> o2 = report.OrderByDescending(x => x).ToList();
      if (!Compare(report, o1) && !Compare(report, o2))
      {
        tmpSafe = false;
      }

      return tmpSafe;
    }


    bool Compare(List<int> a, List<int> b)
    {
      for (int i = 0; i < a.Count; i++)
      {
        if (a[i] != b[i])
        {
          return false;
        }
      }
      return true;
    }

    public object Solve2()
    {

      int safe = 0;
      foreach (List<int> r in Reports)
      {
        bool tmpSafe = SolveReport(r);


        if (tmpSafe)
        {
          safe++;
        }
        else
        {
          for (int i = 0; i < r.Count; i++)
          {
            List<int> tmp = new List<int>(r);
            tmp.RemoveAt(i);
            if (SolveReport(tmp))
            {
              safe++;
              break;
            }
          }
        }

        

      }
      return safe;
    }
  }
}
