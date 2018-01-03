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
        private Player User;
        public void StartAdventure(Player player, PlayerStates state)
        {
            User = player;
            User.SetPlayerStates(state);

            switch (User.GetPlayerState())
            {
                case PlayerStates.Encounter:
                    story = new StoryMaker(1,0,User.GetPlayerLvl());
                    break;

                case PlayerStates.Campaign:

                    break;

                case PlayerStates.CollabBoss:

                    break;

                case PlayerStates.Dungeon:
                    story = new StoryMaker(3, 1, User.GetPlayerLvl());
                    break;

                case PlayerStates.Pvp:

                    break;

                case PlayerStates.Rest:
                    Console.WriteLine("player [" + User.GetPlayername() + "] have broken the game");
                    break;
            }
        }

        public StoryMaker GetStoryMaker()
        {
            return story;
        }

    }

    public class StoryMaker
    {
        private MonsterDatabase MyMonsterDatabase = SingleTon.GetMonsterDatabaseInstace();
        private List<NormalEnemy> EnemyList = new List<NormalEnemy>();
        private NormalEnemy CurrentEnemy;
        public StoryMaker(int rooms, int boss, int playerlvl)
        {
            for (int i = 0; i < rooms; i++)
            {
                EnemyList.Add(MyMonsterDatabase.GetEnemyAroundLvl(playerlvl));
            }
        }

        public int GetBattles()
        {
            return EnemyList.Count;
        }

        public NormalEnemy GetEnemy()
        {
            if(CurrentEnemy == null)
            {
                if (EnemyList.Count == 0)
                {
                    return null;
                }
                else
                {
                    CurrentEnemy = EnemyList[0];
                    return CurrentEnemy;
                }
            }
            else
            {
                return CurrentEnemy;
            }
        }

        public void RemoveAndUpdateList()
        {
            CurrentEnemy = null;
            EnemyList.RemoveAt(0);
        }
    }
}

