using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsukaBot_1._0.Module.RPG.Logic.Items
{
    public class WeaponsItem : BaseItem
    {
        private int Damge;
        private PhyDamgeType MyDamgePhyType;
        private bool Craftable;
        private MagicDamgeType MyMagicEffect;
        private List<CraftingItemInItem> CraftingItems;


        public WeaponsItem(string name, string itemdescribe, int damge, PhyDamgeType mydamgephytype, MagicDamgeType magictype,int price, bool buyable, int itemvalue, bool questItem, bool craftable, List<ItemType> itemDefine, List<CraftingItemInItem> craftingItems, Rarity rare)
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
            MyItemType = itemDefine;
            MyRare = rare;
            MyMagicEffect = magictype;
            
        }

        public Attack attack()
        {
            {
                return new Attack(Damge, MyDamgePhyType, MyMagicEffect);
            }
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

        /// <summary>
        /// Only use for damage check and not actauly damage
        /// </summary>
        /// <returns>damage of the weapon</returns>
        public int GetDamge()
        {
            return Damge;
        }

    }

    public class Attack
    {
        private int Damge;
        private PhyDamgeType DamgephyType;
        private MagicDamgeType DamageElementalType;
        public Attack(int damge, PhyDamgeType damgephyType)
        {
            Damge = damge;
            DamgephyType = damgephyType;
        }

        public Attack(int damge, PhyDamgeType damgephyType, MagicDamgeType damageelemnttype)
        {
            Damge = damge;
            DamgephyType = damgephyType;
            DamageElementalType = damageelemnttype;
        }

        public Attack()
        {

        }

        public MagicDamgeType GetElementalDamageType()
        {
            return DamageElementalType;
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

    //fire = extra damage / Earth = slow attack (skip attack turn) / Wind = more likekly to miss or hit less vital spots(lower damage) / water = self heal
    public enum MagicDamgeType
    {
        Fire, Earth, Wind, Water, None
    }
    //more damage toward flesh, blunt more damage toward Armor, Punture more damage toward magic
    public enum PhyDamgeType
    {
        Slash, Blunt, Punture, None
    }
}