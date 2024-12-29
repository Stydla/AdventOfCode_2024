using AoCLib;
using AoCLib.BFS;
using AoCLib.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2024_21
{
  internal abstract class RobotBase
  {

    public Dictionary<string, List<string>> CodeMap { get; set; }
    protected Dictionary<Point2D, KeypadNode> AllNodes = new Dictionary<Point2D, KeypadNode>();
    public RobotBase()
    {
      CodeMap = new Dictionary<string, List<string>>();

      AddKeypadNodes();

      foreach (KeypadNode from in AllNodes.Values)
      {
        KeypadNodeContext context = new KeypadNodeContext() { AllNodes = AllNodes };
        DFS<KeypadNodeContext> bfs = new DFS<KeypadNodeContext>(from, context);

        foreach (KeypadNode to in AllNodes.Values)
        {
          string key = from.Value.ToString() + to.Value.ToString();
          CodeMap.Add(key, new List<string>());


          
          List<List<IBFSNode<KeypadNodeContext>>> paths = bfs.GetAllPaths(to).ToList();
          foreach(var path in paths)
          {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < path.Count() - 1; i++)
            {
              KeypadNode nodeFrom = path[i] as KeypadNode;
              KeypadNode nodeTo = path[i + 1] as KeypadNode;
              EDirection4 dir = nodeFrom.GetDirection(nodeTo);
              sb.Append(EDirection4Helper.Str(dir));
            }
            sb.Append('A');


            CodeMap[key].Add(sb.ToString());
          }
        }
      }

      foreach(var key in CodeMap.Keys.ToList())
      {
        List<string> keys = CodeMap[key];
        int min = keys.Min(x => x.Length);
        CodeMap[key] = keys.Where(x => x.Length == min).ToList();
      }

    }

    public abstract void AddKeypadNodes();


    protected void AddNode(int y, int x, char value)
    {
      Point2D loc = new Point2D(x, y);
      KeypadNode nn = new KeypadNode(loc, value);
      AllNodes.Add(loc, nn);
    }

    public List<string> Solve(string input)
    {

      List<StringBuilder> results = new List<StringBuilder>();
      results.Add(new StringBuilder());

      for(int i = 0; i < input.Length - 1 ; i++)
      {
        string key = input[i].ToString() + input[i + 1].ToString();
        List<string> newKeys = CodeMap[key];

        int resultsCount = results.Count;
        List<string> originalStrings = results.Select(x => x.ToString()).ToList();
        for (int k = 0; k < newKeys.Count; k++)
        {
          if (k == 0)
          {
            for (int j = 0; j < resultsCount; j++)
            {
              results[j].Append(newKeys[0]);
            }
          }
          else
          {
            for (int j = 0; j < resultsCount; j++)
            {
              results.Add(new StringBuilder(originalStrings[j].ToString()));
              results[results.Count - 1].Append(newKeys[k]);
            }
          }
        }
      }
      return results.Select(x=>x.ToString()).ToList();
    }

  }

  internal class KeypadNode : IBFSNode<KeypadNodeContext>
  {
    public char Value { get; set; }
    public Point2D Location { get; set; }

    public KeypadNode(Point2D location, char value)
    {
      Location = location;
      Value = value;
    }

    public EDirection4 GetDirection(KeypadNode target)
    {
      foreach (var ndir in Location.GetNeightboursDict4())
      {
        if (ndir.Value.Equals(target.Location))
        {
          return ndir.Key;
        }
      }
      throw new Exception("Invalid target");
    }

    public IEnumerable<IBFSNode<KeypadNodeContext>> GetNeighbours(KeypadNodeContext context)
    {
      foreach (Point2D loc in Location.GetNeightbours4())
      {
        if (context.AllNodes.TryGetValue(loc, out var node))
        {
          yield return node;
        }
      }
    }

    public bool IsOpen(KeypadNodeContext context)
    {
      return true;
    }
  }
  internal class KeypadNodeContext
  {
    public Dictionary<Point2D, KeypadNode> AllNodes { get; set; }
  }

}
