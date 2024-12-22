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


      string tmp = "A5";
      List<string> outputs = new List<string>();

      numericRobot.CodeMap["A5"] = "<^^A";
      outputs.Add(numericRobot.Solve(tmp));

      numericRobot.CodeMap["A5"] = "^<^A";
      outputs.Add(numericRobot.Solve(tmp));

      numericRobot.CodeMap["A5"] = "^^<A";
      outputs.Add(numericRobot.Solve(tmp));

      //numericRobot.CodeMap["7A"] = ">vv>vA";
      //outputs.Add(numericRobot.Solve(tmp));

      //numericRobot.CodeMap["7A"] = ">vvv>A";
      //outputs.Add(numericRobot.Solve(tmp));

      //numericRobot.CodeMap["7A"] = "v>>vvA";
      //outputs.Add(numericRobot.Solve(tmp));

      //numericRobot.CodeMap["7A"] = "v>v>vA";
      //outputs.Add(numericRobot.Solve(tmp));

      //numericRobot.CodeMap["7A"] = "v>vv>A";
      //outputs.Add(numericRobot.Solve(tmp));

      //numericRobot.CodeMap["7A"] = "vv>>vA";
      //outputs.Add(numericRobot.Solve(tmp));

      //numericRobot.CodeMap["7A"] = "vv>v>A";
      //outputs.Add(numericRobot.Solve(tmp));

      List<string> outputTmp = new List<string>();
      for (int i = 0; i <10; i++)
      {
        outputTmp = new List<string>();
        foreach (string s in outputs.ToList())
        {
          outputTmp.Add(dirRobot.Solve('A' + s));
        }
        outputs = outputTmp;
        
      }



      for (int i = 0; i < Lines.Count; i++)
      {
        string line = Lines[i];
       
        string output = numericRobot.Solve('A' + line);
        string output2 = dirRobot.Solve('A' + output);
        string output3 = dirRobot.Solve('A' + output2);

        sum += (output3.Length * long.Parse(line.Substring(0, line.Length - 1)));
      }

      return sum;
      
    }

    public object Solve2()
    {
      throw new NotImplementedException();
    }


  }
}
