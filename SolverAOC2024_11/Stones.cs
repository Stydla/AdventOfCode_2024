using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2024_11
{
  internal class StoneLine
  {

    public List<Stone> Stones = new List<Stone>();  

    public StoneLine() { }
    public StoneLine(string input)
    {
      Stones = input.Split(' ').Select(x=>new Stone(long.Parse(x))).ToList();
    }

    public long Blink(int count)
    {
      Dictionary<long, Dictionary<int, long>> cache = new Dictionary<long, Dictionary<int, long>>();

      long stoneCnt = 0;

      foreach (var stone in Stones)
      {
        stoneCnt += stone.Blink(count, cache);
      }

      return stoneCnt;
    }


  }
}
