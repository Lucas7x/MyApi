using AutoMapper;
using MyWallet.Application.DTOs;
using MyWallet.Application.Interfaces;
using MyWallet.Application.QueryFilters;
using MyWallet.Domain.Entities;

namespace MyWallet.Application.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public UserDTO GetById(int id)
        {
            User? user = _userRepository.GetById(id);
            return _mapper.Map<UserDTO>(user);
        }

        public PaginatedResult<UserDTO> List(UserQueryFilter filter)
        {
            throw new NotImplementedException();
        }

        public UserDTO Create(UserCreateDTO userDto)
        {
            var user = _mapper.Map<User>(userDto);

            user = _userRepository.Create(user);
            _userRepository.SaveChanges();

            return _mapper.Map<UserDTO>(user);
        }

        public UserDTO Update(int id, UserUpdateDTO userDto)
        {
            User? user = _userRepository.GetById(id);
            if (user == null)
                throw new KeyNotFoundException("Usuário não encontrado");

            _mapper.Map(userDto, user);
            user.UpdatedAt = DateTime.Now;

            user = _userRepository.Update(user);
            _userRepository.SaveChanges();

            return _mapper.Map<UserDTO>(user);
        }

        public UserDTO Delete(int id)
        {
            User? user = _userRepository.GetById(id);
            if (user == null)
                throw new KeyNotFoundException("Usuário não encontrado.");

            user = _userRepository.Delete(user);
            _userRepository.SaveChanges();

            return _mapper.Map<UserDTO>(user);
        }
    }
}
