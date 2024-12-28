using SolverAOC2024_24.Gates;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SolverAOC2024_24
{
  internal class Data : DataBase
  {

    public List<Wire> AllWires = new List<Wire>();
    public List<Gate> AllGates = new List<Gate>();

    public List<Wire> WiresZ = new List<Wire>();
    public List<Wire> WiresX = new List<Wire>();
    public List<Wire> WiresY = new List<Wire>();

    public int SwappedGatesCount;

    public Number NumberX = new Number('X');
    public Number NumberY = new Number('Y');
    public Number NumberZ = new Number('Z');
    public long X;
    public long Y;

    

    public Data(string input) : base(input)
    {
      // optionaly parse data

      int gateId = 0;

      bool part1 = true;
      for (int i = 0; i < Lines.Count; i++)
      {
        string line = Lines[i];

        if(i == 0)
        {
          SwappedGatesCount = int.Parse(line);
          continue;
        }

        if(string.IsNullOrEmpty(line))
        {
          part1 = false;
          continue;
        }

        if(part1)
        {
          // parse part1
          Regex regex = new Regex(@"(.*): (\d)");
          Match m = regex.Match(line);

          string name = m.Groups[1].Value;
          bool value = m.Groups[2].Value == "1";
          Wire wire = new Wire(name);
          wire.Value = value;
          AllWires.Add(wire);
        }
        else
        {
          // parse part2
          Regex regex = new Regex(@"(.*) (.*) (.*) -> (.*)");
          Match m = regex.Match(line);

          string inputWire1 = m.Groups[1].Value;
          string gateString = m.Groups[2].Value;
          string inputWire2 = m.Groups[3].Value;
          string outputWire = m.Groups[4].Value;

          Wire w1 = GetWire(inputWire1);
          Wire w2 = GetWire(inputWire2);
          Wire w3 = GetWire(outputWire);

          Gate gate = Gate.Create(gateId++ , gateString, w1, w2, w3);
          AllGates.Add(gate);

        }
      }

      WiresZ = AllWires.Where(x => x.Name.StartsWith("z")).OrderBy(x => x.Name).ToList();
      WiresX = AllWires.Where(x => x.Name.StartsWith("x")).OrderBy(x => x.Name).ToList();
      WiresY = AllWires.Where(x => x.Name.StartsWith("y")).OrderBy(x => x.Name).ToList();

      foreach (Wire w in WiresZ)
      {
        w.Number = NumberZ;
      }
      long xn = GetNumber('x');
      foreach (Wire x in WiresX)
      {
        x.Number = NumberX;
      }
      long yn = GetNumber('y');
      foreach (Wire y in WiresY)
      {
        y.Number = NumberY;
      }
      NumberY.Value = yn;
      NumberX.Value = xn;
    }

    public Wire GetWire(string name)
    {
      Wire w = AllWires.FirstOrDefault(x => x.Name == name);
      if(w == null)
      {
        w = new Wire(name);
        AllWires.Add(w);
      }
      return w;
    }

    public object Solve1()
    {
      SolveZ();
      long res = NumberZ.Value;
      return res;
    }

    public void SolveZ()
    {
      foreach (Wire wire in WiresZ)
      {
        wire.Solve();
      } 
    }

    private string GetGraphviz()
    {
      StringBuilder sb = new StringBuilder();

      foreach (Gate g in AllGates)
      {
        sb.AppendLine($"{g.Input1.Name} -> {g.GateType}_{g.ID};");
        sb.AppendLine($"{g.Input2.Name} -> {g.GateType}_{g.ID};");
        sb.AppendLine($"{g.GateType}_{g.ID} -> {g.Output.Name};");

      }

      return sb.ToString();
    }


    public long GetNumber(char wireChar)
    {
      var wires = AllWires.Where(x => x.Name[0] == wireChar).OrderBy(x => x.Name);

      List<bool> results = new List<bool>();

      foreach (Wire wire in wires)
      {
        results.Add(wire.Value);
      }
      results.Reverse();

      string valueStr = string.Join("", results.Select(x => x ? "1" : "0"));
      long res = Convert.ToInt64(valueStr, 2);
      return res;
    }

    public long GetNumber(List<Wire> wires)
    {
      long res = 0;
      int index = 0;
      foreach (Wire wire in wires)
      {
        if(wire.Value)
        {
          res += 1L << index;
        }
        index++;
      }
      return res;
    }


    private void Test()
    {
      SolveZ();

      Debug.WriteLine($"{NumberX.Value} + {NumberY.Value} = {NumberZ.Value}");
      
    }

    private void SwapGatesOutput(Gate g1, Gate g2)
    {
      Wire w1 = g1.Output;
      Wire w2 = g2.Output;

      w1.OutputGate = g2;
      w2.OutputGate = g1;

      g1.Output = w2;
      g2.Output = w1;

      

    }

    public object Solve2()
    {

      if(SwappedGatesCount == 4)
      {

        //for (int i = 0; i < 45; i++)
        //{
        //  Debug.WriteLine($"i = {i}");
        //  NumberX.Value = 0;
        //  NumberY.Value = (1L << i) - 1;

        //  Test();

        //  Debug.WriteLine("");
        //}

        //string graphviz = GetGraphviz();  

        Gate g1 = AllGates.First(x => x.ID == 119);
        Gate g2 = AllGates.First(x => x.ID == 28);
        SwapGatesOutput(g1, g2);

        Gate g3 = AllGates.First(x => x.ID == 141);
        Gate g4 = AllGates.First(x => x.ID == 203);
        SwapGatesOutput(g3, g4);

        Gate g5 = AllGates.First(x => x.ID == 213);
        Gate g6 = AllGates.First(x => x.ID == 88);
        SwapGatesOutput(g5, g6);

        Gate g7 = AllGates.First(x => x.ID == 52);
        Gate g8 = AllGates.First(x => x.ID == 210);
        SwapGatesOutput(g7, g8);

        List<Gate> gates = new List<Gate>() { g1, g2, g3, g4, g5, g6, g7, g8 };

        return string.Join(",", gates.OrderBy(x => x.Output.Name).Select(x => x.Output.Name));
      }
      
     
      string res = "";

      HashSet<Gate> swapped = new HashSet<Gate>();
      SolveZ();
      X = GetNumber(WiresX);
      Y = GetNumber(WiresY);
      if (!SwapGates(SwappedGatesCount, swapped, 0, SwappedGatesCount == 2))
      {
        res = "No solution found";
      }
      else
      {
        List<Wire> resWires = swapped.Select(x => x.Output).OrderBy(x => x.Name).ToList();
        res = string.Join(",", resWires);
      }

      return res;
    }

    private bool SwapGates(int count, HashSet<Gate> swapped, int startIndex, bool test)
    {
      if (count == 0)
      {
        if(test)
        {
          return CheckCircuitTest(swapped);
        } else
        {
          return CheckCircuit(swapped);
        }
      }

      for (int i = startIndex; i < AllGates.Count; i++)
      {
        Gate gate1 = AllGates[i];
        if(swapped.Contains(gate1))
        {
          continue;
        }
        for (int j = i + 1; j < AllGates.Count; j++)
        {
          Gate gate2 = AllGates[j];
          if (swapped.Contains(gate2))
          {
            continue;
          }
          swapped.Add(gate1);
          swapped.Add(gate2);

          Wire tmp = gate1.Output;
          gate1.Output = gate2.Output;
          gate2.Output = tmp;
          gate1.SolveDown(new HashSet<Gate>());
          gate2.SolveDown(new HashSet<Gate>());

          if (SwapGates(count - 1, swapped, i + 1, test))
          {
            return true;
          }

          tmp = gate1.Output;
          gate1.Output = gate2.Output;
          gate2.Output = tmp;
          gate1.SolveDown(new HashSet<Gate>());
          gate2.SolveDown(new HashSet<Gate>());

          swapped.Remove(gate1);
          swapped.Remove(gate2);

        }
      }
      return false;
    }

    private bool CheckCircuitTest(HashSet<Gate> swapped)
    {
      long d1 = GetNumber(WiresX);
      long d2 = GetNumber('x');

      var wiresZ = AllWires.Where(x => x.Name[0] == 'z').OrderBy(x => x.Name).ToList();
      var wiresX = AllWires.Where(x => x.Name[0] == 'x').OrderBy(x => x.Name).ToList();
      //SolveZ();
      var wiresY = AllWires.Where(x => x.Name[0] == 'y').OrderBy(x => x.Name).ToList();

      //Debug.WriteLine($"{string.Join("", wiresX.Select(x => x.Value ? "1" : "0"))} + {string.Join("", wiresY.Select(x => x.Value ? "1" : "0"))} = {string.Join("", wiresZ.Select(x => x.Value ? "1" : "0"))}   ({string.Join(",", swapped.Select(a => a.Output).Select(a => a.Name))})");
      for (int i = 0; i < wiresZ.Count(); i++)
      {
        Wire wZ = wiresZ[i];
        Wire wX = wiresX[i];
        Wire wY = wiresY[i];
        
        if (wZ.Value != (wX.Value && wY.Value))
        {
          return false;
        }
      }
      //Debug.WriteLine($"({string.Join(",", swapped.Select(a => a.Output).Select(a => a.Name))})");
      return true;
    }

    private bool CheckCircuit(HashSet<Gate> swapped)
    {

      long z = NumberZ.Value;

      return X + Y == z;
    }


  }
}

