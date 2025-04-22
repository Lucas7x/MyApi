using Microsoft.AspNetCore.Mvc;
using MyApi.Controllers.DTOs;
using MyApi.Data;
using MyApi.Models;

namespace MyApi.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class UsersController : ControllerBase
    {
        private readonly DataContext _context;

        public UsersController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get(string name = null)
        {
            try
            {
                var users = _context.Users.AsQueryable();

                if (!string.IsNullOrEmpty(name))
                    users = users.Where(x => x.Name.Contains(name));

                return Ok(new
                {
                    users = users,
                });
            }
            catch(Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById([FromRoute]int id)
        {
            try
            {
                var user = _context.Users.Find(id);

                if (user == null)
                    return NotFound("Registro não encontrado");

                return Ok(new
                {
                    user
                });
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]CreateUserDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var newUser = new User
                {
                    Name = dto.Name,
                    Email = dto.Email,
                    Password = dto.Password,
                    IsActive = dto.IsActive,
                };

                _context.Users.Add(newUser);
                _context.SaveChanges();

                return Ok(new
                {
                    id = newUser.Id
                });
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPatch]
        public IActionResult Patch([FromBody] UpdateUserDTO dto, int id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                User? user = _context.Users.Find(id);
                
                if (user == null)
                    return NotFound();
                
                if (dto.Name != null) user.Name = dto.Name;
                if (dto.Email != null) user.Email = dto.Email;

                _context.SaveChanges();

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
