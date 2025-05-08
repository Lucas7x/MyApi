using Microsoft.AspNetCore.Mvc;
using MyApi.Controllers.DTOs;
using MyApi.Data;
using MyApi.Models;

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

        [HttpGet("{id:int}")]
        public IActionResult GetById([FromRoute] int id)
        {
            try
            {
                var wallet = _context.Wallets.Find(id);

                if (wallet == null)
                    return NotFound("Registro não encontrado");

                var walletDto = new GetWalletDTO
                {
                    Id = wallet.Id,
                    Name = wallet.Name,
                    Description = wallet.Description,
                    Balance = wallet.Balance,
                    Income = wallet.Income,
                    OwnerId = wallet.OwnerId,
                    Owner = new GetPersonDTO
                    {
                        Id = wallet.Owner.Id,
                        Name = wallet.Owner.Name,
                        Email = wallet.Owner.Email,
                        IsActive = wallet.Owner.IsActive
                    }
                };

                return Ok(new
                {
                    walletDto
                });
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] CreateWalletDTO dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var owner = _context.Persons.Find(dto.OwnerId);
                if (owner == null)
                    return NotFound("Titular inválido");

                var newWallet = new Wallet
                {
                    Name = dto.Name,
                    Description = dto.Description,
                    Balance = dto.Balance,
                    Income = dto.Income,
                    OwnerId = owner.Id,
                    Owner = owner
                };

                _context.Wallets.Add(newWallet);
                _context.SaveChanges();

                return Ok(new
                {
                    id = newWallet.Id
                });
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
