using AoCLib;
using AoCLib.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2024_16
{
  internal class Data : DataBase
  {

    public HashSet<DirectionField> AllFields { get; set; } = new HashSet<DirectionField>();

    public HashSet<DirectionField> EndFields { get; set; } = new HashSet<DirectionField>();
    public DirectionField StartField { get; set; }

    public Data(string input) : base(input)
    {
      // optionaly parse data

      for(int i = 0; i < Lines.Count; i++)
      {
        string line = Lines[i]; 
        for(int j = 0; j < line.Length; j++)
        {
          char c = line[j];
          Point2D location = new Point2D(j, i);

          switch(c)
          {
            case '.':
              foreach(var f in DirectionField.CreateFields(location))
              {
                AllFields.Add(f);
              }
              break;
            case '#':
              break;
            case 'S':
              foreach (var f in DirectionField.CreateFields(location))
              {
                AllFields.Add(f);
                if(f.Direction == EDirection4.RIGHT)
                {
                  f.Value = 0;
                  StartField = f;
                }
              }
              break;
            case 'E':
              foreach (var f in DirectionField.CreateFields(location))
              {
                AllFields.Add(f);
                EndFields.Add(f);
              }
              break;
          }
        }
      }

    }

    public object Solve1()
    {

      Solve();

      long res = AllFields.Where(x=>EndFields.Contains(x)).Min(x=>x.Value);

      return res;

    }

    private void Solve()
    {
      Queue<DirectionField> fieldsToSolve = new Queue<DirectionField>();

      fieldsToSolve.Enqueue(StartField);

      while (fieldsToSolve.Count > 0)
      {
        DirectionField field = fieldsToSolve.Dequeue();

        foreach (var kv in field.GetValuedNeighbours(AllFields))
        {
          DirectionField neighbour = kv.Item1;
          long value = kv.Item2;
          long newValue = field.Value + value;

          if (neighbour.Value >= newValue)
          {
            neighbour.Value = newValue;
            fieldsToSolve.Enqueue(neighbour);
          }
        }
      }
    }

    public object Solve2()
    {
      Solve();

      long min = AllFields.Where(x => EndFields.Contains(x)).Min(x => x.Value);
      DirectionField endField = EndFields.First(x => x.Value == min);
      
      HashSet<DirectionField> resultFields = new HashSet<DirectionField>();

      Queue<DirectionField> fieldsToSolve = new Queue<DirectionField>();
      fieldsToSolve.Enqueue(endField);

      while(fieldsToSolve.Count > 0) 
      {
        DirectionField field = fieldsToSolve.Dequeue();
        foreach (var kv in field.GetReverseValuedNeighbours(AllFields))
        {
          DirectionField neighbour = kv.Item1;
          long value = kv.Item2;
          long newValue = field.Value - value;

          if (neighbour.Value == newValue)
          {
            fieldsToSolve.Enqueue(neighbour);
            if(!resultFields.Contains(neighbour)) {
              resultFields.Add(neighbour);
            }
          }
        }
      }


      return resultFields.Select(x=>x.Location).Distinct().Count() +1;
    }


  }
}
