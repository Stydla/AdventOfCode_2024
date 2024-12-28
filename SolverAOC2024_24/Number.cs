using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2024_24
{
  internal class Number
  {

    public char ID { get; }

    public long Value { get; set; }

    public Number(char id)
    {
      ID = id;
    }

    public void SetBit(bool valid, int index)
    {
      if (valid)
      {
        Value |= 1L << index;
      }
      else
      {
        Value &= ~(1L << index);
      }
    }

    public bool GetBit(int index)
    {
      return (Value & (1L << index)) != 0;
    }
  }
}
