using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2024_22
{
  internal class Data : DataBase
  {

    public List<SecretNumber> Numbers = new List<SecretNumber>();
    
    public Data(string input) : base(input)
    {
      // optionaly parse data
      foreach (var line in Lines)
      {
        SecretNumber number = new SecretNumber(long.Parse(line));
        Numbers.Add(number);

       
      }
    } 

    public object Solve1()
    {
      long sum = 0;
      foreach(SecretNumber number in Numbers)
      {
        SecretNumber tmp = number;
        for(int i = 0; i < 2000; i++)
        {
          tmp = tmp.Next();
        }
        sum += tmp.Value;
      }
      return sum;
    }

    public object Solve2()
    {
      List<Buyer> buyers = new List<Buyer>();
      List<Sequence> allSequences = GetAllSequences();
      foreach (SecretNumber number in Numbers)
      {
        Buyer buyer = new Buyer(number);
        buyers.Add(buyer);
      }
      long maxBananas = 0;

      foreach (Sequence sequence in allSequences)
      {
        long tmpBananas = 0;
        foreach (Buyer b in buyers)
        {
          long bananas = b.GetBananas(sequence);
          tmpBananas += bananas;
        }
        if (tmpBananas > maxBananas)
        {
          maxBananas = tmpBananas;
        }
      }
      return maxBananas;
    }

    private List<Sequence> GetAllSequences()
    {
      List<Sequence> AllSequences = new List<Sequence>(); 
      for (int i = -9; i < 10; i++)
      {
        for (int j = -9; j < 10; j++)
        {
          for (int k = -9; k < 10; k++)
          {
            for (int l = -9; l < 10; l++)
            {
              Sequence sequence = new Sequence(i, j, k, l);
              AllSequences.Add(sequence);
            }
          }
        }
      }
      return AllSequences;
    }


  }
}
