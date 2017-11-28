using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AsukaBot_1._0.Module.RPG.Logic.Enemy;
using System.Threading;

namespace AsukaBot_1._0.Classes
{
    class SingleTon
    {
        private static MonsterDatabase MyMonsterDatabaseInstace;
        private static ConsoleChecker ConsoleCheckerInstance;
        private static Thread CheckConsole;

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
    }
}
