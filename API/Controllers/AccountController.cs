using AutoMapper;
using Core.Dtos;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        private readonly IUnitOfWork _uow;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly SignInManager<User> _signInManager;
        private readonly ITokenService _tokenService;

        public AccountController(
            IUnitOfWork uow,
            UserManager<User> userManager,
            IMapper mapper,
            SignInManager<User> signInManager,
            ITokenService tokenService
        )
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _uow = uow;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult<UserDto>> Register(UserRegisterDto registerDto)
        {
            if (
                await _userManager.Users.AnyAsync(
                    u => u.NormalizedUserName == registerDto.UserName.ToLower()
                )
            )
                return BadRequest("This UserName already exits");
            if (
                await _userManager.Users.AnyAsync(
                    u => u.NormalizedEmail == registerDto.Email.ToUpper()
                )
            )
                return BadRequest("Email is already registered.");

            var user = _mapper.Map<User>(registerDto);
            user.LastActive = DateTime.UtcNow;

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return new UserDto
            {
                Name = user.Name,
                UserName = user.UserName!,
                Token = await _tokenService.CreateToken(user)
            };
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<UserDto>> Login(UserLoginDto loginDto)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(
                u => u.UserName == loginDto.UserName.ToLower()
            );

            if (user == null)
                return Unauthorized("No Registered User with this Username");

            var result = await _signInManager.CheckPasswordSignInAsync(
                user,
                loginDto.Password,
                false
            );

            if (!result.Succeeded)
                return BadRequest("Invalid username or password.");

            return new UserDto
            {
                UserName = user.UserName!,
                Name = user.Name,
                Token = await _tokenService.CreateToken(user)
            };
        }
    }
}
