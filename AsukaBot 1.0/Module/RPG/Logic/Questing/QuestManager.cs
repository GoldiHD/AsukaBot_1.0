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



        public StoryMaker GetStoryMaker()
        {
            return story;
        }

        public void StartUpDataStory()
        {
            if (story == null)
            {
                story = new StoryMaker();
            }
        }

    }

    public class StoryMaker
    {
        public static List<PlayerRequest> PlayerBattleRequestList = new List<PlayerRequest>();
        public static List<PVPCombatControler> PlayerBattleControler = new List<PVPCombatControler>();
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

        public StoryMaker()
        { }

        public List<PVPCombatControler> GetPlayerBattleControler()
        {
            return PlayerBattleControler;
        }

        public List<PlayerRequest> GetPlayerListRequest()
        {
            return PlayerBattleRequestList;
        }

        public StoryMaker(Player Attacker, Player Defender)
        {
            if (PlayerBattleRequestList == null)
            {
                PlayerBattleRequestList = new List<PlayerRequest>();
            }
            if (PlayerBattleControler == null)
            {
                PlayerBattleControler = new List<PVPCombatControler>();
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

        public void ChangeRequestToCombat(int i)
        {

            PVPCombatControler TempHolder = new PVPCombatControler();
            TempHolder.AssignUsers(PlayerBattleRequestList[i].GetAttacker(), PlayerBattleRequestList[i].GetDefender());
            PlayerBattleRequestList[i].GetAttacker().SetPVPCombatControler(TempHolder);
            PlayerBattleRequestList[i].GetDefender().SetPVPCombatControler(TempHolder);
            PlayerBattleRequestList.RemoveAt(i);
            PlayerBattleControler.Add(TempHolder);
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
            //remove from list and add
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

    public class PVPCombatControler
    {
        private Player Attacker;
        private Player Defender;
        public Stopwatch RoundTimeOut = new Stopwatch();
        private bool AttackersTurn;

        public void AssignUsers(Player attacker, Player defender)
        {
            Attacker = attacker;
            Defender = defender;
            RoundTimeOut.Start();
        }
        public void RotateTurn()
        {
            AttackersTurn = !AttackersTurn;
        }

        public void IsTimeOut()
        {
            if (RoundTimeOut.ElapsedMilliseconds > 10800)  // 3 min
            {
                RoundTimeOut.Reset();
                RoundTimeOut.Start();
                RotateTurn();
            }
        }

        public string AttackOtherPlayer(Player UserAttackRequest)
        {
            if (UserAttackRequest == Attacker && AttackersTurn == true)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("[ERROR] attack not yet implamented in pvp");
                Console.ForegroundColor = ConsoleColor.White;
                RoundTimeOut.Reset();
                RoundTimeOut.Start();
                RotateTurn();
                return "You attacked " + Defender.GetUsername() + " for " + Defender.Attack(Attacker)+ ", and got " + Defender.GetStats().GetVitallity().GetMyHealth() + "/"+ Defender.GetStats().GetVitallity().GetMyMaxHealth();
            }
            else if (UserAttackRequest == Defender && AttackersTurn == true)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("[ERROR] attack not yet implamented in pvp");
                Console.ForegroundColor = ConsoleColor.White;
                RoundTimeOut.Reset();
                RoundTimeOut.Start();
                RotateTurn();
                return "You attacked " + Attacker.GetUsername() + " for " + Defender.Attack(Attacker) + ", and got " + Defender.GetStats().GetVitallity().GetMyHealth() + "/" + Defender.GetStats().GetVitallity().GetMyMaxHealth();
            }
            else
            {
                if (UserAttackRequest.GetUsername() == Attacker.GetUsername())
                {
                    return "it's not your turn yet, either wait  " + (10800 - RoundTimeOut.ElapsedMilliseconds - 10800) + " or if " + Defender.GetUsername() + ", finshes up thire turn";
                }
                else
                {
                    return "it's not your turn yet, either wait  " + (10800 - RoundTimeOut.ElapsedMilliseconds - 10800) + " or if "+Attacker.GetUsername()+", finshes up thire turn";
                }
            }
        }
    }
}

