using System.Globalization;
using MyWallet.Application.DTOs;
using MyWallet.Application.Interfaces;
using MyWallet.Domain.Entities;

namespace MyApi.Services
{
    public class DebtInService : IDebtInService
    {
        private readonly IDebtInRepository _debtInRepository;
        private readonly IWalletRepository _walletRepository;

        public DebtInService(IDebtInRepository debtInRepository, IWalletRepository walletRepository)
        {
            _debtInRepository = debtInRepository;
            _walletRepository = walletRepository;
        }

        public int Create(DebtInCreateDTO dto)
        {
            return 0;
            //var debtor = _debtInRepository.Find(dto.DetborId);
            //if (debtor == null)
            //    throw new ArgumentException("Devedor inválido");

            //Wallet wallet = null;
            //if (dto.WalletId != null)
            //{
            //    wallet = _walletRepository.Find(dto.WalletId);
            //    if (wallet == null)
            //        throw new ArgumentException("Carteira inválida");
            //}

            //var debtIn = new DebtIn
            //{
            //    Description = dto.Description,
            //    Amount = dto.Amount,
            //    CreatedAt = DateTime.Now,
            //    Debtor = debtor,
            //    DebtorId = debtor.Id,
            //};

            //if (wallet != null)
            //    debtIn.Pay(wallet);

            //_debtInRepositoryDebtIns.Add(debtIn);

            //_debtInRepository.SaveChanges();

            //return debtIn.Id;
        }

        public List<DebtInsDTO> GetAll(string? description, int? debtorId, string? initialDate, string? finalDate)
        {
            return new List<DebtInsDTO>();
            //var debtIns = _context.DebtIns
            //    .Include(p => p.Debtor)
            //    .AsQueryable();

            //if (description != null)
            //    debtIns = debtIns.Where(x => x.Description.Contains(description));

            //if (debtorId != null)
            //    debtIns = debtIns.Where(x => x.Debtor.Id == debtorId);

            //if (!string.IsNullOrWhiteSpace(initialDate))
            //{
            //    var dateFormat = "dd-MM-yyyy";
            //    if (!DateTime.TryParseExact(initialDate, dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var initialDateTime))
            //        throw new ArgumentException($"Data inicial inválida. Use o formato {dateFormat}.");

            //    debtIns = debtIns.Where(x => x.CreatedAt >= initialDateTime);
            //}

            //if (!string.IsNullOrWhiteSpace(finalDate))
            //{
            //    var dateFormat = "dd-MM-yyyy";
            //    if (!DateTime.TryParseExact(finalDate, dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var finalDateTime))
            //        throw new ArgumentException($"Data final inválida. Use o formato {dateFormat}.");

            //    debtIns = debtIns.Where(x => x.CreatedAt <= finalDateTime);
            //}

            //var debtInsDto = debtIns.Select(x => new DebtInsDTO
            //{
            //    Id = x.Id,
            //    Description = x.Description,
            //    Amount = x.Amount,
            //    CreatedAt = x.CreatedAt,
            //    PaidAt = x.PaidAt,
            //    Debtor = new DebtInDebtorDTO
            //    {
            //        Id = x.Debtor.Id,
            //        Name = x.Debtor.Name,
            //    },
            //    Wallet = x.Wallet != null ? new DebtInWalletDTO
            //    {
            //        Id = x.Wallet.Id,
            //        Name = x.Wallet.Name
            //    } : null,
            //});

            //return debtInsDto.ToList();
        }

        public void PayDebtIn(int debtInId, int walletId)
        {
            try
            {
                //var debtIn = _context.DebtIns.Find(debtInId);
                //if (debtIn == null)
                //    throw new ArgumentException("Dívida inválida");

                //var wallet = _context.Wallets.Find(walletId);
                //if (wallet == null)
                //    throw new ArgumentException("Carteira inválida");

                //debtIn.Pay(wallet);

                //_context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
