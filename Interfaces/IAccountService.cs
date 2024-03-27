using HotelListing.DTO.Response;

namespace HotelListing.Interfaces
{
    public interface IAccountService
    {
        public Task<UserManagerResponse> Register();

        public Task<UserManagerResponse> Login();

        public Task<UserManagerResponse> RefreshToken();
    }
}
