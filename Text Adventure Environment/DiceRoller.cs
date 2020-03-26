using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Text_Adventure_Environment
{
    static class DiceRoller
    {
        private static Random Ran = new Random();

        public static int RollDice(int max)
        {
            return Ran.Next(1, (max + 1));
        }
    }
}
