using AoCLib;
using AoCLib.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2024_04
{
  internal class Data : DataBase
  {
    public Dictionary<Point2D, Item> Items;

    public Data(string input) : base(input)
    {
      Items = new Dictionary<Point2D, Item>();
      for(int i = 0; i < Lines.Count; i++)
      {
        string line = Lines[i];
        for(int j = 0; j  < line.Length; j++)
        {
          Point2D position = new Point2D(j, i);
          char value = line[j];
          Item item = new Item(value, position);

          Items.Add(position, item);
        }
      }
    }

    public object Solve1()
    {
      int sum = 0;
      foreach(Point2D p in Items.Keys)
      {
        sum += XMasCnt(p);
      }
      return sum;
    }

    

    private int XMasCnt(Point2D position)
    {
      string searchString = "XMAS";
      int cnt = 0;
      foreach(EDirection8 dir in Enum.GetValues(typeof(EDirection8)))
      {
        if(IsMatch(dir, position, 0, searchString))
        {
          cnt++;
        }
      }
      return cnt;
    }

    private bool IsMatch(EDirection8 dir, Point2D position, int currentPos, string searchString)
    {
      if (currentPos >= searchString.Length)
      {
        return true;
      }

      if (Items.TryGetValue(position, out var item))
      {
        if(item.Value != searchString[currentPos])
        {
          return false;
        }
      } else
      {
        return false;
      }

      return IsMatch(dir, position.Move(dir), currentPos + 1, searchString);
    }


    public object Solve2()
    {
      int sum = 0;
      foreach (Point2D p in Items.Keys)
      {
        if(IsMas(p))
        {
          sum++;
        }
      }
      return sum;
    }

    private bool IsMas(Point2D position)
    {
      Point2D ulPoint = position.UL();
      Point2D urPoint = position.UR();

      string searchStr1 = "MAS";
      string searchStr2 = "SAM";

      if (
        (IsMatch(EDirection8.DOWN_RIGHT, ulPoint, 0, searchStr1) || IsMatch(EDirection8.DOWN_RIGHT, ulPoint, 0, searchStr2)) &&
        (IsMatch(EDirection8.DOWN_LEFT, urPoint, 0, searchStr1) || IsMatch(EDirection8.DOWN_LEFT, urPoint, 0, searchStr2))
        )
      {
        return true;
      }
      return false;


    }




  }
}
