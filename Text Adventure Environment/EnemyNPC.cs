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
        public string OffHand = "";
        public int XPValue = 0;

        public int Initiative = 0;

        public Weapon Weapon = new Weapon();
        public Armour Armour = new Armour();
        
        #endregion

        public bool TakeDamage(int Damage)
        {
            bool Dead = false;
            HP -= Damage;
            if(HP <= 0)
            {
                Player.FightXP += XPValue;
                Dead = true;
            }
            return Dead;
        }

    }
}
