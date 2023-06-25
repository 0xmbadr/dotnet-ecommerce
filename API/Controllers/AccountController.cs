using AutoMapper;
using Core.Dtos;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IUnitOfWork _uow;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly SignInManager<User> _signInManager;

        public AccountController(
            IUnitOfWork uow,
            UserManager<User> userManager,
            IMapper mapper,
            SignInManager<User> signInManager
        )
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _uow = uow;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(UserRegisterDto registerDto)
        {
            var user = _mapper.Map<User>(registerDto);
            user.LastActive = DateTime.UtcNow;

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return new UserDto
            {
                Name = user.Name,
                UserName = user.UserName!,
                Token = "Will be implemented"
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(UserLoginDto loginDto)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(
                u => u.UserName == loginDto.UserName.ToLower()
            );

            if (user == null)
                return BadRequest("This user doesn't exist");

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
                Token = "Will be implemented"
            };
        }
    }
}
