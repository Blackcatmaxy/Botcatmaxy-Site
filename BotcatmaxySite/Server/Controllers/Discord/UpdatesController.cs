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
using Discord;

namespace BotcatmaxySite.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UpdatesController : ControllerBase
    {
        [HttpGet]
        public async Task<IEnumerable<UpdateMessage>> Get()
        {
            try
            {
                var messages = await DiscordData.client.GetGuild(285529027383525376)
                    .GetTextChannel(700825496010621041)
                    .GetMessagesAsync()
                    .FlattenAsync();
                return messages.Where(m => m.Source == MessageSource.User).Take(3).Select(m => new UpdateMessage
                {
                    Content = m.Content,
                    Timestamp = m.Timestamp.DateTime
                });
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw exception;
            }
        }
    }
}
