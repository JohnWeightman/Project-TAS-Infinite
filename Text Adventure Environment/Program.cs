using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Text_Adventure_Environment
{
    class Program
    {
        static void Main(string[] args)
        {
            DrawGUI.DrawGUIConsole();
            StartDisplay.DisplayMainMenu();
            Encounter.StartEncounter();
            Console.ReadKey();
        }
    }
}
