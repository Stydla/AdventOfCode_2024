using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2024_17.Instructions
{
  internal interface IInstruction
  {
    int ID { get; }
    void Execute(Computer computer);

  }
}
