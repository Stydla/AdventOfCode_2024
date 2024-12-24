using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2024_24.Gates
{
  internal abstract class Gate
  {


    public Wire Input1 { get; set; }

    public Wire Input2 { get; set; }

    public Wire Output { get; set; }

    public Gate(Wire input1, Wire input2, Wire output)
    {
      Input1 = input1;
      input1.InputGate = this;
      Input2 = input2;
      input2.InputGate = this;
      Output = output;
      output.OutputGate = this;
    }

    public void Solve()
    {
      Input1.Solve();
      Input2.Solve();

      Execute();
    }

    public void SolveDown(HashSet<Gate> visited)
    {
      visited.Add(this);
      if (Execute())
      {
        if (Output != null)
        {
          if (visited.Contains(Output.InputGate))
          {
            return;
          }

          Output.SolveDown(visited);
        }
      }
      
    }

    public abstract bool Execute();


    public static Gate Create(string gateType, Wire input1, Wire input2, Wire output)
    {
      switch(gateType)
      {
        case "AND":
          return new And(input1, input2, output);
        case "OR":
          return new Or(input1, input2, output);
        case "XOR":
          return new Xor(input1, input2, output);
        default:
          throw new Exception("Unknown gate type");
      }
    }

  }
}
