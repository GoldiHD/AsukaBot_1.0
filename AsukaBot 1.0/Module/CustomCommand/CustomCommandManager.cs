using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;

namespace AsukaBot_1._0.Module.CustomCommand 
{
    public class CustomCommandManager : ModuleBase<SocketCommandContext>
    {
        private static List<CommandContainer> CustomCommandList;

        public CustomCommandManager()
        {
            if(CustomCommandList == null )
            {
                CustomCommandList = new List<CommandContainer>();
            }
        }


        //gonna need a little know how to use
        [Command("-create")]
        public async Task CreateCommand(int type, [Remainder] string input)
        {
            CommandContainer TempContainer = new CommandContainer();
            string[] CommandSplit = input.Split();
            switch(type)
            {
                case 0: //Respone
                    if(CheckIfCommandNameAlreadyExists(CommandSplit[0]))
                    {
                        await ReplyAsync("Sorry this command already exists");
                    } 
                    else
                    {
                        TempContainer.author = Context.User.Username;
                        TempContainer.commandName = CommandSplit[0];
                        CommandSplit[0] = "";
                        TempContainer.Info = String.Join(" ", CommandSplit);
                        CustomCommandList.Add(TempContainer);
                        await ReplyAsync("Command " + TempContainer.commandName + " have now been created.");
                    }
                    break;

                case 1: //Link
                    
                    break;

                case 2: //Manage

                    break; 
            }
        }

        private bool CheckIfCommandNameAlreadyExists(string name)
        {
            bool Exists = false;
            for(int i = 0; i < CustomCommandList.Count; i++)
            {
                if(CustomCommandList[i].commandName.ToLower() == name.ToLower())
                {
                    Exists = true;
                }
            }

            return Exists;
        }

        [Command("-")]
        public async Task AllCustomCommands([Remainder] string command)
        {

        }
    }

    public enum CommandTypes
    {
        Respone, Link, Manage
    }

    public class CommandContainer
    {
        public string commandName;
        public CommandTypes myType;
        public string Info;
        public string Describtion;
        public string author;
        public List<string> parametersList;



    }
}
