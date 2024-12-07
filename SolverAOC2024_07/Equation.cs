using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SolverAOC2024_07
{
  internal class Equation
  {

    public List<long> Values { get; set; }

    public long Result { get; set; }

    public Equation(string input)
    {
      Regex r = new Regex("(\\d*): (.*)");
      var matches = r.Match(input);
      Result = long.Parse(matches.Groups[1].Value);

      Values = matches.Groups[2].Value.Split(' ').Select(x => long.Parse(x)).ToList();
    }

    internal bool IsValid()
    {
      return IsValid(1, Values[0]);
    }

    private bool IsValid(int position, long computedResult)
    {
      if (position == Values.Count)
      {
        return Result == computedResult;
      }

      if (IsValid(position + 1, computedResult + Values[position]))
      {
        return true;
      }

      if (IsValid(position + 1, computedResult * Values[position]))
      {
        return true;
      }

      return false;
    }

    internal bool IsValid2()
    {
      return IsValid2(1, Values[0]);
    }

    private bool IsValid2(int position, long computedResult)
    {
      if (position == Values.Count)
      {
        return Result == computedResult;
      }

      if (IsValid2(position + 1, computedResult + Values[position]))
      {
        return true;
      }

      if (IsValid2(position + 1, computedResult * Values[position]))
      {
        return true;
      }

      if (IsValid2(position + 1, long.Parse($"{computedResult}{Values[position]}")))
      {
        return true;
      }

      return false;
    }
  }
}
