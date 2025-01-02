using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2024_21
{
  internal class Data : DataBase
  {

    

    public Data(string input) : base(input)
    {
      // optionaly parse data
    }

    public object Solve1()
    {

      int cnt = 2;

      return Solve(cnt + 1);

    }

    private Robot GetRobot(int depth)
    {
      List<Robot> robots = new List<Robot>();

      for (int i = 0; i < depth; i++)
      {
        Robot tmp = new Robot();
        robots.Add(tmp);

        if (i > 0)
        {
          tmp.Input = robots[i - 1];
        }
      }

      return robots.Last();
    }

    private List<string> SolveNext(List<string> inputs, RobotBase robot)
    {
      List<string> result = new List<string>();
      foreach (var input in inputs)
      {
        List<string> output = robot.Solve('A' + input);
        result.AddRange(output);
      }
      int min = result.Min(x => x.Length);
      var result2 = result.Where(x => x.Length == min);
      return result2.Distinct().ToList();
    }

    public object Solve2()
    {
      int cnt = 25;

      return Solve(cnt + 1);
    }

    private long Solve(int cnt)
    {
      NumericRobot numericRobot = new NumericRobot();

      long sum2 = 0;
      for (int i = 0; i < Lines.Count; i++)
      {
        string line = Lines[i];

        List<string> current = new List<string>();
        current.Add(line);

        current = SolveNext(current, numericRobot);

        List<long> resLen = new List<long>();
        foreach (string s in current)
        {
          long res = GetRobot(cnt).Solve(s);
          resLen.Add(res);
        }

        long len = resLen.Min();

        sum2 += (len * long.Parse(line.Substring(0, line.Length - 1)));
      }

      return sum2;
    }


  }
}
