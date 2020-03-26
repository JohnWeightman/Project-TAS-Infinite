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
        public static int HP = 90;
        public static int MaxHP = 90;
        public static int AC = 18;
        public static int Pro = 2;
        public static int Str = 18;
        public static int Dex = 16;
        public static int Con = 14;
        public static int XP = 1000;
        public static int LU = 2000;
        public static string Weapon = "Longsword";
        public static string OffHand = "Shield";
        public static string Armour = "Chainmail";
        public static List<string> Inventory = new List<string>()
        {
            "Health Potion", "Health Potion", "Health Potion", "Key", "Key"
        };

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

        public static int PlayerInputsLoop(int Options)
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
            CharacterName();
            CharacterAbilityPoints();
        }

        static void CharacterName()
        {
            DrawGUI.UpdateStoryBox("Welcome to the character Creator!                                                                                          " +
            "                                                                                                   Character Name: ");
            Player.Name = Console.ReadLine();
            DrawGUI.UpdateStoryBox("Welcome to the character Creator!                                                                                          " +
            "                                                                                                   Are you sure you want to be known as " +
            Player.Name + "?");
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
            DrawGUI.UpdateStoryBox("Welcome to the character Creator!                                                                                          " +
            "                                                                                                   Str: " + Player.Str + "                        " +
            "                                                                                Dex: " + Player.Dex + "                                           " +
            "                                                            Con: " + Player.Con);
            List<string> Options = new List<string>() {"Reroll Stats", "Continue"};
            DrawGUI.UpdatePlayerOptions(Options);
            int Input = PlayerInputs(Options.Count);
        }

        static void RandomizeAbilityPoints()
        {
            bool Finished = false;
            int Stat = 1;
            while (!Finished)
            {
                int[] Random = new int[] {DiceRoller.RollDice(6), DiceRoller.RollDice(6), DiceRoller.RollDice(6), DiceRoller.RollDice(6), DiceRoller.RollDice(6)};
                Array.Sort(Random);
                if (Stat == 1)
                {
                    Player.Str = Random[2] + Random[3] + Random[4];
                }
                else if( Stat == 2)
                {
                    Player.Dex = Random[2] + Random[3] + Random[4];
                }
                else
                {
                    Player.Con = Random[2] + Random[3] + Random[4];
                    Finished = !Finished;
                }
                Stat += 1;
                for(int x = 0; x < 2; x++)
                {
                    int Ran = DiceRoller.RollDice(3);
                    switch(Ran)
                    {
                        case 1:
                            Player.Str += 1;
                            break;
                        case 2:
                            Player.Dex += 1;
                            break;
                        case 3:
                            Player.Con += 1;
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        #endregion

    }
}