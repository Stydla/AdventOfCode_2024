using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2024_15
{
  internal class Data : DataBase
  {
    public Data(string input) : base(input)
    {
      // optionaly parse data

      
    }

    public object Solve1()
    {
      Map map = new Map(Lines);
      return map.Solve();
    }

    public object Solve2()
    {
      List<string> newLines = Lines.ToList();
      for(int i = 0; i < newLines.Count; i++)
      {
        string line = newLines[i];
        StringBuilder sbNewLine = new StringBuilder();
        for (int j = 0; j < line.Length; j++)
        {
          char c = line[j];
          switch(c)
          {
            case '#':
              sbNewLine.Append("##");
              break;
            case 'O':
              sbNewLine.Append("[]");
              break;
            case '@':
              sbNewLine.Append("@.");
              break;
            case '.':
              sbNewLine.Append("..");
              break;
            default:
              sbNewLine.Append(c);
              break;
          }
        }
        newLines[i] = sbNewLine.ToString();  
      }

      Map map = new Map(newLines);
      return map.Solve2();
    }


  }
}

