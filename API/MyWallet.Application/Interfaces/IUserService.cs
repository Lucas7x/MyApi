using MyWallet.Application.DTOs;
using MyWallet.Application.QueryFilters;
using MyWallet.Domain.Entities;

namespace MyWallet.Application.Interfaces
{
    public interface IUserService
    {
        public UserDTO GetById(int id);
        public PaginatedResult<UserDTO> List(UserQueryFilter filter);
        public User Create(UserCreateDTO UserDto);
        public User Update(int id, UserUpdateDTO UserDto);
        public User Delete(int id);
    }
}
