using AoCLib;
using AoCLib.BFS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2024_20
{
  internal class Data : DataBase
  {

    public int CheatBenefit { get; set; }
    public Dictionary<Point2D, Field> AllFields { get; set; } = new Dictionary<Point2D, Field>();
    public Dictionary<Field, int> DistancesFromStart { get; set; } = new Dictionary<Field, int>();
    public Dictionary<Field, int> DistancesToEnd { get; set; } = new Dictionary<Field, int>();
    public Field StartField { get; set; }
    public Field EndField { get; set; }

    public Data(string input) : base(input)
    {
      // optionaly parse data

      CheatBenefit = int.Parse(Lines[0]);

      for(int i = 0; i < Lines.Count -1 ; i++)
      {
        string line = Lines[i + 1];
        for(int j = 0; j < line.Length; j++) 
        {
          char value = line[j];
          Point2D location = new Point2D(j, i);
          Field field = new Field(location, value);
          switch(value)
          {
            case 'S':
              StartField = field;
              AllFields.Add(location, field);
              break;
            case 'E':
              EndField = field;
              AllFields.Add(location, field);
              break;
            case '.':
              AllFields.Add(location, field);
              break;
            case '#':
              break;
          }
        }
      }

      BFSContext context = new BFSContext(AllFields);
      BFS<BFSContext> bfs = new BFS<BFSContext>(EndField, context);

      foreach (Field startField in AllFields.Values)
      {
        int distance = bfs.GetDistance(startField);
        DistancesToEnd.Add(startField, distance);
      }

      context = new BFSContext(AllFields);
      bfs = new BFS<BFSContext>(StartField, context);

      foreach (Field startField in AllFields.Values)
      {
        int distance = bfs.GetDistance(startField);
        DistancesFromStart.Add(startField, distance);
      }


    }

    public object Solve1()
    {
      int cnt = GetBetterSolutionCount(2);
      return cnt;
    }


    public object Solve2()
    {
      int cnt = GetBetterSolutionCount(20);
      return cnt;
    }

    private int GetBetterSolutionCount(int maxCheatDistance)
    {
      int cnt = 0;

      BFSContext context = new BFSContext(AllFields);
      BFS<BFSContext> bfs = new BFS<BFSContext>(StartField, context);

      int fairTime = bfs.GetDistance(EndField);

      foreach (Field field in bfs.GetPath(EndField))
      {
        int fromStart = DistancesFromStart[field];
        for (int i = 2; i <= maxCheatDistance; i++)
        {
          List<Field> fields = GetCheatFields(field, i);
          foreach (var newField in fields)
          {
            if (DistancesToEnd.ContainsKey(newField))
            {
              int toEnd = DistancesToEnd[newField];
              int cheatTime = fromStart + toEnd + i;
              if (fairTime - cheatTime >= CheatBenefit)
              {
                cnt++;
              }
            }
          }
        }
      }
      return cnt;
    }

    private List<Field> GetCheatFields(Field field, int distance)
    {
      List<Field> fields = new List<Field>();
      foreach (Point2D newLocation in field.Location.GetNeighboursAtDistance(distance))
      {
        if (AllFields.TryGetValue(newLocation, out Field target))
        {
          fields.Add(target);
        }
      }
      return fields;
    }


  }
}
