using AutoMapper;
using HotelListing.Data;
using HotelListing.DTO.Request;
using HotelListing.DTO.Response;
using HotelListing.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace HotelListing.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApiUser> _userManager;
        private readonly IAuthManager _authManager;
        private readonly IMapper _mapper;
        public AccountService(IAuthManager authManager, UserManager<ApiUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _authManager = authManager;
            _mapper = mapper;
        }
        public async Task<UserManagerResponse> Register(UserDTO userDTO)
        {
            var user = _mapper.Map<ApiUser>(userDTO);
            user.UserName = userDTO.Email;
            var newUserResponse = await _userManager.CreateAsync(user, userDTO.Password);
            if (!newUserResponse.Succeeded)
            {
                return new UserManagerResponse
                {
                    IsSuccess = false,
                    Message = "Unable to register a user"
                };
            }
            await _userManager.AddToRolesAsync(user, userDTO.Roles);
            return new UserManagerResponse
            {
                IsSuccess = true,
                Message = "Registeration Completed!"
            };
        }

        public async Task<UserManagerResponse> Login(LoginUserDTO userDTO)
        {
            if (!await _authManager.ValidateUser(userDTO))
            {
                return new UserManagerResponse
                {
                    IsSuccess = false,
                    Message = "Invalid email or password"
                };
            }
            var token = await _authManager.CreateToken();
            var refreshToken = await _authManager.CreateRefreshToken();
            return new UserManagerResponse
            {
                IsSuccess = true,
                Message = "Login successful",
                Token = token,
                RefreshToken = refreshToken
            };
        }
        public async Task<TokenRequestDTO> RefreshToken(TokenRequestDTO request)
        {
            var tokenRequest = await _authManager.VerifyRefreshToken(request);
            if (tokenRequest is null) throw new InvalidOperationException("Token request is unauthorized");
            return new TokenRequestDTO
            {
                Token = tokenRequest.Token,
                RefreshToken = tokenRequest.RefreshToken
            };
        }
    }
}







