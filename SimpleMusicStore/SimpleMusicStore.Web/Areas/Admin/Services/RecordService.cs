using Newtonsoft.Json;
using SimpleMusicStore.Data;
using SimpleMusicStore.Web.Areas.Admin.Models.RecordDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SimpleMusicStore.Web.Areas.Admin.Services
{
    public class RecordService
    {
        private const string release = "releases";
        private const string label = "labels";
        private const string artist = "artists";
        private SimpleDbContext _context;

        public RecordService(SimpleDbContext context)
        {
            _context = context;
        }

        public void ImportFromDiscogs(string discogsUrl)
        {
            var recordDto = Get<RecordDto>(discogsUrl);
            

        }

        private T Get<T>(string discogsUrl)
        {
            var parameters = discogsUrl.ToLower().Split("/");
            var releaseId = parameters.Last().Where(char.IsNumber).ToString();

            var typeOfContent = "";
            if (parameters.Any(p=>p=="artist"))
            {
                typeOfContent = artist;
            }
            else if (parameters.Any(p => p == "label"))
            {
                typeOfContent = label;
            }
            else if (true)
            {
                typeOfContent = release;
            }

            WebClient webClient = new WebClient();
            webClient.Headers.Add("user-agent", "SimpleMusicStore");

            
            string content = webClient.DownloadString($"https://api.discogs.com/{typeOfContent}/{releaseId}?key=VpQTKELQqmtSDIXYycSF&secret=cOgmwRrXvdWmubVEeKYYIuZyjyHBaQfr");
            
             
            return JsonConvert.DeserializeObject<T>(content);
        }
        
    }
}
