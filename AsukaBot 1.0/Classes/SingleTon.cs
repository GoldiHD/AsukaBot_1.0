using AsukaBot_1._0.Module.RPG.Logic.Enemy;
using System.Threading;
using AsukaBot_1._0.Module.Music.Logic;

namespace AsukaBot_1._0.Classes
{
    class SingleTon
    {
        private static MonsterDatabase MyMonsterDatabaseInstace;
        private static ConsoleChecker ConsoleCheckerInstance;
        private static Thread CheckConsole;
        private static RPG RPGInstance;

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

        public static void AssignCheckConsole(Thread MyThread)
        {
            if (CheckConsole == null)
            {
                CheckConsole = MyThread;
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
