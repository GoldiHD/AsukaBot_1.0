using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using Discord;

namespace AsukaBot_1._0.Module.StandardCommands
{
    public class StandardCommandModule : ModuleBase<SocketCommandContext>
    {
        Random random = new Random();
        string[] pics;
        string[] qoutes;
        string[] smugs;


        [Command("purge")]
        public async Task Purge()
        {
            EmbedBuilder builder = new EmbedBuilder();
            builder.WithTitle("Purge!!").WithDescription("Let the purge begin").WithColor(Color.Blue);
            await ReplyAsync("", false, builder.Build());

        }
    }
}
