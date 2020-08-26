using BotcatmaxySite.Shared.Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestSharp;
using System.Text.Json;
using System.Net;
using BotcatmaxySite.Server.Discord;
using Discord.WebSocket;

namespace BotcatmaxySite.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserInfoController : ControllerBase
    {
        [HttpGet("{token}")]
        public BaseUserInfo Get(string token)
        {
            try
            {
                return GetUserFromToken(token);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw new HttpListenerException(401, "Token not valid");
            }
        }

        [HttpGet("guilds/{ID}")]
        public BasicGuildInfo[] Get(ulong ID)
        {
            try
            {
                var guilds = DiscordData.client.GetUser(ID).MutualGuilds;
                List<BasicGuildInfo> guildInfos = new List<BasicGuildInfo>();
                foreach (SocketGuild guild in guilds)
                {
                    BasicGuildInfo guildInfo = new BasicGuildInfo()
                    {
                        iconUrl = guild.IconUrl,
                        id = guild.Id,
                        owner = DiscordData.GetUserInfo(guild.Id, guild.Owner.Id),
                        name = guild.Name
                    };
                    guildInfos.Add(guildInfo);
                }
                return guildInfos.ToArray();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw new HttpListenerException(401, "Token not valid");
            }
        }

        public static BaseUserInfo GetUserFromToken(string token)
        {
            var restClient = new RestClient("https://discord.com/api");
            var request = new RestRequest("users/@me");
            request.AddHeader("Authorization", "Bearer " + token);
            var response = restClient.Get(request);
            return JsonSerializer.Deserialize<BaseUserInfo>(response.Content);
        }
    }
}
