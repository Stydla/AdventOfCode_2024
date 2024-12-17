using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2024_17.Instructions
{
  //The bxc instruction (opcode 4) calculates the bitwise XOR of register B and register C, then stores the result in register B.
  //(For legacy reasons, this instruction reads an operand but ignores it.)
  internal class Bxc : IInstruction
  {
    public int ID => 4;

    public void Execute(Computer computer)
    {
      computer.RegB = computer.RegB ^ computer.RegC;
    }
  }
}
