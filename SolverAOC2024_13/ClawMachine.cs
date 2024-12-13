using AoCLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SolverAOC2024_13
{
  internal class ClawMachine
  {

    public Button ButtonA { get; set; }
    public Button ButtonB { get; set; }
    public Price Price { get; set; }
   
    public ClawMachine(List<string> lines, ref int line) 
    {
      Regex btnA = new Regex("Button A: X\\+(\\d*), Y\\+(\\d*)");
      Match mA = btnA.Match(lines[line]);
      long x = long.Parse(mA.Groups[1].Value);
      long y = long.Parse(mA.Groups[2].Value);
      ButtonA = new Button(x, y);
      
      Regex btnB = new Regex("Button B: X\\+(\\d*), Y\\+(\\d*)");
      Match mB = btnB.Match(lines[line + 1]);
      x = long.Parse(mB.Groups[1].Value);
      y = long.Parse(mB.Groups[2].Value);
      ButtonB = new Button(x, y);

      Regex btnP = new Regex("Prize: X=(\\d*), Y=(\\d*)");
      Match mP = btnP.Match(lines[line + 2]);
      x = long.Parse(mP.Groups[1].Value);
      y = long.Parse(mP.Groups[2].Value);
      Price = new Price(x, y);
      
      line += 4;
    }

    public int MinimumToknes(int maxClickCount, out bool found)
    {
      int minimumTokens = int.MaxValue;
      found = false;
      for(int i = 0; i <= maxClickCount; i++)
      {
        for(int j = 0; j <= maxClickCount; j++) 
        {
          if (ButtonA.X * i + ButtonB.X * j > Price.X) break;
          if (ButtonA.Y * i + ButtonB.Y * j > Price.Y) break;

          if(ButtonA.X * i + ButtonB.X * j == Price.X &&
            ButtonA.Y * i + ButtonB.Y * j == Price.Y)
          {
            int tokens = i * 3 + j;
            if(minimumTokens > tokens)
            {
              minimumTokens = tokens;
              found = true;
            }
          }
        }
      }

      return minimumTokens;
    }

    internal long MinimumToknes(out bool found)
    {

      CommonDiff cd = new CommonDiff(ButtonA, ButtonB);
      if (!cd.Possible)
      {
        found = false;
        return 0;
      }

      long offset = 24000;

      long cnt = (10000000000000 - offset) / cd.Diff;
      long rest = (10000000000000 - offset) % cd.Diff + offset;

      Price.X += rest;
      Price.Y += rest;

      long minTokens = MinimumToknes(1500, out found);

      minTokens += (cnt * cd.Tokens);


      return minTokens;
    }
  }
}
