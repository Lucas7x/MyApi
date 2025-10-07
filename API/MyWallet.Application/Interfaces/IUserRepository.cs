using MyWallet.Application.DTOs;
using MyWallet.Application.QueryFilters;
using MyWallet.Domain.Entities;

namespace MyWallet.Application.Interfaces
{
    public interface IUserRepository
    {
        PaginatedResult<User> List(UserQueryFilter filter);
        User? GetById(int id);
        User Create(User user);
        User Update(User user);
        User Delete(User user);
        void SaveChanges();
    }
}
