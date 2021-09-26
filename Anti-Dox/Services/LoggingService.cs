using Anti_Dox.Database;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Anti_Dox.Services
{
    public class LoggingService
    {
        // declare the fields used later in this class
        private readonly ILogger _logger;
        private DiscordShardedClient _client;
        private readonly DiscordShardedClient _discord;
        private readonly CommandService _commands;
        private readonly IConfiguration _config;


        public LoggingService(IServiceProvider services)
        {
            // get the services we need via DI, and assign the fields declared above to them
            _discord = services.GetRequiredService<DiscordShardedClient>();
            _commands = services.GetRequiredService<CommandService>();
            _logger = services.GetRequiredService<ILogger<LoggingService>>();
            _config = services.GetRequiredService<IConfiguration>();
            var client = services.GetRequiredService<DiscordShardedClient>();
            _client = client;

            // hook into these events with the methods provided below
            _discord.ShardConnected += _discord_ShardReady; ;
            _discord.Log += OnLogAsync;
            _commands.Log += OnLogAsync;
        }

        private Task _discord_ShardReady(DiscordSocketClient arg)
        {
            using (var db = new AntiDoxDb())
            {
                _client.SetStatusAsync(UserStatus.Idle);
                var statusmessage = db.Status.FirstOrDefault();
                _client.SetGameAsync(statusmessage.StatusMessage);
            }
            _logger.LogInformation($"Connected as -> [{_discord.CurrentUser}]");
            _logger.LogInformation($"We are on [{_discord.Guilds.Count}] servers");
            _logger.LogInformation($"We are using [{_discord.Shards.Count}] Shards");
            return Task.CompletedTask;
        }

        



        // this method executes on the bot being connected/ready
        // this method switches out the severity level from Discord.Net's API, and logs appropriately
        public Task OnLogAsync(LogMessage msg)
        {
            string logText = $"{msg.Source}: {msg.Exception?.ToString() ?? msg.Message}";
            switch (msg.Severity.ToString())
            {
                case "Critical":
                    {
                        _logger.LogCritical(logText);
                        break;
                    }
                case "Warning":
                    {
                        _logger.LogWarning(logText);
                        break;
                    }
                case "Info":
                    {
                        _logger.LogInformation(logText);
                        break;
                    }
                case "Verbose":
                    {
                        _logger.LogInformation(logText);
                        break;
                    }
                case "Debug":
                    {
                        _logger.LogDebug(logText);
                        break;
                    }
                case "Error":
                    {
                        _logger.LogError(logText);
                        break;
                    }
            }

            return Task.CompletedTask;
        }
    }
}