using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2024_22
{
  internal class Buyer
  {

    
    public Dictionary<Sequence, long> SequenceBananas { get; set; } = new Dictionary<Sequence, long>(); 

    private List<SecretNumber> SecretNumbers { get; set; } = new List<SecretNumber>();
    private List<long> Prices { get; set; } = new List<long>();
    public Buyer(SecretNumber initialNumber)
    {
      SecretNumbers.Add(initialNumber);
      Prices.Add(initialNumber.Value % 10);
      for (int i = 0; i < 2000; i++)
      {
        SecretNumber nextNumber = SecretNumbers.Last().Next();
        SecretNumbers.Add(nextNumber);
        Prices.Add(nextNumber.Value % 10);
      }

      for (int i = 0; i < Prices.Count - 4; i++)
      {

        Sequence sequence = new Sequence(Prices[i + 1] - Prices[i], Prices[i+2] - Prices[i + 1], Prices[i + 3] - Prices[i + 2], Prices[i + 4] - Prices[i + 3]);
        if(!SequenceBananas.ContainsKey(sequence))
        {
          SequenceBananas[sequence] = Prices[i + 4];
        }
        
      }
    }

    internal long GetBananas(Sequence sequence)
    {
      if(SequenceBananas.TryGetValue(sequence, out long bananas))
      {
        return bananas;
      }
      return 0;
    }
  }
}
