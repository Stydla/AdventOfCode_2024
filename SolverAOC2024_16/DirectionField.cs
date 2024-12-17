using AoCLib;
using AoCLib.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2024_16
{
  internal class DirectionField
  {

    public EDirection4 Direction { get; set; }
    public Point2D Location {  get; set; }
    public long Value { get; set; }


    public DirectionField(Point2D location, EDirection4 direction)
    {
      Location = location;
      Value = long.MaxValue;
      Direction = direction;
    }

    public static List<DirectionField> CreateFields(Point2D location)
    {
      List<DirectionField> ret = new List<DirectionField>();
      foreach(EDirection4 direction in Enum.GetValues(typeof(EDirection4)))
      {
        DirectionField f = new DirectionField(location, direction);
        ret.Add(f);
      }
      return ret;
    }

    List<Tuple<DirectionField, long>> NeighbourCache { get; set; } = new List<Tuple<DirectionField, long>>();

    public IEnumerable<Tuple<DirectionField, long>> GetValuedNeighbours(HashSet<DirectionField> allFields)
    {
      if (NeighbourCache.Count != 0) 
      {
        foreach(var f in NeighbourCache)
        {
          yield return f;
        }
        yield break;
      } 
      Point2D target = Location.Move(Direction);
      if(allFields.TryGetValue(new DirectionField(target, Direction), out DirectionField forward))
      {
        var ret = new Tuple<DirectionField, long>(forward, 1);
        NeighbourCache.Add(ret);
        yield return ret;
      }

      
      if (allFields.TryGetValue(new DirectionField(Location, Direction.Prev()), out DirectionField left))
      {
        var ret = new Tuple<DirectionField, long>(left, 1000);
        NeighbourCache.Add(ret);
        yield return ret;
      }

      if (allFields.TryGetValue(new DirectionField(Location, Direction.Next()), out DirectionField right))
      {
        var ret = new Tuple<DirectionField, long>(right, 1000);
        NeighbourCache.Add(ret);
        yield return ret;
      }
    }

    public IEnumerable<Tuple<DirectionField, long>> GetReverseValuedNeighbours(HashSet<DirectionField> allFields)
    {
      Point2D target = Location.Move(Direction.Next().Next()); ;
      if (allFields.TryGetValue(new DirectionField(target, Direction), out DirectionField forward))
      {
        yield return new Tuple<DirectionField, long>(forward, 1);
      }


      if (allFields.TryGetValue(new DirectionField(Location, Direction.Prev()), out DirectionField left))
      {
        yield return new Tuple<DirectionField, long>(left, 1000);
      }

      if (allFields.TryGetValue(new DirectionField(Location, Direction.Next()), out DirectionField right))
      {
        yield return new Tuple<DirectionField, long>(right, 1000);
      }
    }


    public override string ToString()
    {
      return $"({Location}) {Direction}   {Value}";
    }

    public override int GetHashCode()
    {
      return Location.GetHashCode() + Direction.GetHashCode() * (1 << 16);
    }

    public override bool Equals(object obj)
    {
      var item = obj as DirectionField;

      if (item == null)
      {
        return false;
      }

      return Location.Equals(item.Location) && Direction.Equals(item.Direction);
    }

  }
}
