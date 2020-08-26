using System;
using System.Collections.Generic;
using System.Text;

namespace BotcatmaxySite.Shared
{
    public class JSONUser
    {
        public string id { get; set; }
        public string username { get; set; }
        public string discriminator { get; set; }
        public string avatar { get; set; }
        public bool verified { get; set; }
        public string email { get; set; }
        public int flags { get; set; }
        public int premium_type { get; set; }
    }
}
