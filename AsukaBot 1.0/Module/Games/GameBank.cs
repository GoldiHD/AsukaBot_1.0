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
        private static List<BankAcountProfile> AllBankAccounts = new List<BankAcountProfile>();

        [Command("$createaccount")]
        public async Task CreateAcount()
        {
            if (FindUserInAccountList(Context.Message.Author.Username) == -1)
            {
                AllBankAccounts.Add(new BankAcountProfile(Context.Message.Author.Username, 200));
                await ReplyAsync("Account " + Context.Message.Author.Username + " been created");
            }
            else
            {
                await ReplyAsync("[ERROR] you already have an account");
            }
        }

        [Command("$GetBalance")]
        public async Task GetBalance()
        {
            int ListNumber = FindUserInAccountList(Context.Message.Author.Username);
            if (ListNumber != -1)
            {
                await ReplyAsync(Context.Message.Author.Username + ": " + AllBankAccounts[ListNumber].GetCurrentAmountOfMoney() + "$");
            }
            else
            {
                await ReplyAsync("Account does'n exist");
            }
        }

        [Command("$SendMoney")]
        public async Task SendMoney(int money, [Remainder]string username)
        {
            int listnumber = FindUserInAccountList(Context.Message.Author.Username);
            int OtherUser = FindUserInAccountList(username);

            if (listnumber != -1 && OtherUser != -1)
            {
                if (AllBankAccounts[listnumber].Drawmoney(money))
                {
                    AllBankAccounts[OtherUser].Depositmoney(money);
                    await ReplyAsync(money + " send from " + Context.Message.Author.Username + " to " + username);
                }
                else
                {
                    await ReplyAsync(Context.Message.Author.Username + " don't have enought money to send");
                }
            }
            else
            {
                await ReplyAsync("[ERROR]");
            }
        }

        private int FindUserInAccountList(string username)
        {
            for (int i = 0; i < AllBankAccounts.Count; i++)
            {
                if (AllBankAccounts[i].GetUsername() == username)
                {
                    return i;
                }
            }
            return -1;
        }

        public List<BankAcountProfile> GetBankAccounts()
        {
            return AllBankAccounts;
        }

        [Command("$save")]
        public async Task Tempsave()
        {
            new SaveDataToTxt().SaveData(SaveMode.Bank);
        }
    }

    public class BankAcountProfile
    {
        private string userName;
        private int moneh;

        public BankAcountProfile(string username, int startingamount)
        {
            userName = username;
            moneh = startingamount;
        }

        public bool Drawmoney(int moneytodraw)
        {
            if (moneh >= moneytodraw)
            {
                moneh -= moneytodraw;
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Depositmoney(int monenytodeposit)
        {
            moneh += monenytodeposit;
        }

        public string GetUsername()
        {
            return userName;
        }

        public int GetCurrentAmountOfMoney()
        {
            return moneh;
        }

    }
}
