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

        public static void SetEncounterList(List<string> EnemyType, List<int> EnemyCount, bool StartEncounter)
        {
            int NPCTotal = EnemyCount.Sum(x => Convert.ToInt32(x));
            int Count = 0;
            EnemyNPC[] Temp = new EnemyNPC[NPCTotal];           
            for(int NPCType = 0; NPCType < EnemyType.Count; NPCType++)
            {
                foreach(EnemyNPC NPC in EnemyList)
                {
                    if(NPC.Name == EnemyType[NPCType])
                    {
                        for(int NPCCount = 0; NPCCount < EnemyCount[NPCType]; NPCCount++)
                        {
                            Temp[Count] = new EnemyNPC();
                            Temp[Count] = NPC;
                            Count++;
                        }
                    }
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