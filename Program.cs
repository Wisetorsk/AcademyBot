using AcademyBot.Objects;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace AcademyBot
{
    class Program
    {
        private bool saveLog;
        private bool debug;
        private JObject IDs = JObject.Parse(File.ReadAllText(@"../../../id.json"));

        #region serverIDs
        private readonly ulong serverID = 694326574601994350;
        #endregion

        #region channelIDs
        private readonly ulong generalTextID = 694326574601994353;
        private readonly ulong botChannelID = 800061983973179412;
        private readonly ulong botErrorChannelID = 800066416518365206;
        #endregion

        #region guildObjects
        public static SocketGuild server;

        #endregion

        #region channelObjects // Can be replaced with Messenger object
        public static SocketTextChannel generalTextChannel;
        public static SocketTextChannel botChannel;
        public static SocketTextChannel botErrorChannel;
        #endregion

        private DiscordSocketClient client;
        private CommandService commands;

        private CommandHandler handler;

        //private Messenger messageService;


        #region Properties

        public Messenger MessageService { get; private set; }

        #endregion

        public static void Main(string[] args)
        {
            new Program().MainAsync().GetAwaiter().GetResult();
        }

        public async Task MainAsync()
        {
            SetDebugLevel(); //Removable


            client = new DiscordSocketClient();
            commands = new CommandService();
            handler = new CommandHandler(client, commands);

            client.Log += Log; //Subscribe to the Logging method
            client.Ready += ReadyAsync; // Channel and server client objects can only be loaded after the bot has completed startup!!!

            var token = File.ReadAllText("../../../../token.txt");

            await client.LoginAsync(TokenType.Bot, token);
            await client.StartAsync();
            await client.SetStatusAsync(UserStatus.Online);

            await Task.Delay(-1);

        }

        private async Task<Task> ReadyAsync() // Loads Objects after the bot is done initializing
        {
            server = client.GetGuild(serverID);
            MessageService = new Messenger(client, serverID, generalTextID, botChannelID, botErrorChannelID);
            await MessageService.SendAsync(generalTextID, "Bot is now up and running! Ask me something by tagging or using '![command]'");
            EmbedBuilder builder = new EmbedBuilder();
            builder.WithTitle("Just a title for the testmessage");
            builder.AddField("This is a Field: ", "Another part of it", true);
            builder.AddField("This is another Field: ", "Another part of that", false);
            builder.WithThumbnailUrl("https://upload.wikimedia.org/wikipedia/commons/thumb/e/e6/Noto_Emoji_KitKat_263a.svg/200px-Noto_Emoji_KitKat_263a.svg.png");
            builder.WithColor(Color.Blue);
            await MessageService.SendEmbedAsync(botChannelID, builder);
            return Task.CompletedTask;
        }

        private void SetDebugLevel()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Debug mode? (y/n) [Default 'y']: ");
            var response = Console.ReadLine();
            debug = string.IsNullOrEmpty(response) || response.Contains('y') || response.Contains('Y');

            Console.Write("Log to file? (NOT YET IMPLEMENTED!)  (y/n) [Default 'y']: ");
            response = Console.ReadLine();
            saveLog = string.IsNullOrEmpty(response) || response.Contains('y') || response.Contains('Y');

            Console.WriteLine(new String('-', Console.WindowWidth));
            Console.WriteLine(saveLog ? "Extended log ON" : "Extended log OFF");
            Console.WriteLine(debug ? "Debug mode ON" : "Debug mode OFF");
            Console.WriteLine(new String('-', Console.WindowWidth));
            Console.ResetColor();
            Console.WriteLine();
        }

        private static void CompileAndUpdate()
        {
            /*
             Starts a companion program that waits for main program to exit, or force quits it, then pulls the most recent build from github, compiles it and re-launches the bot. 
            Could be a batch file or shell script
             */
        }

        public static void SendDebugMsg(string message = "Triggered", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            /*
             Function prints to the console what method has been activated and what line in program.cs it exitsts
             */
            var time = DateTime.Now;
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"[{time.ToLocalTime()}]\t{message} at line {lineNumber} ({caller})");
            Console.ResetColor();
            
        }

        private Task Log(LogMessage msg)
        {
            if (saveLog)
            {
                //Write some shit to file
                
            }
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
    }
}
