using Newtonsoft.Json;
using SimpleMusicStore.Web.Areas.Admin.Models;
using SimpleMusicStore.Web.Areas.Admin.Models.RecordDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SimpleMusicStore.Web.Areas.Admin.Utilities
{
    internal static class DiscogsUtilities
    {
        private const string release = "releases";
        private const string label = "labels";
        private const string artist = "artists";

        

        private const string master = "masters";

        internal const string ValidDiscogsUrlPattern = @"https:\/\/www\.discogs\.com\/([^\/]+\/)?((release)|(master))\/[0-9]+([^\/]+)?";

        private const string DiscogsAccessKeySecret = "?key=VpQTKELQqmtSDIXYycSF&secret=cOgmwRrXvdWmubVEeKYYIuZyjyHBaQfr";

        //gets the discogs release id from the provided link
        internal static long GetDiscogsId(string discogsUrl)
        {
            var parameters = discogsUrl.ToLower().Split("/");
            var discogsId = long.Parse(string.Join("", parameters.Last().Where(char.IsNumber).ToArray()));

            //Discogs has two types of release pages - release page and master page. In case the provided url is from a master page, provide the correct release page id
            if (parameters.Any(p => p == "master"))
            {
                if (!IsValidMasterId(discogsId))
                {
                    return -1;
                }

                return Get<MasterDto>(discogsId).Main_Release;
            }

            return discogsId;
        }
        
        //gets a json object from the discogs api and returns a dto
        internal static T Get<T>(long discogsId)
        {


            var typeOfContent = "";
            if (typeof(T) == typeof(ArtistDto))
            {
                typeOfContent = artist;
            }
            else if (typeof(T) == typeof(LabelDto))
            {
                typeOfContent = label;
            }
            else if (typeof(T) == typeof(MasterDto))
            {
                typeOfContent = master;
            }
            else
            {
                typeOfContent = release;
            }
            WebClient webClient = new WebClient();
            webClient.Headers.Add("user-agent", "SimpleMusicStore");


            string content = webClient.DownloadString($"https://api.discogs.com/{typeOfContent}/{discogsId}{DiscogsAccessKeySecret}");


            return JsonConvert.DeserializeObject<T>(content);
        }


        

        
        internal static bool IsValidDiscogsId(long id)
        {
            //validates that the provided url is a real discogs release page
            try
            {
                var recordDto = Get<RecordDto>(id);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        internal static bool IsValidDiscogsUrl(string discogsUrl) => Regex.IsMatch(discogsUrl, DiscogsUtilities.ValidDiscogsUrlPattern) && IsValidDiscogsId(GetDiscogsId(discogsUrl));

        private static bool IsValidMasterId(long id)
        {
            try
            {
                var recordDto = Get<MasterDto>(id);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
