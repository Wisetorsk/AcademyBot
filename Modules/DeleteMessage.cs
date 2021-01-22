using Discord;
using Discord.Commands;
using System.Threading.Tasks;

namespace AcademyBot.Modules
{
    public class DeleteMessage : ModuleBase<SocketCommandContext>
    {
        [Command("delmsg"), RequireUserPermission(GuildPermission.Administrator)]
        [Alias("DeleteMessage", "DELMSG")]
        [Summary("Deletes a message with the given id")]
        public async Task EchoAsync([Remainder] string remainder)
        {
            var arguments = remainder.Split(' ');
            bool outcome = ulong.TryParse(remainder, out ulong msgId);


            if (!outcome)
            {
                var mod = remainder.Split(' ')[0];
                switch (mod)
                {
                    case "all":
                        /*
                        Console.WriteLine("Deleting all");
                        var messages = Context.Channel.GetMessagesAsync();
                        foreach (var message in messages)
                        {
                            var editMsg = message as SocketUserMessage;
                            await Context.Channel.DeleteMessageAsync(editMsg.Id);
                        }*/
                        break;
                    default:
                        break;
                }
            }
            else
            {
                string response = $"Trying to remove message with id: '{msgId}'";
                await Context.Channel.DeleteMessageAsync(msgId);
                await Context.Guild.GetTextChannel(800061983973179412).SendMessageAsync(response);
            }

        }
    }
}
