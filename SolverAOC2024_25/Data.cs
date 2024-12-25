using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2024_25
{
  internal class Data : DataBase
  {

    public List<Key> Keys = new List<Key>();
    public List<Lock> Locks = new List<Lock>();

    public Data(string input) : base(input)
    {
      // optionaly parse data

      for(int i = 0; i < Lines.Count; i+=8)
      {
        if(Lines.Skip(i).First()[0] == '#')
        {
          //Lock
          Lock l = new Lock(Lines.Skip(i + 1).Take(5).ToList());
          Locks.Add(l);

        } else
        {
          //Key
          Key l = new Key(Lines.Skip(i + 1).Take(5).ToList());
          Keys.Add(l);
        }
      }

    }

    public object Solve1()
    {
      int cnt = 0;
      foreach(Key k in Keys)
      {
        foreach (Lock l in Locks)
        {
          if (k.IsMatch(l))
          {
            cnt++;
          }
        }
      }
      return cnt;
    }

    public object Solve2()
    {
      throw new NotImplementedException();
    }


  }
}
