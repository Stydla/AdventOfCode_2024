using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2024_22
{
  internal class Sequence
  {

    private List<long> sequence = new List<long>();

    private int HashCode { get; set; }

    public Sequence(long v1, long v2, long v3, long v4)
    {
      sequence.Add(v1);
      sequence.Add(v2);
      sequence.Add(v3);
      sequence.Add(v4);

      HashCode = (int)sequence[0] * 10000000 + (int)sequence[1] * 10000 + (int)sequence[2]* 100 + (int)sequence[3];
    }

    public override bool Equals(object obj)
    {
      //if (ReferenceEquals(this, obj)) return true;
      if (obj is Sequence other)
      {
        return sequence[0] == other.sequence[0] && sequence[1] == other.sequence[1] && sequence[2] == other.sequence[2] && sequence[3] == other.sequence[3];
      }
      return false;
    }




    public override int GetHashCode()
    {
      return HashCode;
    }

  }
}
