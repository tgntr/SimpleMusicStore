using Microsoft.EntityFrameworkCore;
using SimpleMusicStore.Data;
using SimpleMusicStore.Models;
using SimpleMusicStore.Web.Models.Dtos;
using SimpleMusicStore.Web.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleMusicStore.Web.Services
{
    internal class AddressService :Service
    {

        public AddressService(SimpleDbContext context)
            :base(context)
        {
        }



        internal async Task AddAddress(Address address)
        {
            await _context.Addresses.AddAsync(address);
            await _context.SaveChangesAsync();
        }



        internal async Task EditAddress(AddressDto address, int addressId)
        {
            var addressToEdit = await GetAddress(addressId);

            if (address == null)
            {
                return;
            }

            addressToEdit.Country = address.Country;
            addressToEdit.City = address.City;
            addressToEdit.Street = address.Street;
            await _context.SaveChangesAsync();
        }



        internal async Task RemoveAddress (int addressId)
        {
            var address =  await GetAddress(addressId);

            if (address == null)
            {
                return;
            }

            _context.Addresses.Remove(address);
            await _context.SaveChangesAsync();
        }

        internal async Task<Address> GetAddress(int addressId)
        {
            var address = await _context.Addresses.FirstOrDefaultAsync(a => a.Id == addressId);
            return address;
        }
        
    }
}
