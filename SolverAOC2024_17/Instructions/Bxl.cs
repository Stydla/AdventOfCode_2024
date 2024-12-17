using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2024_17.Instructions
{

  //The bxl instruction (opcode 1) calculates the bitwise XOR of register B and the instruction's literal operand, then stores the result in register B.
  internal class Bxl : IInstruction
  {
    public int ID => 1;

    public void Execute(Computer computer)
    {
      computer.RegB = computer.RegB ^ computer.Literal;
    }
  }
}
