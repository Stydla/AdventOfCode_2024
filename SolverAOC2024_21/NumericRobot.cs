using AoCLib;
using AoCLib.BFS;
using AoCLib.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2024_21
{
  internal class NumericRobot : RobotBase
  {
    public override void AddKeypadNodes()
    {

      /*
+---+---+---+
| 7 | 8 | 9 |
+---+---+---+
| 4 | 5 | 6 |
+---+---+---+
| 1 | 2 | 3 |
+---+---+---+
    | 0 | A |
    +---+---+
       */

      AddNode(0, 0, '7');
      AddNode(0, 1, '8');
      AddNode(0, 2, '9');
      AddNode(1, 0, '4');
      AddNode(1, 1, '5');
      AddNode(1, 2, '6');
      AddNode(2, 0, '1');
      AddNode(2, 1, '2');
      AddNode(2, 2, '3');
      AddNode(3, 1, '0');
      AddNode(3, 2, 'A');

    }

  }
 


}
