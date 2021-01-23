using Discord;
using Discord.Commands;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Timers;

namespace AcademyBot.Modules
{
    public class Update : ModuleBase<SocketCommandContext>
    {
        [Command("update"), RequireUserPermission(GuildPermission.Administrator)]
        [Alias("UPDATE")]
        [Summary("Updates and manages the application")]
        public async Task ShutdownAsync([Remainder] string remainder)
        {
            switch (remainder)
            {
                case "pull":
                    await Context.Channel.SendMessageAsync("Updater started! Pulling from source");
                    Program.UpdateModule.Pull();
                    break;

                case "build":
                    await Context.Channel.SendMessageAsync("Updater building!");
                    Program.UpdateModule.Build();
                    break;
                default:
                    break;
            }
            
        }
    }
}
