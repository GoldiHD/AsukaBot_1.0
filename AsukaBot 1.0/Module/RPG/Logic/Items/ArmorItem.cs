using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsukaBot_1._0.Module.RPG.Logic.Items
{
    class ArmorItem : BaseItem
    {
        private int AC;
        private ArmorType MyArmorType;
        private bool Crafable;
        private List<CraftingItemInItem> CraftingItems;
        private Armorpiece ArmorPos;


        public ArmorItem(string name, string itemdescribe, int ac, ArmorType armor, int price, bool buyable, int itemvalue, bool questitem, bool craftable, List<CraftingItemInItem> craftingItems, Rarity rare, Armorpiece armorpos, List<ItemType> itemDefine)
        {
            MyRare = rare;
            Buyable = buyable;
            Name = name;
            ItemDescribe = itemdescribe;
            AC = ac;
            MyArmorType = armor;
            Price = price;
            ItemValue = itemvalue;
            QuestItem = questitem;
            Crafable = craftable;
            CraftingItems = craftingItems;
            ArmorPos = armorpos;
            MyItemType = itemDefine;
        }

        public ArmorItem(string name, string itemdescribe, int ac, ArmorType armor, int price, bool buyable, int itemvalue, bool questitem, bool craftable, List<CraftingItem> craftingItems, Rarity rare, Armorpiece armorpos, ArmorEnchant enchant)
        {

        }

        public string GetCraftingMaterial()
        {
            string data = "";

            for (int i = 0; i < CraftingItems.Count; i++)
            {
                data += CraftingItems[i].GetItem().Getname() + " " + CraftingItems[i].GetAmount() + ", ";
            }
            data = data.Substring(0, data.Length - 2);
            return data;
        }

        public ArmorType GetArmorType()
        {
            return MyArmorType;
        }

        public Armorpiece GetArmorPos()
        {
            return ArmorPos;
        }

        public int GetAC()
        {
            return AC;
        }
    }

    public enum ArmorType
    {
        Light, Medium, Heavy
    }

    public enum Armorpiece
    {
        Head, Chest, Hands, Legs
    }


    public class ArmorEnchant
    {
        public ArmorEnchant()
        {

        }
    }


}
