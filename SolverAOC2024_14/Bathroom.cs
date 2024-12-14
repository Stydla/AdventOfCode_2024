using AoCLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SolverAOC2024_14
{
  internal class Bathroom
  {

    public List<Robot> Robots { get; set; } = new List<Robot>();

    public int Width { get; set; }
    public int Height { get; set; }

    public int TotalMoves { get; set; }

    public Bathroom(int widht, int height, List<string> robotLines)
    {
      Width = widht;
      Height = height;
      TotalMoves = 0;

      Regex r = new Regex("p=(\\d*),(\\d*) v=(-?\\d*),(-?\\d*)");
      foreach (string robotLine in robotLines)
      {
        Match m = r.Match(robotLine);
        int rx = int.Parse(m.Groups[1].Value);
        int ry = int.Parse(m.Groups[2].Value);
        int vx = int.Parse(m.Groups[3].Value);
        int vy = int.Parse(m.Groups[4].Value);

        Point2D location = new Point2D(rx, ry);
        Point2D velocity = new Point2D(vx, vy);
        Robot robot = new Robot(location, velocity);
        Robots.Add(robot);

      }
    }

    public void Move(int count)
    {
      foreach (Robot robot in Robots)
      {
        robot.Location.X += robot.Velocity.X * count;
        robot.Location.X = robot.Location.X % Width;
        robot.Location.Y += robot.Velocity.Y * count;
        robot.Location.Y = robot.Location.Y % Height;

        if (robot.Location.X < 0)
        {
          robot.Location.X += Width;
        }
        if (robot.Location.Y < 0)
        {
          robot.Location.Y += Height;
        }

      }
      TotalMoves += count;
    }

    public long GetSafetyFactor()
    {
      List<long> results = new List<long>();
      for(int i = 0; i < 4; i++)
      {
        results.Add(0);
      }

      int index = 0;
      for (int i = 0; i < Height; i++)
      {
        if (i == Height / 2) continue;
        int indexI = index;
        if (i > Height / 2)
        {
          indexI += 1;
        }
        
        for (int j = 0; j < Width; j++)
        {
          if (j == Width / 2) continue;
          int indexJ = indexI;
          if(j > Width / 2)
          {
            indexJ += 2;
          }
          Point2D point = new Point2D(j, i);
          int cnt = Robots.Count(x=> x.Location.Equals(point));
          results[indexJ] += cnt;
        }
      }

      return results.Aggregate((x, y) => x * y);
    }

    public bool IsEasterEgg()
    {
      for(int i = 0; i < 20; i++)
      {
        if(Robots.Where(x=>x.Location.Y == i).Count() > 4)
        {
          return false;
        }
      }
      for (int i = 0; i < 20; i++)
      {
        if (Robots.Where(x => x.Location.X == i).Count() > 4)
        {
          return false;
        }
      }
      return true;
    }

    internal void Print()
    {
      Debug.WriteLine(PrintString());
    }

    private string PrintString()
    {
      StringBuilder sb = new StringBuilder();
      sb.AppendLine($"Total moves: {TotalMoves}");
      for (int i = 0; i < Height; i++)
      {
        for (int j = 0; j < Width; j++)
        {
          Point2D point = new Point2D(j, i);
          if (Robots.Any(x => x.Location.Equals(point)))
          {
            sb.Append("#");
          }
          else
          {
            sb.Append(".");
          }
        }
        sb.AppendLine();
      }
      sb.AppendLine();
      return sb.ToString();
    }


  }
}
