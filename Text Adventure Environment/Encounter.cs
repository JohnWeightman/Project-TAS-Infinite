using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Text_Adventure_Environment
{
    static class Encounter
    {
        public static List<EnemyNPC> FightOrder = new List<EnemyNPC>();
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
                        EnemyNPC PlayerIni = new EnemyNPC();
                        PlayerIni.Name = Player.Name;
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
            DrawGUI.UpdateNPCBoxes();
        }

        static int RollInitiative(int DexMod)
        {
            int Initiative = DiceRoller.RollDice(12) + DexMod;
            Initiatives.Add(Initiative);
            return Initiative;
        }

        #endregion

        #region Fight Code

        #region FightMechanics

        public static void StartEncounter()
        {
            SortFightOrder();
            List<string> SE1 = new List<string>() { "Your under attack!", "" };
            foreach (EnemyNPC Enemy in Enemies.EnemyList)
                SE1.Add(Enemy.Name);
            SE1.Add("");
            SE1.Add("Stamina: " + Player.Stamina + "/" + Player.StaminaMax);
            DrawGUI.UpdateStoryBox(SE1);
            List<string> SE2 = new List<string>() { "Continue" };
            DrawGUI.UpdatePlayerOptions(SE2);
            int Input = Player.PlayerInputs(SE2.Count);
            EncounterLoop();
        }

        static void EncounterLoop()
        {
            bool Finished = false;
            sbyte CharTurn = 0;
            while (!Finished)
            {
                if (FightOrder[CharTurn].Name == Player.Name)
                    PlayerTurn();
                else
                    NPCTurn();
                CharTurn++;
                if (CharTurn == FightOrder.Count)
                    CharTurn = 0;
                Finished = CheckFightStatus();
            }
        }

        #endregion

        #region Players Turn

        static void PlayerTurn()
        {
            bool TurnDone = false;
            while (!TurnDone)
            {
                DrawGUI.UpdatePlayerOptions(Player.FightOptions);
                int Input = Player.PlayerInputs(Player.FightOptions.Count);
                int TargetEnemy = 0;
                switch (Input)
                {
                    case 1:
                        if(Player.Stamina >= Player.FightOptionCosts[Input - 1])
                        {
                            Player.Stamina -= Player.FightOptionCosts[Input - 1];
                            TargetEnemy = WhichEnemy();
                            AttackEnemy(TargetEnemy, "Heavy");
                        }
                        else
                        {
                            List<string> Update = new List<string>() { "Not Enough Stamina!", "", "Stamina: " + Player.Stamina + "/" + Player.StaminaMax };
                            DrawGUI.UpdateStoryBox(Update);
                        }
                        break;
                    case 2:
                        if(Player.Stamina >= Player.FightOptionCosts[Input - 1])
                        {
                            Player.Stamina -= Player.FightOptionCosts[Input - 1];
                            TargetEnemy = WhichEnemy();
                            AttackEnemy(TargetEnemy, "Light");
                        }
                        else
                        {
                            List<string> Update = new List<string>() { "Not Enough Stamina!", "", "Stamina: " + Player.Stamina + "/" + Player.StaminaMax };
                            DrawGUI.UpdateStoryBox(Update);
                        }
                        break;
                    case 3:
                        if(Player.Stamina >= Player.FightOptionCosts[Input - 1])
                        {
                            Player.Stamina -= Player.FightOptionCosts[Input - 1];
                            HealthPotion();
                        }
                        else
                        {
                            List<string> Update = new List<string>() { "Not Enough Stamina!", "", "Stamina: " + Player.Stamina + "/" + Player.StaminaMax };
                            DrawGUI.UpdateStoryBox(Update);
                        }
                        break;
                    case 4:
                        TurnDone = true;
                        break;
                    default:
                        break;
                }
                if(!TurnDone)
                    TurnDone = CheckFightStatus();
            }
        }

        static int WhichEnemy()
        {
            List<string> EnemyList = new List<string>();
            foreach(EnemyNPC Enemy in Enemies.EnemyList)
            {
                EnemyList.Add(Enemy.Name);
            }
            DrawGUI.UpdatePlayerOptions(EnemyList);
            int Input = Player.PlayerInputs(EnemyList.Count) - 1;
            return Input;
        }

        static void AttackEnemy(int TargetEnemy, string AttackType)
        {
            int Attack = DiceRoller.RollDice(12) + Player.StrMod + (Player.Level / 3);
            if (Attack >= Enemies.EnemyList[TargetEnemy].AC)
            {
                Attack = DamageEnemy(AttackType);
                bool Dead = Enemies.EnemyList[TargetEnemy].TakeDamage(Attack);
                if (Dead)
                {
                    List<string> Update = new List<string>() { "You strike down " + Enemies.EnemyList[TargetEnemy].Name + "!", "", "Stamina: " + Player.Stamina + "/" + Player.StaminaMax };
                    FightOrder.Remove(Enemies.EnemyList[TargetEnemy]);
                    Enemies.EnemyList.Remove(Enemies.EnemyList[TargetEnemy]);
                    DrawGUI.UpdateNPCBoxes();
                    DrawGUI.UpdateStoryBox(Update);
                }
                else
                {
                    List<string> Update = new List<string>() { "You strike " + Enemies.EnemyList[TargetEnemy].Name + " for " + Attack + " Damage!", "", "Stamina: " + Player.Stamina + "/" + Player.StaminaMax };
                    DrawGUI.UpdateStoryBox(Update);
                }
            }
            else
            {
                List<string> Update = new List<string>() { "Your attack missed!", "", "Stamina: " + Player.Stamina + "/" + Player.StaminaMax };
                DrawGUI.UpdateStoryBox(Update);
            }
        }

        static int DamageEnemy(string AttackType)
        {
            int Damage = DiceRoller.RollDice(Player.Weapon.Damage) + Player.StrMod;
            if (AttackType == "Light")
                Damage = (Damage / 3) * 2;
            return Damage;
        }

        static void HealthPotion()
        {
            bool Potion = false;
            foreach(string Item in Player.Inventory)
            {
                if(Item == "Health Potion")
                {
                    Potion = !Potion;
                    break;
                }
            }
            if (Potion)
            {
                int Regen = 0;
                for (int x = 0; x < Player.Level; x++)
                    Regen += DiceRoller.RollDice(4) + 1;
                if (Regen > (Player.MaxHP - Player.HP))
                    Regen = Player.MaxHP - Player.HP;
                Player.HP += Regen;
                List<string> Update = new List<string>() { "You Heal for " + Regen + "HP!", "", "Stamina: " + Player.Stamina + "/" + Player.StaminaMax };
                DrawGUI.UpdateStoryBox(Update);            
            }
            else
            {
                Player.Stamina += Player.FightOptionCosts[2];
                List<string> Update = new List<string>() { "You don't have any Health Potions!", "", "Stamina: " + Player.Stamina + "/" + Player.StaminaMax };
                DrawGUI.UpdateStoryBox(Update);
            }
        }

        #endregion

        static void NPCTurn()
        {

        }

        static bool CheckFightStatus()
        {
            bool Finished = false;
            if (Player.HP <= 0 || Enemies.EnemyList.Count == 0)
                Finished = true;
            return Finished;
        }

        #endregion

        public static void ClearFightOrder()
        {
            FightOrder.Clear();
        }
    }
}
