using Discord;
using Discord.Commands;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcademyBot.Modules
{
    public class ListUsers : ModuleBase<SocketCommandContext>
    {
        [Command("listUsers"), RequireUserPermission(GuildPermission.Administrator)]
        [Alias("lstusr", "Listusers", "listusers", "ListUsers", "LISTUSERS")]
        [Summary("Gets a list of all users currently registered in server")]

        public async Task ListUsersAsync(string remainder)
        {
            Program.ReloadUsers();
            string usersString = "Registered Users: \n";
            foreach (var user in Program.LoadedUsers)
            {
                usersString += $"ID: {user.Id}\tUsername: {user.Username}\n";
            }
            await Context.Channel.SendMessageAsync(usersString);
        }

    }
}
