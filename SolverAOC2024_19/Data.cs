using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2024_19
{
  internal class Data : DataBase
  {

    public HashSet<Node> AllNodes { get; } = new HashSet<Node>();

    public List<string> Designes { get; } = new List<string>();

    public Data(string input) : base(input)
    {
      // optionaly parse data

      foreach (string stripe in Lines[0].Split(new string[] { ", " }, StringSplitOptions.None))
      {
        Node parent = null;
        for (int i = 0; i < stripe.Length; i++)
        {
          char value = stripe[i];

          Node current = null;
          if(parent != null)
          {
            current = parent.NextNodes.FirstOrDefault(x => x.Value == value);
            if(current == null)
            {
              current = new Node(value);
              AllNodes.Add(current);
              parent.NextNodes.Add(current);
            }
          } 
          else
          {
            current = AllNodes.FirstOrDefault(x => x.Value == value && x.IsFirst);
            if (current == null)
            { 
              current = new Node(value);
              AllNodes.Add(current);
            }
          }

          if(i == 0)
          {
            current.IsFirst = true;
          }
          if(i == stripe.Length - 1)
          {
            current.IsFinal = true;
          }
          parent = current;
        }
      }

      var finalNodes = AllNodes.Where(x => x.IsFinal).ToList();
      var firstNodes = AllNodes.Where(x => x.IsFirst).ToList();
      foreach (Node final in finalNodes)
      {
        foreach (Node first in firstNodes)
        {
          final.AddNextNode(first);
        }
      }

      for(int i = 2; i < Lines.Count; i++)
      {
        Designes.Add(Lines[i]);
      }
    }

    public object Solve1()
    {
      int sum = 0;
      foreach(string design in Designes)
      {

        HashSet<Node> currentNodes; 
        HashSet<Node> nextNodes = new HashSet<Node>();

        char valueStart = design[0];
        currentNodes = AllNodes.Where(x => x.IsFirst && x.Value == valueStart).ToHashSet();

        List<int> counts = new List<int>(Enumerable.Repeat(0, design.Length));
        counts[0] = currentNodes.Count(x=>x.IsFinal);


        for (int i = 1; i < design.Length; i++)
        {
          char value = design[i];

          foreach (Node node in currentNodes)
          {
            foreach (Node next in node.NextNodes)
            {
              if (next.Value == value)
              {
                if (!nextNodes.Contains(next))
                {
                  nextNodes.Add(next);
                  counts[i]++;
                }
              }
            }
          }
          currentNodes = nextNodes;
          nextNodes = new HashSet<Node>();  
        }

        bool valid = currentNodes.Any(x => x.IsFinal);
        if (valid)
        {
          sum++;
        }

      }

      return sum;
    }

    public object Solve2()
    {
      long sum = 0;
      foreach (string design in Designes)
      {

        HashSet<CountedNode> currentNodes;
        HashSet<CountedNode> nextNodes = new HashSet<CountedNode>();

        char valueStart = design[0];
        currentNodes = AllNodes.Where(x => x.IsFirst && x.Value == valueStart).Select(x=>new CountedNode(x)).ToHashSet();
        foreach (var item in currentNodes)
        {
          item.Count = 1;
        }


        for (int i = 1; i < design.Length; i++)
        {
          char value = design[i];

          foreach (CountedNode currentCountedNode in currentNodes)
          {
            foreach (CountedNode nextCountedNode in currentCountedNode.Node.NextNodes.Select(x=>new CountedNode(x)))
            {
              if (nextCountedNode.Node.Value == value)
              {
                if (!nextNodes.Select(x=>x.Node).Contains(nextCountedNode.Node))
                {
                  nextNodes.Add(nextCountedNode);
                  nextCountedNode.Count = currentCountedNode.Count;
                } else
                {
                  nextNodes.First(x=>x.Node.Equals(nextCountedNode.Node)).Count += currentCountedNode.Count;
                }
              }
            }
          }
          currentNodes = nextNodes;
          nextNodes = new HashSet<CountedNode>();
        }

        var finalNodes =  currentNodes.Where(x => x.Node.IsFinal);


        sum += finalNodes.Sum(x => x.Count);
        

      }

      return sum;
    }

    private class CountedNode
    {
      public Node Node { get; set; }
      public long Count { get; set; }
      public CountedNode(Node node)
      {
        Node = node;
      }
    }

  }
}
