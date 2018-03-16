using AsukaBot_1._0.Module.RPG.Logic.Enemy;
using System.Threading;
using AsukaBot_1._0.Module.Music.Logic;
using Discord.Audio;
using AsukaBot_1._0.Module.RPG.Logic.Questing;

namespace AsukaBot_1._0.Classes
{
    class SingleTon
    {
        private static MonsterDatabase MyMonsterDatabaseInstace;
        private static ConsoleChecker ConsoleCheckerInstance;
        private static Thread CheckConsole;
        private static Thread RPGThreadCheck;
        private static RPG RPGInstance;
        private static RPGThreadChecker RPGThreadCheckerInstance;

        public static MonsterDatabase GetMonsterDatabaseInstace()
        {
            if(MyMonsterDatabaseInstace == null)
            {
                MyMonsterDatabaseInstace = new MonsterDatabase();
            }
            return MyMonsterDatabaseInstace;
        }

        public static ConsoleChecker GetConsoleCheckerInstance()
        {
            if (ConsoleCheckerInstance == null)
            {
                ConsoleCheckerInstance = new ConsoleChecker();
            }
            return ConsoleCheckerInstance;
        }

        public static RPGThreadChecker GetRPGThread()
        {
            if(RPGThreadCheckerInstance == null)
            {
                RPGThreadCheckerInstance = new RPGThreadChecker();
            }
            return RPGThreadCheckerInstance;
        }

        public static void AssignRPGCThreadCheck(Thread myThread)
        {
            if(RPGThreadCheck == null)
            {
                RPGThreadCheck = myThread;
                RPGThreadCheck.Start();
            }
        }

        public static void AssignCheckConsole(Thread myThread)
        {
            if (CheckConsole == null)
            {
                CheckConsole = myThread;
                CheckConsole.Start();
            }
        }

        public static RPG GetRPG()
        {
            return RPGInstance;
        }

        public static void SetRPG(RPG instance)
        {
            if(RPGInstance == null)
            {
                RPGInstance = instance;
            }
        }

    }
}
