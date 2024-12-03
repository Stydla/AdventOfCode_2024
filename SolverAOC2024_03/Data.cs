using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SolverAOC2024_03
{
  internal class Data : DataBase
  {

    public List<Item> Items = new List<Item>();

    public Data(string input) : base(input)
    {
      // optionaly parse data

      Regex r = new Regex("mul\\((\\d*),(\\d*)\\)|(don\\'t\\(\\))|(do\\(\\))");
      
        bool process = true;
      foreach (string line in Lines)
      {
        foreach(Match m in r.Matches(line))
        {
          if (m.Groups[1].Success)
          {
            Item item = new Item();
            item.Num1 = int.Parse(m.Groups[1].Value);
            item.Num2 = int.Parse(m.Groups[2].Value);
            item.Process = process;

            Items.Add(item);
          }
          else if (m.Groups[3].Success) //dont
          {
            process = false;
          }
          else if(m.Groups[4].Success) // do
          {
            process = true;
          }
        }
      }

    }

    public object Solve1()
    {
      int res = 0;
      foreach(Item item in Items)
      {
        res += (item.Num1 * item.Num2);
      }
      return res;
    }

    public object Solve2()
    {
      int res = 0;
      foreach (Item item in Items.Where(x=>x.Process == true))
      {
        res += (item.Num1 * item.Num2);
      }
      return res;
    }


  }
}
