using AutoMapper;
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
                    .FirstOrDefault(p => p.Id == id);

            return person;
        }

        public PaginatedResult<PersonDTO> List(PersonQueryFilter filter)
        {
            var persons = _context.Persons.AsQueryable();

            if (filter.IncludeWallets)
                persons = persons.Include(x => x.Wallets);

            if (!string.IsNullOrEmpty(filter.Name))
                persons = persons.Where(x => x.Name.ToLower().Contains(filter.Name.ToLower()));

            if (!string.IsNullOrEmpty(filter.Email))
                persons = persons.Where(x => x.Email.ToLower().Contains(filter.Email.ToLower()));

            if (!filter.ShowInative.HasValue || filter.ShowInative == false)
                persons = persons.Where(x => x.IsActive == true);

            persons = ApplySortFilter(persons, filter);
            int totalItens = persons.Count();

            persons = ApplyPagination(persons, filter);

            var rows = persons.Select(x => _mapper.Map<PersonDTO>(x));

            PaginatedResult<PersonDTO> p = new PaginatedResult<PersonDTO>()
            {
                TotalItens = totalItens,
                CurrentPage = (int)filter.PageIndex,
                PageSize = (int)filter.PageSize,
                Sort = filter.SortBy + (!filter.Descending ? " asc" : " desc"),
                Rows = rows.ToList()
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

        private IQueryable<Person> ApplyPagination(IQueryable<Person> query, PersonQueryFilter filter)
        {
            return query.AsNoTracking()
                        .Skip((filter.PageIndex - 1) * filter.PageSize)
                        .Take(filter.PageSize);
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
