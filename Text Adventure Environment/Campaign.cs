using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Text_Adventure_Environment
{
    class Campaigns
    {
        public string Name;
        public bool Complete = false;
        public List<Module> Modules = new List<Module>();

        #region Module Functions

        public int StoryModule(Module Mod)
        {
            DrawGUI.UpdateStoryBox(Mod.Story);
            DrawGUI.UpdatePlayerOptions(Mod.Options.OptionsList);
            int Input = Player.PlayerInputs(Mod.Options.OptionsList.Count);
            int OptDir = Mod.Options.OptionDirections[Input - 1];
            return OptDir;
        }

        public int EncounterModule(Module Mod)
        {
            DrawGUI.UpdateStoryBox(Mod.Story);
            DrawGUI.UpdatePlayerOptions(Mod.Options.OptionsList);
            int Input = Player.PlayerInputs(Mod.Options.OptionsList.Count);
            Enemies.SetEncounterList(Mod.Encounter.EnemyTypes, Mod.Encounter.EnemyNumber, true);
            return Mod.Options.OptionDirections[0];
        }

        #endregion
    }

    class Module
    {
        public string Name;
        public string ID;
        public byte ModType;
        public Options Options = new Options();
        public Encounters Encounter = new Encounters();
        public Shops Shop = new Shops();
        public List<string> Story = new List<string>();
    }

    class Options
    {
        public List<string> OptionsList = new List<string>();
        public List<int> OptionDirections = new List<int>();
    }

    class Encounters
    {
        public List<string> EnemyTypes = new List<string>();
        public List<int> EnemyNumber = new List<int>();
    }

    class Shops
    {
        public List<Weapon> Weapons = new List<Weapon>();
        public List<Armour> Armour = new List<Armour>();

        public void AddWeaponToStock(string WeaponName, int Cost)
        {
            foreach(Weapon Weapon in Equipment.Weapons)
                if(Weapon.Name == WeaponName)
                {
                    Weapon NewWeapon = new Weapon();
                    NewWeapon = Weapon;
                    NewWeapon.Cost = Cost;
                    Weapons.Add(NewWeapon);
                    break;
                }
        }

        public void AddArmourToStock(string ArmourName, int Cost)
        {
            foreach(Armour ArmourObj in Equipment.Armour)
                if(ArmourObj.Name == ArmourName)
                {
                    Armour NewArmour = new Armour();
                    NewArmour = ArmourObj;
                    NewArmour.Cost = Cost;
                    Armour.Add(NewArmour);
                }
        }
    }
}
