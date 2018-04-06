
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
            if(RPGRefrence == null)
            {
                RPGRefrence = new RPG();
            }
            while (true)
            {
                if (RPGRefrence.GetPlayers() != null)
                {
                    if (RPGRefrence.GetPlayers().Count != 0)
                    {
                        if (RPGRefrence.GetPlayers()[0].GetQuestManager().GetStoryMaker().GetPlayerListRequest() != null)
                        {
                            if (RPGRefrence.GetPlayers()[0].GetQuestManager().GetStoryMaker().GetPlayerListRequest().Count != 0)
                            {
                                for (int i = 0; i < RPGRefrence.GetPlayers()[0].GetQuestManager().GetStoryMaker().GetPlayerListRequest().Count; i++)
                                {
                                    if (RPGRefrence.GetPlayers()[0].GetQuestManager().GetStoryMaker().GetPlayerListRequest()[i].IsTimeOut())
                                    {
                                        RPGRefrence.GetPlayers()[0].GetQuestManager().GetStoryMaker().GetPlayerListRequest().Remove(RPGRefrence.GetPlayers()[0].GetQuestManager().GetStoryMaker().GetPlayerListRequest()[i]);
                                    }
                                }
                            }
                        }
                        if(RPGRefrence.GetPlayers()[0].GetQuestManager().GetStoryMaker().GetPlayerBattleControler() != null)
                        {
                            if(RPGRefrence.GetPlayers()[0].GetQuestManager().GetStoryMaker().GetPlayerBattleControler().Count != 0)
                            {
                                for(int i = 0; i < RPGRefrence.GetPlayers()[0].GetQuestManager().GetStoryMaker().GetPlayerBattleControler().Count; i++)
                                {
                                    RPGRefrence.GetPlayers()[0].GetQuestManager().GetStoryMaker().GetPlayerBattleControler()[i].IsTimeOut();
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
