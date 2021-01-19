using System;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Newtonsoft.Json.Linq;

namespace AcademyBot
{
    class Program
    {
        private bool saveLog;
        private bool debug;
        private JObject IDs = JObject.Parse(File.ReadAllText(@"../../../id.json"));

        #region serverIDs
        private readonly ulong _ServerID = 694326574601994350;
        #endregion

        #region channelIDs
        private readonly ulong _GeneralTextID = 694326574601994353;
        //private readonly ulong _BotChannelID = 800061983973179412;
        private readonly ulong _BotErrorChannelID = 800066416518365206;
        #endregion

        #region guildObjects
        //SocketGuild server;
        #endregion

        #region channelObjects
        private SocketTextChannel generalTextChannel;
        private SocketTextChannel botChannel;
        #endregion

        private DiscordSocketClient _client;
        private CommandService _commands;

        private CommandHandler _handler;


        public static void Main(string[] args)
        {
            new Program().MainAsync().GetAwaiter().GetResult();
        }

        public async Task MainAsync()
        {
            SetDebugLevel(); //Removable


            _client = new DiscordSocketClient();
            _commands = new CommandService();
            _handler = new CommandHandler(_client, _commands);

            _client.Log += Log; //Subscribe to the Logging method
            //_client.MessageReceived += DoShit; //Moved into CommandHandler object
            //_client.Ready += BotReady; //removable

            generalTextChannel = _client.GetGuild(_ServerID).GetTextChannel(_GeneralTextID);
            //botChannel = _client.GetGuild(_ServerID).GetTextChannel(_BotChannelID);

            var token = File.ReadAllText("../../../../token.txt");

            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();
            await _client.SetStatusAsync(UserStatus.Online);

            await generalTextChannel.SendMessageAsync("TESTING");

            await Task.Delay(-1);

        }



        private Task BotReady()
        {
            Console.WriteLine("The bot is now ready");
            //Console.WriteLine(IDs["Server"]);
            
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
