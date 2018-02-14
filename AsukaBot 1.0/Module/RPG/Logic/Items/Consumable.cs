using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsukaBot_1._0.Module.RPG.Logic.Items
{
    public class Consumable : BaseItem
    {
        private Effect MyEffect;
        private int ItemStrength;
        private ConsumableItem MyConsuamableType;

        public Consumable(string name, string itemdescribe, bool buyable, int price, int itemStrength, bool questitem, Rarity rare, ConsumableItem itemType, List<ItemType> itemDefine)
        {
            Name = name;
            ItemDescribe = itemdescribe;
            Buyable = buyable;
            Price = price;
            ItemStrength = itemStrength;
            QuestItem = questitem;
            MyRare = rare;
            MyConsuamableType = itemType;
            MyItemType = itemDefine;
            switch (MyConsuamableType)
            {
                case ConsumableItem.Healing:
                    MyEffect = new Heal(ItemStrength);
                    break;

                case ConsumableItem.Food:
                    MyEffect = new Food(ItemStrength);
                    break;

                case ConsumableItem.Boost:
                    MyEffect = new Buff(ItemStrength);
                    break;

                case ConsumableItem.Defense:
                    MyEffect = new Defense(ItemStrength);
                    break;

                case ConsumableItem.Mana:
                    MyEffect = new ManaPotion(ItemStrength);
                    break;
            }
        }

        public Effect GetEffect()
        {
            return MyEffect;
        }

    }

    public class Effect
    {
        protected int Strenght;
        //make override here
        public virtual string UseAblility(Player user)
        {
            return null;
        }
        public int GetStrenght()
        {
            return Strenght;
        }
    }

    public class Heal : Effect
    {
        private int healeffect;
        public Heal(int strenght)
        {
            Strenght = strenght;
            healeffect = Strenght * 10;
        }

        //do override here
        public override string UseAblility(Player user)
        {
            user.GetStats().GetVitallity().SetHealth(user.GetStats().GetVitallity().GetMyHealth() + healeffect);
            return user.GetPlayername() + "'s health is now " + user.GetStats().GetVitallity().GetMyHealth() + "/" + user.GetStats().GetVitallity().GetMyMaxHealth();
        }
    }

    public class Buff : Effect
    {
        private int DamgeIncrease;
        public Buff(int strenght)
        {
            Strenght = strenght;
            DamgeIncrease = Strenght * 10;
        }
        public override string UseAblility(Player user)
        {
            //do buff

            return user.GetPlayername() + " have recived a " + DamgeIncrease + "% increase in attack";
        }
    }

    public class Defense : Effect
    {
        private int DefenseIncrease;
        public Defense(int strenght)
        {
            Strenght = strenght;
            DefenseIncrease = Strenght * 10;
        }
        public override string UseAblility(Player user)
        {
            // do buff
            return user.GetPlayername() + "'s have increased his defense by " + DefenseIncrease;
        }
    }

    public class Food : Effect
    {
        private int HealthRegain;
        public Food(int strenght)
        {
            Strenght = strenght;
        }
        public override string UseAblility(Player user)
        {
            HealthRegain = Strenght * (5 * user.GetPlayerLvl());
            return user.GetPlayername() + "'s health is now " + user.GetStats().GetVitallity().GetMyHealth() + "/" + user.GetStats().GetVitallity().GetMyMaxHealth();
        }
    }

    public class ManaPotion : Effect
    {
        private int ManaRegain;

        public ManaPotion(int strenght)
        {
            Strenght = strenght;
            ManaRegain = Strenght * 10;
        }

        public override string UseAblility(Player user)
        {
            user.GetStats().GetMagic().SetMana(user.GetStats().GetMagic().GetMana() + ManaRegain);
            return user.GetPlayername() + "'s mana is now" + user.GetStats().GetMagic().GetMana() + "/" + user.GetStats().GetMagic().GetMaxMana();
        }
    }

    public enum ConsumableItem
    {
        Healing, Boost, Food, Defense, Mana, Attack

    }
}

