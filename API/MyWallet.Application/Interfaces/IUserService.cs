using MyWallet.Application.DTOs;
using MyWallet.Application.QueryFilters;
using MyWallet.Domain.Entities;

namespace MyWallet.Application.Interfaces
{
    public interface IUserService
    {
        public UserDTO GetById(int id);
        public PaginatedResult<UserDTO> List(UserQueryFilter filter);
        UserDTO Create(UserCreateDTO userDto);
        UserDTO Update(int id, UserUpdateDTO userDto);
        UserDTO Delete(int id);
    }
}
