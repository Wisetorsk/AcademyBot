using Discord;
using Discord.WebSocket;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AcademyBot.Objects
{
    class Messenger
    {

        public DiscordSocketClient Client { get; private set; }
        public IDictionary<ulong, SocketTextChannel> Channels;

        public Messenger(DiscordSocketClient client, ulong guildID, params ulong[] channelIDs)
        {
            Client = client;
            Channels = new Dictionary<ulong, SocketTextChannel>();
            foreach (var channel in channelIDs)
            {
                Channels.Add(channel, Client.GetGuild(guildID).GetTextChannel(channel));
            }
        }

        public async Task SendAsync(ulong channel, string content)
        {
            await Channels[channel].SendMessageAsync(content);
        }

        public async Task SendEmbedAsync(ulong channel, EmbedBuilder embed)
        {
            await Channels[channel].SendMessageAsync("", false, embed.Build());
        }

        public async Task SendEmbedAsync(
            ulong channel,
            string title,
            Dictionary<string, string>[] fields)
        {
            EmbedBuilder embed = new EmbedBuilder();
            embed.WithTitle(title);
            foreach (var pair in fields)
            {
                embed.AddField(pair["Title"], pair["Content"]);
            }
            await Channels[channel].SendMessageAsync("", false, embed.Build());
        }

        public static Dictionary<string, string> MakeFieldDict(string title, string content)
        {
            return new Dictionary<string, string>() { { "Title", title }, { "Content", content } };
        }

        public void SendDirectMessage(ulong id, string text)
        {
            Client.GetUser(id).SendMessageAsync(text);
        }

    }
}
