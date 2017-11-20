using System;
using System.Threading.Tasks;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Discord.Commands;
using System.Reflection;
using Discord;

namespace AsukaBot_1._0
{
    class Program
    {
        static void Main(string[] args) => new Program().Start().GetAwaiter().GetResult();

        private DiscordSocketClient client;
        private CommandService command;
        private IServiceProvider service;

        public async Task Start()
        {
            client = new DiscordSocketClient();
            command = new CommandService();
            service = new ServiceCollection().AddSingleton(client).AddSingleton(command).BuildServiceProvider();
            string botToken = "Mjg1Mzc2NDM1NTU0ODc3NDQx.C5RVig.DSje5ToZYsU7-8jh4HunqG8I4KY";

            //event subcribsion
            client.Log += Log;
            client.UserJoined += AnnouceUserJoined;
            await RegisterCommandAsync();
            await client.LoginAsync(Discord.TokenType.Bot, botToken);
            await client.StartAsync();
            await Task.Delay(-1);
        }

        private async Task AnnouceUserJoined(SocketGuildUser user)
        {
            SocketGuild guild = user.Guild;
            SocketTextChannel channel = guild.DefaultChannel;
            await channel.SendMessageAsync($"Welcome, {user.Mention}");
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
            if(message == null || message.Author.IsBot)
            {
                return;
            }
            int argPos = 0;
            if(message.HasStringPrefix("!", ref argPos) || message.HasMentionPrefix(client.CurrentUser, ref argPos))
            {
                SocketCommandContext context = new SocketCommandContext(client, message);
                IResult result = await command.ExecuteAsync(context, argPos, service);
                if(!result.IsSuccess)
                {
                    Console.WriteLine(result.ErrorReason);
                }
            }
        }
    }
}
