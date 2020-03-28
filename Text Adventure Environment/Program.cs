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
            List<string> EnemyType = new List<string>() { "Bandit", "Thief" };
            List<int> EnemyCount = new List<int>() { 2, 3 };
            Enemies.LoadEnemeisFromFile(EnemyType, EnemyCount);
            DrawGUI.UpdateEnemiesBox();
            Console.ReadKey();
        }
    }
}
