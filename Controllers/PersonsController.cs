using Microsoft.AspNetCore.Mvc;
using MyApi.Controllers.DTOs;
using MyApi.Data;
using MyApi.Models;
using MyApi.Utils;

namespace MyApi.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class PersonsController : ControllerBase
    {
        private readonly DataContext _context;

        public PersonsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get([FromQuery]string? name, string? email, bool? isActive)
        {
            try
            {
                var users = _context.Persons.AsQueryable();

                if (!string.IsNullOrEmpty(name))
                    users = users.Where(x => x.Name.Contains(name));

                if (!string.IsNullOrEmpty(email))
                    users = users.Where(x => x.Email.Contains(email));

                if (isActive.HasValue)
                    users = users.Where(x => x.IsActive == isActive.Value);

                var userDtos = users.Select(x => new GetPersonDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    Email = x.Email,
                    IsActive = x.IsActive
                }).ToList();

                return Ok(new
                {
                    users = userDtos,
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
                var user = _context.Persons.Find(id);

                if (user == null)
                    return NotFound("Registro não encontrado");

                var userDto = new GetPersonDTO
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    IsActive = user.IsActive
                };

                return Ok(new
                {
                    userDto
                });
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]CreatePersonDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var newUser = new Person
                {
                    Name = dto.Name,
                    Email = dto.Email,
                    IsActive = true,
                };

                _context.Persons.Add(newUser);
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
        public IActionResult Patch([FromBody] UpdatePersonDTO dto, int id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                Person? user = _context.Persons.Find(id);
                
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

        [HttpDelete("{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {
            try
            {
                Person? user = _context.Persons.Find(id);

                if (user == null)
                    return NotFound();

                user.IsActive = false;

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
