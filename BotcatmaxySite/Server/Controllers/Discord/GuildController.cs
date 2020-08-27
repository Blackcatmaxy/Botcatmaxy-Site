using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotcatmaxySite.Server.Discord;
using BotcatmaxySite.Shared.Discord;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BotcatmaxySite.Server.Controllers.Discord
{
    [Route("[controller]")]
    [ApiController]
    public class GuildController : ControllerBase
    {
        [HttpGet("{id}")]
        public BaseGuildInfo Get(ulong id)
        {
            return new GuildInfo(DiscordData.client.GetGuild(id));
        }
    }
}
