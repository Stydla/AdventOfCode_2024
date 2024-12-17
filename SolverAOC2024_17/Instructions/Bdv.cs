using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2024_17.Instructions
{
  //The bdv instruction (opcode 6) works exactly like the adv instruction except that the result is stored in the B register.
  //(The numerator is still read from the A register.)
  internal class Bdv : IInstruction
  {
    public int ID => 6;

    public void Execute(Computer computer)
    {
      long numerator = computer.RegA;
      long denominator = 1L << (int)computer.Combo;
      computer.RegB = numerator / denominator;
    }
  }
}
