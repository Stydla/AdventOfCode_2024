using AoCLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2024_14
{
  internal class Robot
  {
    public Point2D Location { get; set; }
    public Point2D Velocity { get; set; }

    public Robot(Point2D location, Point2D velocity)
    {
      Location = location;
      Velocity = velocity;
    }

  }
}
