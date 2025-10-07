using MyWallet.Application.DTOs;
using MyWallet.Application.QueryFilters;
using MyWallet.Domain.Entities;

namespace MyWallet.Application.Interfaces
{
    public interface IUserRepository
    {
        PaginatedResult<User> List(UserQueryFilter filter);
        Person? GetById(int id);
        Person Create(User user);
        Person Update(User user);
        Person Delete(User user);
        void SaveChanges();
    }
}
