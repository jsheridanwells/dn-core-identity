using System;
using System.Threading.Tasks;
using AuthService.Entities;
using AuthService.Models;
using AuthService.Services;
using AuthService.Util;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AuthService.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly IMapper _mapper;
        private readonly IOptions<AppSettings> _settings;

        public UsersController(IUserService service, IMapper mapper, IOptions<AppSettings> settings)
        {
            _service = service;
            _mapper = mapper;
            _settings = settings;
        }
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            try
            {
                await _service.Create(user, userDto.Password);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = e.Message });
            }
            
        }

        [AllowAnonymous]
        [HttpPost("auth")]
        public async Task<IActionResult> Authenticate([FromBody] LoginDto loginDto)
        {
            var user = _mapper.Map<User>(loginDto);
            throw new NotImplementedException();
        }

        [HttpGet]
        public IActionResult  GetAllTheData()
        {
            return Ok("congrats, you see the secret data");
        }
    }
}