using Newtonsoft.Json;
using SimpleMusicStore.Data;
using SimpleMusicStore.Web.Areas.Admin.Models;
using SimpleMusicStore.Web.Areas.Admin.Models.RecordDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SimpleMusicStore.Web.Areas.Admin.Services
{
    internal class RecordService
    {
        
        private SimpleDbContext _context;

        public RecordService(SimpleDbContext context)
        {
            _context = context;
        }

        internal void ImportFromDiscogs(string discogsUrl)
        {
            

        }

        
        
    }
}
