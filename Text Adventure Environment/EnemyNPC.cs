using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Text_Adventure_Environment
{
    class EnemyNPC
    {

        #region Enemy Data

        public string Name = "";
        public int HP = 0;
        public int AC = 0;
        public int Str = 0;
        public int Dex = 0;
        public int Con = 0;
        public int StrMod = 0;
        public int DexMod = 0;
        public int ConMod = 0;
        public string Weapon = "";
        public string OffHand = "";
        public string Armour = "";
        public int XPValue = 0;

        public int Initiative = 0;
        #endregion

    }
}
