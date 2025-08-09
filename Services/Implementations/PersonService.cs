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

        public Person GetById(int id)
        {
            return _personRepository.GetById(id);
        }

        public List<Person> List(PersonQueryFilter filter)
        {
            return _personRepository.List(filter);
        }

        public Person Create(PersonCreateDTO personDto)
        {
            Person person = new Person
            {
                Name = personDto.Name,
                Email = personDto.Email,
                IsActive = true
            };

            _personRepository.Create(person);
            _personRepository.SaveChanges();

            return person;
        }
        
        public Person Update(int id, PersonUpdateDTO personDto)
        {
            Person person = _personRepository.GetById(id);
            if (person == null)
                throw new KeyNotFoundException("Pessoa não encontrada");

            if (personDto.Name != null) person.Name = personDto.Name;
            if (personDto.Email != null) person.Email = personDto.Email;

            _personRepository.Update(person);
            _personRepository.SaveChanges();

            return person;
        }

        public Person Delete(int id)
        {
            Person? person = _personRepository.GetById(id);

            if (person == null)
                throw new KeyNotFoundException("Pessoa não encontrada");

            _personRepository.Delete(person);
            _personRepository.SaveChanges();

            return person;
        }
    }
}
