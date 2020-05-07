using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Text_Adventure_Environment
{

    static class Player
    {

        #region Player Stats

        public static string Name = "ISP";
        public static int Level = 1;
        public static int HP = 13;
        public static int MaxHP = 13;
        public static int AC = 10;
        public static int Str = 12;
        public static int Dex = 9;
        public static int Con = 9;
        public static int StrMod = 4;
        public static int DexMod = 3;
        public static int ConMod = 3;
        public static int XP = 0;
        public static int LU = 100;
        public static string OffHand = "N/A";
        public static List<string> Inventory = new List<string>() { "Health Potion", "Health Potion", "Health Potion", "Key", "Key" };
        public static List<string> FightOptions = new List<string>() { "Heavy Attack (10s)", "Light Attack (7s)", "Drink Potion (3s)", "End Turn (0s)" };
        public static List<int> FightOptionCosts = new List<int>() { 10, 7, 3, 0 };
        public static int Initiative = 0;
        public static int Stamina = 10;
        public static int StaminaMax = 10;
        public static bool Dead = false;
        public static int Gold = 0;

        public static Weapon Weapon = new Weapon();
        public static Armour Armour = new Armour();

        #endregion

        #region Update Player Stats

        public static void UpdateAbilityModifiers()
        {
            Player.StrMod = Player.Str / 3;
            Player.DexMod = Player.Dex / 3;
            Player.ConMod = Player.Con / 3;
        }

        public static int UpdatePlayerAC()
        {
            int AC = 0;
            if (Player.Armour.Name != "N/A")
            {
                if(Player.Armour.Weight == "Light")
                {
                    if (Player.DexMod > 5)
                        AC = Player.Armour.AC + 5;
                    else
                        AC = Player.Armour.AC + Player.DexMod;
                }
                else
                {
                    AC = Player.Armour.AC;
                }
            }
            else
            {
                if (Player.DexMod > 5)
                    AC = 11;
                else
                    AC = 6 + Player.DexMod;
            }
            return AC;
        }

        #endregion

        #region Player Options

        public static int PlayerInputs(int Options)
        {
            bool Answer = false;
            int Input = 0;
            while (!Answer)
            {
                Input = Player.PlayerInputsLoop(Options);
                if (Input != 404)
                    Answer = !Answer;
            }
            return Input;
        }

        static int PlayerInputsLoop(int Options)
        {
            ConsoleKey Input = Console.ReadKey(true).Key;
            if (Input == ConsoleKey.D1 || Input == ConsoleKey.D2 || Input == ConsoleKey.D3 || Input == ConsoleKey.D4 || Input == ConsoleKey.D5 || 
                Input == ConsoleKey.D6 || Input == ConsoleKey.D7 || Input == ConsoleKey.D8 || Input == ConsoleKey.D9 || Input == ConsoleKey.D0 || 
                Input == ConsoleKey.NumPad1 || Input == ConsoleKey.NumPad2 || Input == ConsoleKey.NumPad3 || Input == ConsoleKey.NumPad4 || 
                Input == ConsoleKey.NumPad5 || Input == ConsoleKey.NumPad6 || Input == ConsoleKey.NumPad7 || Input == ConsoleKey.NumPad8 || 
                Input == ConsoleKey.NumPad9 || Input == ConsoleKey.NumPad0)
            {
                string OptionString = Input.ToString();
                int OptionNumber = Convert.ToInt32(OptionString.Remove(0, 1));
                if (OptionNumber <= Options)
                    return OptionNumber;                
            }
            return 404;
        }

        #endregion

        #region Character Creation

        public static void StartCharacterCreator()
        {
            Dead = false;
            CharacterName();
            CharacterAbilityPoints();
            EmptyOtherStats();
            DisplayCharacterSheet();
            DrawGUI.UpdatePlayersStatBoxes();
        }

        static void CharacterName()
        {
            List<string> CCName1 = new List<string>() {"Welcome to the Character Creator", "", "Name: "};
            DrawGUI.UpdateStoryBox(CCName1);
            Player.Name = Console.ReadLine();
            List<string> CCName2 = new List<string>() { "Welcome to the Character Creator", "", "Are you sure you want to be known as " + Player.Name + "?"};
            DrawGUI.UpdateStoryBox(CCName2);
            List<string> Options = new List<string>() { "Yes", "No" };
            DrawGUI.UpdatePlayerOptions(Options);
            int Input = PlayerInputs(Options.Count);
            switch (Input)
            {
                case 1:
                    return;
                case 2:
                    CharacterName();
                    break;
                default:
                    CharacterName();
                    break;
            }
        }

        static void CharacterAbilityPoints()
        {
            RandomizeAbilityPoints();
            List<string> CCAbilities1 = new List<string>() {"Welcome to the Character Creator", "", "Name: " + Name, "", "HP: " + HP + "/" + MaxHP, "",
                "Str: " + Str + " (+" + StrMod + ")", "Dex: " + Dex + " (+" + DexMod + ")", "Con: " + Con + " (+" + ConMod + ")"};
            DrawGUI.UpdateStoryBox(CCAbilities1);
            List<string> Options = new List<string>() {"Reroll Stats", "Continue"};
            DrawGUI.UpdatePlayerOptions(Options);
            int Input = PlayerInputs(Options.Count);
            switch (Input)
            {
                case 1:
                    CharacterAbilityPoints();
                    break;
                case 2:
                    return;
                default:
                    return;
            }
        }

        static void RandomizeAbilityPoints()
        {
            Str = 1;
            Dex = 1;
            Con = 1;
            int Count = 27;
            while(Count > 0)
            {
                int Stat = DiceRoller.RollDice(3);
                switch (Stat)
                {
                    case 1:
                        Str += 1;
                        if(Str > 15)
                        {
                            Str = 15;
                            Count++;
                        }
                        break;
                    case 2:
                        Dex += 1;
                        if(Dex > 15)
                        {
                            Dex = 15;
                            Count++;
                        }
                        break;
                    case 3:
                        Con += 1;
                        if(Con > 15)
                        {
                            Con = 15;
                            Count++;
                        }
                        break;
                    default:
                        Str += 1;
                        break;
                }
                Count--;
            }
            UpdateAbilityModifiers();
            MaxHP = 10 + ConMod;
            HP = MaxHP;
            StaminaMax = 4 + (2 * DexMod);
            if (StaminaMax < 7)
                StaminaMax = 7;
            Stamina = StaminaMax;            
        }

        static void EmptyOtherStats()
        {
            Armour.UpdateArmourString("N/A");
            Weapon.UpdateWeaponString("Shortsword");
            AC = UpdatePlayerAC();
            XP = 0;
            LU = 100;
            OffHand = "N/A";
            Inventory.Clear();
            Gold = 0;
        }

        #endregion

        public static void DisplayCharacterSheet()
        {
            List<string> CharacterSheet = new List<string>() { "Welcome to the Character Creator", "", "Name: " + Name, "Level: " + Level, "HP: " + HP + "/" + MaxHP, "STA: " + 
                Stamina + "/" + StaminaMax , "AC: " + AC, "", "Str: " + Str + " (+" + StrMod + ")", "Dex: " + Dex + " (+" + DexMod + ")", "Con: " + Con + " (+" + ConMod + ")", "",
                "XP: " + XP, "Next Level: " + LU, "XP Till Next Level: " + (LU - XP), "", "Weapon: " + Weapon.Name, "Off-Hand: " + OffHand, "Armour: " + Armour.Name};
            DrawGUI.UpdateStoryBox(CharacterSheet);
            List<string> Options = new List<string>() {"Continue"};
            DrawGUI.UpdatePlayerOptions(Options);
            int Input = PlayerInputs(Options.Count);
        }

        public static void LevelUp()
        {
            Level += 1;
            LU = (LU * 2) + 50;
            List<string> Update = new List<string>() { "Level Up!", "", "Level: " + Level, "", "You have 1 Ability Point to spend!", "Str: " + Str, "Dex: " + 
                Dex, "Con: " + Con };
            List<string> Options = new List<string>() { "Str", "Dex", "Con" };
            DrawGUI.UpdateStoryBox(Update);
            DrawGUI.UpdatePlayerOptions(Options);
            int Input = PlayerInputs(Options.Count);
            switch (Input)
            {
                case 1:
                    Str += 1;
                    break;
                case 2:
                    Dex += 1;
                    break;
                case 3:
                    Con += 1;
                    break;
                default:
                    break;
            }
            MaxHP += DiceRoller.RollDice(10) + ConMod;
            HP = MaxHP;
            UpdateAbilityModifiers();
            DrawGUI.UpdatePlayersFirstStatsBox();
            DrawGUI.UpdatePlayersSecondStatsBox();
            DrawGUI.UpdatePlayersThirdStatsBox();
            DisplayCharacterSheet();
        }

        public static void PlayerDeath()
        {
            List<string> Update = new List<string>() { "YOU DIED" };
            List<string> Options = new List<string>() { "Main Menu" };
            DrawGUI.UpdateStoryBox(Update);
            DrawGUI.UpdatePlayerOptions(Options);
            int Input = PlayerInputs(Options.Count);
            Dead = true;
        }

    }
}
