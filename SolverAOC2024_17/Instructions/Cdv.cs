using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2024_17.Instructions
{
  //The cdv instruction (opcode 7) works exactly like the adv instruction except that the result is stored in the C register.
  //(The numerator is still read from the A register.)
  internal class Cdv : IInstruction
  {
    public int ID => 7;

    public void Execute(Computer computer)
    {
      long numerator = computer.RegA;
      long denominator = 1L << (int)computer.Combo;
      computer.RegC = numerator / denominator;
    }
  }
}
