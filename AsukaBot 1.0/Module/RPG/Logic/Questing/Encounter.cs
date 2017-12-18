using AsukaBot_1._0.Module.RPG.Logic.Enemy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsukaBot_1._0.Module.RPG.Logic.Questing
{
    public class Encounter : QuestManager
    {
        private EnemyBase MyEnemy;

        public Encounter(EnemyBase myenemy)
        {
            MyEnemy = myenemy;
        }
    }
}
