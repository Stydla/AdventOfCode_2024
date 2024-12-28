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

    public abstract string GateType { get; }

    public int ID { get; }

    private static int IDCounter = 0;

    public Gate(int gateId, Wire input1, Wire input2, Wire output)
    {
      Input1 = input1;
      input1.InputGate = this;
      Input2 = input2;
      input2.InputGate = this;
      Output = output;
      output.OutputGate = this;
      ID = gateId;
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

    public int ChildHeight()
    {
      if (Output == null)
      {
        return 0;
      }
      return Output.ChildHeight() + 1;
    }

    public int ParentHeight()
    {
      if(Input1 == null && Input2 == null)
      {
        return 0;
      }

      if(Input1 == null)
      {
        return Input2.ParentHeight() + 1;
      }

      if(Input1 == null)
      {
        return Input1.ParentHeight() + 1;
      }

      return Math.Max(Input1.ParentHeight(), Input2.ParentHeight()) + 1;
    }


    public abstract bool Execute();


    public static Gate Create(int gateId, string gateType, Wire input1, Wire input2, Wire output)
    {
      switch(gateType)
      {
        case "AND":
          return new And(gateId, input1, input2, output);
        case "OR":
          return new Or(gateId, input1, input2, output);
        case "XOR":
          return new Xor(gateId, input1, input2, output);
        default:
          throw new Exception("Unknown gate type");
      }
    }


  }
}
