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
        SaveLoadRPGData SaverLoader;
        private string Command = "";

        public void StartUp()
        {
            SingleTon.AssignCheckConsole(new Thread(RunTime));
        }

        private void RunTime()
        {
            if(SaverLoader == null)
            {
                SaverLoader = new SaveLoadRPGData();
            }
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

                    SaverLoader.SaveData();
                    Console.WriteLine("saved"); 
                    break;

                    //don't work because it can't access the right instance of the program

                case "loadrpg":
                    try
                    {
                        SaverLoader.LoadData();
                        Console.WriteLine("Loaded");
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine("Failed to load");
                        Console.WriteLine("[ERORR] :" + ex);
                    }
                    break;

                default:
                    Console.WriteLine("command not recognised");
                    break;
                    
            }
        }
    }
}
