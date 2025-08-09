using MyApi.DTOs;
using MyApi.Models;

namespace MyApi.Services.Interfaces
{
    public interface IPersonService
    {
        public Person GetPersonById(int id);
        public List<Person> List(string? name, string? email, bool? isActive);
        public Person Create(Person person);
        public Person UpdatePartial(int id, PersonUpdateDTO personDto);
        public Person Delete(int id);

    }
}
