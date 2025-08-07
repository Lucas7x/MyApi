using Microsoft.EntityFrameworkCore;
using MyApi.Controllers.DTOs;
using MyApi.Data;
using MyApi.Models;
using MyApi.Repositories.Interfaces;

namespace MyApi.Repositories.Implementations
{
    public class PersonRepository : IPersonRepository
    {
        private readonly DataContext _context;

        public PersonRepository(DataContext context)
        {
            _context = context;
        }

        public Person Create(Person person)
        {
            _context.Persons.Add(person);
            _context.SaveChanges();

            return person;
        }

        public Person Delete(Person person)
        {
            person.IsActive = false;

            _context.SaveChanges();

            return person;
        }

        public Person Get(int id)
        {
            var person = _context.Persons
                    .Include(p => p.Wallets)
                    .FirstOrDefault(p => p.Id == id);

            return person;
        }

        public List<Person> List(string? name, string? email, bool? isActive)
        {
            var persons = _context.Persons.AsQueryable();

            if (!string.IsNullOrEmpty(name))
                persons = persons.Where(x => x.Name.Contains(name));

            if (!string.IsNullOrEmpty(email))
                persons = persons.Where(x => x.Email.Contains(email));

            if (isActive.HasValue)
                persons = persons.Where(x => x.IsActive == isActive.Value);

            return persons.ToList();
        }

        public Person Update(int id, Person person)
        {
            Person updatedPerson = this.Get(id);

            if (updatedPerson == null)
                return null;

            if (person.Name != null) updatedPerson.Name = person.Name;
            if (person.Email != null) updatedPerson.Email = person.Email;

            _context.SaveChanges();

            return person;
        }
    }
}
