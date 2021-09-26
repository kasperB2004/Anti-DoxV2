using Anti_Dox.Services;
using Discord;
using Discord.Commands;
using Discord.Net;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Anti_Dox.Interactions.SlashCreator
{
    public class HelpSlashCreator
    {
        // declare the fields used later in this class
        private readonly ILogger _logger;
        private DiscordShardedClient _client;
        private readonly DiscordShardedClient _discord;
        private readonly CommandService _commands;
        private readonly IConfiguration _config;


        public HelpSlashCreator(IServiceProvider services)
        {
            // get the services we need via DI, and assign the fields declared above to them
            _discord = services.GetRequiredService<DiscordShardedClient>();
            _commands = services.GetRequiredService<CommandService>();
            _logger = services.GetRequiredService<ILogger<LoggingService>>();
            _config = services.GetRequiredService<IConfiguration>();
            _client = services.GetRequiredService<DiscordShardedClient>();
            

            // hook into these events with the methods provided below
            _discord.ShardReady += _discord_ShardReady;
            
        }

        public async Task _discord_ShardReady(DiscordSocketClient arg)
        {
            

            List<ApplicationCommandProperties> applicationCommandProperties = new();
            try
            {
                ulong GuildId = 791377762337554473;
                var InviteCommand = new SlashCommandBuilder();
                InviteCommand.WithName("invite");
                InviteCommand.WithDescription("Generates an invite link for the bot!");
                applicationCommandProperties.Add(InviteCommand.Build());
                var SupportCommand = new SlashCommandBuilder();
                SupportCommand.WithName("support");
                SupportCommand.WithDescription("Sends invite link for the support server!");
                applicationCommandProperties.Add(SupportCommand.Build());   
                var DonateCommand = new SlashCommandBuilder();
                DonateCommand.WithName("donate");
                DonateCommand.WithDescription("Wanna help out support the bot? feel free to donate");
                applicationCommandProperties.Add(DonateCommand.Build());
                await _client.Rest.BulkOverwriteGuildCommands(applicationCommandProperties.ToArray(), GuildId);
                _logger.LogInformation("Help Slash commands created");
            }
            catch (ApplicationCommandException exception)
            { 
                var json = JsonConvert.SerializeObject(exception.Error, Formatting.Indented);
                _logger.LogError($"Failed to created Help slash commands with error = {json}");
            }
        }
    }
}
