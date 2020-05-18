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
            Encounter.StartEncounter(Mod.Encounter.EncounterData);
            return Mod.Options.OptionDirections[0];
        }

        public int ShopModule(Module Mod)
        {
            DrawGUI.UpdateStoryBox(Mod.Story);
            DrawGUI.UpdatePlayerOptions(Mod.Options.OptionsList);
            int Input = Player.PlayerInputs(Mod.Options.OptionsList.Count);
            if (Input == 1)
                Stores.StoreLoop(Mod.Shop);
            return Mod.Options.OptionDirections[0];
        }

        public int TrapModule(Module Mod)
        {
            Mod.Trap.Trapped();
            return Mod.Options.OptionDirections[0];
        }

        public int EndCampaignModule(Module Mod)
        {
            DrawGUI.UpdateStoryBox(Mod.Story);
            DrawGUI.UpdatePlayerOptions(Mod.Options.OptionsList);
            int Input = Player.PlayerInputs(Mod.Options.OptionsList.Count);
            Complete = true;
            return 0;
        }

        #endregion
    }

    class Module
    {
        public string Name;
        public string ID;
        public byte ModType;
        public bool EndCampaign = false;
        public Options Options = new Options();
        public Encounters Encounter = new Encounters();
        public Shops Shop = new Shops();
        public Traps Trap = new Traps();
        public List<string> Story = new List<string>();
    }

    class Options
    {
        public List<string> OptionsList = new List<string>();
        public List<int> OptionDirections = new List<int>();
    }

    class Encounters
    {
        public List<EnemyNPC> EncounterData = new List<EnemyNPC>();

        public void SetEncounter(List<Tuple<string, int>> NPCs)
        {
            int NPCTotal = 0;
            for (int x = 0; x < NPCs.Count; x++)
                NPCTotal += NPCs[x].Item2;
            int Count = 0;
            EnemyNPC[] Temp = new EnemyNPC[NPCTotal];
            for (int NPCType = 0; NPCType < NPCs.Count; NPCType++)
            {
                foreach (EnemyNPC NPC in GameObjects.NPCs)
                {
                    if (NPC.Name == NPCs[NPCType].Item1)
                    {
                        for (int NPCCount = 0; NPCCount < NPCs[NPCType].Item2; NPCCount++)
                        {
                            Temp[Count] = new EnemyNPC();
                            Temp[Count] = NPC;
                            Temp[Count].Name +=  " " + (NPCCount + 1);
                            Temp[Count].Gold = DiceRoller.RandomRange(2 * Temp[Count].DifBonus, 5 * Temp[Count].DifBonus);
                            Count++;
                        }
                    }
                }
            }
            foreach (EnemyNPC NPC in Temp)
                EncounterData.Add(NPC);
        }
    }

    class Shops
    {
        public List<Weapon> Weapons = new List<Weapon>();
        public List<Armour> Armour = new List<Armour>();
        public List<Potions> Potions = new List<Potions>();

        public void AddWeaponToStock(string WeaponName, int Cost)
        {
            foreach(Weapon Weapon in GameObjects.Weapons)
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
            foreach(Armour ArmourObj in GameObjects.Armour)
                if(ArmourObj.Name == ArmourName)
                {
                    Armour NewArmour = new Armour();
                    NewArmour = ArmourObj;
                    NewArmour.Cost = Cost;
                    Armour.Add(NewArmour);
                    break;
                }
        }

        public void AddPotionsToStock(string PotionName, int Cost)
        {
            foreach(Potions Potion in GameObjects.Potions)
                if(Potion.Name == PotionName)
                {
                    Potions NewPotion = new Potions();
                    NewPotion = Potion;
                    NewPotion.Cost = Cost;
                    Potions.Add(NewPotion);
                    break;
                }
        }
    }

    class Traps
    {
        public string Name = "";
        public int DiceNum = 0;
        public int DiceSize = 0;
        public int Modifier = 0;
        public string SaveType = "";
        public int SaveTarget = 0;
        public List<string> SaveSuccess = new List<string>();
        public List<string> SaveFail = new List<string>();

        public void Trapped()
        {
            int Save = Player.SavingThrow(SaveType);
            if (Save >= SaveTarget)
            {
                //int XP = (DiceNum + DiceSize + Modifier);
                DrawGUI.UpdateStoryBox(SaveSuccess);
            }
            else
            {
                int Damage = CalcDamage();
            }
                
        }

        int CalcDamage()
        {
            int Damage = Modifier;
            for (int x = 0; x < DiceNum; x++)
                Damage += DiceRoller.RollDice(DiceSize);
            return Damage;
        }
    }
}
