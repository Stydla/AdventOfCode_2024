using SolverAOC2024_24.Gates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2024_24
{
  internal class Wire
  {

    public string Name { get; set; }
    public int Index { get; set; }
    public Number Number { get; set; }

    public bool _Value;
    public bool Value 
    {
      get
      {
        if(Number != null)
        {
          return Number.GetBit(Index);
        }
        return _Value;
      }
      set
      {
        _Value = value;
        if(Number != null)
        {
          Number.SetBit(value, Index);
        }
      }
    }


    

    public Gate InputGate { get; set; }
    public Gate OutputGate { get; set; }

    public Wire(string name)
    {
      Name = name;
      if (name[0] == 'x' || name[0] == 'y'|| name[0] == 'z')
      {
        Index = int.Parse(name.Substring(1));
      }
    }

    public void Solve()
    {
      if (OutputGate != null)
      {
        OutputGate.Solve();
      }
    }

    public void SolveDown(HashSet<Gate> visited)
    {
      if (InputGate != null)
      {
        InputGate.SolveDown(visited);
      }
    }

    public int ChildHeight()
    {
      if (InputGate == null)
      {
        return 0;
      }
      return InputGate.ChildHeight() + 1;
    }

    public int ParentHeight()
    {
      if (OutputGate == null)
      {
        return 0;
      }
      return OutputGate.ParentHeight() + 1;
    }

    public override string ToString()
    {
      return Name;
    }

    public override bool Equals(object obj)
    {
      if (obj == null) return false;
      if (obj == this) return true;
      if (obj is Wire wire)
      {
        return wire.Name == Name;
      }
      return false;
    }

    public override int GetHashCode()
    {
      return Name.GetHashCode();
    }
  }
}

