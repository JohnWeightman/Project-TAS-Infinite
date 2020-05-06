using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Text_Adventure_Environment
{
    static class Program
    {
        public static Campaigns Campaign = new Campaigns();

        static void Main(string[] args)
        {
            PrepareGameEnvironment();
            StartDisplay.DisplayMainMenu();
        }

        static void PrepareGameEnvironment()
        {
            GameObjects.LoadGameObjects();
            //Enemies.LoadEnemyDataFromFile();
            DrawGUI.DrawGUIConsole();
        }

        public static void GameLoop()
        {
            int ModChoice = 0;
            while (!Player.Dead && !Campaign.Complete)
            {
                switch (Campaign.Modules[ModChoice].ModType)
                {
                    case 0:
                    case 2:
                        ModChoice = Campaign.StoryModule(Campaign.Modules[ModChoice]);
                        break;
                    case 1:
                        ModChoice = Campaign.EncounterModule(Campaign.Modules[ModChoice]);
                        break;
                    default:
                        break;
                }
            }
            StartDisplay.DisplayCampaignMenu();
        }
    }
}
