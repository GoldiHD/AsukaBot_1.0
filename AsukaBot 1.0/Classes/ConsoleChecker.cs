using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsukaBot_1._0.Classes
{
    class ConsoleChecker
    {
        private string Command = "";

        public void StartUp()
        {
            SingleTon.AssignCheckConsole(new Thread(RunTime));
        }

        private void RunTime()
        {
            while (true)
            {
                Command = Console.ReadLine().ToLower();
                CheckCommand();
            }
        }

        private void CheckCommand()
        {
            switch (Command.ToLower())
            {
                case "clear":
                    Console.Clear();
                    break;

                case "saverpg":
                    SaveLoadRPGData Saver = new SaveLoadRPGData();
                    Saver.SaveData();
                    Console.WriteLine("saveed");
                    break;
                    
            }
        }
    }
}
