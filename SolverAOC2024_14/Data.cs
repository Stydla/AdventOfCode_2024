using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2024_14
{
  internal class Data : DataBase
  {

    Bathroom Bathroom;

    public Data(string input) : base(input)
    {

      // optionaly parse data

      string size = Lines[0];
      string []vals = size.Split(' ');
      int width = int.Parse(vals[0]);
      int height = int.Parse(vals[1]);

      Bathroom = new Bathroom(width, height, Lines.Skip(1).ToList());

    }

    public object Solve1()
    {
      Bathroom.Move(100);
      return Bathroom.GetSafetyFactor();
    }

    public object Solve2()
    {
      while(true)
      {
        Bathroom.Move(1);
        if(Bathroom.IsEasterEgg())
        {
          //Bathroom.Print();
          return Bathroom.TotalMoves;
        }
      }
    }


  }
}
