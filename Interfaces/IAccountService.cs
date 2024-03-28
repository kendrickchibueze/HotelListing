using HotelListing.Data;
using HotelListing.DTO.Request;
using HotelListing.DTO.Response;

namespace HotelListing.Interfaces
{
    public interface IAccountService
    {
        public Task<UserManagerResponse> Register(UserDTO userDTO);
        public Task<UserManagerResponse> Login(LoginUserDTO userDTO);
        public Task<TokenRequestDTO> RefreshToken(TokenRequestDTO request);       
    }
}
