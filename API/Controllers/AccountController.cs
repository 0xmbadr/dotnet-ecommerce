using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace API.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IUnitOfWork _uow;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;

        public AccountController(
            IUnitOfWork uow,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IMapper mapper
        )
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _uow = uow;
            _mapper = mapper;
        }
    }
}
