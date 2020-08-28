using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotcatmaxySite.Shared.Discord;
using Discord;
using Discord.WebSocket;

namespace BotcatmaxySite.Server.Discord
{
    public class GuildInfo : BaseGuildInfo
    {
        public GuildInfo(SocketGuild guild)
        {
            iconUrl = guild.IconUrl;
            id = guild.Id;
            owner = DiscordData.GetUserInfo(guild.Id, guild.Owner.Id);
            name = guild.Name;
            MemberCount = (uint?)guild.MemberCount;
        }
    }
}
