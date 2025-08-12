using MyApi.DTOs;
using MyApi.Models;

namespace MyApi.Repositories.Interfaces
{
    public interface IPersonRepository
    {
        PaginatedResult<PersonDTO> List(PersonQueryFilter filter);
        Person? GetById(int id);
        Person Create(Person person);
        Person Update(Person person);
        Person Delete(Person person);
        void SaveChanges();
    }
}
