using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2024_25
{
  internal class Lock
  {
    public List<int> Values = new List<int>();

    public Lock(List<string> input)
    {
      for (int j = 0; j < 5; j++)
      {
        Values.Add(0);
        for (int i = 0; i < 5; i++)
        {
          if (input[i][j] == '#')
          {
            Values[j]++;
          }
        }
      }
    }
  }
}
