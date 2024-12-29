using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2024_21
{
  internal class DirectionRobot : RobotBase
  {
    public override void AddKeypadNodes()
    {
      /*
    +---+---+
    | ^ | A |
+---+---+---+
| < | v | > |
+---+---+---+
       */

      AddNode(0, 1, '^');
      AddNode(0, 2, 'A');
      AddNode(1, 0, '<');
      AddNode(1, 1, 'v');
      AddNode(1, 2, '>');

    }

  }
}
