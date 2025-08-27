using MyWallet.Application.DTOs;
using MyWallet.Application.QueryFilters;
using MyWallet.Domain.Entities;

namespace MyWallet.Application.Interfaces
{
    public interface IPersonService
    {
        public PersonDTO GetById(int id);
        public PaginatedResult<PersonDTO> List(PersonQueryFilter filter);
        public Person Create(PersonCreateDTO personDto);
        public Person Update(int id, PersonUpdateDTO personDto);
        public Person Delete(int id);

    }
}
