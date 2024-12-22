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

    public Dictionary<string, string> CodeMap { get; set; }
    protected Dictionary<Point2D, KeypadNode> AllNodes = new Dictionary<Point2D, KeypadNode>();
    public RobotBase()
    {
      CodeMap = new Dictionary<string, string>();

      AddKeypadNodes();

      foreach (KeypadNode from in AllNodes.Values)
      {
        KeypadNodeContext context = new KeypadNodeContext() { AllNodes = AllNodes };
        BFS<KeypadNodeContext> bfs = new BFS<KeypadNodeContext>(from, context);

        foreach (KeypadNode to in AllNodes.Values)
        {
          StringBuilder sb = new StringBuilder();
          List<IBFSNode<KeypadNodeContext>> path = bfs.GetPath(to).ToList();
          for (int i = 0; i < path.Count() - 1; i++)
          {
            KeypadNode nodeFrom = path[i] as KeypadNode;
            KeypadNode nodeTo = path[i + 1] as KeypadNode;
            EDirection4 dir = nodeFrom.GetDirection(nodeTo);
            sb.Append(EDirection4Helper.Str(dir));
          }
          sb.Append('A');

          string key = from.Value.ToString() + to.Value.ToString();
          CodeMap.Add(key, sb.ToString());
        }
      }

      Fix();
    }

    public abstract void AddKeypadNodes();

    public abstract void Fix();

    protected void AddNode(int y, int x, char value)
    {
      Point2D loc = new Point2D(x, y);
      KeypadNode nn = new KeypadNode(loc, value);
      AllNodes.Add(loc, nn);
    }

    public string Solve(string input)
    {
      StringBuilder sb = new StringBuilder();

      for(int i = 0; i < input.Length - 1 ; i++)
      {
        string key = input[i].ToString() + input[i + 1].ToString();
        sb.Append(CodeMap[key]);
      }
      return sb.ToString();
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
