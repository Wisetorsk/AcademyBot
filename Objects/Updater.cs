using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcademyBot.Objects
{
    class Updater
    {
        public string RepoUrl { get; set; }
        public Messenger MessageService { get; set; }
        public Updater(string repoUrl, Messenger ?messageService = null)
        {
            RepoUrl = repoUrl;
            MessageService = messageService;
        }

        public void Pull()
        {
            // Downloads the newest build from git. 
        }
    }
}
