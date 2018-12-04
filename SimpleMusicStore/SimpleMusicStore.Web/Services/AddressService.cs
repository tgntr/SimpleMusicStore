using SimpleMusicStore.Data;
using SimpleMusicStore.Models;
using System.Linq;

namespace SimpleMusicStore.Web.Services
{
    internal class AddressService
    {

        private SimpleDbContext _context;

        public AddressService(SimpleDbContext context)
        {
            _context = context;
        }

        internal void AddAddress(Address address)
        {
            _context.Addresses.Add(address);
            _context.SaveChanges();
        }
    }
}
