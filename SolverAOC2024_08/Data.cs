using AoCLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2024_08
{
  internal class Data : DataBase
  {

    HashSet<Point2D> Locations = new HashSet<Point2D>();
    Dictionary<char, Antenna> Antennas = new Dictionary<char, Antenna>();

    public Data(string input) : base(input)
    {
      // optionaly parse data
      for (int i = 0; i < Lines.Count; i++)
      {
        string line = Lines[i];
        for(int j = 0; j < line.Length; j++)
        {
          char c = line[j];
          Point2D loc = new Point2D(j, i);
          Locations.Add(loc);
          if(c != '.')
          {
            if (!Antennas.ContainsKey(c))
            {
              Antennas.Add(c, new Antenna(c));
            }

            Antennas[c].Locations.Add(loc); 

          }
        }
      }
    }
  
    public object Solve1()
    {
      foreach (var antenna in Antennas.Values)
      {
        antenna.CalculateAntinodes(Locations, false);
      }

      List<Point2D> antinodes = Antennas.Values.SelectMany(x => x.Antinodes).ToList();

      return antinodes.Distinct().Count();
      
    }

    public object Solve2()
    {
      foreach (var antenna in Antennas.Values)
      {
        antenna.CalculateAntinodes(Locations, true);
      }

      List<Point2D> antinodes = Antennas.Values.SelectMany(x => x.Antinodes).ToList();
      antinodes.AddRange(Antennas.Values.SelectMany(y => y.Locations));


      return antinodes.Distinct().Count();
    }

    private void Print()
    {
      foreach (var antenna in Antennas.Values)
      {
        for (int i = 0; i < Locations.Max(x => x.Y) + 1; i++)
        {
          for (int j = 0; j < Locations.Max(x => x.X) + 1; j++)
          {
            Point2D loc = new Point2D(j, i);
            if (antenna.Locations.Contains(loc))
            {
              Console.Write(antenna.Name);
            }
            else if (antenna.Antinodes.Contains(loc))
            {
              Console.Write("X");
            }
            else
            {
              Console.Write(".");
            }
          }
          Console.WriteLine();
        }

        Console.WriteLine();
      }

    }
  }
}
