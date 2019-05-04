using SimpleMusicStore.Web.Models.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleMusicStore.Web.Models.BindingModels
{
    public class RegisterBindingModel
    {
        private List<AddressDto> addresses = new List<AddressDto>();

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Country")]
        public string Country { get; set; }
        
        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
        [Display(Name = "City")]
        public string City { get; set; }

        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 5)]
        [Required]
        [Display(Name = "Street")]
        public string Street { get; set; }



        public List<AddressDto> Addresses
        {
            get
            {
                var addresses = new List<AddressDto>();
                addresses.Add(new AddressDto { Country = this.Country, City = this.City, Street = this.Street});
                return addresses;
            }

        }

    }
    
}
