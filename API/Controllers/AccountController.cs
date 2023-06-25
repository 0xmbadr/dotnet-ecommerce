using AutoMapper;
using Core.Dtos;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IUnitOfWork _uow;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public AccountController(IUnitOfWork uow, UserManager<User> userManager, IMapper mapper)
        {
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

            return new UserDto { Name = user.Name, UserName = user.UserName! };
        }
    }
}
