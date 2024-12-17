using SolverAOC2024_17.Instructions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2024_17
{
  internal class Computer
  {
    public long RegA { get; set; }
    public long RegB { get; set; }
    public long RegC { get; set; }
    public List<int> Program {  get; set; }
    public int InstructionPointer { get; set; }

    public List<int> Output { get; set; } = new List<int>();
    public List<IInstruction> Instructions { get; set; } = new List<IInstruction>();
    public IInstruction CurrentInstruction
    {
      get
      {
        return Instructions[Program[InstructionPointer]];
      }
    }

    public int CurrentOperand
    {
      get
      {
        return Program[InstructionPointer + 1];
      }
    }

    public long Combo
    {
      get
      {
        if (CurrentOperand < 4)
        {
          return CurrentOperand;
        }
        if (CurrentOperand == 4)
        {
          return RegA;
        }
        if (CurrentOperand == 5)
        {
          return RegB;
        }
        if (CurrentOperand == 6)
        {
          return RegC;
        }
        throw new Exception($"Invalid Combo {CurrentOperand}");
      }
    }


    public string OutputString()
    {
      return string.Join(",", Output);
    }
    public long OutputLongR()
    {
      
      return long.Parse(string.Join("", Output.Select(x => x + 1).Reverse<int>()));
    }
    public long ProgramLongR()
    {
      return long.Parse(string.Join("", Program.Select(x=>x + 1).Reverse<int>()));
    }

    public int Literal
    {
      get
      {
        return CurrentOperand;
      }
    }

    public bool ISOutputProgram()
    {
      if (Program.Count != Output.Count) return false;
      for(int i = 0; i < Program.Count; i++)
      {
        if (Program[i] != Output[i]) return false;
      }
      return true;
    }


    public Computer(long regA, long regB, long regC, List<int> program)
    {
      RegA = regA;
      RegB = regB;
      RegC = regC;
      Program = program;

      Instructions.Add(new Adv());
      Instructions.Add(new Bxl());
      Instructions.Add(new Bst());
      Instructions.Add(new Jnz());
      Instructions.Add(new Bxc());
      Instructions.Add(new Out());
      Instructions.Add(new Bdv());
      Instructions.Add(new Cdv());
    }

    public void Run()
    {
      while(InstructionPointer < Program.Count)
      {
        IInstruction instruction = CurrentInstruction;
        instruction.Execute(this);
        if (instruction is Jnz) continue;

        InstructionPointer += 2;
      }
    }

    public void Run2()
    {
      while (InstructionPointer < Program.Count)
      {
        IInstruction instruction = CurrentInstruction;
        instruction.Execute(this);
        if (instruction is Jnz) continue;

        //if (!IsLastOutputCorrect()) break;

        InstructionPointer += 2;
      }
    }

    private bool IsLastOutputCorrect()
    {
      if (Output.Count == 0) return true;
      int index = Output.Count - 1;
      return Output[index] == Program[index];

    }
  }
}
