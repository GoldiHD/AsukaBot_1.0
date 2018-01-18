using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsukaBot_1._0.Module.RPG.Logic.Items
{
    public class MagicAttacks : BaseItem
    {
        private int Damage;
        private ElementType AttackType;
        private SpellTier SpellLvl;
        private int MagicLvlReqiurement;

        public MagicAttacks(string name, string spelldescribe, int damage, ElementType attackType, SpellTier spellLvl, int magiclvlrequrements, int price, bool buyable, List<ItemType> itemTags, int itemvalue, bool questitem, Rarity rare)
        {
            Name = name;
            ItemDescribe = spelldescribe;
            Damage = damage;
            AttackType = attackType;
            SpellLvl = spellLvl;
            Price = price;
            Buyable = buyable;
            MyItemType = itemTags;
            ItemValue = itemvalue;
            QuestItem = questitem;
            MyRare = rare;
            MagicLvlReqiurement = magiclvlrequrements;
        }

        /*
        public Attack attack()
        {
            return new Attack();
        }
        */
    }

    public enum SpellTier
    {
        I, II, III, IV, V, VI, VII, VIII, IX, X
    }

    public enum ElementType
    {
        Water, Thunder, Fire, Earth, Light, Dark
    }
}
