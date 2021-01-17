using Debugger;

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
            DrawGUI.DrawGUIConsole();
        }

        public static void GameLoop()
        {
            int ModChoice = 0;
            while (!Player.Dead && !Campaign.Complete)
            {
                try
                {
                    switch (Campaign.Modules[ModChoice].ModType)
                    {
                        case 0:
                            ModChoice = Campaign.StoryModule(Campaign.Modules[ModChoice]);
                            break;
                        case 1:
                            ModChoice = Campaign.EncounterModule(Campaign.Modules[ModChoice]);
                            break;
                        case 2:
                            ModChoice = Campaign.ShopModule(Campaign.Modules[ModChoice]);
                            break;
                        case 3:
                            ModChoice = Campaign.TrapModule(Campaign.Modules[ModChoice]);
                            break;
                        case 4:
                            ModChoice = Campaign.EndCampaignModule(Campaign.Modules[ModChoice]);
                            break;
                        default:
                            break;
                    }
                }
                catch
                {
                    Debug.Log("Program/GameLoop() - Error With Module " + Campaign.Modules[ModChoice].Name);
                }
            }
            StartDisplay.DisplayCampaignMenu();
        }
    }
}
