using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2024_11
{
  internal class Data : DataBase
  {
    StoneLine StoneLine { get; set; }

    public Data(string input) : base(input)
    {
      // optionaly parse data
      StoneLine = new StoneLine(input);
    }

    public object Solve1()
    {
      return StoneLine.Blink(25);

    }

    public object Solve2()
    {
      return StoneLine.Blink(75);
    }


  }
}
