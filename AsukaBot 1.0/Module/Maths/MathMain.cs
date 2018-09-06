using Discord.Commands;
using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsukaBot_1._0.Module.Maths
{
    public class MathMain : ModuleBase<SocketCommandContext>
    {
        [Command("cal")]
        public async Task Calculate([Remainder]string content)
        {

        }
    }
}