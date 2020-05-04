using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Text_Adventure_Environment
{
    static class Equipment
    {
        public static List<Weapon> Weapons = new List<Weapon>();
        public static List<Armour> Armour = new List<Armour>();

        public static void LoadEquipment()
        {
            XmlDocument Doc = new XmlDocument();
            Doc.Load("GameObjects.xml");
            foreach (XmlNode Node in Doc.DocumentElement)
            {
                if (Node.Name == "Weapons")
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
            foreach(Weapon Weapon in Equipment.Weapons)
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
            foreach(Armour Armour in Equipment.Armour)
                if(Armour.Name == NewArmour)
                {
                    Name = Armour.Name;
                    Weight = Armour.Weight;
                    AC = Armour.AC;
                }
        }
    }
}
