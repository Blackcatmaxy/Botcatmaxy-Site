using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotcatmaxySite.Server.Discord;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BotcatmaxySite.Server
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await DiscordData.StartUp();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
