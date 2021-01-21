using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace AcademyBot.Modules
{
    public class Echo : ModuleBase<SocketCommandContext>
    {
        [Command("echo"), RequireUserPermission(GuildPermission.ManageChannels)]
        [Alias("Echo", "ECHO")]
        [Summary("")]
        public async Task EchoAsync([Remainder] string remainder)
        {
            await Context.Channel.SendMessageAsync(remainder);
        }
    }
}