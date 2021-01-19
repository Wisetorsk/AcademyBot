using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AcademyBot
{
    public class CommandHandler
    {
        private readonly DiscordSocketClient _client;
        private readonly CommandService _commands;

        public CommandHandler(DiscordSocketClient client, CommandService commands)
        {
            _commands = commands;
            _client = client;
            InstallingCommands();
        }

        private void InstallingCommands()
        {
            _commands.AddModulesAsync(assembly: Assembly.GetEntryAssembly(), services: null);

            _client.MessageReceived += HandleCommandAsync;
        }

        private async Task HandleCommandAsync(SocketMessage messageParam)
        {
            Program.SendDebugMsg();
            //Console.WriteLine("got something");
            var message = messageParam as SocketUserMessage;
            if (message == null) return;

            int argPos = 0;

            // Determine if the message has the selected prefix or the sender is a bot
            if (!(message.HasCharPrefix('!', ref argPos) ||
                message.HasMentionPrefix(_client.CurrentUser, ref argPos)) ||
                message.Author.IsBot)
                return; //breaks out

            var context = new SocketCommandContext(_client, message);

            var result = await _commands.ExecuteAsync(
            context: context,
            argPos: argPos,
            services: null);
            if (!result.IsSuccess && result.Error != CommandError.UnknownCommand)
            {
                await context.Channel.SendMessageAsync(result.ErrorReason);
            }
        }
    }

}
