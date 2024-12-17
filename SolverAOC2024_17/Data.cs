using AoCLib;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SolverAOC2024_17
{
  internal class Data : DataBase
  {


    public Computer Computer {  get; set; }

    public long ProgramLong { get; set; }

    public Data(string input) : base(input)
    {
      // optionaly parse data
      Regex r = new Regex("(\\d+)");
      int regA = int.Parse(r.Match(Lines[0]).Groups[1].Value);
      int regB = int.Parse(r.Match(Lines[1]).Groups[1].Value);
      int regC = int.Parse(r.Match(Lines[2]).Groups[1].Value);

      List<int> program = Lines[4].Split(' ')[1].Split(',').Select(x => int.Parse(x)).ToList();

      ProgramLong = long.Parse(string.Join("", program));

      Computer = new Computer(regA, regB, regC, program);

    }

    public object Solve1()
    {
      Computer.Run();
      return Computer.OutputString();
    }

    public object Solve2()
    {
      List<long> currentNumbers = new List<long>();

      for (int i = 1; i < 8; i++)
      {
        Computer.RegA = i;
        Computer.Output.Clear();
        Computer.InstructionPointer = 0;
        Computer.Run2();
        if (Computer.Output[0] == Computer.Program[Computer.Program.Count - 1])
        {
          currentNumbers.Add(i);
        }
      }


      for (int i = 0; i < Computer.Program.Count-1; i++)
      {
        List<long> nextNumbers = new List<long>();
        foreach (long number in currentNumbers) 
        {
          List<long> vals = GetNextNumbers(number, i+1);
          nextNumbers.AddRange(vals);
        }
        currentNumbers = nextNumbers;
        if(i == Computer.Program.Count-2)
        {
          return currentNumbers[0];
        }
      }

      return -1;
    }

    public List<long> GetNextNumbers(long regValue, int index)
    {
      List<long> res = new List<long>();
      long tmp = regValue * 8;
      for (long i = 0; i < 8; i++)
      {
        Computer.RegA = tmp + i;
        Computer.Output.Clear();
        Computer.InstructionPointer = 0;
        Computer.Run2();

        if (Computer.Output[0] == Computer.Program[Computer.Program.Count - 1 - index])
        {
          res.Add(tmp+i);
        }
      }
      return res;
    }

  }
}
