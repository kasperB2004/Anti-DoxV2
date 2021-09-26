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
    public class Staff : ModuleBase
    {
        private readonly EmbedBuilder _embed;
        private readonly IConfiguration _config;
        private readonly IServiceProvider _services;
        public Staff(IServiceProvider services)
        {

            _config = services.GetRequiredService<IConfiguration>();
            _services = services;
            _embed = new EmbedBuilder();

        }

        [Command("ban")]
        [Summary("Bans a user")]
        [RequireBotPermission(GuildPermission.BanMembers, ErrorMessage = "Missing Ban permission")]
        [RequireUserPermission(GuildPermission.BanMembers, ErrorMessage = "U don't have the permission to excute this command")]
        public async Task banUser(SocketGuildUser user,[Remainder] string reason = "No reason has been provied")
        {

            SocketGuildUser excuter = Context.User as SocketGuildUser;

            if (user.Roles.Max(r => r.Position) > excuter.Roles.Max(r => r.Position))
            {
                var embed = new EmbedBuilder();
                embed.Title = "Error";
                embed.Description = "This user is above u in the role hierarchy";
                embed.Color = Color.Red;
                MessageReference msg = new MessageReference(messageId: Context.Message.Id);
                AllowedMentions allowed = new AllowedMentions(AllowedMentionTypes.None);
                await ReplyAsync("", false, embed.Build(), null, allowed, msg).ConfigureAwait(false);
                return;
            }

            try
            {
                await Context.Guild.AddBanAsync(user, 0, reason);
                var embed = new EmbedBuilder();
                embed.Title = "User banned";
                var sb = new StringBuilder();
                sb.AppendLine($"[**{user.Username}**] got banned");
                sb.AppendLine($"");
                sb.AppendLine($"[**Reason**] = {reason}");
                embed.Description = sb.ToString();
                embed.WithCurrentTimestamp();
                embed.Color = Color.Green;
                var Log = new ChannelLogging();
                await Log.LogAction(Context.Guild, embed.Build());
            }
            catch
            {
                var embed = new EmbedBuilder();
                embed.Title = "Error";
                embed.Description = $"Failed to ban {user.Username}";
                embed.Color = Color.Red;
                MessageReference msg = new MessageReference(messageId: Context.Message.Id);
                AllowedMentions allowed = new AllowedMentions(AllowedMentionTypes.None);
                await ReplyAsync("", false, embed.Build(), null, allowed, msg).ConfigureAwait(false);
            }
        }

    }
}
