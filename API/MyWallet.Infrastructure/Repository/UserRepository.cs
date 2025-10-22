using AutoMapper;
using MyWallet.Application.DTOs;
using MyWallet.Application.Interfaces;
using MyWallet.Application.QueryFilters;
using MyWallet.Domain.Entities;
using MyWallet.Infrastructure.Database;

namespace MyWallet.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public UserRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public User? GetById(int id)
        {
            var user = _context.Users
                .Where(x => x.DeletedAt == null)
                .FirstOrDefault(x => x.Id == id);

            return user;
        }

        public PaginatedResult<User> List(UserQueryFilter filter)
        {
            throw new NotImplementedException();
        }

        public User Create(User user)
        {
            _context.Users.Add(user);
            return user;
        }

        public User Update(User user)
        {
            _context.Users.Update(user);
            return user;
        }

        public User Delete(User user)
        {
            user.DeletedAt = DateTime.Now;
            return user;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
