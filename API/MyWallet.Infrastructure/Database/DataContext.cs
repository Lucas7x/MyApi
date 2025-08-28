using Microsoft.EntityFrameworkCore;
using MyWallet.Domain.Entities;

namespace MyWallet.Infrastructure.Database
{
    public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<DebtIn> DebtIns { get; set; } 
    }
}
