using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AsukaBot_1._0.Module.RPG.Logic.Enemy;
using AsukaBot_1._0.Classes;

namespace AsukaBot_1._0.Module.RPG.Logic.Questing
{
    public class QuestManager
    {
        private StoryMaker story;
        private CombatManager MyCombatManager;
        private Player User;
        public void StartAdventure(Player player, PlayerStates state)
        {
            User = player;
            User.SetPlayerStates(state);

            switch (User.GetPlayerState())
            {
                case PlayerStates.Encounter:
                    MyCombatManager = new CombatManager(User.GetPlayerLvl());
                    break;

                case PlayerStates.Campaign:

                    break;

                case PlayerStates.CollabBoss:

                    break;

                case PlayerStates.Dungeon:
                    story = new StoryMaker();
                    MyCombatManager = new CombatManager(story.GetNextEnemy().GetName());
                    break;

                case PlayerStates.Pvp:

                    break;

                case PlayerStates.Rest:
                    Console.WriteLine("player [" + User.GetPlayername() + "] have broken the game");
                    break;
            }
        }

        public CombatManager GetCombatManager()
        {
            return MyCombatManager;
        }

    }

    class StoryMaker
    {
        private MonsterDatabase MyMonsterDatabase = SingleTon.GetMonsterDatabaseInstace();
        List<NormalEnemy> EnemyList = new List<NormalEnemy>();
        public StoryMaker()
        {

        }

        public NormalEnemy GetNextEnemy()
        {
            return GetNextAndRemove();
        }

        private NormalEnemy GetNextAndRemove()
        {
            NormalEnemy temp = EnemyList[0];
            EnemyList.RemoveAt(0);
            return temp;
        }
    }

    public class CombatManager
    {
        private NormalEnemy CurrentEnemy;
        private MonsterDatabase MyMonsterDatabase = SingleTon.GetMonsterDatabaseInstace();
        private int cooldown; //### STILL NEED INPLAMTENTATION ###

        public NormalEnemy GetEnemy()
        {
            return CurrentEnemy;
        }

        public CombatManager(string name)
        {

        }

        public CombatManager(EnemyType type)
        {

        }

        public CombatManager(int playerlvl)
        {
            CurrentEnemy = MyMonsterDatabase.GetEnemyAroundLvl(playerlvl);
        }

        public CombatManager()
        {

        }

    }
}

