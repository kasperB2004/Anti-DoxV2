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
using System.Threading.Tasks;
namespace Anti_Dox.Interactions.SlashHandler
{
    public class HelpSlashHandler
    {
        // declare the fields used later in this class
        private readonly ILogger _logger;
        private DiscordShardedClient _client;
        private readonly DiscordShardedClient _discord;
        private readonly CommandService _commands;
        private readonly IConfiguration _config;


        public HelpSlashHandler(IServiceProvider services)
        {
            // get the services we need via DI, and assign the fields declared above to them
            _discord = services.GetRequiredService<DiscordShardedClient>();
            _commands = services.GetRequiredService<CommandService>();
            _logger = services.GetRequiredService<ILogger<LoggingService>>();
            _config = services.GetRequiredService<IConfiguration>();
            _client = services.GetRequiredService<DiscordShardedClient>();

            _discord.SlashCommandExecuted += _discord_SlashCommandExecuted;
            
        }

        private async Task _discord_SlashCommandExecuted(SocketSlashCommand arg)
        {
            var Channel = arg.Channel as SocketGuildChannel;
            
            _logger.LogInformation($"Command [{arg.Data.Name}] executed for [{arg.User.Username}] on [{Channel.Guild.Name}] in [{Channel.Name}]");
            switch (arg.Data.Name)
            {
                case "invite":
                    await HandleInviteCommand(arg);
                    break;
                case "support":
                    await HandleSupportCommand(arg);
                    break;
                case "donate":
                    await HandleDonateCommand(arg);
                    break;
            }
        }

        private async Task HandleDonateCommand(SocketSlashCommand command)
        {
            //create embed object
            var embed = new EmbedBuilder();
            //set the color
            embed.Color = Color.Green;
            //set Description
            embed.Description = $"click [here](https://discord.gg/thTg45xutf) to support the bot by donating!";
            //send Message
            await command.RespondAsync(embed: embed.Build());
        }

        private async  Task HandleSupportCommand(SocketSlashCommand command)
        {
            //create embed object
            var embed = new EmbedBuilder();
            //set the color
            embed.Color = Color.Green;
            //set Description
            embed.Description = $"click [here](https://discord.gg/thTg45xutf) to join the support server.";
            //send Message
            await command.RespondAsync(embed: embed.Build());
        }

        private async Task HandleInviteCommand(SocketSlashCommand command)
        {
            //create embed object
            var embed = new EmbedBuilder();
            //set the color
            embed.Color = Color.Green;
            //set Description
            embed.Description = $"click [here](https://discord.com/api/oauth2/authorize?client_id=791964642234466316&permissions=2416258118&scope=bot%20applications.commands) to add the bot to your server.";
            //send Message
            await command.RespondAsync(embed: embed.Build());

        }
    }
}
