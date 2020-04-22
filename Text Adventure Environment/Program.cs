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
        static int ModNum = 0;

        static void Main(string[] args)
        {
            DrawGUI.DrawGUIConsole();
            StartDisplay.DisplayMainMenu();
            StartDisplay.DisplayCampaignMenu();
            GameLoop();
        }

        static void GameLoop()
        {
            int ModChoice = 0;
            while (!Player.Dead && !Campaign.Complete)
            {
                switch (Campaign.Modules[ModChoice].ModType)
                {
                    case 0:
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
