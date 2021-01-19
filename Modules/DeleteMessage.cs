using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AcademyBot.Modules
{
    public class DeleteMessage : ModuleBase<SocketCommandContext>
    {
        [Command("delmsg"), RequireUserPermission(GuildPermission.SendMessages)]
        [Alias("DeleteMessage", "DELMSG")]
        [Summary("Deletes a message with the given id")]
        public async Task EchoAsync([Remainder] string remainder)
        {
            bool outcome = ulong.TryParse(remainder, out ulong msgId);
            string response = outcome ? $"Trying to remove message with id: '{msgId}'" : "Could not parse message from command";
            if (outcome) await Context.Channel.DeleteMessageAsync(msgId);
            await Context.Channel.SendMessageAsync(response);
        }
    }
}
