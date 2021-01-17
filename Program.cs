using System;
using System.IO;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace AcademyBot
{
    class Program
    {
        private bool saveLog;

        private DiscordSocketClient _client;

        public static void Main(string[] args)
        {
            new Program().MainAsync().GetAwaiter().GetResult();
        }

        public async Task MainAsync()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Debug mode? (y/n) [Default 'y']");
            var response = Console.ReadLine();
            var debug = string.IsNullOrEmpty(response) || response.Contains('y') || response.Contains('Y');
            Console.WriteLine(debug ? "Debug mode on" : "Debug mode off");
            Console.ResetColor();

            _client = new DiscordSocketClient();
            _client.Log += Log; //Subscribe to the Logging method
            var token = File.ReadAllText("../../../../token.txt");

            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            await Task.Delay(-1);

        }

        private static void CompileAndUpdate()
        {
            /*
             Starts a companion program that waits for main program to exit, or force quits it, then pulls the most recent build from github, compiles it and re-launches the bot. 
             */
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
