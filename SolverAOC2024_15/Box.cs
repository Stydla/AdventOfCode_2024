using AoCLib;
using AoCLib.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2024_15
{
  internal class Box
  {

    public List<Field> Fields { get; set; } = new List<Field>();

    public Field Left
    {
      get
      {
        return Fields[0];
      }
    }
    public Field Right
    {
      get
      {
        return Fields[1];
      }
    }
    public Box(Field left, Field right)
    {
      Fields.Add(left);
      Fields.Add(right);
    }

    public void GetMoveFields(EDirection4 dir, List<Field> result, List<Field> allFields)
    {
      if (result.Contains(Right)) return;

      result.Add(Right);
      result.Add(Left);

      foreach (Field field in Fields)
      {
        Point2D p1 = field.Location.Move(dir);
        Field f1 = allFields.FirstOrDefault(x => x.Location == p1);
        if (f1 != null && f1.IsLargeBox && f1.Box != this)
        {
          f1.Box.GetMoveFields(dir, result, allFields);
        }
        else
        {
          if (f1 != null && f1.IsWall)
          {
            result.Add(f1);
            return;
          }
        }
      }
    }
  }
}
