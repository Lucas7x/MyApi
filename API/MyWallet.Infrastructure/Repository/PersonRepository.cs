using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyWallet.Application.DTOs;
using MyWallet.Application.Interfaces;
using MyWallet.Application.QueryFilters;
using MyWallet.Domain.Entities;
using MyWallet.Infrastructure.Database;

namespace MyWallet.Infrastructure.Repository
{
    public class PersonRepository : IPersonRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public PersonRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Person? GetById(int id)
        {
            var person = _context.Persons
                    .Include(p => p.Wallets)
                    .Where(x => x.DeletedAt == null)
                    .FirstOrDefault(p => p.Id == id);

            return person;
        }

        public PaginatedResult<Person> List(PersonQueryFilter filter)
        {
            var persons = _context.Persons.AsQueryable();

            if (filter.IncludeWallets)
                persons = persons.Include(x => x.Wallets);

            if (!string.IsNullOrEmpty(filter.Name))
                persons = persons.Where(x => x.Name.ToLower().Contains(filter.Name.ToLower()));

            if (!string.IsNullOrEmpty(filter.Email))
                persons = persons.Where(x => x.Email.ToLower().Contains(filter.Email.ToLower()));

            if (!filter.ShowInative.HasValue || filter.ShowInative == false)
                persons = persons.Where(x => x.DeletedAt == null);

            PaginatedResult<Person> paginatedResult = new PaginatedResult<Person>(persons, filter.PageSize, filter.PageIndex, filter.SortBy, filter.Descending);

            return paginatedResult;
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
            person.DeletedAt = DateTime.Now;

            return person;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
