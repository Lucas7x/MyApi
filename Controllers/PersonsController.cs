using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
                var persons = _context.Persons.AsQueryable();

                if (!string.IsNullOrEmpty(name))
                    persons = persons.Where(x => x.Name.Contains(name));

                if (!string.IsNullOrEmpty(email))
                    persons = persons.Where(x => x.Email.Contains(email));

                if (isActive.HasValue)
                    persons = persons.Where(x => x.IsActive == isActive.Value);

                var personDtos = persons.Select(x => new GetPersonDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    Email = x.Email,
                    IsActive = x.IsActive
                }).ToList();

                return Ok(new
                {
                    persons = personDtos,
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
                var person = _context.Persons
                    .Include(p => p.Wallets)
                    .FirstOrDefault(p => p.Id == id);

                if (person == null)
                    return NotFound("Registro não encontrado");

                var personDto = new GetPersonDTO
                {
                    Id = person.Id,
                    Name = person.Name,
                    Email = person.Email,
                    IsActive = person.IsActive,
                    Wallets = person.Wallets.Select(w => new GetPersonWalletDTO
                    {
                        Id = w.Id,
                        Name = w.Name,
                        Description = w.Description,
                        Balance = w.Balance,
                        Income = w.Income
                    }).ToList()
                };

                return Ok(new
                {
                    personDto
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

                var newPerson = new Person
                {
                    Name = dto.Name,
                    Email = dto.Email,
                    IsActive = true,
                };

                _context.Persons.Add(newPerson);
                _context.SaveChanges();

                return Ok(new
                {
                    id = newPerson.Id
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

                Person? person = _context.Persons.Find(id);
                
                if (person == null)
                    return NotFound();
                
                if (dto.Name != null) person.Name = dto.Name;
                if (dto.Email != null) person.Email = dto.Email;

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
                Person? person = _context.Persons.Find(id);

                if (person == null)
                    return NotFound();

                person.IsActive = false;

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
