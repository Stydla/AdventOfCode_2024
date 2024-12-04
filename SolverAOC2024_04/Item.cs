using AoCLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2024_04
{
  internal class Item
  {

    public Point2D Position { get; set; }
    public char Value { get; set; } 

    public Item(char value, Point2D position) 
    { 
      Value = value;
      Position = position;
    }

  }
}
