﻿using System;
using System.Collections.Generic;
using System.Xml;
using Debugger;

namespace Text_Adventure_Environment
{
    static class GameObjects
    {
        public static List<EnemyNPC> NPCs = new List<EnemyNPC>();
        public static List<Weapon> Weapons = new List<Weapon>();
        public static List<Armour> Armour = new List<Armour>();
        public static List<Potions> Potions = new List<Potions>();

        public static void LoadGameObjects()
        {
            XmlDocument Doc = new XmlDocument();
            Doc.Load("GameObjects.xml");
            foreach (XmlNode Node in Doc.DocumentElement)
            {
                try
                {
                    LoadNodeByType(Node);
                }
                catch
                {
                    Debug.Log("GameObjects/LoadGameObjects() - Error Loading " + Node.Name + " Object", 4);
                }
            }

            void LoadNodeByType(XmlNode Node)
            {
                switch (Node.Name)
                {
                    case "Enemies":
                        LoadEnemiesNode(Node);
                        break;
                    case "Weapons":
                        LoadWeaponsNode(Node);
                        break;
                    case "Armour":
                        LoadArmourNode(Node);
                        break;
                    case "Potions":
                        LoadPotionsNode(Node);
                        break;
                }
            }

            void LoadEnemiesNode(XmlNode Node)
            {
                int Count = 0;
                int ChildCount = Convert.ToInt32(Node.Attributes[0].Value);
                EnemyNPC[] EnmTemp = new EnemyNPC[ChildCount];
                foreach (XmlNode Child in Node.ChildNodes)
                {
                    EnmTemp[Count] = new EnemyNPC();
                    EnmTemp[Count].Name = Child.Name;
                    EnmTemp[Count].HP = Convert.ToInt32(Child.Attributes[0].Value);
                    EnmTemp[Count].MaxHP = EnmTemp[Count].HP;
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
                    EnmTemp[Count].Weapon.UpdateWeaponString(Child.Attributes[11].Value);
                    EnmTemp[Count].OffHand = Child.Attributes[12].Value;
                    EnmTemp[Count].Armour.UpdateArmourString(Child.Attributes[13].Value);
                    EnmTemp[Count].XPValue = Convert.ToInt32(Child.Attributes[14].Value);
                    Count++;
                }
                foreach (EnemyNPC NPC in EnmTemp)
                    NPCs.Add(NPC);
            }

            void LoadWeaponsNode(XmlNode Node)
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

            void LoadArmourNode(XmlNode Node)
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

            void LoadPotionsNode(XmlNode Node)
            {
                int Count = 0;
                int ChildCount = Convert.ToInt32(Node.Attributes[0].Value);
                Potions[] PotTemp = new Potions[ChildCount];
                foreach (XmlNode Child in Node.ChildNodes)
                {
                    PotTemp[Count] = new Potions();
                    PotTemp[Count].Name = Child.Attributes[0].Value;
                    PotTemp[Count].DiceNum = Convert.ToInt32(Child.Attributes[1].Value);
                    PotTemp[Count].DiceSize = Convert.ToInt32(Child.Attributes[2].Value);
                    PotTemp[Count].Modifier = Convert.ToInt32(Child.Attributes[3].Value);
                }
                foreach (Potions Potion in PotTemp)
                    Potions.Add(Potion);
            }
        }
    }

    class EnemyNPC
    {

        #region Enemy Data

        public string Name = "";
        public int MaxHP = 0;
        public int HP = 0;
        public int AC = 0;
        public int Str = 0;
        public int Dex = 0;
        public int Con = 0;
        public int StrMod = 0;
        public int DexMod = 0;
        public int ConMod = 0;
        public string OffHand = "";
        public int XPValue = 0;
        public int Initiative = 0;
        public int Stamina = 0;
        public int StaminaMax = 0;
        public int DifBonus = 0;
        public int Gold = 0;
        public Weapon Weapon = new Weapon();
        public Armour Armour = new Armour();
        public ConsoleColor Colour = ConsoleColor.Green;

        #endregion

        public bool TakeDamage(int Damage)
        {
            bool Dead = false;
            HP -= Damage;
            if (HP <= 0)
            {
                Encounter.EncounterXP += XPValue;
                Encounter.EncounterGold += Gold;
                Dead = true;
            }
            else
                SetColour();
            return Dead;
        }

        void SetColour()
        {
            EnemySettings CC = Program.Campaign.Settings.Enemies;
            float Health = ((float)HP / (float)MaxHP) * 100;
            if (Health <= 100 && Health >= CC.EnemyNamePlateColourGreen)
                Colour = ConsoleColor.Green;
            else if (CC.EnemyNamePlateColourGreen < 100 && Health >= CC.EnemyNamePlateColourDarkGreen)
                Colour = ConsoleColor.DarkGreen;
            else if (Health < CC.EnemyNamePlateColourDarkGreen && Health >= CC.EnemyNamePlateColourDarkYellow)
                Colour = ConsoleColor.DarkYellow;
            else if (Health < CC.EnemyNamePlateColourDarkYellow && Health >= CC.EnemyNamePlateColourRed)
                Colour = ConsoleColor.Red;
            else
                Colour = ConsoleColor.DarkRed;
        }

        #region Combat Decision

        public byte CombatDecision()
        {
            byte Decision = 0;
            if (Stamina >= Player.FightOptionCosts[1])
            {
                if (Stamina / Player.FightOptionCosts[0] >= 2)
                    Decision = 0;
                else if (Stamina < (Player.FightOptionCosts[1] * 2) && Stamina >= Player.FightOptionCosts[0])
                    Decision = 0;
                else
                    Decision = 1;
            }
            else
                Decision = 2;
            return Decision;
        }

        #endregion
    }

    class Weapon
    {
        public string Name = "Shortsword";
        public int Damage = 6;
        public bool TwoHanded = false;
        public bool Versatile = false;
        public int Cost = 0;

        public void UpdateWeaponString(string NewWeapon)
        {
            foreach(Weapon Weapon in GameObjects.Weapons)
                if(Weapon.Name == NewWeapon)
                {
                    Name = Weapon.Name;
                    Damage = Weapon.Damage;
                    TwoHanded = Weapon.TwoHanded;
                    Versatile = Weapon.Versatile;
                    Cost = Weapon.Cost;
                    break;
                }
        }

        public void UpdateWeaponObject(Weapon NewWeapon)
        {
            Name = NewWeapon.Name;
            Damage = NewWeapon.Damage;
            TwoHanded = NewWeapon.TwoHanded;
            Versatile = NewWeapon.Versatile;
            Cost = NewWeapon.Cost;
        }
    }

    class Armour
    {
        public string Name = "Leather";
        public string Weight = "Light";
        public int AC = 7;
        public int Cost = 0;

        public void UpdateArmourString(string NewArmour)
        {
            foreach(Armour Armour in GameObjects.Armour)
                if(Armour.Name == NewArmour)
                {
                    Name = Armour.Name;
                    Weight = Armour.Weight;
                    AC = Armour.AC;
                    Cost = Armour.Cost;
                }
        }

        public void UpdateArmourObject(Armour NewArmour)
        {
            Name = NewArmour.Name;
            AC = NewArmour.AC;
            Weight = NewArmour.Weight;
            Cost = NewArmour.Cost;
        }
    }

    class Potions
    {
        public string Name = "Weak Health Potion";
        public int DiceNum = 2;
        public int DiceSize = 4;
        public int Modifier = 2;
        public int Cost = 0;

        public int HealthRegen()
        {
            int Regen = 0;
            for (int x = 0; x < DiceNum; x++)
                Regen += DiceRoller.RollDice(DiceSize);
            Regen += Modifier;
            return Regen;
        }
    }

    static class DiceRoller
    {
        private static Random Ran = new Random();

        public static int RollDice(int max)
        {
            return Ran.Next(1, (max + 1));
        }

        public static int RandomRange(int Min, int Max)
        {
            return Ran.Next(Min, (Max + 1));
        }
    }
}
