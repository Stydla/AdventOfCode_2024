using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2024_09
{
  internal class Block
  {

    public int ID { get; set; }

    public bool IsEmpty { get { return ID == -1; } }  

    public Block(int id) 
    {
      ID = id;
    }
    public override string ToString()
    {
      return ID == -1 ? "." : ID.ToString();
    }

  }
}
