using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Text_Adventure_Environment
{
    class Program
    {
        static void Main(string[] args)
        {
            DrawGUI.DrawGUIConsole();
            StartDisplay.DisplayMainMenu();
            StartDisplay.DisplayCampaignMenu();
            Encounter.StartEncounter();
            Console.ReadKey();
        }
    }
}
