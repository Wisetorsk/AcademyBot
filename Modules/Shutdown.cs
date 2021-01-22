using Discord;
using Discord.Commands;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
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
            string[] arguments = remainder.Split(" ");
            string modifyer = arguments[0].ToLower();
            bool resultArg2 = Int32.TryParse(arguments[1], out int timeToExit);
            switch (modifyer)
            {
                case "now":
                    await Context.Message.AddReactionAsync(new Emoji("😭"));
                    Console.WriteLine("Shutting down!");
                    await Context.Client.LogoutAsync();
                    var process = Process.GetCurrentProcess();
                    process.Kill();
                    break;
                case "time":
                    if (!resultArg2) break;
                    await Context.Message.AddReactionAsync(new Emoji("😭"));
                    Console.WriteLine($"Shutting down in {timeToExit} seconds");
                    await Context.Channel.SendMessageAsync($"Exiting application in {timeToExit} seconds");
                    _ = Context.Client.LogoutAsync(); //Dirty hack. I am ashamed
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
        }

        private void DshutdownAsync(object sender, ElapsedEventArgs e) // i know it's no longer async...
        {
            var process = Process.GetCurrentProcess();
            process.Kill();
        }

    }
}
