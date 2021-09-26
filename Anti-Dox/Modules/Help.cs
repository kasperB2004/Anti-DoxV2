using Discord;
using Discord.Commands;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Reflection;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using Anti_Dox.Database;
using Discord.WebSocket;
using Anti_Dox.CustomPreattributes;
using Discord.Net;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Anti_Dox.Services;


namespace Anti_Dox.Modules
{
    public class Help : ModuleBase
    {
        private readonly EmbedBuilder _embed;
        private readonly IConfiguration _config;
        private readonly IServiceProvider _services;
        public Help(IServiceProvider services)
        {

            _config = services.GetRequiredService<IConfiguration>();
            _services = services;
            _embed = new EmbedBuilder();

        }

        [Command("Invite")]
        [Summary("Sends the invite link to the user to invite the bot")]
        public async Task Invite()
        {
            //create embed object
            var embed = new EmbedBuilder();
            //set the color
            embed.Color = Color.Green;
            //set Description
            embed.Description = $"click [here](https://discord.com/api/oauth2/authorize?client_id=791964642234466316&permissions=2416258118&scope=bot%20applications.commands) to add the bot to your server.";
            //set messageReference and tell not to mention and send message
            MessageReference msg = new MessageReference(messageId: Context.Message.Id);
            AllowedMentions allowed = new AllowedMentions(AllowedMentionTypes.None);
            await ReplyAsync("", false, embed.Build(), null, allowed, msg).ConfigureAwait(false);
        }
        [Command("Support")]
        [Summary("Sends invite link to the support server")]
        public async Task Support()
        {
            //create embed object
            var embed = new EmbedBuilder();
            //set the color
            embed.Color = Color.Green;
            //set Description
            embed.Description = $"click [here](https://discord.gg/thTg45xutf) to join the support server.";
            //set messageReference and tell not to mention and send message
            MessageReference msg = new MessageReference(messageId: Context.Message.Id);
            AllowedMentions allowed = new AllowedMentions(AllowedMentionTypes.None);
            await ReplyAsync("", false, embed.Build(), null, allowed, msg).ConfigureAwait(false);
        }
        [Command("Donate")]
        [Summary("Wanna help out support the bot? feel free to donate")]
        public async Task Donate()
        {
            //create embed object
            var embed = new EmbedBuilder();
            //set the color
            embed.Color = Color.Green;
            //set Description
            embed.Description = $"click [here](https://discord.gg/thTg45xutf) to support the bot by donating!";
            //set messageReference and tell not to mention and send message
            MessageReference msg = new MessageReference(messageId: Context.Message.Id);
            AllowedMentions allowed = new AllowedMentions(AllowedMentionTypes.None);
            await ReplyAsync("", false, embed.Build(), null, allowed, msg).ConfigureAwait(false);
        }

    }
}
