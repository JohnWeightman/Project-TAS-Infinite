using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Text_Adventure_Environment
{
    class Weapon
    {
        public string Name = "Shortsword";
        public int Damage = 6;
        public bool TwoHanded = false;
        public bool Versatile = false;

        public void UpdateWeapon(string NewWeapon)
        {
            XmlDocument Doc = new XmlDocument();
            Doc.Load("GameObjects.xml");
            foreach (XmlNode Node in Doc.DocumentElement)
            {
                if (Node.Name == "Weapons")
                {
                    foreach (XmlNode Child in Node.ChildNodes)
                    {
                        if(Child.Name == NewWeapon)
                        {
                            Name = Child.Attributes[0].Value;
                            Damage = Convert.ToInt32(Child.Attributes[1].Value);
                            TwoHanded = Convert.ToBoolean(Child.Attributes[2].Value);
                            Versatile = Convert.ToBoolean(Child.Attributes[3].Value);
                        }
                    }
                }
            }
        }
    }

    class Armour
    {
        public string Name = "Leather";
        public string Weight = "Light";
        public int AC = 7;

        public void UpdateArmour(string NewArmour)
        {
            XmlDocument Doc = new XmlDocument();
            Doc.Load("GameObjects.xml");
            foreach (XmlNode Node in Doc.DocumentElement)
            {
                if (Node.Name == "Armour")
                {
                    foreach (XmlNode Child in Node.ChildNodes)
                    {
                        if (Child.Name == NewArmour)
                        {
                            Name = Child.Attributes[0].Value;
                            AC = Convert.ToInt32(Child.Attributes[1].Value);
                            Weight = Child.Attributes[2].Value;
                        }
                    }
                }
            }
        }
    }
}
