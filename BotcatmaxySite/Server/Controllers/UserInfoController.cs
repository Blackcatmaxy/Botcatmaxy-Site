using BotcatmaxySite.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestSharp;
using System.Text.Json;
using System.Net;

namespace BotcatmaxySite.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserInfoController : ControllerBase
    {
        [HttpGet("{token}")]
        public JSONUser Get(string token)
        {
            try
            {
                var restClient = new RestClient("https://discord.com/api");
                var request = new RestRequest("users/@me");
                request.AddHeader("Authorization", "Bearer " + token);
                var response = restClient.Get(request);
                var user = JsonSerializer.Deserialize<JSONUser>(response.Content);
                return user;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw new HttpListenerException(401, "Token not valid");
            }
        }
    }
}
