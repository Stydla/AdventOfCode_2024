using AoCLib;
using AoCLib.BFS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2024_10
{
  internal class Field : IBFSNode<BFSContext>
  {

    public Point2D Location { get; set; }
    public int Value { get; set; }

    public Field(Point2D location, int value)
    {
      Location = location;
      Value = value;
    }

    public IEnumerable<IBFSNode<BFSContext>> GetNeighbours(BFSContext context)
    {
      foreach(Point2D p in Location.GetNeightbours4())
      {
        if(context.AllFields.TryGetValue(p, out var field))
        {
          if(field.Value == Value + 1)
          {
            yield return field;
          }
        }
      }
    }

    public bool IsOpen(BFSContext context)
    {
      return true;
    }

    public override string ToString()
    {
      return $"{Value} - {Location}";
    }
  }

  internal class BFSContext
  {
    public Dictionary<Point2D, Field> AllFields = new Dictionary<Point2D, Field>();
  }
}
