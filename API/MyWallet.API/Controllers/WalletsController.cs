using Microsoft.AspNetCore.Mvc;
using MyWallet.Application.DTOs;
using MyWallet.Domain.Entities;
using MyWallet.Infrastructure.Database;

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

                var walletDtos = wallets.Select(x => new WalletDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Balance = x.Balance,
                    Income = x.Income,
                    OwnerId = x.OwnerId,
                    Owner = new PersonDTO
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

                var walletDto = new WalletDTO
                {
                    Id = wallet.Id,
                    Name = wallet.Name,
                    Description = wallet.Description,
                    Balance = wallet.Balance,
                    Income = wallet.Income,
                    OwnerId = wallet.OwnerId,
                    Owner = new PersonDTO
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
        public IActionResult Post([FromBody] WalletCreateDTO dto)
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

        [HttpPatch]
        public IActionResult Patch([FromBody] WalletUpdateDTO dto, int id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                Wallet? wallet = _context.Wallets.Find(id);

                if (wallet == null)
                    return NotFound("Carteira inválida");

                if (dto.OwnerId != null)
                {
                    Person owner = _context.Persons.Find(dto.OwnerId);
                    if (owner == null)
                        return NotFound("Titular inválido");

                    wallet.OwnerId = owner.Id;
                    wallet.Owner = owner;
                }

                if (dto.Name != null) wallet.Name = dto.Name;
                if (dto.Description != null) wallet.Description = dto.Description;
                if (dto.Income != null) wallet.Income = (double)dto.Income;

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
