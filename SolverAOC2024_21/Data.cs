using System;
using System.Collections.Generic;
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

      NumericRobot numericRobot = new NumericRobot();
      DirectionRobot dirRobot = new DirectionRobot();


      long sum = 0;

      for (int i = 0; i < Lines.Count; i++)
      {
        string line = Lines[i];

        List<string> current = new List<string>();
        current.Add(line);

        current = SolveNext(current, numericRobot);
        current = SolveNext(current, dirRobot);
        current = SolveNext(current, dirRobot);

        int len = current.Min(x => x.Length); 
        
        sum += (len * long.Parse(line.Substring(0, line.Length - 1)));
      }

      return sum;
      
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
      throw new NotImplementedException();
    }


  }
}
