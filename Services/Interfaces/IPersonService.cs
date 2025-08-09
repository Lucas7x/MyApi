using MyApi.DTOs;
using MyApi.Models;

namespace MyApi.Services.Interfaces
{
    public interface IPersonService
    {
        public Person GetById(int id);
        public List<Person> List(string? name, string? email, bool? isActive);
        public Person Create(PersonCreateDTO personDto);
        public Person Update(int id, PersonUpdateDTO personDto);
        public Person Delete(int id);

    }
}
