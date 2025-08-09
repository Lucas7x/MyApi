using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApi.Data;
using MyApi.DTOs;
using MyApi.Models;
using MyApi.Services.Interfaces;
using MyApi.Utils;

namespace MyApi.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class PersonsController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonsController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpGet]
        public IActionResult Get([FromQuery]string? name, string? email, bool? isActive)
        {
            try
            {
                var persons = _personService.List(name, email, isActive);

                var personDtos = persons.Select(x => new PersonDTO
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
                var person = _personService.GetById(id);

                if (person == null)
                    return NotFound("Registro não encontrado");

                var personDto = new PersonDTO
                {
                    Id = person.Id,
                    Name = person.Name,
                    Email = person.Email,
                    IsActive = person.IsActive,
                    Wallets = person.Wallets.Select(w => new PersonWalletDTO
                    {
                        Id = w.Id,
                        Name = w.Name,
                        Description = w.Description,
                        Balance = w.Balance,
                        Income = w.Income
                    }).ToList()
                };

                return Ok(personDto);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        
        [HttpPost]
        public IActionResult Post([FromBody]PersonCreateDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                Person newPerson = _personService.Create(dto);

                return Ok(newPerson);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        
        [HttpPatch]
        public IActionResult Patch([FromBody] PersonUpdateDTO dto, int id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                Person updatedPerson = _personService.Update(id, dto);

                return Ok(updatedPerson);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
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
                Person deletedPerson = _personService.Delete(id);

                return Ok(deletedPerson);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
