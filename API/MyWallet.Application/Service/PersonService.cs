using AutoMapper;
using MyWallet.Application.DTOs;
using MyWallet.Application.Interfaces;
using MyWallet.Application.QueryFilters;
using MyWallet.Domain.Entities;

namespace MyWallet.Application.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;

        public PersonService(IPersonRepository personRepository, IMapper mapper)
        {
            _personRepository = personRepository;
            _mapper = mapper;
        }

        public PersonDTO GetById(int id)
        {
            Person? person = _personRepository.GetById(id);
            return _mapper.Map<PersonDTO>(person); 
        }

        public PaginatedResult<PersonDTO> List(PersonQueryFilter filter)
        {
            var persons = _personRepository.List(filter);
            return _mapper.Map<PaginatedResult<PersonDTO>>(persons);
        }

        public Person Create(PersonCreateDTO personDto)
        {
            Person person = _mapper.Map<Person>(personDto);

            _personRepository.Create(person);
            _personRepository.SaveChanges();

            return person;
        }
        
        public Person Update(int id, PersonUpdateDTO personDto)
        {
            Person? person = _personRepository.GetById(id);
            if (person == null)
                throw new KeyNotFoundException("Pessoa não encontrada");

            _mapper.Map(personDto, person);
            person.UpdatedAt = DateTime.Now;

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
