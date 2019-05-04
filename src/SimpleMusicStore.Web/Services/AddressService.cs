using Microsoft.EntityFrameworkCore;
using SimpleMusicStore.Data;
using SimpleMusicStore.Data.Models;
using SimpleMusicStore.Web.Models.Dtos;
using SimpleMusicStore.Web.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleMusicStore.Web.Services
{
    internal class AddressService :Service
    {

        public AddressService(SimpleMusicStoreContext context)
            :base(context)
        {
        }



        internal async Task AddAsync(Address address)
        {
            await _context.Addresses.AddAsync(address);
            await _context.SaveChangesAsync();
        }



        internal async Task EditAsync(AddressDto address, int addressId)
        {
            var addressToEdit = await GetAsync(addressId);

            if (address == null)
            {
                return;
            }

            addressToEdit.Country = address.Country;
            addressToEdit.City = address.City;
            addressToEdit.Street = address.Street;
            await _context.SaveChangesAsync();
        }



        internal async Task RemoveAsync (int addressId)
        {
            var address =  await GetAsync(addressId);

            if (address == null)
            {
                return;
            }

            address.IsActive = false;
            await _context.SaveChangesAsync();
        }

        internal async Task<Address> GetAsync(int addressId)
        {
            var address = await _context.Addresses.FirstOrDefaultAsync(a => a.Id == addressId);
            return address;
        }

        internal async Task<List<Address>> AllByUserAsync(string userId)
        {
            return await _context.Addresses.Where(a => a.UserId == userId && a.IsActive).ToListAsync();
        }
        
    }
}
