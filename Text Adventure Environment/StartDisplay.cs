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

        public static List<string> StartStory = new List<string>() {"Welcome to a text adventure!"};
        public static List<string> StartOptions = new List<string>() {"Option 1", "Option 2", "Option 3", "Option 4", "Option 5", "Option 6", "Option 7",
            "Option 8", "Option 9", "Option 10", "Option 11", "Option 12", "Option 13", "Option 14"};
        public static List<string> StartEvents = new List<string>() {"Event 1", "Event 2", "Event 3", "Event 4", "Event 5", "Event 6", "Event 7", "Event 8",
            "Event 9"};

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
                    if (Input2 == CamName)
                    {
                        LoadCampaign(Campaign);
                        Done = true;
                    }
                }
            }
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
            return Story;
        }

        #endregion

        #region LoadCampaignData

        static void LoadCampaign(string Campaign)
        {
            Program.Campaign.Name = Campaign.Remove(0, 3);
            XmlReader XML = XmlReader.Create("Campaigns\\" + Program.Campaign.Name + ".xml");
            int ModNum = 0;
            while (XML.Read())
            {
                if ((XML.NodeType == XmlNodeType.Element) && (XML.Name == "Module"))
                {
                    Module Mod = new Module();
                    Mod.Name = XML.GetAttribute("Name");
                    Program.Campaign.Modules.Add(Mod);
                }
                else if ((XML.NodeType == XmlNodeType.Element) && (XML.Name == "Story"))
                {
                    LoadModuleStory(XML, ModNum);
                }
                else if((XML.NodeType == XmlNodeType.Element) && (XML.Name == "OptionsList"))
                {
                    LoadModuleOptions(XML, ModNum);
                    XML.ReadToNextSibling("OptionsType");
                    LoadModuleOptionTypes(XML, ModNum);
                    ModNum += 1;
                }
            }
        }

        static void LoadModuleStory(XmlReader XML, int ModNum)
        {
            for(int x = 0; x < XML.AttributeCount; x++)
                Program.Campaign.Modules[ModNum].Story.Add(XML.GetAttribute(x));
        }

        static void LoadModuleOptions(XmlReader XML, int ModNum)
        {
            for (int x = 0; x < XML.AttributeCount; x++)
                Program.Campaign.Modules[ModNum].Options.OptionsList.Add(XML.GetAttribute(x));

        }

        static void LoadModuleOptionTypes(XmlReader XML, int ModNum)
        {
            for (int x = 0; x < XML.AttributeCount; x++)
                Program.Campaign.Modules[ModNum].Options.OptionTypes.Add(Convert.ToByte(XML.GetAttribute(x)));
        }

        #endregion

        #region Game Functions

        public static void DisplayCampaignMenu()
        {
            List<string> GameTitle = new List<string>() { "Welcome to 'Game Title'" };
            DrawGUI.UpdateStoryBox(GameTitle);
            List<string> MenuOptions = new List<string>() {"Start Game", "Quit Game"};
            DrawGUI.UpdatePlayerOptions(MenuOptions);
            int Input = Player.PlayerInputs(MenuOptions.Count);
            switch (Input)
            {
                case 1:
                    DrawGUI.ClearPlayersStatBoxes();
                    DrawGUI.ClearNPCBoxes();
                    DrawGUI.ClearEventsBox();
                    Player.StartCharacterCreator();
                    break;
                case 2:
                    System.Environment.Exit(1);
                    break;
                default:
                    System.Environment.Exit(1);
                    break;
            }
        }

        #endregion

    }
}
