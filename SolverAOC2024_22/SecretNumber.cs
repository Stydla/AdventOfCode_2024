using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2024_22
{
  internal class SecretNumber
  {

    public long Value { get; set; }

    public SecretNumber(long value)
    {
      Value = value;
    }


    public SecretNumber Next()
    {
      long tmp = Value;
      tmp = MixAndPrune(tmp, tmp * 64);
      tmp = MixAndPrune(tmp, tmp / 32);
      tmp = MixAndPrune(tmp, tmp * 2048);
      return new SecretNumber(tmp);
    }

    private long MixAndPrune(long secretNumber, long number)
    {
      return (number ^ secretNumber) % 16777216;
    }

  }
}
