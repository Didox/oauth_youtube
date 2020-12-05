using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using oauth_youtube.Records;
using oauth_youtube.Services;

namespace oauth_youtube.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("/")]
        public ActionResult Index()
        {
            return Redirect("/swagger");
        }

        [HttpGet]
        [Route("/facebook-oauth")]
        public ActionResult FacebookOauth(string code = "")
        {
            var oauthConfig = new OauthConfig();
            if(string.IsNullOrEmpty(code))
            {
                return Redirect($"https://graph.facebook.com/oauth/authorize?client_id={oauthConfig.ClientId}&redirect_uri={oauthConfig.RedirectUri}");
            }


            // return Redirect($"https://graph.facebook.com/{oauthConfig.Version}/oauth/access_token?client_id={oauthConfig.ClientId}&redirect_uri={oauthConfig.RedirectUri}&client_secret={oauthConfig.AppSecret}&code={code}");

            string json = string.Empty;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"https://graph.facebook.com/{oauthConfig.Version}/oauth/access_token?client_id={oauthConfig.ClientId}&redirect_uri={oauthConfig.RedirectUri}&client_secret={oauthConfig.AppSecret}&code={code}");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = new StreamReader(receiveStream);
                json = readStream.ReadToEnd();
                response.Close();
                readStream.Close();
            }

            var oauthToken = JsonConvert.DeserializeObject<OauthToken>(json);

            return Redirect($"https://graph.facebook.com/me?fields=name,gender,birthday,picture.width(320).height(320)&access_token={oauthToken.access_token}");
        }
    }
}
