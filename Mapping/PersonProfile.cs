using AutoMapper;
using MyApi.DTOs;
using MyApi.Models;

namespace MyApi.Mapping
{
    public class PersonProfile : Profile
    {
        public PersonProfile()
        {
            CreateMap<Person, PersonDTO>().ReverseMap();
            CreateMap<Wallet, PersonWalletDTO>().ReverseMap();
        }
    }
}
