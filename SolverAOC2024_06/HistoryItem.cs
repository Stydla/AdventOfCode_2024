using AoCLib;
using AoCLib.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2024_06
{
  internal class HistoryItem
  {

    public EDirection4 Direction { get; set; }
    public Point2D Location { get; set; }

    public HistoryItem(EDirection4 direction, Point2D location) 
    {
      Direction = direction;
      Location = location;
    }

    public override bool Equals(object obj)
    {
      if (obj is HistoryItem h)
      {
        return h.Direction == Direction && h.Location.Equals(Location);
      }
      return false;
    }

    public override int GetHashCode()
    {
      return Location.GetHashCode() + Direction.GetHashCode();
    }
  }
}
