using System.Collections.Generic;
using System.Diagnostics;

namespace AcademyBot.Objects
{
    class Updater
    {
        public string RepoUrl { get; set; }
        public Messenger MessageService { get; set; }
        public string SolutionName { get; set; }
        private ProcessStartInfo StartInfo { get; set; }
        private Process LocalProcess { get; set; }


        public Updater(string solutionName, string repoUrl = null, Messenger messageService = null)
        {
            RepoUrl = repoUrl;
            SolutionName = solutionName;
            MessageService = messageService;
            
        }

        public void Pull()
        {
            // Downloads the newest build from git. 
            LocalProcess = new Process();
            StartInfo = new ProcessStartInfo();
            StartInfo.WindowStyle = ProcessWindowStyle.Normal; //Hidden
            StartInfo.FileName = "cmd.exe";
            StartInfo.Arguments = "/C cd ../../../ && git pull";
            LocalProcess.StartInfo = StartInfo;
            LocalProcess.Start();
        }

        public void Build()
        {
            // Compiles and builds from source
            
            LocalProcess = new Process();
            StartInfo = new ProcessStartInfo();
            StartInfo.WindowStyle = ProcessWindowStyle.Normal; //Hidden
            StartInfo.FileName = "cmd.exe";
            StartInfo.Arguments = $"/C cd ../../../ && start C:/Windows/Microsoft.NET/Framework64/v4.0.30319/msbuild.exe {SolutionName} -p:OutDir=C:/Users/thewi/source/repos/AcademyBot > C:/Users/thewi/source/repos/AcademyBot/log.txt"; // && set /p=press any key to continue && start bin/Debug/netcoreapp3.1/{SolutionName.Replace("sln", "exe")}";
            LocalProcess.StartInfo = StartInfo;
            LocalProcess.Start();
        }
    }
}
