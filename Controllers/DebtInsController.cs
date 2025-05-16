using Microsoft.AspNetCore.Mvc;
using MyApi.Controllers.DTOs;
using MyApi.Services;

namespace MyApi.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class DebtInsController : ControllerBase
    {
        private readonly DebtInService _service;

        public DebtInsController(DebtInService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult Post([FromBody] CreateDebtInDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var debtInId = _service.Create(dto);

                return Ok(new
                {
                    id = debtInId,
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public IActionResult Get([FromQuery] string? description, int? debtorId, string? initialDate, string? finalDate)
        {
            try
            {
                var debtIns = _service.GetAll(description, debtorId, initialDate, finalDate);

                return Ok(new
                {
                    debtIns
                });
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
