using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;

namespace Text_Adventure_Environment
{
    static class StartDisplay
    {

        #region About Information

        static List<string> AboutTAS = new List<string>() { "TAS Infinite", "", "TAS Infinite was developed by John Weightman, a hobbyist game developer who " +
            "built this project initially to pass the time during the Coronavirus lockdown in 2020. I continued to work on the project afterwards, however, " +
            "using it as my main ongoing project and also completed '#100DaysOfCode' working on this project.", "", "The project originally started with just " +
            "the GUI class that I built and played around one evening. I continued to add to it till I decided to build a shell of a game for amatuer " +
            "developers to take and build a game from. This evolved into the game before you, a community based text adventure game, with a in built tool to " +
            "encourage people to build and share their own adventures for people to play.", "", "Much of the game is built to try and give players " +
            "opportunities to change and play around with the game files, as well as for me to learn new things in C#, the language the game is written in. " +
            "I encourage people to play around with things, and the project code is also avaliable on Github if any developers or hobbyists want to have a " +
            "play around! Github link below:", "", "https://github.com/JohnWeightman/Project-TAS-Infinite", "", "Also follow me on Twitter for updates and " +
            "other coding and D&D tweets!", "", "@JohnTheScout" };
        static List<string> AboutDev = new List<string>() { "John Weightman is a 23 year old hobbyist game developer with a love of Computer Science and " +
            "Tech. After 4 years at the University of Lincoln, I've grown to love many things besides Computer science. I was president of the HEMA society " +
            "2018-19, part of the Lincoln Royals dodgeball team, an avid scout leader and many other things besides.", "", "I have spent 4 years doing HEMA " +
            "(Historical European Martial Arts) learning different masters from different weapons though I'm the most proficient with a longsword. I spent 3 " +
            "years as an explorer leader at Viking explorers in Lincoln and also joined the Three Arrows Scout Survival Team. I'm also a regular D&D player, " +
            "both as a player and as a DM in D&D 5e. Currently part of 2 campaigns, the first I DM and the second I play 'Thalmar Rotdal', a drunk dwarven " +
            "fool of a fighter.", "", "I've also become a big supporter of all things game jam related. Ive taken part in 4 Global Game Jams and several " +
            "others besides. Normally part of a team called LowPoly Games, you can find most our jam games on our Itch.IO profile linked below. I've also " +
            "produced a few videos offering tips for new jammers and promoting a few of my favourate games that I've seen other jammers make.", "","Links:",
            "Twitter: @JohnTheScout", "Itch: https://istalriskolirproductions.itch.io/", "Github: https://github.com/JohnWeightman", "", "LowPoly Games " +
            "Itch: https://lowpolygames.itch.io/" };
       
        #endregion

        #region Campaign Selection

        public static void DisplayMainMenu()
        {
            List<string> Story = new List<string>() { "Welcome To TAS Infinite!" };
            List<string> Options = new List<string>() { "Campaign Selection", "About TAS Infinite", "About Developer", "Quit" };
            DrawGUI.UpdateStoryBox(Story);
            DrawGUI.UpdatePlayerOptions(Options);
            int Input = Player.PlayerInputs(Options.Count);
            switch (Input)
            {
                case 1:
                    SelectCampaign();
                    break;
                case 2:
                    DisplayAboutTASInfinite();
                    DisplayMainMenu();
                    break;
                case 3:
                    DisplayAboutDeveloper();
                    DisplayMainMenu();
                    break;
                case 4:
                    Environment.Exit(1);
                    break;
                default:
                    Environment.Exit(1);
                    break;
            }
        }

        public static void SelectCampaign()
        {
            string[] CampaignFiles = Directory.GetFiles("Campaigns\\", "*.xml");
            List<string> Campaigns = GetCampaignFiles();
            DrawGUI.UpdateStoryBox(Campaigns);
            Campaigns.RemoveAt(0);
            Campaigns.RemoveAt(0);
            int Input = Player.SelectItem(Campaigns);
            if (Input == -1)
                DisplayMainMenu();
            else
            {
                string CampSel = Campaigns[Input].ToLower();
                LoadCampaign(CampSel);
                DisplayCampaignMenu();
            }
        }

        static List<string> GetCampaignFiles()
        {
            List<string> Story = new List<string>() { "Campaigns", "" };
            string[] CampaignFiles = Directory.GetFiles("Campaigns\\", "*.xml");
            for (int x = 0; x < CampaignFiles.Length; x++)
            {
                CampaignFiles[x] = CampaignFiles[x].Substring(10);
                int StringLength = CampaignFiles[x].Length - 4;
                CampaignFiles[x] = CampaignFiles[x].Remove(StringLength, 4);
                Story.Add(CampaignFiles[x]);
            }
            return Story;
        }

        static void DisplayAboutTASInfinite()
        {
            DrawGUI.UpdateStoryBox(AboutTAS);
            List<string> Options = new List<string>() { "Continue" };
            DrawGUI.UpdatePlayerOptions(Options);
            int Input = Player.PlayerInputs(Options.Count);
        }

        static void DisplayAboutDeveloper()
        {
            DrawGUI.UpdateStoryBox(AboutDev);
            List<string> Options = new List<string>() { "Continue" };
            DrawGUI.UpdatePlayerOptions(Options);
            int Input = Player.PlayerInputs(Options.Count);
        }

        #endregion

        #region LoadCampaignData

        static void LoadCampaign(string Campaign)
        {
            Program.Campaign.Name = Campaign;
            XmlDocument Doc = new XmlDocument();
            Doc.Load("Campaigns\\" + Program.Campaign.Name + ".xml");
            int ModNum = 0;
            foreach (XmlNode Node in Doc.DocumentElement)
            {
                if(Node.Name == "Module")
                {
                    Module Mod = new Module();
                    Mod.Name = Node.Attributes[0].Value;
                    Mod.ModType = Convert.ToByte(Node.Attributes[1].Value);
                    Mod.ID = Node.Attributes[2].Value;
                    if (Mod.ModType == 3)
                        Mod.EndCampaign = true;
                    Program.Campaign.Modules.Add(Mod);
                    foreach (XmlNode ModChild in Node.ChildNodes)
                    {
                        switch (ModChild.Name)
                        {
                            case "Story":
                                LoadModuleStory(ModChild, ModNum);
                                break;
                            case "Options":
                                LoadModuleOptions(ModChild, ModNum);
                                break;
                            case "Encounter":
                                LoadModuleEncounter(ModChild, ModNum);
                                break;
                            case "Shop":
                                LoadModuleShop(ModChild, ModNum);
                                break;
                            case "Trap":
                                LoadModuleTrap(ModChild, ModNum);
                                break;
                            default:
                                break;
                        }
                    }
                    ModNum++;
                }
            }
        }

        static void LoadModuleStory(XmlNode Story, int ModNum)
        {
            for (int x = 0; x < Story.Attributes.Count; x++)
                Program.Campaign.Modules[ModNum].Story.Add(Story.Attributes[x].Value);
        }

        static void LoadModuleOptions(XmlNode Options, int ModNum)
        {
            foreach(XmlNode Type in Options)
                if (Type.Name == "OptionsList")
                    for (int x = 0; x < Type.Attributes.Count; x++)
                        Program.Campaign.Modules[ModNum].Options.OptionsList.Add(Type.Attributes[x].Value);
                else if (Type.Name == "OptionDirections")
                    for (int x = 0; x < Type.Attributes.Count; x++)
                        Program.Campaign.Modules[ModNum].Options.OptionDirections.Add(Convert.ToInt32(Type.Attributes[x].Value));
        }

        static void LoadModuleEncounter(XmlNode Encounter, int ModNum)
        {
            List<string> Names = new List<string>();
            List<Tuple<string, int>> EncounterData = new List<Tuple<string, int>>();
            foreach (XmlNode Type in Encounter)
                if (Type.Name == "EncounterEnemyType")
                    for (int x = 0; x < Type.Attributes.Count; x++)
                        Names.Add(Type.Attributes[x].Value);
                else if (Type.Name == "EncounterEnemyNumber")
                    for (int x = 0; x < Type.Attributes.Count; x++)
                        EncounterData.Add(new Tuple<string, int>(Names[x], Convert.ToInt32(Type.Attributes[x].Value)));
            Program.Campaign.Modules[ModNum].Encounter.SetEncounter(EncounterData);
        }

        static void LoadModuleShop(XmlNode Shop, int ModNum)
        {
            foreach (XmlNode Stock in Shop)
                if (Stock.Name == "WeaponStock")
                    foreach (XmlNode Weapon in Stock)
                        Program.Campaign.Modules[ModNum].Shop.AddWeaponToStock(Weapon.Name, Convert.ToInt32(Weapon.Attributes[0].Value));
                else if (Stock.Name == "ArmourStock")
                    foreach (XmlNode Armour in Stock)
                        Program.Campaign.Modules[ModNum].Shop.AddArmourToStock(Armour.Name, Convert.ToInt32(Armour.Attributes[0].Value));
                else if (Stock.Name == "PotionStock")
                    foreach (XmlNode Potion in Stock)
                        Program.Campaign.Modules[ModNum].Shop.AddPotionsToStock(Potion.Attributes[0].Value, Convert.ToInt32(Potion.Attributes[1].Value));
        }

        static void LoadModuleTrap(XmlNode Trap, int ModNum)
        {
            Program.Campaign.Modules[ModNum].Trap.Name = Trap.Attributes[0].Value;
            Program.Campaign.Modules[ModNum].Trap.DiceNum = Convert.ToInt32(Trap.Attributes[1].Value);
            Program.Campaign.Modules[ModNum].Trap.DiceSize = Convert.ToInt32(Trap.Attributes[2].Value);
            Program.Campaign.Modules[ModNum].Trap.Modifier = Convert.ToInt32(Trap.Attributes[3].Value);
            Program.Campaign.Modules[ModNum].Trap.SaveType = Trap.Attributes[4].Value;
            Program.Campaign.Modules[ModNum].Trap.SaveTarget = Convert.ToInt32(Trap.Attributes[5].Value);
            Program.Campaign.Modules[ModNum].Trap.SaveSuccess.Add(Trap.Attributes[6].Value);
            Program.Campaign.Modules[ModNum].Trap.SaveFail.Add(Trap.Attributes[7].Value);
        }

        #endregion

        #region Game Functions

        public static void DisplayCampaignMenu()
        {
            List<string> GameTitle = new List<string>() { "Welcome to 'Game Title'" };
            DrawGUI.UpdateStoryBox(GameTitle);
            List<string> MenuOptions = new List<string>() { "Start Game", "Quit Game" };
            DrawGUI.UpdatePlayerOptions(MenuOptions);
            int Input = Player.PlayerInputs(MenuOptions.Count);
            switch (Input)
            {
                case 1:
                    DrawGUI.ClearPlayersStatBoxes();
                    DrawGUI.ClearNPCBoxes();
                    DrawGUI.ClearEventsBox();
                    Player.StartCharacterCreator();
                    Program.GameLoop();
                    break;
                case 2:
                    DisplayMainMenu();
                    break;
                default:
                    System.Environment.Exit(1);
                    break;
            }
        }

        #endregion

    }
}
