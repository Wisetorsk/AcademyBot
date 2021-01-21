using Discord;
using Discord.Commands;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Timers;

namespace AcademyBot.Modules
{
    public class Shutdown : ModuleBase<SocketCommandContext>
    {
        [Command("shutdown"), RequireUserPermission(GuildPermission.Administrator)]
        [Alias("SHUTDOWN")]
        [Summary("Shuts down the bot application")]
        public async Task ShutdownAsync([Remainder] string remainder)
        {
            await Context.Message.AddReactionAsync(new Emoji("😭"));
            string[] arguments = remainder.Split(" ");
            string modifyer = arguments[0].ToLower();
            bool resultArg2 = Int32.TryParse(arguments[1], out int timeToExit);
            switch (modifyer)
            {
                case "now":
                    _ = Context.Client.LogoutAsync();
                    var process = Process.GetCurrentProcess();
                    process.Kill();
                    break;
                case "time":
                    if (!resultArg2) break;
                    await Context.Channel.SendMessageAsync($"Exiting application in {timeToExit} seconds");
                    var exitTimer = new Timer(timeToExit * 1000)
                    {
                        AutoReset = false,
                        Enabled = true
                    };
                    exitTimer.Elapsed += DshutdownAsync;
                    break;
                default:
                    await Context.Channel.SendMessageAsync("Unable to parse modifyer. Use pattern '!shutdown [mod] [time]'");
                    break;
            }
            //
        }

        private void DshutdownAsync(object sender, ElapsedEventArgs e)
        {
            _ = Context.Client.LogoutAsync();
            var process = Process.GetCurrentProcess();
            process.Kill();
        }
    }
}
