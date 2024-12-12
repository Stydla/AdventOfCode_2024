using AoCLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2024_12
{
  internal class Field
  {

    public Point2D Location { get; set; }
    public char Value { get; set; }

    public Field(Point2D location, char value)
    {
      Location = location;
      Value = value;
    }

  }
}
