using AoCLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2024_15
{
  internal class Field
  {
    public Point2D Location { get; set; }
    public char Value { get; set; }
    public Box Box { get; set; }

    public bool IsWall 
    {
      get
      {
        return Value == '#';
      }
    }

    public bool IsBox
    {
      get
      {
        return Value == 'O';
      }
    }

    public bool IsLargeBox
    {
      get
      {
        return Box != null;
      }
    }

    public Field(Point2D location, char value)
    {
      Location = location;
      Value = value;
    }






  }
}
