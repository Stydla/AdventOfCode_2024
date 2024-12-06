using AoCLib;
using AoCLib.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2024_06
{
  internal class Guard
  {

    public Point2D Location { get; set; }
    public EDirection4 FaceDirection { get; set; }
    public HashSet<HistoryItem> History { get; set; } = new HashSet<HistoryItem>();

    public Guard(EDirection4 direction,  Point2D location)
    {
      FaceDirection = direction;
      Location = location;
    }

    public Point2D NextLocation()
    {
      return Location.Move(FaceDirection);
    }
    public void Move()
    {
      History.Add(new HistoryItem(FaceDirection, Location));
      Location = Location.Move(FaceDirection);
    }

    public void Rotate()
    {
      FaceDirection = FaceDirection.Next();
    }

    internal bool IsInLoop()
    {
      HistoryItem tmp = new HistoryItem(FaceDirection, Location);
      return History.Contains(tmp);
    }
  }
}
