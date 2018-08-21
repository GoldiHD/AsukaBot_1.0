using AsukaBot_1._0.Module.RPG.Bank;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsukaBot_1._0.Module.RPG.Logic.Guilds
{
    public class GuildManager
    {
        public static List<Guild> guilds;

        public GuildManager()
        {
            if(guilds == null)
            {
                guilds = new List<Guild>();
            }
        }

        public bool CreateGuild(string guildname, Player creator)
        {
            if (creator.MyGuild == null)
            {
                for (int i = 0; i < guilds.Count; i++)
                {
                    if (guilds[i].guildName == guildname)
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }
            return true;
        }
    }

    public class Guild
    {
        private RPGBankAccount guildBank;
        public Player Leader;
        public string guildName; 
        public bool InviteOnly = false;
        public List<GuildMemebers> guildMemebers = new List<GuildMemebers>();

        public Guild(string guildname, Player usercreator)
        {
            guildName = guildname;
            Leader = usercreator;
        }

        public void SetLeader(Player leader)
        {
            Leader = leader;
        }

        public void DonateToGuild(int amount, Player user)
        {
            guildBank.AddToBalance(amount, user);
        }

        public int GetguildRiches()
        {
            return guildBank.ViewAccount();
        }

    }

    public class GuiildUpGradeModule
    {

    }

    public class GuildMemebers
    {
        public Player User;
        GuildRank UserRank;

        public GuildMemebers(Player user, GuildRank startrank)
        {
            User = user;
            UserRank = startrank;
        }
    }

    public enum GuildRank
    {
        Recruit, Member, Veteran, GuildLeader
    }
}
