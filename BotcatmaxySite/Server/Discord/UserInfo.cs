using BotcatmaxySite.Shared.Discord;
using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BotcatmaxySite.Server.Discord
{
    public class UserInfo : BaseUserInfo
    {
        public UserInfo(IUser user)
        {
            if (user != null)
            {
                id = user.Id.ToString();
                username = user.Username;
                avatar = user.GetAvatarUrl(ImageFormat.Png) ?? user.GetDefaultAvatarUrl();
            }
        }
    }
}
