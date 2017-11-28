using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using AsukaBot_1._0.Classes;
using AsukaBot_1._0.Core;

namespace AsukaBot_1._0.Module.Games
{
    public class GameBank : ModuleBase<SocketCommandContext>
    {
        private SaveDataToTxt SaveLoad = new SaveDataToTxt();


        [Command("$createaccount")]
        public async Task CreateAcount()
        {
            SaveLoad.LoadBankData(ModuleType.Bank);

            await Context.Channel.SendMessageAsync("Account have been created");
        }
        [Command("$GetBalance")]
        public async Task GetBalance()
        {
            List<string> name;
            List<string> balance;
            List<string>[] MainTemp = SaveLoad.LoadBankData(ModuleType.Bank);
            name = MainTemp[0];
            balance = MainTemp[1];
            for (int i = 0; i < name.Count; i++)
            {
                if (name[i].Equals(Context.User.Username))
                {
                    await Context.Channel.SendMessageAsync(Context.User.Username + ": " + balance[i]);
                }
            }
        }
    }
}
