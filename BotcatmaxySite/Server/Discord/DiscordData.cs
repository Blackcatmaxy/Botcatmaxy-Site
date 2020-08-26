using System.Collections.Generic;
using System.Threading.Tasks;
using Discord.WebSocket;
using System.Text.Json;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Linq;
using System.Net;
using System.IO;
using RestSharp;
using Discord;
using System;
using BotcatmaxySite.Shared.Discord;

namespace BotcatmaxySite.Server.Discord
{
    public static class DiscordData
    {
        public static RestClient restClient;
        public static DiscordSocketClient client;

        public static async Task StartUp()
        {
            var config = new DiscordSocketConfig
            {
                AlwaysDownloadUsers = true,
                ConnectionTimeout = 6000,
                MessageCacheSize = 120,
                ExclusiveBulkDelete = false,
                DefaultRetryMode = RetryMode.AlwaysRetry
            };
            client = new DiscordSocketClient(config);
            client.Ready += HandleReady;
            client.Log += (log) => Task.Run(() => Console.WriteLine(log.ToString()));
            await client.LoginAsync(TokenType.Bot, HiddenInfo.mainToken);
            await client.StartAsync();
            restClient = new RestClient("https://discord.com/api");
        }

        public static UserInfo GetUserInfo(ulong guildID, ulong userID)
        {
            SocketUser user = GetUser(guildID, userID);
            /*UserInfo userInfo = new UserInfo(user);
            if (user is SocketGuildUser)
            {
                return new UserInfo(user as SocketGuildUser);
            }
            userInfo.id = userID;*/
            var userInfo = new UserInfo(user);
            return userInfo;
        }

        public static SocketUser GetUser(ulong guildID, ulong userID)
        {
            SocketUser user = client.GetGuild(guildID)?.GetUser(userID);
            //If not in guild try to get object from other guilds
            //user ??= client.GetUser(userID);
            return user;
        }

        /*public static UserData GetUserData(ulong guildID, ulong userID)
        {
            SocketGuild guild = client.GetGuild(guildID);
            if (guild == null) return null;
            UserData userData = new UserData
            {
                info = GetUserInfo(guildID, userID),
                infractions = userID.GetInfractions(guild)?.ToArray(),
                acts = userID.LoadActRecord(guild, false)?.ToArray()
            };
            return userData;
        }*/

        public static SocketGuild[] GetUserGuilds(ulong id)
        {
            var user = client.GetUser(id);
            return user?.MutualGuilds.ToArray();
        }

        public static async Task HandleReady()
        {
            _ = Task.Run(() => OnReady());
        }

        public static async Task OnReady()
        {
            Console.WriteLine("Discord ready");
            await client.DownloadUsersAsync(client.Guilds);
            Console.WriteLine("Users ready");
            if (client.Guilds.Count != client.CurrentUser.MutualGuilds.Count) Console.WriteLine("ERROR, missing guilds");
            if (client.Guilds.Any(guild => guild.GetUser(client.CurrentUser.Id) == null)) Console.WriteLine("ERROR, missing in guild(s)");
        }

        public static ulong IDFromToken(string token)
        {
            var request = new RestRequest("users/@me");
            request.AddHeader("Authorization", "Bearer " + token);
            var response = restClient.Get(request);
            UserInfo user = JsonSerializer.Deserialize<UserInfo>(response.Content);
            if (user == null) throw new HttpListenerException(401, "Token not valid");
            return user.idNum;
        }

        public static IEnumerable<UserInfo> AllUsers(this ulong guildID)
        {
            var guild = client.GetGuild(guildID);
            return guild.Users.Select(user => new UserInfo(user));
        }

        public static void Authenticate(string token, ulong guildID, GuildPermission perm, out SocketGuild guild, out SocketGuildUser gUser)
        {
            guild = client.GetGuild(guildID);
            if (guild == null) throw new HttpListenerException(404);
            gUser = guild.GetUser(IDFromToken(token));
            if (gUser == null) throw new HttpListenerException(401, "User not in guild");
            if (!(gUser.GuildPermissions.Administrator || gUser.GuildPermissions.Has(perm))) throw new HttpListenerException(401, "User missing permission");
        }

        public static async void SendMessage(this ulong channelID, string message)
        {
            var channel = client.GetChannel(channelID) as ITextChannel;
            await channel.SendMessageAsync(message);
        }
    }
}