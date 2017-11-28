using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsukaBot_1._0.Module.RPG.Logic.Items
{
    class WeaponsItem : BaseItem
    {
        private int Damge;
        private PhyDamgeType MyDamgePhyType;
        private WeaponMagicEffect MyWeapoEffect;
        private bool Craftable;
        private List<CraftingItemInItem> CraftingItems;

        public WeaponsItem(string name, string itemdescribe, int damge, PhyDamgeType mydamgephytype, int price, bool buyable, int itemvalue, bool questItem, bool craftable, List<CraftingItemInItem> craftingItems, Rarity rare)
        {
            Name = name;
            ItemDescribe = itemdescribe;
            Damge = damge;
            MyDamgePhyType = mydamgephytype;
            Price = price;
            Buyable = buyable;
            ItemValue = itemvalue;
            QuestItem = questItem;
            Craftable = craftable;
            CraftingItems = craftingItems;
            MyRare = rare;

        }

        public Attack attack()
        {
            if (MyWeapoEffect == null)
            {
                return new Attack(Damge + MyWeapoEffect.GetExtraDamage(), MyDamgePhyType);
            }
            else
            {
                return new Attack(Damge, MyDamgePhyType);
            }
        }

        public WeaponsItem(string name, string itemdescribe, int damge, PhyDamgeType mydamgephytype, int price, bool buyable, int itemvalue, bool questItem, bool craftable, List<CraftingItemInItem> craftingItems, Rarity rare, WeaponMagicEffect myweaponeffect)
        {
            Name = name;
            ItemDescribe = itemdescribe;
            Damge = damge;
            MyDamgePhyType = mydamgephytype;
            Price = price;
            Buyable = buyable;
            ItemValue = itemvalue;
            MyWeapoEffect = myweaponeffect;
            QuestItem = questItem;
        }

        public int GetDamge()
        {
            return Damge;
        }

        public PhyDamgeType GetPhyDamgeType()
        {
            return MyDamgePhyType;
        }

        public string GetCraftingMaterial()
        {
            string data = "";
            if (CraftingItems.Count <= 0)
            {
                data = "uncraftable";
            }
            else
            {
                for (int i = 0; i < CraftingItems.Count; i++)
                {
                    data += CraftingItems[i].GetItem().Getname() + " " + CraftingItems[i].GetAmount() + ", ";
                }
                data = data.Substring(0, data.Length - 2);
            }

            return data;

        }

    }

    class Attack
    {
        private int Damge;
        private PhyDamgeType DamgephyType;
        public Attack(int damge, PhyDamgeType damgephyType)
        {
            Damge = damge;
            DamgephyType = damgephyType;
        }

        public int GetDamge()
        {
            return Damge;
        }

        public PhyDamgeType GetDamgeTypePhy()
        {
            return DamgephyType;
        }

    }


    public enum MagicDamgeType
    {
        Fire, Earth, Wind, Water
    }

    public enum PhyDamgeType
    {
        Slash, Blunt, Punture, None
    }
}