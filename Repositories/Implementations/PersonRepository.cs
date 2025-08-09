using Microsoft.EntityFrameworkCore;
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

        public Person GetById(int id)
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

        public Person Create(Person person)
        {
            _context.Persons.Add(person);

            return person;
        }

        public Person Update(Person person)
        {
            _context.Update(person);
            return person;
        }

        public Person Delete(Person person)
        {
            person.IsActive = false;

            return person;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
