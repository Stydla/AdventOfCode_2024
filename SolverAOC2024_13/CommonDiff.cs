using AoCLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2024_13
{
  internal class CommonDiff
  {

    public long BtnAPress { get; private set; }
    public long BtnBPress { get; private set; }

    public long Diff { get; private set; }
    public bool Possible { get; private set; } = true;

    public long Tokens
    {
      get
      {
        return BtnAPress * 3 + BtnBPress;
      }
    }
    

    public CommonDiff(Button btnA, Button btnB) 
    {
      
        BtnBPress = Math.Abs(btnA.X - btnA.Y);
        BtnAPress = Math.Abs(btnB.Y - btnB.X);
      
      
      ulong gcd = ModuloMath.GCD((ulong)BtnAPress, (ulong)BtnBPress);

      BtnAPress = (long)((ulong)BtnAPress / gcd);
      BtnBPress = (long)((ulong)BtnBPress / gcd);

      Diff = BtnAPress * btnA.X + BtnBPress * btnB.X;

      if(Diff != BtnAPress * btnA.Y + BtnBPress * btnB.Y)
      {
        Possible = false;
      }

    }

    public override string ToString()
    {
      return $"{Diff} {BtnAPress} {BtnBPress}";
    }

  }
}

