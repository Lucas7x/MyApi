using AutoMapper;
using MyWallet.Application.DTOs;
using MyWallet.Domain.Entities;

namespace MyApi.Mapping
{
    public class PersonProfile : Profile
    {
        public PersonProfile()
        {
            CreateMap<Person, PersonDTO>().ReverseMap();
            CreateMap<Wallet, PersonWalletDTO>().ReverseMap();
            CreateMap<PaginatedResult<Person>, PaginatedResult<PersonDTO>>().ReverseMap();
        }
    }
}
