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
        public string Name = "Longsword";
        public int Damage = 8;
        public bool TwoHanded = false;
        public bool Versatile = true;

        public void UpdateWeapon(string NewWeapon)
        {
            XmlReader XML = XmlReader.Create("XMLFiles\\Weapons.xml");
            while (XML.Read())
            {
                if ((XML.NodeType == XmlNodeType.Element) && (XML.Name == NewWeapon))
                {
                    Name = XML.GetAttribute("Name");
                    Damage = Convert.ToInt32(XML.GetAttribute("Damage"));
                    TwoHanded = Convert.ToBoolean(XML.GetAttribute("TwoHanded"));
                    Versatile = Convert.ToBoolean(XML.GetAttribute("Versatile"));
                }
            }
        }
    }

    class Armour
    {
        public string Name = "Chainmail";
        public string Weight = "Heavy";
        public int AC = 16;

        public void UpdateArmour(string NewArmour)
        {
            XmlReader XML = XmlReader.Create("XMLFiles\\Armour.xml");
            while (XML.Read())
            {
                if ((XML.NodeType == XmlNodeType.Element) && (XML.Name == NewArmour))
                {
                    Name = XML.GetAttribute("Name");
                    AC = Convert.ToInt32(XML.GetAttribute("AC"));
                    Weight = XML.GetAttribute("Weight");
                }
                else
                {
                    Name = "N/A";
                }
            }
        }
    }
}
