using Microsoft.EntityFrameworkCore.Internal;
using MyApi.Controllers.DTOs;
using MyApi.Data;
using MyApi.Models;

namespace MyApi.Services
{
    public class DebtInService
    {
        private readonly DataContext _context;

        public DebtInService(DataContext context)
        {
            _context = context;
        }

        public int Create(CreateDebtInDTO dto)
        {
            var debtor = _context.Persons.Find(dto.DetborId);
            if (debtor == null)
                throw new ArgumentException("Devedor inválido");

            Wallet wallet = null;
            if (dto.WalletId != null)
            {
                wallet = _context.Wallets.Find(dto.WalletId);
                if (wallet == null)
                    throw new ArgumentException("Carteira inválida");
            }

            var debtIn = new DebtIn
            {
                Description = dto.Description,
                Amount = dto.Amount,
                CreatedAt = DateTime.Now,
                Debtor = debtor,
                DebtorId = debtor.Id,
            };

            if (wallet != null)
                debtIn.Pay(wallet);

            _context.DebtIns.Add(debtIn);

            _context.SaveChanges();

            return debtIn.Id;
        }
    }
}
