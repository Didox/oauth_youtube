using System;
using System.IO;
using Newtonsoft.Json.Linq;

namespace oauth_youtube.Services
{
    public class OauthConfig
    {
        public OauthConfig(){
          JToken jAppSettings = JToken.Parse(File.ReadAllText(Path.Combine(Environment.CurrentDirectory,"appsettings.json")));
          this.ClientId = jAppSettings["oauth"]["client_id"].ToString();
          this.AppSecret = jAppSettings["oauth"]["app_secret"].ToString();
          this.RedirectUri = jAppSettings["oauth"]["redirect_uri"].ToString();
          this.Version = jAppSettings["oauth"]["version"].ToString();
        }
        public string ClientId { get; set; }
        public string AppSecret { get; set; }
        public string RedirectUri { get; set; }
        public string Version { get; set; }
    }
}
