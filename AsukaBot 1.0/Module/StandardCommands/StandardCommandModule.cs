using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using Discord;
using System.IO;

namespace AsukaBot_1._0.Module.StandardCommands
{
    public class StandardCommandModule : ModuleBase<SocketCommandContext>
    {
        Random random = new Random();
        string[] pics;
        string[] qoutes;
        string[] smugs;
        Random rng = new Random();

        StandardCommandModule()
        {
            SetDataForArrays();
        }

        private void SetDataForArrays()
        {
            qoutes = new string[]
            {
                "Would fuck that ass : spark",
                "Hentai for senpai : Goldi",
                "Gal I Done **HAD** **IT**. \nYou shit on **ME??**.\nYou **LAUGH?**.\nYou Think it's **FUNNY??**\nYou gonna try and mock me, in front of my herd??\nIn front of **MY FAMILY!?!**\n \nDarbi"
            };
            pics = loadFilesFromLocation(@"assets\pic");
            smugs = loadFilesFromLocation(@"assets\smugs");
        }

        private string[] loadFilesFromLocation(string path)
        {
            string[] TempHolder;
            if (Directory.Exists(path))
            {
                TempHolder = Directory.GetFiles(path);
                return TempHolder;
            }
            else
            {
                Console.WriteLine("Can't find or doesn't have access to " + path);
                return TempHolder = new string[1] { "" }; //picture error process
            }
        }

        [Command("purge")]
        public async Task Purge(int amount = 1)
        {
            
            try
            {
                Console.WriteLine("Purge comited by " + Context.User.Mention + " wiped " + amount + " message(s)");
                var messageToDelete = await Context.Channel.GetMessagesAsync(amount + 1).Flatten();
                await Context.Channel.DeleteMessagesAsync(messageToDelete);
            }
            catch
            {
                await Context.Channel.SendMessageAsync("the message can't be over 2 weeks old or delete more then 99 messages");
            }
            await ReplyAsync("Purging");
        }

        [Command("smug")]
        public async Task smug()
        {
            int i = rng.Next(0, smugs.Length);
            await Context.Channel.SendFileAsync(smugs[i]);
        }

        [Command("pic")]
        public async Task PostPicture()
        {
            int i = rng.Next(0, pics.Length);
            await Context.Channel.SendFileAsync(pics[i]);
        }

        [Command("qoute")]
        public async Task PostQoutes()
        {
            int i = rng.Next(0, qoutes.Length);
            await Context.Channel.SendFileAsync(qoutes[i]);
        }

        [Command("bestgrill")]
        public async Task PostBestGrill()
        {
            await Context.Channel.SendMessageAsync("I'm best gril");
            await Context.Channel.SendFileAsync(@"assets\smugofbestgrill.png");
        }

        [Command("come over kazuma")]
        public async Task PostKazuma()
        {
            await Context.Channel.SendMessageAsync("Kazuma-desu");
        }

        [Command("help")]
        public async Task Help(string data = "empthy")
        {
            switch (data)
            {
                case "empthy":
                    await Context.Channel.SendMessageAsync("!Shutdown, !Help (Module)");
                    break;

                case "game":
                    await Context.Channel.SendMessageAsync("!RPS (Rock, Paper Scissor)");
                    break;

                case "bank":
                    await Context.Channel.SendMessageAsync("!$Withdraw, !$Transfer, !$Suck dick, !$CheckAccount, !$CreateAccount");
                    break;

                case "standard":
                    await Context.Channel.SendMessageAsync("!Pic, !Qoute, !Purge, !Bestgrill, !Smug");
                    break;

                case "dnd":
                    await Context.Channel.SendMessageAsync("!#Settings \n!#SetSettings(Rate)(Should Xp rate Follow Lvl Rate) \n!#CreateCreature(name)(HP)(CR)(Str)(Dex)(Con)(Int)(Wis)(Cha)(XpGain) \n!#ShowAllCreatures\n!#GetCreature(Name)(LVL) \n!#WipeCreature(name)");
                    break;

                case "module":
                    await Context.Channel.SendMessageAsync("Standard, DND, Bank, Game, RPG, Music");
                    break;

                case "rpg":
                    await Context.Channel.SendMessageAsync("!_inv, !_itemwiki(this is only here for temp work), !_itemlist, !_Iteminfo(item name), !_equip, !_encounter, !_escape, !_attack, !_combat, !_stats, !_status, !_power(stat point), !_pow(stat point), !_magic(stat point), !_mag(stat point), !_dexterity(stat point), !_dex(stat point), !_intellegenc(stat point), !_int(stat point), !_vitallity(stat point), !_vit(stat point), !_luck(stat point)");
                    break;

                case "music":
                    await Context.Channel.SendMessageAsync("!#Play, !#Stop, !#Playlist, !#Vol");
                    break;
            }
        }


        [Command("roadmap")]
        public async Task Roadmap()
        {
            await Context.Channel.SendMessageAsync("https://trello.com/b/M4UcwzI1/discordbot-asukabot");
        }

        [Command("shutdown")]
        public async Task Shutdown()
        {
            Console.WriteLine("want to accept shutdown? yes/no");
            string consoleRespone = Console.ReadLine();
            if (consoleRespone == "y" || consoleRespone == "yes")
            {
                await Context.Channel.SendMessageAsync("Bye");
                //disconnect
                Environment.Exit(0);
            }
            else
            {
                await Context.Channel.SendMessageAsync("I'm gonna shoot who ever tried to shut me down");
                await Context.Channel.SendMessageAsync(@"assets\sys\failedToclose.gif");
            }
        }

        [Command("purge")]
        public async Task PerformPurge(int amount)
        {
            IEnumerable<IMessage> messageToPurge= await Context.Channel.GetMessagesAsync(amount + 1).Flatten();
            try
            {
                if (amount <= 100 || amount >= 0)
                {
                    await Context.Channel.DeleteMessagesAsync(messageToPurge);
                }
                else
                {
                    await ReplyAsync("discord does not allow deleting over 100 messages at a time");
                }
            }
            catch
            {
                await ReplyAsync("the messages can't be older then 2 weeks");
            }

        }

    }
}

//context


//[Command("purge")]
//public async Task Purge()
//{
//    EmbedBuilder builder = new EmbedBuilder();
//    builder.WithTitle("Purge!!").WithDescription("Let the purge begin").WithColor(Color.Blue);
//    await ReplyAsync("", false, builder.Build());

//}
//[Command("come over kazuma")]
//public async Task Kazuma()
//{
//    EmbedBuilder build = new EmbedBuilder();
//    build.AddField("Kazuma", "desu").AddInlineField("Kazuma", "desu");
//    await ReplyAsync("", false, build.Build());
//}      