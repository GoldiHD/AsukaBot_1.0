
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AsukaBot_1._0.Module.RPG;
using AsukaBot_1._0.Module.Music.Logic;

namespace AsukaBot_1._0.Classes
{
    class RPGThreadChecker
    {
        public void StartUp()
        {
            SingleTon.AssignRPGCThreadCheck(new Thread(RunTime));
        }

        public void RunTime()
        {
            RPG RPGRefrence = SingleTon.GetRPG();
            while (true)
            {
                if (RPGRefrence.GetPlayers() != null)
                {
                    if (RPGRefrence.GetPlayers()[0].GetQuestManager().GetPlayerListRequest() != null)
                    {
                        if (RPGRefrence.GetPlayers()[0].GetQuestManager().GetPlayerListRequest().Count != 0)
                        {
                            for (int i = 0; i < RPGRefrence.GetPlayers()[0].GetQuestManager().GetPlayerListRequest().Count; i++)
                            {
                                if(RPGRefrence.GetPlayers()[0].GetQuestManager().GetPlayerListRequest()[i].IsTimeOut())
                                {
                                    RPGRefrence.GetPlayers()[0].GetQuestManager().GetPlayerListRequest().Remove(RPGRefrence.GetPlayers()[0].GetQuestManager().GetPlayerListRequest()[i]);
                                }
                            }
                        }
                    }
                }
                Thread.Sleep(5000);
            }
        }
    }
}
