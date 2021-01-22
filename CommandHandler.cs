using Discord.Commands;
using Discord.WebSocket;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace AcademyBot
{
    public class CommandHandler
    {
        private readonly DiscordSocketClient _client;
        private readonly CommandService _commands;
        private readonly JObject IDs = JObject.Parse(File.ReadAllText(@"../../../json/id.json"));
        private readonly ulong errorChannel;

        public CommandHandler(DiscordSocketClient client, CommandService commands)
        {
            //var botChannelID = ulong.Parse(IDs["TextChannels"]["Bot"].ToString());
            errorChannel = ulong.Parse(IDs["TextChannels"]["Bot_errors"].ToString());
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
            //Program.SendDebugMsg();
            var message = messageParam as SocketUserMessage;
            if (message == null) return;
            int argPos = 0;

            // Determine if the message has the selected prefix or the sender is a bot
            if (!(message.HasCharPrefix('!', ref argPos) ||
                message.HasMentionPrefix(_client.CurrentUser, ref argPos)) ||
                message.Author.IsBot)
                return; //breaks out

            // Deletes the invoking message 
            var context = new SocketCommandContext(_client, message);
            await context.Channel.DeleteMessageAsync(context.Message.Id);

            var result = await _commands.ExecuteAsync(
            context: context,
            argPos: argPos,
            services: null);
            if (!result.IsSuccess && result.Error != CommandError.UnknownCommand)
            {
                //await context.Channel.SendMessageAsync(result.ErrorReason);
                await Program.MessageService.SendAsync(errorChannel, result.ErrorReason);
            }
        }

    }

}
