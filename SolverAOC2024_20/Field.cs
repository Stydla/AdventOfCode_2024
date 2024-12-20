using AoCLib;
using AoCLib.BFS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2024_20
{
  public class Field : IBFSNode<BFSContext>
  {
    public Point2D Location { get; set; } 
    public char Value { get; set; } 

    public Field(Point2D location, char value)
    {
      Location = location;
      Value = value;
    }

    public IEnumerable<IBFSNode<BFSContext>> GetNeighbours(BFSContext context)
    {
      foreach(Point2D newLocation in Location.GetNeightbours4())
      {
        if(context.AllFields.TryGetValue(newLocation, out var field))
        {
          yield return field;
        }
      }
    }

    public bool IsOpen(BFSContext context)
    {
      return true;
    }

    public override string ToString()
    {
      return Location.ToString();
    }

  }

  public class BFSContext
  {
    public Dictionary<Point2D, Field> AllFields { get; set; } = new Dictionary<Point2D, Field>();

    public BFSContext(Dictionary<Point2D, Field> allFields)
    {
      AllFields = allFields;
    }
  }
}
