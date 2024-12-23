using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2024_23
{
  internal class Computer
  {

    public string Name { get; set; }
    public HashSet<Computer> Connections { get; private set; } = new HashSet<Computer>();

    public Computer(string name)
    {
      Name = name;
    }

    public void AddConnection(Computer computer)
    {
      Connections.Add(computer);
    }

    public bool IsConnectedTo(Computer computer)
    {
      return Connections.Contains(computer);
    }


    public override bool Equals(object obj)
    {
      if(obj == null)
      {
        return false;
      } 
      if (ReferenceEquals(this, obj))
      {
        return true;
      }
      if(obj is Computer cmp)
      {
        return Name.Equals(cmp.Name);
      }
      return false;
    }

    public override int GetHashCode()
    {
      return Name.GetHashCode();
    }

    public override string ToString()
    {
      return Name;
    }

  }
}
