using Microsoft.AspNetCore.Mvc;
using MyApi.Models;
using MyWallet.Application.DTOs;
using MyWallet.Application.Interfaces;

namespace MyApi.Controllers
{
    [ApiController()]
    [Route("[Controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthenticateService _authenticateService;

        public UsersController(IUserService userService, IAuthenticateService authenticateService)
        {
            _userService = userService;
            _authenticateService = authenticateService;
        }

        [HttpPost]
        public IActionResult Post(UserCreateDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var userExists = _authenticateService.UserExists(dto.Email);
                if (userExists)
                    return BadRequest("O e-mail informado já está sendo utilizado.");

                var createdUser = _userService.Create(dto);
                if (createdUser == null)
                    return BadRequest("Ocorreu um erro ao cadastrar usuário");

                var token = _authenticateService.GenerateToken(createdUser.Id, createdUser.Email);

                return Ok(new UserToken
                {
                    Token = token
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
