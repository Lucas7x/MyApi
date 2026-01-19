using MyWallet.Application.DTOs;
using MyWallet.Application.QueryFilters;

namespace MyWallet.Application.Interfaces
{
    public interface IUserService
    {
        UserDTO GetById(int id);
        PaginatedResult<UserDTO> List(UserQueryFilter filter);
        UserDTO Create(UserCreateDTO userDto);
        UserDTO Update(int id, UserUpdateDTO userDto);
        UserDTO Delete(int id);
        UserDTO GetByEmail(string email);
    }
}
