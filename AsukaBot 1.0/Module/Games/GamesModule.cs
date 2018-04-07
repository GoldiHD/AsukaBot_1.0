using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;

namespace AsukaBot_1._0.Module.Games
{
    public class GamesModule : ModuleBase<SocketCommandContext>
    {
        private Random RNG = new Random();

        [Command("RPS")]
        public async Task RockPaperScissors(string choice)
        {
            if (choice.ToLower() == "rock" || choice.ToLower() == "paper" || choice.ToLower() == "scissor")
            {
                await Context.Channel.SendMessageAsync(RPB(Context.User.Username, choice.ToLower()));
            }
            else
            {
                await Context.Channel.SendMessageAsync("That isn't how you play rock, paper, scissor");
            }
        }

        private string RPB(string Sender, string SenderChoose)
        {
            string output;
            string AsukaBot;
            string Winner = "";

            string[] RPBArray = new string[3] { "rock", "paper", "scissor" };
            AsukaBot = RPBArray[RNG.Next(0, RPBArray.Length)];
            switch (SenderChoose)
            {
                case "rock":
                    if (AsukaBot == "rock")
                    {
                        Winner = "It was a draw, nobody";
                    }
                    else if (AsukaBot == "paper")
                    {
                        Winner = "Asuka";
                    }
                    else if (AsukaBot == "scissor")
                    {
                        Winner = Sender;
                    }
                    break;

                case "paper":
                    if (AsukaBot == "rock")
                    {
                        Winner = Sender;
                    }
                    else if (AsukaBot == "paper")
                    {
                        Winner = "It was a draw, nobody";
                    }
                    else if (AsukaBot == "scissor")
                    {
                        Winner = "Asuka";
                    }
                    break;

                case "scissor":
                    if (AsukaBot == "rock")
                    {
                        Winner = "Asuka";
                    }
                    else if (AsukaBot == "paper")
                    {
                        Winner = Sender;
                    }
                    else if (AsukaBot == "scissor")
                    {
                        Winner = "It was a draw, nobody";
                    }
                    break;

                default:
                    Winner = "Error";
                    break;
            }
            output = "Asuka bot got " + AsukaBot + ", " + Sender + " got " + SenderChoose + ", so " + Winner + " won";
            return output;
        }

        [Command("rr")]
        public async Task RussianRoulette()
        {
            await ReplyAsync(RusB());
        }

        [Command("rouettle")]
        public async Task RR()
        {
            await ReplyAsync(RusB());
        }

        private string RusB()
        {
            bool[] Chamber = new bool[6] { true, false, false, false, false, false };

            if (Chamber[RNG.Next(0, Chamber.Length)])
            {
                return ("You died faggot");
            }
            else
            {
                return ("You survived for now");
            }
        }

    }
}
