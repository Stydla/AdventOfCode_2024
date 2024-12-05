using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2024_05
{
  internal class Data : DataBase
  {

    
    List<Rule> Rules = new List<Rule>();
    List<List<int>> Updates = new List<List<int>>();

    public Data(string input) : base(input)
    {

      bool firstSection = true;

      foreach (var line in Lines)
      {
        if(string.IsNullOrEmpty(line))
        {
          firstSection = false;
          continue;
        }
        if (firstSection)
        {
          string[] pages = line.Split('|');
          Rule rule = new Rule();
          rule.From = int.Parse(pages[0]);
          rule.To = int.Parse(pages[1]);
          Rules.Add(rule);
        }
        else
        {
          Updates.Add(line.Split(',').Select(x => int.Parse(x)).ToList());
        }
      }
      // optionaly parse data
    }

    public object Solve1()
    {
      int sum = 0;
      foreach(var update in Updates)
      {
        if (Test(update))
        {
          sum += update[update.Count / 2];
        }
      }
      return sum;
    }

    private bool Test(List<int> update)
    {
      foreach (Rule r in Rules)
      {
        if (update.Contains(r.From) && update.Contains(r.To))
        {
          if (update.IndexOf(r.From) > update.IndexOf(r.To))
          {
            return false;
          }
        }
      }
      return true;
    }

    public object Solve2()
    {
      int sum = 0;
      foreach (var update in Updates)
      {
        if (!Test(update))
        {
          Fix(update);
          sum += update[update.Count / 2];
        }
      }
      return sum;
    }

    void Fix(List<int> update)
    {
      bool change = true;
      while (change)
      {
        change = false;
        foreach (Rule r in Rules)
        {
          if (update.Contains(r.From) && update.Contains(r.To))
          {
            int i1 = update.IndexOf(r.From);
            int i2 = update.IndexOf(r.To);
            if (i1 > i2)
            {
              update.RemoveAt(i1);
              update.Insert(i2, r.From);
              change = true;
              break;
            }
          }
        }
      }
    }


  }
}
