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
            while (true)
            {
                Encounter.StartEncounter();
            }
        }

        static void GameLoop()
        {
            while (!Player.Dead && !Campaign.Complete)
            {
                DrawGUI.UpdateStoryBox(Campaign.Modules[ModNum].Story);
                DrawGUI.UpdatePlayerOptions(Campaign.Modules[ModNum].Options.OptionsList);
                int Input = Player.PlayerInputs(Campaign.Modules[ModNum].Options.OptionsList.Count);
            }
            StartDisplay.DisplayCampaignMenu();
        }
    }
}
