using HotelListing.DTO.Request;
using System.ComponentModel.DataAnnotations;

namespace HotelListing.DTO.Response
{
    public class UserDTO : LoginUserDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        public ICollection<string> Roles { get; set; }

    }
}
