using AoCLib;
using AoCLib.BFS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2024_18
{
  internal class Field : IBFSNode<BFSContext>
  {

    public Point2D Location { get; set; }

    public Field(Point2D location)
    {
      Location = location;
    }

    public IEnumerable<IBFSNode<BFSContext>> GetNeighbours(BFSContext context)
    {
      foreach(Point2D neighbourLocation in Location.GetNeightbours4())
      {
        if(context.AllFields.TryGetValue(neighbourLocation, out var field))
        {
          yield return field;
        }
      }
    }

    public bool IsOpen(BFSContext context)
    {
      return true;
    }
  }

  internal class BFSContext
  {
    public Dictionary<Point2D, Field> AllFields = new Dictionary<Point2D, Field>();
  }
}
