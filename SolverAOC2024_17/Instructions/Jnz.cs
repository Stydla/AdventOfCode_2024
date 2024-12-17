using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2024_17.Instructions
{
  //The jnz instruction (opcode 3) does nothing if the A register is 0.
  //However, if the A register is not zero, it jumps by setting the instruction pointer to the value of its literal operand;
  //if this instruction jumps, the instruction pointer is not increased by 2 after this instruction.
  internal class Jnz : IInstruction
  {
    public int ID => 3;

    public void Execute(Computer computer)
    {
      if (computer.RegA == 0)
      {
        computer.InstructionPointer += 2;
        return;
      }

      computer.InstructionPointer = computer.Literal;


    }
  }
}
