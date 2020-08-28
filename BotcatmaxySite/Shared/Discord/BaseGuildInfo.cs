using System;
using System.Collections.Generic;
using System.Text;

namespace BotcatmaxySite.Shared.Discord
{
    public class BaseGuildInfo
    {
        public BaseUserInfo owner { get; set; }
        public string iconUrl { get; set; }
        public ulong id { get; set; }
        public string name { get; set; }
        public ChannelInfo[] channels { get; set; }
        public uint? MemberCount { get; set; }
    }
}
