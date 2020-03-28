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
                            EnemyNPC[Count].Weapon = XML.GetAttribute("Weapon");
                            EnemyNPC[Count].OffHand = XML.GetAttribute("OffHand");
                            EnemyNPC[Count].Armour = XML.GetAttribute("Armour");
                            EnemyNPC[Count].XPValue = Convert.ToInt32(XML.GetAttribute("XPValue"));
                            Count += 1;
                        }
                    }
                }
            }
            foreach(object Enemy in EnemyNPC)
            {
                EnemyList.Add(Enemy);
            }
        }
    }
}