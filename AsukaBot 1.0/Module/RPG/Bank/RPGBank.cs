using AsukaBot_1._0.Module.RPG.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsukaBot_1._0.Module.RPG.Bank
{
    class RPGBank
    {
        private static List<RPGBankAccount> accounts;

        public RPGBank()
        {
            if(accounts == null)
            {
                accounts = new List<RPGBankAccount>();
            }
        }

        public void CreateAccount()
        {

        }
    }

    class RPGBankAccount
    {
        if()
        private int CurrentBalance = 0;

        public bool AddToBalance(int amount, Player user)
        {
            if (user.GetInventory().GetGold() > amount)
            {
                CurrentBalance += amount;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Withdraw(int amount)
        {
            if(amount <= CurrentBalance)
            {
                CurrentBalance -= amount;
                return true;
            }
            else
            {
                return false;
            }
        }

        public int ViewAccount()
        {
            return CurrentBalance;
        }
    }


}
