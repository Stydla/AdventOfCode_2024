using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2024_11
{
  internal class Stone
  {
    public Stone(long value)
    {
      Value = value;
    }

    public long Value { get; set; }

    internal IEnumerable<Stone> Blink()
    {

      if(Value == 0)
      {
        yield return new Stone(1);
      }
      else if(Value.ToString().Length % 2 == 0)
      {
        string strVal = Value.ToString();
        yield return new Stone(long.Parse(strVal.Substring(0, strVal.Length / 2)));
        yield return new Stone(long.Parse(strVal.Substring(strVal.Length / 2)));
      }
      else
      {
        yield return new Stone(Value * 2024);
      }
    }

    public override string ToString()
    {
      return Value.ToString();
    }

    public long Blink(int remainingBlinks, Dictionary<long, Dictionary<int, long>> cache)
    {
      if (remainingBlinks == 0) return 1;
      if(cache.TryGetValue(Value, out Dictionary<int, long> stoneCache))
      {
        if(stoneCache.TryGetValue(remainingBlinks, out long value))
        {
          return value;
        } else
        {
          List<Stone> newStone = Blink().ToList();
          long tmpVal = 0;
          foreach (var stone in newStone)
          {
            tmpVal += stone.Blink(remainingBlinks - 1, cache);
          }
          cache[Value][remainingBlinks] = tmpVal;
          return tmpVal;
        }
      } else
      {
        List<Stone> newStone = Blink().ToList();
        long tmpVal = 0;
        foreach(var stone in newStone)
        {
          tmpVal += stone.Blink(remainingBlinks - 1, cache);
        }
        if(!cache.ContainsKey(Value))
        {
          cache.Add(Value, new Dictionary<int, long>());
        }
        cache[Value][remainingBlinks] = tmpVal;
        return tmpVal;
      }
    }
  }
}
