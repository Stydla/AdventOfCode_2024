using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2024_24.Gates
{
  internal class And : Gate
  {
    public And(int gateId, Wire input1, Wire input2, Wire output) : base(gateId, input1, input2, output)
    {
    }
    public override string GateType => "AND";
    public override bool Execute()
    {
      bool prev = Output.Value;
      Output.Value = Input1.Value && Input2.Value;
      return prev != Output.Value;
    }
  }
}
