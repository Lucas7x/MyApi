using Microsoft.AspNetCore.Mvc;
using MyWallet.Application.DTOs;
using MyWallet.Application.Interfaces;
using MyWallet.Application.QueryFilters;
using MyWallet.Domain.Entities;

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
        public IActionResult Get([FromQuery] string? sortBy, bool descending, int pageIndex, int pageSize, string? name, string? email, bool showInative, bool includeWallets)
        {
            try
            {
                PersonQueryFilter filter = new PersonQueryFilter {
                    SortBy = sortBy,
                    Descending = descending,
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    Name = name,
                    Email = email,
                    ShowInative = showInative,
                    IncludeWallets = includeWallets
                };

                var persons = _personService.List(filter);

                return Ok(persons);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById([FromRoute]int id)
        {
            try
            {
                PersonDTO person = _personService.GetById(id);

                if (person == null)
                    return NotFound("Registro não encontrado");

                return Ok(person);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
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


        [HttpPatch("{id:int}")]
        public IActionResult Patch([FromBody] PersonUpdateDTO dto, [FromRoute] int id)
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
