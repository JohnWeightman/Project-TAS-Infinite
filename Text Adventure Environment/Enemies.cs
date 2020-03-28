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
        public static List<object> EnemyList = new List<object>();

        public static void LoadEnemeisFromFile(List<string> EnemyType, List<int> EnemyCount)
        {
            int EnemyTotal = EnemyCount.Sum(x => Convert.ToInt32(x));
            EnemyNPC[] EnemyNPC = new EnemyNPC[EnemyTotal];
            int Count = 0;
            for (int x = 0; x < EnemyType.Count; x++)
            {
                XmlReader XML = XmlReader.Create("XMLFiles\\Enemies.xml");
                while (XML.Read())
                {
                    if((XML.NodeType == XmlNodeType.Element) && (XML.Name == EnemyType[x]))
                    {
                        for(int EnemyNumber = 0; EnemyNumber < EnemyCount[x]; EnemyNumber++)
                        {
                            EnemyNPC[Count] = new EnemyNPC(EnemyType[x] + " " + EnemyNumber, Convert.ToInt32(XML.GetAttribute("HP")), 
                                Convert.ToInt32(XML.GetAttribute("AC")), Convert.ToInt32(XML.GetAttribute("Str")), Convert.ToInt32(XML.GetAttribute("Dex")),
                                Convert.ToInt32(XML.GetAttribute("Con")), Convert.ToInt32(XML.GetAttribute("StrMod")), 
                                Convert.ToInt32(XML.GetAttribute("DexMod")), Convert.ToInt32(XML.GetAttribute("ConMod")),
                                XML.GetAttribute("Weapon"), XML.GetAttribute("OffHand"), XML.GetAttribute("Armour"), 
                                Convert.ToInt32(XML.GetAttribute("XPValue")));
                            //EnemyNPC[Count].Name = EnemyType[x] + " " + EnemyNumber;
                            //EnemyNPC[Count].HP = Convert.ToInt32(XML.GetAttribute("HP"));
                            //EnemyNPC[Count].AC = Convert.ToInt32(XML.GetAttribute("AC"));
                            //EnemyNPC[Count].Str = Convert.ToInt32(XML.GetAttribute("Str"));
                            //EnemyNPC[Count].Dex = Convert.ToInt32(XML.GetAttribute("Dex"));
                            //EnemyNPC[Count].Con = Convert.ToInt32(XML.GetAttribute("Con"));
                            //EnemyNPC[Count].StrMod = Convert.ToInt32(XML.GetAttribute("StrMod"));
                            //EnemyNPC[Count].DexMod = Convert.ToInt32(XML.GetAttribute("DexMod"));
                            //EnemyNPC[Count].ConMod = Convert.ToInt32(XML.GetAttribute("ConMod"));
                            //EnemyNPC[Count].Weapon = XML.GetAttribute("Weapon");
                            //EnemyNPC[Count].OffHand = XML.GetAttribute("OffHand");
                            //EnemyNPC[Count].Armour = XML.GetAttribute("Armour");
                            //EnemyNPC[Count].XPValue = Convert.ToInt32(XML.GetAttribute("XPValue"));
                            Count += 1;
                        }
                    }
                }
            }
            foreach(Object Enemy in EnemyNPC)
            {
                EnemyList.Add(Enemy);
            }
        }
    }
}
//public string Name = "";
//public int HP = 0;
//public int AC = 0;
//public int Str = 0;
//public int Dex = 0;
//public int Con = 0;
//public int StrMod = 0;
//public int DexMod = 0;
//public int ConMod = 0;
//public string Weapon = "";
//public string OffHand = "";
//public string Armour = "";
//public int XPValue = 0;