using AutoMapper;
using HotelListing.Controllers;
using HotelListing.Data;
using Microsoft.AspNetCore.Identity;

namespace HotelListing.Services
{
    public class AccountService
    {
        private readonly UserManager<ApiUser> _userManager;
        private readonly SignInManager<ApiUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly ILogger<AccountService> _logger;


        public AccountService(ILogger<AccountService>  logger,
            SignInManager<ApiUser> signInManager, RoleManager<IdentityRole> roleManager, UserManager<ApiUser> userManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }
    }
}
