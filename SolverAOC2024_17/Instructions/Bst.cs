using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2024_17.Instructions
{
  //The bst instruction (opcode 2) calculates the value of its combo operand modulo 8 (thereby keeping only its lowest 3 bits), then writes that value to the B register.
  internal class Bst : IInstruction
  {
    public int ID => 2;

    public void Execute(Computer computer)
    {
      computer.RegB = computer.Combo % 8;
    }
  }
}
