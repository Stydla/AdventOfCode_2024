using AoCLib;
using AoCLib.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2024_06
{
  internal class Data : DataBase
  {


    public Dictionary<Point2D, Field> Fields = new Dictionary<Point2D, Field>();
    public Guard Guard;
    public Point2D GuardStartLocation;
    public EDirection4 GuardStartDirection;
    public Data(string input) : base(input)
    {
      // optionaly parse data

      for(int i = 0; i < Lines.Count; i++)
      {
        string line = Lines[i];
        for(int j = 0; j < line.Length; j++) 
        {
          char value = line[j];
          Point2D location = new Point2D(j, i);
          Field field = new Field(value, location);
          
          Fields.Add(location, field);

          EDirection4 dir;
          if(EDirection4Helper.TryParse(value, out dir))
          {
            GuardStartLocation = location;
            GuardStartDirection = dir;
            Guard = new Guard(dir, location);
          }
        }
      }

    }

    public object Solve1()
    {

      while(true)
      {
        if(CanMove())
        {
          Guard.Move();
        } else
        {
          Guard.Rotate();
        }
        if (!Fields.Keys.Contains(Guard.Location))
        {
          break;
        }
      }

      return Guard.History.Select(x=>x.Location).Distinct().Count();
    }

    private bool CanMove()
    {
      Point2D nextLocation = Guard.NextLocation();
      if(Fields.ContainsKey(nextLocation) && Fields[nextLocation].Value != '#')
      {
        return true;
      }
      if(!Fields.ContainsKey(nextLocation))
      {
        return true;
      }
      return false;
    }

    public object Solve2()
    {
      int loopCnt = 0;


      Solve1();
      List<Point2D> locationsForObstructions = Guard.History.Select(x => x.Location).Distinct().ToList();

      foreach(Point2D location in locationsForObstructions)
      {

        Guard.Location = GuardStartLocation;
        Guard.FaceDirection = GuardStartDirection;
        Guard.History.Clear();

        Fields[location].Value = '#';

        while (true)
        {
          if (CanMove())
          {
            Guard.Move();
          }
          else
          {
            Guard.Rotate();
          }
          if (Guard.IsInLoop())
          {
            loopCnt++;
            break;
          }
          if (!Fields.Keys.Contains(Guard.Location))
          {
            break;
          }
        }
        Fields[location].Value = '.';
      }

      return loopCnt++;
     


    }


  }
}
