using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2024_23
{
  internal class ComputerGroup
  {

    public HashSet<Computer> Computers = new HashSet<Computer>();

    private int HashCodeCache;
    public ComputerGroup(IEnumerable<Computer> computers)
    {
      foreach (Computer computer in computers)
      {
        Computers.Add(computer);
      }
      UpdateHashCode();
    }

    public ComputerGroup(ComputerGroup computerGroup) : this(computerGroup.Computers)
    {
    }

    public bool Contains(Computer computer)
    {
      return Computers.Contains(computer);
    }

    public void AddComputer(Computer computer)
    {
      Computers.Add(computer);
      UpdateHashCode();
    }

    private void UpdateHashCode()
    {
      HashCodeCache = 0;
      foreach(Computer computer in Computers.OrderBy(x=>x.Name))
      {
        HashCodeCache += computer.GetHashCode();
      }
    }

    public override bool Equals(object obj)
    {
      if (obj is ComputerGroup other)
      {
        if(other.Computers.Count != Computers.Count)
        {
          return false;
        }
        foreach (Computer computer in Computers)
        {
          if (!other.Computers.Contains(computer))
          {
            return false;
          }
        }
      }
      return true;
    }

    public override int GetHashCode()
    {
      return HashCodeCache;
    }

    internal bool AreAllConnectedTo(Computer c)
    {
      return Computers.All(x => x.IsConnectedTo(c));
    }
  }
}
