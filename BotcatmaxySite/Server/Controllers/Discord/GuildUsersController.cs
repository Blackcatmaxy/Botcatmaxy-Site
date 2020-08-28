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
    public class GuildUsersController : ControllerBase
    {
        [HttpGet("{id}")]
        public IEnumerable<BaseUserInfo> Get(ulong id)
        {
            return id.AllGuildUsers();
        }

        [HttpGet("{id}/page/{page}")]
        public IEnumerable<BaseUserInfo> Get(ulong id, ushort page)
        {
            var guild = DiscordData.client.GetGuild(id);
            //Basically just gets a range of users, other solutions that might be faster are available
            var users = guild.Users.Skip(page * 100).Take(100);

            return users.Select(user => new UserInfo(user));
        }
    }
}
