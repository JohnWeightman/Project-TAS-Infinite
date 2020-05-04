using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Text_Adventure_Environment
{
    static class Enemies
    {
        public static List<EnemyNPC> EncounterList = new List<EnemyNPC>();
        static List<EnemyNPC> EnemyList = new List<EnemyNPC>();

        public static void LoadEnemeisFromFile(List<string> EnemyType, List<int> EnemyCount, bool StartEncounter)
        {
            int EnemyTotal = EnemyCount.Sum(x => Convert.ToInt32(x));
            EnemyNPC[] EnemyNPC = new EnemyNPC[EnemyTotal];
            int Count = 0;
            for (int x = 0; x < EnemyType.Count; x++)
            {
                XmlReader XML = XmlReader.Create("XMLFiles\\Enemies.xml");
                while (XML.Read())
                {
                    if ((XML.NodeType == XmlNodeType.Element) && (XML.Name == EnemyType[x]))
                    {
                        for (int EnemyNumber = 0; EnemyNumber < EnemyCount[x]; EnemyNumber++)
                        {
                            EnemyNPC[Count] = new EnemyNPC();
                            EnemyNPC[Count].Name = EnemyType[x] + " " + (EnemyNumber + 1);
                            EnemyNPC[Count].HP = Convert.ToInt32(XML.GetAttribute("HP"));
                            EnemyNPC[Count].AC = Convert.ToInt32(XML.GetAttribute("AC"));
                            EnemyNPC[Count].Str = Convert.ToInt32(XML.GetAttribute("Str"));
                            EnemyNPC[Count].Dex = Convert.ToInt32(XML.GetAttribute("Dex"));
                            EnemyNPC[Count].Con = Convert.ToInt32(XML.GetAttribute("Con"));
                            EnemyNPC[Count].StrMod = Convert.ToInt32(XML.GetAttribute("StrMod"));
                            EnemyNPC[Count].DexMod = Convert.ToInt32(XML.GetAttribute("DexMod"));
                            EnemyNPC[Count].ConMod = Convert.ToInt32(XML.GetAttribute("ConMod"));
                            EnemyNPC[Count].Stamina = Convert.ToInt32(XML.GetAttribute("Stamina"));
                            EnemyNPC[Count].StaminaMax = Convert.ToInt32(XML.GetAttribute("StaminaMax"));
                            EnemyNPC[Count].DifBonus = Convert.ToInt32(XML.GetAttribute("DifBonus"));
                            EnemyNPC[Count].OffHand = XML.GetAttribute("OffHand");
                            EnemyNPC[Count].XPValue = Convert.ToInt32(XML.GetAttribute("XPValue"));
                            EnemyNPC[Count].Weapon.UpdateWeapon(XML.GetAttribute("Weapon"));
                            EnemyNPC[Count].Armour.UpdateArmour(XML.GetAttribute("Armour"));
                            EnemyNPC[Count].Gold = DiceRoller.RandomRange(3 * EnemyNPC[Count].DifBonus, 5 * EnemyNPC[Count].DifBonus);
                            Count += 1;
                        }
                    }
                }
            }
            foreach (EnemyNPC Enemy in EnemyNPC)
                EncounterList.Add(Enemy);
            if (StartEncounter)
                Encounter.StartEncounter();
        }

        public static void SetEncounterList(List<string> EnemyType, List<int> EnemyCount, bool StartEncounter)
        {
            int EType = 0;
            int EnemyTotal = EnemyCount.Sum(x => Convert.ToInt32(x));
            EnemyNPC[] Temp = new EnemyNPC[EnemyTotal];
            for (int x = 0; x < EnemyTotal; x++)
            {
                foreach(EnemyNPC NPC in EnemyList)
                {
                    if (NPC.Name == EnemyType[EType])
                    {
                        for (int y = 0; y < EnemyCount[EType]; y++)
                        {
                            Temp[x] = new EnemyNPC();
                            Temp[x] = NPC;
                            x++;
                        }
                    }
                    EType++;
                }
            }
            foreach (EnemyNPC NPC in Temp)
                EncounterList.Add(NPC);
            if (StartEncounter)
                Encounter.StartEncounter();
        }

        public static void LoadEnemyDataFromFile()
        {
            XmlDocument Doc = new XmlDocument();
            Doc.Load("GameObjects.xml");
            foreach (XmlNode Node in Doc.DocumentElement)
            {
                if (Node.Name == "Enemies")
                {
                    int Count = 0;
                    int ChildCount = Convert.ToInt32(Node.Attributes[0].Value);
                    EnemyNPC[] EnmTemp = new EnemyNPC[ChildCount];
                    foreach (XmlNode Child in Node.ChildNodes)
                    {
                        EnmTemp[Count] = new EnemyNPC();
                        EnmTemp[Count].Name = Child.Name;
                        EnmTemp[Count].HP = Convert.ToInt32(Child.Attributes[0].Value);
                        EnmTemp[Count].AC = Convert.ToInt32(Child.Attributes[1].Value);
                        EnmTemp[Count].Str = Convert.ToInt32(Child.Attributes[2].Value);
                        EnmTemp[Count].StrMod = Convert.ToInt32(Child.Attributes[3].Value);
                        EnmTemp[Count].Dex = Convert.ToInt32(Child.Attributes[4].Value);
                        EnmTemp[Count].DexMod = Convert.ToInt32(Child.Attributes[5].Value);
                        EnmTemp[Count].Con = Convert.ToInt32(Child.Attributes[6].Value);
                        EnmTemp[Count].ConMod = Convert.ToInt32(Child.Attributes[7].Value);
                        EnmTemp[Count].Stamina = Convert.ToInt32(Child.Attributes[8].Value);
                        EnmTemp[Count].StaminaMax = Convert.ToInt32(Child.Attributes[9].Value);
                        EnmTemp[Count].DifBonus = Convert.ToInt32(Child.Attributes[10].Value);
                        EnmTemp[Count].Weapon.UpdateWeapon(Child.Attributes[11].Value);
                        EnmTemp[Count].OffHand = Child.Attributes[12].Value;
                        EnmTemp[Count].Armour.UpdateArmour(Child.Attributes[13].Value);
                        EnmTemp[Count].XPValue = Convert.ToInt32(Child.Attributes[14].Value);
                        Count++;
                    }
                    foreach (EnemyNPC NPC in EnmTemp)
                        EnemyList.Add(NPC);
                }
            }
        }
    }
}