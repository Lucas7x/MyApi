using MyWallet.Application.DTOs;
using MyWallet.Application.QueryFilters;
using MyWallet.Domain.Entities;

namespace MyWallet.Application.Interfaces
{
    public interface IPersonRepository
    {
        PaginatedResult<Person> List(PersonQueryFilter filter);
        Person? GetById(int id);
        Person Create(Person person);
        Person Update(Person person);
        Person Delete(Person person);
        void SaveChanges();
    }
}
