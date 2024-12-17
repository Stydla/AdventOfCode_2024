using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2024_17.Instructions
{
  //The out instruction (opcode 5) calculates the value of its combo operand modulo 8, then outputs that value.
  //(If a program outputs multiple values, they are separated by commas.)
  internal class Out : IInstruction
  {
    public int ID => 5;

    public void Execute(Computer computer)
    {
      long res = computer.Combo % 8;
      computer.Output.Add((int)res);
    }
  }
}
