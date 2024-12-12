using AoCLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2024_12
{
  internal class Data : DataBase
  {

    public Dictionary<Point2D, Field> AllFields = new Dictionary<Point2D, Field>();

    public List<Region> Regions = new List<Region>();
    

    public Data(string input) : base(input)
    {
      
      for(int i = 0; i < Lines.Count; i++)
      {
        string line = Lines[i];
        for (int j = 0; j < line.Length; j++)
        {
          char c = line[j];
          Point2D location = new Point2D(j, i);
          Field field = new Field(location, c);
          AllFields.Add(location, field);
        }
      }

      HashSet<Field> visited = new HashSet<Field>();

      foreach (Field field in AllFields.Values)
      {
        if(visited.Contains(field))
        {
          continue;
        }

        Region region = CreateRegion(field, visited);
        Regions.Add(region);
      }

    }

    private Region CreateRegion(Field field, HashSet<Field> visited)
    {
      Region region = new Region(field.Value);
      CreateRegionRec(region, field, visited);
      return region;
    }

    private void CreateRegionRec(Region region, Field currentField, HashSet<Field> visited)
    {
      if(region.Value != currentField.Value)
      {
        return;
      }
      if(region.Fields.Contains(currentField))
      {
        return;
      }

      region.Fields.Add(currentField);
      visited.Add(currentField);

      foreach (Point2D neighbourLocation in currentField.Location.GetNeightbours4())
      {
        if (AllFields.TryGetValue(neighbourLocation, out Field neighbour))
        {
          CreateRegionRec(region, neighbour, visited);
        }
      }
    }

    public object Solve1()
    {
      long totalPrice = 0;
      foreach(Region region in Regions)
      {
        totalPrice += region.GetPrice(AllFields);
      }
      return totalPrice;
    }

    public object Solve2()
    {
      long totalPrice = 0;
      foreach (Region region in Regions)
      {
        totalPrice += region.GetPrice2(AllFields);
      }
      return totalPrice;
    }


  }
}
