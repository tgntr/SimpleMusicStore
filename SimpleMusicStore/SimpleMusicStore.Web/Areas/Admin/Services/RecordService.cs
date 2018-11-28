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
        private SimpleDbContext _context;

        public RecordService(SimpleDbContext context)
        {
            _context = context;
        }

        public void ImportFromDiscogs(string discogsUrl)
        {
            var recordDto = GetRecord(discogsUrl);
        }

        private RecordDto GetRecord(string discogsUrl)
        {
            var releaseId = discogsUrl.Split("/").Last();

            WebClient webClient = new WebClient();
            webClient.Headers.Add("user-agent", "SimpleMusicStore");

            
            var url = $"https://api.discogs.com/releases/{releaseId}?key=VpQTKELQqmtSDIXYycSF&secret=cOgmwRrXvdWmubVEeKYYIuZyjyHBaQfr";
            string content = webClient.DownloadString($"https://api.discogs.com/releases/{releaseId}?key=VpQTKELQqmtSDIXYycSF&secret=cOgmwRrXvdWmubVEeKYYIuZyjyHBaQfr");

            return JsonConvert.DeserializeObject<RecordDto>(content);
        }
    }
}
