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

        public static string Name = "Player";
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
        public static List<string> Inventory = new List<string>() {
            "Health Potion",
            "Health Potion",
            "Health Potion",
            "Key",
            "Key"
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
            string OptionString = Input.ToString();
            int OptionNumber = Convert.ToInt32(OptionString.Remove(0, 1));
            if ((Input == ConsoleKey.D1 && OptionNumber <= Options) || (Input == ConsoleKey.NumPad1 && OptionNumber <= Options))         
                return OptionNumber;
            else if((Input == ConsoleKey.D2 && OptionNumber <= Options) || (Input == ConsoleKey.NumPad2 && OptionNumber <= Options))
                return OptionNumber;
            else if((Input == ConsoleKey.D3 && OptionNumber <= Options) || (Input == ConsoleKey.NumPad3 && OptionNumber <= Options))
                return OptionNumber;
            else if ((Input == ConsoleKey.D4 && OptionNumber <= Options) || (Input == ConsoleKey.NumPad4 && OptionNumber <= Options))
                return OptionNumber;
            else if ((Input == ConsoleKey.D5 && OptionNumber <= Options) || (Input == ConsoleKey.NumPad5 && OptionNumber <= Options))
                return OptionNumber;
            else if ((Input == ConsoleKey.D6 && OptionNumber <= Options) || (Input == ConsoleKey.NumPad6 && OptionNumber <= Options))
                return OptionNumber;
            else if ((Input == ConsoleKey.D7 && OptionNumber <= Options) || (Input == ConsoleKey.NumPad7 && OptionNumber <= Options))
                return OptionNumber;
            else if ((Input == ConsoleKey.D8 && OptionNumber <= Options) || (Input == ConsoleKey.NumPad8 && OptionNumber <= Options))
                return OptionNumber;
            else if ((Input == ConsoleKey.D9 && OptionNumber <= Options) || (Input == ConsoleKey.NumPad9 && OptionNumber <= Options))
                return OptionNumber;
            else if ((Input == ConsoleKey.D0 && OptionNumber <= Options) || (Input == ConsoleKey.NumPad0 && OptionNumber <= Options))
                return OptionNumber;
            return 404;
        }

        #endregion

    }
}