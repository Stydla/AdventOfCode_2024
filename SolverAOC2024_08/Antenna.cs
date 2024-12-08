using AoCLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2024_08
{
  internal class Antenna
  {

    public char Name { get; set; }

    public List<Point2D> Locations { get; set; } = new List<Point2D>();

    public List<Point2D> Antinodes { get; set; } = new List<Point2D>(); 

    public Antenna(char name)
    {
      Name = name;
    }

    internal void CalculateAntinodes(HashSet<Point2D> allLocations, bool endless)
    {
      for (int i = 0; i < Locations.Count; i++)
      {
        Point2D loc = Locations[i];
        for (int j = i + 1; j < Locations.Count; j++)
        {
          Point2D loc2 = Locations[j];
          
          int diffX = loc.X - loc2.X;
          int diffY = loc.Y - loc2.Y;

          int mult = 1;
          while (true)
          {
            Point2D pNew = new Point2D(loc.X + mult * diffX, loc.Y + mult * diffY);
            if (allLocations.Contains(pNew))
            {
              Antinodes.Add(pNew);
              mult++;
            }
            else
            {
              break;
            }
            if (!endless)
            {
              break;
            }
          }

          mult = 1;
          while (true)
          {
            Point2D pNew = new Point2D(loc2.X - mult * diffX, loc2.Y - mult * diffY);
            if (allLocations.Contains(pNew))
            {
              Antinodes.Add(pNew);
              mult++;
            } 
            else
            {
              break;
            }
            if (!endless)
            {
              break;
            }
          }
          
          


        }
      }
    }

  }
}
