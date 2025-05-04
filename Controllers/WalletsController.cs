using Microsoft.AspNetCore.Mvc;
using MyApi.Controllers.DTOs;
using MyApi.Data;

namespace MyApi.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class WalletsController : ControllerBase
    {
        private readonly DataContext _context;

        public WalletsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] string? name, string? ownerName, int? ownerId)
        {
            try
            {
                var wallets = _context.Wallets.AsQueryable();

                if (!string.IsNullOrEmpty(name))
                    wallets = wallets.Where(x => x.Name.Contains(name));

                if (!string.IsNullOrEmpty(ownerName))
                    wallets = wallets.Where(x => x.Owner.Name.Contains(ownerName));

                if (ownerId != null)
                    wallets = wallets.Where(x => x.OwnerId == ownerId);

                var walletDtos = wallets.Select(x => new GetWalletDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Balance = x.Balance,
                    Income = x.Income,
                    OwnerId = x.OwnerId,
                    Owner = new GetPersonDTO
                    {
                        Id = x.Owner.Id,
                        Name = x.Owner.Name,
                        Email = x.Owner.Email,
                        IsActive = x.Owner.IsActive
                    }
                }).ToList();

                return Ok(new
                {
                    wallets = walletDtos
                });
            }
            catch(Exception)
            {
                return BadRequest();
            }
        }
    }
}
