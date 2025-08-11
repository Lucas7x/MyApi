using Microsoft.EntityFrameworkCore;
using MyApi.Data;
using MyApi.DTOs;
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

        public PaginatedResult<Person> List(PersonQueryFilter filter)
        {
            var persons = _context.Persons.AsQueryable();

            if (!string.IsNullOrEmpty(filter.Name))
                persons = persons.Where(x => x.Name.ToLower().Contains(filter.Name.ToLower()));

            if (!string.IsNullOrEmpty(filter.Email))
                persons = persons.Where(x => x.Email.ToLower().Contains(filter.Email.ToLower()));

            if (!filter.ShowInative.HasValue || filter.ShowInative == false)
                persons = persons.Where(x => x.IsActive == true);

            persons = ApplySortFilter(persons, filter);

            PaginatedResult<Person> p = new PaginatedResult<Person>()
            {
                TotalItens = persons.Count(),
                CurrentPage = (int)filter.PageIndex,
                PageSize = (int)filter.PageSize,
                Sort = filter.SortBy + (filter.Descending ? " asc" : " desc"),
                Rows = persons.Skip((filter.PageIndex - 1) * filter.PageSize)
                                .Take(filter.PageSize)
                                .ToList()
            };

            return p;
        }

        private IQueryable<Person> ApplySortFilter(IQueryable<Person> query, PersonQueryFilter filter)
        {
            switch (filter.SortBy)
            {
                case "name":
                    query = filter.Descending ? query.OrderByDescending(p => p.Name) : query.OrderBy(p => p.Name);
                    break;
                case "email":
                    query = filter.Descending ? query.OrderByDescending(p => p.Email) : query.OrderBy(p => p.Email);
                    break;
                default:
                    query = filter.Descending ? query.OrderByDescending(p => p.Id) : query.OrderBy(p => p.Id);
                    break;
            }

            return query;
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
