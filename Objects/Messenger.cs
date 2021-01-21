using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;
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
    }
}
