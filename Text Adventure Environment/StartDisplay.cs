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

        #region Program Start Information

        public static List<string> StartStory = new List<string>() { "Welcome to a text adventure!" };
        public static List<string> StartOptions = new List<string>() { "Option 1", "Option 2", "Option 3", "Option 4", "Option 5", "Option 6", "Option 7",
            "Option 8", "Option 9", "Option 10", "Option 11", "Option 12", "Option 13", "Option 14" };
        public static List<string> StartEvents = new List<string>() { "Event 1", "Event 2", "Event 3", "Event 4", "Event 5", "Event 6", "Event 7", "Event 8",
            "Event 9" };

        #endregion

        #region Campaign Selection

        public static void DisplayMainMenu()
        {
            List<string> Story = new List<string>() { "Welcome To TAS Infinite!" };
            List<string> Options = new List<string>() { "Continue" };
            DrawGUI.UpdateStoryBox(Story);
            DrawGUI.UpdatePlayerOptions(Options);
            int Input = Player.PlayerInputs(Options.Count);
            string[] CampaignFiles = Directory.GetFiles("Campaigns\\", "*.xml");
            bool Done = false;
            while (!Done)
            {
                Story.Clear();
                Story = GetCampaignFiles();
                DrawGUI.UpdateStoryBox(Story);
                string Input2 = DrawGUI.OptionsReadLine();
                foreach(string Campaign in Story)
                {
                    string CamName = Campaign.Remove(0, 3);
                    string CamNameLower = CamName.ToLower();
                    if (Input2 == CamName || Input2 == CamNameLower)
                    {
                        LoadCampaign(Campaign);
                        Done = true;
                    }
                    else if(Input2 == "Quit" || Input2 == "quit")
                    {
                        System.Environment.Exit(1);
                    }
                }
            }
            DisplayCampaignMenu();
        }

        static List<string> GetCampaignFiles()
        {
            List<string> Story = new List<string>();
            string[] CampaignFiles = Directory.GetFiles("Campaigns\\", "*.xml");
            for (int x = 0; x < CampaignFiles.Length; x++)
            {
                CampaignFiles[x] = CampaignFiles[x].Substring(10);
                int StringLength = CampaignFiles[x].Length - 4;
                CampaignFiles[x] = (x + 1) + ". " + CampaignFiles[x].Remove(StringLength, 4);
                Story.Add(CampaignFiles[x]);
            }
            Story.Add("Quit");
            return Story;
        }

        #endregion

        #region LoadCampaignData

        static void LoadCampaign(string Campaign)
        {
            Program.Campaign.Name = Campaign.Remove(0, 3);
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
            foreach (XmlNode Type in Encounter)
                if (Type.Name == "EncounterEnemyType")
                    for (int x = 0; x < Type.Attributes.Count; x++)
                        Program.Campaign.Modules[ModNum].Encounter.EnemyTypes.Add(Type.Attributes[x].Value);
                else if (Type.Name == "EncounterEnemyNumber")
                    for (int x = 0; x < Type.Attributes.Count; x++)
                        Program.Campaign.Modules[ModNum].Encounter.EnemyNumber.Add(Convert.ToInt32(Type.Attributes[x].Value));
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
