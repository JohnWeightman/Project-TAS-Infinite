using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Text_Adventure_Environment
{
    static class GameObjects
    {
        public static List<Weapon> Weapons = new List<Weapon>();
        public static List<Armour> Armour = new List<Armour>();
        public static List<EnemyNPC> NPCs = new List<EnemyNPC>();

        public static void LoadGameObjects()
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
                        NPCs.Add(NPC);
                }
                else if (Node.Name == "Weapons")
                {
                    int Count = 0;
                    int ChildCount = Convert.ToInt32(Node.Attributes[0].Value);
                    Weapon[] WeapTemp = new Weapon[ChildCount];
                    foreach (XmlNode Child in Node.ChildNodes)
                    {
                        WeapTemp[Count] = new Weapon();
                        WeapTemp[Count].Name = Child.Name;
                        WeapTemp[Count].Damage = Convert.ToInt32(Child.Attributes[1].Value);
                        WeapTemp[Count].TwoHanded = Convert.ToBoolean(Child.Attributes[2].Value);
                        WeapTemp[Count].Versatile = Convert.ToBoolean(Child.Attributes[3].Value);
                        WeapTemp[Count].Cost = Convert.ToInt32(Child.Attributes[4].Value);
                        Count++;
                    }
                    foreach (Weapon Weapon in WeapTemp)
                        Weapons.Add(Weapon);
                }
                else if (Node.Name == "Armour")
                {
                    int Count = 0;
                    int ChildCount = Convert.ToInt32(Node.Attributes[0].Value);
                    Armour[] ArmTemp = new Armour[ChildCount];
                    foreach (XmlNode Child in Node.ChildNodes)
                    {
                        ArmTemp[Count] = new Armour();
                        ArmTemp[Count].Name = Child.Name;
                        ArmTemp[Count].AC = Convert.ToInt32(Child.Attributes[1].Value);
                        ArmTemp[Count].Weight = Child.Attributes[2].Value;
                        ArmTemp[Count].Cost = Convert.ToInt32(Child.Attributes[3].Value);
                        Count++;
                    }
                    foreach (Armour Arm in ArmTemp)
                        Armour.Add(Arm);
                }
            }
        }
    }

    class Weapon
    {
        public string Name = "Shortsword";
        public int Damage = 6;
        public bool TwoHanded = false;
        public bool Versatile = false;
        public int Cost = 0;

        public void UpdateWeapon(string NewWeapon)
        {
            foreach(Weapon Weapon in GameObjects.Weapons)
                if(Weapon.Name == NewWeapon)
                {
                    Name = Weapon.Name;
                    Damage = Weapon.Damage;
                    TwoHanded = Weapon.TwoHanded;
                    Versatile = Weapon.Versatile;
                    break;
                }
        }
    }

    class Armour
    {
        public string Name = "Leather";
        public string Weight = "Light";
        public int AC = 7;
        public int Cost = 0;

        public void UpdateArmour(string NewArmour)
        {
            foreach(Armour Armour in GameObjects.Armour)
                if(Armour.Name == NewArmour)
                {
                    Name = Armour.Name;
                    Weight = Armour.Weight;
                    AC = Armour.AC;
                }
        }
    }
}
