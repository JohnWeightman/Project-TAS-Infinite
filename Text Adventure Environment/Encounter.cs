using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Text_Adventure_Environment
{
    static class Encounter
    {
        public static List<EnemyNPC> EncounterNPCs = new List<EnemyNPC>();
        public static List<EnemyNPC> FightOrder = new List<EnemyNPC>();
        public static int EncounterXP = 0;
        public static int EncounterGold = 0;
        static List<int> Initiatives = new List<int>();

        #region Fight Setup

        public static void SortFightOrder()
        {
            if(EncounterNPCs.Count > 0)
            {
                Player.Initiative = RollInitiative(Player.DexMod);
                foreach (EnemyNPC Enemy in EncounterNPCs)
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
                    foreach (EnemyNPC Enemy in EncounterNPCs)
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
                EnemyNPC DefaultNPC = new EnemyNPC();
                DefaultNPC = GameObjects.NPCs[0];
                EncounterNPCs.Add(DefaultNPC);
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

        #region FightMechanics

        public static void StartEncounter(List<EnemyNPC> EncounterData)
        {
            foreach (EnemyNPC NPC in EncounterData)
                EncounterNPCs.Add(NPC);
            SortFightOrder();
            List<string> SE1 = new List<string>() { "Your under attack!", "" };
            foreach (EnemyNPC Enemy in EncounterNPCs)
                SE1.Add(Enemy.Name);
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
                    NPCTurn(FightOrder[CharTurn]);
                CharTurn++;
                if (CharTurn == FightOrder.Count)
                    CharTurn = 0;
                Finished = CheckFightStatus();
            }
            if (Player.HP <= 0)
            {
                FightOrder.Clear();
                EncounterNPCs.Clear();
                Player.PlayerDeath();
            }
            else
                EncounterEnd();
        }

        static void EncounterEnd()
        {
            List<string> Update = new List<string>() { "You won the fight!" };
            List<string> Options = new List<string>() { "Continue" };
            DrawGUI.UpdateStoryBox(Update);
            DrawGUI.UpdatePlayerOptions(Options);
            Events.NewEvent("EncounterWon", ES1: Player.Name);
            int Input = Player.PlayerInputs(Options.Count);
            List<string> Update2 = new List<string>() { "You won the fight!", "", "XP Earned: " + EncounterXP, "Gold Found: " + EncounterGold };
            Player.XP += EncounterXP;
            EncounterXP = 0;
            Player.Gold += EncounterGold;
            EncounterGold = 0;
            Player.Stamina = Player.StaminaMax;
            DrawGUI.UpdatePlayersThirdStatsBox();
            DrawGUI.UpdateInventory();
            DrawGUI.UpdateStoryBox(Update2);
            Input = Player.PlayerInputs(Options.Count);
            FightOrder.Clear();
            if (Player.XP >= Player.LU)
                Player.LevelUp();
        }

        static bool CheckFightStatus()
        {
            bool Finished = false;
            if (Player.HP <= 0 || EncounterNPCs.Count == 0)
                Finished = true;
            return Finished;
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
                            DrawGUI.UpdatePlayersFirstStatsBox();
                            AttackEnemy(TargetEnemy, "Heavy");
                        }
                        else
                        {
                            List<string> Update = new List<string>() { "Not Enough Stamina!" };
                            DrawGUI.UpdateStoryBox(Update);
                        }
                        break;
                    case 2:
                        if(Player.Stamina >= Player.FightOptionCosts[Input - 1])
                        {
                            Player.Stamina -= Player.FightOptionCosts[Input - 1];
                            TargetEnemy = WhichEnemy();
                            DrawGUI.UpdatePlayersFirstStatsBox();
                            AttackEnemy(TargetEnemy, "Light");
                        }
                        else
                        {
                            List<string> Update = new List<string>() { "Not Enough Stamina!" };
                            DrawGUI.UpdateStoryBox(Update);
                        }
                        break;
                    case 3:
                        if(Player.Stamina >= Player.FightOptionCosts[Input - 1])
                        {
                            Player.Stamina -= Player.FightOptionCosts[Input - 1];
                            DrawGUI.UpdatePlayersFirstStatsBox();
                            HealthPotion();
                        }
                        else
                        {
                            List<string> Update = new List<string>() { "Not Enough Stamina!" };
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
            Player.Stamina = Player.StaminaMax;
        }

        static int WhichEnemy()
        {
            List<string> EnemyList = new List<string>();
            foreach(EnemyNPC Enemy in EncounterNPCs)
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
            if (Attack >= EncounterNPCs[TargetEnemy].AC)
            {
                Events.NewEvent("AttackRoll", Attack - Player.StrMod - (Player.Level / 3), Player.StrMod, Player.Level / 3, Attack,
                    EncounterNPCs[TargetEnemy].AC, Player.Name, EncounterNPCs[TargetEnemy].Name, "HIT");Attack = DamageEnemy(AttackType, 
                    EncounterNPCs[TargetEnemy]);
                bool Dead = EncounterNPCs[TargetEnemy].TakeDamage(Attack);
                if (Dead)
                {
                    Events.NewEvent("NPCDeath", ES1: Player.Name, ES2: EncounterNPCs[TargetEnemy].Name);
                    List<string> Update = new List<string>() { "You strike down " + EncounterNPCs[TargetEnemy].Name + "!" };
                    FightOrder.Remove(EncounterNPCs[TargetEnemy]);
                    EncounterNPCs.Remove(EncounterNPCs[TargetEnemy]);
                    DrawGUI.UpdateNPCBoxes();
                    DrawGUI.UpdateStoryBox(Update);
                }
                else
                {
                    List<string> Update = new List<string>() { "You strike " + EncounterNPCs[TargetEnemy].Name + " for " + Attack + " Damage!" };
                    DrawGUI.UpdateStoryBox(Update);
                }
            }
            else
            {
                Events.NewEvent("AttackRoll", Attack - Player.StrMod - (Player.Level / 3), Player.StrMod, Player.Level / 3, Attack,
                    EncounterNPCs[TargetEnemy].AC, Player.Name, EncounterNPCs[TargetEnemy].Name, "MISS");
                List<string> Update = new List<string>() { "Your attack missed!" };
                DrawGUI.UpdateStoryBox(Update);
            }
        }

        static int DamageEnemy(string AttackType, EnemyNPC NPC)
        {
            int Damage = DiceRoller.RollDice(Player.Weapon.Damage) + Player.StrMod;
            int Damage2 = Damage;
            if (AttackType == "Light")
            {
                Damage2 = (Damage / 3) * 2;
                Events.NewEvent("LightDamageRoll", EN1: Damage - Player.StrMod, EN2: Player.StrMod, EN3: Damage2, ES1: Player.Name,
                    ES2: NPC.Name);
            }
            else
            {
                Events.NewEvent("HeavyDamageRoll", EN1: Damage - Player.StrMod, EN2: Player.StrMod, EN3: Damage2, ES1: Player.Name,
                    ES2: NPC.Name);
            }

            return Damage2;
        }

        static void HealthPotion()
        {
            if(Player.Inventory.Potions.Count > 0)
            {
                DrawGUI.OptionsSelectItem(Player.Inventory.Potions[0].Name);
                int Input = Player.PlayerInputs(4);
                if(Input == -1)
                {
                    Player.Stamina += Player.FightOptionCosts[2];
                    return;
                }
                else
                {
                    int Regen = Player.Inventory.Potions[Input].HealthRegen();
                    if (Player.HP + Regen > Player.MaxHP)
                        Regen = Player.MaxHP - Player.HP;
                    Player.HP += Regen;
                    List<string> Update = new List<string>() { "You recovered " + Regen + "HP!" };
                    DrawGUI.UpdateStoryBox(Update);
                    DrawGUI.UpdatePlayersFirstStatsBox();
                    DrawGUI.UpdateInventory();
                }
            }
            else
            {
                Player.Stamina += Player.FightOptionCosts[2];
                List<string> Update = new List<string>() { "You don't have any Health Potions!" };
                DrawGUI.UpdateStoryBox(Update);
            }
            if (false)
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
        }

        #endregion

        #region NPCs Turn

        static void NPCTurn(EnemyNPC NPC)
        {
            bool TurnDone = false;
            while (!TurnDone)
            {
                byte Decision = NPC.CombatDecision();
                switch (Decision)
                {
                    case 0:
                        NPC.Stamina -= Player.FightOptionCosts[0];
                        AttackPlayer(NPC, Decision);
                        break;
                    case 1:
                        NPC.Stamina -= Player.FightOptionCosts[1];
                        AttackPlayer(NPC, Decision);
                        break;
                    case 2:
                        TurnDone = !TurnDone;
                        break;
                    default:
                        break;
                }
            }
            NPC.Stamina = NPC.StaminaMax;
        }

        static void AttackPlayer(EnemyNPC NPC, byte AttackType)
        {
            int Attack = DiceRoller.RollDice(12) + NPC.StrMod + NPC.DifBonus;
            if(Attack >= Player.AC)
            {
                Events.NewEvent("AttackRoll", Attack - (NPC.StrMod + NPC.DifBonus), NPC.StrMod, NPC.DifBonus, Attack, Player.AC, NPC.Name, Player.Name, "HIT");
                DamagePlayer(NPC, AttackType);
            }
            else
            {
                Events.NewEvent("AttackRoll", Attack - (NPC.StrMod + NPC.DifBonus), NPC.StrMod, NPC.DifBonus, Attack, Player.AC, NPC.Name, Player.Name, "MISS");
                List<string> Update = new List<string>() { NPC.Name + " attacked you and missed!" };
                DrawGUI.UpdateStoryBox(Update);
            }
            List<string> Options = new List<string>() { "Continue" };
            DrawGUI.UpdatePlayerOptions(Options);
            int Input = Player.PlayerInputs(Options.Count);
        }

        static void DamagePlayer(EnemyNPC NPC, byte AttackType)
        {
            int Damage = DiceRoller.RollDice(NPC.Weapon.Damage) + NPC.StrMod;
            int Damage2 = Damage;
            if (AttackType == 1)
            {
                Damage2 = (Damage / 3) * 2;
                Events.NewEvent("LightDamageRoll", EN1: Damage - NPC.StrMod, EN2: NPC.StrMod, EN3: Damage2, ES1: NPC.Name, ES2: Player.Name);
            }
            else
            {
                Events.NewEvent("HeavyDamageRoll", EN1: Damage - NPC.StrMod, EN2: NPC.StrMod, EN3: Damage, ES1: NPC.Name, ES2: Player.Name);
            }
            Player.HP -= Damage2;
            List<string> Update = new List<string>() { NPC.Name + " attacked you for " + Damage2 + " Damage!" };
            DrawGUI.UpdateStoryBox(Update);
            DrawGUI.UpdatePlayersFirstStatsBox();
        }

        #endregion

    }
}
