using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsukaBot_1._0.Module.RPG.Logic.Enemy
{
    class MonsterDatabase
    {
        private List<NormalEnemy> Database;
        private Random RNG = new Random();
        private Inventory Inven = new Inventory();


        public MonsterDatabase()
        {
            if (Database == null)
            {
                Database = new List<NormalEnemy>();

                #region Monster

                Database.Add(new NormalEnemy("Green Slime", 1, 0, 5, 5, 1, 1, 3, 1, 2, EnemyType.Slime, new List<LootDrop>() { new LootDrop(1, Inven.GetAllItemsList()[Inven.GetItemByName("Regular wood")]) }, 5, 0));
                Database.Add(new NormalEnemy("Blue Slime", 3, 1, 10, 10, 2, 1, 5, 3, 5, EnemyType.Slime, new List<LootDrop>() { new LootDrop(1, Inven.GetAllItemsList()[Inven.GetItemByName("Regular wood")]) }, 5, 0));
                Database.Add(new NormalEnemy("Goblin Mage", 12, 10, 23, 23, 5, 3, 5, 7, 10, EnemyType.Goblin, new List<LootDrop>() { new LootDrop(1, Inven.GetAllItemsList()[Inven.GetItemByName("Regular wood")]) }, 5, 0));
                Database.Add(new NormalEnemy("Savage Goblin", 8, 5, 30, 30, 8, 3, 5, 4, 25, EnemyType.Goblin, new List<LootDrop>() { new LootDrop(1, Inven.GetAllItemsList()[Inven.GetItemByName("Regular wood")]) }, 5, 0));
                Database.Add(new NormalEnemy("Goblin Ranger", 10, 8, 25, 25, 4, 3, 5, 4, 15, EnemyType.Goblin, new List<LootDrop>() { new LootDrop(1, Inven.GetAllItemsList()[Inven.GetItemByName("Regular wood")]) }, 5, 0));

                #endregion
            }
        }

        public NormalEnemy GetMonsterByName(string Name)
        {
            for (int i = 0; i < Database.Count; i++)
            {
                if (Database[i].GetName() == Name)
                {
                    return new NormalEnemy(Database[i]);
                }
            }
            return Database[0];
        }

        public NormalEnemy GetByEnemyType(EnemyType ChoosenType)
        {
            List<NormalEnemy> Templist = new List<NormalEnemy>();
            for (int i = 0; i < Database.Count; i++)
            {
                if (Database[i].GetEnemyState() == ChoosenType)
                {
                    Templist.Add(Database[i]);
                }
            }
            if (Templist.Count == 0)
            {
                return new NormalEnemy(Database[0]);
            }
            else
            {
                return new NormalEnemy(Templist[RNG.Next(0, Templist.Count)]);
            }
        }

        public NormalEnemy GetEnemyAroundLvl(int lvl)
        {
            List<NormalEnemy> TempList = new List<NormalEnemy>();
            for (int i = 0; i < Database.Count; i++)
            {
                if (Database[i].GetMaxSpawningLvl() >= lvl && Database[i].GetMinSpawningLvl() <= lvl)
                {
                    TempList.Add(Database[i]);
                }
            }
            if (TempList.Count == 0 || TempList.Count == 1)
            {
                return new NormalEnemy(Database[0]);
            }
            else
            {
                return new NormalEnemy(TempList[RNG.Next(0, TempList.Count)]);
            }

        }

        public NormalEnemy GetRandomEnemy()
        {
            return new NormalEnemy(Database[RNG.Next(0, Database.Count)]);
        }

    }
}

