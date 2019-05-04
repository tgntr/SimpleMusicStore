using SimpleMusicStore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleMusicStore.Web.Utilities
{
    internal abstract class Service
    {
        protected SimpleMusicStoreContext _context;
            

        internal Service(SimpleMusicStoreContext context)
        {
            _context = context;
        }
        
    }
}
