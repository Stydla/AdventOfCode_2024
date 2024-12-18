using AoCLib;
using AoCLib.BFS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2024_18
{
  internal class Data : DataBase
  {

   
    public Dictionary<Point2D, int> AllCorruptedLocations = new Dictionary<Point2D, int>();


    public int SizeX { get; set; }
    public int SizeY { get; set; }
    public int InputSize { get; set; }


    public Data(string input) : base(input)
    {
      // optionaly parse data

      string[] init = Lines[0].Split(' ');
      SizeX = int.Parse(init[0]);
      SizeY = int.Parse(init[1]);
      InputSize = int.Parse(init[2]);

      for(int i = 1; i < Lines.Count; i++)
      {
        string line = Lines[i];
        string[] lineParts = line.Split(',');
        int x = int.Parse(lineParts[0]);
        int y = int.Parse(lineParts[1]);
        Point2D location = new Point2D(x, y);
        AllCorruptedLocations.Add(location, i - 1);
      }
    }

    private Dictionary<Point2D, Field> GetValidFields(int cnt)
    {
      Dictionary<Point2D, Field> validFields = new Dictionary<Point2D, Field>();
      for (int i = 0; i < SizeY; i++)
      {
        for (int j = 0; j < SizeX; j++)
        {
          Point2D location = new Point2D(j, i);
          if (!AllCorruptedLocations.ContainsKey(location))
          {
            Field field = new Field(location);
            validFields.Add(location, field);
          } else
          {
            int index = AllCorruptedLocations[location];
            if(index >= cnt)
            {
              Field field = new Field(location);
              validFields.Add(location, field);
            }
          }
        }
      }
      return validFields;
    }

    public object Solve1()
    {
      Dictionary<Point2D, Field> validFields = GetValidFields(InputSize);
     

      Field start = validFields[new Point2D(0, 0)];
      Field target = validFields[new Point2D(SizeX - 1, SizeY - 1)];

      BFSContext context = new BFSContext();
      context.AllFields = validFields;
      BFS<BFSContext> bfs = new BFS<BFSContext>(start, context);
      int distance = bfs.GetDistance(target);
      return distance;
    }

    public object Solve2()
    {

      for(int i = AllCorruptedLocations.Count; i >=1; i--)
      {
        Dictionary<Point2D, Field> validFields = GetValidFields(i);


        Field start = validFields[new Point2D(0, 0)];
        Field target = validFields[new Point2D(SizeX - 1, SizeY - 1)];

        BFSContext context = new BFSContext();
        context.AllFields = validFields;
        BFS<BFSContext> bfs = new BFS<BFSContext>(start, context);
        bool isReachable = bfs.IsReachable(target);
        if(isReachable)
        {
          Point2D loc = AllCorruptedLocations.Where(x=>x.Value == i).FirstOrDefault().Key;
          return $"{loc.X},{loc.Y}";
        }
      }
      return -1;
    }


  }
}
