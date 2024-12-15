using AoCLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2024_15
{
  internal class Map
  {

    public List<Field> AllFields { get; private set; } = new List<Field>();
    //public List<Box> AllBoxes { get; private set; } = new List<Box>();

    public Robot Robot { get; set; }

    public int Width { get; set; }
    public int Height { get; set; }

    public Map(List<string> inputLines)
    {

      bool loadMap = true;
      
      for (int i = 0; i < inputLines.Count; i++)
      {
        string line = inputLines[i];  
        if(string.IsNullOrEmpty(line))
        {
          loadMap = false;
          continue;
        }

        if(loadMap)
        {
          Width = line.Length;
          Height++;
          for (int j = 0; j < line.Length; j++)
          {
            Point2D location = new Point2D(j, i);
            char value = line[j];

            switch(value)
            {
              case '#':
              case 'O':
                AllFields.Add(new Field(location, value));
                break;
              case '@':
                Robot = new Robot(location);
                break;
              case '[':
                Field fieldLeft = new Field(location, value);
                j += 1;
                location = new Point2D(j , i);
                value = line[j];
                Field fieldRight = new Field(location, value);
                Box box = new Box(fieldLeft, fieldRight);
                //AllBoxes.Add(box);
                fieldLeft.Box = box;
                fieldRight.Box = box; 
                AllFields.Add(fieldLeft);
                AllFields.Add(fieldRight);
                break;
              case '.':
                break;
                default:
                throw new Exception("Unknown value");
            }
          }
        }

        if(!loadMap)
        {
          Robot.AddDirections(line);
        }
      }
    }

    public long Solve()
    {
      while(!Robot.IsAtFinish())
      {
        //Debug.WriteLine(PrintToString()); 
        Robot.Move(AllFields);
      }
      long sum = SumGPSCoordinates();
      return sum;
    }

    public long Solve2()
    {
      while (!Robot.IsAtFinish())
      {
        //Debug.WriteLine(PrintToString()); 
        Robot.Move2(AllFields);
      }
      long sum = SumGPSCoordinates2();
      return sum;
    }

    public long SumGPSCoordinates()
    {
      var boxes = AllFields.Where(x => x.IsBox);
      long sum = 0;
      foreach (var box in boxes)
      {
        long xVal = box.Location.X;
        long yVal = box.Location.Y * 100;
        sum += (xVal + yVal);
      }
      return sum;
    }

    public long SumGPSCoordinates2()
    {
      var boxes = AllFields.Where(x => x.IsLargeBox).Select(x=>x.Box).Distinct();
      long sum = 0;
      foreach (var box in boxes)
      {
        long xVal = box.Left.Location.X;
        long yVal = box.Left.Location.Y * 100;
        sum += (xVal + yVal);
      }
      return sum;
    }

    public string PrintToString()
    {
      StringBuilder sb = new StringBuilder();
      sb.AppendLine(Robot.LastMoveText());
      for (int i = 0; i < Height; i++)
      {
        for (int j = 0; j < Width; j++)
        {
          Point2D location = new Point2D(j, i);
          Field field = AllFields.FirstOrDefault(x => x.Location.Equals(location));
          if (field != null)
          {
            sb.Append(field.Value);
          }
          else
          {
            if(Robot.Location.Equals(location))
            {
              sb.Append('@');
            }
            else
            {
              sb.Append('.');
            }
          }
        }
        sb.AppendLine();
      }
      return sb.ToString();
    }
  }
}
