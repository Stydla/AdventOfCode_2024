using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2024_09
{
  internal class File
  {

    public int StartIndex { get; set; }
    public int EndIndex { get; set; }
    public int Length
    {
      get
      {
        return EndIndex - StartIndex + 1;
      }
    }
    public bool IsEmpty
    {
      get
      {
        return  ID == -1;
      }
    }

    public int ID { get; internal set; }

    public override string ToString()
    {
      return $"{ID,10} ({StartIndex};{EndIndex})";
    }
  }
}
