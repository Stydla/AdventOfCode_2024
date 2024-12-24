using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2024_24.Gates
{
  internal class Xor : Gate
  {
    public Xor(Wire input1, Wire input2, Wire output) : base(input1, input2, output)
    {
    }

    public override bool Execute()
    {
      bool prev = Output.Value;
      Output.Value = Input1.Value ^ Input2.Value;
      return prev != Output.Value;
    }
  }
}
