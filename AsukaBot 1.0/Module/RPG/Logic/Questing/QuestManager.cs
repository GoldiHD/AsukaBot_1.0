using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AsukaBot_1._0.Module.RPG.Logic.Enemy;
using AsukaBot_1._0.Classes;
using System.Diagnostics;

namespace AsukaBot_1._0.Module.RPG.Logic.Questing
{
    public class QuestManager
    {
        private StoryMaker story;
        private Player User;
        private static List<PlayerRequest> PlayerRequestList;
        public void StartAdventure(Player player, PlayerStates state, Player Defender = null)
        {
            User = player;
            User.SetPlayerStates(state);

            switch (User.GetPlayerState())
            {
                case PlayerStates.Encounter:
                    story = new StoryMaker(1, 0, User.GetPlayerLvl());
                    break;

                case PlayerStates.Campaign:

                    break;

                case PlayerStates.CollabBoss:

                    break;

                case PlayerStates.Dungeon:
                    story = new StoryMaker(3, 1, User.GetPlayerLvl());
                    break;

                case PlayerStates.Pvp:
                    story = new StoryMaker(player, Defender);
                    break;

                case PlayerStates.Rest:
                    Console.WriteLine("player [" + User.GetPlayername() + "] have broken the game");
                    break;
            }
        }

        public List<PlayerRequest> GetPlayerListRequest()
        {
            return PlayerRequestList;
        }

        public StoryMaker GetStoryMaker()
        {
            return story;
        }

    }

    public class StoryMaker
    {
        public static List<PlayerRequest> PlayerBattleRequestList = new List<PlayerRequest>();
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
        public StoryMaker(Player Attacker, Player Defender)
        {
            if(PlayerBattleRequestList == null)
            {
                PlayerBattleRequestList = new List<PlayerRequest>();
            }
            PlayerBattleRequestList.Add(new PlayerRequest(Attacker, Defender));
        }

        public int GetBattles()
        {
            return EnemyList.Count;
        }

        public NormalEnemy GetEnemy()
        {
            if (CurrentEnemy == null)
            {
                if (EnemyList.Count > 0)
                {
                    CurrentEnemy = EnemyList[0];
                    EnemyList.RemoveAt(0);
                }
            }
            return CurrentEnemy;

        }

        public void RemoveAndUpdateList()
        {
            CurrentEnemy = null;
            if (EnemyList.Count > 0)
            {
                CurrentEnemy = EnemyList[0];
                EnemyList.RemoveAt(0);
            }
        }
    }

    public class PlayerRequest
    {
        private Player Attacker;
        private Player Defender;
        Stopwatch RequestTimeout;
        public PlayerRequest(Player attack, Player defender)
        {
            Attacker = attack;
            Defender = defender;
            RequestTimeout = new Stopwatch();
        }

        public bool IsTimeOut()
        {
            if (RequestTimeout.ElapsedMilliseconds > 10800)  // 3 min
            {
                Attacker.SetPlayerStates(PlayerStates.Rest);
                return true;
            }
            else
            {
                return false;
            }
        }
        public void acceptRequest()
        {
            Attacker.SetPlayerStates(PlayerStates.Pvp);
            Defender.SetPlayerStates(PlayerStates.Pvp);
            //CREATE PVP INSTANCE 
        }

        public Player GetAttacker()
        {
            return Attacker;
        }

        public Player GetDefender()
        {
            return Defender;
        }
    }
}

