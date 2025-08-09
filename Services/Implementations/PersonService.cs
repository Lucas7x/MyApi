using Microsoft.EntityFrameworkCore;
using MyApi.DTOs;
using MyApi.Models;
using MyApi.Repositories.Interfaces;
using MyApi.Services.Interfaces;

namespace MyApi.Services.Implementations
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;

        public PersonService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public Person Create(Person person)
        {
            return _personRepository.Create(person);
        }

        public Person Delete(int id)
        {
            Person? person = _personRepository.Get(id);

            if (person == null)
                throw new KeyNotFoundException("Pessoa não encontrada");

            return _personRepository.Delete(person);
        }

        public Person GetPersonById(int id)
        {
            return _personRepository.Get(id);
        }

        public List<Person> List(string? name, string? email, bool? isActive)
        {
            return _personRepository.List(name, email, isActive);
        }

        public Person UpdatePartial(int id, UpdatePersonDTO personDto)
        {
            Person person = _personRepository.Get(id);
            if (person == null)
                throw new KeyNotFoundException("Pessoa não encontrada");

            if (personDto.Name != null) person.Name = personDto.Name;
            if (personDto.Email != null) person.Email = personDto.Email;

            return _personRepository.Update(id, person);
        }
    }
}
