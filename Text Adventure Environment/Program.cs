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
            DrawGUI.DrawGUIConsole();
            StartDisplay.DisplayMainMenu();
            StartDisplay.DisplayCampaignMenu();
            while (true)
            {
                Encounter.StartEncounter();
            }
            Console.ReadKey();
        }
    }
}
