using System;
using System.Collections.Generic;
using System.Text;

namespace BotcatmaxySite.Shared.Discord
{
    public class BasicGuildInfo
    {
        public BaseUserInfo owner { get; set; }
        public string iconUrl { get; set; }
        public ulong id { get; set; }
        public string name { get; set; }
        public ChannelInfo[] channels { get; set; }
    }
}
