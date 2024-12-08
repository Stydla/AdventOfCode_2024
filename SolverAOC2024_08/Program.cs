﻿using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2024_08
{
  public class Program : BaseAdventSolver, IAdventSolver
  {

    public override string SolverName => "Day 8: Resonant Collinearity"/*TODO: Task Name*/;

    public override string InputsFolderName => "SolverAOC2024_08";

    public override string SolveTask1(string InputData)
    {
      Data d = new Data(InputData);
      var res = d.Solve1();
      return res.ToString();
    }

    public override string SolveTask2(string InputData)
    {
      Data d = new Data(InputData);
      var res = d.Solve2();
      return res.ToString();
    }
  }
}
