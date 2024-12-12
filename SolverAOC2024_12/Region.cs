using AoCLib;
using AoCLib.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2024_12
{
  internal class Region
  {
    public HashSet<Field> Fields = new HashSet<Field>();

    public char Value { get; set; }

    public Region(char value)
    {
      Value = value;
    }

    public long GetPrice(Dictionary<Point2D, Field> allFields)
    {
      return GetArea() * GetPerimeter(allFields);
    }

    public long GetArea()
    {
      return Fields.Count;
    }

    public long GetPerimeter(Dictionary<Point2D, Field> allFields)
    {
      long perimeter = 0;

      foreach (Field field in Fields)
      {
        foreach (Point2D neighbourLocation in field.Location.GetNeightbours4())
        {
          if(allFields.TryGetValue(neighbourLocation, out Field neighbour))
          {
            if (neighbour.Value != Value)
            {
              perimeter++;
            }
          } else
          {
            perimeter++;
          }
        }
      }
      return perimeter;
    }

    public long GetSides()
    {
      long sides = 0;
      sides += GetSidesVertical(EDirection4.UP);
      sides += GetSidesVertical(EDirection4.DOWN);
      sides += GetSidesHorizontal(EDirection4.LEFT);
      sides += GetSidesHorizontal(EDirection4.RIGHT);

      return sides;

    }

    private int MinX { get { return Fields.Min(f => f.Location.X); } }
    private int MaxX { get { return Fields.Max(f => f.Location.X); } }
    private int MinY { get { return Fields.Min(f => f.Location.Y); } }
    private int MaxY { get { return Fields.Max(f => f.Location.Y); } }

    private long GetSidesHorizontal(EDirection4 dir)
    {
      long sides = 0;
      for (int x = MinX; x <= MaxX; x++)
      {
        bool inRow = false;
        for (int y = MinY; y <= MaxY; y++)
        {
          Point2D location = new Point2D(x, y);
          Field field = Fields.Where(f => f.Location.Equals(location)).FirstOrDefault();
          if (field == null)
          {
            inRow = false;
            continue;
          }

          if (Fields.FirstOrDefault(f => f.Location.Equals(field.Location.Move(dir))) == null)
          {
            if (!inRow)
            {
              sides++;
              inRow = true;
            } 
          } else
          {
            inRow = false;
          }

        }
      }
      return sides;
    }

    private long GetSidesVertical(EDirection4 dir)
    {
      long sides = 0;
      for (int y = MinY; y <= MaxY; y++)
      {
        bool inRow = false;
        for (int x = MinX; x <= MaxX; x++)
        {
          Point2D location = new Point2D(x, y);
          Field field = Fields.Where(f => f.Location.Equals(location)).FirstOrDefault();
          if (field == null)
          {
            inRow = false;
            continue;
          }

          if (Fields.FirstOrDefault(f => f.Location.Equals(field.Location.Move(dir))) == null)
          {
            if (!inRow)
            {
              sides++;
              inRow = true;
            }
          }
          else
          {
            inRow = false;
          }

        }
      }
      return sides;
    }

    internal long GetPrice2(Dictionary<Point2D, Field> allFields)
    {
      long sides = GetSides();
      long area = GetArea();  
      return area * sides;
    }
  }
}
