using System;
using System.IO;
using Newtonsoft.Json.Linq;

namespace oauth_youtube.Records
{
    public record OauthToken
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public string expires_in { get; set; }
    }
}
