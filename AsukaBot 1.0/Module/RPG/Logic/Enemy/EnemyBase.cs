using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AsukaBot_1._0.Module.RPG.Logic.Items;

namespace AsukaBot_1._0.Module.RPG.Logic.Enemy
{
    public class EnemyBase
    {
        protected List<LootDrop> Loot = new List<LootDrop>();
        protected int GoldDropMax;
        protected int GoldDropMin;
        protected int Lvl;
        protected string Name;
        protected int DamgeMax;
        protected int DamgeMin;
        protected int HP;
        protected int CurrentHP;
        protected int AC;
        protected int lvlRangeMin;
        protected int lvlRangeMax;
        protected int XpGain;
        protected Random rng = new Random();

        //5% to crit
        //5% to dodge

        public EnemyBase()
        {

        }

        public int GetDamge()
        {
            return rng.Next(DamgeMin, DamgeMax + 1);
        }

        public string GetName()
        {
            return Name;
        }


    }

    class Boss : EnemyBase
    {
        public Boss(string name, int damgemax, int damgemin, int hp, int currenthp, int ac, int levelrangemin, int levelrangemax)
        {

        }
    }

    public class NormalEnemy : EnemyBase
    {
        protected float CritChance = 0.12f;
        protected EnemyType MyType;

        /// <summary>
        /// Creating an enemy that the player can fight
        /// </summary>
        /// <param name="name">Name of the enemy you're creating</param>
        /// <param name="damgemax">maximum damge the enemy can make</param>
        /// <param name="damgemin">the minimum damge the enemy can make</param>
        /// <param name="hp">the max hp the enemy has</param>
        /// <param name="currenthp">the current health of the enemy, this may be lowered in case the creature is alread damged</param>
        /// <param name="ac">how much of the damge gets negated</param>
        /// <param name="levelrangemin">minimum lvl the player can be before it can fight this enemy</param>
        /// <param name="levelrangemax">maximum lvl the player can be before it can fight this enemy</param>
        /// <param name="lvl">the lvl of the enemy, this is used in calculationg armor</param>
        /// <param name="xpGain">how xp the player gain from fighting this enemy</param>
        /// <param name="mytype">what type of enemy the enemy is</param>
        public NormalEnemy(string name, int damgemax, int damgemin, int hp, int currenthp, int ac, int levelrangemin, int levelrangemax, int lvl, int xpGain, EnemyType mytype)
        {
            Name = name;
            DamgeMax = damgemax;
            DamgeMin = damgemin;
            HP = hp;
            CurrentHP = currenthp;
            AC = ac;
            lvlRangeMin = levelrangemin;
            lvlRangeMax = levelrangemax;
            Lvl = lvl;
            XpGain = xpGain;
            MyType = mytype;
        }

        public NormalEnemy(string name, int damgemax, int damgemin, int hp, int currenthp, int ac, int levelrangemin, int levelrangemax, int lvl, int xpGain, EnemyType mytype, List<LootDrop> loot, int goldDropMax, int goldDropMin)
        {
            Name = name;
            DamgeMax = damgemax;
            DamgeMin = damgemin;
            HP = hp;
            CurrentHP = currenthp;
            AC = ac;
            lvlRangeMin = levelrangemin;
            lvlRangeMax = levelrangemax;
            Lvl = lvl;
            XpGain = xpGain;
            MyType = mytype;
            Loot = loot;
            GoldDropMax = goldDropMax;
            GoldDropMin = goldDropMin;
        }

        public NormalEnemy(NormalEnemy Copy)
        {
            Name = Copy.GetName();
            DamgeMax = Copy.GetMaxdamge();
            DamgeMin = Copy.GetMindamge();
            HP = Copy.GetMaxHP();
            CurrentHP = Copy.GetHP();
            AC = Copy.GetAc();
            lvlRangeMin = Copy.GetMinSpawningLvl();
            lvlRangeMax = Copy.GetMaxSpawningLvl();
            Lvl = Copy.GetLvl();
            XpGain = Copy.GetXP();
            MyType = Copy.GetMyType();
            //temp
            if (Copy.GetLootDrop() != null)
            {
                Loot = Copy.GetLootDrop();
                GoldDropMax = Copy.GetMaxGold();
                GoldDropMin = Copy.GetMinGold();
            }
        }

        public string GetGold(Player user, int rate)
        {
            int GoldForKill = rng.Next(GoldDropMin, GoldDropMax + 1);
            user.GetInventory().GiveGold(GoldForKill * rate);
            if (GoldForKill == 0)
            {
                return "you didn't gain any gold";
            }
            else
            {
                return "You gained " + GoldForKill * rate + " gold";
            }
        }

        public List<LootDrop> GetLootDrop()
        {
            return Loot;
        }

        public int GetMaxGold()
        {
            return GoldDropMax;
        }

        public int GetMinGold()
        {
            return GoldDropMin;
        }

        public int GetLvl()
        {
            return Lvl;
        }

        public EnemyType GetMyType()
        {
            return MyType;
        }

        public int GetAc()
        {
            return AC;
        }

        public float GetCritchance()
        {
            return CritChance;
        }

        public int GetMaxdamge()
        {
            return DamgeMax;
        }

        public int GetMindamge()
        {
            return DamgeMin;
        }

        public EnemyType GetEnemyState()
        {
            return MyType;
        }
        public void Attack(Player theplayer, int damge)
        {
            theplayer.GetStats().GetVitallity().SetHealth(theplayer.GetStats().GetVitallity().GetMyHealth() - damge);
        }

        public int GetHP()
        {
            return CurrentHP;
        }

        public string GetLoot(Player user)
        {
            List<BaseItem> ReturnLoot = new List<BaseItem>();
            BaseItem TempHolder = null;

            string Lootreturnstring;
            for (int i = 0; i < rng.Next(1, 4); i++)     //chance to get all the way up to 3 loot drops
            {
                TempHolder = GetDropChance();
                if (TempHolder != null)
                {
                    ReturnLoot.Add(TempHolder);
                    user.GetInventory().GetTheInventory().Add(ReturnLoot[ReturnLoot.Count - 1]);
                }

            }
            Lootreturnstring = "You recived:";
            if (ReturnLoot.Count == 0)
            {
                Lootreturnstring += " Nothing";
            }
            for (int i = 0; i < ReturnLoot.Count; i++)
            {
                Lootreturnstring += " " + ReturnLoot[i].Getname();

            }
            return Lootreturnstring;
        }

        public int GetMaxHP()
        {
            return HP;
        }

        public int GetMaxSpawningLvl()
        {
            return lvlRangeMax;
        }

        public int GetMinSpawningLvl()
        {
            return lvlRangeMin;
        }

        public int GetXP()
        {
            return XpGain;
        }

        public void DealDamge(int damge)
        {
            CurrentHP -= damge;
        }

        public float DamgeModify()
        {
            return (AC + Lvl)/100;
        }
        public BaseItem GetDropChance()
        {
            if (Loot.Count() == 0 || Loot == null)
            {
                return null;
            }
            else
            {
                List<BaseItem> PercentageChance = new List<BaseItem>();
                for (int i = 0; i < Loot.Count; i++)
                {
                    for (int x = 0; x < Loot[i].GetDropChance(); x++)
                    {
                        PercentageChance.Add(Loot[i].GetItem());
                    }
                }

                int FinalDrop = rng.Next(0, Loot.Count);
                return Loot[FinalDrop].GetItem();
            }
        }
    }

    public class LootDrop
    {
        private int DropChance;
        private BaseItem Item;


        public LootDrop(int dropChance, BaseItem item)
        {
            DropChance = dropChance;
            Item = item;
        }


        public BaseItem GetItem()
        {
            return Item;
        }

        public int GetDropChance()
        {
            return DropChance;
        }

    }

    public enum EnemyType
    {
        Goblin, Slime, Troll, Giant, Witch, Wolf, Dragon
    }
}
