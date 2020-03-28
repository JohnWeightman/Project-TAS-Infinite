using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Text_Adventure_Environment
{
    static class Encounter
    {
        public static List<object> FightOrder = new List<object>();
        static List<int> Initiatives = new List<int>();

        #region Fight Setup

        public static void SortFightOrder()
        {
            if(Enemies.EnemyList.Count > 0)
            {
                Player.Initiative = RollInitiative(Player.DexMod);
                foreach (EnemyNPC Enemy in Enemies.EnemyList)
                {
                    Enemy.Initiative = RollInitiative(Enemy.DexMod);
                }
                int Ini = Initiatives.Max();
                while (Ini != 0)
                {
                    if (Player.Initiative == Ini)
                    {
                        TempPlayer PlayerIni = new TempPlayer();
                        FightOrder.Add(PlayerIni);
                    }
                    foreach (EnemyNPC Enemy in Enemies.EnemyList)
                    {
                        if (Enemy.Initiative == Ini)
                        {
                            FightOrder.Add(Enemy);
                        }
                    }
                    Ini--;
                }
            }
            else
            {
                List<string> DefaultEnemy = new List<string>() { "Bandit" };
                List<int> DefaultAmount = new List<int>() { 1 };
                Enemies.LoadEnemeisFromFile(DefaultEnemy, DefaultAmount);
                SortFightOrder();
            }
            DrawGUI.UpdateFightOrderBox();
        }

        static int RollInitiative(int DexMod)
        {
            int Initiative = DiceRoller.RollDice(12) + DexMod;
            Initiatives.Add(Initiative);
            return Initiative;
        }

        #endregion

    }
}
