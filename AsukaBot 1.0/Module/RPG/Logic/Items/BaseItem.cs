using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsukaBot_1._0.Module.RPG.Logic.Items
{
    class BaseItem
    {
        protected Rarity MyRare;
        protected int Price;
        protected bool Buyable;
        protected string Name;
        protected int ItemValue;
        protected string ItemDescribe;
        protected bool QuestItem;
        protected ItemType MyItemType;

        public BaseItem(int price, bool buyable, string name, int itemValue, string itemDescribe, bool questItem, Rarity rare, ItemType myitemtype)
        {
            Price = price;
            Buyable = buyable;
            Name = name;
            ItemValue = itemValue;
            ItemDescribe = itemDescribe;
            QuestItem = questItem;
            MyRare = rare;
            MyItemType = myitemtype;
        }

        public string GetFlavorText()
        {
            return ItemDescribe;
        }

        public BaseItem()
        {

        }

        public ItemType GetItemType()
        {
            return MyItemType;
        }

        public int Sell()
        {
            if (Buyable)
            {
                return Price;
            }
            else
            {
                return 0;
            }
        }

        public Rarity GetRarity()
        {
            return MyRare;
        }

        public string Getname()
        {
            return Name;
        }

        public bool GetBuyableState()
        {
            return Buyable;
        }

        public bool GetQuestState()
        {
            return QuestItem;
        }

        public int GetPrice()
        {
            return Price;
        }


    }

    public enum ItemType
    {
        Weapon, Magic, Craftingitem, Armor, Consumable
    }

    public enum Rarity
    {
        common, uncommon, rare, legendary, mythicc, hentai
    }
}