using AoCLib;
using AoCLib.BFS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2024_10
{
  internal class Data : DataBase
  {

    public Dictionary<Point2D, Field> AllFields = new Dictionary<Point2D, Field>();

    public Data(string input) : base(input)
    {
      // optionaly parse data
      for(int i = 0; i < Lines.Count; i++)
      {
        string line = Lines[i];
        for(int j = 0; j < line.Length; j++) 
        {
          int value = line[j] - '0';
          Point2D location = new Point2D(j, i);
          Field field = new Field(location, value);

          AllFields.Add(location, field);
        }
      }
    }

    public object Solve1()
    {
      int cnt = 0;

      BFSContext context = new BFSContext();
      context.AllFields = AllFields;

      var starts = AllFields.Values.Where(x => x.Value == 0).ToList();
      var finishes = AllFields.Values.Where(x => x.Value == 9).ToList();
      foreach (Field start in starts)
      {
        BFS<BFSContext> bfs = new BFS<BFSContext>(start, context);

        foreach(Field end in finishes)
        {
          if(bfs.IsReachable(end))
          {
            cnt++;
          }
        }
      }

      return cnt;

    }

    public object Solve2()
    {
      int cnt = 0;

      BFSContext context = new BFSContext();
      context.AllFields = AllFields;

      var starts = AllFields.Values.Where(x => x.Value == 0).ToList();
      var finishes = AllFields.Values.Where(x => x.Value == 9).ToList();
      foreach (Field start in starts)
      {
        DFS<BFSContext> bfs = new DFS<BFSContext>(start, context);

        foreach (Field end in finishes)
        {
          List<List<IBFSNode<BFSContext>>> paths = bfs.GetAllPaths(end);
          cnt += paths.Count();
        }
      }

      return cnt;
    }


  }
}
