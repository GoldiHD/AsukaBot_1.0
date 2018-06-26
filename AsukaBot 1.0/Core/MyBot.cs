using System;
using System.Threading.Tasks;
using Discord.WebSocket;
using Discord.Commands;
using System.Reflection;
using Discord;
using AsukaBot_1._0.Classes;
using System.IO;

namespace AsukaBot_1._0.Core
{
    class MyBot
    {
        private string botToken;
        public static DiscordSocketClient client;
        private CommandService command;
        private IServiceProvider service;
        private Random RNG = new Random();

        public async Task Start()
        {
            Console.WriteLine("Created by Lars H M/Goldi");
            //if(File.Exists(Directory.GetCurrentDirectory() + "//assets"))
            if (File.Exists(Directory.GetCurrentDirectory() + "//assets//credentials.txt"))
            {

                client = new DiscordSocketClient();
                command = new CommandService();

                botToken = File.ReadAllText(Directory.GetCurrentDirectory() + "//assets//credentials.txt").Remove(0, 15);
                SingleTon.GetConsoleCheckerInstance().StartUp();
                SingleTon.GetRPGThread().StartUp();
                SingleTon.SetClient(client);
                //event subcribsion
                client.Log += Log;
                client.UserJoined += AnnouceUserJoined;
                await ChangeStatus();
                await RegisterCommandAsync();
                await client.LoginAsync(TokenType.Bot, botToken);
                await client.StartAsync();
                await Task.Delay(-1);
            }
            else
            {
                Console.WriteLine("no credentials file could be found at: " + Directory.GetCurrentDirectory() + "//assets//credentials.txt \nso one have been created");
                File.WriteAllText(Directory.GetCurrentDirectory() + "//assets//credentials.txt", "Discord token: ");
                Console.ReadKey();
            }
        }

        private async Task AnnouceUserJoined(SocketGuildUser user)
        {
            SocketGuild guild = user.Guild;
            SocketTextChannel channel = guild.DefaultChannel;
            await channel.SendMessageAsync($"Welcome, {user.Mention}");
        }

        public async Task ChangeStatus()
        {
            string[] GameStatues = new string[] { "Working on code with senpai", "Deleting code", "Selling user data to FBI", "Creating virus", "9/11 was an inside job", "The earth is flat", "Leading the communist party"};
            await client.SetGameAsync(GameStatues[RNG.Next(0, GameStatues.Length)]);
        }

        private Task Log(LogMessage arg)
        {
            Console.WriteLine(arg);
            return Task.CompletedTask;
        }

        public async Task RegisterCommandAsync()
        {
            client.MessageReceived += HandleCommandAsync;
            await command.AddModulesAsync(Assembly.GetEntryAssembly());
        }

        private async Task HandleCommandAsync(SocketMessage arg)
        {
            SocketUserMessage message = arg as SocketUserMessage;
            if (message == null || message.Author.IsBot)
            {
                return;
            }
            int argPos = 0;
            if (message.HasStringPrefix("!", ref argPos) || message.HasMentionPrefix(client.CurrentUser, ref argPos))
            {
                SocketCommandContext context = new SocketCommandContext(client, message);
                IResult result = await command.ExecuteAsync(context, argPos, service);
                if (!result.IsSuccess)
                {
                    Console.WriteLine(result.ErrorReason);
                }
            }
        }
    }
    public enum ModuleType
    {
        Bank, Music, Standard, RPG, DnD
    }
}
