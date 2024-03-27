using HotelListing.DTO.Request;

namespace HotelListing.Interfaces
{
    public interface IAuthManager
    {
        Task<bool> ValidateUser(LoginUserDTO userDTO);
        Task<string> CreateToken();
        Task<string> CreateRefreshToken();
        Task<TokenRequestDTO> VerifyRefreshToken(TokenRequestDTO request);
    }
}
