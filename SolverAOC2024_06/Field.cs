using AoCLib;
using AoCLib.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2024_06
{
  internal class Field
  {
    public Point2D Location { get; set; }

    public char Value { get; set; }

   


    public Field(char value,  Point2D location)
    {
      Value = value;
      Location = location;
    }
  }
}
