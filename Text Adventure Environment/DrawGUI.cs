using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Text_Adventure_Environment
{
    static class DrawGUI
    {

        #region Program Start

        public static void DrawGUIConsole()     //Main start function to set up interface
        {
            Console.SetWindowSize(171, 50);     
            Console.SetBufferSize(172, 51);
            Console.Clear();
            DrawBorders();                      
            DrawPlayersFirstStatsBox();        
            DrawPlayersSecondStatsBox();
            DrawPlayersThirdStatsBox();
            DrawPlayersFourthStatsBox();
            DrawPlayersFifthStatsBox();
            DrawFightOrderBox();                
            DrawEnemiesBox();
            UpdateStoryBox(StartDisplay.StartStory);
            List<string> StartDisplayOptions = StartDisplay.StartOptions;
            UpdatePlayerOptions(StartDisplayOptions);
            List<string> StartDisplayEvents = StartDisplay.StartEvents;
            UpdateEventBox();
            Encounter.EncounterNPCs.Clear();
            Encounter.FightOrder.Clear();
        }

        static void DrawBorders() //Draws the main borders for the interface
        {
            Draw.RectangleFromTop(170, 48, 0, 0, ConsoleColor.Blue);
            Draw.RectangleFromTop(170, 3, 0, 0, ConsoleColor.Blue);
            Draw.RectangleFromTop(146, 44, 24, 4, ConsoleColor.Blue);
            Draw.RectangleFromTop(24, 44, 146, 4, ConsoleColor.Blue);
            Draw.RectangleFromTop(122, 34, 24, 4, ConsoleColor.Blue);
            Draw.RectangleFromTop(61, 9, 85, 39, ConsoleColor.Blue);
            Console.SetCursorPosition(74, 2);
            Console.Write("Text Adventure GUI"); 
        }

        #endregion

        #region Additional Functions

        public static void UpdateNPCBoxes()
        {
            UpdateFightOrderBox();
            UpdateEnemiesBox();
        }

        public static void UpdatePlayersStatBoxes()
        {
            UpdatePlayersFirstStatsBox();
            UpdatePlayersSecondStatsBox();
            UpdatePlayersThirdStatsBox();
            UpdatePlayersFourthStatsBox();
            UpdateInventory();
        }

        public static void ClearAllBoxes()
        {
            ClearPlayersStatBoxes();
            ClearNPCBoxes();
            ClearGameBoxes();
        }

        public static void ClearPlayersStatBoxes()
        {
            ClearPlayersFirstStatsBox();
            ClearPlayersSecondStatsBox();
            ClearPlayersThirdStatsBox();
            ClearPlayersFourthStatsBox();
            ClearInventory();
        }

        public static void ClearNPCBoxes()
        {
            ClearFightOrderBox();
            ClearEnemiesBox();
        }

        public static void ClearGameBoxes()
        {
            ClearStory();
            ClearPlayerOptions();
            ClearEventsBox();
        }

        #endregion

        #region Players First Stats Box

        static void DrawPlayersFirstStatsBox() //Draws the Players first stats box
        {
            Draw.Rectangle(20, 5, 2, 3, Draw.DrawKind.BelowCursorButKeepCursorLocation, color: ConsoleColor.Green);
            UpdatePlayersFirstStatsBox(); 
        }

        public static void UpdatePlayersFirstStatsBox() //Updates the information in the Players first stats box
        {
            ClearPlayersFirstStatsBox(); 
            Console.SetCursorPosition(4, 6); 
            Console.Write("Name: " + Player.Name);
            Console.SetCursorPosition(4, 7);
            Console.Write("Lvl: " + Player.Level);
            Console.SetCursorPosition(4, 8);
            Console.Write("HP: " + Player.HP + "/" + Player.MaxHP);
            Console.SetCursorPosition(4, 9);
            Console.Write("STA: " + Player.Stamina + "/" + Player.StaminaMax);
            Console.SetCursorPosition(4, 10);
            Console.Write("AC: " + Player.AC);
        }

        public static void ClearPlayersFirstStatsBox() //Clears the Players first stats box
        {
            for (int YPos = 6; YPos < 11; YPos++)
            {
                Console.SetCursorPosition(4, YPos);
                for (int x = 0; x < 18; x++)
                {
                    Console.Write(" ");
                }
            }
        }

        #endregion

        #region Players Second Stats Box

        static void DrawPlayersSecondStatsBox() //Draws the Players second stats box
        {
            Draw.Rectangle(20, 3, 2, 1, Draw.DrawKind.BelowCursorButKeepCursorLocation, color: ConsoleColor.Green);
            UpdatePlayersSecondStatsBox();
        }

        public static void UpdatePlayersSecondStatsBox() //Updates the Players second stats box
        {
            ClearPlayersSecondStatsBox();
            Console.SetCursorPosition(4, 12);
            Console.Write("STR: " + Player.Str + " (+" + Player.StrMod + ")");
            Console.SetCursorPosition(4, 13);
            Console.Write("DEX: " + Player.Dex + " (+" + Player.DexMod + ")");
            Console.SetCursorPosition(4, 14);
            Console.Write("CON: " + Player.Con + " (+" + Player.ConMod + ")");
        }

        public static void ClearPlayersSecondStatsBox() //Clears the Players third stats box
        {
            for (int YPos = 12; YPos < 15; YPos++)
            {
                Console.SetCursorPosition(4, YPos);
                for (int x = 0; x < 18; x++)
                {
                    Console.Write(" ");
                }
            }
        }

        #endregion

        #region Players Third Stats Box

        static void DrawPlayersThirdStatsBox() //Draws the Players third stats box
        {
            Draw.Rectangle(20, 2, 2, 1, Draw.DrawKind.BelowCursorButKeepCursorLocation, color: ConsoleColor.Green);
            UpdatePlayersThirdStatsBox();
        }

        public static void UpdatePlayersThirdStatsBox() //Updates the Players third stat box
        {
            ClearPlayersThirdStatsBox();
            Console.SetCursorPosition(4, 16);
            Console.Write("XP: " + Player.XP);
            Console.SetCursorPosition(4, 17);
            Console.Write("LU: " + Player.LU);
        }

        public static void ClearPlayersThirdStatsBox() //Clears the Players third stat box
        {
            for (int YPos = 16; YPos < 18; YPos++)
            {
                Console.SetCursorPosition(4, YPos);
                for (int x = 0; x < 18; x++)
                {
                    Console.Write(" ");
                }
            }
        }

        #endregion

        #region Players Fourth Stats Box

        static void DrawPlayersFourthStatsBox() //Draws the Players fourth stats box
        {
            Draw.Rectangle(20, 3, 2, 1, Draw.DrawKind.BelowCursorButKeepCursorLocation, color: ConsoleColor.Green);
            Player.Weapon.UpdateWeaponString("Shortsword");
            Player.Armour.UpdateArmourString("Leather");
            UpdatePlayersFourthStatsBox();
        }

        public static void UpdatePlayersFourthStatsBox() //Update the Players fourth stat box
        {
            ClearPlayersFourthStatsBox();
            Console.SetCursorPosition(4, 19);
            Console.Write("Weapon: " + Player.Weapon.Name);
            Console.SetCursorPosition(4, 20);
            Console.Write("OfHand: " + Player.OffHand);
            Console.SetCursorPosition(4, 21);
            Console.Write("Armour: " + Player.Armour.Name);
        }

        public static void ClearPlayersFourthStatsBox() //Clear the Players fourth stat box
        {
            for (int YPos = 19; YPos < 22; YPos++)
            {
                Console.SetCursorPosition(4, YPos);
                for (int x = 0; x < 18; x++)
                {
                    Console.Write(" ");
                }
            }
        }

        #endregion

        #region Players Inventory Box

        static void DrawPlayersFifthStatsBox() //Draws the Players fifth stats box
        {
            Draw.Rectangle(20, 25, 2, 1, Draw.DrawKind.BelowCursorButKeepCursorLocation, color: ConsoleColor.Green);
            UpdateInventory();
        }

        public static void UpdateInventory() //Updates the Players Inventory box
        {
            ClearInventory();
            Console.SetCursorPosition(4, 23);
            Console.Write("Inventory");
            Console.SetCursorPosition(4, 25);
            Console.Write("Gold: " + Player.Gold);
            int YPos = 26;
            foreach (string Item in Player.Inventory)
            {
                Console.SetCursorPosition(4, YPos);
                Console.Write(Item);
                YPos += 1;
            }
        }

        public static void ClearInventory() //Clears the Players Inventory box
        {
            for (int YPos = 23; YPos < 48; YPos++)
            {
                Console.SetCursorPosition(4, YPos);
                for (int x = 0; x < 18; x++)
                {
                    Console.Write(" ");
                }
            }
        }

        #endregion

        #region Fight Order Box

        static void DrawFightOrderBox() //Draws the Fight Order box
        {
            Draw.Rectangle(20, 10, 148, -25, Draw.DrawKind.BelowCursorButKeepCursorLocation, color: ConsoleColor.Red);
            for (int x = 0; x < 7; x++)
                Encounter.EncounterNPCs.Add(GameObjects.NPCs[DiceRoller.RollDice(GameObjects.NPCs.Count - 1)]);
            Encounter.SortFightOrder();
            UpdateFightOrderBox();
        }

        public static void UpdateFightOrderBox() //Updates the Fight Order box
        {
            ClearFightOrderBox();
            Console.SetCursorPosition(150, 6);
            Console.Write("Fight Order");
            int YPos = 8;
            foreach (EnemyNPC Character in Encounter.FightOrder)
            {
                Console.SetCursorPosition(150, YPos);
                Console.Write(Character.Name);
                YPos += 1;
            }
        }

        public static void ClearFightOrderBox() //Clears the Fight Order box
        {
            for (int YPos = 6; YPos < 16; YPos++)
            {
                Console.SetCursorPosition(150, YPos);
                for (int x = 0; x < 18; x++)
                {
                    Console.Write(" ");
                }
            }
        }

        #endregion

        #region Enemies Box

        static void DrawEnemiesBox() //Draws the Enemies box
        {
            Draw.Rectangle(20, 10, 148, 1, Draw.DrawKind.BelowCursorButKeepCursorLocation, color: ConsoleColor.Red);
            UpdateEnemiesBox();
        }

        public static void UpdateEnemiesBox() //Updates the Enmies Box
        {
            ClearEnemiesBox();
            Console.SetCursorPosition(150, 17);
            Console.Write("Enemies");
            int YPos = 19;
            foreach (EnemyNPC Enemy in Encounter.EncounterNPCs)
            {
                Console.SetCursorPosition(150, YPos);
                Console.Write(Enemy.Name);
                YPos += 1;
            }
        }

        public static void ClearEnemiesBox() //Clears the Enemies box
        {
            for (int YPos = 17; YPos < 26; YPos++)
            {
                Console.SetCursorPosition(150, YPos);
                for (int x = 0; x < 18; x++)
                {
                    Console.Write(" ");
                }
            }
        }

        #endregion

        #region Story Box

        public static void UpdateStoryBox(List<string> StoryStrings) //Updates the Story box
        {
            ClearStory();
            int YPos = 6;
            foreach (string StoryString in StoryStrings)
            {
                List<string> Story = CheckStringLength(StoryString);
                foreach (string Line in Story)
                {
                    Console.SetCursorPosition(28, YPos);
                    Console.Write(Line);
                    YPos += 1;
                }
            }
        }

        static List<string> CheckStringLength(string Text)   //limites each written line is no longer than 97 characters
        {
            List<string> Story = new List<string>();
            bool TextTooLong = true;
            int CharNum = 110;
            while (TextTooLong)
            {
                if (Text.Length > CharNum)
                {
                    char[] CharString = Text.ToCharArray();
                    bool Found = false;
                    while (!Found)
                    {
                        if (CharString[CharNum] == ' ' || CharString[CharNum] == '.')
                        {
                            char[] chars = Text.ToCharArray(0, CharNum);
                            string Temp = new string(chars);
                            Story.Add(Temp);
                            if (CharString[CharNum] == ' ')
                            {
                                Text = Text.Substring(CharNum + 1);
                            }
                            else
                            {
                                Text = Text.Substring(CharNum);
                            }
                            CharNum = 110;
                            Found = !Found;
                        }
                        else
                        {
                            CharNum -= 1;
                        }
                    }
                }
                else
                {
                    TextTooLong = !TextTooLong;
                    Story.Add(Text);
                }
            }
            return Story;
        }

        public static void ClearStory() //Clears the story box
        {
            for (int YPos = 6; YPos < 39; YPos++)
            {
                Console.SetCursorPosition(28, YPos);
                for (int x = 0; x < 115; x++)
                {
                    Console.Write(" ");
                }
            }
        }

        #endregion

        #region Player Options Box

        public static void UpdatePlayerOptions(List<string> Options) //Updates the Players Options box
        {
            ClearPlayerOptions();
            int YPos = 41;
            int OptionNum = 1;
            bool Left = true;
            foreach(string Option in Options)
            {
                if (Left)
                {
                    Console.SetCursorPosition(28, YPos);
                    Console.Write(OptionNum + ". " + Option);
                    OptionNum += 1;
                    YPos += 1;
                    if(YPos > 47)
                    {
                        YPos = 41;
                        Left = !Left;
                    }
                }
                else
                {
                    Console.SetCursorPosition(50, YPos);
                    Console.Write(OptionNum + ". " + Option);
                    OptionNum += 1;
                    YPos += 1;
                }
            }
        }

        public static void ClearPlayerOptions() //Clears the Players Option box
        {
            for (int YPos = 41; YPos < 49; YPos++)
            {
                Console.SetCursorPosition(28, YPos);
                for (int x = 0; x < 55; x++)
                {
                    Console.Write(" ");
                }
            }
        }

        public static string OptionsReadLine()
        {
            ClearPlayerOptions();
            Console.SetCursorPosition(28, 41);
            String Input = Console.ReadLine();
            return Input;
        }

        public static void OptionsSelectItem(string Item)
        {
            ClearPlayerOptions();
            Console.SetCursorPosition(28, 41);
            Console.Write(Item);
            Console.SetCursorPosition(28, 43);
            Console.Write("1. Up");
            Console.SetCursorPosition(28, 44);
            Console.Write("2. Down");
            Console.SetCursorPosition(28, 45);
            Console.Write("3. Select Item");
            Console.SetCursorPosition(28, 46);
            Console.Write("4. Leave");
        }

        #endregion

        #region Events Box

        public static void UpdateEventBox() //Updates the Event box
        {
            ClearEventsBox();
            int YPos = 40;
            int Count = Events.EventsList.Count;
            while(Count >= 0)
            {
                Count--;
                if (Count < 0)
                    break;
                Console.SetCursorPosition(86, YPos);
                Console.Write(Events.EventsList[Count]);
                YPos += 1;
            }
        }

        public static void ClearEventsBox() //Updates the Event box
        {
            for (int YPos = 40; YPos < 49; YPos++)
            {
                Console.SetCursorPosition(86, YPos);
                for (int x = 0; x < 60; x++)
                {
                    Console.Write(" ");
                }
            }
        }

        #endregion

    }

    public static class Draw///Externally Sourced Class
    {
        /// <summary>
        /// Draws a rectangle in the console using several WriteLine() calls.
        /// </summary>
        /// <param name="width">The width of the rectangle.</param>
        /// <param name="height">The right of the rectangle.</param>
        /// <param name="xLocation">The left side position.</param>
        /// <param name="yLocation">The top position.</param>
        /// <param name="keepOriginalCursorLocation">If true, 
        /// the cursor will return back to the starting location.</param>
        /// <param name="color">The color to use. null=uses current color Default: null</param>
        /// <param name="useDoubleLines">Enables double line boarders. Default: false</param>
        public static void RectangleFromCursor(int width,
            int height,
            int xLocation = 0,
            int yLocation = 0,
            bool keepOriginalCursorLocation = false,
            ConsoleColor? color = null,
            bool useDoubleLines = false)
        {
            {
                // Save original cursor location
                int savedCursorTop = Console.CursorTop;
                int savedCursorLeft = Console.CursorLeft;

                // if the size is smaller then 1 then don't do anything
                if (width < 1 || height < 1)
                {
                    return;
                }

                // Save and then set cursor color
                ConsoleColor savedColor = Console.ForegroundColor;
                if (color.HasValue)
                {
                    Console.ForegroundColor = color.Value;
                }

                char tl, tt, tr, mm, bl, br;

                if (useDoubleLines)
                {
                    tl = '+'; tt = '-'; tr = '+'; mm = '¦'; bl = '+'; br = '+';
                }
                else
                {
                    tl = '+'; tt = '-'; tr = '+'; mm = '¦'; bl = '+'; br = '+';
                }

                for (int i = 0; i < yLocation; i++)
                {
                    Console.WriteLine();
                }

                Console.WriteLine(
                    string.Empty.PadLeft(xLocation, ' ')
                    + tl
                    + string.Empty.PadLeft(width - 1, tt)
                    + tr);

                for (int i = 0; i < height; i++)
                {
                    Console.WriteLine(
                        string.Empty.PadLeft(xLocation, ' ')
                        + mm
                        + string.Empty.PadLeft(width - 1, ' ')
                        + mm);
                }

                Console.WriteLine(
                    string.Empty.PadLeft(xLocation, ' ')
                    + bl
                    + string.Empty.PadLeft(width - 1, tt)
                    + br);


                if (color.HasValue)
                {
                    Console.ForegroundColor = savedColor;
                }

                if (keepOriginalCursorLocation)
                {
                    Console.SetCursorPosition(savedCursorLeft, savedCursorTop);
                }
            }
        }

        /// <summary>
        /// Draws a rectangle in a console window using the top line of the buffer as the offset.
        /// </summary>
        /// <param name="xLocation">The left side position.</param>
        /// <param name="yLocation">The top position.</param>
        /// <param name="width">The width of the rectangle.</param>
        /// <param name="height">The right of the rectangle.</param>
        /// <param name="color">The color to use. null=uses current color Default: null</param>
        public static void RectangleFromTop(
            int width,
            int height,
            int xLocation = 0,
            int yLocation = 0,
            ConsoleColor? color = null,
            bool useDoubleLines = false)
        {
            Rectangle(width, height, xLocation, yLocation, DrawKind.FromTop, color, useDoubleLines);
        }

        /// <summary>
        /// Specifies if the draw location should be based on the current cursor location or the
        /// top of the window.
        /// </summary>
        public enum DrawKind
        {
            BelowCursor,
            BelowCursorButKeepCursorLocation,
            FromTop,
        }

        /// <summary>
        /// Draws a rectangle in the console window.
        /// </summary>
        /// <param name="width">The width of the rectangle.</param>
        /// <param name="height">The right of the rectangle.</param>
        /// <param name="xLocation">The left side position.</param>
        /// <param name="yLocation">The top position.</param>
        /// <param name="drawKind">Where to draw the rectangle and 
        /// where to leave the cursor when finished.</param>
        /// <param name="color">The color to use. null=uses current color Default: null</param>
        /// <param name="useDoubleLines">Enables double line boarders. Default: false</param>
        public static void Rectangle(
            int width,
            int height,
            int xLocation = 0,
            int yLocation = 0,
            DrawKind drawKind = DrawKind.FromTop,
            ConsoleColor? color = null,
            bool useDoubleLines = false)
        {
            // if the size is smaller then 1 than don't do anything
            if (width < 1 || height < 1)
            {
                return;
            }

            // Save original cursor location
            int savedCursorTop = Console.CursorTop;
            int savedCursorLeft = Console.CursorLeft;

            if (drawKind == DrawKind.BelowCursor || drawKind == DrawKind.BelowCursorButKeepCursorLocation)
            {
                yLocation += Console.CursorTop;
            }

            // Save and then set cursor color
            ConsoleColor savedColor = Console.ForegroundColor;
            if (color.HasValue)
            {
                Console.ForegroundColor = color.Value;
            }

            char tl, tt, tr, mm, bl, br;

            if (useDoubleLines)
            {
                tl = '+'; tt = '-'; tr = '+'; mm = '¦'; bl = '+'; br = '+';
            }
            else
            {
                tl = '+'; tt = '-'; tr = '+'; mm = '¦'; bl = '+'; br = '+';
            }

            SafeDraw(xLocation, yLocation, tl);
            for (int x = xLocation + 1; x < xLocation + width; x++)
            {
                SafeDraw(x, yLocation, tt);
            }
            SafeDraw(xLocation + width, yLocation, tr);

            for (int y = yLocation + height; y > yLocation; y--)
            {
                SafeDraw(xLocation, y, mm);
                SafeDraw(xLocation + width, y, mm);
            }

            SafeDraw(xLocation, yLocation + height + 1, bl);
            for (int x = xLocation + 1; x < xLocation + width; x++)
            {
                SafeDraw(x, yLocation + height + 1, tt);
            }
            SafeDraw(xLocation + width, yLocation + height + 1, br);

            // Restore cursor
            if (drawKind != DrawKind.BelowCursor)
            {
                Console.SetCursorPosition(savedCursorLeft, savedCursorTop);
            }

            if (color.HasValue)
            {
                Console.ForegroundColor = savedColor;
            }
        }

        private static void SafeDraw(int xLocation, int yLocation, char ch)
        {
            if (xLocation < Console.BufferWidth && yLocation < Console.BufferHeight)
            {
                Console.SetCursorPosition(xLocation, yLocation);
                Console.Write(ch);
            }
        }
    }
}