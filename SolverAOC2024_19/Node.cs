using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2024_19
{
  internal class Node
  {

    public bool IsFirst { get; set; } = false;
    public bool IsFinal { get; set; } = false;

    public char Value { get; private set; }
    public int Id { get; private set; }

    public HashSet<Node> NextNodes { get; } = new HashSet<Node>();


    private static int IdCounter = 0;
    public Node(char value)
    {
      Value = value;
      Id = IdCounter++;
    }

    public void AddNextNode(Node n)
    {
      if(!NextNodes.Contains(n))
      {
        NextNodes.Add(n);
      }
    } 

    public override string ToString()
    {
      return $"{Value} {Id} {(IsFirst ? "First" : "")} {(IsFinal ? "Final" : "")}";
    }

    public override bool Equals(object obj)
    {
      if (obj is Node n)
      {
        return n.Value == Value && n.Id == Id;
      }
      return false;
    }

    public override int GetHashCode()
    {
      return Value.GetHashCode() + Id.GetHashCode();
    }

  }
}
