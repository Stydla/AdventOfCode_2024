using AoCLib;
using AoCLib.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2024_15
{
  internal class Robot
  {
    public Point2D Location { get; set; }

    public List<EDirection4> Directions { get; set; }
    public int CurrentIndex { get; set; }

    public Robot(Point2D location)
    {
      Location = location;
      Directions = new List<EDirection4>();
      CurrentIndex = 0;
    }

    public bool IsAtFinish()
    {
      return CurrentIndex == Directions.Count;
    }

    internal void AddDirections(string line)
    {
      foreach (char c in line)
      {
        if (EDirection4Helper.TryParse(c, out EDirection4 direction))
        {
          Directions.Add(direction);
        }
      }
    }

    public void Move(List<Field> allFields)
    {

      EDirection4 dir = Directions[CurrentIndex++];

      List<Field> fieldsToMove = GetFieldsInDirection(dir, allFields);
      if (fieldsToMove.Any(x => x.IsWall))
      {
        return;
      }
      foreach (Field field in fieldsToMove)
      {
        field.Location = field.Location.Move(dir);
      }
      Location = Location.Move(dir);
    }

    private List<Field> GetFieldsInDirection(EDirection4 dir, List<Field> allFields)
    {
      List<Field> fields = new List<Field>();

      Point2D tmpLoc = Location.Move(dir);
      Field tmpField = allFields.FirstOrDefault(x => x.Location == tmpLoc);

      while (tmpField != null)
      {
        fields.Add(tmpField);
        tmpLoc = tmpLoc.Move(dir);
        tmpField = allFields.FirstOrDefault(x => x.Location == tmpLoc);
      }
      return fields;
    }

    public string LastMoveText()
    {
      if (CurrentIndex == 0)
      {
        return "Start";
      }
      else
      {
        return Directions[CurrentIndex - 1].ToString();
      }

    }

    private List<Field> GetFieldsInDirection2(EDirection4 dir, List<Field> allFields)
    {
      List<Field> fields = new List<Field>();

      Point2D tmpLoc = Location.Move(dir);
      Field tmpField = allFields.FirstOrDefault(x => x.Location == tmpLoc);
    
      if(tmpField!= null)
      {
        if(tmpField.IsLargeBox)
        {
          tmpField.Box.GetMoveFields(dir, fields, allFields);
        }
        if(tmpField.IsWall)
        {
          fields.Add(tmpField);
        }
      }

      return fields;
    }

    internal void Move2(List<Field> allFields)
    {
      EDirection4 dir = Directions[CurrentIndex++];

      List<Field> fieldsToMove = GetFieldsInDirection2(dir, allFields);
      if (fieldsToMove.Any(x => x.IsWall))
      {
        return;
      }
      foreach (Field field in fieldsToMove)
      {
        field.Location = field.Location.Move(dir);
      }
      Location = Location.Move(dir);
    }
  }
}
