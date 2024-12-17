using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2024_17.Instructions
{
  //The adv instruction (opcode 0) performs division.
  //The numerator is the value in the A register.
  //The denominator is found by raising 2 to the power of the instruction's combo operand. (So, an operand of 2 would divide A by 4 (2^2);
  //an operand of 5 would divide A by 2^B.) The result of the division operation is truncated to an integer and then written to the A register.
  internal class Adv : IInstruction
  {
    public int ID { get => 0; }

    public void Execute(Computer computer)
    {
      long numerator = computer.RegA;
      long denominator = 1L << (int)computer.Combo;
      computer.RegA = numerator / denominator;
    }
  }
}
