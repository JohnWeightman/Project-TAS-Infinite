using System.Collections.Generic;

namespace Text_Adventure_Environment
{

    static class Stores
    {
        static Shops Store = new Shops();
        static List<string> Wares = new List<string>();
        static List<string> WaresNames = new List<string>();

        public static void StoreLoop(Shops ModShop)
        {
            Store = ModShop;
            DisplayWares();
            while (true)
            {
                int SelItem = Player.SelectItem(WaresNames);
                if (SelItem == -1)
                    return;
                else
                    ObjectType(SelItem);
            }
        }

        static void DisplayWares()
        {
            foreach(Weapon Weapon in Store.Weapons)
            {
                string WeaponData = Weapon.Name + " - DMG: " + Weapon.Damage;
                if (Weapon.TwoHanded)
                    WeaponData += ", TH";
                else if (Weapon.Versatile)
                    WeaponData += ", V";
                WeaponData += ", Cost: " + Weapon.Cost;
                Wares.Add(WeaponData);
                WaresNames.Add(Weapon.Name);
            }
            foreach(Armour Armour in Store.Armour)              
            {
                Wares.Add(Armour.Name + " - AC: " + Armour.AC + ", Weight: " + Armour.Weight + ", Cost: " + Armour.Cost);
                WaresNames.Add(Armour.Name);
            }
            foreach (Potions Potion in Store.Potions)
            {
                Wares.Add(Potion.Name + " - Regen: " + Potion.DiceNum + "D" + Potion.DiceSize + "+" + Potion.Modifier + ", Cost: " + Potion.Cost);
                WaresNames.Add(Potion.Name);
            }
            DrawGUI.UpdateStoryBox(Wares);
        }

        static void ObjectType(int SelItem)
        {
            if (SelItem < Store.Weapons.Count)
                BuyWeapon(SelItem);
            else
            {
                SelItem -= Store.Weapons.Count;
                BuyArmour(SelItem);
            }
        }

        static void BuyWeapon(int SelItem)
        {
            if (Player.Gold >= Store.Weapons[SelItem].Cost)
            {
                Events.NewEvent("BoughtWeapon", ES1: Store.Weapons[SelItem].Name, EN1: Store.Weapons[SelItem].Damage - Player.Weapon.Damage);
                Player.Gold -= Store.Weapons[SelItem].Cost;
                Player.Weapon.UpdateWeaponObject(Store.Weapons[SelItem]);
                List<string> Update = new List<string>() { "You bought a " + Player.Weapon.Name + "!", "", "Damage: " + Player.Weapon.Damage };
                if (Player.Weapon.TwoHanded)
                    Update.Add("Two Handed");
                else if (Player.Weapon.Versatile)
                    Update.Add("Versatile");
                List<string> Options = new List<string>() { "Continue" };
                DrawGUI.UpdateStoryBox(Update);
                DrawGUI.UpdatePlayerOptions(Options);
                DrawGUI.UpdatePlayersFourthStatsBox();
                DrawGUI.UpdateInventory();
                int Input = Player.PlayerInputs(Options.Count);
            }
            else
                NotEnoughGold();
        }

        static void BuyArmour(int SelItem)
        {
            if (Player.Gold >= Store.Armour[SelItem].Cost)
            {
                Events.NewEvent("BoughtArmour", ES1: Store.Armour[SelItem].Name, EN1: Store.Armour[SelItem].AC - Player.Armour.AC);
                Player.Gold -= Store.Armour[SelItem].Cost;
                Player.Armour.UpdateArmourObject(Store.Armour[SelItem]);
                Player.UpdatePlayerAC();
                List<string> Update = new List<string>() { "You bought " + Player.Armour.Name + " armour!", "", "Armour AC: " + Player.Armour.AC, "Total AC: " +
                    Player.AC, "Weight: " + Player.Armour.Weight };
                List<string> Options = new List<string>() { "Continue" };
                DrawGUI.UpdateStoryBox(Update);
                DrawGUI.UpdatePlayerOptions(Options);
                DrawGUI.UpdatePlayersFourthStatsBox();
                DrawGUI.UpdateInventory();
                int Input = Player.PlayerInputs(Options.Count);
            }
            else
                NotEnoughGold();
        }

        static void NotEnoughGold()
        {
            List<string> Update = new List<string>() { "Not Enough Gold!" };
            List<string> Options = new List<string>() { "Continue" };
            DrawGUI.UpdateStoryBox(Update);
            DrawGUI.UpdatePlayerOptions(Options);
            int Input = Player.PlayerInputs(1);
            DisplayWares();
        }
    }
}
