using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryClass
{
    interface BattleCommands
    {
        int CalculateAttack();
        int CalculateDefense(int value);
        int CalculateEscape();
    }
}
