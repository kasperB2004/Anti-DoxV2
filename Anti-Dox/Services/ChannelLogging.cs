using Anti_Dox.Database;
using Discord;
using Discord.WebSocket;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anti_Dox.Services
{
    public class ChannelLogging
    {
        //method takes guild and an embed and sends it in the logging channel.
        public async Task LogAction(IGuild guild, Embed embed)
        {
            //get db object
            using (var db = new AntiDoxDb())
            {
                //Get the entracance for current guild
                var Logsettings = db.LogSettings.AsNoTracking().Where(x => x.ServerId == guild.Id).FirstOrDefault();
                //if none is found return
                if (Logsettings == null)
                    return;
                //if logs are disabled return
                if (Logsettings.Enabled == false)
                    return;
                //gets channel and sends embed
                ISocketMessageChannel channel = await guild.GetChannelAsync(Logsettings.ChannelId) as ISocketMessageChannel;
                await channel.SendMessageAsync(embed: embed);
            }
        }
    }
}
