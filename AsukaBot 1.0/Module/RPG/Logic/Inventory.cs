using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AsukaBot_1._0.Module.RPG.Logic.Items;

namespace AsukaBot_1._0.Module.RPG.Logic
{
    class Inventory
    {
        private Random RNG = new Random();
        public static List<BaseItem> AllItems;
        List<BaseItem> TheInventory = new List<BaseItem>();
        private int Gold = 0;

        public void Setup()
        {
            if (AllItems == null)
            {
                AllItems = new List<BaseItem>();

                #region ItemDefinging
                #region Crafting

                AllItems.Add(new CraftingItem("Leather", "it's some skin from an animal which can be used for armor", 2, true, Rarity.common, new List<ItemType>() { ItemType.Leather }));
                AllItems.Add(new CraftingItem("Iron", "a 1lb of crude iron, ready to be melted into a weapon or armor", 5, true, Rarity.common, new List<ItemType>() { ItemType.Iron, ItemType.Metal }));
                AllItems.Add(new CraftingItem("Regular wood", "a solid stick ready to be used as a handel for a weapon or staff", 1, true, Rarity.common, new List<ItemType>() { ItemType.Wood }));
                AllItems.Add(new CraftingItem("Copper", "", 3, true, Rarity.common, new List<ItemType>() { ItemType.Metal }));
                AllItems.Add(new CraftingItem("Dragon scale", "", 1000, false, Rarity.legendary, new List<ItemType>() { ItemType.Dragon }));
                AllItems.Add(new CraftingItem("Demon dog pelt", "", 230, true, Rarity.rare, new List<ItemType>() { ItemType.Demon }));
                AllItems.Add(new CraftingItem("Dragon tooth", "", 3000, false, Rarity.legendary, new List<ItemType>() { ItemType.Dragon }));
                AllItems.Add(new CraftingItem("Steel", "", 8, true, Rarity.uncommon, new List<ItemType>() { ItemType.Steel, ItemType.Metal }));

                #endregion

                #region Weapon

                AllItems.Add(new WeaponsItem("Wooden Sword", "A wooden sword! It might look harmless, but a slap from this baby does sting quite a bit!", 1, PhyDamgeType.Blunt, 1, true, 0, false, true, new List<ItemType>() { ItemType.Wood, ItemType.Weapon, ItemType.Blunt }, new List<CraftingItemInItem>() { new CraftingItemInItem((CraftingItem)AllItems[GetItemByName("Regular wood")], 4) }, Rarity.common));
                AllItems.Add(new WeaponsItem("Dragon tooth spear", "A mighty spear made of a dragon tooth strapped onto some elderwood", 1, PhyDamgeType.Punture, 1, false, 0, false, true, new List<ItemType>() { ItemType.Dragon, ItemType.Punture, ItemType.Weapon }, new List<CraftingItemInItem>() { new CraftingItemInItem((CraftingItem)AllItems[GetItemByName("Dragon tooth")], 2), new CraftingItemInItem((CraftingItem)AllItems[GetItemByName("Regular wood")], 4) }, Rarity.legendary));
                AllItems.Add(new WeaponsItem("God slayers hammer", "a hammer made for slaying even gods", 250, PhyDamgeType.Blunt, 1000000, false, 0, false, false, new List<ItemType>() { ItemType.Blunt, ItemType.Weapon }, new List<CraftingItemInItem>(), Rarity.mythicc));
                #region Iron Tier
                AllItems.Add(new WeaponsItem("Iron Spear", "It's a sturdy iron spear with a long reach, you gain reach buff (+5% dodge)", 1, PhyDamgeType.Punture, 1, true, 0, false, true, new List<ItemType>() { ItemType.Iron, ItemType.Weapon, ItemType.Punture }, new List<CraftingItemInItem>() { new CraftingItemInItem((CraftingItem)AllItems[GetItemByName("Iron")], 2), new CraftingItemInItem((CraftingItem)AllItems[GetItemByName("Regular wood")], 4) }, Rarity.common));
                AllItems.Add(new WeaponsItem("Iron Sword", "A sturdy iron sword, capable of infliction slashing damage.", 1, PhyDamgeType.Slash, 1, true, 1, false, true, new List<ItemType>() { ItemType.Weapon, ItemType.Iron, ItemType.Slash }, new List<CraftingItemInItem>() { new CraftingItemInItem((CraftingItem)AllItems[GetItemByName("Iron")], 4) }, Rarity.common));
                AllItems.Add(new WeaponsItem("Iron Greatsword", "A sturdy iron greatsword, capable of inflicting slashing damage", 1, PhyDamgeType.Slash, 1, true, 0, false, true, new List<ItemType>() { ItemType.Weapon, ItemType.Iron, ItemType.Slash }, new List<CraftingItemInItem>() { new CraftingItemInItem((CraftingItem)AllItems[GetItemByName("Iron")], 8) }, Rarity.common));
                AllItems.Add(new WeaponsItem("Iron Hammer", "A sturdy iron hammer, capable of infliction blunt damage.", 1, PhyDamgeType.Blunt, 1, true, 0, false, true, new List<ItemType>() { ItemType.Iron, ItemType.Weapon, ItemType.Blunt }, new List<CraftingItemInItem> { new CraftingItemInItem((CraftingItem)AllItems[GetItemByName("Iron")], 4) }, Rarity.common));
                AllItems.Add(new WeaponsItem("Iron Warhammer", "A sturdy iron warhammer, capable of inflicting blunt damage.", 1, PhyDamgeType.Blunt, 1, true, 0, false, true, new List<ItemType>() { ItemType.Iron, ItemType.Weapon, ItemType.Blunt }, new List<CraftingItemInItem> { new CraftingItemInItem((CraftingItem)AllItems[GetItemByName("Iron")], 8) }, Rarity.common));
                AllItems.Add(new WeaponsItem("Iron Axe", "A sturdy iron axe, capable of inflicting slashing damage.", 1, PhyDamgeType.Slash, 1, true, 0, false, true, new List<ItemType>() { ItemType.Iron, ItemType.Weapon, ItemType.Slash }, new List<CraftingItemInItem> { new CraftingItemInItem((CraftingItem)AllItems[GetItemByName("Iron")], 2) }, Rarity.common));
                AllItems.Add(new WeaponsItem("Iron Battleaxe", "A sturdy iron battleaxe, capable of inflicting slashing damage.", 1, PhyDamgeType.Slash, 1, true, 0, false, true, new List<ItemType>() { ItemType.Weapon, ItemType.Iron, ItemType.Slash }, new List<CraftingItemInItem> { new CraftingItemInItem((CraftingItem)AllItems[GetItemByName("Iron")], 4) }, Rarity.common));
                AllItems.Add(new WeaponsItem("Iron Dagger", "A sturdy iron dagger, capable of inflicting puncturing damage.", 1, PhyDamgeType.Punture, 1, true, 0, false, true, new List<ItemType>() {ItemType.Weapon, ItemType.Iron, ItemType.Punture}, new List<CraftingItemInItem> { new CraftingItemInItem((CraftingItem)AllItems[GetItemByName("Iron")], 2) }, Rarity.common));
                #endregion
                #endregion

                #region Armor
                #region Leather
                AllItems.Add(new ArmorItem("Leather pants", "", 2, ArmorType.Medium, 4, true, 1, false, true, new List<CraftingItemInItem>() { new CraftingItemInItem((CraftingItem)AllItems[GetItemByName("Leather")], 4) }, Rarity.common, Armorpiece.Legs, new List<ItemType>() { ItemType.Leather, ItemType.Armor, ItemType.Legs, ItemType.Medium}));
                AllItems.Add(new ArmorItem("Leather chestplate", "", 3, ArmorType.Medium, 7, true, 9, false, true, new List<CraftingItemInItem> { new CraftingItemInItem((CraftingItem)AllItems[GetItemByName("Leather")], 8) }, Rarity.common, Armorpiece.Chest, new List<ItemType>() { ItemType.Leather, ItemType.Armor, ItemType.Chest, ItemType.Medium}));
                AllItems.Add(new ArmorItem("Leather gloves", "", 1, ArmorType.Medium, 2, true, 10, false, true, new List<CraftingItemInItem> { new CraftingItemInItem((CraftingItem)AllItems[GetItemByName("Leather")], 2) }, Rarity.common, Armorpiece.Hands, new List<ItemType>() { ItemType.Leather, ItemType.Armor, ItemType.Hands, ItemType.Medium}));
                AllItems.Add(new ArmorItem("Leather helmet", "", 1, ArmorType.Medium, 2, true, 11, false, true, new List<CraftingItemInItem>() { new CraftingItemInItem((CraftingItem)AllItems[GetItemByName("Leather")], 2) }, Rarity.common, Armorpiece.Head, new List<ItemType>() { ItemType.Leather, ItemType.Armor, ItemType.Head, ItemType.Medium}));
                #endregion

                #region Iron

                #endregion

                #endregion

                #region Consumable
                AllItems.Add(new Consumable("Lesser Potion of Healing", "it's a small potion that gives you health back", true, 10, 1, false, Rarity.common, ConsumableItem.Healing, new List<ItemType>() { ItemType.Consumable, ItemType.Heal}));

                #endregion

                #endregion
            }
            StartingEquipment();
        }

        public List<BaseItem> GetTheInventory()
        {
            return TheInventory;
        }

        public int GetGold()
        {
            return Gold;
        }

        public void GiveGold(int MoreGold)
        {
            Gold += MoreGold;
        }

        public void BuyStuff(int LessGold)
        {
            Gold -= LessGold;
        }

        public bool DoesTimeExistsInInventory(string name)
        {
            bool doesexists = false;
            for(int i = 0; i < TheInventory.Count; i++)
            {
                if (TheInventory[i].Getname().ToLower() == name.ToLower())
                {
                    doesexists = true;
                }
            }
            if(doesexists == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int GetItemByName(string name)
        {
            int ItemPlace = 0;
            for(int i = 0; i < AllItems.Count; i++)
            {
                if(AllItems[i].Getname().ToLower() == name.ToLower())
                {
                    ItemPlace = i;
                }
            }
            return ItemPlace;
        }

        public object GetinventoryItemByName(string itemname)
        {
            int itemplace = -1;
            for (int i = 0; i < TheInventory.Count; i++)
            {
                if (TheInventory[i].Getname().ToLower() == itemname.ToLower())
                {
                    itemplace = i;
                }
            }
            return TheInventory[itemplace];
        }

        public void GivePlayerItem(object item)
        {
            TheInventory.Add((BaseItem)item);
        }

        private void StartingEquipment()
        {
            
        }

        public List<BaseItem> GetAllItemsList()
        {
            return AllItems;
        }

        public Inventory()
        {
            Setup();

        }
    }
}
